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
        }

        private void Log(string txt)
        {
            textBox_log.AppendText(txt);
        }

        private async void button_proxy_check_best_Click(object sender, EventArgs e)
        {
            Log("\r\n开始测试 github Proxy 可用性...\r\n");
            for (var i = 0; i < proxyList.Count; i++)
            {
                var p = proxyList[i];
                var delay = await scoopActions.checkUrlDelay(p.url);
                p.latency = delay;
                label_status.Text = $"[{i+1}/{proxyList.Count}] {p.url} => {delay}ms ...";
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

            textBox_proxy_url.Text = proxyList[0].url;
            label_status.Text = "已选择最佳proxy!";
        }

        private async void button_proxy_get_Click(object sender, EventArgs e)
        {
            Log("\r\n正在请求可用github Proxy...\r\n");
            proxyList = await scoopActions.getGithubProxyList();
            Log($"获取成功，一共 [ {proxyList.Count} ] 记录:\r\n\r\n- 序号 - server ----- 理论延时 - URL --------\r\n");
            for(var i=0;i<proxyList.Count;i++)
            {
                var p = proxyList[i];
                Log($" [{(i+1).ToString("D3")}]  {p.server.PadRight(16)} {p.latency}\t{p.url}\r\n");
            }
            textBox_proxy_url.Text = proxyList[0].url;
            label_status.Text = "已选择第一个proxy，建议测试最佳proxy后使用，或者手动填写!";
        }

        private void button_log_clear_Click(object sender, EventArgs e)
        {
            textBox_log.Text = "";
        }

        private void button_proxy_set_Click(object sender, EventArgs e)
        {
            var proxy = textBox_proxy_url.Text;
            if (proxy.Length == 0  || !proxy.StartsWith("http"))
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
            var output = scoopActions.runPowershellCmd($"scoop config scoop_repo");
            Log($"当前 scoop_repo : {output} \r\n");
        }

        private void button_proxy_reset_Click(object sender, EventArgs e)
        {
            var url = $"{scoopActions.SCOOP_REPO}";
            scoopActions.runPowershellCmd($"scoop config scoop_repo {url}");
            Log($"设置 scoop_repo : {url} \r\n");
        }

        private void button_install_official_Click(object sender, EventArgs e)
        {
            Log($"【Official】正在安装，url: https://get.scoop.sh ... 【耗时很长，耐心等待!】\r\n");
            var cmd = "Invoke-RestMethod -Uri https://get.scoop.sh | Invoke-Expression";
            var output = scoopActions.runPowershellCmd(cmd);
            Log(output);
        }

        private void button_install_pre_Click(object sender, EventArgs e)
        {
            Log($"【准备】正在设置powershell选项...\r\n");
            var cmd = "Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser";
            scoopActions.runPowershellCmd(cmd);
            Log("Done\r\n");

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
            var output = scoopActions.runPowershellCmd($"&{filepath}");
            Log($"{output}\r\n");
        }

        private void button_install_env_clear_Click(object sender, EventArgs e)
        {
            scoopActions.setEnvValue(scoopActions.SCOOP_ENV_GLOBAL, null);
            scoopActions.setEnvValue(scoopActions.SCOOP_ENV_ROOT, null);
            MessageBox.Show("Done");
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
                }
            }
        }

        private void button_bucket_modify_url_Click(object sender, EventArgs e)
        {
            Log($"\r\n开始修改 bucket git url\r\n");
            var output = scoopActions.bucketModifyUrl(textBox_bucket_name.Text, textBox_bucket_url.Text);
            Log($"{output}\r\n");
        }
        private string fmtBucketList(string[] list)
        {
            if (list.Length != 4) return "";
            return $"{list[0].PadRight(12)} {list[1].PadRight(48)} {list[2].PadRight(18)} {list[3].PadLeft(10)}";
        }
        private void button_bucket_list_Click(object sender, EventArgs e)
        {
            Log("\r\n正在请求 bucket list...\r\n");
            bucketList = scoopActions.runScoopCmd(ScoopCmdType.BUCKET_LIST);
            Log($"获取成功，一共 [ {bucketList.valueList.Count} ] 记录:\r\n\r\n 序号 {fmtBucketList(bucketList.keys)}\r\n");

            comboBox_bucket_list.Items.Clear();
            for (var i = 0; i < bucketList.valueList.Count; i++)
            {
                var p = bucketList.valueList[i];
                comboBox_bucket_list.Items.Add(p[0]);
                Log($"[{(i + 1).ToString("D3")}] {fmtBucketList(p)}\r\n");
            }

            if(comboBox_bucket_list.Items.Count > 0)
            {
                comboBox_bucket_list.SelectedIndex = 0;
            }
        }

        private void comboBox_bucket_list_SelectedValueChanged(object sender, EventArgs e)
        {
            var index = comboBox_bucket_list.SelectedIndex;
            var b = bucketList.valueList[index];
            textBox_bucket_name.Text = b[0];
            textBox_bucket_url.Text = b[1];
            textBox_bucket_update.Text = b[2];
            textBox_bucket_apps.Text = b[3];
        }

        private void button_bucket_del_Click(object sender, EventArgs e)
        {
            var msg = $"你确定要【删除】bucket {textBox_bucket_name.Text} 吗?";
            if(MessageBox.Show(msg, "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Log($"正在删除 bucket...\r\n");
                var cmd = $"scoop bucket rm {textBox_bucket_name.Text}";
                var output = scoopActions.runPowershellCmd(cmd);
                Log($"Done: \r\n{output}\r\n");

                // rm from list
                bucketList.valueList.RemoveAt(comboBox_bucket_list.SelectedIndex);
                comboBox_bucket_list.Items.RemoveAt(comboBox_bucket_list.SelectedIndex);
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
            var output = await scoopActions.installGitLocal(proxy);
            Log($"Done:\r\n{output}\r\n");

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
            var output = await scoopActions.installGitLocal(proxy);
            Log($"Done:\r\n{output}\r\n");
            Log("==> Git,7z 已完成安装，可以手动删除 tmp bucket.\r\n");
        }


        private void button_bucket_official_Click(object sender, EventArgs e)
        {
            var msg = "此行为会先删除原有的 main extras versions 等buckets, 然后重新添加！";
            if (MessageBox.Show(msg, "警告", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            string proxy = null;
            Log($"正在添加 official buckets...\r\n");
            var output = scoopActions.bucketAddOfficial(proxy);
            Log($"Done:\r\n{output}\r\n");
        }
        private void button_bucket_install_official_Click(object sender, EventArgs e)
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
            var output = scoopActions.bucketAddOfficial(proxy);
            Log($"Done:\r\n{output}\r\n");
        }
        private string fmtAppList(string[] list)
        {
            if (list.Length != 5) return "";
            return $"{list[0].PadRight(16)} {list[1].PadRight(12)} {list[2].PadRight(8)} {list[3].PadRight(20)}";
        }
        private void button_app_list_Click(object sender, EventArgs e)
        {
            Log("\r\n正在请求 app list...\r\n");
            appList = scoopActions.runScoopCmd(ScoopCmdType.APP_LIST);
            Log($"获取成功，一共 [ {appList.valueList.Count} ] 记录:\r\n\r\n 序号 {fmtAppList(appList.keys)}\r\n");

            checkedListBox_app_list.Items.Clear();
            for (var i = 0; i < appList.valueList.Count; i++)
            {
                var p = appList.valueList[i];
                var fmt = fmtAppList(p);
                checkedListBox_app_list.Items.Add(fmt);
                Log($"[{(i + 1).ToString("D3")}] {fmt}\r\n");
            }
        }

        private void button_app_update_Click(object sender, EventArgs e)
        {
            var cmd = "scoop update ";
            var count = 0;
            for(var i = 0; i < checkedListBox_app_list.Items.Count; i++)
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
            var output = scoopActions.runPowershellCmd(cmd);
            Log($"Done: \r\n{output}\r\n");

            button_app_list.PerformClick();
        }

        private void button_app_bucket_update_Click(object sender, EventArgs e)
        {
            var cmd = "&{scoop update; scoop status;}";
            Log("\r\n正在请求 scoop update & status ...\r\n");
            var output = scoopActions.runPowershellCmd(cmd);
            Log($"Done: \r\n{output}\r\n");
        }

        private void button_app_uninstall_Click(object sender, EventArgs e)
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
            var output = scoopActions.runPowershellCmd(cmd);
            Log($"Done: \r\n{output}\r\n");

            button_app_list.PerformClick();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            var cmd = "scoop search ";
            if (radioButton_ss_old.Checked)
            {
                cmd = "scoop search ";
            }
            if (radioButton_ss_go.Checked)
            {
                cmd = "scoop-search ";
            }
            if (radioButton_ss_ss.Checked)
            {
                cmd = "ss -r ";
            }
            var app = textBox_search.Text.Trim().ToLower();
            if (app.Length == 0)
            {
                MessageBox.Show("请输入一个有效的名称！");
                return;
            }
            cmd += app;
            if (!radioButton_ss_soft.Checked) { 
                Log($"\r\n开始执行搜索指令 {cmd} ，此行为耗时很长，耐心等待...\r\n");
                var output = scoopActions.runPowershellCmd(cmd);
                Log($"Done: \r\n{output}\r\n");
            }
            else
            {
                // soft search logic
                var root = scoopActions.getScoopRootPath();
                var search_results = new Dictionary<string, List<string>>();
                Log($"\r\n开始执行软件搜索，耐心等待...\r\n");
                var dirList = Directory.EnumerateDirectories(root+"/buckets").ToList(); // 全路径
                //官方仓库优先
                var buckets_first = new string[] { "main", "extras", "versions", "nonportable" };
                for (var i = dirList.Count - 1;  i >= 0; i--)
                {
                    var dirname = Path.GetFileName(dirList[i]);
                    label_status.Text = $"正在搜索 {dirname} ...";
                    if ( buckets_first.Contains(dirname) )
                    {
                        // find logic
                        var json = search_app_json(app, Path.Combine(root, "buckets", dirname));
                        if ( json != null )
                        {
                            search_results.Add(dirname, json);
                        }
                        Log($"{dirname.PadRight(12)} => count {json.Count}\r\n");
                        // remove it
                        dirList.RemoveAt(i);
                    }
                }
                // 然后其他bucket
                for (var i = 0; i < dirList.Count - 1; i++)
                {
                    var dirname = Path.GetDirectoryName(dirList[i]);
                    label_status.Text = $"正在搜索 {dirname} ...";
                    // find logic
                    var json = search_app_json(app, Path.Combine(root, "buckets", dirname));
                    if (json != null)
                    {
                        search_results.Add(dirname, json);
                    }
                    Log($"{dirname.PadRight(12)} => {json.Count}\r\n");
                }
                Log($"\r\n遍历完成，查找最相似结果...\r\n");
                // 查找最相似
                string result_bucket = null;
                string result_json = null;
                foreach(var key in search_results.Keys)
                {
                    var f = Path.GetFileNameWithoutExtension(search_results[key].ToLower());
                    if ( f == app)
                    {
                        result_bucket = key;
                        result_json = search_results[key];
                        break;
                    }
                    if (f.Contains(app))
                    {
                        result_bucket = key;
                        result_json = search_results[key];
                        break;
                    }
                    if (app.Contains(' ') || app.Contains('-') || app.Contains('_'))
                    {
                        var key_words = app.Split(new char[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
                        if(key_words.All( val => f.Contains(val)))
                        {
                            result_bucket = key;
                            result_json = search_results[key];
                            break;
                        }
                    }
                }
                // result
                Log($"找到的最佳选项为 【{result_bucket}】 {result_json}");
            }
        }

        public List<string> search_app_json(string app, string bucketPath)
        {
            var result_json = new List<string>();
            foreach (var key in Directory.EnumerateFiles(bucketPath, "*.json"))
            {
                var f = Path.GetFileNameWithoutExtension(key);
                if (f.Contains(app))
                {
                    result_json.Add(key);
                }
                else if (app.Contains(' ') || app.Contains('-') || app.Contains('_'))
                {
                    var key_words = app.Split(new char[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
                    if (key_words.All(val => f.Contains(val)))
                    {
                        result_json.Add(key);
                    }
                }
            }
            return result_json;
        }

        private void button_search_install_Click(object sender, EventArgs e)
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
            var output = scoopActions.runPowershellCmd(cmd);
            Log($"Done: \r\n{output}\r\n");
        }

        private void button_search_install_proxy_Click(object sender, EventArgs e)
        {
            // 基于程序自己的搜索功能，查找到对应的bucket + json
            // 修改 json文件中的url地址，支持install行为
            // 基于git revert 指令还原仓库的修改
        }
    }
}
