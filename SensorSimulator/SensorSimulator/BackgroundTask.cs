using SensorSimulator.RabbitMQProducer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SensorSimulator
{
    public class BackgroundTask
    {
        private Task? _timerTask;

        private readonly PeriodicTimer _timer;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private TextBox textBox;

        private readonly IMessageProducer _messageProducer;

        private StreamReader _reader;

        private string UserDeviceID { get; set; }

        public BackgroundTask(TimeSpan interval, TextBox textBox,IMessageProducer messageProducer, string id)
        {
            _timer = new PeriodicTimer(interval);
            this.textBox = textBox;
            _messageProducer = messageProducer;
            UserDeviceID = id;
            _reader = new StreamReader(@"D:\Anul_4\NewDS\Assignment 2\sensor.csv");
        }

        public void Start()
        {
            _timerTask = DoWorkAsync();
        }

        private async Task DoWorkAsync()
        {
            try
            {
                while(await _timer.WaitForNextTickAsync(_cts.Token))
                {             
                    textBox.AppendText(Environment.NewLine);
                    

                    var measurement = 0.0;
                    if (!_reader.EndOfStream)
                        measurement = double.Parse(_reader.ReadLine());
                    else
                        throw new OperationCanceledException("End of file");

                    ReadingDTO reading = new()
                    {
                        Timestamp = DateTime.UtcNow.ToString("o"),
                        DeviceId = UserDeviceID,
                        MeasurementValue = measurement
                    };

                    textBox.AppendText(measurement.ToString());

                    _messageProducer.SendMessage(reading);


                }

            }
            catch (OperationCanceledException e)
            {
                textBox.Text = e.Message;
            }
        }

        public void StopAsync()
        {
            if(_timerTask is null)
            {
                return;
            }

            _cts.Cancel();
            _cts.Dispose();
            textBox.AppendText("Task cancelled");
        }
    }
}
