using AspNetWebServer.Middleware;
using AspNetWebServer.Pipelines;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetWebServer
{
    public class Startup : IConfigurator
    {
        public void Configure(MiddlewareBuilder middlewareBuilder)
        {
            middlewareBuilder.Use<MvcMiddleware>();
            //middlewareBuilder.Use<HtmlFileMiddleware>();
            //middlewareBuilder.Use<LoggingMiddleware>();
        }
    }
}
