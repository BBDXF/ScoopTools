using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

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
                    if (content_end == false)
                    {
                        break; // parse content end
                    }
                    i += 1;
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
                var vals = new List<string>();
                for (var j = 0; j < offsets.Count - 1; j++)
                {
                    var val = line.Substring(offsets[j], offsets[j + 1] - offsets[j]).Trim();
                    vals.Add(val);
                }
                respList.valueList.Add(vals.ToArray());
                i += 1;
            }
            return respList;
        }
    }
    public class ScoopAcions
    {
        public string Scoop_root; // D:\scoop\root
        public readonly string SCOOP_PWSH_AUTH = "Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser";
        public readonly string SCOOP_ENV_ROOT = "SCOOP";
        public readonly string SCOOP_ENV_GLOBAL = "SCOOP_GLOBAL";
        public readonly string SCOOP_REPO = "https://github.com/ScoopInstaller/Scoop";
        public readonly string SCOOP_DIR_APP = "apps";
        public readonly string SCOOP_DIR_BUCKET = "buckets";

        // 处理powershell应答的标记
        private ScoopCmdType _pwsh_cmd_type = ScoopCmdType.NONE;


        // 获取环境变量的值
        public string getEnvValue(string key)
        {
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);
        }
        // 添加环境变量
        public void setEnvValue(string key, string val)
        {
            Environment.SetEnvironmentVariable(key, val, EnvironmentVariableTarget.User);
        }
        // 修改 bucket url
        public string bucketModifyUrl(string bucket, string url)
        {
            var scoop_root = getEnvValue(SCOOP_ENV_ROOT);
            var bucketPath = Path.Combine(scoop_root, SCOOP_DIR_BUCKET, bucket);
            var cmd = $"&{{ cd {bucketPath}; git remote set-url origin {url}; git remote -v }}";
            var output = runPowershellCmd(cmd);
            output = output.Replace("\n", "\r\n");
            return output;
        }
        // 添加常见bucket
        public string bucketAddOfficial(string proxy)
        {
            var buckets = new Dictionary<string, string>();
            buckets["main"] = "https://github.com/ScoopInstaller/Main";
            buckets["extras"] = "https://github.com/ScoopInstaller/Extras";
            buckets["versions"] = "https://github.com/ScoopInstaller/Versions";
            buckets["nonportable"] = "https://github.com/ScoopInstaller/Nonportable";

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
                cmd += $"scoop bucket add {key} {buckets[key]} ;";
            }
            cmd += "}";
            var output = runPowershellCmd(cmd);
            output = output.Replace("\n", "\r\n");
            return output;
        }
        // powershell control
        // 启动一个powershell进程，然后捕获output
        public string runPowershellCmd(string cmd)
        {
            string output = null;
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

                    // 读取标准输出
                    output = process.StandardOutput.ReadToEnd();
                    // 读取错误输出
                    string error = process.StandardError.ReadToEnd();

                    // 等待进程结束
                    process.WaitForExit();

                    // 检查是否有错误信息
                    if (!string.IsNullOrEmpty(error))
                    {
                        //output = $"执行命令时发生错误: {error}\r\n";
                        output = null;
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常
                    //output = $"发生异常: {ex.Message}";
                    output = null;
                }
            }
            return output;
        }
        public ScoopResponseValue runScoopCmd(ScoopCmdType cmd, string arg="")
        {
            _pwsh_cmd_type = cmd;
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
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP 请求出错: {e.Message}");
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
