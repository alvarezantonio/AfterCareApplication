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
    /// Interaction logic for selectFacultyPage.xaml
    /// </summary>
    public partial class SelectUserPage : Page
    {
        private string userName;
        private string userTitle;
        private User userInfo;
        public string UserName { get { return this.userName; } set { this.userName = value; } }
        public string UserTitle { get { return this.userTitle; } set { this.userTitle = value; } }
        public User UserInfo { get { return this.userInfo; } set { this.userInfo = value; } }

        public SelectUserPage(List<User> listOfUsers)
        {
            InitializeComponent();
            ListCollectionView view = new ListCollectionView(listOfUsers);
            this.userList.ItemsSource = listOfUsers;
            userList.SelectedIndex = 0;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DialogResult = false;
            closeWindow();
        }

        private void setUserButton_Click(object sender, RoutedEventArgs e)
        {
            itemSelected();
        }

        private void itemSelected()
        {
            int index = userList.SelectedIndex;
            User user = (User) userList.Items.GetItemAt(index);
            UserInfo = user;
            Window.GetWindow(this).DialogResult = true;
            closeWindow();
        }

        private void closeWindow()
        {
            Window theWindow = Window.GetWindow(this);
            theWindow.Close();
        }

        private void userList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            itemSelected();
        }
    }
}
