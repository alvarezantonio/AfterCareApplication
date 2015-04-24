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

namespace AfterCareApplication
{
    /// <summary>
    /// Interaction logic for FacultyFormPage.xaml
    /// </summary>
    public partial class FacultyFormPage : Page
    {
        public string streetNumber { get; set; }
        public string streetName { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string home { get; set; }
        public string cell { get; set; }
        public string userID { get; set; }
        public float wage { get; set; }
        public string wageType { get; set; }
        AfterCareDataContext db;
        public FacultyFormPage()
        {
            InitializeComponent();
        }
        public FacultyFormPage(AfterCareDataContext db, string UserID)
        {
            this.db = db;
            this.DataContext = this;
            InitializeComponent();
            userID = UserID;
            home = null;
            cell = null;
            wage = 0;
            streetNumber = null;
            streetName = null;
            city = null;
            state = null;
            zip = null;
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
                this.wage >= 0 &&
                this.wageType != null &&
                this.streetNumber != null &&
                this.streetName != null &&
                this.zip != null &&
                this.city != null &&
                this.state != null
            )
            {

                Faculty newFaculty = new Faculty { userId = this.userID, streetNumber = this.streetNumber, streetName = this.streetName, city = this.city, state = this.state, zip = this.zip };
                db.Faculties.InsertOnSubmit(newFaculty);
                db.SubmitChanges();

                db.ExecuteCommand("INSERT INTO Faculty_Wage VALUES ({0},{1},{2})",newFaculty.facultyId, wage, wageType); 

                if (home != null)
                {
                    db.ExecuteCommand("INSERT INTO Faculty_Phone VALUES ({0},{1},{2})", newFaculty.facultyId, home, "Home Phone");
                    db.SubmitChanges();
                }
                else if (cell != null)
                {
                    db.ExecuteCommand("INSERT INTO Faculty_Phone VALUES ({0},{1},{2})", newFaculty.facultyId, cell, "Cell Phone");
                    db.SubmitChanges();
                }

                MessageBox.Show("Faculty was added successfully!");
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("Missing Value");
            }
        }

        private void updateWageType(object sender, RoutedEventArgs e)
        {
            ComboBox wageTypeBox = (ComboBox)sender;
            ComboBoxItem type = (ComboBoxItem)wageTypeBox.SelectedItem;
            this.wageType = type.Content.ToString();
        }

    }
}
