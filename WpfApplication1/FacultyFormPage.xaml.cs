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
    /// Interaction logic for FacultyFormPage.xaml
    /// </summary>
    public partial class FacultyFormPage : Page
    {
        AfterCareDataContext db;
        public FacultyFormPage()
        {
            InitializeComponent();
        }
        public FacultyFormPage(AfterCareDataContext db)
        {
            this.db = db;
            InitializeComponent();
        }
    }
}
