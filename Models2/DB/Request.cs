using Models2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Frameworks.Helpers;

namespace Models2.DB
{
    [TableName("Request")]
    public class Request:Modelbase
    {
        public int id { get; set; } 
        [DBColumnName("SingleOrReturn")]
        public string SingleOrReturn { get; set; }

        [DBColumnName("T_destination")]
        public string Destination { get; set; }

        [DBColumnName("T_date")]
        public string Date { get; set; }

        [DBColumnName("D_city")]
        public string DestinationCity { get; set; }

        [DBColumnName("D_state")]
        public string DestinationState { get; set; }

        [DBColumnName("NoOfdays")]
        public int NumberOfDays { get; set; }

        [DBColumnName("T_source")]
        public string Source { get; set; }

        [DBColumnName("S_city")]
        public string SourceCity { get; set; }

        [DBColumnName("S_State")]
        public string SourceState { get; set; }

        [DBColumnName("Purpose")]
        public string Purpose { get; set; }

        [DBColumnName("E_Id")]
        public int E_Id { get; set; }
        [DBColumnName("Allot_Id")]
        public int? Allot_Id { get; set; }
        [DBColumnName("Res_Id")]
        public int? Res_Id { get; set; }

        [DBColumnName("S_time")]
        public string StartTime { get; set; }

        [DBColumnName("D_time")]
        public string EndTime { get; set; }

        [DBColumnName("Staus")]
        public string Status { get; set; }



    }
}
