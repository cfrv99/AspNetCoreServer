using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebServer
{
    public class HttpHandlers
    {
        public delegate Task HttpHandler(HttpListenerContext context);
    }
}
