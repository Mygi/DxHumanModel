using System;
using StackExchange.Redis;
using System.Timers;
using System.IO;
using ProtoBuf;

namespace DxHumanModel.Core
{
    [ProtoContract]
    public class ClockEvent
    {
        [ProtoMember(1)]
        public int Increment { get; set; }

        [ProtoMember(2)]
        public DateTime ElapsedTime { get; set; }

    }
    public class Clock
    {
        private int ticks = 0;
        private int maxticks = 10000;
        private double _interval = 1000;
        private Timer _timer;
        private ISubscriber sub;
        private string timerChannel;

        public Clock(ConnectionMultiplexer redis, string timerChannel)
        {
            this.sub = redis.GetSubscriber();
            this.timerChannel = timerChannel;
            //this.Subscribe();
            this.SetTimer();


          

        }
        public void Stop()
        {
            this._timer.Stop();
            this._timer.Dispose();
        }
       /* private void Subscribe()
        {
            this.sub.Subscribe(this.timerChannel, (ChannelMessage, message) =>
            {
                if (message == "Stop")
                {
                    sub.Unsubscribe(this.timerChannel);
                }
            });

        }*/
        private void SetTimer()
        {
           this._timer = new Timer(this._interval);
            this._timer.Elapsed += tick;

            this._timer.AutoReset = true;
            this._timer.Enabled = true;
            this._timer.Start();

        }
        public void tick(Object source, ElapsedEventArgs e)
        {
            if( this.ticks < this.maxticks)
            {
                this.ticks++;
            }
            else
            {
                Console.WriteLine(this.maxticks);
                this._timer.Stop();
                sub.Unsubscribe(this.timerChannel);
            }
            var value = new ClockEvent()
            {
                Increment = this.ticks,
                ElapsedTime = e.SignalTime
            };

            using (var memoryStream = new MemoryStream())
            {
                // serialize a ChatMessage using protobuf-net
                Serializer.Serialize(memoryStream, value );
                Console.WriteLine("Publishing" + value.ElapsedTime.ToString() + " - " + value.ElapsedTime.ToString());
                this.sub.Publish(this.timerChannel, memoryStream.ToArray());
            }         
        }

    }


}
