## Asp.net CORE HTTP 的相关技术与说明

> 1. 通过在Program中的BuildWebHost方法后加上ConfigureAppConfiguratin来覆盖原来的配置信息。在Startup中的Configure方法中读取配置信息内容。
> 1. 在项目属性->调试->应用程序参数 中加上``name=atwind``,然后Startup中读取出来。注意调试的时候出现的控制台里是不会有参数显示的。



