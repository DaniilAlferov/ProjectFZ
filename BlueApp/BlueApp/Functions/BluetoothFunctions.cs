using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Windows.Forms;

namespace BlueApp.Functions
{
    class BluetoothFunctions
    {
        public static BluetoothClient client = new BluetoothClient();

        public static BluetoothDeviceInfo[] Scan()
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
            /*
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
            }*/
        }

        public static void Connection(BluetoothDeviceInfo[] devices, string name, Form1 context)
        {
            var serviceClass = BluetoothService.SerialPort;
            foreach (BluetoothDeviceInfo device in devices)
            {
                if (device.DeviceName.ToString() == name)
                {
                    var ep = new BluetoothEndPoint(device.DeviceAddress, serviceClass);
                    if (!device.Connected)
                    {
                        client.Connect(ep); //Подключаемся к устройству с именем name
                        MessageBox.Show("Устройство " + name + " подключено!");
                    }
                    else
                    {
                        MessageBox.Show("Устройство " + name + " уже подключено!");
                    }

                    ReadData.ReadDataStream((a, e) =>
                    {
                        if (e.NewItems.Count > 0)
                        {
                            context.Messages += e.NewItems[0].ToString();
                        }
                    });
                    /*
                    try
                    {
                        if (!device.Connected)
                        {
                            client.Connect(ep); //Подключаемся к устройству с именем name
                            MessageBox.Show("Устройство " + name + " подключено!");
                        }
                        else
                        {
                            MessageBox.Show("Устройство " + name + " уже подключено!");
                        }
                        
                        ReadData.ReadDataStream((a, e) =>
                        {
                            if (e.NewItems.Count > 0)
                            {
                                context.Messages += e.NewItems[0].ToString();
                            }
                        });
                        
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        client.Close();
                        //MessageBox.Show("Не удается подключится к устройству " + name + "!");
                    }*/
                }
            }
        }
    }
}
