using Models2.DB;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webAPI.Controllers
{
    public class AdminvehController : ApiController
    {
        DataManupulation rep = new DataManupulation();
        [HttpGet]
        public IHttpActionResult GetAvlVehicle(string date,string For)
        { 
                List<Vehicle> x = rep.GetVehicle(date,For);
                return Ok(x);
            
          
        }
        [HttpPost]
        public IHttpActionResult Addveh(Vehicle v)
        {
            var x = rep.AddVeh(v);
            return Ok(x);
        }
    }
}
