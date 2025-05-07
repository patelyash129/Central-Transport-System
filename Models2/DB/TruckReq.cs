using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models2.DB
{
    public class TruckReq
    {
            public int Id { get; set; }
            public string EmployeeId { get; set; }
            public string Name { get; set; }
            public string DepartmentName { get; set; }
            public string PhoneNumber { get; set; }
            public string Purpose { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan Time { get; set; }
            public string RequestFor { get; set; }
            public int NumberOfVehicles { get; set; }
            public string Venue { get; set; }
        
    }
}
