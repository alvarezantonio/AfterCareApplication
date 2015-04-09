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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void Grid_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LogIn();
            }
        }
        private void LogIn()
        {
            Page welcomeWin = new WelcomePage();
            this.NavigationService.Navigate(welcomeWin);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogIn();
        }
    }
}
