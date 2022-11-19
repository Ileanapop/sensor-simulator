using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorSimulator.RabbitMQProducer
{
    public class ReadingDTO
    {
        public string Timestamp { get; set; }
        public string DeviceId{ get; set; }

        public double MeasurementValue { get; set; }

    }
}
