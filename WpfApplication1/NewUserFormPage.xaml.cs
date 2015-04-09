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
        public NewUserFormPage()
        {
            InitializeComponent();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            Page form = null;
            if (userTypeBox.SelectedItem != null)
            {
                switch(userTypeBox.SelectionBoxItem.ToString())
                {
                    case "Student":
                        form = new StudentFormPage();
                    break;
                    case "Guardian":
                        form = new GuardianFormPage();
                    break;
                    case "Faculty":
                        form = new FacultyFormPage();
                    break;
                }
                Window.GetWindow(this).Content = form;
            }
        }
    }
}
