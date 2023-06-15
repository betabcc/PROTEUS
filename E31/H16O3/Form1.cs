using System;
using System.Data;
using System.Data.OleDb;
using System.IO.Ports;
using System.Windows.Forms;

namespace H16O3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection bag = new OleDbConnection("provider=Microsoft.Jet.OleDb.4.0;data source=H16O3.mdb");
        void listele()
        {
            try
            {
                OleDbCommand komut = new OleDbCommand("SELECT TARİH, RIGHT(SAAT,8) AS SAAT, SICAKLIK FROM KAYIT ORDER BY TARİH, SAAT DESC", bag);
                OleDbDataAdapter adp = new OleDbDataAdapter(komut);
                DataTable tablo = new DataTable();
                adp.Fill(tablo);
                dataGridView1.DataSource = tablo;
            }
            catch (Exception hata)
            {
                if (bag.State == ConnectionState.Open) bag.Close();
                MessageBox.Show(hata.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            string[] portlar = SerialPort.GetPortNames();
            foreach (string portAdi in portlar)
            {
                comboBox1.Items.Add(portAdi);
            }
            timer1.Interval = 1000; timer1.Stop();
            timer2.Interval = 5000; timer2.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "BAĞLANTIYI AÇ")
                {
                    serialPort1.PortName = comboBox1.Text;
                    button1.Text = "BAĞLANTIYI KAPAT";
                    if (!serialPort1.IsOpen) serialPort1.Open();
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
        string s;
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            s = serialPort1.ReadLine();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            s = s.Substring(0, s.Length - 1);
            label2.Text = "SICAKLIK: " + s + " ᵒC";
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
                button3.Text = "KAYDETMEYE BAŞLA";
                if (serialPort1.IsOpen)
                {
                    timer1.Stop();
                    timer2.Stop();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "KAYDETMEYE BAŞLA")
            {
                button3.Text = "KAYDETMEYİ DURDUR";
                if (serialPort1.IsOpen) timer2.Start();
            }
            else
            {
                button3.Text = "KAYDETMEYE BAŞLA";
                if (serialPort1.IsOpen) { timer2.Stop(); }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime zaman = DateTime.Now;
                string sorgu = "INSERT INTO KAYIT VALUES (@t,@s," + s + ")";
                OleDbCommand komut = new OleDbCommand(sorgu, bag);
                komut.Parameters.AddWithValue("@t", zaman.ToShortDateString());
                komut.Parameters.AddWithValue("@s", zaman.ToLongTimeString());
                bag.Open(); komut.ExecuteNonQuery(); bag.Close();
                listele();
            }
            catch (Exception hata)
            {
                if (bag.State == ConnectionState.Open) bag.Close();
                MessageBox.Show(hata.Message);
            }
        }
    }
}