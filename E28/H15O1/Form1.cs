using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace H15O1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] portlar = SerialPort.GetPortNames();
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "SERİ PORTU BAŞLAT")
            {
                serialPort1.Open();
                button1.Text = "SERİ PORTU KAPAT";
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                serialPort1.Close();
                button1.Text = "SERİ PORTU BAŞLAT";
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "1. LED'İ YAK")
                button2.Text = "1. LED'İ SÖNDÜR";
            else
                button2.Text = "1. LED'İ YAK";
            serialPort1.Write("1");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "2. LED'İ YAK")
                button3.Text = "2. LED'İ SÖNDÜR";
            else
                button3.Text = "2. LED'İ YAK";
            serialPort1.Write("2");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "3. LED'İ YAK")
                button4.Text = "3. LED'İ SÖNDÜR";
            else
                button4.Text = "3. LED'İ YAK";
            serialPort1.Write("3");
        }
    }
}