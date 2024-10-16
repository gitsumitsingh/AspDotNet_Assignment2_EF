using System.Web.Mvc;
using Assignment2_EF.Models;
using System.Web.Security;
using System.Collections.Generic;
using System;
using System.Web;

namespace Assignment2_EF.Controllers
{
    public class AccountController : Controller
    {
        // Static credentials using Tuple (older syntax)
        private readonly Dictionary<string, Tuple<string, string>> _users = new Dictionary<string, Tuple<string, string>>
            {
                { "admin", new Tuple<string, string>("admin123", "Admin") },
                { "employee", new Tuple<string, string>("emp123", "Employee") }
            };


        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_users.TryGetValue(model.Username, out var user) && user.Item1 == model.Password)
                {
                    // Authentication successful
                    var role = _users[model.Username].Item2;
                    // Create authentication ticket (cookie)
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,                                   // Ticket version
                    model.Username,                       // Username
                    DateTime.Now,                         // Issue time
                    DateTime.Now.AddMinutes(30),          // Expiry time
                    false,                                // Is persistent
                    role,                                      // Store the role in the ticket
                                                          // Store the role in the ticket
                    FormsAuthentication.FormsCookiePath   // Cookie path
                    );
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(cookie);
                    // Redirect based on role
                    if (role == "Admin")
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                    else if (role == "Employee")
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password";
                    return View();
                }

            }

            return View(model);
        }

        // Logout logic
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}
