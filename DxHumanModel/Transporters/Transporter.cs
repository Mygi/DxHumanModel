using System;
using System.Collections.Generic;
using System.IO;
using DxHumanModel.Inputs;
using ProtoBuf;
using StackExchange.Redis;

namespace DxHumanModel.Transporters
{
    public class Transporter
    {
        private readonly ProbabilityMap H2O = new ProbabilityMap((double)0.2, (double)0.5);
        private readonly ProbabilityMap Na = new ProbabilityMap((double)0.005, (double)0.005);
        private readonly ProbabilityMap K = new ProbabilityMap((double)0.002, (double)0.001);
        private readonly ProbabilityMap Other = new ProbabilityMap((double)0.2, (double)0.4);

        private int intervalsPerMeal;
        private ISubscriber sub;
        private string shipChannel;

        public Transporter(int intervals, ConnectionMultiplexer redis, string shipChannel)
        {
            intervalsPerMeal = intervals;

            this.sub = redis.GetSubscriber();
            this.shipChannel = shipChannel;
        }
        public void PublishShip(int increment)
        {
            if ( increment % this.intervalsPerMeal == 0)
            {
                Console.WriteLine("We have a meal");
                Meal meal = new Meal();
                Console.WriteLine("We construct a meal");
                var value = new TransportEvent()
                {
                    Concentrations = meal.Fill(),
                    volume = meal.GetVolume(),
                    messageIcrement = increment
                };
                Console.WriteLine("We created a value {0}", value);
                using (var memoryStream = new MemoryStream())
                {
                    // serialize a ChatMessage using protobuf-net
                    Serializer.Serialize(memoryStream, value);
                    Console.WriteLine("Publishing meal: Volume: {0}, H0 Concentrations: {1} at increment: {2}" + value.volume, value.Concentrations, value.messageIcrement);
                    this.sub.Publish(this.shipChannel, memoryStream.ToArray());
                }
            }
        }
        public void Dispose()
        {
            this.sub.Unsubscribe(this.shipChannel);
        }
    }
}
