namespace Hosting
{
    public interface IServer
    {
        void Start<TContext>(IHttpApplication<TContext> application);
    }
}
