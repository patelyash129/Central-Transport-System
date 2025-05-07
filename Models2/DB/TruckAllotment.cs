using Models2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Frameworks.Helpers;

namespace Models2.DB
{
    [TableName("TruckAllotment")]

    public class TruckAllotment:Allotbase
    {
        public int Allot_Id { get; set; }

        [DBColumnName("R_Id")]
        public int R_Id { get; set; }

        [DBColumnName("V_Id")]
        public int V_Id { get; set; }

        [DBColumnName("date")]
        public string date { get; set; }

        [DBColumnName("Venue")]
        public string Venue { get; set; }

        [DBColumnName("Time")]
        public string Time { get; set; }

        [DBColumnName("Res_id")]
        public int Res_id { get; set; }

        [DBColumnName("is_deleted")]
        public string is_deleted { get; set; }
    }
}
