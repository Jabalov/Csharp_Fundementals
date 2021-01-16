using System;
using RabbitMQ.Client;

namespace Demo
{
    class Program
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";
        static void Main(string[] args)
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory() 
            {
                Password = Password,
                UserName = UserName,
                HostName = HostName,
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            model.QueueDeclare("MyQueue", true ,false, false, null);
            Console.WriteLine("Queue Created!");

            model.ExchangeDeclare("MyExchange", ExchangeType.Topic);
            Console.WriteLine("Exchange Created");

            model.QueueBind("MyQueue", "MyExchange", "cars");
            Console.WriteLine("Exchange And Queue bound!");

            Console.ReadLine();
        }

    }
}
