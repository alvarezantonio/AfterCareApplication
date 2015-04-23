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
    /// Interaction logic for NewUserFormPage.xaml
    /// </summary>
    public partial class NewUserFormPage : Page
    {
        AfterCareDataContext db = new AfterCareDataContext();
        
        public string userId { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string password { get; set; }
        public int accessId { get; set; }
        public List<Access> source;
        public List<Access> dSource
        {
            get { return this.source; }
            set { this.source = value; }
        }
        
        
        public NewUserFormPage()
        {
            this.DataContext = this;
            InitializeComponent();
            this.userId = null;
            this.lastName = null;
            this.firstName = null;
            this.password = null;
            this.accessId = -1;
        }
 
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (userId != null && lastName != null && firstName != null && password != null && accessId >= 0)
            {
                User newUser = new User
                {   
                    userId = this.userId,
                    accessId = this.accessId,
                    firstName = this.firstName,
                    lastName = this.lastName,
                    password = this.password
                };
                db.Users.InsertOnSubmit(newUser);
                Page form = null;
                if (userTypeBox.SelectedItem != null)
                {

                    switch (userTypeBox.SelectionBoxItem.ToString())
                    {
                        case "Student":
                            form = new StudentFormPage(db, userId);
                            break;
                        case "Guardian":
                            form = new GuardianFormPage(db, userId);
                            break;
                        case "Faculty":
                            form = new FacultyFormPage(db, userId);
                            break;
                    }
                    Window.GetWindow(this).Content = form;
                }
            }
            else
            {
                nextErrorLabel.Content = "*Required fields are missing!";
            }
        }

        private void userTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)combobox.SelectedItem;
            if (item.Content != null)
            {
                switch (item.Content.ToString())
                {
                    case "Student":
                        this.dSource = (List<Access>)(
                            from a in db.Accesses
                            where a.accessId == 1
                            select a).ToList();
                        break;
                    case "Guardian":
                        this.dSource = (List<Access>)(
                        from a in db.Accesses
                        where a.accessId == 0
                        select a).ToList();
                        break;
                    case "Faculty":
                        this.dSource = (List<Access>)(
                            from a in db.Accesses
                            where a.accessId >= 2
                            select a).ToList();
                        break;
                }
                updateAccessSource();
            }
        }

        private void updateAccessSource()
        {
            if (accessBox != null)
            {
                accessBox.ItemsSource = null;
                accessBox.ItemsSource = this.dSource;
                accessBox.Items.Refresh();
                accessBox.SelectedIndex = 0;
                setAccess();
            }
        }

        private void accessBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setAccess();
        }

        private void setAccess()
        {
            ComboBox combobox = (ComboBox)accessBox;
            Access item = (Access)combobox.SelectedItem;
            if (item != null && item.accessName.Length > 0)
            {
                Access tempAccess = (from a in db.Accesses
                                     where a.accessName == item.accessName
                                     select a).Single();
                this.accessId = tempAccess.accessId;
            } 
        }

        private void passwordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passBox = (PasswordBox) sender;
            string passValue = passBox.Password;
            if (passBox.Password.Length > 0)
            {
                if (String.IsNullOrWhiteSpace(passValue))
                {
                    passErrorLabel.Content = "*Password may not be a blank space";
                }
                else
                {
                    passErrorLabel.Content = "";
                    this.password = passValue;
                    confirmPasswordInput_PasswordChanged(confirmPasswordInput, e);
                }
            }
            else
            {
                passErrorLabel.Content = "*Password may not be empty";
            }
        }

        private void confirmPasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passBox = (PasswordBox)sender;
            string passValue = passBox.Password;
            
            if (passValue.Length > 0 && passValue != password)
            {
                passErrorLabel.Content = "*Passwords do not match";
            }
            else
            {
                passErrorLabel.Content = "";
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = false;
            Window.GetWindow(this).Close();
        }

        private void accessBox_Loaded(object sender, RoutedEventArgs e)
        {
            setAccess();
        }

    }
}
