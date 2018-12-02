using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Threading;
using System.Windows.Forms;

namespace BlueApp.Functions
{
    class BluetoothFunctions
    {
        public static BluetoothClient client = new BluetoothClient();

        public static BluetoothDeviceInfo[] Scan()
        {
            try
            {
                BluetoothRadio.PrimaryRadio.Mode = RadioMode.Connectable;
                BluetoothDeviceInfo[] devices = client.DiscoverDevices(); //Ищем bluetooth устройства
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

        public static void Connection(BluetoothDeviceInfo[] devices, string name, Form1 context)
        {
            var serviceClass = BluetoothService.SerialPort;
            foreach (BluetoothDeviceInfo device in devices)
            {
                if (device.DeviceName.ToString() == name)
                {
                    var tuple = Tuple.Create(new BluetoothEndPoint(device.DeviceAddress, serviceClass), context);

                    try
                    {
                        if (!device.Connected)
                        {
                            Thread ConnectThread = new Thread(new ParameterizedThreadStart(ThreadConnect));
                            ConnectThread.Name = "Поток подключения к Bluetooth устройству";
                            ConnectThread.Start(tuple);
                        }
                        else
                        {
                            MessageBox.Show("Устройство " + name + " уже подключено!");
                            ReadDataFunction(context);
                        }
                    }
                    catch
                    {
                        client.Close();
                        MessageBox.Show("Не удается подключится к устройству " + name + "!");
                    }
                }
            }
        }

        private static void ThreadConnect(object tuple)
        {
            client.Connect((BluetoothEndPoint)((Tuple<BluetoothEndPoint, Form1>)tuple).Item1); //Подключаемся к устройству с именем name
            ReadDataFunction(((Tuple<BluetoothEndPoint, Form1>)tuple).Item2);
        }

        public static void ReadDataFunction(Form1 context)
        {
            ReadData.ReadDataStream((a, e) =>
            {
                if (e.NewItems.Count > 0)
                {
                    context.Messages += e.NewItems[0].ToString();
                }
            });
        }
    }
}
