using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static AspNetWebServer.HttpHandlers;

namespace AspNetWebServer.Middleware
{
    public class StaticFileMiddleware : IMiddleware
    {
        public HttpHandlers.HttpHandler next { get; set; }

        public StaticFileMiddleware(HttpHandler next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpListenerContext context)
        {
            if (context.Request.RawUrl.Contains("."))
            {
                var types = new Dictionary<string, string>()
                {
                    ["html"] = "text/html",
                    ["css"] = "text/css",
                    ["json"] = "application/json",
                    ["xml"] = "application/xml",
                    ["ico"] = "image/x-icon"
                };
                var path = "wwwroot" + context.Request.RawUrl;
                if (File.Exists(path))
                {
                    var file = File.ReadAllBytes(path);
                    var type = types[path.Split('.').Last()];
                    context.Response.ContentType = type;
                    context.Response
                          .OutputStream
                          .Write(file, 0, file.Length);
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
                context.Response.Close();
            }
            else
            {
                await this.next.Invoke(context);
            }
        }
    }
}
