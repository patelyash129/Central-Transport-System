using Models2.DB;
using Models2.Employee;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webAPI.Controllers
{
    public class AdminAddController : ApiController
    {
        DataManupulation rep = new DataManupulation();
        [HttpGet]
        public IHttpActionResult TruckAllRequest()
        {
            List<TruckRequest> x = rep.ShowAllTruck();
            return Ok(x);
        }
        [HttpPost]
        public IHttpActionResult Insertemp(Employee employee)
        {
            var x = rep.AddData(employee);
            return Ok(x);
        }
    }
}
