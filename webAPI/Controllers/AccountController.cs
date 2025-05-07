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
    public class AccountController : ApiController
    {
        DataManupulation rep = new DataManupulation();
        [HttpPost]
        public IHttpActionResult Register(int id, Signup sg)
        {
            if (id == 1)
            {
                var x = rep.Signupadd(sg);
                return Ok(x);
            }
            else if (id == 2)
            {
                Loginmodel lg = new Loginmodel()
                {
                    Email = sg.email,
                    Password = sg.password
                };
                var x = rep.LoginReq(lg);
                return Ok(x);
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
