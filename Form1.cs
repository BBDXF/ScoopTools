using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Text.RegularExpressions;

namespace ScoopTools
{
    public partial class FormMain : Form
    {
        private ScoopAcions scoopActions = new ScoopAcions();
        private List<GithubProxy> proxyList = new List<GithubProxy>();
        private ScoopResponseValue bucketList = new ScoopResponseValue();
        private ScoopResponseValue appList = new ScoopResponseValue();


        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            scoopActions.cb_log += Log;
            var proxy = config_get("proxy");
            if (proxy != null)
            {
                textBox_proxy_url.Text = proxy;
            }
        }

        private void Log(string txt)
        {
            if (textBox_log.InvokeRequired)
            {
                textBox_log.Invoke(new Action(() =>
                {
                    textBox_log.AppendText(txt);
                    Application.DoEvents();
                }));
            }
            else
            {
                textBox_log.AppendText(txt);
                Application.DoEvents();
            }
        }

        private void config_save(string key, string val)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings.AllKeys.Contains(key))
            {
                config.AppSettings.Settings[key].Value = val;
            }
            else
            {
                config.AppSettings.Settings.Add(key, val);
            }
            // Save the configuration file.    
            config.AppSettings.SectionInformation.ForceSave = true;
            config.Save(ConfigurationSaveMode.Modified);
            // Force a reload of the changed section.    
            ConfigurationManager.RefreshSection("appSettings");
        }
        private string config_get(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
        private async void button_proxy_check_best_Click(object sender, EventArgs e)
        {
            Log("\r\n开始测试 github Proxy 可用性...\r\n");
            for (var i = 0; i < proxyList.Count; i++)
            {
                var p = proxyList[i];
                var delay = await scoopActions.checkUrlDelay(p.url);
                p.latency = delay;
                label_status.Text = $"[{i + 1}/{proxyList.Count}] {p.url} => {delay}ms ...";
            }

            // sort
            proxyList.Sort(new GithubProxyComparer());

            // print
            Log($"\r\n- 序号 - server ----- 实际延时 - URL --------\r\n");
            for (var i = 0; i < proxyList.Count; i++)
            {
                var p = proxyList[i];
                Log($" [{(i + 1).ToString("D3")}]  {p.server.PadRight(16)} {p.latency}\t{p.url}\r\n");
            }

            if (proxyList.Count > 0)
            {
                textBox_proxy_url.Text = proxyList[0].url;
                label_status.Text = "已选择最佳proxy!";
            }
        }

        private async void button_proxy_get_Click(object sender, EventArgs e)
        {
            Log("\r\n正在请求可用github Proxy...\r\n");
            proxyList = await scoopActions.getGithubProxyList();
            Log($"获取成功，一共 [ {proxyList.Count} ] 记录:\r\n\r\n- 序号 - server ----- 理论延时 - URL --------\r\n");
            for (var i = 0; i < proxyList.Count; i++)
            {
                var p = proxyList[i];
                Log($" [{(i + 1).ToString("D3")}]  {p.server.PadRight(16)} {p.latency}\t{p.url}\r\n");
            }

            if (proxyList.Count > 0)
            {
                textBox_proxy_url.Text = proxyList[0].url;
                label_status.Text = "已选择第一个proxy，建议测试最佳proxy后使用，或者手动填写!";
            }
        }

        private void button_log_clear_Click(object sender, EventArgs e)
        {
            textBox_log.Text = "";
        }

        private void button_proxy_set_Click(object sender, EventArgs e)
        {
            var proxy = textBox_proxy_url.Text;
            if (proxy.Length == 0 || !proxy.StartsWith("http"))
            {
                MessageBox.Show("请设置一个有效的Proxy地址！\r\n【提示：】可以使用获取Github Proxy自动查找可用Proxy地址！", "参数错误");
                return;
            }
            var url = $"{proxy}/{scoopActions.SCOOP_REPO}";
            scoopActions.runPowershellCmd($"scoop config scoop_repo {url}");
            Log($"设置 scoop_repo : {url} \r\n");
        }

        private void button_proxy_info_Click(object sender, EventArgs e)
        {
            var output = scoopActions.runPowershellCmd($"scoop config");
            Log($"当前 scoop_repo : {output} \r\n");
        }

        private void button_proxy_reset_Click(object sender, EventArgs e)
        {
            var url = $"{scoopActions.SCOOP_REPO}";
            scoopActions.runPowershellCmd($"scoop config scoop_repo {url}");
            Log($"设置 scoop_repo : {url} \r\n");
        }

        private async void button_install_official_Click(object sender, EventArgs e)
        {
            Log($"【Official】正在安装，url: https://get.scoop.sh ... 【耗时很长，耐心等待!】\r\n");
            var cmd = "Invoke-RestMethod -Uri https://get.scoop.sh | Invoke-Expression";
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });
            MessageBox.Show("安装完成，因为OS限制，建议！！重启软件！！后再使用其他功能！");
        }

        private async void button_install_pre_Click(object sender, EventArgs e)
        {
            Log($"【准备】正在设置powershell选项...\r\n");
            var cmd = "Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser";
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });

            // 读取默认设置
            Log($"\r\n当前 Scoop 存储路径为：【{scoopActions.getScoopRootPath()}】，请确保这是你期望的安装位置!\r\n");
        }

        private async void button_install_official_proxy_Click(object sender, EventArgs e)
        {
            // proxy下载官方脚本
            var proxy = textBox_proxy_url.Text;
            if (proxy.Length == 0 || !proxy.StartsWith("http"))
            {
                MessageBox.Show("请设置一个有效的Proxy地址！\r\n【提示：】可以使用获取Github Proxy自动查找可用Proxy地址！", "参数错误");
                return;
            }
            Log($"【准备1】正在下载官方脚本...\r\n");
            var url = $"{proxy}/https://raw.githubusercontent.com/ScoopInstaller/Install/master/install.ps1";
            var content = await scoopActions.HttpGet(url);
            // 修改脚本中的github地址，添加proxy
            Log($"【准备2】为官方脚本下载地址增加proxy...\r\n");
            var oldstr = "https://github.com/ScoopInstaller/";
            content = content.Replace(oldstr, $"{proxy}/{oldstr}");
            var filepath = Path.GetFullPath("./scoop_install.ps1");
            File.WriteAllText(filepath, content);
            // 执行
            Log($"【安装】正在执行安装指令...\r\n");
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB($"&{filepath}");
            });
            MessageBox.Show("安装完成，因为OS限制，建议！！重启软件！！后再使用其他功能！");
        }

        private void button_install_env_clear_Click(object sender, EventArgs e)
        {
            scoopActions.setEnvValue(scoopActions.SCOOP_ENV_GLOBAL, null);
            scoopActions.setEnvValue(scoopActions.SCOOP_ENV_ROOT, null);
            scoopActions.NotifyOSEnvChanged();
            MessageBox.Show("设置完成，因为OS限制，建议！！重启软件！！后再使用其他功能！");
        }

        private void button_install_env_set_Click(object sender, EventArgs e)
        {
            using (var dirDiag = new FolderBrowserDialog())
            {
                dirDiag.Description = "请选择一个目录作为根路径：";
                dirDiag.ShowNewFolderButton = true;
                dirDiag.RootFolder = Environment.SpecialFolder.MyComputer;

                if (dirDiag.ShowDialog() == DialogResult.OK)
                {
                    var root = dirDiag.SelectedPath;
                    Log($"scoop 根路径为：{root}\r\n");
                    var rootDir = Path.Combine(root, "root");
                    var globalDir = Path.Combine(root, "global");
                    Directory.CreateDirectory(rootDir);
                    scoopActions.setEnvValue(scoopActions.SCOOP_ENV_ROOT, rootDir);
                    Log($"scoop root   创建成功：{rootDir}\r\n");
                    Directory.CreateDirectory(globalDir);
                    scoopActions.setEnvValue(scoopActions.SCOOP_ENV_GLOBAL, globalDir);
                    Log($"scoop global 创建成功：{globalDir}\r\n");

                    scoopActions.NotifyOSEnvChanged();

                    MessageBox.Show("设置完成，因为OS限制，建议！！重启软件！！后再使用其他功能！");
                }
            }
        }

        private async void button_bucket_modify_url_Click(object sender, EventArgs e)
        {
            Log($"\r\n开始修改 bucket git url\r\n");
            await Task.Run(() =>
            {
                scoopActions.bucketModifyUrl(textBox_bucket_name.Text, textBox_bucket_url.Text);
            });
        }
        private string fmtBucketList(string[] list)
        {
            if (list == null || list.Length != 4) return "";
            return $"{list[0].PadRight(12)} {list[1].PadRight(48)} {list[2].PadRight(18)} {list[3].PadLeft(10)}";
        }
        private async void button_bucket_list_Click(object sender, EventArgs e)
        {
            Log("\r\n正在请求 bucket list...\r\n");
            await Task.Run(() =>
            {
                bucketList = scoopActions.runScoopCmd(ScoopCmdType.BUCKET_LIST);
            });
            comboBox_bucket_list.Items.Clear();
            if (bucketList == null)
            {
                Log("bucket list is null, 安装 bucket 后再使用.\r\n");
                return;
            }
            Log($"获取成功，一共 [ {bucketList.valueList.Count} ] 记录:\r\n\r\n 序号 {fmtBucketList(bucketList.keys)}\r\n");

            for (var i = 0; i < bucketList.valueList.Count; i++)
            {
                var p = bucketList.valueList[i];
                comboBox_bucket_list.Items.Add(p[0]);
                Log($"[{(i + 1).ToString("D3")}] {fmtBucketList(p)}\r\n");
            }

            if (comboBox_bucket_list.Items.Count > 0)
            {
                comboBox_bucket_list.SelectedIndex = 0;
            }
        }

        private void comboBox_bucket_list_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_bucket_list.Items.Count == 0) return;

            var index = comboBox_bucket_list.SelectedIndex;
            var b = bucketList.valueList[index];
            textBox_bucket_name.Text = b[0];
            textBox_bucket_url.Text = b[1];
            textBox_bucket_update.Text = b[2];
            textBox_bucket_apps.Text = b[3];
        }

        private async void button_bucket_del_Click(object sender, EventArgs e)
        {
            var msg = $"你确定要【删除】bucket {textBox_bucket_name.Text} 吗?";
            if (MessageBox.Show(msg, "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Log($"正在删除 bucket...\r\n");
                var cmd = $"scoop bucket rm {textBox_bucket_name.Text}";
                await Task.Run(() =>
                {
                    scoopActions.runPowershellCmdCB(cmd);
                });

                // rm from list
                bucketList.valueList.RemoveAt(comboBox_bucket_list.SelectedIndex);
                comboBox_bucket_list.Items.RemoveAt(comboBox_bucket_list.SelectedIndex);
                if (comboBox_bucket_list.Items.Count > 0)
                    comboBox_bucket_list.SelectedIndex = 0;
            }
        }

        private void button_buckt_url_proxy_Click(object sender, EventArgs e)
        {
            var proxy = textBox_proxy_url.Text;
            if (proxy.Length == 0 || !proxy.StartsWith("http"))
            {
                MessageBox.Show("请设置一个有效的Proxy地址！\r\n【提示：】可以使用获取Github Proxy自动查找可用Proxy地址！", "参数错误");
                return;
            }
            if (textBox_bucket_url.Text.StartsWith(proxy)) return;
            textBox_bucket_url.Text = $"{proxy}/{textBox_bucket_url.Text}";
        }

        private async void button_install_git_Click(object sender, EventArgs e)
        {
            string proxy = null;
            Log($"正在添加 tmp bucket ...\r\n");
            Log($"正在安装 tmp/7z tmp/git ...\r\n");
            await Task.Run(() =>
            {
                scoopActions.installGitLocal(proxy);
            });
            Log("==> Git,7z 已完成安装，可以手动删除 tmp bucket.\r\n");
        }

        private async void button_install_git_proxy_Click(object sender, EventArgs e)
        {
            var proxy = textBox_proxy_url.Text;
            if (proxy.Length == 0 || !proxy.StartsWith("http"))
            {
                MessageBox.Show("请设置一个有效的Proxy地址！\r\n【提示：】可以使用获取Github Proxy自动查找可用Proxy地址！", "参数错误");
                return;
            }
            Log($"正在添加 tmp bucket 【+ Proxy】 ...\r\n");
            Log($"正在安装 tmp/7z tmp/git ...\r\n");
            await Task.Run(() =>
            {
                scoopActions.installGitLocal(proxy);
            });
            Log("==> Git,7z 已完成安装，可以手动删除 tmp bucket.\r\n");
        }


        private async void button_bucket_official_Click(object sender, EventArgs e)
        {
            var msg = "此行为会先删除原有的 main extras versions 等buckets, 然后重新添加！";
            if (MessageBox.Show(msg, "警告", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            string proxy = null;
            Log($"正在添加 official buckets...\r\n");
            await Task.Run(() =>
            {
                scoopActions.bucketAddOfficial(proxy);
            });
        }
        private async void button_bucket_install_official_Click(object sender, EventArgs e)
        {
            var msg = "此行为会先删除原有的 main extras versions 等buckets, 然后重新添加！";
            if (MessageBox.Show(msg, "警告", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            var proxy = textBox_proxy_url.Text;
            if (proxy.Length == 0 || !proxy.StartsWith("http"))
            {
                MessageBox.Show("请设置一个有效的Proxy地址！\r\n【提示：】可以使用获取Github Proxy自动查找可用Proxy地址！", "参数错误");
                return;
            }
            // add
            Log($"正在添加 proxy bucket...\r\n");
            await Task.Run(() =>
            {
                scoopActions.bucketAddOfficial(proxy);
            });
        }
        private string fmtAppList(string[] list)
        {
            if (list == null || list.Length != 5) return "";
            return $"{list[0].PadRight(20)} {list[1].PadRight(16)} {list[2].PadRight(12)} {list[3].PadRight(20)}";
        }
        private async void button_app_list_Click(object sender, EventArgs e)
        {
            Log("\r\n正在请求 app list...\r\n");
            await Task.Run(() =>
            {
                appList = scoopActions.runScoopCmd(ScoopCmdType.APP_LIST);
            });
            if (appList == null)
            {
                Log("app list is null, 请安装 app 后再使用.\r\n");
                return;
            }
            checkedListBox_app_list.Items.Clear();
            Log($"获取成功，一共 [ {appList.valueList.Count} ] 记录:\r\n\r\n 序号 {fmtAppList(appList.keys)}\r\n");

            for (var i = 0; i < appList.valueList.Count; i++)
            {
                var p = appList.valueList[i];
                var fmt = fmtAppList(p);
                checkedListBox_app_list.Items.Add(fmt);
                Log($"[{(i + 1).ToString("D3")}] {fmt}\r\n");
            }
        }

        private async void button_app_update_Click(object sender, EventArgs e)
        {
            var cmd = "scoop update ";
            var count = 0;
            for (var i = 0; i < checkedListBox_app_list.Items.Count; i++)
            {
                if (checkedListBox_app_list.GetItemChecked(i))
                {
                    count += 1;
                    cmd += $"{appList.valueList[i][0]} ";
                }
            }
            if (count == 0)
            {
                cmd += "*"; //all
            }
            Log($"\r\n正在执行 {cmd} ...\r\n");
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });

            button_app_list.PerformClick();
        }

        private async void button_app_bucket_update_Click(object sender, EventArgs e)
        {
            Log("\r\n正在请求 scoop update & status 【此行为非常耗时，耐心等待或者解决Proxy后再试】...\r\n");
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB("scoop update");
                scoopActions.runPowershellCmdCB("scoop status");
            });
        }

        private async void button_app_uninstall_Click(object sender, EventArgs e)
        {
            var cmd = "scoop uninstall ";
            var count = 0;
            for (var i = 0; i < checkedListBox_app_list.Items.Count; i++)
            {
                if (checkedListBox_app_list.GetItemChecked(i))
                {
                    count += 1;
                    cmd += $"{appList.valueList[i][0]} ";
                }
            }
            if (count == 0)
            {
                Log($"\r\n没有选择任何软件，退出操作 ...\r\n");
                return;
            }
            var msg = $"你确定要卸载软件吗？\r\n 【{cmd}】";
            if (MessageBox.Show(msg, "警告", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Log($"\r\n正在执行 {cmd} ...\r\n");
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });

            button_app_list.PerformClick();
        }

        private async void button_search_Click(object sender, EventArgs e)
        {
            textBox_search_bucket.Text = "";
            textBox_search_json.Text = "";

            var app = textBox_search.Text.Trim().ToLower();
            if (app.Length == 0)
            {
                MessageBox.Show("请输入一个有效的名称！");
                return;
            }

            var cmd = "scoop search ";
            if (radioButton_ss_old.Checked)
            {
                cmd = "scoop search ";
            }
            if (radioButton_ss_go.Checked)
            {
                cmd = "scoop-search ";
            }
            cmd += app;
            if (!radioButton_ss_soft.Checked)
            {
                Log($"\r\n开始执行搜索指令 {cmd} ，此行为耗时很长，耐心等待...\r\n");
                await Task.Run(() =>
                {
                    scoopActions.runPowershellCmdCB(cmd);
                });
            }
            else
            {

                string result_bucket = null;
                string result_json = null;
                label_status.Text = "开始搜索 " + app;
                await Task.Run(() =>
                {
                    // soft search logic
                    var root = scoopActions.getScoopRootPath();
                    var search_results = new Dictionary<string, List<string>>();
                    Log($"\r\n开始执行软件搜索，耐心等待...\r\n");
                    var dirList = Directory.EnumerateDirectories(root + "\\buckets").ToList(); // 全路径
                                                                                               //官方仓库优先
                    var buckets_first = new string[] { "main", "extras", "versions", "nonportable", "sysinternals", "nirsoft" };
                    for (var i = dirList.Count - 1; i >= 0; i--)
                    {
                        var dirname = Path.GetFileName(dirList[i]);
                        if (buckets_first.Contains(dirname))
                        {
                            // find logic
                            Log($"bucket [{dirname}]:\r\n");
                            var json = search_app_json(app, Path.Combine(root, "buckets", dirname, "bucket"));
                            if (json != null && json.Count > 0)
                            {
                                search_results.Add(dirname, json);
                            }
                            // remove it
                            dirList.RemoveAt(i);
                        }
                    }
                    // 然后其他bucket
                    for (var i = 0; i < dirList.Count; i++)
                    {
                        var dirname = Path.GetFileName(dirList[i]);
                        // find logic
                        Log($"bucket [{dirname}]:\r\n");
                        var json = search_app_json(app, Path.Combine(root, "buckets", dirname, "bucket"));
                        if (json != null && json.Count > 0)
                        {
                            search_results.Add(dirname, json);
                        }
                    }
                    Log($"遍历完成，匹配最相似结果...\r\n");


                    // 查找最相似
                    foreach (var b in buckets_first)
                    {
                        if (search_results.Keys.Contains(b))
                        {
                            var json = search_results[b].Find(val => Path.GetFileNameWithoutExtension(val) == app);
                            if (json != null && json.Length > 0)
                            {
                                result_bucket = b;
                                result_json = Path.GetFileName(json);
                                break;
                            }
                        }
                    }
                    // 再次到第三方bucket里查找
                    if (result_json == null)
                    {
                        foreach (var k in search_results.Keys)
                        {
                            if (buckets_first.Contains(k)) continue;
                            var json = search_results[k].Find(val => Path.GetFileNameWithoutExtension(val) == app);
                            if (json != null && json.Length > 0)
                            {
                                result_bucket = k;
                                result_json = Path.GetFileName(json);
                                break;
                            }
                        }
                    }
                });

                // result
                if (result_json == null)
                {
                    Log("没有找到名称完全匹配的软件，请根据搜索结果修改查询条件后，再试!\r\n");
                }
                else
                {
                    Log($"找到的最佳选项为 【{result_bucket}】 {result_json}\r\n");
                    textBox_search_bucket.Text = result_bucket;
                    textBox_search_json.Text = result_json;

                    // read app info
                    var name = result_json.Substring(0, result_json.Length - 5);
                    await Task.Run(() =>
                    {
                        scoopActions.runPowershellCmdCB($"scoop info {result_bucket}/{name}");
                    });
                }

                label_status.Text = "搜索完成";
            }
        }

        public List<string> search_app_json(string app, string bucketPath)
        {
            var result_json = new List<string>();
            var index = 0;
            foreach (var key in Directory.EnumerateFiles(bucketPath, "*.json"))
            {
                var f = Path.GetFileNameWithoutExtension(key);
                //Debug.WriteLine($"{f} vs {app}  in {key}\r\n");
                if (f.Contains(app))
                {
                    result_json.Add(key);
                    index += 1;
                    Log($"  {index.ToString("D2")}. {f}\r\n");
                }
                else if (app.Contains(' ') || app.Contains('-') || app.Contains('_'))
                {
                    var key_words = app.Split(new char[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
                    if (key_words.All(val => f.Contains(val)))
                    {
                        result_json.Add(key);
                        index += 1;
                        Log($"  {index.ToString("D2")}. {f}\r\n");
                    }
                }
            }
            Log("\r\n");
            return result_json;
        }

        private async void button_search_install_Click(object sender, EventArgs e)
        {
            var cmd = "scoop install ";
            var app = textBox_search.Text.Trim();
            if (app.Length == 0)
            {
                MessageBox.Show("请输入一个有效的名称！");
                return;
            }
            cmd += app;
            Log($"\r\n开始执行安装指令 {cmd} ，此行为耗时很长，耐心等待...\r\n");
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });
        }
        private string appJsonModifyProxy(string text, string proxy_url)
        {
            // 原生仓库json
            //var content = text.Replace("\"https://github.com", $"\"{proxy}/https://github.com");
            //content = content.Replace("\"https://raw.githubusercontent.com", $"\"{proxy}/https://raw.githubusercontent.com");

            // scoop-cn 等被修改过的
            var content = text;
            var proxy = proxy_url;
            if (proxy != null && proxy.StartsWith("http") && !proxy.EndsWith("/"))
            {
                proxy += "/";
            }
            // github.com
            var regex1 = new Regex(@"\""url\"":\s*""(.*?)github.com");
            var match1 = regex1.Matches(content);
            content = regex1.Replace(content, $"\"url\": \"{proxy}https://github.com");

            // raw.githubusercontent.com
            var regex2 = new Regex(@"\""url\"":\s*""(.*?)raw.githubusercontent.com");
            var match2 = regex2.Matches(content);
            content = regex2.Replace(content, $"\"url\": \"{proxy}https://raw.githubusercontent.com");

            return content;
        }
        private async void button_search_install_proxy_Click(object sender, EventArgs e)
        {
            // 基于程序自己的搜索功能，查找到对应的bucket + json
            // 修改 json文件中的url地址，支持install行为
            // 基于git revert 指令还原仓库的修改
            var bucket = textBox_search_bucket.Text;
            var json = textBox_search_json.Text;
            if (bucket.Length == 0)
            {
                MessageBox.Show("请使用软件搜索功能查找到对应bucket和json文件后，再使用此功能!");
                return;
            }
            var proxy = textBox_proxy_url.Text;
            if (proxy.Length == 0 || !proxy.StartsWith("http"))
            {
                MessageBox.Show("请设置一个有效的Proxy地址！\r\n【提示：】可以使用获取Github Proxy自动查找可用Proxy地址！", "参数错误");
                return;
            }
            Log($"\r\n检查下载url，添加Proxy ...\r\n");
            var file = Path.Combine(scoopActions.getScoopRootPath(), "buckets", bucket, "bucket", json);
            var content = File.ReadAllText(file);
            var content2 = appJsonModifyProxy(content, proxy);
            File.WriteAllText(file, content2);

            // install
            var cmd = $"scoop install {bucket}/{Path.GetFileNameWithoutExtension(json)}";
            Log($"Url修改完成，开始执行安装 {cmd} ...\r\n");
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });

            // revert
            Log("开始恢复本地 bucket app json 文件 ...\r\n");
            File.WriteAllText(file, content);
            Log("Done.\r\n");
            //var dir = Path.Combine(scoopActions.getScoopRootPath(), "buckets", bucket);
            //cmd = "&{ cd " + dir + "; git checkout .; }";
            //await Task.Run(() =>
            //{
            //    scoopActions.runPowershellCmdCB(cmd);
            //});
        }
        private async void button_ss_install_no_proxy_Click(object sender, EventArgs e)
        {
            var bucket = textBox_search_bucket.Text;
            var json = textBox_search_json.Text;
            if (bucket.Length == 0)
            {
                MessageBox.Show("请使用软件搜索功能查找到对应bucket和json文件后，再使用此功能!");
                return;
            }

            Log($"\r\n检查下载url，删除 Proxy ...\r\n");
            var file = Path.Combine(scoopActions.getScoopRootPath(), "buckets", bucket, "bucket", json);
            var content = File.ReadAllText(file);
            var content2 = appJsonModifyProxy(content, "");
            File.WriteAllText(file, content2);

            // install
            var cmd = $"scoop install {bucket}/{Path.GetFileNameWithoutExtension(json)}";
            Log($"Url修改完成，开始执行安装 {cmd} ...\r\n");
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });

            // revert
            Log("开始恢复本地 bucket app json 文件 ...\r\n");
            File.WriteAllText(file, content);
            Log("Done.\r\n");
        }

        private void button_app_bucket_Click(object sender, EventArgs e)
        {
            var bucket = textBox_app_bucket_new.Text.Trim();
            if (bucket.Length == 0)
            {
                MessageBox.Show("没有提供新的bucket名字！\r\n提示：可以在search页面，查找到之后再修改!", "警告");
                return;
            }
            Log("\r\n开始修改选中 app 的 bucket ...\r\n");
            for (var i = 0; i < appList.valueList.Count; i++)
            {
                if (checkedListBox_app_list.GetItemChecked(i))
                {
                    var app = appList.valueList[i];
                    var file = Path.Combine(scoopActions.getScoopRootPath(), "apps", app[0], "current", "install.json");
                    var json = File.ReadAllText(file);
                    var data = JObject.Parse(json);
                    Log($"  {app[0]} bucket {data["bucket"]} => {bucket}\r\n");
                    data["bucket"] = bucket;
                    File.WriteAllText(file, data.ToString());
                }
            }
            Log("Done.\r\n");
        }

        private async void button_install_config_Click(object sender, EventArgs e)
        {
            var cmd = "&{";
            cmd += "git config --global credential.helper manager ;";
            //cmd += "git config --global http.sslverify false ;";
            cmd += $"scoop config scoop_repo {scoopActions.SCOOP_REPO} ;";
            cmd += "scoop config aria2-enabled false ;";
            //cmd += "scoop config rm proxy ;";

            cmd += "}";
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });
        }

        private async void button_config_proxy_dis_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB("&{scoop config rm proxy; scoop config;}");
            });
        }

        private async void button_config_proxy_en_Click(object sender, EventArgs e)
        {
            var cmd = $"&{{scoop config proxy {textBox_scoop_config_proxy.Text}; scoop config; }}";
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            // save 
            if (textBox_proxy_url.Text.Length > 0)
                config_save("proxy", textBox_proxy_url.Text);
        }

        private async void button_bucket_fix_Click(object sender, EventArgs e)
        {
            var cmd = "&{ ";
            var root = scoopActions.getScoopRootPath();
            Log($"\r\n开始扫描 bucket ...\r\n");
            foreach(var dir in Directory.EnumerateDirectories(root + "\\buckets"))
            {
                cmd += $"cd {dir} ;";
                cmd += "git checkout -- . ;";
            }
            cmd += "}";
            Log($"开始还原 bucket ...\r\n");
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });
        }

        private async void button_search_update_proxy_Click(object sender, EventArgs e)
        {
            // 基于程序自己的搜索功能，查找到对应的bucket + json
            // 修改 json文件中的url地址，支持install行为
            // 基于git revert 指令还原仓库的修改
            var bucket = textBox_search_bucket.Text;
            var json = textBox_search_json.Text;
            if (bucket.Length == 0)
            {
                MessageBox.Show("请使用软件搜索功能查找到对应bucket和json文件后，再使用此功能!");
                return;
            }
            var proxy = textBox_proxy_url.Text;
            if (proxy.Length == 0 || !proxy.StartsWith("http"))
            {
                MessageBox.Show("请设置一个有效的Proxy地址！\r\n【提示：】可以使用获取Github Proxy自动查找可用Proxy地址！", "参数错误");
                return;
            }
            Log($"\r\n检查下载url，添加Proxy ...\r\n");
            var file = Path.Combine(scoopActions.getScoopRootPath(), "buckets", bucket, "bucket", json);
            var content = File.ReadAllText(file);
            var content2 = appJsonModifyProxy(content, proxy);
            File.WriteAllText(file, content2);

            // install
            var cmd = $"scoop download {bucket}/{Path.GetFileNameWithoutExtension(json)}";
            Log($"Url修改完成，开始执行升级 {cmd} ...\r\n");
            await Task.Run(() =>
            {
                scoopActions.runPowershellCmdCB(cmd);
            });

            // revert
            Log("开始恢复本地 bucket app json 文件 ...\r\n");
            File.WriteAllText(file, content);
            Log("Done.\r\n");
        }
    }
}
