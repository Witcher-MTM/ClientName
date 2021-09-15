using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static string ipAddr = "127.0.0.1";
        static int port = 8000;
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ipAddr), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(iPEndPoint);

                int bytes = 0;
                byte[] data = new byte[256];
                StringBuilder stringBuilder = new StringBuilder();

                Console.Write("Enter path: ");
                string sms = Console.ReadLine();
                try
                {
                    data = File.ReadAllBytes(sms);

                    socket.Send(data);
                    Console.WriteLine(File.ReadAllText(sms));
                    Console.WriteLine($"Sms \"{sms}\" send to SERVER [{ipAddr}]!");
                }
                catch (Exception)
                {

                    throw;
                }
              

                do
                {
                    bytes = socket.Receive(data);
                    stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);

                Console.WriteLine(stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
         

        }
    }
    
}
