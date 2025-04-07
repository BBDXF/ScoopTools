using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;


namespace ScoopTools
{

    public class ScoopResponseValue
    {
        public string[] keys;
        public List<string[]> valueList;
    }
    public class ScoopResponse
    {
        // app list
        // bucket list
        public static ScoopResponseValue parseToList(string resp)
        {
            var lines = resp.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var i = 0;
            var content_end = true;
            var offsets = new List<int>();
            var respList = new ScoopResponseValue();
            respList.valueList = new List<string[]>();
            while (i < lines.Length)
            {
                var line = lines[i];
                if (line.Length == 0)
                {
                    i += 1;
                    if (content_end == false)
                    {
                        break; // parse content end
                    }
                    content_end = true;
                    continue;
                }
                if (line.StartsWith("Name ") && lines[i + 1].StartsWith("---- "))
                {
                    // 查找 offset
                    respList.keys = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var k in respList.keys)
                    {
                        offsets.Add(line.IndexOf(k));
                    }
                    offsets.Add(line.Length);
                    // goto content
                    i += 2;
                    content_end = false;
                    continue;
                }
                // parse content
                if (offsets.Count > 0) { 
                    var vals = new List<string>();
                    for (var j = 0; j < offsets.Count - 1; j++)
                    {
                        var val = line.Substring(offsets[j], offsets[j + 1] - offsets[j]).Trim();
                        vals.Add(val);
                    }
                    respList.valueList.Add(vals.ToArray());
                }
                i += 1;
            }
            return respList;
        }
    }

    public class ScoopAcions
    {
        private const int HWND_BROADCAST = 0xFFFF;
        private const int WM_SETTINGCHANGE = 0x001A;
        private const uint SMTO_ABORTIFHUNG = 0x0002;
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam, uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

        public readonly string SCOOP_ENV_ROOT = "SCOOP";
        public readonly string SCOOP_ENV_GLOBAL = "SCOOP_GLOBAL";
        public readonly string SCOOP_REPO = "https://github.com/ScoopInstaller/Scoop";
        public readonly string SCOOP_DIR_APP = "apps";
        public readonly string SCOOP_DIR_BUCKET = "buckets";

        public delegate void D_UiLog(string txt);
        public D_UiLog cb_log;

        // 获取环境变量的值
        public string getEnvValue(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
        // 添加环境变量
        public void setEnvValue(string key, string val)
        {
            Environment.SetEnvironmentVariable(key, val, EnvironmentVariableTarget.User);
        }
        // 通知OS env修改了
        public void NotifyOSEnvChanged()
        {
            IntPtr result;
            SendMessageTimeout((IntPtr)HWND_BROADCAST, WM_SETTINGCHANGE, IntPtr.Zero, "Environment", SMTO_ABORTIFHUNG, 2000, out result);
        }
        // 修改 bucket url
        public void bucketModifyUrl(string bucket, string url)
        {
            var scoop_root = getEnvValue(SCOOP_ENV_ROOT);
            var bucketPath = Path.Combine(scoop_root, SCOOP_DIR_BUCKET, bucket);
            var cmd = $"&{{ cd {bucketPath};  git checkout .; git remote set-url origin {url}; git remote -v; }}";
            runPowershellCmdCB(cmd);
        }
        // 添加常见bucket
        public void bucketAddOfficial(string proxy)
        {
            var buckets = new Dictionary<string, string>();
            buckets["main"] = "https://github.com/ScoopInstaller/Main";
            buckets["extras"] = "https://github.com/ScoopInstaller/Extras";
            buckets["versions"] = "https://github.com/ScoopInstaller/Versions";
            buckets["nonportable"] = "https://github.com/ScoopInstaller/Nonportable";
            buckets["sysinternals"] = "https://github.com/niheaven/scoop-sysinternals";
            buckets["nirsoft"] = "https://github.com/ScoopInstaller/Nirsoft";

            if ( proxy!=null && proxy.Length > 0 )
            {
                var keys = buckets.Keys.ToList<string>();
                foreach(var key in keys)
                {
                    buckets[key] = $"{proxy}/{buckets[key]}";
                }
            }
            // cmd
            var cmd = "&{ ";
            
            foreach (var key in buckets.Keys)
            {
                cmd += $"scoop bucket rm {key} ;";
                cmd += $"scoop bucket add {key} {buckets[key]} ;";
            }
            cmd += "}";
            runPowershellCmdCB(cmd);
        }

        public string getScoopRootPath()
        {
            var root = getEnvValue(SCOOP_ENV_ROOT);
            if(root == null)
            {
                root = Path.Combine(getEnvValue("USERPROFILE"), "scoop");
            }
            return root;
        }

        // 本地构建最简单的bucket,下载git
        public async void installGitLocal(string proxy)
        {
            // https://github.com/xuchaoxin1375/scoop-cn/blob/master/Deploy-ScoopForCNUser/Deploy-ScoopForCNUser.md
            // 创建 tmp bucket, 然后手动下载 git 和 7z json 文件，替换下载地址
            var bucketDir = getScoopRootPath();
            bucketDir = Path.Combine(bucketDir, "buckets/tmp");
            // $env:USERPROFILE\scoop\buckets\tmp
            var gitFiles = new string[][]{
                new string[]{ "bucket/git.json", "https://raw.githubusercontent.com/ScoopInstaller/Main/master/bucket/git.json" },
                new string[]{ "scripts/git/install-context.reg", "https://raw.githubusercontent.com/ScoopInstaller/Main/master/scripts/git/install-context.reg" },
                new string[]{ "scripts/git/uninstall-context.reg", "https://raw.githubusercontent.com/ScoopInstaller/Main/master/scripts/git/uninstall-context.reg" },
                new string[]{ "scripts/git/install-file-associations.reg", "https://raw.githubusercontent.com/ScoopInstaller/Main/master/scripts/git/install-file-associations.reg" },
                new string[]{ "scripts/git/uninstall-file-associations.reg", "https://raw.githubusercontent.com/ScoopInstaller/Main/master/scripts/git/uninstall-file-associations.reg" },
                
                new string[]{ "bucket/7zip.json", "https://raw.githubusercontent.com/ScoopInstaller/Main/master/bucket/7zip.json" },
                new string[]{ "scripts/7-zip/install-context.reg", "https://raw.githubusercontent.com/ScoopInstaller/Main/master/scripts/7-zip/install-context.reg" },
                new string[]{ "scripts/7-zip/uninstall-context.reg", "https://raw.githubusercontent.com/ScoopInstaller/Main/master/scripts/7-zip/uninstall-context.reg" },
            };
            // 创建文件夹
            Directory.CreateDirectory(Path.Combine(bucketDir, "bucket"));
            Directory.CreateDirectory(Path.Combine(bucketDir, "scripts"));
            Directory.CreateDirectory(Path.Combine(bucketDir, "scripts/7-zip"));
            Directory.CreateDirectory(Path.Combine(bucketDir, "scripts/git"));
            // 下载文件
            foreach (var f in gitFiles)
            {
                var path = Path.Combine(bucketDir, f[0]);
                var url = f[1];
                if(proxy!=null && proxy.Length > 0)
                {
                    url = $"{proxy}/{url}";
                }
                var content = await HttpGet(url);
                if(content == null)
                {
                    cb_log( "文件下载失败："+url+"\r\n建议手动检查一下网络问题。\r\n");
                    return;
                }
                if (proxy != null && proxy.Length > 0 && f[0].EndsWith(".json"))
                {
                    content = content.Replace("$bucketsdir\\\\main\\\\scripts", "$bucketsdir\\\\tmp\\\\scripts");
                    content = content.Replace("\"https://github.com", $"\"{proxy}/https://github.com");
                    content = content.Replace("\"https://raw.githubusercontent.com", $"\"{proxy}/https://raw.githubusercontent.com");
                }
                File.WriteAllText(path, content);
            }
            // 安装 git 7z
            var cmd = "&{scoop install tmp/7zip; scoop install tmp/git; }";
            runPowershellCmdCB(cmd);
        }
        // powershell control
        private void _pwsh_cb(object sender, DataReceivedEventArgs e)
        {
            var output = e.Data;
            if(output != null)
            {
                cb_log(output+"\r\n");
            }
        }
        //private void _pwsh_cb_exit(object sender, EventArgs e)
        //{
        //    var output = e.ToString();
        //    //output = output.Replace("\r\n", "\n").Replace("\n", "\r\n");
        //    cb_log("Exit: "+output);
        //}
        // 启动一个powershell进程，然后捕获output
        public void runPowershellCmdCB(string cmd)
        {
            cb_log($"[cmd cb] Run `{cmd}` ...\r\n");
            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -Command \"{cmd}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                };
                //process.EnableRaisingEvents = true;
                process.OutputDataReceived += _pwsh_cb;
                process.ErrorDataReceived += _pwsh_cb;
                //process.Exited += _pwsh_cb_exit;
                try
                {
                    // 启动进程
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                }
                catch (Exception )
                {
                }
                cb_log($"[cmd cb] Done.\r\n\r\n");
            }
        }
        public string runPowershellCmd(string cmd)
        {
            string output = "";
            cb_log($"[cmd] Run `{cmd}` ...\r\n");
            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -Command \"{cmd}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                try
                {
                    // 启动进程
                    process.Start();
                    output = process.StandardOutput.ReadToEnd();
                    output = output.Replace("\r\n", "\n").Replace("\n", "\r\n");
                    process.WaitForExit();
                }
                catch (Exception)
                {
                }
                cb_log($"[cmd] Done.\r\n\r\n");
            }
            return output;
        }
        public ScoopResponseValue runScoopCmd(ScoopCmdType cmd, string arg="")
        {
            ScoopResponseValue retVal = null;
            switch (cmd)
            {
                case ScoopCmdType.APP_LIST:
                    retVal = ScoopResponse.parseToList(runPowershellCmd("scoop list"));
                    break;
                case ScoopCmdType.BUCKET_LIST:
                    retVal = ScoopResponse.parseToList(runPowershellCmd("scoop bucket list"));
                    break;
                case ScoopCmdType.SEARCH_LIST:
                    retVal = ScoopResponse.parseToList(runPowershellCmd("scoop search " +arg));
                    break;
                case ScoopCmdType.SEARCH_LIST_SS:
                case ScoopCmdType.SEARCH_LIST_APP:
                    break;
                default:
                    break;
            }
            return retVal;
        }

        public async Task<string> HttpGet(string url, int timeout=5)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(timeout);
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public async Task<List<GithubProxy>> getGithubProxyList()
        {
            var list = new List<GithubProxy>();
            var resp = await HttpGet("https://api.akams.cn/github");
            if ( resp == null || resp.Length == 0 )
            {
                return list;
            }
            var data = JObject.Parse(resp)["data"];
            foreach (JObject item in data)
            {
                var proxy = new GithubProxy();
                proxy.url = (string)item["url"];
                proxy.server = (string)item["server"];
                proxy.ip = (string)item["ip"];
                proxy.latency = (int)item["latency"];

                list.Add(proxy);
            }

            // sort by latency
            list.Sort(new GithubProxyComparer());

            return list;
        }
        public async Task<List<GithubProxy>> getGithubProxyList2()
        {
            var list = new List<GithubProxy>();
            var resp = await HttpGet("https://status.akams.cn/status/services");
            if (resp == null || resp.Length == 0)
            {
                return list;
            }
            var dt_start = resp.IndexOf("window.preloadData");
            var dt_end = resp.IndexOf("</script>", dt_start);
            var dt_index1 = resp.IndexOf(@"'name':'GitHub \u516C\u76CA\u4EE3\u7406'", dt_start);
            while(dt_index1>0 && dt_index1 < dt_end)
            {
                var dt_index2 = resp.IndexOf("'url':'", dt_index1);
                var dt_index3 = resp.IndexOf("'", dt_index2 + 7);
                if(dt_index2>0 && dt_index3 > 0)
                {
                    var url = resp.Substring(dt_index2 + 7, dt_index3 - dt_index2 - 7);
                    dt_index1 = dt_index3 + 1;
                    url = System.Text.RegularExpressions.Regex.Unescape(url);
                    if (url.EndsWith("/"))
                    {
                        url.Substring(0, url.Length - 1);
                    }

                    var proxy = new GithubProxy();
                    proxy.url = url;
                    list.Add(proxy);
                }
                else
                {
                    break;
                }
            }

            // sort by latency
            //list.Sort(new GithubProxyComparer());

            return list;
        }
        public async Task<int> checkUrlDelay(string url)
        {
            var delay = 9999; // timeout
            DateTime cur = DateTime.Now;
            var testUrl = $"{url}/https://raw.githubusercontent.com/microsoft/vscode/main/resources/linux/code.png?t={cur.Second}";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(3);
                    HttpResponseMessage response = await client.GetAsync(testUrl);
                    response.EnsureSuccessStatusCode();
                    //_ = response.StatusCode;
                    delay = (DateTime.Now - cur).Milliseconds;
                }
            }
            catch (Exception )
            {
            }
            return delay;
        }
    }
    public class GithubProxy
    {
        public string url;
        public string ip;
        public string server;
        public int latency;
    }
    public class GithubProxyComparer : IComparer<GithubProxy>
    {
        public int Compare(GithubProxy x, GithubProxy y)
        {
            return x.latency.CompareTo(y.latency);
        }
    }
    public enum ScoopCmdType
    {
        NONE,
        APP_LIST, // scoop list
        BUCKET_LIST, // scoop bucket list
        SEARCH_LIST, // scoop search xxxx
        SEARCH_LIST_SS, // ss xxx
        SEARCH_LIST_APP, // scoop_search xxxx 
    };

}
