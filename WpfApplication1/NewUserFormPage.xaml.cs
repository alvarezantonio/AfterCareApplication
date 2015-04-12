﻿using System;
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

        }
 
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            Page form = null;
            if (userTypeBox.SelectedItem != null)
            {
                
                switch(userTypeBox.SelectionBoxItem.ToString())
                {
                    case "Student":
                        form = new StudentFormPage(db);
                    break;
                    case "Guardian":
                        form = new GuardianFormPage(db);
                    break;
                    case "Faculty":
                        form = new FacultyFormPage(db);
                    break;
                }
                Window.GetWindow(this).Content = form;
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
                accessBox.ItemsSource = this.source;
                accessBox.SelectedIndex = 0;
            }
        }

        private void userTypeBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            accessBox.Items.Refresh();
            accessBox.SelectedIndex = 0;
        }

        private void accessBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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



    }
}
