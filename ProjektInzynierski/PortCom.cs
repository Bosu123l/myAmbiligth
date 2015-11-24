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
        private bool _flag = true;
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
                _flag = true;
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
            if (_flag)
            {
                byte[] arrayOfBytes = new byte[(24*3)];
              
                int couter = 0;
        
                foreach (string color in listOfColors)
                {
                    var convertFromString = ColorConverter.ConvertFromString(color);
                    if (convertFromString != null)
                    {
                        Color buff = (Color) convertFromString;

                        arrayOfBytes[couter++] = buff.R;
                        arrayOfBytes[couter++] = buff.G;
                        arrayOfBytes[couter++] = buff.B;
                    }
                }

                //    _serialPort.Write(arrayOfBytes, 0, arrayOfBytes.Length);

                _flag = false;
            }
            else
            {
                _duration++;
                if (_duration > 20)
                {
                    _flag = true;
                    Debug.WriteLine("stracił połaczenie");
                }
                    
            }
        }
    }
}
