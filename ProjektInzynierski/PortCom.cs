using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO.Ports;
using System.Timers;
using System.Windows.Documents;
using System.Windows.Media;
using System.Diagnostics;
namespace ProjektInzynierski
{
    public class PortCom
    {
        private readonly SerialPort _serialPort;
        private bool _flaga = true;
        private int _duration = 0;
        public PortCom(string portName, int boundRate)
        {
            _serialPort = new SerialPort(portName, boundRate);
            _serialPort.DataReceived += _serialPort_DataReceived;

        }

        ~PortCom()
        {
           
            ClosePort();
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var test = _serialPort.ReadLine();
            if (test == "Oki")
            {
                _flaga = true;
            }
        }

        public void OpenPort()
        {
            _serialPort.Open();
        }

        public void ClosePort()
        {
            _serialPort.Close();
            
        }

        public void SendColors(List<string> listOfColors)
        {
            if (_flaga)
            {
                byte[] arrayOfBytes = new byte[24*3];
              
                int couter = 0;
                string ToSend = String.Empty;
                foreach (string color in listOfColors)
                {

                    Color buff = (Color) ColorConverter.ConvertFromString(color);

                    arrayOfBytes[couter++] = buff.R;
                    arrayOfBytes[couter++] = buff.G;
                    arrayOfBytes[couter++] = buff.B;



                }

                _serialPort.Write(arrayOfBytes, 0, arrayOfBytes.Length);

                _flaga = false;
            }
            else
            {
                _duration++;
                if (_duration > 200)
                {
                    _flaga = true;
                    Debug.WriteLine("stracił połaczenie");
                }
                    
            }
        }
    }
}
