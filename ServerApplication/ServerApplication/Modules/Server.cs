using ServerApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServerApplication.Modules
{
    public class Server : IServer
    {
        Socket _serversocket;


        public int? Port { get; set; }
        public IPAddress Ip { get; set; }
        public bool State { get; set; }

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
            State = true;
            Task.Run(Write);
            OnStart?.Invoke();
        }

        public void Stop()
        {
            State = false;
            OnStop();
            //  _serversocket.Shutdown(SocketShutdown.Both);
            _serversocket.Dispose();
            //_serversocket.Close();
        }
        
        public async Task Write()
        {
            int bytes = 0; // количество полученных байтов
            byte[] data = new byte[256]; // буфер для получаемых данных                       

            await Task.Run(() =>
            {
                while (State)
                {
                    try
                    {
                        Socket handler = _serversocket.Accept();
                        StringBuilder builder = new StringBuilder();

                        do
                        {
                            bytes = handler.Receive(data);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));

                        } while (handler.Available > 0);

                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                        // отправляем ответ
                        string message = "ваше сообщение доставлено";
                        data = Encoding.Unicode.GetBytes(message);
                        handler.Send(data);
                        // закрываем сокет
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
            });
        }
    }
}
