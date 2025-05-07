using Models2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Frameworks.Helpers;

namespace Models2.DB
{
    [TableName("TruckRequest")]
    public class TruckRequest:BaseModel
    {
        [DBColumnName("Id")]
        public int id { get; set; }

        [DBColumnName("E_Id")]
        public double EmployeeId { get; set; }

        [DBColumnName("venue")]
        public string Venue { get; set; }

        [DBColumnName("Phone number")]
        public double PhoneNumber { get; set; }

        [DBColumnName("Purpose")]
        public string Purpose { get; set; }

        [DBColumnName("Date")]
        public string Date { get; set; }

        [DBColumnName("Time")]
        public string Time { get; set; }

        [DBColumnName("Request For")]
        public string RequestFor { get; set; }

        [DBColumnName("Res_Id")]
        public int? Res_Id { get; set; }

        [DBColumnName("Allot_Id")]
        public int? AllotId { get; set; }

        [DBColumnName("Status")]
        public string Status { get; set; }
    }

}

