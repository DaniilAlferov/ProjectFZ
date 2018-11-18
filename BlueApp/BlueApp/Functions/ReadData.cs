using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace BlueApp.Functions
{
    class ReadData
    {
        public static void ReadDataStream()
        {
            Stream getStream = BluetoothFunctions.client.GetStream();
            try
            {
                byte[] buf = new byte[1000];
                int readLen = getStream.Read(buf, 0, buf.Length); //Читаем стрим
                if (readLen == 0)
                {
                    MessageBox.Show("Соединение закрыто!");
                }
                else
                {
                    Thread ReadThread = new Thread(ThreadRead(buf, getStream)); //Создаем поток для чтения и записи данных
                    ReadThread.Start();
                }
            }
            catch
            {
                getStream.Close();
                MessageBox.Show("Стрим упал!");
            }
        }

        public static List<double> listData = new List<double>();

        //public event EventHandler OnChange; //Событие OnChange

        private static ThreadStart ThreadRead(byte[] buf, Stream getStream)
        {
            try
            {
                do
                {
                    Array.Clear(buf, 0, buf.Length); //Чистим буфер
                    getStream.Read(buf, 0, buf.Length); //Читаем стрим
                    listData.Add(Convert.ToDouble(buf.ToString().Trim())); //Записываем в список значения
                    //MessageBox.Show("Получили данные!!!!!");

                    //OnChange(this, new EventArgs()); //Вызываем событие OnChange
                }
                while (true);
            }
            catch
            {
                MessageBox.Show("Поток упал!");
                return null;
            }
        }
    }
}
