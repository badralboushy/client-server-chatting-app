using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

/*
 * 
 * author : badr
 * 
 * 
 */
namespace Server
{
    class Server
    {
        static int _clients;

        static void Main(string[] args)
        {
            var serverFile = "Server.txt";
            File.Delete(serverFile);
            string s = "HI this is a test \n";
            string line = String.Empty;
            Console.WriteLine("server preparing the file ..");
            for(int i=0; i<100; i++)
            {
                line += s;

            }
            for (int i = 0; i < 100; i++)
                File.AppendAllText(serverFile, line);

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, 4000);
            server.Start();
            Console.WriteLine("Server Started");
            ConsoleColor[] colors = new ConsoleColor[5]
            {
                ConsoleColor.Blue,
                ConsoleColor.Cyan,
                ConsoleColor.Yellow,
                ConsoleColor.Magenta,
                ConsoleColor.Green
            };
            while (true)
            {
                var recClient = server.AcceptSocket();
                _clients++;
                Console.WriteLine("New Client Joined\n\n\n");
                new Thread(() =>
                {
                    ServeClient(recClient, serverFile, colors[_clients % 5]);
                    

                }).Start();
            }
        }
        static void ServeClient(Socket client, string file, ConsoleColor color)
        {
            try
            {
                Console.WriteLine("************start*************");
                byte[] tosend = Encoding.ASCII.GetBytes(file);
                byte[] content = File.ReadAllBytes(file);

                byte[] whatosend = new byte[6 + tosend.Length + content.Length];

                byte[] fileNameLen = BitConverter.GetBytes(tosend.Length);
                fileNameLen.CopyTo(whatosend, 0);
                    tosend.CopyTo(whatosend, 6);
                content.CopyTo(whatosend, 6 + tosend.Length);
                client.Send(whatosend);
                Console.WriteLine("************it's ended   ***********");
                //sr.Close();
                //client.Close();
                client.Close();

            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
            }
           
        }
    }
}
