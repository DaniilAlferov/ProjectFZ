using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace BlueApp.Functions
{
    class Bt
    {
        public static BluetoothClient client = new BluetoothClient();

        public static BluetoothDeviceInfo[] Scan()
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

        public static bool Connection(BluetoothDeviceInfo[] devices, string name)
        {
            bool con = false;
            try
            {
                var serviceClass = BluetoothService.SerialPort;
                foreach (BluetoothDeviceInfo device in devices)
                {
                    if (device.DeviceName.ToString() == name)
                    {
                        if (!device.Connected)
                        {
                            var ep = new BluetoothEndPoint(device.DeviceAddress, serviceClass);
                            client.Connect(ep);
                            MessageBox.Show("Устройство " + name + " подключено!");
                            con = true;
                            return con;
                        }
                        else
                        {
                            MessageBox.Show("Устройство " + name + " уже подключено!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удается подключится к устройству " + name + "!");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к устройству Bluetooth!");
            }
            return con;
        }

        public static string ReaderBT()
        {
            //Тут как то надо слушать то что передается, чтобы понять подключено ли все таки устройство

            NetworkStream stream = client.GetStream();

            if (stream.CanRead)
            {
                byte[] myReadBuffer = new byte[1024];
                StringBuilder myCompleteMessage = new StringBuilder();
                int numberOfBytesRead = 0;
                
                do
                {
                    numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);

                    myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                    return myCompleteMessage.ToString();
                }
                while (stream.DataAvailable);
            }
            return null;
        }
    }
}
