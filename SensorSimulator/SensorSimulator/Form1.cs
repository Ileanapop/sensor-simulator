using SensorSimulator.RabbitMQProducer;

namespace SensorSimulator
{
    public partial class Form1 : Form
    {

        public string UserDeviceId { get; set; }

        private BackgroundTask? backgroundTask;

        public Form1(string userDeviceId)
        {
            InitializeComponent();
            UserDeviceId = userDeviceId;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = UserDeviceId;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IMessageProducer producer = new RabbitMQProducerMessage();
            backgroundTask = new BackgroundTask(TimeSpan.FromMilliseconds(1000),textBox1,producer,UserDeviceId);
            backgroundTask.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(backgroundTask != null)
                backgroundTask.StopAsync();
        }
    }
}