using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static AspNetWebServer.HttpHandlers;

namespace AspNetWebServer.Middleware
{
    public class LoggingMiddleware : IMiddleware
    {
        public HttpHandler next { get; set; }

        public LoggingMiddleware(HttpHandler next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpListenerContext context)
        {
            Console.WriteLine($"Resuqest Method: {context.Request.HttpMethod}, Requeest Path: {context.Request.RawUrl}, Date Time :{DateTime.Now}");
            next?.Invoke(context);
        }
    }
}
