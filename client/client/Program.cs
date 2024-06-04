
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    static void Main(string[] args)
    {
        Client client = new Client("127.0.0.1", 420);

        client.SendData("Hello, Server!");
    }
}
class Client
{
    TcpClient client;
    public Client(string serverIP, int port)
    {
        client = new TcpClient(serverIP, port);
    }

    public void SendData(object data)
    {
        NetworkStream stream = client.GetStream();

        BinaryFormatter formatter = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            formatter.Serialize(ms, data);
            byte[] bytes = ms.ToArray();
            stream.Write(bytes, 0, bytes.Length);

            // Receiving data
            byte[] resp = new byte[1024];
            int length = stream.Read(resp, 0, resp.Length);
            using (var msResp = new MemoryStream(resp, 0, length))
            {
                var receivedData = formatter.Deserialize(msResp);
                // Process received data here
            }
        }
    }
}
