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
            IServer server = new Server(IPAddress.Parse("127.0.0.1"), 8005);

            server.OnStart += () => { Console.WriteLine('\n' + "Started"); };
            server.OnStop  += () => { Console.WriteLine('\n' + "Stop"); };
            server.Start();

            
            Next:
            if (Console.ReadKey().KeyChar == 'n') server.Stop();
            

            if (Console.ReadKey().KeyChar == 'y') server.Start();


            goto Next; 


        }

    }
}
