using System;
using System.IO;
using DxHumanModel.Core;
using DxHumanModel.Transporters;
using ProtoBuf;
using StackExchange.Redis;

namespace DxHumanModel
{
    class Program
    {
        private static ConnectionMultiplexer redis;

        private static readonly int incrementsPerMeal = 10;
        private static readonly string mealChannel = "Meals";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Program.redis = ConnectionMultiplexer.Connect("localhost");

            var localSubscriber = Program.redis.GetSubscriber();


            Transporter trprt = new Transporter(Program.incrementsPerMeal, Program.redis, Program.mealChannel);


            localSubscriber.Subscribe("timerQueue", (ChannelMessage, bytes) =>
            {
                using (var memoryStream = new MemoryStream((byte[])bytes))
                {
                    var message = Serializer.Deserialize<ClockEvent>(memoryStream);
                    Console.WriteLine($"Incremented to {message.Increment}");
                    trprt.PublishShip(message.Increment);
                }
            });
            var clock = new Clock(Program.redis, "timerQueue");

            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            clock.Stop();
            localSubscriber.Unsubscribe("timerQueue");
            Console.WriteLine("Terminating the application...");
        }
    }
}
