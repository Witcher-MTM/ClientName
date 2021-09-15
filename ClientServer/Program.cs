using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace ClientServer
{
    class Program
    {
        static int port = 8000;
        static void Main(string[] args)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Start server...");

            Dictionary<string, int> words_count = new Dictionary<string, int>();
            string[] arr = null;
            string str = String.Empty;
            try
            {
                socket.Bind(iPEndPoint);
                socket.Listen(10);

                while (true)
                {
                    Socket socketClient = socket.Accept();


                   
                   
                    StringBuilder stringBuilder = new StringBuilder();

                    int bytes = 0;
                    byte[] data = new byte[256];

                    do
                    {
                        bytes = socketClient.Receive(data);
                       
                        File.WriteAllBytes(DateTime.Now.ToString().Replace('.', '_').Replace(':', '_').Replace(' ', '_') + ".txt", data);

                    } while (socketClient.Available > 0);

                   





                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }
    }
}
