using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text;
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
                Thread ReadThread = new Thread(ThreadRead); //Создаем поток для чтения и записи данных
                ReadThread.Name = "Поток получения данных";
                ReadThread.Start();

                collection.CollectionChanged += handler;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static ObservableCollection<string> collection = new ObservableCollection<string>();

        private static void ThreadRead()
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
                        string str = System.Text.Encoding.UTF8.GetString(buf, 0, buf.Length);//Перевод байтов в строку
                        collection.Add(str.ToString().Trim()); //Записываем в список значения
                    }
                    while (true);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
