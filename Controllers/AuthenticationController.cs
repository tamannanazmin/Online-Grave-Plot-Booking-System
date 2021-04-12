using GraveBooking.Models;
using GraveBooking.Services.Data;
using GraveBooking.Services.Operation;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraveBooking.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            NameValueCollection request = Request.Form;
            string phone, password;
            ViewBag.verification = false;
            if (!string.IsNullOrEmpty(request["phone"]) && !string.IsNullOrEmpty(request["password"]))
            {
                phone = request["phone"];
                password = request["password"];
                User user = new User();
                user.phone = phone;
                user.password = password;
                SecurityServices service = new SecurityServices();
                if (service.Authenticate(user))
                {

                    Session["Users"] = phone;
                    SecurityDAO db = new SecurityDAO();
                    int userId = db.getUserId(phone, password);
                    Session["userId"] = userId;
                    return RedirectToAction("UserProfile", "Home");

                }
                else
                {
                    ViewBag.verification = true;

                    return View();
                }
            }


            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("LoginAs", "Home");
        }
        public ActionResult Registration()
        {

            NameValueCollection request = Request.Form;
            string name, email, phone, password;
            ViewBag.verification = false;
            if (!string.IsNullOrEmpty(request["username"]) && !string.IsNullOrEmpty(request["phone"]) && !string.IsNullOrEmpty(request["password"]))
            {
                name = request["username"];
                email = request["email"];
                phone = request["phone"];
                password = request["password"];


                User user = new User();
                user.userName = name;
                user.email = email;
                user.phone = phone;
                user.password = password;
                SecurityDAO db = new SecurityDAO();
                SecurityServices service = new SecurityServices();
                if (service.Authenticate(user))
                {
                    ViewBag.verification = true;
                    //return View();
                    return RedirectToAction("UserProfile", "Home");
                }
                else
                    db.createUser(user);
                //return View();
                TaskDialog.Show("User Profile created successfully.Please Login");
                return RedirectToAction("Login", "Authentication");
            }

            return View();
        }
        public ActionResult EditProfileView()
        {
            return View();
        }

    }
}
