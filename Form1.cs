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
            var url = $"{proxy}/https://raw.githubusercontent.com/ScoopInstaller/Install/refs/heads/master/install.ps1";
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

        private void button_install_apps_Click(object sender, EventArgs e)
        {
            var cmd = "scoop install git 7zip";

        }

        private void button_bucket_official_Click(object sender, EventArgs e)
        {
            string proxy = "";
            if (checkBox_bucket_proxy_enable.Enabled)
            {
                proxy = textBox_proxy_url.Text;
                if (proxy.Length == 0 || !proxy.StartsWith("http"))
                {
                    MessageBox.Show("请设置一个有效的Proxy地址！\r\n【提示：】可以使用获取Github Proxy自动查找可用Proxy地址！", "参数错误");
                    return;
                }
            }

            // add
            Log($"正在添加 bucket...\r\n");
            var output = scoopActions.bucketAddOfficial(proxy);
            Log($"Done:\r\n{output}\r\n");
        }
    }
}
