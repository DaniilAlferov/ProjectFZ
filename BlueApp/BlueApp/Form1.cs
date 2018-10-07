using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using System.IO;

namespace BlueApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            scan();
        }

        private void scan()
        {
            comboBox1.Enabled = false;
            button1.Enabled = false;
            comboBox1.Items.Clear();
            try
            {
                BluetoothRadio.PrimaryRadio.Mode = RadioMode.Connectable;
                BluetoothClient client = new BluetoothClient();
                BluetoothDeviceInfo[] devices = client.DiscoverDevices();
                BluetoothClient bluetoothClient = new BluetoothClient();

                String deviceName;

                foreach (BluetoothDeviceInfo device in devices)
                {
                    deviceName = device.DeviceName.ToString();
                    comboBox1.Items.Add(deviceName);
                }
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.Text = "Выберите устройство:";
                }
                else
                {
                    comboBox1.Text = "Устройств не найдено!";
                }
            }
            catch
            {
                //Ошибка. У ПК проблем с Bluetooth
            }
            comboBox1.Enabled = true;
            button1.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Тут будет подключение к выбранному устройству из комбобокса
            }
            catch
            {
                //Ошибка подключения к устройству.
            }
        }
    }
}
