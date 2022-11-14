using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorSimulator.RabbitMQProducer
{
    public interface IMessageProducer
    {
        void SendMessage<T> (T message);
    }
}
