using AspNetWebServer.Middleware;
using System;
using System.Collections.Generic;
using System.Text;
using static AspNetWebServer.HttpHandlers;

namespace AspNetWebServer.Pipelines
{
    public class MiddlewareBuilder
    {
        private Stack<Type> types = new Stack<Type>();

        public MiddlewareBuilder Use<T>() where T : IMiddleware
        {
            types.Push(typeof(T));
            return this;
        }

        public HttpHandler Build()
        {
            HttpHandler handler = async (context) =>
            {
                context.Response.Close();
            };

            while (types.Count > 0)
            {
                Type type = types.Pop();
                //LoggingMiddleware mid = new LoggingMiddleware();
                var middleware = Activator.CreateInstance(type, handler) as IMiddleware;
                handler = middleware.Invoke;
            }
            //[LoggingMiddleware(),HtmlFileMiddleware(),A(),B()]
            return handler;
        }
    }
}
