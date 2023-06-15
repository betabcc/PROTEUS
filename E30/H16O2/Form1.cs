using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace H16O2
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
            foreach (string portAdi in portlar)
            {
                comboBox1.Items.Add(portAdi);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "BAĞLANTIYI AÇ")
                {
                    if (!serialPort1.IsOpen) serialPort1.Open();
                    button1.Text = "BAĞLANTIYI KAPAT";
                }
                else
                {
                    button1.Text = "BAĞLANTIYI AÇ";
                    if (serialPort1.IsOpen) serialPort1.Close();
                    button2.Text = "OKUMAYA BAŞLA"; timer1.Stop();

                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "OKUMAYA BAŞLA")
            {
                button2.Text = "OKUMAYI DURDUR";
                if (serialPort1.IsOpen) timer1.Start();
            }
            else
            {
                button2.Text = "OKUMAYA BAŞLA";
                if (serialPort1.IsOpen) timer1.Stop();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        string deger;
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            deger = serialPort1.ReadLine();
            deger = deger.Substring(0, deger.Length - 1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "SICAKLIK: " + deger + " ᵒC";
        }
    }
}