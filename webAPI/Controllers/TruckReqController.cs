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
    public class TruckReqController : ApiController
    {
        DataManupulation dm=new DataManupulation();
        [HttpPost]
        public IHttpActionResult AddTruckRequest(string str,TruckRequest Tq)
        {
            var x = dm.AddTruckreq(str,Tq);
            return Ok(x);
        }
    }
}
