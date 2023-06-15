using System;
using System.Windows.Forms;

namespace H17O1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "BAĞLANTIYI AÇ")
            {
                if (!serialPort1.IsOpen) serialPort1.Open();
                timer1.Start();
                button1.Text = "BAĞLANTIYI KAPAT";
                button2.Enabled = true;
            }
            else
            {
                if (serialPort1.IsOpen) serialPort1.Close();
                timer1.Stop();
                button1.Text = "BAĞLANTIYI AÇ";
                button2.Enabled = false;
            }

        }
        string m;
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            m = serialPort1.ReadLine();
            m = m.Substring(0, m.Length - 1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "MESAFE: " + m + " cm";
            if (Convert.ToInt32(m) < 50)
            {
                button2.Enabled = false;
                button2.Text = "MOTORU ÇALIŞTIR";
            }
            else
                button2.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "MOTORU ÇALIŞTIR")
            {

                serialPort1.Write("motorcalistir");
                button2.Text = "MOTORU DURDUR";
            }
            else
            {
                serialPort1.Write("motordur");
                button2.Text = "MOTORU ÇALIŞTIR";
            }
        }
    }
}