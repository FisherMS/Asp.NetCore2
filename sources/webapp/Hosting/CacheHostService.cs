using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace webapp.Hosting
{

    /// <summary>
    /// 当我们希望随着 ASP.NET Core 的启动，来执行一些后台任务（如：定期的刷新缓存等）时，并在 ASP.NET Core 停止时，可以优雅的关闭，则可以使用IHostedService
    /// 如上，我们定义了一个在台后每5秒刷新一次缓存的服务，并在 ASP.NET Core 程序停止时，优雅的关闭。最后，将它注册到 DI 系统中即可
    /// </summary>
    public class CacheHostService : IHostedService
    {
        private readonly ICacheService _cacheService;
        private CancellationTokenSource _cts;
        private Task _executingTask;

        public CacheHostService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _executingTask = Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    Console.WriteLine("cancellationToken:" + _cts.IsCancellationRequested);
                    await _cacheService.Refresh();
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // 发送停止信号，以通知我们的后台服务结束执行。
            _cts.Cancel();

            // 等待后台服务的停止，而 ASP.NET Core 大约会等待5秒钟（可在上面介绍的UseShutdownTimeout方法中配置），如果还没有执行完会发送取消信号，以防止无限的等待下去。
            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));

            cancellationToken.ThrowIfCancellationRequested();
        }
    }

    public interface ICacheService
    {
        Task Refresh();
    }

    public class CacheService : ICacheService
    {
        public  Task Refresh()
        {
            return  Task.Run(()=> Console.WriteLine($"Reresh:{DateTime.Now:HH:mm:ss fffff}"));
        }
    }
}