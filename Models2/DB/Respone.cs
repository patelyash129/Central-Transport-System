using Models2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Frameworks;
using Tech.Frameworks.Helpers;

namespace Models2.DB
{
    
        [TableName("Respone")]
        public class Respone:Resbase
        {
            
            [DBColumnName("Req_id")]
            public int Req_id { get; set; } // Nullable int

            [DBColumnName("V_id")]
            public int V_id { get; set; } // Nullable int
            
            [DBColumnName("Status")]
            public string Status { get; set; }

            [DBColumnName("Reason")]
            public string Reason { get; set; }
        }
}
