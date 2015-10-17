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

<<<<<<< HEAD
<<<<<<< HEAD
        ~PortCom()
        {
            ClosePort();
        }
<<<<<<< HEAD
      
=======
>>>>>>> parent of 7461568... działajacy program i kom,unikjujacy sie
=======

>>>>>>> parent of ab6cf44... Wizuzalizacjja
=======
>>>>>>> parent of 7461568... działajacy program i kom,unikjujacy sie
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var test = _serialPort.ReadLine();
            if (test == "Oki")
            {
                flaga = true;
            }
        }

        public void OpenPort()
        {
            _serialPort.Open();
        }

        public void ClosePort()
        {
            _serialPort.Close(); ;
        }
        public void SendColors(List<string> listOfColors)
        {
            if (flaga)
            {
                string ToSend = String.Empty;
                foreach (string color in listOfColors)
                {
                    //    var test = color.Replace("#", "");
                    Color buff = (Color)ColorConverter.ConvertFromString(color);
                    ToSend += buff.R + "," + buff.G + "," + buff.B + "\n";

                }
                //int i = 0;
                //var test = ToSend.Replace("#", "");
                // System.Drawing.Color.FromArgb()

                _serialPort.WriteLine(ToSend);

                flaga = false;
            }



        }
    }
}
//#FF313134,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF3A3834,#FF303034,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF292B28,#FF1A221F,#FF1C2322,#FF202423,#FF1B2220,#FF1A2724,#FF36332B,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF2D2D30,#FF2D2D30,