using Models2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Frameworks.Helpers;

namespace Models2.Employee
{
        [TableName("Department")]
        public class Department:BaseModel
        {

            [DBColumnName("Name")]
            public string DepartmentName { get; set; }

            [DBColumnName("Designation")]
            public string DepartmentDesignation { get; set; }

            [DBColumnName("Location")]
            public string DepartmentLocation { get; set; }
        }
}
