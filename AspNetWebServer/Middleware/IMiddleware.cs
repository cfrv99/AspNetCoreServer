using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static AspNetWebServer.HttpHandlers;

namespace AspNetWebServer.Middleware
{
    public interface IMiddleware
    {
        HttpHandler next { get; set; }
        Task Invoke(HttpListenerContext context);
    }
}
