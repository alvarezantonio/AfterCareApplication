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
    /// Interaction logic for UserInfoPage.xaml
    /// </summary>
    public partial class UserInfoPage : Page
    {
        public List<User> userList;
        private string userType = "";
        private AfterCareDataContext db = new AfterCareDataContext();
        public UserInfoPage()
        {
            InitializeComponent();
        }

        public void setUserInfo(User userInfo)
        {
            
            info.captionLabel.Content = userType;
            info.UserName = userInfo.firstName + " " + userInfo.lastName;
            info.UserTitle = userType;
            ContextHelper cH = new ContextHelper();
            ListCollectionView view = cH.getUserByTypeAndId(userType, userInfo.userId);
            if (view != null)
            {
                addToGrid(view);
            }
        }

        private void addToGrid(ListCollectionView userView){
            info.GridInfo = userView;
        }

        private void facultyInfoButton_Click(object sender, RoutedEventArgs e)
        {
            userType = "Faculty";
            userList = (List<User>)(
                from u in db.Users
                from f in db.Faculties
                where u.userId == f.userId
                select u).ToList();
            openSelectUserDialog(userList);
        }

        private void myInfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void openSelectUserDialog(List<User> list)
        {
            Window dialogWin = new Window();
            dialogWin.ResizeMode = ResizeMode.NoResize;
            dialogWin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dialogWin.WindowStyle = WindowStyle.None;
            dialogWin.Height = 490;
            dialogWin.Width = 400;
            SelectUserPage facPage = new SelectUserPage(list);
            dialogWin.Content = facPage;
            dialogWin.ShowDialog();
            if (dialogWin.DialogResult == true)
            {
                setUserInfo(facPage.UserInfo);
            }
        }

        private void studentInfoButton_Click(object sender, RoutedEventArgs e)
        {
            userType = "Student";
            userList = (List<User>)(
                from u in db.Users
                from f in db.Students
                where u.userId == f.userId
                select u).ToList();
            openSelectUserDialog(userList);
        }

        private void guadianInfoButton_Click(object sender, RoutedEventArgs e)
        {
            userType = "Guardian";
            userList = (List<User>)(
                from u in db.Users
                from f in db.Guardians
                where u.userId == f.userId
                select u).ToList();
            openSelectUserDialog(userList);
        }
    }
}
