namespace platformy_lab12;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

class Program
{
    public static void run(string[] args)
    {
        Server server = new Server("127.0.0.1", 420);
    }
}
class Server
{
    TcpListener server = null;
    public Server(string ip, int port)
    {
        IPAddress localAddr = IPAddress.Parse(ip);
        server = new TcpListener(localAddr, port);
        server.Start();
        StartListener();
    }

    public void StartListener()
    {
        try
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected");

                Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                t.Start(client);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
            server.Stop();
        }
    }

    public void HandleDeivce(Object obj)
    {
        TcpClient client = (TcpClient)obj;
        var stream = client.GetStream();

        byte[] bytes = new byte[1024];
        int length = stream.Read(bytes, 0, bytes.Length);
        BinaryFormatter formatter = new BinaryFormatter();
        using (var ms = new MemoryStream(bytes, 0, length))
        {
            var data = formatter.Deserialize(ms);

            byte[] msg = new byte[1024];
            ms.Position = 0;
            length = ms.Read(msg, 0, msg.Length);
            Console.WriteLine(((Message)data).time.ToString());
            Console.WriteLine(((Message)data).name);
            Console.WriteLine(((Message)data).id);
            Console.WriteLine(((Message)data).message);
            Console.WriteLine(((Message)data).id + ((Message)data).time.Year); 
        }

    }
}
