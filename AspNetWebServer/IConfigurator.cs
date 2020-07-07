using AspNetWebServer.Pipelines;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetWebServer
{
    public interface IConfigurator
    {
        void Configure(MiddlewareBuilder middlewareBuilder);
    }
}
