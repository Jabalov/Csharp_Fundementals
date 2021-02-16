using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {

        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip = null;

            try
            {
                Console.WriteLine("Socket Client Starte:");
                Console.WriteLine("Write a valid ip address and press enter!");
                string strIPAddress = Console.ReadLine();

                Console.WriteLine("Apply a valid port number:");
                string strPort = Console.ReadLine();
                int nPort = 0;

                IPAddress ipaddr;
                if (!IPAddress.TryParse(strIPAddress, out ipaddr))
                {
                    Console.WriteLine("Invalid Server IP");
                    return;
                }
                if (!int.TryParse(strPort, out nPort))
                {
                    Console.WriteLine("Invalid Server Port");
                    return;
                }

                if (nPort <= 0 || nPort > 65535)
                {
                    Console.WriteLine("Enter a Valid Port Number");
                }

                Console.WriteLine($"IPAddress: {ipaddr.ToString()}, PortNumber: {nPort}");
                client.Connect(ipaddr, nPort);

                string inputCommand = string.Empty;

                while(true)
                {
                    inputCommand = Console.ReadLine();
                    if (inputCommand == "exit")
                        break;

                    var bufferSend  = Encoding.ASCII.GetBytes(inputCommand);
                    client.Send(bufferSend);
                    
                    var bufferRecieved = new byte[128];
                    int nRec = client.Receive(bufferRecieved);

                    Console.WriteLine($"{ Encoding.ASCII.GetString(bufferRecieved, 0, nRec)}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if(client != null)
                {
                    if(client.Connected)
                        client.Shutdown(SocketShutdown.Both);

                    client.Close();
                    client.Dispose();
                }
                
            }

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
