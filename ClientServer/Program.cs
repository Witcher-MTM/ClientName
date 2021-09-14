using System;
using System.Collections.Generic;
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
                        stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (socketClient.Available > 0);


                    Console.WriteLine($"MSG: {stringBuilder.ToString()}");

                    arr = stringBuilder.ToString().Split(' ');

                    foreach (var item in arr)
                    {
                        if (!words_count.ContainsKey(item))
                        {
                            words_count.Add(item, 1);
                        }
                        else
                        {
                            words_count[item]++;
                        }
                    }
                    for (int i = 0; i < words_count.Count; i++)
                    {
                        str += words_count.ElementAt(i).Key + " "+ words_count.ElementAt(i).Value + "\n";
                    }
                  
                    socketClient.Send(Encoding.Unicode.GetBytes(str));
                   
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Shluxa");
            }
        }
    }
}
