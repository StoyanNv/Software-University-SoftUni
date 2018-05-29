using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _03.Simple_Web_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = IPAddress.Parse("127.0.0.1");
            var port = 3456;
            var server = new TcpListener(address, port);
            server.Start();
            Console.WriteLine("Server started");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1:{port}");
            Task.Run(async () => await ConnectWithTcpClient(server)).Wait();
        }

        public static async Task ConnectWithTcpClient(TcpListener listener)
        {
            while (true)
            {
                Console.WriteLine("Waiting for client...");
                var client = await listener.AcceptTcpClientAsync();

                Console.WriteLine("Client connected");
                var request = new byte[1024];
                await client.GetStream().ReadAsync(request, 0, request.Length);

                Console.WriteLine(Encoding.UTF8.GetString(request));

                var data = Encoding.UTF8.GetBytes("Hello from server!");
                await client.GetStream().WriteAsync(data, 0, data.Length);
                Console.WriteLine("Closing connection");
                client.Dispose();
            }
        }
    }
}
