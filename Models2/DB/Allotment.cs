using Models2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Frameworks.Helpers; // Assuming this namespace contains the DBColumnName and TableName attributes

namespace Models2.DB
{
        [TableName("Allotment")]
        public class Allotment:Allotbase
        {
            public int Allot_id { get; set; }

            [DBColumnName("R_Id")]
            public int R_Id { get; set; }

            [DBColumnName("V_Id")]
            public int V_Id { get; set; }

            [DBColumnName("date")]
            public string Date { get; set; }

            [DBColumnName("destination")]
            public string Destination { get; set; }

            [DBColumnName("source")]
            public string Source { get; set; }

            [DBColumnName("D_time")]
            public string D_time { get; set; }

            [DBColumnName("S_time")]
            public string S_time { get; set; }

            [DBColumnName("Res_id")]
            public int Res_id { get; set; }
        }
    }
