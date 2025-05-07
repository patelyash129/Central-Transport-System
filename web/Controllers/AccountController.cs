using Models2.DB;
using Models2.Employee;
using Models2.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace web.Controllers
{

    public class AccountController : Controller
    {
        // GET: Account
        HttpClient client = new HttpClient();
        public PartialViewResult Login()
        {
            return PartialView("_Login");
        }
        public PartialViewResult Signup()
        {
            return PartialView("_Signup");
        }
        [HttpPost]
         public ActionResult LoginData(String z, String y)
        {
            Signup login = new Signup()
            {
                password = y,
                email = z
            };
            client.BaseAddress = new Uri("http://localhost:52144/API/Account/Register");
            // HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            var x = client.PostAsJsonAsync("Register?id=2", login);
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                var response = test.Content.ReadAsStringAsync().Result;
                string k = response.ToString().Trim('"');
                if (k=="")
                {
                   // return Json(new { success = true, message = "Login failed!" });

                    return View("Login", "Account");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(k, false);
                   // return Json(new { success = true, message = "Login successfully!" });

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
               // return Json(new { success = true, message = "Login Exception!" });

                return View("Login");
            }

        }
        [HttpPost]
        public ActionResult Register(Signup signup)
        {
            client.BaseAddress = new Uri("http://localhost:52144/API/Account/Register");
            var x = client.PostAsJsonAsync("Register?id=1", signup);
            x.Wait();
            var test = x.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View("Signup");
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}