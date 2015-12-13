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
        private readonly ScreenCapture _screenCapture;

        public int MarginHorizontal
        {
            get;
            set;
        }
        public int MarginVertical
        {
            get;
            set;
        }

        private const int Density = 4;
        private const int NumberOfLedInLine = 7;
        private const int NumberOfLeds = 24;

        private List<Collection<long>> _listOfPositions = new List<Collection<long>>();

        public void CalculatePosition(int marginHorizontal, int marginVertical)
        {
            if (marginHorizontal < 8)
            {
                MarginHorizontal = MarginHorizontal;
            }
            if (MarginVertical < 8)
            {
                MarginVertical = MarginVertical;
            }

            MarginHorizontal = marginHorizontal;
            MarginVertical = marginVertical;

            _listOfPositions = null;
            _listOfPositions = new List<Collection<long>>();

            for (int i = 0; i < NumberOfLeds; i++)
            {
                _listOfPositions.Add(new Collection<long>());
            }

            int margin = 8;

            int widthScreenSize = Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) - MarginHorizontal;
            int _heightScreenSize = Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenHeight) - MarginVertical;

            int[] _collumnPosition = new int[NumberOfLedInLine];
            int[] _rowPosition = new int[NumberOfLedInLine];

            for (int i = 0; i < NumberOfLedInLine; i++)
            {
                _collumnPosition[i] = ((widthScreenSize - MarginHorizontal) * i) / NumberOfLedInLine + MarginHorizontal;
                _rowPosition[i] = ((_heightScreenSize - MarginVertical) * i) / NumberOfLedInLine + MarginVertical;
            }

            long x, y;
            long step = 10;
            long position;

            y = MarginVertical;
            for (x = MarginHorizontal; x < widthScreenSize; x += step)
            {
                position = (y * Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) + x) * Density;

                int z;

                for (z = 0; z < NumberOfLedInLine - 1; z++)
                {
                    if (x >= _collumnPosition[z] && x < _collumnPosition[z + 1])
                    {

                        _listOfPositions[z].Add(position);
                        z = NumberOfLedInLine;

                    }

                }
                if (z == NumberOfLedInLine - 1)
                {
                    _listOfPositions[z].Add(position);
                }
            }

            y = _heightScreenSize;

            for (x = MarginHorizontal; x < widthScreenSize; x += step)
            {
                position = (y * Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) + x) * Density;

                int z;

                for (z = 0; z < NumberOfLedInLine - 1; z++)
                {
                    if (x >= _collumnPosition[z] && x < _collumnPosition[z + 1])
                    {

                        _listOfPositions[18 - z].Add(position);
                        z = NumberOfLedInLine;

                    }

                }
                if (z == NumberOfLedInLine - 1)
                {
                    _listOfPositions[18 - z].Add(position);
                }
            }



            x = MarginHorizontal;
            for (y = MarginVertical + 1; y < _heightScreenSize; y += step)
            {
                position = (y * Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) + x) * Density;

                int z;

                for (z = 0; z < NumberOfLedInLine - 1; z++)
                {
                    if (y >= _rowPosition[z] && y < _rowPosition[z + 1])
                    {
                        if (z == 0)
                        {
                            _listOfPositions[0].Add(position);
                        }
                        else
                        {
                            _listOfPositions[24 - z].Add(position);

                        }
                        z = NumberOfLedInLine;


                    }

                }
                if (z == NumberOfLedInLine - 1)
                {
                    _listOfPositions[24 - z].Add(position);
                }
            }



            x = widthScreenSize;

            for (y = MarginVertical + 1; y < _heightScreenSize; y += step)
            {
                position = (y * Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth) + x) * Density;

                int z;

                for (z = 0; z < NumberOfLedInLine - 1; z++)
                {
                    if (y >= _rowPosition[z] && y < _rowPosition[z + 1])
                    {

                        _listOfPositions[6 + z].Add(position);

                        z = NumberOfLedInLine;


                    }

                }
                if (z == NumberOfLedInLine - 1)
                {
                    _listOfPositions[6 + z].Add(position);
                }
            }
        }

        public ColorCalculate(ScreenCapture screenCapture)
        {
            if (screenCapture == null)
                throw new ArgumentNullException("screenCapture null");
            _screenCapture = screenCapture;


            CalculatePosition(8, 8);



        }

        public List<string> Calculate()
        {

            Surface _surface = _screenCapture.CaptureScreen();
            DataRectangle _dataRectangle = _surface.LockRectangle(LockFlags.None);
            DataStream dataStream = _dataRectangle.Data;

            List<string> listOfColors = new List<string>();

            foreach (Collection<long> collection in _listOfPositions)
            {
                var buff = AvgColor(dataStream, collection).ToString();
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