using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class TimeLabel : System.Windows.Forms.Label
    {
        System.Timers.Timer tmr = new System.Timers.Timer();
        public TimeLabel()
        {
            if (!DesignMode)
            {
                tmr.Interval = Interval;
                tmr.Elapsed += Tmr_Elapsed;
                tmr.Start();
            }
        }

        protected int _interval = 1000;
        public int Interval
        {
            get { return _interval; }
            set
            {
                _interval = value;
                if (_interval < 100)
                {
                    _interval = 100;
                }
                else if (_interval > 5000)
                {
                    _interval = 5000;
                }
                tmr.Interval = _interval;
            }
        }

        public string TimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";
        private void Tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.InvokeIfRequired(() => 
            {
                try
                {
                    this.Text = DateTime.Now.ToString(TimeFormat);
                }
                catch 
                {
                    return;
                }
            });
        }
    }
}
