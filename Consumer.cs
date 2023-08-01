using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Case
{
    class Consumer
    {
        static void SaveProducts(string jsonData)
        {
            // Parse the JSON data and retrieve the list of product objects
            var productObjects = JObject.Parse(jsonData)["products"].ToObject<List<JObject>>();

            using (var dbContext = new AppDbContext())
            {
                foreach (var productObject in productObjects)
                {
                    // Create a new Product object and populate its properties from the JSON data
                    var product = new Product
                    {
                        Id = (int)productObject["id"],
                        Title = (string)productObject["title"],
                        Description = (string)productObject["description"],
                        Price = (decimal)productObject["price"],
                        DiscountPercentage = (decimal)productObject["discountPercentage"],
                        Rating = (decimal)productObject["rating"],
                        Stock = (int)productObject["stock"],
                        Brand = (string)productObject["brand"],
                        Category = (string)productObject["category"],
                        Thumbnail = (string)productObject["thumbnail"]
                    };

                    // Add the product to the database context
                    dbContext.Products.Add(product);

                    var images = productObject["images"];
                    if (images != null && images.HasValues)
                    {
                        // Iterate through the images and create ProductImage objects for each image
                        foreach (var imageUrl in images)
                        {
                            var productImage = new ProductImage
                            {
                                ProductId = product.Id,
                                ImageUrl = imageUrl.ToString()
                            };

                            // Add the product image to the database context
                            dbContext.ProductImages.Add(productImage);
                        }
                    }

                    // Save the changes to the database
                    dbContext.SaveChanges();
                }
            }
        }

        public void StartConsuming()
        {
            // Create a connection factory for RabbitMQ
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare the queue to consume messages from
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Create a consumer for the queue
                var consumer = new EventingBasicConsumer(channel);

                // Handle the received messages
                consumer.Received += (model, ea) =>
                {
                    // Convert the message body to a string
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    // Call the SaveProducts method to process and save the products to the database
                    SaveProducts(message);

                    // Print the received message to the console
                    Console.WriteLine(" [x] Received Message: {0}", message);
                };

                // Start consuming messages from the queue
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                // Wait for a key press to exit the application
                Console.WriteLine(" Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
