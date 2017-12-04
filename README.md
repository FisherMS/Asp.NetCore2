# Asp.NetCore2
asp.net core 2 app 学习

------------------------------

### -- 2017-11 ------------------------ ###
## dotnet new --help 显示出所有可用的项目模板 
> 用VS2017创建Model-Controller-View的MVC项目后。通过工具栏上的运行下拉框可选择不同的运行与调试模式。       
> 可以通过工具栏的运行下拉上选择IISExpress进行运行。      
> 还可以选择项目名称直接通过控制台运行。此时，每个请求的Info日志会输出到控制台上。     
> dotnet publish   发布项目至Debug目录下。     
> dotnet publish -o ..\www    发布至当前目录上一级的WWW目录中。      
> VS2017的发布，只需要右键项目选择发布，按文件夹发布即可。      
> <font color=red>**TODO：原来一个服务器上安了NVXM，现在无法正确安装dotnet2.0.3。**</font>     

       
## 发布至CentOS + Nginx反应代码 
> 关于在不同平台上发布与部署的参考：[参考1](https://docs.microsoft.com/en-us/aspnet/core/publishing/linuxproduction?tabs=aspnetcore2x)          
> search in google use install nginx on centos ,then step to step.    
> other step: [参考2](https://www.microsoft.com/net/learn/get-started/linuxcentos)
> 如果发现配置好反向代理后，结果显示 **502 Bad Gateway** 可以用下面的命令 /usr/sbin/setsebool -P httpd_can_network_connect 1      [参考3](http://sysadminsjourney.com/content/2010/02/01/apache-modproxy-error-13permission-denied-error-rhel/)      

## CommandLineSample 控制台示例程序    
> 通过Nuget安装 **Microsoft.AspNetCore**  注意Microsoft.AspNetCore 与  Micosoft.netCore.app(控制台app默认)。      
> 通过 `var builder = new ConfigurationBuilder().AddCommandLine(args);`读取命令行里的参数：`dotnet CommandLineSample.dll name=atwind age=100`。      
> 从内存（即代码初始化配置）中加入配置集合`.AddInMemoryCollection(settings)`  按照AddMethodx的先后顺序，后面的配置会自动覆盖前面的配置。 
          
## 读取Json文件配置 ## 
> 载入： `var builder = new ConfigurationBuilder().AddJsonFile("cfg.json");`。    
> 读取： ` configuration["classNo"]   configuration["students:0:name"]  ` 集合:索引:属性。   

## Bind读取配置到C#实例 示例：OptionBindSample ## 
> 新建OptionBindSample Web网站，选择空模板建立。   
> 把Configuration加为属性。
>  Configuration.Bind() 把对像与配置进行绑定。    
> 根止录下的appsettings.json会自动读取到。    

## 在Core Mvc中使用Options方式 示例：OptionBindSample ## 

> 注掉上一节中的app.run代码块，否则就一直会显示这个运行里的内容。    
> `services.Configure<Class>(Configuration); //注册配置文件与对应的类` 这个注册一定要加上。   
> 直接取得注入的结果可以在视图中直接调用：`@inject IOptions<OptionBindSample.Class> clsAccesser;`，调用时直接`@clsAccesser.Value.属性`即可。     

## 配置的热更新  ##

> appsettings.json 中的更改，会立即更新到配置属性上去，只需要把原来的`@inject IOptions<OptionBindSample.Class> clsAccesser;`改为：`@inject IOptionsSnapshot<OptionBindSample.Class> clsAccesser;` 即可。   
> 在Program.cs中的代码`public static IWebHost BuildWebHost(string[] args) =>          WebHost.CreateDefaultBuilder(args)  ` 即为增加配置读取相关的代码，可以自已重写这个方法：`WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration(cfg => { cfg.AddJsonFile("appsettings.json", false, false); }) //替换原CreateDefaultBuilder里的配置信息。`      。    
  
## DI初始化的源码解读 ##     

> <font color=yellow>**查看实现的时候，不要查接口的定义，要找创建的实例（它继承了接口）这时才能看到具体的代码。**</font>        
> 如何替换其它的Ioc容器Autofac。示例项目：**WebAPI1** 只需要把Startup类里面的 ConfigureService的 返回值从 void改为 IServiceProvider即可。而返回的则是一个AutoServiceProvider。      
> 通过Nuget安装**Autofac.Extensions.DependencyInjection**和**Autofac**。      

## Docker 化  示例 WebApi1 ##

> 在VS2017中右键项目-》添加-》Docker支持-》目标服务器选择Linux（因为我是在Hyper-v的虚拟机上安的Ubuntu）。这时会多一个Dockerfile文件，没有后缀，打开看看，是文本文件（就是个脚本）。       
> 修改生成的默认Dockerfile文件内容。   [参考1](http://www.cnblogs.com/wangjieguang/p/docker-dotnetcore2.html)       。          
> `sudo docker build -t demo:v1 .　　//注意后边还有一个点呢，构建镜像`。     
> `docker run -p 80:80 -e "ASPNETCORE_URLS=http://+:80" demo:v1` ，运行后就会提示你服务可用了。这时就能直接访问了。是不是很容易？      

### -- 2017-12 ------------------------ ###  

## Middleware 实践  示例:MiddlewareDemo  ##
> 创建一个新的Asp.netCore web 空项目MiddlewareDemo。    
> 创建一个新类 ： GuestIpMiddleware.cs  ，再增加一个扩展，使用IApplicationBuilder.UserMiddleware<GuestIpMiddleware>() ，当然也可以直接写在Start里，一般的习惯是用扩展，方便修改 。    
> 使用中间件(Middleware)：在 Startup.cs 添加 app.UseGuestIp()  

## ASP.NET Core 运行原理解剖 **Hosting** 示例：webapp  ##   
> 创建新的空的webapp项目，这里面是啥也没有。只有两个类，一个Program，一个Start。
> WebHost的创建：对于一个程序控制台程序来说，它的入口点便是 Program 中的 Main 方法，ASP.NET Core 程序自然也不例外。   

> <font color=green>**创建IWebHost是由静态类WebHost.CreateDefaultBuilder()这个静态方法来调用的，这个就是真正的入口方法。按F12是可以直接看到源码的（注意：如果安了ReShaper要在Option-》Tools-》Build-》External Sources选项中，选择Navigation to Sources（要选中Decompile methods））。**   </font>  注意：args方法是会通过AddCommandLine加入到配置方法中的。      

> CreateDefaultBilder这个静态方法做了以下工作：
>1. 注册 Kestrel 中间件，指定 WebHost 要使用的 Server（HTTP服务器）；
>1. 设置 Content 根目录，将当前项目的根目录作为 ContentRoot 的目录；
>1. 读取 appsettinggs.json 配置文件，开发环境下的 UserSecrets 以及环境变量和命令行参数；
>1. 读取配置文件中的 Logging 节点，对日志系统进行配置；添加 IISIntegration 中间件；
>1. 设置开发环境下， ServiceProvider 的 ValidateScopes 为 true，避免直接在 Configure 方法中获取 Scope 实例。
>1. 然后指定 Startup 类，最后通过 Build 方法创建 WebHost 对象。

> WebHostBuilder.Build()中创建了IOC用到的IServiceCollection实例。 在方法体中可以找到有个HostingStartupAttribute类。  [WebHostBuilder代码参考](https://github.com/aspnet/Hosting/blob/dev/src/Microsoft.AspNetCore.Hosting/WebHostBuilder.cs) 
>1.  HostingStartupAttribute 给我们一个在其它程序集中做一些启动配置的机会，在我们进行多层开发及模块化的时候非常有用。  
>1.  接着就是查找Startup类，但是，通常我们会使用 UseStartup<Startup> 的方法来注册 Startup 类，而他们的作用是一样的，都是将我们的 Startup 类做为一个单例注册到了 DI 系统。     
>1. 而最终 BuildCommonServices 返回一个 IServiceCollection，用于构建 hostingServiceProvider。     
>1. 接下来创建 WebHost。注意：hostingServiceProvider 是 ASP.NET Core 中的第一个 ServiceProvider，也是根 ServiceProvider，但它是在我们的 Starpup 类执行之前创建的，也就是说并不会包含我们在 ConfigureServices 中注册的服务（但包含使用 HostingStartupAttribute 注册的服务）。      

> WebHost的启动流程：（当WebHost创建完成后，即调用Run方法运行，这里面实际上运行的是RunAsync。）         
>+ 初始化，构建 RequestDelegate(RequestDelegate 是我们的应用程序处理请求，输出响应的整个过程，也就是我们的 ASP.NET Core 请求管道。)         
>>1. 调用 Startup 中的 ConfigureServices 方法，在前面介绍过，我们的 Startup 类已经注册到了 ASP.NET Coer 的 DI 系统中，因此可以直接从 DI 中获取（这里使用的 _hostingServiceProvider 是我们在 WebHost 中创建的根 ServieProvider）。   
>>1. 初始化 Http Server。Server 是一个HTTP服务器，负责HTTP的监听，接收一组 FeatureCollection 类型的原始请求，并将其包装成 HttpContext 以供我们的应用程序完成响应的处理。Server的初始化主要是配置要监听的地址。     
>>1. 创建 IApplicationBuilder。IApplicationBuilder 用于构建应用程序的请求管道，也就是生成 RequestDelegate。IApplicationBuilderFactory 的默认实现 ApplicationBuilderFactory。           
>>1. 配置 IApplicationBuilder。我们比较的熟悉的是在 Startup 类的 Configure 方法中对 IApplicationBuilder 进行配置，其实还有一个 IStartupFilter 也可以用来配置 IApplicationBuilder，并且在 Startup 类的Configure 方法之前执行。然后调用 IApplicationBuilder 的 Build 方法，便完成了 RequestDelegate 的创建。         
>+ 启动 Server，监听请求并响应。Server 本身是并不清楚 HttpContext 的细节的，因此它需要接收一个 IHttpApplication 类型的参数，来负责 HttpContext 的创建。 它的默认实现是 HostingApplication 类，而 ProcessRequestAsync 方法则调用我们上面创建的 RequestDelegate 委托，来完成对 HttpContext 的处理。最后启动 Server。Server 会绑定一个监听端口，注册HTTP连接事件，最终交给 Http2Stream<TContext> 来处理，通过上面的 hostingApp 来切入到我们的应用程序中，完成整个请求的处理。 
>+ 启动 HostedService。HostedService 为我们提供一个注册后台运行服务的机会，它会在随着我们的 ASP.NET Core 程序启动而启动，并在 ASP.NET Core 停止时进行优雅的关闭。而它是通过 HostedServiceExecutor 来执行的。WebHost 会调用 HostedServiceExecutor 的 StartAsync ，从而完成对 HostedService 的启动。 这里还有对 IApplicationLifetime 启动事件的触发。   
+ [参考内容1](https://weblog.west-wind.com/posts/2016/Jun/06/Publishing-and-Running-ASPNET-Core-Applications-with-IIS)     
+ [参考内容2](http://www.cnblogs.com/RainingNight/p/hosting-in-asp-net-core.html)

## ASP.NET Core 运行原理解剖 **Hosting之配置** 示例：webapp  ## 
> **WebHostBuild** 用来构建 WebHost ，也是我们最先接触的一个类。 它提供了如下方法：
>+   ConfigureAppConfiguration: Configuration 在 ASP.NET Core 进行了全新的设计，使其更加灵活简洁，可以支持多种数据源。    
>+ UseSetting:UseSetting 是一个非常重要的方法，它用来配置 WebHost 中的 IConfiguration 对象。需要注意与上面ConfigureAppConfiguration的区别， WebHost 中的 Configuration 只限于在 WebHost 使用，并且我们不能配置它的数据源，它只会读取ASPNETCORE_开头的环境变量UseSetting 是一个非常重要的方法，它用来配置 WebHost 中的 IConfiguration 对象。需要注意与上面ConfigureAppConfiguration的区别， WebHost 中的 Configuration 只限于在 WebHost 使用，并且我们不能配置它的数据源，它只会读取ASPNETCORE_开头的环境变量。而我们比较熟悉的当前执行环境，也是通过该_config来读取的，虽然我们不能配置它的数据源，但是它为我们提供了一个UseSetting方法，为我们提供了一个设置_config的机会。而我们通过UseSetting设置的变量最终也会以MemoryConfigurationProvider的形式添加到上面介绍的ConfigureAppConfiguration所配置的IConfiguration对象中。           
>+ UseStartup: UseStartup 这个我们都比较熟悉，它用来显式注册我们的Startup类，可以使用泛性，Type , 和程序集名称三种方式来注册。通过反射创建实例，然后注入到 DI 系统中。    
>+ ConfigureLogging: ConfigureLogging 用来配置日志系统，在 ASP.NET Core 1.x 中是在Startup类的Configure方法中，通过ILoggerFactory扩展来注册的，在 ASP.NET Core 中也变得更加简洁，并且统一通过 WebHostBuild 来配置。       
>+ ConfigureServices: 在上面的几个方法中，多次用到 ConfigureServices，而 ConfigureServices 与 Starup 中的 ConfigureServices 类似，都是用来注册服务的。       

> **ISartup**: ISartup 是我们比较熟悉的，因为在我们创建一个默认的 ASP.NET Core 项目时，都会有一个Startup.cs文件，包含三个约定的方法，按执行顺序排列如下：   
>+ ConfigureServices : ASP.NET Core 框架本身提供了一个 DI（依赖注入）系统，并且可以非常灵活的去扩展，很容易的切换成其它的 DI 框架（如 Autofac，Ninject 等）。在 ASP.NET Core 中，所有的实例都是通过这个 DI 系统来获取的，并要求我们的应用程序也使用 DI 系统，以便我们能够开发出更具弹性，更易维护，测试的应用程序。总之在 ASP.NET Core 中，一切皆注入。关于 “依赖注入” 这里就不再多说。**在 DI 系统中，想要获取服务，首先要进行注册，而ConfigureServices方法便是用来注册服务的**。            
>+ ConfigureContainer（不常用）:ConfigureContainer 是用来替换 DI 框架的，如下，我们将 ASP.NET Core 内置的 DI 框架替换为 Autofac ：
```C#
public void ConfigureContainer(ContainerBuilder builder)
{
    builder.RegisterModule(new AutofacModule());
}
```
>+ Configure： Configure 接收一个IApplicationBuilder类型参数，用来构建请求管道的，因此，也可以说 Configure 方法是用来配置请求管道的，通常会在这里会注册一些中间件。所谓中间件，也就是对 HttpContext 进行处理的一种便捷方式。而如Startup中的代码，我们注册了一个最简单的中间件，通过浏览器访问，便可以看到 “Hello ASP.NET Core!” 。            

> **IHostingStartup**：一个项目中只能一个Sartup，因为如果配置多个，则最后一个会覆盖之前的。而在一个多层项目中，Sartup类一般是放在展现层中，我们在其它层也需要注册一些服务或者配置请求管道时，通常会写一个扩展方法。然后在 Startup 中调用这些扩展方法。
>+ WebHost 会在 Starup 这前调用 IHostingStartup，于是可以用`[assembly: HostingStartup(typeof(Zero.EntityFramework.EFRepositoryStartup))]`来进行调用，避免修改Startup代码。但还是需要修改Program中的Host代码，调用UseSetting方法注册。同时还要将相关的dll放到对应的bin目录里。     
>+ **IHostingStartup 是由 WebHostBuilder 来调用的，执行时机较早，在创建 WebHost 之前执行，因此可以替换一些在 WebHost 中需要使用的服务。**        

> **IStartupFilter**： IStartupFilter 是除Startup和HostingStartup之处另一种配置IApplicationBuilder的方式。它只有一个Configure方法，是对 Starup 类中Configure方法的拦截器，给我们一个在Configure方法执行之前进行一些配置的机会。WebHost 在执行 Starup 类中Configure方法之前，会从 DI 系统中获取所有的IStartupFilter来执行（方法ConfigureServicees先于Configure执行）。注意：配置都是首次运行时执行一次，后面是不在执行了。   

> **IHostedService**：当我们希望随着 ASP.NET Core 的启动，来执行一些后台任务（如：定期的刷新缓存等）时，并在 ASP.NET Core 停止时，可以优雅的关闭，则可以使用IHostedService。见代码：CacheHostService类的实现。     

> **IApplicationLifetime**：IApplicationLifetime用来实现 ASP.NET Core 的生命周期钩子，我们可以在 ASP.NET Core 停止时做一些优雅的操作，如资源的清理等。IApplicationLifetime已被 ASP.NET Core 注册到 DI 系统中，我们使用的时候，只需要注入即可。它有三个CancellationToken类型的属性，是异步方法终止执行的信号，表示 ASP.NET Core 生命周期的三个阶段：启动，开始停止，已停止。                 
[参考1](http://www.cnblogs.com/RainingNight/p/hosting-configure-in-asp-net-core.html)     
[ASP-NET-Core-2-IHostedService](https://www.stevejgordon.co.uk/asp-net-core-2-ihostedservice)        
[ASPNET-Core-2.0-Stripping-Away-Cross-Cutting-Concerns](https://cetus.io/tim/ASPNET-Core-2.0-Stripping-Away-Cross-Cutting-Concerns/)          
[Looking-at-asp-net-cores-iapplicationlifetime](http://www.khalidabuhakmeh.com/looking-at-asp-net-cores-iapplicationlifetime)        













