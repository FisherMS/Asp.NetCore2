# Asp.NetCore2
asp.net core 2 app 学习

------------------------------

### -- 2017-11-24 ------------------------ ###

## dotnet new --help 显示出所有可用的项目模板
用VS2017创建Model-Controller-View的MVC项目后。通过工具栏上的运行下拉框可选择不同的运行与调试模式。       
. 可以通过工具栏的运行下拉上选择IISExpress进行运行。      
. 还可以选择项目名称直接通过控制台运行。此时，每个请求的Info日志会输出到控制台上。     
. dotnet publish   发布项目至Debug目录下。     
. dotnet publish -o ..\www    发布至当前目录上一级的WWW目录中。      
. VS2017的发布，只需要右键项目选择发布，按文件夹发布即可。      
. TODO:@@问题@@ 在服务器上无法使用IIS，在本地可以。     

## 发布至CentOS + Nginx反应代码 
. 关于在不同平台上发布与部署的参考：[参考1](https://docs.microsoft.com/en-us/aspnet/core/publishing/linuxproduction?tabs=aspnetcore2x)          
. search in google use install nginx on centos ,then step to step.    
. other step: [参考2](https://www.microsoft.com/net/learn/get-started/linuxcentos)    
. 如果发现配置好反向代理后，结果显示 **502 Bad Gateway** 可以用下面的命令 /usr/sbin/setsebool -P httpd_can_network_connect 1      [参考3](http://sysadminsjourney.com/content/2010/02/01/apache-modproxy-error-13permission-denied-error-rhel/)      


### -- 2017-11-27 ------------------------ ###

## CommandLineSample 控制台示例程序    
. 通过Nuget安装 **Microsoft.AspNetCore**  注意Microsoft.AspNetCore 与  Micosoft.netCore.app(控制台app默认)。      
. 通过 `var builder = new ConfigurationBuilder().AddCommandLine(args);`读取命令行里的参数：`dotnet CommandLineSample.dll name=atwind age=100`。      
. 从内存（即代码初始化配置）中加入配置集合`.AddInMemoryCollection(settings)`  按照AddMethodx的先后顺序，后面的配置会自动覆盖前面的配置。 
          
## 读取Json文件配置 ## 
. 载入： `var builder = new ConfigurationBuilder().AddJsonFile("cfg.json");`。      
. 读取： ` configuration["classNo"]   configuration["students:0:name"]  ` 集合:索引:属性。   

## Bind读取配置到C#实例 示例：OptionBindSample ## 
. 新建OptionBindSample Web网站，选择空模板建立。   
. 把Configuration加为属性。
.  Configuration.Bind() 把对像与配置进行绑定。    
. 根止录下的appsettings.json会自动读取到。    

## 在Core Mvc中使用Options方式 示例：OptionBindSample ## 
. 注掉上一节中的app.run代码块，否则就一直会显示这个运行里的内容。    
. `services.Configure<Class>(Configuration); //注册配置文件与对应的类` 这个注册一定要加上。   
. 直接取得注入的结果可以在视图中直接调用：`@inject IOptions<OptionBindSample.Class> clsAccesser;`，调用时直接`@clsAccesser.Value.属性`即可。     


     
