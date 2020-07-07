using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetWebServer
{
    public class ProductController
    {
        public string Index()
        {
            return "<h1>Salam</h1>";
        }
        public string Home()
        {
            return "Home Action";
        }
    }
}
