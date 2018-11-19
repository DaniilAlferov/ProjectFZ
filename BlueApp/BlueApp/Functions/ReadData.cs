using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace BlueApp.Functions
{
    class ReadData
    {
        public static void ReadDataStream(NotifyCollectionChangedEventHandler handler)
        {
            try
            {
                Thread ReadThread = new Thread(ThreadRead()); //Создаем поток для чтения и записи данных
                ReadThread.Start();

                collection.CollectionChanged += handler;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static ObservableCollection<double> collection = new ObservableCollection<double>();

        private static ThreadStart ThreadRead()
        {
            Stream getStream = BluetoothFunctions.client.GetStream();
            try
            {
                byte[] buf = new byte[1000];
                int readLen = getStream.Read(buf, 0, buf.Length); //Читаем стрим
                if (readLen != 0)
                {
                    do
                    {
                        Array.Clear(buf, 0, buf.Length); //Чистим буфер
                        getStream.Read(buf, 0, buf.Length); //Читаем стрим
                        collection.Add(Convert.ToDouble(buf.ToString().Trim())); //Записываем в список значения
                    }
                    while (true);
                }
                else
                {
                    MessageBox.Show("Соединение закрыто!");
                    return null;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
    }
}
