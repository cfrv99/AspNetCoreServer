using AspNetWebServer.WebServer;
using System;
using System.Net;

namespace AspNetWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServerBuilder builder = new WebServerBuilder("http://localhost", 8080);
            builder.UseStartup<Startup>();
            builder.Run();
        }
    }
}
