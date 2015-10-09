using System;
using System.Collections.ObjectModel;
using SlimDX;
using System.Drawing;
using SlimDX.Direct3D9;

namespace ProjektInzynierski
{
    public class ColorCalculate
    {
        private ScreenCapture _screenCapture;
        private readonly Surface _surface;
        private DataRectangle _dataRectangle;
        private DataStream _dataStream;

        public ColorCalculate(ScreenCapture screenCapture)
        {
            if(screenCapture==null)
                throw new ArgumentNullException("screenCapture null");
            _screenCapture = screenCapture;
            _surface = _screenCapture.CaptureScreen();
            _dataRectangle = _surface.LockRectangle(LockFlags.None);
            _dataStream = _dataRectangle.Data;

        }

        public void Calculate()
        {
            //TODO : dodac liste colekcji  w argumencie  i zwrocic ja po przeliczeniu wartosci 

            _surface.UnlockRectangle();
            _surface.Dispose();
        }
        private Color AvgColor(DataStream dataStream, Collection<long> positions)
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
            }

            return Color.FromArgb(r / i, g / i, b / i);
        }
    }
}