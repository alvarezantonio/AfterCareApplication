using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterCareApplication
{
    class GuardianUser
    {
        public string UserID { get; set; }
        public int GuardianID { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public GuardianUser()
        {

        }
    }
}
