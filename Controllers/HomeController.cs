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
    public class HomeController : Controller
    {
        private int Id;

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

           // int Id = 0;
            
            NameValueCollection request = Request.Form;
                string name1, contact1, feedback1;
  
                if (!string.IsNullOrEmpty(request["name1"]) && !string.IsNullOrEmpty(request["contact1"]) && !string.IsNullOrEmpty(request["feedback1"]))
                {
                    name1 = request["name1"];
                    contact1 = request["contact1"];
                    feedback1 = request["feedback1"];

                    try
                    {
                       
                        Contact contact = new Contact();
                        contact.name1 = name1;
                        contact.contact1 = contact1;
                        contact.feedback1 = feedback1;
                        
                        SecurityDAO dao = new SecurityDAO();
                        dao.addContact(contact);
                        return RedirectToAction("Contact", "Home");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                        return Content(ex.Message.ToString());
                    }
                }
                else return View();
            
        }

        public ActionResult LoginAs()
        {
            return View();
        }
        public ActionResult UserProfile()
        {
            int userId = 0;
            if (Session["userId"] != null)
            {
                // Value is not null, read actual value from session
                userId = (int)Session["userId"];
            }

            SecurityDAO dAO = new SecurityDAO();
            List<Booking> bookingList = dAO.fetchBookingInfo(userId);
            List<User> UserList = dAO.fetchUser(userId);
            ViewModel allmodel = new ViewModel();
            allmodel.Bookings = bookingList;
            allmodel.Users = UserList;
            return View(allmodel);
        }
            
        [HttpPost]
        public ActionResult UserProfile(FormCollection form)
        {
            int userId = 0;
            if (Session["userId"] != null)
            {
                // Value is not null, read actual value from session
                userId = (int)Session["userId"];

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


                    User user = new User();
                    user.userName = name;
                    user.email = email;
                    user.phone = phone;
                    user.password = password;
                    SecurityDAO db = new SecurityDAO();
                    db.updateUser(user,userId);
                    //ViewBag.Message = "Profile updated successfully";
                    TaskDialog.Show("Updated successfully");
                    return RedirectToAction("UserProfile", "Home");
                }

            }
            return View();
        }

    }
}