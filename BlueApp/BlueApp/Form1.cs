using System;
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
            set => textBox1.Text = value;
        }

        BluetoothDeviceInfo[] devices;

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            button1.Enabled = false;
            comboBox1.Items.Clear();

            devices = BluetoothFunctions.Scan(); //Получение имен всех найденных девайсов

            String deviceName;

            if (devices != null)
            {
                foreach (BluetoothDeviceInfo device in devices)
                {
                    deviceName = device.DeviceName.ToString();
                    comboBox1.Items.Add(deviceName);
                }
                if (comboBox1.Items.Count != 0)
                {
                    comboBox1.Enabled = true;
                    comboBox1.Text = "Выберите устройство:";
                }
            }
            button1.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BluetoothFunctions.Connection(devices, comboBox1.SelectedItem.ToString(), this); //Подключение к выбранному девайсу
            comboBox1.Enabled = false;
        }

    }
}
