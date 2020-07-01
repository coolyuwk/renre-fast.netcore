# renre-fast.netcore


### 简单介绍  
此项目是renre-fast的.net core 3.1后台，代码比较清蒸。

### 已实现模块
  * 管理员列表
  * 角色管理
  * 菜单管理
  * 参数管理
  * 文件上传

### 未实现模块
  * SQL监控
  * 定时任务
  * 系统日志


### 快速开始

1. 修改 appsettings.json 配置连接字符串，默认使用MSSQL
```
  "ConnectionStrings": {
    "passport": "Server=;User ID=;Password=;Database=;"
  }
```

2. 设置RenRen.Fast.Api为启动项目（如果安装Docker可以设置docker-compose为启动项目），启动后表结构和初始数据会自动迁移到目标数据库

3. 打开 http://localhost:9003 登录使用默认用户名: admin 密码：admin 

