using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace H16O1
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
                    serialPort1.PortName = comboBox1.Text;
                    if (!serialPort1.IsOpen) serialPort1.Open();
                    button1.Text = "BAĞLANTIYI KAPAT";
                    button2.Enabled = true;
                }
                else
                {
                    if (serialPort1.IsOpen) serialPort1.Close();
                    button1.Text = "BAĞLANTIYI AÇ";
                    button2.Enabled = false;
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                serialPort1.Write(textBox1.Text);
            else
                MessageBox.Show("MESAJ GİRMEDİNİZ!");
        }
    }
}