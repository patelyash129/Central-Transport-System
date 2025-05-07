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
    public class AdmindepController : ApiController
    {
        DataManupulation rep = new DataManupulation();
        [HttpPost]
        public IHttpActionResult AddDep(Department dep)
        {
            var x = rep.AddDepartment(dep);
            return Ok(x);
        }

    }
}
