# asp-dotnet-6-application

## why start this repo ?

- 【背景】近期在做 **windows 客户端** 的自动化构建发布服务。
- 【过程】初期首选考虑过使用 **Jenkins** 来解决这个需求，实际使用过后才发现几个躲不开的点：
  1. 直接使用 **Jenkins** 提供的插件进行 **`msbuild`** 来构建，需要额外配置一下 **`msbuild`** 环境，但是 **Visual Studio** 安装完成后已经提供了两个（`cmd` + `powershell`） **集成终端 `Developer Command Prompt` + `Developer Powershell`** ，**为什么我要额外再配置一下 msbuild ？我只想无脑直接用！（虽然只需要一次）**
  2. 没办法那么灵活解析解决方案下的项目，需要额外开发 **Jenkins** 相关的插件，工作量又上来了。再加上这个服务需要集成到另一个平台，两边联调，还没测试过，不保证会遇到什么跨域、鉴权的问题
  3. 从现阶段线下构建的场景，还是直接写服务吧，至少可以完全自定义逻辑，不需要按照 **Jenkins** 的实现逻辑
  4. 有点重了
- 【结果】花了 3 周搭了个 **asp .net 6 应用** ，完成基础功能了：
  1. 指定分支进行构建发布
  2. 指定 **configuration** 和 **runtime** 进行构建发布（和 **Visual Studio** 内构建流程逻辑一致）
  3. 可以指定签名配置对发布结果的 **.exe** 进行签名
  4. 【TODO】使用 **advanced installer** 出 **installer.exe**
  5. 【TODO】推送发布 **installer.exe** 到发布平台
  6. 【TODO】查询构建发布历史和日志，下载免安装版的 **archive.zip** 、 **installer.exe**
  7. 【TODO】构建、发布、推送 **nuget package**
  8. 【TODO】构建、发布其他类型的应用：**electron application**
- 【额外收获】
  1. **.net 6** 支持跨平台，为什么不在 **MacOS** 下也用 **asp .net core** 作为后端？
  2. **asp .net core** 的 **ORM** 真好用，比 **java** 平台高出 N 个 **sequelize**
  3. **asp .net core** 的 **web** 模板框架真·无脑好上手，很 **eggjs**

## what would this repo contains ?

1. 基础的前后端分离的 **web** 应用脚手架
2. 基于脚手架进行开发的自己的一个 **web** 应用

## how to clone and use this repo ?

> **main 分支**：主分支不包含我自己开发的任何内容，只包含脚手架内容和基础的 demo 示例
>
> **dak-site 分支**：基于主分支进行开发的内容

基于分支的作用，可直接 clone 主分支后，在此基础上进行修改，补充接口和服务、补充前端页面

## about develop plan ...

### milestone 1

- **2024.03.01** 完成基础脚手架搭建
  1. 包含注册登录和鉴权逻辑
  2. 前端完成脚手架搭建，可以本地进行开发调试和注册登录

### milestone 2

- **2024.05.01** 完成项目的一阶段迁移

### _to be continued_

## Sln Detail

> 简单介绍说明一下模板项目基础之上的一些自定义模块，以及魔改后的配置，便于二次开发使用

### 环境配置
+ 前景
  + `Properties/launchSettings.json` 的配置主要是用于本地开发启动应用时传入的一些配置信息
  + 可以指定本地启动项目时，使用哪一个配置
+ 配置
  + 项目 **.sln** 中配置了当前解决方案中定义的一些配置名：**`Debug`** 、 **`Release`** 等
+ 指定环境变量名
  + 模板项目默认的 **env variable** 是 **`Development`** （详见 `Properties/launchSettings.json`）
  + 为了方便自己开发，以及环境配置名一致，将 **`ASPNETCORE_ENVIRONMENT`** 字段调整为 **`Debug`**
  + 这样的调整带来的影响
    ```csharp
    // 原先默认的判断是否 dev 环境
    if (app.Environment.IsDevelopment()) {
      // do something in dev enviroment
    }

    // 判断逻辑调整为
    // 如果你习惯使用 development ，则在 .sln 中修改配置名，调整判断逻辑
    if (app.Environment.EnvironmentName == "Debug") {
      // do someing i dev enviroment
    }
    ```
+ 应用配置
  + 项目自带了 **transform settings** 的逻辑
  + 默认配置：**`appsettings.json`**
  + 不同环境下的配置：**`appsettings.{enviroment}.json`** （**`appsettings.Debug.json`**）
  + 在使用对应环境配置进行构建时，会进行 merge

### 接口测试
+ 项目已集成 **`NSwag`** ，启动后访问地址 **`http://localhost:<port>/swagger/index.html`** 即可进行测试

### Controllers
> 标准 MVC 模式的 controller 层

### Models
> 数据库表结构对应的实体类，映射到对应的数据库表

### DTOs
> 端侧传入的参数，或者返回给端侧的数据结构体

### Services
> 标准 MVC 模式的 service 层

### wwwroot
> 静态资源存储目录，不再赘述

### ~~Views~~
> 前后端分离，不涉及后端渲染视图了，直接移除了该目录

### 其他细节说明
1. **`Directory.Build.props`** + **`Directory.Build.targets`**
> DTOs 目录中存放的是和端侧通信的数据结构，考虑到习惯，采用的小驼峰的命名方式
>
> 但是 c# 中首选且建议用大驼峰作为属性名，所以针对该目录，抑制 IDE1006 的警告