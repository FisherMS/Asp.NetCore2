using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hosting
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly IList<Func<RequestDelegate, RequestDelegate>> _middlewares
            = new List<Func<RequestDelegate, RequestDelegate>>();

        public RequestDelegate Build()
        {
            RequestDelegate seed = context => Task.Run(() => { });
            return _middlewares.Reverse().Aggregate(seed, (next, current) => current(next));
        }

        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}