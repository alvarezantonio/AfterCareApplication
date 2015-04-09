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
    /// Interaction logic for InfoPage.xaml
    /// </summary>
    public partial class InfoPage : Page
    {
        private string userName;
        private string userTitle;
        private ListCollectionView gridInfo;
        //public string userName, userTitle, userAddress;
        public string UserName { get { return this.userName; } set { this.userName = value; nameOutput.Content = this.userName; } }
        public string UserTitle {get { return this.userTitle; } set { this.userTitle = value; titleOutput.Content = this.userTitle;}}
        public ListCollectionView GridInfo { get { return this.gridInfo; } set { this.gridInfo = value; userDataGrid.ItemsSource = this.gridInfo; } }

        public InfoPage()
        {
            InitializeComponent();
        }
    }
}
