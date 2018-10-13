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
    class ScanBt
    {
        public static BluetoothDeviceInfo[] scan()
        {
            try
            {
                BluetoothRadio.PrimaryRadio.Mode = RadioMode.Connectable;
                BluetoothClient client = new BluetoothClient();
                BluetoothDeviceInfo[] devices = client.DiscoverDevices();
                BluetoothClient bluetoothClient = new BluetoothClient();

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
    }
}
