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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AfterCareApplication
{
    /// <summary>
    /// Interaction logic for InvoicePage.xaml
    /// </summary>
    public partial class InvoicePage : Page
    {
        public InvoicePage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void buildListforInvoices()
        {
            AfterCareDataContext dbContext = new AfterCareDataContext();
            List<InvoiceItem> guardian_invoiceList = (
                from s in dbContext.Guardian_Invoices
                from n in dbContext.Guardians
                from u in dbContext.Users
                where s.guardianId == n.guardianId
                where n.userId == u.userId
                select new InvoiceItem {InvoiceId = s.invoiceId , GuardianId = n.guardianId, FirstName = u.firstName, LastName = u.lastName,
                    StreetNumber = n.streetNumber, StreetName = n.streetName, City = n.city, State = n.state, Zip = n.zip,
                    StartDate = s.startDate.ToString(), EndDate = s.endDate.ToString(), Balance = s.balance, Paid = s.paid,
                    }).ToList();

            List<Student_Guardian> studentList = (
                from sg in dbContext.Student_Guardians
                select new Student_Guardian {guardianId = sg.guardianId, studentId = sg.studentId}).ToList();

            List<User> studentUser = new List<User>();
            guardian_invoiceList.ForEach(delegate(InvoiceItem guardList)
            {
                studentList.ForEach(delegate(Student_Guardian studList)
                {
                    if (guardList.GuardianId == studList.guardianId)
                    {
                        guardList.addStudId(studList.studentId);
                        int xtemp = studList.studentId;
                        List<User> studentsByID = (
                            from su in dbContext.Users
                            from s in dbContext.Students
                            where su.userId == s.userId
                            where s.studentId == xtemp
                            select su).ToList();
                        guardList.StudentID.Add(studentsByID.Single());
                    }
                });
            });
            
            ListCollectionView invoiceView = new ListCollectionView(guardian_invoiceList);
            PropertyGroupDescription invoiceGroup = new PropertyGroupDescription("GuardianId");
            invoiceView.GroupDescriptions.Add(invoiceGroup);
            
            invoiceGrid.ItemsSource = invoiceView;
            
            
            
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            buildListforInvoices();
        }
    }
}
