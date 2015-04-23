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
    /// Interaction logic for GuardianFormPage.xaml
    /// </summary>
    public partial class GuardianFormPage : Page
    {
        public string streetNumber { get; set; }
        public string streetName { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string home { get; set; }
        public string cell { get; set; }
        public string userID { get; set; }
        private AfterCareDataContext db;
        public GuardianFormPage()
        {
            InitializeComponent();
        }
        public GuardianFormPage(AfterCareDataContext db, string UserID)
        {
            this.db = db;
            this.DataContext = this;
            InitializeComponent();
            userID = UserID;
            home = null;
            cell = null;
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
                this.streetNumber != null &&
                this.streetName != null &&
                this.zip != null &&
                this.city != null &&
                this.state != null
            )
            {
                
                Guardian newGuardian = new Guardian { userId = this.userID, streetNumber = this.streetNumber, streetName = this.streetName, city = this.city, state = this.state, zip = this.zip };
                db.Guardians.InsertOnSubmit(newGuardian);
                db.SubmitChanges();
                if (home != null)
                {
                    db.ExecuteCommand("INSERT INTO Guardian_Phone VALUES ({0},{1},{2})", newGuardian.guardianId, home, "Home Phone" );
                    db.SubmitChanges();
                }
                else if (cell != null)
                {
                    db.ExecuteCommand("INSERT INTO Guardian_Phone VALUES ({0},{1},{2})", newGuardian.guardianId, cell, "Cell Phone");
                    db.SubmitChanges();
                }
                MessageBox.Show("Guardian was added successfully!");
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("Missing Value");
            }
        }

        
    }
}
