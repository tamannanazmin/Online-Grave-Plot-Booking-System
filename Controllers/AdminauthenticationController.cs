using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GraveBooking.Models;
using GraveBooking.Services.Data;
using GraveBooking.Services.Operation;
using System.Collections.Specialized;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GraveBooking.Controllers
{
    public class AdminauthenticationController : Controller
    {
        // GET: Adminauthentication
        public ActionResult adminLogin()
        {
            NameValueCollection request = Request.Form;
            string adminPhone, adminPassword;
            ViewBag.verification = false;
            if (!string.IsNullOrEmpty(request["adminPhone"]) && !string.IsNullOrEmpty(request["adminPassword"]))
            {
                adminPhone = request["adminPhone"];
                adminPassword = request["adminPassword"];
                admin admin = new admin();
                admin.adminPhone = adminPhone;
                admin.adminPassword = adminPassword;
              

                SecurityServices service = new SecurityServices();
                if (service.AuthenticateAdmin(admin))
                {
                    Session["admin"] = adminPhone;
                    SecurityDAO db = new SecurityDAO();
                   int count= db.loginCount(adminPhone);
                    int adminId = db.getAdminId(adminPhone,adminPassword);
                    Session["count"] = Convert.ToString(count);
                    count++;
                    Session["adminId"] = adminId;
                    db.updateCount(adminPhone,count);
                    
                    return RedirectToAction("adminProfile", "viewGrave");
                    


                }
                else
                {
                    ViewBag.verification = true;
                    return View();
                }
            }


            return View();
        }
        public ActionResult adminLogout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("LoginAs", "Home");
        }
        public ActionResult adminRegistration()
        {

            NameValueCollection request = Request.Form;
            string adminName, adminEmail, adminPhone, adminPassword;
            ViewBag.verification = false;
            if (!string.IsNullOrEmpty(request["adminName"]) && !string.IsNullOrEmpty(request["adminPhone"]) && !string.IsNullOrEmpty(request["adminPassword"]))
            {
                adminName = request["adminName"];
                adminEmail = request["adminEmail"];
                adminPhone = request["adminPhone"];
                adminPassword = request["adminPassword"];


                admin admin = new admin();
                admin.adminName = adminName;
                admin.adminEmail = adminEmail;
                admin.adminPhone = adminPhone;
                admin.adminPassword = adminPassword;
                SecurityDAO db = new SecurityDAO();
                SecurityServices service = new SecurityServices();

                int adminId = db.getAdminId(adminPhone, adminPassword);
                Session["adminId"] = adminId;

                if (service.AuthenticateAdmin(admin))
                {
                    ViewBag.verification = true;
                    return View();
                }
                else
                    db.createAdmin(admin);

                TaskDialog.Show("Admin Profile created successfully.You can sign in now");
                return RedirectToAction("adminLogin", "adminAuthentication");
                //return RedirectToAction("adminProfile", "viewGrave");
            }

            return View();
        }



    }
}