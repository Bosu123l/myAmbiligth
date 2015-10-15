using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Documents;
using System.Windows.Media;

namespace ProjektInzynierski
{
    public class PortCom
    {
        private SerialPort _serialPort;
        private bool flaga = true;
        public PortCom(string PortName, int BoundRate)
        {
            _serialPort = new SerialPort(PortName, BoundRate);
            _serialPort.DataReceived += _serialPort_DataReceived;
        }

        ~PortCom()
        {
            ClosePort();
        }
      
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            if (_serialPort.ReadLine() == "Oki")
            {
                flaga = true;
            }
        }

        public void OpenPort()
        {
            _serialPort.Open();
        }

        private void ClosePort()
        {
            _serialPort.Close(); ;
        }

        public void SendColors(List<string> listOfColors)
        {
            if (flaga)
            {
                byte[] tablicaBytes = new byte[24 * 3];
                int licznik = 0;
                string ToSend = String.Empty;
                foreach (string color in listOfColors)
                {
                    var test = color.Replace("#", "");
                    Color buff = (Color)ColorConverter.ConvertFromString(color);

                    tablicaBytes[licznik++] = buff.R;
                    tablicaBytes[licznik++] = buff.G;
                    tablicaBytes[licznik++] = buff.B;



                }

                _serialPort.Write(tablicaBytes, 0, tablicaBytes.Length);

                flaga = false;
            }



        }
    }
}
//#FF313134,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF3A3834,#FF303034,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF292B28,#FF1A221F,#FF1C2322,#FF202423,#FF1B2220,#FF1A2724,#FF36332B,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF2D2D30,