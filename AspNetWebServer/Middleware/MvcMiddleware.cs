using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static AspNetWebServer.HttpHandlers;

namespace AspNetWebServer.Middleware
{
    public class MvcMiddleware : IMiddleware
    {
        public HttpHandler next { get; set; }

        public MvcMiddleware(HttpHandler next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpListenerContext context)
        {
            try
            {
                var parts = context.Request.RawUrl.Split('?')[0].Split('/');
                var controllerName = parts[1] + "CONTROLLER";
                var methodName = parts[2];
                var controllerType = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .FirstOrDefault(t => t.Name.Equals(controllerName,
                                StringComparison.InvariantCultureIgnoreCase));
                if (controllerType == null)
                {
                    context.Response.StatusCode = 404;
                    Console.WriteLine("Sagol ))");
                    context.Response.Close();
                    return;
                }
                var actionInfo = controllerType.GetMethods()
                                .FirstOrDefault(m => m.Name.Equals(methodName,
                                StringComparison.InvariantCultureIgnoreCase));
                if (actionInfo == null)
                {
                    context.Response.StatusCode = 404;
                    Console.WriteLine("Sagol)");

                    context.Response.Close();
                    return;
                }
                var controller = Activator.CreateInstance(
                    controllerType);

                //var pars = actionInfo.GetParameters();
                //var args = new object[pars.Length];
                //// ?id=42&name=orxan
                //// string Index(string id, string name)
                //for (int i = 0; i < pars.Length; ++i)
                //{
                //    var val = context.Request.QueryString[pars[i].Name];
                //    var type = pars[i].ParameterType;
                //    var arg = Convert.ChangeType(val, type);
                //    args[i] = arg;
                //}

                var result = actionInfo.Invoke(controller, new object[0]);
                context.Response.ContentType = "text/html";
                using (var sw = new StreamWriter(context.Response.OutputStream))
                {
                    sw.Write(result.ToString());

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Sagol))");
            }
            context.Response.Close();
            next?.Invoke(context);

        }
    }
}
