 using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Loopback;
            IPEndPoint endPoint = new IPEndPoint(ip, 23000);

            try
            {
                socketListener.Bind(endPoint);
                socketListener.Listen(5);

                Socket client = socketListener.Accept();
                Console.WriteLine($"Client Connected {client.ToString()} - IP End-Point {client.RemoteEndPoint.ToString()}");

                byte[] buffer = new byte[128];
                int numOfRecievedBytes = 0;

                while (true)
                {
                    numOfRecievedBytes = client.Receive(buffer);
                    Console.WriteLine($"Number Of recieved bytes: {numOfRecievedBytes}");

                    var recieved = Encoding.ASCII.GetString(buffer, 0, numOfRecievedBytes);
                    Console.WriteLine($"Data is: {recieved}");

                    client.Send(buffer);

                    if (recieved == "x")
                        break;

                    Array.Clear(buffer, 0, buffer.Length);
                    numOfRecievedBytes = 0;
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }

    }
}
