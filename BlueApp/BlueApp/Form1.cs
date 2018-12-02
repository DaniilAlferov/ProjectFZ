using System;
using System.Threading;
using System.Windows.Forms;
using BlueApp.Functions;
using InTheHand.Net.Sockets;

namespace BlueApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string Messages
        {
            get => textBox1.Text;
            set => textBox1.Invoke((ThreadStart)delegate ()
            {
                textBox1.Text = value;
            });
        }

        BluetoothDeviceInfo[] devices;

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            button1.Enabled = false;
            comboBox1.Items.Clear();

            //Надо сделать, если бт не вкл, то сособщение или вкл его
            Thread ScanThread = new Thread(ThreadScan);//Создание отдельного потока для сканирования сети Bluetooth
            ScanThread.Name = "Поток сканирования сети Bluetooth";
            ScanThread.Start();
        }

        private void ThreadScan()
        {
            devices = BluetoothFunctions.Scan(); //Получение имен всех найденных девайсов

            String deviceName;

            if (devices != null)
            {
                foreach (BluetoothDeviceInfo device in devices)
                {
                    deviceName = device.DeviceName.ToString();
                    comboBox1.Invoke((ThreadStart)delegate ()
                    {
                        comboBox1.Items.Add(deviceName);
                    });
                }
                if (comboBox1.Items.Count != 0)
                {
                    comboBox1.Invoke((ThreadStart)delegate ()
                    {
                        comboBox1.Enabled = true;
                        comboBox1.Text = "Выберите устройство:";
                    });
                }
            }
            button1.Invoke((ThreadStart)delegate ()
            {
                button1.Enabled = true;
            });
            button1.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BluetoothFunctions.Connection(devices, comboBox1.SelectedItem.ToString(), this); //Подключение к выбранному девайсу
            comboBox1.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}
