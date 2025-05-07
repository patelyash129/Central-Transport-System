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
    public class AdminController : Controller
    {
        // GET: Admin
        HttpClient client = new HttpClient();

        public PartialViewResult DashBoard()
        {
            return PartialView("_AdminDash");
        }
        public PartialViewResult AddEmployee()
        {
            return PartialView("_AddEmployee");
        }


        public PartialViewResult AddDepartment()
        {
            return PartialView("AddDepartment");
        }
        public PartialViewResult AddVehicle()
        {
            return PartialView("_AddVehicle");
        }
        public PartialViewResult Reject()
        {
            return PartialView("_Reject");
        }
        [HttpGet]
        public PartialViewResult AllRequest()
        {
            List<Request> demo = new List<Request>();
            client.BaseAddress = new Uri("http://localhost:52144/API/Admin/GetAll");
            var x = client.GetAsync("GetAll?id=1");
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Request>>();
                demo = display.Result;
                return PartialView("_AllRequest", demo);
            }
            else
            {
                return PartialView("_Admindash");
            }

        }


        [HttpGet]
        public PartialViewResult TruckAllRequest()
        {
            List<TruckRequest> demo = new List<TruckRequest>();
            client.BaseAddress = new Uri("http://localhost:52144/API/AdminAdd/TruckAllRequest");
            var x = client.GetAsync("TruckAllRequest");
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<TruckRequest>>();
                demo = display.Result;
                return PartialView("_TruckRequest", demo);
            }
            else
            {
                return PartialView("_Admindash");
            }

        }

        [HttpGet]
        public PartialViewResult getAdminRejected()
        {
            string username = "";
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the username from the cookie
                username = User.Identity.Name;
            }
            client.BaseAddress = new Uri("http://localhost:52144/API/Employee/Profile");

            Listmodel e = new Listmodel();
            var x = client.GetAsync("Profile?str=hello&id=6");
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

        [HttpGet]
        public PartialViewResult getAdminProved()
        {
            string username = "";
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the username from the cookie
                username = User.Identity.Name;
            }
            client.BaseAddress = new Uri("http://localhost:52144/API/Employee/Profile");

            Listmodel e = new Listmodel();
            var x = client.GetAsync("Profile?str=hello&id=7");
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

        [HttpGet]
        public PartialViewResult History()
        {
            List<Request> demo = new List<Request>();
            client.BaseAddress = new Uri("http://localhost:52144/API/Admin/GetAll");
            var x = client.GetAsync("GetAll?id=2");
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Request>>();
                demo = display.Result;
                return PartialView("_History", demo);
            }
            else
            {
                return PartialView("_Admindash");
            }

        }


        [HttpGet]
        public PartialViewResult Check()
        {

            List<Vehicle> demo = new List<Vehicle>();
            // int id = Convert.ToInt32(TempData["key"]);
            // string date = "12/09/24";
            string date = TempData["Date"].ToString();
            string For = TempData["For"].ToString();
            TempData.Keep();
            client.BaseAddress = new Uri("http://localhost:52144/API/Adminveh/GetAvlVehicle");
            var x = client.GetAsync("GetAlvVehicle?date=" + date + "&For=" + For);
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Vehicle>>();
                demo = display.Result;
                return PartialView("_CheckAvailibility", demo);
            }
            else
            {
                return PartialView("_AdminDash");
            }
        }




        [HttpPost]
        public JsonResult Rejection(string data)
        {
            try
            {
                //   var x = JsonConvert.DeserializeObject<String>(data); // Deserialize using JSON.Net

                Allotment all = new Allotment()
                {
                    R_Id = Convert.ToInt32(TempData["Req_id"] ?? "0"), // Using null coalescing operator to handle null value
                    V_Id = 0,
                    Date = TempData["Date"]?.ToString(), // Using null conditional operator to prevent NullReferenceException
                    Destination = TempData["Destination"]?.ToString(), // Using null conditional operator to prevent NullReferenceException
                    Source = data, // Assuming 'data' is not null
                    D_time = TempData["D_time"]?.ToString(), // Using null conditional operator to prevent NullReferenceException
                    S_time = TempData["S_time"]?.ToString(), // Using null conditional operator to prevent NullReferenceException
                    Res_id = Convert.ToInt32(TempData["Res_id"] ?? "1") // Using null coalescing operator to handle null value
                };

                client.BaseAddress = new Uri("http://localhost:52144/API/Admin/AllotVehicle");
                var x = client.PostAsJsonAsync("AllotVehicle?id=2", all);
                x.Wait();
                var test = x.Result;
                if (test.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Vehicle allotted successfully!" });
                }
                // Implement the logic to process the vehicle allotment
                // return View();
                else
                {
                    return Json(new { success = true, message = "Vehicle allocation failed!" });

                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                // return View();
            }
        }

        [HttpPost]
        public JsonResult Allot(int id)
        {
            try
            {
                Allotment all = new Allotment()
                {
                    R_Id = Convert.ToInt32(TempData["Req_id"] ?? "0"),
                    V_Id = id,
                    Date = TempData["Date"]?.ToString() ?? "", // Null-conditional operator (?.) and null-coalescing operator (??)
                    Destination = TempData["Destination"]?.ToString() ?? "",
                    Source = TempData["Source"]?.ToString() ?? "",
                    D_time = TempData["D_time"]?.ToString() ?? "",
                    S_time = TempData["S_time"]?.ToString() ?? "",
                    Res_id = 1 // Assuming this value is constant or fetched from a different source
                };

                client.BaseAddress = new Uri("http://localhost:52144/API/Admin/AllotVehicle");
                var x = client.PostAsJsonAsync("AllotVehicle?id=1", all);
                x.Wait();
                var test = x.Result;
                if (test.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Vehicle allotted successfully!" });
                }
                // Implement the logic to process the vehicle allotment
                // return View();
                else
                {
                    return Json(new { success = true, message = "Vehicle allocation failed!" });

                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                // return View();
            }
        }
        [HttpPost]
        public JsonResult request(Allotment Data)
        {
            try
            {
                TempData["Req_id"] = Data.Allot_id;
                TempData["Date"] = Data.Date;
                TempData["Destination"] = Data.Destination;
                TempData["Source"] = Data.Source;
                TempData["D_time"] = Data.D_time;
                TempData["S_time"] = Data.S_time;
                TempData["For"] = "Car";

                return Json(new { success = true, message = "Vehicle is allotted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                // return View();
            }
        }
        [HttpPost]
        public JsonResult Truckrequest(TruckAllotment Data)
        {
            try
            {
                TempData["Req_id"] = Data.R_Id;
                TempData["Date"] = Data.date;
                TempData["Destination"] = Data.Venue;
                TempData["Source"] = Data.Venue;
                TempData["D_time"] = Data.Time;
                TempData["S_time"] = Data.Time;
                TempData["For"] = "Truck";
                return Json(new { success = true, message = "Vehicle is allotted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                // return View();
            }
        }
        [HttpPost]
        public JsonResult Rejectrequest(Allotment Data)
        {
            try
            {
                TempData["Req_id"] = Data.Allot_id;
                TempData["Date"] = Data.Date;
                TempData["Destination"] = Data.Destination;
                TempData["Source"] = Data.Source;
                TempData["D_time"] = Data.D_time;
                TempData["S_time"] = Data.S_time;
                return Json(new { success = true, message = "Vehicle is allotted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
                // return View();
            }
        }
        [HttpPost]
        public ActionResult ProcessVehicleData(Vehicle v)
        {
            client.BaseAddress = new Uri("http://localhost:52144/API/Adminveh/Addveh");
            // HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            var x = client.PostAsJsonAsync("Addveh", v);
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult ProcessDepartmentData(Department dp)
        {
            client.BaseAddress = new Uri("http://localhost:52144/API/Admindep/AddDep");
            // HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            var x = client.PostAsJsonAsync("AddDep", dp);
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult ProcessFormData(Employee emp)
        {
            client.BaseAddress = new Uri("http://localhost:52144/API/AdminAdd/Insertemp");
            // HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            var x = client.PostAsJsonAsync("Insertemp", emp);
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
            // Process the received data here
            // Example: Save the data to a database

            // Return a response (optional)
            //   return RedirectToAction("Dashboard");

        }

    }
}
