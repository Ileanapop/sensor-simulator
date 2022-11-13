using System;
using System.Collections.Generic;
using System.Linq;
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

        public BackgroundTask(TimeSpan interval, TextBox textBox)
        {
            _timer = new PeriodicTimer(interval);
            this.textBox = textBox;
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
                    textBox.AppendText(DateTime.Now.ToString("O"));
                    textBox.AppendText(Environment.NewLine);

                }

            }
            catch (OperationCanceledException)
            {

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
