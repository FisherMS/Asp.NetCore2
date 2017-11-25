# Asp.NetCore2
asp.net core 2 app 学习

------------------------------









### -- 2017-11-24 ------------------------ ###
## CommandLineSample 控制台示例程序    
. 通过Nuget安装 **Microsoft.AspNetCore**  注意Microsoft.AspNetCore 与  Micosoft.netCore.app(控制台app默认)    

     



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


