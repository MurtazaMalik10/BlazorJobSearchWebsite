using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EntCompany
    {
        public int CompanyID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Roles { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string MobileNumber { get; set; }
        public string Reg_Date { get; set; }
        public string Last_Login { get; set; }
        public string DisplayPicture { get; set; }


    }
}