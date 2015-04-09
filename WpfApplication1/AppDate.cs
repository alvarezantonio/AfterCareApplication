using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Globalization;

namespace AfterCareApplication
{
    public partial class AppDate : Timer
    {
        private static string[] hourArray = {"12","1","2","3","4","5","6","7","8","9","10","11",
                                            "12","1","2","3","4","5","6","7","8","9","10","11"};
        string DOW = System.DateTime.Now.DayOfWeek.ToString();
        string MONTH = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        string DAY = System.DateTime.Now.Day.ToString();
        string YEAR = System.DateTime.Now.Year.ToString();
        string TIME = System.DateTime.Now.TimeOfDay.ToString();
        string AMPM = "AM";

        public AppDate()
        {
            Interval = 1000;
        }

        public void updateTime(){
            int HOUR = System.DateTime.Now.Hour;
            string MINUTE = System.DateTime.Now.Minute.ToString("00");
            string SECOND = System.DateTime.Now.Second.ToString("00");
            if (HOUR >= 12) { AMPM = "PM"; } else { AMPM = "AM"; }
            TIME = string.Format("{0}:{1}:{2} {3}", hourArray[HOUR], MINUTE, SECOND, AMPM);
        }

        public string getDateString()
        {
            string date = DOW + "   " + MONTH + " " + DAY + ", " + YEAR;
            return date;
        }

        public string getTimeString()
        {
            updateTime();
            return TIME;
        }

    }
}
