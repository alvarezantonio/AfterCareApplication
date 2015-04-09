using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;

namespace AfterCareApplication
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void sessionButton_Click(object sender, RoutedEventArgs e)
        {
            Page sessionPg = new SessionPage();
            this.NavigationService.Navigate(sessionPg);
        }

        private void userButton_Click(object sender, RoutedEventArgs e)
        {
            Page userInfo = new UserInfoPage();            
            this.NavigationService.Navigate(userInfo);
        }
        
        private void reportsButton_Click(object sender, RoutedEventArgs e)
        {
            Page reports = new ReportsPage();
            this.NavigationService.Navigate(reports);
        }

        private void invoicesButton_Click(object sender, RoutedEventArgs e)
        {
            Page invoices = new InvoicePage();
            this.NavigationService.Navigate(invoices);  
        }

        private void accessButton_Click(object sender, RoutedEventArgs e)
        {
            Page access = new AccessLevelsPage();
            this.NavigationService.Navigate(access);
        }

        private void scheduleButton_Click(object sender, RoutedEventArgs e)
        {
            Page schedule = new SchedulePage();
            this.NavigationService.Navigate(schedule);
        }

        private void enrollButton_Click(object sender, RoutedEventArgs e)
        {
            Page enroll = new EnrollmentPage();
            this.NavigationService.Navigate(enroll);
        }

        private void manageButton_Click(object sender, RoutedEventArgs e)
        {
            Page management = new ManagementPage();
            this.NavigationService.Navigate(management);
        }
    }
}
