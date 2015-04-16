using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterCareApplication
{
    class InvoiceItem
    {
        public int InvoiceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Balance { get; set; }
        public bool Paid { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int GuardianId { get; set; }
        public string GroupProperty { get; set; }
        public List<int> studentId = new List<int>();
        public List<User> studentUser = new List<User>();
        public List<User> StudentID
        {
            get
            {
                return studentUser;
            }
            set 
            {
                studentUser = value;
            }
        }
        public int studentCount = 0;
        public int StudentCount { get { return studentCount; } set { studentCount = value; } }
        public InvoiceItem(){
            
        }
        public void addStudId(int id){
            studentId.Add(id);
            studentCount++;
        }
    }
}
