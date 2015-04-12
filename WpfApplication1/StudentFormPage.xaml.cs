using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace AfterCareApplication
{
    /// <summary>
    /// Interaction logic for StudentFormPage.xaml
    /// </summary>
    public partial class StudentFormPage : Page
    {
        private int startingYear = 2000;
        private int dobYear = 0;
        private int dobMonth = 0;
        private int dobDay = 0;
        AfterCareDataContext db;

        public StudentFormPage()
        {
            InitializeComponent();
            setYearItems();
        }

        public StudentFormPage(AfterCareDataContext db)
        {
            this.db = db;
            InitializeComponent();
            setYearItems();
        }

        private void setYearItems()
        {
            IEnumerable<int> years = Enumerable.Range(startingYear,DateTime.Now.Year-(startingYear-1)).Select(year => year);
            foreach (int year in years)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = year;
                yearCombo.Items.Add(temp);
            }
        }

        private void setMonthItems()
        {
            monthCombo.Items.Clear();
            IEnumerable<int> months = Enumerable.Range(1, 12).Select(mon => mon);
            foreach (int mon in months)
            {
                ComboBoxItem tmp = new ComboBoxItem();
                tmp.Content = mon;
                monthCombo.Items.Add(tmp);
            }
            monthCombo.SelectedIndex = dobMonth;
            monthCombo.Visibility = Visibility.Visible;
            monthLabel.Visibility = Visibility.Visible;
        }

        private void setDayItems()
        {
            daysCombo.Items.Clear();
            int indx = 0;
            IEnumerable<int> days = Enumerable.Range(1, DateTime.DaysInMonth(dobYear, dobMonth)).Select(day => day);
            foreach (int day in days)
            {
                ComboBoxItem tmp = new ComboBoxItem();
                tmp.Content = day;
                daysCombo.Items.Add(tmp);
            }
            if (dobDay > daysCombo.Items.Count)
            {
                dobDay = daysCombo.Items.Count;
                indx = dobDay - 1;
            }
            else if(dobDay == 0)
            {
                indx = dobDay;
            }
            else {
                indx = dobDay-1;
            }
            daysCombo.SelectedIndex = indx;
            daysCombo.Visibility = Visibility.Visible;
            dayLabel.Visibility = Visibility.Visible;
            setDOBLabel();
        }

        private void yearCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem tmp = (ComboBoxItem)yearCombo.SelectedItem;
            dobYear = int.Parse(tmp.Content.ToString());
            setMonthItems();
        }

        private void monthCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!monthCombo.Items.IsEmpty)
            {
                ComboBoxItem tmp = (ComboBoxItem)monthCombo.SelectedItem;
                dobMonth = int.Parse(tmp.Content.ToString());
                setDayItems();
            }
        }

        private void daysCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!daysCombo.Items.IsEmpty)
            {
                ComboBoxItem tmp = (ComboBoxItem)daysCombo.SelectedItem;
                dobDay = int.Parse(tmp.Content.ToString());
                setDOBLabel();
            }
        }

        private void setDOBLabel()
        {
            birthdayOutput.Content = string.Format("{0} {1}, {2}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dobMonth), dobDay, dobYear);
        }
    }
}
