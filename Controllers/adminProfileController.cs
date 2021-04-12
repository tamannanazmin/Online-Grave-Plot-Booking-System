using GraveBooking.Models;
using GraveBooking.Services.Data;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraveBooking.Controllers
{
    public class adminProfileController : Controller
    {
        // GET: adminProfile
        public ActionResult updateAdminProfile()
        {
            int adminId = 0;
            if (Session["adminId"] != null)
            {
                // Value is not null, read actual value from session
                adminId = (int)Session["adminId"];
            }

            SecurityDAO dAO = new SecurityDAO();
            List<admin> adminList = dAO.fetchAdmin(adminId);
            ViewModel allmodel = new ViewModel();
            
            allmodel.admins = adminList;

            return View(allmodel);
        }

        [HttpPost]
        public ActionResult updateAdminProfile(FormCollection form)
        {
            int userId = 0;
            if (Session["adminId"] != null)
            {
                // Value is not null, read actual value from session
                userId = (int)Session["adminId"];

                SecurityDAO dAO = new SecurityDAO();


                NameValueCollection request = Request.Form;
                string name, email, phone, password;
                ViewBag.verification = false;
                if (!string.IsNullOrEmpty(request["username"]) && !string.IsNullOrEmpty(request["phone"]) && !string.IsNullOrEmpty(request["email"]))
                {
                    name = request["username"];
                    email = request["email"];
                    phone = request["phone"];
                    password = request["password"];


                    admin admin = new admin();
                    admin.adminName = name;
                    admin.adminEmail = email;
                    admin.adminPhone = phone;
                    admin.adminPassword = password;
                    SecurityDAO db = new SecurityDAO();
                    db.updateAdmin(admin, userId);
                    //ViewBag.Message = "Profile updated successfully";
                    TaskDialog.Show("Updated successfully");
                    return RedirectToAction("updateAdminProfile", "adminProfile");
                }

            }
            return View();
        }

    }
}