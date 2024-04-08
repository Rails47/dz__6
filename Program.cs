using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.100.3");
            int port = 12345;

            UdpClient udpServer = new UdpClient(port);

            try
            {
                Console.WriteLine("Сервер запущен. Ожидание запросов...");

                while (true)
                {
                    IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = udpServer.Receive(ref clientEndPoint);
                    string request = Encoding.UTF8.GetString(data);

                    string response = ProcessRequest(request);
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    udpServer.Send(responseData, responseData.Length, clientEndPoint);

                    Console.WriteLine($"Запрос обработан от {clientEndPoint}: {request}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                udpServer.Close();
            }
        }

        static string ProcessRequest(string request)
        {
            switch (request.ToLower())
            {
                case "процессор":
                    return "Цена на процессор: $200";
                case "материнская плата":
                    return "Цена на материнскую плату: $150";
                case "видеокарта":
                    return "Цена на видеокарту: $300";
                default:
                    return "Запчасть не найдена";
            }
        }
    }
}
