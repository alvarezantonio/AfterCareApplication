using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace AfterCareApplication
{
    class ContextHelper
    {
        private AfterCareDataContext db = new AfterCareDataContext();
        private ListCollectionView theView;
        public ContextHelper()
        {
        }

        //Returns a listcollectionview by userType only
        public ListCollectionView getUsersByType(String userType)
        {
            theView = null;
            switch (userType)
            {
                case ("Student"):
                    var stud = (
                        from s in db.Students
                        from u in db.Users
                        where s.userId == u.userId
                        select new
                        {
                            UserID = s.userId,
                            First = u.firstName,
                            Last = u.lastName,
                            DOB = s.birthday,
                            Homeroom = s.homeroom
                        }).ToList();
                    theView = new ListCollectionView(stud);
                    break;
                case ("Faculty"):
                    var fac = (
                    from f in db.Faculties
                    from u in db.Users
                    where f.userId == u.userId
                    select new
                    {
                        UserID = f.userId,
                        First = u.firstName,
                        Last = u.lastName,
                        StreetNumber = f.streetNumber,
                        StreetName = f.streetName,
                        City = f.city,
                        State = f.state,
                        Zip = f.zip
                    }).ToList();
                    theView = new ListCollectionView(fac);
                    break;
                case ("Guardian"):
                    List<GuardianUser> guard = (
                        from g in db.Guardians
                        from u in db.Users
                        where g.userId == u.userId
                        select new GuardianUser
                        {
                            UserID = g.userId,
                            GuardianID = g.guardianId,
                            First = u.firstName,
                            Last = u.lastName,
                            StreetNumber = g.streetNumber,
                            StreetName = g.streetName,
                            City = g.city,
                            State = g.state,
                            Zip = g.zip
                        }).ToList();
                    theView = new ListCollectionView(guard);
                    break;
            }
            return theView;
        }

        //Returns a listcollectionview by userType and userId
        public ListCollectionView getUserByTypeAndId(String userType, String userId)
        {
            theView = null;
            switch (userType)
            {
                case("Student"):
                    var stud = (
                        from s in db.Students
                        from u in db.Users
                        where s.userId == u.userId
                        where s.userId == userId
                        select new { UserID = s.userId, First = u.firstName, Last = u.lastName,
                        DOB = s.birthday, Homeroom = s.homeroom}).ToList();
                        theView = new ListCollectionView(stud);
                    break;
                case ("Faculty"):
                        var fac = (
                        from f in db.Faculties
                        from u in db.Users
                        where f.userId == u.userId
                        where f.userId == userId
                        select new { UserID = f.userId, First = u.firstName, Last = u.lastName,
                        StreetNumber = f.streetNumber, StreetName = f.streetName, City = f.city,
                        State = f.state, Zip = f.zip}).ToList();
                        theView = new ListCollectionView(fac);
                    break;
                case ("Guardian"):
                    var guard = (
                        from g in db.Guardians
                        from u in db.Users
                        where g.userId == u.userId
                        where g.userId == userId
                        select new { UserID = g.userId, First = u.firstName, Last = u.lastName,
                        StreetNumber = g.streetNumber, StreetName = g.streetName, City = g.city,
                        State = g.state, Zip = g.zip}).ToList();
                        theView = new ListCollectionView(guard);
                    break;
            }
            return theView;
        }

    }
}
