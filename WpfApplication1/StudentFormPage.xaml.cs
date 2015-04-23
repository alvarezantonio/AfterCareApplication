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
        public int guardianID { get; set; }
        public string relationship { get; set; }
        public double studentFee { get; set; }
        public string feeType { get; set; }
        public string homeroom { get; set; }
        public string birthday { get; set; }
        private int startingYear = 2000;
        private int dobYear = 0;
        private int dobMonth = 0;
        private int dobDay = 0;
        public string userID { get; set; }
        AfterCareDataContext db;

        public StudentFormPage(AfterCareDataContext db, string UserID)
        {
            this.DataContext = this;
            InitializeComponent();
            this.userID = UserID;
            this.guardianID = -1;
            this.relationship = null;
            this.studentFee = 0;
            this.feeType = null;
            this.homeroom = null;
            this.birthday = null;
            this.db = db;
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
            birthday = string.Format("{0}-{1}-{2}",dobYear,dobMonth,dobDay);
            birthdayOutput.Content = string.Format("{0} {1}, {2}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dobMonth), dobDay, dobYear);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            setYearItems();
            ContextHelper cH = new ContextHelper();
            ListCollectionView view = cH.getUsersByType("Guardian");
            if (view != null) { guardianCombo.ItemsSource = view; }
        }

        private void guardianCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox gCombo = (ComboBox)sender;
            GuardianUser gItem = (GuardianUser)gCombo.SelectedItem;
            if (gItem != null)
            {
                this.guardianID = gItem.GuardianID;
            }
        }

        private void relationshipCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)combo.SelectedItem;
            string val = string.Format("{0}", item.Content);
            this.relationship = val;
        }

        private void feeTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)combo.SelectedItem;
            string val = string.Format("{0}", item.Content);
            this.feeType = val;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = false;
            Window.GetWindow(this).Close();
        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            if
            (
                this.guardianID >= 0 &&
                this.relationship != null &&
                this.studentFee > 0 &&
                this.feeType != null &&
                this.homeroom != null &&
                this.birthday != null
            )
            {
                int studID = db.Students.Count();
                Student newStudent = new Student { birthday = this.birthday, homeroom = this.homeroom, userId = this.userID};
                db.Students.InsertOnSubmit(newStudent);
                db.SubmitChanges();
                db.ExecuteCommand("INSERT INTO Student_Fee VALUES ({0},{1},{2})", newStudent.studentId, this.studentFee, this.feeType);
                db.ExecuteCommand("INSERT INTO Student_Guardian VALUES ({0},{1},{2})", this.guardianID, newStudent.studentId, this.relationship);
                db.SubmitChanges();
                MessageBox.Show("Student was added successfully!");
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("Missing Value");
            }
        }
    }
}
