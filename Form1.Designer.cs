
namespace ScoopTools
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage_index = new System.Windows.Forms.TabPage();
            this.textBox_readme = new System.Windows.Forms.TextBox();
            this.tabPage_proxy = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_proxy_url = new System.Windows.Forms.TextBox();
            this.button_proxy_get = new System.Windows.Forms.Button();
            this.button_proxy_check_best = new System.Windows.Forms.Button();
            this.tabPage_install = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.button_config_proxy_en = new System.Windows.Forms.Button();
            this.button_config_proxy_dis = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_scoop_config_proxy = new System.Windows.Forms.TextBox();
            this.button_install_config = new System.Windows.Forms.Button();
            this.button_install_git_proxy = new System.Windows.Forms.Button();
            this.button_install_git = new System.Windows.Forms.Button();
            this.button_proxy_info = new System.Windows.Forms.Button();
            this.button_proxy_reset = new System.Windows.Forms.Button();
            this.button_proxy_set = new System.Windows.Forms.Button();
            this.button_install_env_set = new System.Windows.Forms.Button();
            this.button_install_env_clear = new System.Windows.Forms.Button();
            this.button_install_pre = new System.Windows.Forms.Button();
            this.button_install_official_proxy = new System.Windows.Forms.Button();
            this.button_install_official = new System.Windows.Forms.Button();
            this.tabPage_buckets = new System.Windows.Forms.TabPage();
            this.button_bucket_fix = new System.Windows.Forms.Button();
            this.button_bucket_install_official = new System.Windows.Forms.Button();
            this.textBox_bucket_update = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_bucket_apps = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_buckt_url_proxy = new System.Windows.Forms.Button();
            this.button_bucket_modify_url = new System.Windows.Forms.Button();
            this.textBox_bucket_name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_bucket_url = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_bucket_list = new System.Windows.Forms.ComboBox();
            this.button_bucket_del = new System.Windows.Forms.Button();
            this.button_bucket_list = new System.Windows.Forms.Button();
            this.button_bucket_official = new System.Windows.Forms.Button();
            this.tabPage_apps = new System.Windows.Forms.TabPage();
            this.textBox_app_bucket_new = new System.Windows.Forms.TextBox();
            this.button_app_uninstall = new System.Windows.Forms.Button();
            this.button_app_bucket = new System.Windows.Forms.Button();
            this.button_app_bucket_update = new System.Windows.Forms.Button();
            this.button_app_update = new System.Windows.Forms.Button();
            this.button_app_list = new System.Windows.Forms.Button();
            this.checkedListBox_app_list = new System.Windows.Forms.CheckedListBox();
            this.tabPage_search = new System.Windows.Forms.TabPage();
            this.button_search_update_proxy = new System.Windows.Forms.Button();
            this.button_ss_install_no_proxy = new System.Windows.Forms.Button();
            this.textBox_search_json = new System.Windows.Forms.TextBox();
            this.textBox_search_bucket = new System.Windows.Forms.TextBox();
            this.button_search_install_proxy = new System.Windows.Forms.Button();
            this.button_search_install = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButton_ss_go = new System.Windows.Forms.RadioButton();
            this.radioButton_ss_soft = new System.Windows.Forms.RadioButton();
            this.radioButton_ss_old = new System.Windows.Forms.RadioButton();
            this.button_search = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_status = new System.Windows.Forms.Label();
            this.button_log_clear = new System.Windows.Forms.Button();
            this.textBox_log = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_proxy_get2 = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabPage_index.SuspendLayout();
            this.tabPage_proxy.SuspendLayout();
            this.tabPage_install.SuspendLayout();
            this.tabPage_buckets.SuspendLayout();
            this.tabPage_apps.SuspendLayout();
            this.tabPage_search.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage_index);
            this.tabMain.Controls.Add(this.tabPage_proxy);
            this.tabMain.Controls.Add(this.tabPage_install);
            this.tabMain.Controls.Add(this.tabPage_buckets);
            this.tabMain.Controls.Add(this.tabPage_apps);
            this.tabMain.Controls.Add(this.tabPage_search);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(778, 400);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabPage_index
            // 
            this.tabPage_index.Controls.Add(this.textBox_readme);
            this.tabPage_index.Location = new System.Drawing.Point(4, 26);
            this.tabPage_index.Name = "tabPage_index";
            this.tabPage_index.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_index.Size = new System.Drawing.Size(770, 370);
            this.tabPage_index.TabIndex = 0;
            this.tabPage_index.Text = "0. 说明";
            this.tabPage_index.UseVisualStyleBackColor = true;
            // 
            // textBox_readme
            // 
            this.textBox_readme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_readme.Location = new System.Drawing.Point(3, 3);
            this.textBox_readme.Multiline = true;
            this.textBox_readme.Name = "textBox_readme";
            this.textBox_readme.ReadOnly = true;
            this.textBox_readme.Size = new System.Drawing.Size(764, 364);
            this.textBox_readme.TabIndex = 0;
            this.textBox_readme.Text = resources.GetString("textBox_readme.Text");
            // 
            // tabPage_proxy
            // 
            this.tabPage_proxy.Controls.Add(this.label10);
            this.tabPage_proxy.Controls.Add(this.label1);
            this.tabPage_proxy.Controls.Add(this.textBox_proxy_url);
            this.tabPage_proxy.Controls.Add(this.button_proxy_get2);
            this.tabPage_proxy.Controls.Add(this.button_proxy_get);
            this.tabPage_proxy.Controls.Add(this.button_proxy_check_best);
            this.tabPage_proxy.Location = new System.Drawing.Point(4, 26);
            this.tabPage_proxy.Name = "tabPage_proxy";
            this.tabPage_proxy.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_proxy.Size = new System.Drawing.Size(770, 370);
            this.tabPage_proxy.TabIndex = 1;
            this.tabPage_proxy.Text = "1. 代理";
            this.tabPage_proxy.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Navy;
            this.label10.Location = new System.Drawing.Point(124, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(403, 85);
            this.label10.TabIndex = 5;
            this.label10.Text = "Github Proxy 知识：\r\n1. 大部分github proxy都支持文件下载，但不是所有的都支持clone操作。\r\n2. scoop bucket 依赖" +
    " clone 操作，所以需要挑选合适的proxy.\r\n3. 本工具暂时无法检查proxy对clone的支持情况。\r\n4. Proxy的延时并不是越小越好，差不多" +
    "就行了。";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "最佳Proxy:";
            // 
            // textBox_proxy_url
            // 
            this.textBox_proxy_url.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_proxy_url.ForeColor = System.Drawing.Color.Green;
            this.textBox_proxy_url.Location = new System.Drawing.Point(27, 141);
            this.textBox_proxy_url.Name = "textBox_proxy_url";
            this.textBox_proxy_url.Size = new System.Drawing.Size(723, 31);
            this.textBox_proxy_url.TabIndex = 2;
            this.textBox_proxy_url.Text = "https://gh.idayer.com";
            this.textBox_proxy_url.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_proxy_get
            // 
            this.button_proxy_get.Location = new System.Drawing.Point(27, 29);
            this.button_proxy_get.Name = "button_proxy_get";
            this.button_proxy_get.Size = new System.Drawing.Size(139, 42);
            this.button_proxy_get.TabIndex = 0;
            this.button_proxy_get.Text = "获取Github代理[弃用]";
            this.button_proxy_get.UseVisualStyleBackColor = true;
            this.button_proxy_get.Click += new System.EventHandler(this.button_proxy_get_Click);
            // 
            // button_proxy_check_best
            // 
            this.button_proxy_check_best.Location = new System.Drawing.Point(369, 29);
            this.button_proxy_check_best.Name = "button_proxy_check_best";
            this.button_proxy_check_best.Size = new System.Drawing.Size(311, 42);
            this.button_proxy_check_best.TabIndex = 0;
            this.button_proxy_check_best.Text = "检查当前网络最优Github代理";
            this.button_proxy_check_best.UseVisualStyleBackColor = true;
            this.button_proxy_check_best.Click += new System.EventHandler(this.button_proxy_check_best_Click);
            // 
            // tabPage_install
            // 
            this.tabPage_install.Controls.Add(this.label12);
            this.tabPage_install.Controls.Add(this.button_config_proxy_en);
            this.tabPage_install.Controls.Add(this.button_config_proxy_dis);
            this.tabPage_install.Controls.Add(this.label11);
            this.tabPage_install.Controls.Add(this.textBox_scoop_config_proxy);
            this.tabPage_install.Controls.Add(this.button_install_config);
            this.tabPage_install.Controls.Add(this.button_install_git_proxy);
            this.tabPage_install.Controls.Add(this.button_install_git);
            this.tabPage_install.Controls.Add(this.button_proxy_info);
            this.tabPage_install.Controls.Add(this.button_proxy_reset);
            this.tabPage_install.Controls.Add(this.button_proxy_set);
            this.tabPage_install.Controls.Add(this.button_install_env_set);
            this.tabPage_install.Controls.Add(this.button_install_env_clear);
            this.tabPage_install.Controls.Add(this.button_install_pre);
            this.tabPage_install.Controls.Add(this.button_install_official_proxy);
            this.tabPage_install.Controls.Add(this.button_install_official);
            this.tabPage_install.Location = new System.Drawing.Point(4, 22);
            this.tabPage_install.Name = "tabPage_install";
            this.tabPage_install.Size = new System.Drawing.Size(770, 374);
            this.tabPage_install.TabIndex = 2;
            this.tabPage_install.Text = "2. 安装";
            this.tabPage_install.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label12.Location = new System.Drawing.Point(61, 178);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(304, 51);
            this.label12.TabIndex = 14;
            this.label12.Text = "可以使用 main bucket 直接安装git 7z！\r\n方法：使用 search 页面搜索，然后 proxy 安装！\r\n如果 main 不可用，使用这里【修复" +
    "】功能安装git 7z！";
            // 
            // button_config_proxy_en
            // 
            this.button_config_proxy_en.ForeColor = System.Drawing.Color.SlateBlue;
            this.button_config_proxy_en.Location = new System.Drawing.Point(634, 313);
            this.button_config_proxy_en.Name = "button_config_proxy_en";
            this.button_config_proxy_en.Size = new System.Drawing.Size(97, 40);
            this.button_config_proxy_en.TabIndex = 13;
            this.button_config_proxy_en.Text = "使用 Proxy";
            this.toolTip1.SetToolTip(this.button_config_proxy_en, "这个会影响 scoop install/bucket 等操作，与github proxy完全不同，自备翻墙软件时可以使用。");
            this.button_config_proxy_en.UseVisualStyleBackColor = true;
            this.button_config_proxy_en.Click += new System.EventHandler(this.button_config_proxy_en_Click);
            // 
            // button_config_proxy_dis
            // 
            this.button_config_proxy_dis.ForeColor = System.Drawing.Color.SlateBlue;
            this.button_config_proxy_dis.Location = new System.Drawing.Point(528, 313);
            this.button_config_proxy_dis.Name = "button_config_proxy_dis";
            this.button_config_proxy_dis.Size = new System.Drawing.Size(87, 40);
            this.button_config_proxy_dis.TabIndex = 13;
            this.button_config_proxy_dis.Text = "禁用 Proxy";
            this.button_config_proxy_dis.UseVisualStyleBackColor = true;
            this.button_config_proxy_dis.Click += new System.EventHandler(this.button_config_proxy_dis_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(525, 258);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 17);
            this.label11.TabIndex = 12;
            this.label11.Text = "scoop config proxy :";
            // 
            // textBox_scoop_config_proxy
            // 
            this.textBox_scoop_config_proxy.Location = new System.Drawing.Point(528, 278);
            this.textBox_scoop_config_proxy.Name = "textBox_scoop_config_proxy";
            this.textBox_scoop_config_proxy.Size = new System.Drawing.Size(214, 23);
            this.textBox_scoop_config_proxy.TabIndex = 11;
            this.textBox_scoop_config_proxy.Text = "127.0.0.1:7890";
            // 
            // button_install_config
            // 
            this.button_install_config.ForeColor = System.Drawing.Color.Teal;
            this.button_install_config.Location = new System.Drawing.Point(371, 258);
            this.button_install_config.Name = "button_install_config";
            this.button_install_config.Size = new System.Drawing.Size(112, 95);
            this.button_install_config.TabIndex = 10;
            this.button_install_config.Text = "scoop config \r\n常规设置";
            this.toolTip1.SetToolTip(this.button_install_config, "添加 repo, aria2, git等的常规设置");
            this.button_install_config.UseVisualStyleBackColor = true;
            this.button_install_config.Click += new System.EventHandler(this.button_install_config_Click);
            // 
            // button_install_git_proxy
            // 
            this.button_install_git_proxy.ForeColor = System.Drawing.Color.Red;
            this.button_install_git_proxy.Location = new System.Drawing.Point(371, 209);
            this.button_install_git_proxy.Name = "button_install_git_proxy";
            this.button_install_git_proxy.Size = new System.Drawing.Size(286, 36);
            this.button_install_git_proxy.TabIndex = 9;
            this.button_install_git_proxy.Text = "【修复+Proxy】为没有Git的电脑安装7z+Git";
            this.button_install_git_proxy.UseVisualStyleBackColor = true;
            this.button_install_git_proxy.Click += new System.EventHandler(this.button_install_git_proxy_Click);
            // 
            // button_install_git
            // 
            this.button_install_git.ForeColor = System.Drawing.Color.Red;
            this.button_install_git.Location = new System.Drawing.Point(371, 170);
            this.button_install_git.Name = "button_install_git";
            this.button_install_git.Size = new System.Drawing.Size(286, 36);
            this.button_install_git.TabIndex = 9;
            this.button_install_git.Text = "【修复】为没有Git的电脑安装7z+Git";
            this.button_install_git.UseVisualStyleBackColor = true;
            this.button_install_git.Click += new System.EventHandler(this.button_install_git_Click);
            // 
            // button_proxy_info
            // 
            this.button_proxy_info.Location = new System.Drawing.Point(36, 258);
            this.button_proxy_info.Name = "button_proxy_info";
            this.button_proxy_info.Size = new System.Drawing.Size(89, 95);
            this.button_proxy_info.TabIndex = 8;
            this.button_proxy_info.Text = "查看scoop config";
            this.button_proxy_info.UseVisualStyleBackColor = true;
            this.button_proxy_info.Click += new System.EventHandler(this.button_proxy_info_Click);
            // 
            // button_proxy_reset
            // 
            this.button_proxy_reset.Location = new System.Drawing.Point(143, 258);
            this.button_proxy_reset.Name = "button_proxy_reset";
            this.button_proxy_reset.Size = new System.Drawing.Size(182, 40);
            this.button_proxy_reset.TabIndex = 7;
            this.button_proxy_reset.Text = "Reset scoop_repo";
            this.button_proxy_reset.UseVisualStyleBackColor = true;
            this.button_proxy_reset.Click += new System.EventHandler(this.button_proxy_reset_Click);
            // 
            // button_proxy_set
            // 
            this.button_proxy_set.Location = new System.Drawing.Point(143, 313);
            this.button_proxy_set.Name = "button_proxy_set";
            this.button_proxy_set.Size = new System.Drawing.Size(182, 40);
            this.button_proxy_set.TabIndex = 6;
            this.button_proxy_set.Text = "scoop_repo 添加proxy";
            this.button_proxy_set.UseVisualStyleBackColor = true;
            this.button_proxy_set.Click += new System.EventHandler(this.button_proxy_set_Click);
            // 
            // button_install_env_set
            // 
            this.button_install_env_set.ForeColor = System.Drawing.Color.Blue;
            this.button_install_env_set.Location = new System.Drawing.Point(371, 63);
            this.button_install_env_set.Name = "button_install_env_set";
            this.button_install_env_set.Size = new System.Drawing.Size(360, 36);
            this.button_install_env_set.TabIndex = 2;
            this.button_install_env_set.Text = "【ENV】设置自定义安装目录";
            this.toolTip1.SetToolTip(this.button_install_env_set, "目录尽量简短，没有中文字符");
            this.button_install_env_set.UseVisualStyleBackColor = true;
            this.button_install_env_set.Click += new System.EventHandler(this.button_install_env_set_Click);
            // 
            // button_install_env_clear
            // 
            this.button_install_env_clear.ForeColor = System.Drawing.Color.Blue;
            this.button_install_env_clear.Location = new System.Drawing.Point(36, 63);
            this.button_install_env_clear.Name = "button_install_env_clear";
            this.button_install_env_clear.Size = new System.Drawing.Size(318, 36);
            this.button_install_env_clear.TabIndex = 2;
            this.button_install_env_clear.Text = "【ENV】恢复默认安装位置";
            this.toolTip1.SetToolTip(this.button_install_env_clear, "删除scoop环境变量");
            this.button_install_env_clear.UseVisualStyleBackColor = true;
            this.button_install_env_clear.Click += new System.EventHandler(this.button_install_env_clear_Click);
            // 
            // button_install_pre
            // 
            this.button_install_pre.Location = new System.Drawing.Point(36, 16);
            this.button_install_pre.Name = "button_install_pre";
            this.button_install_pre.Size = new System.Drawing.Size(695, 36);
            this.button_install_pre.TabIndex = 1;
            this.button_install_pre.Text = "【准备】设置powershell 安全选项";
            this.toolTip1.SetToolTip(this.button_install_pre, "可能需要管理员选项！");
            this.button_install_pre.UseVisualStyleBackColor = true;
            this.button_install_pre.Click += new System.EventHandler(this.button_install_pre_Click);
            // 
            // button_install_official_proxy
            // 
            this.button_install_official_proxy.ForeColor = System.Drawing.Color.Red;
            this.button_install_official_proxy.Location = new System.Drawing.Point(371, 115);
            this.button_install_official_proxy.Name = "button_install_official_proxy";
            this.button_install_official_proxy.Size = new System.Drawing.Size(360, 38);
            this.button_install_official_proxy.TabIndex = 0;
            this.button_install_official_proxy.Text = "【Proxy】修改官方脚本，添加Proxy";
            this.toolTip1.SetToolTip(this.button_install_official_proxy, "使用官方github安装脚本，仅仅增加url的proxy，方便国内用户不翻墙使用！");
            this.button_install_official_proxy.UseVisualStyleBackColor = true;
            this.button_install_official_proxy.Click += new System.EventHandler(this.button_install_official_proxy_Click);
            // 
            // button_install_official
            // 
            this.button_install_official.ForeColor = System.Drawing.Color.Red;
            this.button_install_official.Location = new System.Drawing.Point(36, 115);
            this.button_install_official.Name = "button_install_official";
            this.button_install_official.Size = new System.Drawing.Size(318, 38);
            this.button_install_official.TabIndex = 0;
            this.button_install_official.Text = "【Official】官方途径安装";
            this.toolTip1.SetToolTip(this.button_install_official, "官方url下载安装，大概率需要自己解决翻墙问题。");
            this.button_install_official.UseVisualStyleBackColor = true;
            this.button_install_official.Click += new System.EventHandler(this.button_install_official_Click);
            // 
            // tabPage_buckets
            // 
            this.tabPage_buckets.Controls.Add(this.button_bucket_fix);
            this.tabPage_buckets.Controls.Add(this.button_bucket_install_official);
            this.tabPage_buckets.Controls.Add(this.textBox_bucket_update);
            this.tabPage_buckets.Controls.Add(this.label6);
            this.tabPage_buckets.Controls.Add(this.textBox_bucket_apps);
            this.tabPage_buckets.Controls.Add(this.label5);
            this.tabPage_buckets.Controls.Add(this.button_buckt_url_proxy);
            this.tabPage_buckets.Controls.Add(this.button_bucket_modify_url);
            this.tabPage_buckets.Controls.Add(this.textBox_bucket_name);
            this.tabPage_buckets.Controls.Add(this.label4);
            this.tabPage_buckets.Controls.Add(this.textBox_bucket_url);
            this.tabPage_buckets.Controls.Add(this.label3);
            this.tabPage_buckets.Controls.Add(this.label2);
            this.tabPage_buckets.Controls.Add(this.comboBox_bucket_list);
            this.tabPage_buckets.Controls.Add(this.button_bucket_del);
            this.tabPage_buckets.Controls.Add(this.button_bucket_list);
            this.tabPage_buckets.Controls.Add(this.button_bucket_official);
            this.tabPage_buckets.Location = new System.Drawing.Point(4, 22);
            this.tabPage_buckets.Name = "tabPage_buckets";
            this.tabPage_buckets.Size = new System.Drawing.Size(770, 374);
            this.tabPage_buckets.TabIndex = 3;
            this.tabPage_buckets.Text = "3. Buckets";
            this.tabPage_buckets.UseVisualStyleBackColor = true;
            // 
            // button_bucket_fix
            // 
            this.button_bucket_fix.Location = new System.Drawing.Point(386, 216);
            this.button_bucket_fix.Name = "button_bucket_fix";
            this.button_bucket_fix.Size = new System.Drawing.Size(186, 38);
            this.button_bucket_fix.TabIndex = 12;
            this.button_bucket_fix.Text = "还原/修复 bucket中的问题";
            this.toolTip1.SetToolTip(this.button_bucket_fix, "使用git相关命令，尝试还原改动，防止无法update");
            this.button_bucket_fix.UseVisualStyleBackColor = true;
            this.button_bucket_fix.Click += new System.EventHandler(this.button_bucket_fix_Click);
            // 
            // button_bucket_install_official
            // 
            this.button_bucket_install_official.Location = new System.Drawing.Point(52, 270);
            this.button_bucket_install_official.Name = "button_bucket_install_official";
            this.button_bucket_install_official.Size = new System.Drawing.Size(253, 38);
            this.button_bucket_install_official.TabIndex = 11;
            this.button_bucket_install_official.Text = "【Proxy】添加Official常见buckets";
            this.button_bucket_install_official.UseVisualStyleBackColor = true;
            this.button_bucket_install_official.Click += new System.EventHandler(this.button_bucket_install_official_Click);
            // 
            // textBox_bucket_update
            // 
            this.textBox_bucket_update.Location = new System.Drawing.Point(514, 148);
            this.textBox_bucket_update.Name = "textBox_bucket_update";
            this.textBox_bucket_update.ReadOnly = true;
            this.textBox_bucket_update.Size = new System.Drawing.Size(217, 23);
            this.textBox_bucket_update.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(422, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "更新日期:";
            // 
            // textBox_bucket_apps
            // 
            this.textBox_bucket_apps.Location = new System.Drawing.Point(155, 148);
            this.textBox_bucket_apps.Name = "textBox_bucket_apps";
            this.textBox_bucket_apps.ReadOnly = true;
            this.textBox_bucket_apps.Size = new System.Drawing.Size(201, 23);
            this.textBox_bucket_apps.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "软件数量:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button_buckt_url_proxy
            // 
            this.button_buckt_url_proxy.Location = new System.Drawing.Point(588, 72);
            this.button_buckt_url_proxy.Name = "button_buckt_url_proxy";
            this.button_buckt_url_proxy.Size = new System.Drawing.Size(143, 34);
            this.button_buckt_url_proxy.TabIndex = 6;
            this.button_buckt_url_proxy.Text = "Url 添加 Proxy";
            this.button_buckt_url_proxy.UseVisualStyleBackColor = true;
            this.button_buckt_url_proxy.Click += new System.EventHandler(this.button_buckt_url_proxy_Click);
            // 
            // button_bucket_modify_url
            // 
            this.button_bucket_modify_url.Location = new System.Drawing.Point(474, 72);
            this.button_bucket_modify_url.Name = "button_bucket_modify_url";
            this.button_bucket_modify_url.Size = new System.Drawing.Size(98, 34);
            this.button_bucket_modify_url.TabIndex = 6;
            this.button_bucket_modify_url.Text = "修改 Url";
            this.button_bucket_modify_url.UseVisualStyleBackColor = true;
            this.button_bucket_modify_url.Click += new System.EventHandler(this.button_bucket_modify_url_Click);
            // 
            // textBox_bucket_name
            // 
            this.textBox_bucket_name.Location = new System.Drawing.Point(155, 76);
            this.textBox_bucket_name.Name = "textBox_bucket_name";
            this.textBox_bucket_name.ReadOnly = true;
            this.textBox_bucket_name.Size = new System.Drawing.Size(201, 23);
            this.textBox_bucket_name.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Name:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_bucket_url
            // 
            this.textBox_bucket_url.Location = new System.Drawing.Point(155, 112);
            this.textBox_bucket_url.Name = "textBox_bucket_url";
            this.textBox_bucket_url.Size = new System.Drawing.Size(576, 23);
            this.textBox_bucket_url.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Url:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Bucket 列表:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_bucket_list
            // 
            this.comboBox_bucket_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_bucket_list.FormattingEnabled = true;
            this.comboBox_bucket_list.Location = new System.Drawing.Point(155, 13);
            this.comboBox_bucket_list.Name = "comboBox_bucket_list";
            this.comboBox_bucket_list.Size = new System.Drawing.Size(201, 25);
            this.comboBox_bucket_list.TabIndex = 3;
            this.comboBox_bucket_list.SelectedValueChanged += new System.EventHandler(this.comboBox_bucket_list_SelectedValueChanged);
            // 
            // button_bucket_del
            // 
            this.button_bucket_del.Location = new System.Drawing.Point(373, 72);
            this.button_bucket_del.Name = "button_bucket_del";
            this.button_bucket_del.Size = new System.Drawing.Size(84, 34);
            this.button_bucket_del.TabIndex = 2;
            this.button_bucket_del.Text = "删除";
            this.button_bucket_del.UseVisualStyleBackColor = true;
            this.button_bucket_del.Click += new System.EventHandler(this.button_bucket_del_Click);
            // 
            // button_bucket_list
            // 
            this.button_bucket_list.Location = new System.Drawing.Point(373, 9);
            this.button_bucket_list.Name = "button_bucket_list";
            this.button_bucket_list.Size = new System.Drawing.Size(107, 38);
            this.button_bucket_list.TabIndex = 2;
            this.button_bucket_list.Text = "刷新列表";
            this.button_bucket_list.UseVisualStyleBackColor = true;
            this.button_bucket_list.Click += new System.EventHandler(this.button_bucket_list_Click);
            // 
            // button_bucket_official
            // 
            this.button_bucket_official.Location = new System.Drawing.Point(52, 216);
            this.button_bucket_official.Name = "button_bucket_official";
            this.button_bucket_official.Size = new System.Drawing.Size(253, 38);
            this.button_bucket_official.TabIndex = 0;
            this.button_bucket_official.Text = "添加 Official 常见 buckets";
            this.toolTip1.SetToolTip(this.button_bucket_official, "建议删除原有bucket后再添加");
            this.button_bucket_official.UseVisualStyleBackColor = true;
            this.button_bucket_official.Click += new System.EventHandler(this.button_bucket_official_Click);
            // 
            // tabPage_apps
            // 
            this.tabPage_apps.Controls.Add(this.textBox_app_bucket_new);
            this.tabPage_apps.Controls.Add(this.button_app_uninstall);
            this.tabPage_apps.Controls.Add(this.button_app_bucket);
            this.tabPage_apps.Controls.Add(this.button_app_bucket_update);
            this.tabPage_apps.Controls.Add(this.button_app_update);
            this.tabPage_apps.Controls.Add(this.button_app_list);
            this.tabPage_apps.Controls.Add(this.checkedListBox_app_list);
            this.tabPage_apps.Location = new System.Drawing.Point(4, 22);
            this.tabPage_apps.Name = "tabPage_apps";
            this.tabPage_apps.Size = new System.Drawing.Size(770, 374);
            this.tabPage_apps.TabIndex = 4;
            this.tabPage_apps.Text = "4. Apps";
            this.tabPage_apps.UseVisualStyleBackColor = true;
            // 
            // textBox_app_bucket_new
            // 
            this.textBox_app_bucket_new.BackColor = System.Drawing.Color.White;
            this.textBox_app_bucket_new.ForeColor = System.Drawing.Color.Red;
            this.textBox_app_bucket_new.Location = new System.Drawing.Point(585, 262);
            this.textBox_app_bucket_new.Name = "textBox_app_bucket_new";
            this.textBox_app_bucket_new.Size = new System.Drawing.Size(148, 23);
            this.textBox_app_bucket_new.TabIndex = 2;
            this.textBox_app_bucket_new.Text = "main";
            this.textBox_app_bucket_new.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.textBox_app_bucket_new, "App要修改的目标 bucket");
            // 
            // button_app_uninstall
            // 
            this.button_app_uninstall.ForeColor = System.Drawing.Color.Red;
            this.button_app_uninstall.Location = new System.Drawing.Point(593, 174);
            this.button_app_uninstall.Name = "button_app_uninstall";
            this.button_app_uninstall.Size = new System.Drawing.Size(126, 41);
            this.button_app_uninstall.TabIndex = 1;
            this.button_app_uninstall.Text = "卸载软件";
            this.button_app_uninstall.UseVisualStyleBackColor = true;
            this.button_app_uninstall.Click += new System.EventHandler(this.button_app_uninstall_Click);
            // 
            // button_app_bucket
            // 
            this.button_app_bucket.ForeColor = System.Drawing.Color.Red;
            this.button_app_bucket.Location = new System.Drawing.Point(551, 302);
            this.button_app_bucket.Name = "button_app_bucket";
            this.button_app_bucket.Size = new System.Drawing.Size(211, 41);
            this.button_app_bucket.TabIndex = 1;
            this.button_app_bucket.Text = "修改 app 的 bucket";
            this.toolTip1.SetToolTip(this.button_app_bucket, "请确认你知道此功能的作用，否则不要随意修改!");
            this.button_app_bucket.UseVisualStyleBackColor = true;
            this.button_app_bucket.Click += new System.EventHandler(this.button_app_bucket_Click);
            // 
            // button_app_bucket_update
            // 
            this.button_app_bucket_update.Location = new System.Drawing.Point(593, 66);
            this.button_app_bucket_update.Name = "button_app_bucket_update";
            this.button_app_bucket_update.Size = new System.Drawing.Size(126, 41);
            this.button_app_bucket_update.TabIndex = 1;
            this.button_app_bucket_update.Text = "更新buckets";
            this.button_app_bucket_update.UseVisualStyleBackColor = true;
            this.button_app_bucket_update.Click += new System.EventHandler(this.button_app_bucket_update_Click);
            // 
            // button_app_update
            // 
            this.button_app_update.Location = new System.Drawing.Point(593, 117);
            this.button_app_update.Name = "button_app_update";
            this.button_app_update.Size = new System.Drawing.Size(126, 41);
            this.button_app_update.TabIndex = 1;
            this.button_app_update.Text = "更新软件";
            this.button_app_update.UseVisualStyleBackColor = true;
            this.button_app_update.Click += new System.EventHandler(this.button_app_update_Click);
            // 
            // button_app_list
            // 
            this.button_app_list.Location = new System.Drawing.Point(593, 14);
            this.button_app_list.Name = "button_app_list";
            this.button_app_list.Size = new System.Drawing.Size(126, 41);
            this.button_app_list.TabIndex = 1;
            this.button_app_list.Text = "刷新列表";
            this.button_app_list.UseVisualStyleBackColor = true;
            this.button_app_list.Click += new System.EventHandler(this.button_app_list_Click);
            // 
            // checkedListBox_app_list
            // 
            this.checkedListBox_app_list.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkedListBox_app_list.FormattingEnabled = true;
            this.checkedListBox_app_list.Location = new System.Drawing.Point(3, 3);
            this.checkedListBox_app_list.Name = "checkedListBox_app_list";
            this.checkedListBox_app_list.Size = new System.Drawing.Size(542, 344);
            this.checkedListBox_app_list.TabIndex = 0;
            // 
            // tabPage_search
            // 
            this.tabPage_search.Controls.Add(this.button_search_update_proxy);
            this.tabPage_search.Controls.Add(this.button_ss_install_no_proxy);
            this.tabPage_search.Controls.Add(this.textBox_search_json);
            this.tabPage_search.Controls.Add(this.textBox_search_bucket);
            this.tabPage_search.Controls.Add(this.button_search_install_proxy);
            this.tabPage_search.Controls.Add(this.button_search_install);
            this.tabPage_search.Controls.Add(this.label8);
            this.tabPage_search.Controls.Add(this.radioButton_ss_go);
            this.tabPage_search.Controls.Add(this.radioButton_ss_soft);
            this.tabPage_search.Controls.Add(this.radioButton_ss_old);
            this.tabPage_search.Controls.Add(this.button_search);
            this.tabPage_search.Controls.Add(this.label9);
            this.tabPage_search.Controls.Add(this.label7);
            this.tabPage_search.Controls.Add(this.textBox_search);
            this.tabPage_search.Location = new System.Drawing.Point(4, 22);
            this.tabPage_search.Name = "tabPage_search";
            this.tabPage_search.Size = new System.Drawing.Size(770, 374);
            this.tabPage_search.TabIndex = 5;
            this.tabPage_search.Text = "5. Search";
            this.tabPage_search.UseVisualStyleBackColor = true;
            // 
            // button_search_update_proxy
            // 
            this.button_search_update_proxy.ForeColor = System.Drawing.Color.Red;
            this.button_search_update_proxy.Location = new System.Drawing.Point(489, 169);
            this.button_search_update_proxy.Name = "button_search_update_proxy";
            this.button_search_update_proxy.Size = new System.Drawing.Size(119, 42);
            this.button_search_update_proxy.TabIndex = 9;
            this.button_search_update_proxy.Text = "【Proxy】Down";
            this.toolTip1.SetToolTip(this.button_search_update_proxy, "通过Proxy下载软件!\r\n然后，可以继续使用 update 或者 install 命令了");
            this.button_search_update_proxy.UseVisualStyleBackColor = true;
            this.button_search_update_proxy.Click += new System.EventHandler(this.button_search_update_proxy_Click);
            // 
            // button_ss_install_no_proxy
            // 
            this.button_ss_install_no_proxy.ForeColor = System.Drawing.Color.Red;
            this.button_ss_install_no_proxy.Location = new System.Drawing.Point(627, 119);
            this.button_ss_install_no_proxy.Name = "button_ss_install_no_proxy";
            this.button_ss_install_no_proxy.Size = new System.Drawing.Size(99, 42);
            this.button_ss_install_no_proxy.TabIndex = 8;
            this.button_ss_install_no_proxy.Text = "【No Proxy】净化安装";
            this.toolTip1.SetToolTip(this.button_ss_install_no_proxy, "去除 url 链接中的proxy部分，使用原生 github 链接下载安装");
            this.button_ss_install_no_proxy.UseVisualStyleBackColor = true;
            this.button_ss_install_no_proxy.Click += new System.EventHandler(this.button_ss_install_no_proxy_Click);
            // 
            // textBox_search_json
            // 
            this.textBox_search_json.Location = new System.Drawing.Point(257, 129);
            this.textBox_search_json.Name = "textBox_search_json";
            this.textBox_search_json.ReadOnly = true;
            this.textBox_search_json.Size = new System.Drawing.Size(215, 23);
            this.textBox_search_json.TabIndex = 7;
            // 
            // textBox_search_bucket
            // 
            this.textBox_search_bucket.Location = new System.Drawing.Point(130, 129);
            this.textBox_search_bucket.Name = "textBox_search_bucket";
            this.textBox_search_bucket.ReadOnly = true;
            this.textBox_search_bucket.Size = new System.Drawing.Size(121, 23);
            this.textBox_search_bucket.TabIndex = 7;
            // 
            // button_search_install_proxy
            // 
            this.button_search_install_proxy.ForeColor = System.Drawing.Color.Red;
            this.button_search_install_proxy.Location = new System.Drawing.Point(489, 119);
            this.button_search_install_proxy.Name = "button_search_install_proxy";
            this.button_search_install_proxy.Size = new System.Drawing.Size(119, 42);
            this.button_search_install_proxy.TabIndex = 6;
            this.button_search_install_proxy.Text = "【Proxy】安装";
            this.toolTip1.SetToolTip(this.button_search_install_proxy, "通过Proxy下载安装软件!\r\n此方案必须配合软件自身app查找功能使用！\r\n对于已经有proxy的bucket, 也会尝试修复后使用！\r\n目前只能处理githu" +
        "b url的proxy");
            this.button_search_install_proxy.UseVisualStyleBackColor = true;
            this.button_search_install_proxy.Click += new System.EventHandler(this.button_search_install_proxy_Click);
            // 
            // button_search_install
            // 
            this.button_search_install.ForeColor = System.Drawing.Color.Blue;
            this.button_search_install.Location = new System.Drawing.Point(627, 19);
            this.button_search_install.Name = "button_search_install";
            this.button_search_install.Size = new System.Drawing.Size(102, 31);
            this.button_search_install.TabIndex = 5;
            this.button_search_install.Text = "安装";
            this.toolTip1.SetToolTip(this.button_search_install, "原生方式安装软件");
            this.button_search_install.UseVisualStyleBackColor = true;
            this.button_search_install.Click += new System.EventHandler(this.button_search_install_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(120, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(488, 85);
            this.label8.TabIndex = 4;
            this.label8.Text = "几种搜索方式的区别：\r\n\r\n1. 原生搜索，速度慢，展示信息详细。\r\n2. scoop-search, 第三方golang编写，搜索本地bucket得到数据，速度" +
    "快。【需要安装】\r\n3. 软件自身方案。【推荐】【支持Url Proxy安装、净化功能】";
            // 
            // radioButton_ss_go
            // 
            this.radioButton_ss_go.AutoSize = true;
            this.radioButton_ss_go.Location = new System.Drawing.Point(246, 68);
            this.radioButton_ss_go.Name = "radioButton_ss_go";
            this.radioButton_ss_go.Size = new System.Drawing.Size(105, 21);
            this.radioButton_ss_go.TabIndex = 3;
            this.radioButton_ss_go.Text = "scoop-search";
            this.radioButton_ss_go.UseVisualStyleBackColor = true;
            // 
            // radioButton_ss_soft
            // 
            this.radioButton_ss_soft.AutoSize = true;
            this.radioButton_ss_soft.Checked = true;
            this.radioButton_ss_soft.Location = new System.Drawing.Point(136, 68);
            this.radioButton_ss_soft.Name = "radioButton_ss_soft";
            this.radioButton_ss_soft.Size = new System.Drawing.Size(74, 21);
            this.radioButton_ss_soft.TabIndex = 3;
            this.radioButton_ss_soft.TabStop = true;
            this.radioButton_ss_soft.Text = "软件搜索";
            this.radioButton_ss_soft.UseVisualStyleBackColor = true;
            // 
            // radioButton_ss_old
            // 
            this.radioButton_ss_old.AutoSize = true;
            this.radioButton_ss_old.Location = new System.Drawing.Point(397, 68);
            this.radioButton_ss_old.Name = "radioButton_ss_old";
            this.radioButton_ss_old.Size = new System.Drawing.Size(94, 21);
            this.radioButton_ss_old.TabIndex = 3;
            this.radioButton_ss_old.Text = "原生搜索(慢)";
            this.radioButton_ss_old.UseVisualStyleBackColor = true;
            // 
            // button_search
            // 
            this.button_search.Location = new System.Drawing.Point(513, 19);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(95, 31);
            this.button_search.TabIndex = 2;
            this.button_search.Text = "搜索";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(8, 132);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 17);
            this.label9.TabIndex = 1;
            this.label9.Text = "bucket info:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(24, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "查找的软件";
            // 
            // textBox_search
            // 
            this.textBox_search.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_search.ForeColor = System.Drawing.SystemColors.Highlight;
            this.textBox_search.Location = new System.Drawing.Point(130, 19);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(377, 31);
            this.textBox_search.TabIndex = 0;
            this.textBox_search.Text = "git";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_status);
            this.panel1.Controls.Add(this.button_log_clear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 714);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 30);
            this.panel1.TabIndex = 3;
            // 
            // label_status
            // 
            this.label_status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_status.Location = new System.Drawing.Point(75, 0);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(703, 30);
            this.label_status.TabIndex = 1;
            this.label_status.Text = "Init Done.";
            this.label_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_log_clear
            // 
            this.button_log_clear.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_log_clear.Location = new System.Drawing.Point(0, 0);
            this.button_log_clear.Name = "button_log_clear";
            this.button_log_clear.Size = new System.Drawing.Size(75, 30);
            this.button_log_clear.TabIndex = 0;
            this.button_log_clear.Text = "清除Log";
            this.button_log_clear.UseVisualStyleBackColor = true;
            this.button_log_clear.Click += new System.EventHandler(this.button_log_clear_Click);
            // 
            // textBox_log
            // 
            this.textBox_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_log.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_log.Location = new System.Drawing.Point(0, 400);
            this.textBox_log.Multiline = true;
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.ReadOnly = true;
            this.textBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_log.Size = new System.Drawing.Size(778, 314);
            this.textBox_log.TabIndex = 4;
            // 
            // button_proxy_get2
            // 
            this.button_proxy_get2.Location = new System.Drawing.Point(172, 29);
            this.button_proxy_get2.Name = "button_proxy_get2";
            this.button_proxy_get2.Size = new System.Drawing.Size(139, 42);
            this.button_proxy_get2.TabIndex = 0;
            this.button_proxy_get2.Text = "获取Github代理-New";
            this.button_proxy_get2.UseVisualStyleBackColor = true;
            this.button_proxy_get2.Click += new System.EventHandler(this.button_proxy_get2_Click);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(778, 744);
            this.Controls.Add(this.textBox_log);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabMain);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Scoop管理工具";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabMain.ResumeLayout(false);
            this.tabPage_index.ResumeLayout(false);
            this.tabPage_index.PerformLayout();
            this.tabPage_proxy.ResumeLayout(false);
            this.tabPage_proxy.PerformLayout();
            this.tabPage_install.ResumeLayout(false);
            this.tabPage_install.PerformLayout();
            this.tabPage_buckets.ResumeLayout(false);
            this.tabPage_buckets.PerformLayout();
            this.tabPage_apps.ResumeLayout(false);
            this.tabPage_apps.PerformLayout();
            this.tabPage_search.ResumeLayout(false);
            this.tabPage_search.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage_index;
        private System.Windows.Forms.TabPage tabPage_proxy;
        private System.Windows.Forms.TextBox textBox_readme;
        private System.Windows.Forms.TabPage tabPage_install;
        private System.Windows.Forms.Button button_proxy_check_best;
        private System.Windows.Forms.Button button_proxy_get;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Button button_log_clear;
        private System.Windows.Forms.TextBox textBox_log;
        private System.Windows.Forms.TextBox textBox_proxy_url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_install_official;
        private System.Windows.Forms.Button button_install_official_proxy;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_install_pre;
        private System.Windows.Forms.Button button_install_env_set;
        private System.Windows.Forms.Button button_install_env_clear;
        private System.Windows.Forms.TabPage tabPage_buckets;
        private System.Windows.Forms.Button button_bucket_official;
        private System.Windows.Forms.Button button_bucket_list;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_bucket_list;
        private System.Windows.Forms.TextBox textBox_bucket_url;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_bucket_modify_url;
        private System.Windows.Forms.TextBox textBox_bucket_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_bucket_del;
        private System.Windows.Forms.Button button_buckt_url_proxy;
        private System.Windows.Forms.TextBox textBox_bucket_update;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_bucket_apps;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_proxy_info;
        private System.Windows.Forms.Button button_proxy_reset;
        private System.Windows.Forms.Button button_proxy_set;
        private System.Windows.Forms.TabPage tabPage_apps;
        private System.Windows.Forms.Button button_install_git;
        private System.Windows.Forms.Button button_install_git_proxy;
        private System.Windows.Forms.Button button_bucket_install_official;
        private System.Windows.Forms.Button button_app_bucket;
        private System.Windows.Forms.Button button_app_bucket_update;
        private System.Windows.Forms.Button button_app_list;
        private System.Windows.Forms.CheckedListBox checkedListBox_app_list;
        private System.Windows.Forms.Button button_app_update;
        private System.Windows.Forms.TabPage tabPage_search;
        private System.Windows.Forms.Button button_app_uninstall;
        private System.Windows.Forms.RadioButton radioButton_ss_go;
        private System.Windows.Forms.RadioButton radioButton_ss_old;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_search;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_search_install;
        private System.Windows.Forms.Button button_search_install_proxy;
        private System.Windows.Forms.TextBox textBox_search_json;
        private System.Windows.Forms.TextBox textBox_search_bucket;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radioButton_ss_soft;
        private System.Windows.Forms.TextBox textBox_app_bucket_new;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button_install_config;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_scoop_config_proxy;
        private System.Windows.Forms.Button button_config_proxy_en;
        private System.Windows.Forms.Button button_config_proxy_dis;
        private System.Windows.Forms.Button button_ss_install_no_proxy;
        private System.Windows.Forms.Button button_bucket_fix;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button_search_update_proxy;
        private System.Windows.Forms.Button button_proxy_get2;
    }
}

