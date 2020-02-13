using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using MiningConsole.Model;

namespace MiningConsole
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RequestSite site = new RequestSite();
        RequestMrr req = new RequestMrr();
        RequestMinear reqm = new RequestMinear();
        RequestSp sp = new RequestSp();
        //RequestBtc btc = new RequestBtc();
        RequestRate rate = new RequestRate();
        bool up = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if((int.Parse(DateTime.Now.Minute.ToString().Substring(DateTime.Now.Minute.ToString().Length - 1, 1)) == 0 ) && up || (int.Parse(DateTime.Now.Minute.ToString().Substring(DateTime.Now.Minute.ToString().Length - 1, 1)) == 5 && up))
                {
                up = false;
                site.req();
                req.Upload();
                sp.Upload();
                //btc.Upload();
                rate.Upload();
                reqm.Upload();
                increment = 0;
                Console.WriteLine("update: " + DateTime.Now.ToString() );
            } else if (!up && int.Parse(DateTime.Now.Minute.ToString().Substring(DateTime.Now.Minute.ToString().Length - 1, 1)) != 0 && int.Parse(DateTime.Now.Minute.ToString().Substring(DateTime.Now.Minute.ToString().Length - 1, 1)) != 5)
            {
                up = true;
            }

        }
        private void TimerShow(object sender, RoutedEventArgs e)
        {
            DispatcherTimer show = new DispatcherTimer();
            show.Interval = TimeSpan.FromSeconds(1);
            show.Tick += showTicker;
            show.Start();
            Update();
        }

        private void Update()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private int increment = 0;
        private void showTicker(object sender, EventArgs e)
        {
            var ts = TimeSpan.FromSeconds(increment);
            increment++;
            TimerLabel.Content = ts.Minutes.ToString() + ":" + ts.Seconds.ToString();
        }
    }
}
