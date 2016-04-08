using ServerApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerApplication.Modules
{
    class Server : IServer
    {
        Socket _serversocket;

        public int? Port { get; set; }
        public IPAddress Ip { get; set; }

        public Server()
        {
            _serversocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public event ServerEvents OnStart;
        public event ServerEvents OnStop;

        public void Start()
        {
            if (Port == null || Ip == null) return;
            _serversocket.Bind(new IPEndPoint(Ip, (int)Port));
            _serversocket.Listen(10);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
