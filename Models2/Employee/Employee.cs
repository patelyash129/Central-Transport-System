using Models2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Frameworks.Helpers;

namespace Models2.Employee
{
    [TableName("Employee")]
    public class Employee:BaseModel
    {
        [DBColumnName("Name")]
        public string Name { get; set; }

        [DBColumnName("Address")]
        public string Address { get; set; }

        [DBColumnName("Age")]
        public string Age { get; set; }

        [DBColumnName("Email")]
        public string Email { get; set; }

        [DBColumnName("Salary")]
        public double Salary { get; set; }

        [DBColumnName("Number")]
        public string Number { get; set; }

        [DBColumnName("D_Id")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
