using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ServerApplication.Interfaces
{
    public delegate void ServerEvents();

    public interface IServer
    {
        int? Port { get; set; } //порт
        IPAddress Ip { get; set; } //Ip адрес
        event ServerEvents OnStart;
        event ServerEvents OnStop;
        void Start();
        void Stop();
    }
}
