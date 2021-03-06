﻿using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO.Ports;
using System.Timers;
using System.Windows.Documents;
using System.Windows.Media;
using System.Diagnostics;
using System.Linq;

namespace ProjektInzynierski
{
    public class PortCom
    {
        private readonly SerialPort _serialPort;
        private bool _flag = true;
        private int _checkSum = 0;

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
            var test = _serialPort.ReadChar();
            if (test == _checkSum)
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

        private int generateCheckSum(byte[] arrayOfBytes)
        {
            var sum = 0;
            var checkSum = 0;
            var length = arrayOfBytes.Length;
            var flaga = true;
            for (var i = length - 2; i >= 0; i--)
            {
                int temp = arrayOfBytes[i];
                if (flaga)
                {
                    temp *= 2;
                    if (temp > 9)
                    {
                        temp -= 9;
                    }
                }
                sum += temp;
                flaga = !flaga;
            }
            var modulo = sum % 10;
            if (modulo > 0)
            {
                modulo = 10 - modulo;
            }
            checkSum = modulo;
            return checkSum;
        }

        public byte[] MirrorReflection(byte[] color)
        {




            byte[] tmpTab = color;
            byte[] newTab = new byte[color.Length];



            return newTab;
        }

        private List<string> reverteColor(List<string> color)
        {

            int[] index = new int[] { 6, 5, 4, 3, 2, 1, 0, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7 };


            string[] newColors = new string[color.Count];

            List<string> odColors = color;

            for (int i = 0; i < color.Count; i++)
            {
                newColors[i] = color[index[i]];
            }

            return newColors.ToList();
        }


        public void SendColors(List<string> listOfColors)
        {
            listOfColors = reverteColor(listOfColors);
            // listOfColors.Reverse();
            if (_flag)
            {
                byte[] arrayOfBytes = new byte[(24 * 3)];

                int couter = 0;

                foreach (string color in listOfColors)
                {
                    var convertFromString = ColorConverter.ConvertFromString(color);
                    if (convertFromString != null)
                    {
                        Color buff = (Color)convertFromString;

                        arrayOfBytes[couter++] = buff.R;
                        arrayOfBytes[couter++] = buff.G;
                        arrayOfBytes[couter++] = buff.B;
                    }
                }
                _checkSum = generateCheckSum(arrayOfBytes);

                _serialPort.Write(arrayOfBytes, 0, arrayOfBytes.Length);

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
