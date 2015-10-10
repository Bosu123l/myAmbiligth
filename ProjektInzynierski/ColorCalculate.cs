using System;
using System.Collections.ObjectModel;
using SlimDX;
using System.Drawing;
using SlimDX.Direct3D9;
using System.Collections.Generic;

namespace ProjektInzynierski
{
    public class ColorCalculate
    {
        private ScreenCapture _screenCapture;



        private const int Bpp = 4;
        private const int NumberOfLedInLine = 7;
        private const int NumberOfLeds = 24;

        private List<Collection<long>> ListOfPositions = new List<Collection<long>>();

        public ColorCalculate(ScreenCapture screenCapture)
        {
            if (screenCapture == null)
                throw new ArgumentNullException("screenCapture null");
            _screenCapture = screenCapture;
          



            for (int i = 0; i < NumberOfLeds; i++)
            {
                ListOfPositions.Add(new Collection<long>());
            }

            int margin = 8;

            int _widthScreenSize = Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) - margin;
            int _heightScreenSize = Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenHeight) - margin;

            int[] _collumnPosition = new int[7];
            int[] _rowPosition = new int[7];

            for (int i = 0; i < NumberOfLedInLine; i++)
            {
                _collumnPosition[i] = ((_widthScreenSize - margin) * i) / NumberOfLedInLine + margin;
                _rowPosition[i] = ((_heightScreenSize - margin) * i) / NumberOfLedInLine + margin;
            }

            long x, y;
            long step = 5;
            long position;

            y = margin;
            for (x = margin; x < _widthScreenSize; x += step)
            {
                position = (y * Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) + x) * Bpp;

                int z;

                for (z = 0; z < NumberOfLedInLine - 1; z++)
                {
                    if (x >= _collumnPosition[z] && x < _collumnPosition[z + 1])
                    {

                        ListOfPositions[z].Add(position);
                        z = NumberOfLedInLine;

                    }

                }
                if (z == NumberOfLedInLine - 1)
                {
                    ListOfPositions[z].Add(position);
                }
            }

            y = _heightScreenSize;

            for (x = margin; x < _widthScreenSize; x += step)
            {
                position = (y * Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) + x) * Bpp;

                int z;

                for (z = 0; z < NumberOfLedInLine - 1; z++)
                {
                    if (x >= _collumnPosition[z] && x < _collumnPosition[z + 1])
                    {

                        ListOfPositions[18 - z].Add(position);
                        z = NumberOfLedInLine;

                    }

                }
                if (z == NumberOfLedInLine - 1)
                {
                    ListOfPositions[18 - z].Add(position);
                }
            }



            x = margin;
            for (y = margin + 1; y < _heightScreenSize; y += step)
            {
                position = (y * Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) + x) * Bpp;

                int z;

                for (z = 0; z < NumberOfLedInLine - 1; z++)
                {
                    if (y >= _rowPosition[z] && y < _rowPosition[z + 1])
                    {
                        if (z == 0)
                        {
                            ListOfPositions[0].Add(position);
                        }
                        else
                        {
                            ListOfPositions[24 - z].Add(position);

                        }
                        z = NumberOfLedInLine;


                    }

                }
                if (z == NumberOfLedInLine - 1)
                {
                    ListOfPositions[24 - z].Add(position);
                }
            }



            x = _widthScreenSize;

            for (y = margin + 1; y < _heightScreenSize; y += step)
            {
                position = (y * Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) + x) * Bpp;

                int z;

                for (z = 0; z < NumberOfLedInLine - 1; z++)
                {
                    if (y >= _rowPosition[z] && y < _rowPosition[z + 1])
                    {

                        ListOfPositions[6 + z].Add(position);

                        z = NumberOfLedInLine;


                    }

                }
                if (z == NumberOfLedInLine - 1)
                {
                    ListOfPositions[6 + z].Add(position);
                }
            }

        }

        public List<string> Calculate()
        {

            Surface _surface = _screenCapture.CaptureScreen();
            DataRectangle _dataRectangle = _surface.LockRectangle(LockFlags.None);
            DataStream dataStream = _dataRectangle.Data;

            List<string> listOfColors = new List<string>();

            foreach (Collection<long> collection in ListOfPositions)
            {
                var buff = AvgColor(dataStream,collection).ToString();
                //          string color
                listOfColors.Add(buff);
            }
            _surface.UnlockRectangle();
            _surface.Dispose();
            return listOfColors;
        }
        private System.Windows.Media.Color AvgColor(DataStream dataStream, Collection<long> positions)
        {
      
            byte[] bu = new byte[4];
            int r = 0;
            int g = 0;
            int b = 0;
            int i = 0;

            foreach (long pos in positions)
            {
                dataStream.Position = pos;
                dataStream.Read(bu, 0, 4);
                r += bu[2];
                g += bu[1];
                b += bu[0];
                i++;
                dataStream.Position = 0;
            }
            byte red = (byte)(r / i);
            byte green = (byte)(g / i);
            byte blue = (byte)(b / i);
            return System.Windows.Media.Color.FromArgb(255, red, green, blue);
        }
    }
}