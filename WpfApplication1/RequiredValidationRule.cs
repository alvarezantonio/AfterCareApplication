using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace AfterCareApplication
{
    class RequiredValidationRule : ValidationRule
    {

        public string _propertyName { get; set; }

        public RequiredValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            AfterCareDataContext db = new AfterCareDataContext();
            var str = (string)value;
            if (str == null || str.Length < 1)
            {
                
                return new ValidationResult(false, "*Required field");
            }
            else if(String.IsNullOrWhiteSpace(str))
            {
                return new ValidationResult(false, "*Field can't be a blank space");
            }
            else if (_propertyName != null && _propertyName.Length > 1)
            {
                switch (_propertyName)
                {
                    case "userId":
                        try
                        {
                            List<User> userList = (
                                from u in db.Users
                                select u).ToList();
                            User userExists = (
                                from u in db.Users
                                where u.userId.Equals(str)
                                select u).Single();
                            int idx = userList.IndexOf(userExists);
                            if (idx >= 0)
                            {
                                return new ValidationResult(false, "*UserID Exists");
                            }
                        }
                        catch (Exception e) {}
                        break;
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
