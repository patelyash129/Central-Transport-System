using Models2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Frameworks;
using Tech.Frameworks.Helpers;

namespace Models2.User
{
    [TableName("UserTable")]
    public class Signup:UserBase
    {
        [DBColumnName("U_email")]
        public string email {  get; set; }
        [DBColumnName("U_Password")]
        public string password { get; set; }
        [DBColumnName("E_Id")]
        public int Emp_Id { get; set; }

       
    }
}
