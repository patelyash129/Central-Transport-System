using Models2.Common;
using Models2.DB;
using Models2.Employee;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: Employee
       
        public PartialViewResult Dashboard()
        {
            return PartialView("_Dashboard");
        }
        public PartialViewResult Truck()
        {
            return PartialView("_TruckRequest");
        }
        [HttpGet]
        public PartialViewResult Cancellable()
        {

            return PartialView("_Cancellable");
        }
        public PartialViewResult AdminReject()
        {
            return PartialView("_Reject");
        }
        [Authorize]
        public PartialViewResult CarRequest()
        {
            return PartialView("_CarRequest");
        }
        [Authorize]
        [HttpGet]
        public PartialViewResult profile() 
        {
            string username = "";
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the username from the cookie
                username = User.Identity.Name;
            }
            client.BaseAddress = new Uri("http://localhost:52144/API/Employee/Profilet");

            Employee e = new Employee();
            var x = client.GetAsync("Profile?str="+username+"&id=1");
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                var k=test.Content.ReadAsAsync<Employee>();
                e=k.Result;

            }
            return PartialView("_Profile",e);
        }
        [Authorize]
        [HttpGet]
        public PartialViewResult getinfo()
        {
            string username = "";
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the username from the cookie
                username = User.Identity.Name;
            }
            client.BaseAddress = new Uri("http://localhost:52144/API/Employee/Profile");

            Listmodel e = new Listmodel();
            var x = client.GetAsync("Profile?str=" + username+"&id=2");
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                var k = test.Content.ReadAsAsync<Listmodel>();
                e = k.Result;
                ViewBag.list1 = e.Request;
                ViewBag.list2 = e.TruckRequest;

            }
            return PartialView("_getinfo");
        }


        [Authorize]
        [HttpGet]
        public PartialViewResult getProved()
        {
            string username = "";
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the username from the cookie
                username = User.Identity.Name;
            }
            client.BaseAddress = new Uri("http://localhost:52144/API/Employee/Profile");

            Listmodel e = new Listmodel();
            var x = client.GetAsync("Profile?str=" + username + "&id=3");
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                var k = test.Content.ReadAsAsync<Listmodel>();
                e = k.Result;
                ViewBag.list1 = e.Request;
                ViewBag.list2 = e.TruckRequest;

            }
            return PartialView("_getinfo");
        }


        [Authorize]
        [HttpGet]
        public PartialViewResult getRejected()
        {
            string username = "";
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the username from the cookie
                username = User.Identity.Name;
            }
            client.BaseAddress = new Uri("http://localhost:52144/API/Employee/Profile");

            Listmodel e = new Listmodel();
            var x = client.GetAsync("Profile?str=" + username + "&id=4");
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                var k = test.Content.ReadAsAsync<Listmodel>();
                e = k.Result;
                ViewBag.list1 = e.Request;
                ViewBag.list2 = e.TruckRequest;

            }
            return PartialView("_getinfo");
        }

        [Authorize]
        public PartialViewResult Status()
        {
            return PartialView("_status");
        }
        [HttpPost]
        public ActionResult Handle(string Data) // Changed to use a strongly typed model
        {
            try
            {
                var username = "";
                Request model = JsonConvert.DeserializeObject<Request>(Data); // Deserialize using JSON.Net
                if (User.Identity.IsAuthenticated)
                {
                    // Retrieve the username from the cookie
                     username = User.Identity.Name;               
                }

                // Assuming 'Request' is a valid model that matches the structure of your JSON
                client.BaseAddress = new Uri("http://localhost:52144/API/Employee/AddRequest");
                var x = client.PostAsJsonAsync("AddRequest?str="+username, model);
                x.Wait();
                var response = x.Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Dashboard"); // Redirect or send a success response
                }
                else
                {
                    return View("ErrorView"); // Redirect or send an error response
                }
            }
            catch (Exception ex)
            {
                return View("ErrorView"); // Handle exception and show error view
            }
        }

        [HttpPost]
        public ActionResult ProcessTruckRequest(string Data)
        {
            try
            {
                var username = "";
                TruckRequest model = JsonConvert.DeserializeObject<TruckRequest>(Data);
                if (User.Identity.IsAuthenticated)
                {
                    // Retrieve the username from the cookie
                    username = User.Identity.Name;
                }
                client.BaseAddress = new Uri("http://localhost:52144/API/TruckReq/AddTruckRequest");
                var x = client.PostAsJsonAsync("AddTruckRequest?str="+username, model);
                x.Wait();
                var response = x.Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    return RedirectToAction("Truck");
                }
            }
            catch (Exception ex)
            {
                return View("");
            }
           
        }
    }
}