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
> 1. Microsoft.DotNet.Watcher.Tools 把加到项目文件csproj 中


