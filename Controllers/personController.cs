using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraveBooking.Controllers
{
    public class personController : Controller
    {
        // GET: person
        public ActionResult personProfile()
        {
            return View();
        }
       
    }
}