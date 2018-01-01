using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ModelLibrary;
using Newtonsoft.Json;
using REST_WCF_TEMPLATE;

namespace REST_CLIENT_TEMPLATE
{
    //for eksamen
    class Program
    {
        static void Main(string[] args)
        {
            RestClient client = new RestClient();
            client.Start();

            Console.ReadLine();
        }
    }
}
