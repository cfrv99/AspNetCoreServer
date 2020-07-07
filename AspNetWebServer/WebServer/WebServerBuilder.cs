using AspNetWebServer.Pipelines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static AspNetWebServer.HttpHandlers;

namespace AspNetWebServer.WebServer
{
    public class WebServerBuilder
    {
        public string Domain { get; set; }
        public int Port { get; set; }
        public HttpListener httpListener;
        public HttpHandler middleware;
        public WebServerBuilder(string domain, int port)
        {
            Domain = domain;
            Port = port;
            httpListener = new HttpListener();
            httpListener.Prefixes.Add($"{Domain}:{Port}/");
        }

        public void Run()
        {
            httpListener.Start();
            while (true)
            {
                var context = httpListener.GetContext();

                Process(context);
            }
        }

        public void UseStartup<T>() where T : IConfigurator, new()
        {
            var config = new T();
            var builder = new MiddlewareBuilder();
            config.Configure(builder);
            middleware = builder.Build();
        }

        public void Process(HttpListenerContext context)
        {
            middleware?.Invoke(context);
            context.Response.Close();
        }

    }
}
