## dotnet core 的调试 In Visual Studio Code
> 1. 使用VSCode来搞搞
> 1. 打开VSCode，然后打开控制台（在 查看 菜单项下面）。
> 1. 转到目标目录，然后运行： ``dotnet new web --name DebugInVSCodeWeb``
> 1. 然后通过VSCode打开项目文件夹。
> 1. 在终端中使用 ``dotnet run`` 来运行。
> 1. 安装 C# for Visual Studio Code (powered by OmniSharp) 这个插件。
> 1. 切换到调试选项卡，点击调试按钮。打上断点，重新刷新页面，执行到断点后就能看到调试信息了。
> 1. 注意，如果更新了，要重新启动调试。

### 使用 Microsoft.DotNet.Watcher.Tools 来调试。
> 1. Microsoft.DotNet.Watcher.Tools 把加到项目文件csproj 中(``<DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.0.0" />``) ，然后执行 ``dotnet restore `` 。
> 1. 然后直接使用``dotnet watch run `` 命令来运行。然后，可以在运行的时候更改代码，它会自已重启并重新运行。
> 1. 在调试选项卡上选择 ``.Net Core Attch`` 点运行时会让你选项要附加到的进程（选择你当前的项目的运行的进程）。
> 1. 在附加进程调试的时候，如果修改了代码，那么程序会重新启动。这个时候就要重新附加了。 


