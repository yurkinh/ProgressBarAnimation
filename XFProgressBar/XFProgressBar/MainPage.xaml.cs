using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Timers;

namespace XFProgressBar
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private Timer _timer;
        private Random RAND = new Random();

        int startValue;        
        uint interval = 2000;
        public MainPage()
        {
            InitializeComponent();

            _timer = new Timer()
            {
                Interval = interval
            };
            //Trigger event every second      
            _timer.Elapsed += OnTimedEvent;

            _timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
           {
               int progress = RAND.Next(100);
               AnimateProgress1(progress);
               AnimateProgress2(progress);
               startValue = progress;
           });
        }

        void AnimateProgress1(int progress)
        {
            var animation = new Animation(v => VGaugeControl.Progress = (int)v, startValue, progress, easing: Easing.SinInOut);

            animation.Commit(VGaugeControl, "Progress", length: interval, finished: (l, c) => animation = null);
        }

        void AnimateProgress2(int progress)
        {
            var animation = new Animation(v => GaugeControl.Value = (int)v, startValue, progress, easing: Easing.SinInOut);

            animation.Commit(GaugeControl, "Value", length: interval, finished: (l, c) => animation = null);
        }

    }
}
