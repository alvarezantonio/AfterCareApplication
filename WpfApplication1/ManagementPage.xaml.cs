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
    /// Interaction logic for ManagementPage.xaml
    /// </summary>
    public partial class ManagementPage : Page
    {
        public ManagementPage()
        {
            InitializeComponent();
        }
        private void openNewUserForm()
        {
            Window dialogWin = new Window();
            dialogWin.ResizeMode = ResizeMode.NoResize;
            dialogWin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dialogWin.WindowStyle = WindowStyle.None;
            dialogWin.Height = 400;
            dialogWin.Width = 650;
            NewUserFormPage newUser = new NewUserFormPage();
            dialogWin.Content = newUser;
            dialogWin.ShowDialog();
            if (dialogWin.DialogResult == true)
            {
                //setUserInfo(facPage.UserName, facPage.UserTitle, facPage.UserAddress);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            openNewUserForm();
        }
    }
}
