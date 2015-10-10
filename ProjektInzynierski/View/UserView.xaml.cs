using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        public string resolution
        {
            get;
            set;
        }
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



        public UserView()
        {
            ColorOfScreen = new List<string>();
            _timer = new Timer(100);//1000 =1s
            _timer.Elapsed += _timer_Elapsed;
            _screenCapture = new ScreenCapture();
            _colorCalculate = new ColorCalculate(_screenCapture);

            resolution = System.Windows.SystemParameters.PrimaryScreenWidth.ToString() + " x " + System.Windows.SystemParameters.PrimaryScreenHeight.ToString();


            InitializeComponent();
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ColorOfScreen = _colorCalculate.Calculate();
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
    }
}
