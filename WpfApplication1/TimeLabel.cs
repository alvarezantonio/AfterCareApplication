using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Timers;
using System.Windows;

namespace AfterCareApplication
{
    public class TimeLabel : Label
    {
        public AppDate timer = new AppDate();

        public TimeLabel()
        {
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            timer.Disposed += timer_Disposed;
            updateContent();
        }

        void timer_Disposed(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void updateContent()
        {
            timer.updateTime();
            string apptime = (string)timer.getTimeString();
            try
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    this.Content = apptime;
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            updateContent();
        }
    }
}
