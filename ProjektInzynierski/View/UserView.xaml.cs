﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace ProjektInzynierski.View
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl, INotifyPropertyChanged
    {
        private ScreenCapture _screenCapture;
        private ColorCalculate _colorCalculate;
        private List<string> _colorOfScreen;

        private Thickness _horizontalMargin;

        private PortCom _portCom;
        private string _resolution;
        public string Resolution
        {
            get
            {
                return _resolution;
            }
            set
            {
                if (_resolution != value)
                {
                    _resolution = value;
                    NotifyPropertyChanged("Resolution");
                }
            }
        }
        public Thickness HorizontalMargin
        {
            get
            {
                return _horizontalMargin;
            }
            set
            {
                if (_horizontalMargin != value)
                {
                    _horizontalMargin = value;
                    NotifyPropertyChanged("HorizontalMargin");
                }
            }
        }

        private Thickness _verticalMargin;

        public Thickness VerticalMargin
        {
            get
            {
                return _verticalMargin;
            }
            set
            {
                if (_verticalMargin != value)
                {
                    _verticalMargin = value;
                    NotifyPropertyChanged("VerticalMargin");
                }
            }
        }

        private Thickness _horizontalMarginOposit;

        public Thickness HorizontalMarginOposit
        {
            get
            {
                return _horizontalMarginOposit;
            }
            set
            {
                if (_horizontalMarginOposit != value)
                {
                    _horizontalMarginOposit = value;
                    NotifyPropertyChanged("HorizontalMarginOposit");
                }
            }
        }

        private Thickness _verticalMarginOposit;

        public Thickness VerticalMarginOposit
        {
            get
            {
                return _verticalMarginOposit;
            }
            set
            {
                if (_verticalMarginOposit != value)
                {
                    _verticalMarginOposit = value;
                    NotifyPropertyChanged("VerticalMarginOposit");
                }
            }
        }

        public List<string> ColorOfScreen
        {
            get
            {
                return _colorOfScreen;
            }
            set
            {
                if (_colorOfScreen != value)
                {
                    _colorOfScreen = value;
                    NotifyPropertyChanged("ColorOfScreen");
                }
            }
        }

        private Timer _timer;

       
        private Thickness _marginTop;

        public Thickness MarginTop
        {
            get
            {
                return _marginTop;
            }
            set
            {
                if (_marginTop != value)
                {
                    _marginTop = value;
                    NotifyPropertyChanged("MarginTop");
                }
            }
        }

        private List<string> _portNames;

        public UserView()
        {
            InitializeComponent();
            ColorOfScreen = new List<string>();
            _timer = new Timer(150);//1000 =1s
            _timer.Elapsed += _timer_Elapsed;
            _screenCapture = new ScreenCapture();
            _colorCalculate = new ColorCalculate(_screenCapture);

            _portNames = new List<string>();
            foreach (string srt in SerialPort.GetPortNames())
            {
                _portNames.Add(srt);
            }

            _resolution = System.Windows.SystemParameters.PrimaryScreenWidth.ToString() + " x " + System.Windows.SystemParameters.PrimaryScreenHeight.ToString();
            int index = 0;

                 _portCom = new PortCom("COM5", 115200);
                _portCom.OpenPort();

            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ColorOfScreen = _colorCalculate.Calculate();
           _portCom.SendColors(ColorOfScreen);
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            var tempHandler = PropertyChanged;
            if (tempHandler != null)
            {
                tempHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var top = VerticalMargin.Top;
            var bottom = VerticalMargin.Bottom;
            top += 1;
            bottom -= 1;
            _colorCalculate.CalculatePosition(8, (int)top);
            VerticalMarginOposit = new Thickness(0, (top * -1), 0, (bottom * -1));
            VerticalMargin = new Thickness(0, top, 0, bottom);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var top = VerticalMargin.Top;
            var bottom = VerticalMargin.Bottom;
            top -= 1;
            bottom += 1;

            _colorCalculate.CalculatePosition(8, (int)top);
            VerticalMarginOposit = new Thickness(0, (top * -1), 0, (bottom * -1));
            VerticalMargin = new Thickness(0, top, 0, bottom);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var left = HorizontalMargin.Left;
            var right = HorizontalMargin.Right;
            left += 1;
            right -= 1;
 
            _colorCalculate.CalculatePosition((int)left, 8);
            HorizontalMarginOposit = new Thickness((left * -1), 0, (right * -1), 0);
            HorizontalMargin = new Thickness(left, 0, right, 0);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var left = HorizontalMargin.Left;
            var right = HorizontalMargin.Right;
            
            left -= 1;
            right += 1;
     
            _colorCalculate.CalculatePosition((int)left, 8);
            HorizontalMarginOposit = new Thickness((left * -1), 0, (right * -1), 0);
            HorizontalMargin = new Thickness(left, 0, right, 0);
        }
    }
}