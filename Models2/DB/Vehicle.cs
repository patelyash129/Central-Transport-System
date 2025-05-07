using Models2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Frameworks.Helpers; // Import for custom attributes

namespace Models2.DB
{
    [TableName("vehicle")]
    public class Vehicle : BaseModel
    {
        // Making V_Id nullable
        public int V_Id { get; set; }

        [DBColumnName("V_Name")]
        public string V_Name { get; set; }

        [DBColumnName("V_type")]
        public string V_type { get; set; }

        [DBColumnName("V_number")]
        public string V_number { get; set; }

        [DBColumnName("V_color")]
        public string V_color { get; set; }
    }
}
