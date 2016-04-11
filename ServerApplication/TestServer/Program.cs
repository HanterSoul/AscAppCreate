using ServerApplication.Interfaces;
using ServerApplication.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IServer server = new Server()
            {
                Ip = IPAddress.Parse("127.0.0.1"),
                Port = 8005
            };
            server.OnStart += () => { Console.WriteLine("Started"); };
            server.OnStop  += () => { Console.WriteLine('\n'+"Stop"); };
            server.Start();

            if (Console.ReadKey().KeyChar == 'n') server.Stop();
            Console.ReadKey();

            if (Console.ReadKey().KeyChar == 'y') server.Start();
            Console.ReadKey();

        }

    }
}
