using System;
using RabbitMQ.Client;
using System.Text;

namespace Case
{
    class Producer
    {
        // Method to send a message to RabbitMQ queue
        public void SendMessage(string jsonData, string queueName)
        {
            // Create a connection factory with the RabbitMQ server hostname
            var factory = new ConnectionFactory() { HostName = "localhost" };

            // Create a connection and a channel to communicate with RabbitMQ server
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare the queue with specified queueName
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Convert the jsonData to a byte array using UTF-8 encoding
                var body = Encoding.UTF8.GetBytes(jsonData);

                // Publish the message to the specified queue
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);

                // Print a confirmation message to the console indicating the sent message
                Console.WriteLine($" [x] Sent Message ({queueName}): {jsonData}");
            }
        }
    }
}
