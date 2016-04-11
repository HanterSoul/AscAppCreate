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
        TcpListener _serversocket;
        
        public bool State { get; set; }

        public Server(IPAddress Ip, int Port)
        {           
            _serversocket = new TcpListener(Ip, Port);
        }

        public event ServerEvents OnStart;
        public event ServerEvents OnStop;

        public void Start()
        {        
            State = true;
            _serversocket.Start();

            Task.Run(Write);
            OnStart?.Invoke();
        }

        public void Stop()
        {
            State = false;           
            OnStop?.Invoke();
        }
        
        public async Task Write()
        {
            Byte[] bytes = new Byte[256];
            String data = null;
            TcpClient Client = null;

            await Task.Run(() =>
            {
                while (State)
                {
                    try
                    {
                        Client = _serversocket.AcceptTcpClient();
                        data = null;
                        NetworkStream stream = Client.GetStream();

                        int i;

                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                            Console.WriteLine("Received: {0}", data);
                            data = data.ToUpper();

                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                            
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine("Sent: {0}", data);

                        }
                        Client.Close();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }

                if (!State)
                {
                    NetworkStream stream = Client.GetStream();
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes("Close");
                    stream.Write(msg, 0, msg.Length);
                    _serversocket.Stop();
                }

            });
        }
    }
}
