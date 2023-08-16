using System;
using System.Text;
using RabbitMQ.Client;

namespace Prodrucer
{
     class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };// Cria as conexoes com o RabbitMQ
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "FirstQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            int count = 0;
            while(count < 10) {
                const string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: "FirstQueue",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($" [x] Sent {message}");
                System.Threading.Thread.Sleep(5000);
                count++;
            }

           
        }
    }
}
