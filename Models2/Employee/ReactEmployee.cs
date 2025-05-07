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
    public class ReactEmployee:Modelbase
    {
        [DBColumnName("Id")]
        public int id { get; set; }
        [DBColumnName("Name")]
        public string Name { get; set; }
        [DBColumnName("Address")]
        public string Address { get; set; }
        [DBColumnName("URL")]
        public string Url { get; set; }
    }
}
