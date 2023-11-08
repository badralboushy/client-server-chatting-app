using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

/*
 * 
 * author : badr
 * 
 * 
 */
namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            //var client = (Socket)socket;
            //StreamReader sr = new StreamReader(path);
            //var tosend = sr.ReadLine();
            //tosend = tosend + "\n";
            //while (tosend != null)
                TcpClient client = new TcpClient();
            client.Connect("localhost", 4000);
            if (client.Connected)
            {
                Console.WriteLine( "Enter path: ");
                byte[] buffer = new byte[1024 * 1000];
                //var client = (Socket)socket;
                //StreamReader sr = new StreamReader(path);
                //var tosend = sr.ReadLine();
                //tosend = tosend + "\n";
                string path = Console.ReadLine();
                
                var stream = client.GetStream();
                int bytes = stream.Read(buffer, 0, buffer.Length);
                int length = BitConverter.ToInt32(buffer, 0);

                string name = Encoding.ASCII.GetString(buffer, 6, length);
                //var serverMessage = Encoding.ASCII.GetBytes(tosend);
                //client.Send(serverMessage);
                //tosend = sr.ReadLine();
                string add = String.Empty;
                for(int i=1; i<=10000; i++)
                {
                    add += Encoding.ASCII.GetString(buffer, 6 + (19 * (i-1)) , 19);
                }
                var fileWriter = new BinaryWriter(File.Open(path + name, FileMode.Append));
                fileWriter.Write(buffer, 6 + length, bytes - 6 - length);
                //client.Send(Encoding.ASCII.GetBytes("bye"));

                stream.Close();
                client.Close();
                Console.WriteLine(" ************************* FIN *************************************");
                string a;
                a = Console.ReadLine();
               
            }

            //    while (true)
            //    {
            //        byte[] recBytes = new byte[2];
            //        int length = client.Receive(recBytes);
            //        if (length >= 1)
            //        {
            //            var message = Encoding.ASCII.GetString(recBytes, 0, length);
            //            Console.ForegroundColor = color;
            //            Console.WriteLine($"Client:" + message);
            //            var serverMessage = Encoding.ASCII.GetBytes(message);
            //            client.Send(serverMessage);
            //            if (message == "bye")
            //            {
            //                _clients--;
            //                Console.ForegroundColor = color;
            //                Console.WriteLine("Client Left");
            //                Console.WriteLine("Remaining Client: " + _clients);
            //                client.Close();
            //                break;
            //            }
            //        }
            //    }


        }
    }
}
