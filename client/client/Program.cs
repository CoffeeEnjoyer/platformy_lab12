
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter name:");
        String name = Console.ReadLine();
        Client client = new Client("127.0.0.1", 420, name);
        
        while (true)
        {
            String message = Console.ReadLine();
            client.SendMessage(message);
        }

    }
}
class Client
{
    private String name;
    private static int id = 0;
    TcpClient client;
    public Client(string serverIP, int port, String n)
    {
        client = new TcpClient(serverIP, port);
        name = n;
    }

    public void SendMessage(String message)
    {
        NetworkStream stream = client.GetStream();
        
        Message data = new Message();
        data.name = name;
        data.message = message;
        data.id = id;
        data.time = DateTime.Now;

        BinaryFormatter formatter = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            formatter.Serialize(ms, data);
            byte[] bytes = ms.ToArray();
            stream.Write(bytes, 0, bytes.Length);
        }

        id++;
    }
}

[Serializable]
class Message
{
    public String name;
    public String message;
    public int id;
    public DateTime time;
}