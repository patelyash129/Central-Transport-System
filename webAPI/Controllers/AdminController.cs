using Models2.DB;
using Models2.Employee;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace webAPI.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]

    public class AdminController : ApiController
    {
        DataManupulation rep = new DataManupulation();
       
        [HttpGet]
        public IHttpActionResult GetAll(int id)
        {
            if(id == 1)
            {
                List<Request> x = rep.GetAllData();
                return Ok(x);
            }
            else if(id==2) {
                List<Request> x = rep.GetAllHistoryData();
                return Ok(x);
            }
            else if(id==3)
             {
                List<ReactEmployee> x = rep.GetAllEmp();
                return Ok(x);
            }
            else
            {
                return BadRequest("bad");
            }
           
        }
       
        [HttpPost]
        public IHttpActionResult AllotVehicle(int id,Allotment All)
        {
            if (id==1)
            {
                var x = rep.AllotVeh(All);
                return Ok(x);
            }
            else if(id==2)
            {
                var x = rep.Rej(All);
                return Ok(x);
            }
            else
            {
                return BadRequest();
            }
            
        }


        public string state()
        {
            return "successfully get the data";
        }
       
    }
}
