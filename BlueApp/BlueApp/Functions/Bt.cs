using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueApp.Functions
{
    class Bt
    {
        public static BluetoothClient client = new BluetoothClient();

        public static BluetoothDeviceInfo[] scan()
        {
            BluetoothRadio.PrimaryRadio.Mode = RadioMode.Connectable;
            BluetoothDeviceInfo[] devices = client.DiscoverDevices();
            BluetoothClient bluetoothClient = new BluetoothClient();

            try
            {
                if (devices.Length != 0)
                {
                    return (devices);
                }
                else
                {
                    MessageBox.Show("Устройства Bluetooth не найдены!");
                    return null;
                }
            }
            catch
            {
                MessageBox.Show("Проверьте подключение Bluetooth на ПК!");
                return null;
            }
        }

        public static void Connection(BluetoothDeviceInfo[] devices, string name)
        {
            try
            {
                foreach (BluetoothDeviceInfo device in devices)
                {
                    if (device.DeviceName.ToString() == name)
                    {
                        //Хз как подключить(
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к устройству Bluetooth!");
            }
        }
    }
}
