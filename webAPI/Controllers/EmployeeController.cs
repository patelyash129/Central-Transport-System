using Models2.DB;
using Models2.Employee;
using Models2.User;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webAPI.Controllers
{


    public class EmployeeController : ApiController
    {
        DataManupulation rep = new DataManupulation();
        [HttpPost]
        public IHttpActionResult AddRequest(string str,Request req)
        {
                var x = rep.AddRequest(str, req);
                return Ok(x);
          
        }
        [HttpGet]
        public IHttpActionResult Profile(string str,int id)
        {
            if (id == 1)
            {
                var x = rep.profile(str);
                return Ok(x);
            }
            else if(id==2)
            {
                List<Request> x= rep.reqdata(str);
               List<TruckRequest> y=rep.reqdatatruck(str);
                Listmodel n=new Listmodel()
                {
                    Request=x,
                    TruckRequest=y,
                };
               
                return Ok(n);
            }

            else if (id == 3)
            {
                List<Request> x = rep.reqapprovedata(str);
                List<TruckRequest> y = rep.reqapprovedatatruck(str);
                Listmodel n = new Listmodel()
                {
                    Request = x,
                    TruckRequest = y,
                };

                return Ok(n);
            }

            else if (id == 4)
            {
                List<Request> x = rep.reqrejectdata(str);
                List<TruckRequest> y = rep.reqrejectdatatruck(str);
                Listmodel n = new Listmodel()
                {
                    Request = x,
                    TruckRequest = y,
                };

                return Ok(n);
            }
            else if (id == 6)
            {
                List<Request> x = rep.reqAdminrejectdata();
                List<TruckRequest> y = rep.reqAdminrejectdatatruck();
                Listmodel n = new Listmodel()
                {
                    Request = x,
                    TruckRequest = y,
                };

                return Ok(n);
            }

            else if (id == 7)
            {
                List<Request> x = rep.reqAdminapprovedata();
                List<TruckRequest> y = rep.reqAdminapprovedatatruck();
                Listmodel n = new Listmodel()
                {
                    Request = x,
                    TruckRequest = y,
                };

                return Ok(n);
            }

            else { return BadRequest(); }
        }
    }
}
