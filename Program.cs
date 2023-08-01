using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Case
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two separate threads for running Producer and Consumer simultaneously.
            Thread producerThread = new Thread(() => StartProducer());
            Thread consumerThread = new Thread(() => StartConsumer());

            producerThread.Start();
            consumerThread.Start();

            // Wait for Producer and Consumer threads to complete.
            producerThread.Join();
            consumerThread.Join();

            Console.WriteLine("Application completed. Press any key to exit.");
            Console.ReadKey();
        }

        // Method to start the Producer thread.
        static void StartProducer()
        {
            var jsonData = GetDataFromAPI();
            var producer = new Producer();
            producer.SendMessage(jsonData, "hello");
        }

        // Method to fetch data from the API.
        static string GetDataFromAPI()
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync("https://dummyjson.com/products").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new Exception("Failed to fetch data from the API.");
                }
            }
        }

        // Method to start the Consumer thread.
        static void StartConsumer()
        {
            var consumer = new Consumer();
            consumer.StartConsuming();
        }

        // Method to save products to the database from the received JSON data.
        static void SaveProducts(string jsonData)
        {
            var productObjects = JObject.Parse(jsonData)["products"].ToObject<List<JObject>>();

            using (var dbContext = new AppDbContext())
            {
                foreach (var productObject in productObjects)
                {
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

                    dbContext.Products.Add(product);

                    var images = productObject["images"];
                    if (images != null && images.HasValues)
                    {
                        foreach (var imageUrl in images)
                        {
                            var productImage = new ProductImage
                            {
                                ProductId = product.Id,
                                ImageUrl = imageUrl.ToString()
                            };

                            dbContext.ProductImages.Add(productImage);
                        }
                    }

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
