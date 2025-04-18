# ScoopTools
C# WinForm 开发的GUI程序，为方便国内环境安装和维护scoop，bucket, app 的小工具。  

依赖：Net Framework 4.8

# 功能
- Github Proxy 获取和可用性检测
- Scoop 自定义安装路径
- Scoop多种安装方式：官方， **Proxy方式**
- 本地缺少 git 时，直接通过 scoop 安装 git,7z. 告别鸡生蛋，蛋生鸡问题。 **支持Proxy**
- Bucket 管理，不删除情况下，维护 url.  **支持Proxy**
- APP 管理,更新，支持修改 app 的 bucket.
- APP 搜索功能，集成常见搜索软件。
	- 开发了基于软件自身的搜索功能，速度更快，扩展了新的功能
	- **支持通过Proxy直接安装**， 解决了常见bucket url混乱问题, 包含 scoop cn 一类的带有proxy的url, 也支持定制proxy安装了。
	- 支持 **纯净url** 安装，防止 scoop cn 一类的仓库bucket proxy不能使用问题。

此工具既能满足可以访问github情况下的使用，也满足被墙情况下的安装和使用。  

> 注意：由于此工具小功能众多，使用者必须了解scoop的基本操作后，再使用此工具。

# Preview
![install](./doc/install.png)  
![apps](./doc/app.png)  
![search](./doc/ss.png)  


# Note
- 2025/2/18
	- 完成基础功能，满足基本需求
- 2025/2/21
    - 完成了 proxy url 修复安装功能
	- 优化 UI 卡死问题
- 2025/3/21
    - 增加download功能，可以配合install/update 使用
- 2025/4/7
	- 修复Github Proxy列表不可用问题。
	
> By BBDXF