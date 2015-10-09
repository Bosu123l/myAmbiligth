using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class UserView : UserControl
    {
        private ScreenCapture _screenCapture;
        private ColorCalculate _colorCalculate;
        private Timer _timer;
        public string Cos
        {
            get;
            set;
        }

    

        public UserView()
        {
            _timer=new Timer(100);//1000 =1s
            _timer.Elapsed += _timer_Elapsed;
            Cos = "Janusz";
            _screenCapture = new ScreenCapture();
            _colorCalculate=new ColorCalculate(_screenCapture);
            InitializeComponent();
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           //TODO:  obliczenie z klasy ColorCalculate
        }
    }
}
