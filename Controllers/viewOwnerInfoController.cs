using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraveBooking.Models;
using GraveBooking.Services.Data;
using System.Collections.Specialized;
using System.Dynamic;

namespace GraveBooking.Controllers
{
    public class viewOwnerInfoController : Controller
    {
        // GET: viewOwnerInfo
        public ActionResult viewOwnerInfo()
        {
            int adminId = 0;
            if (Session["adminId"] != null)
            {
                // Value is not null, read actual value from session
                adminId = (int)Session["adminId"];

                SecurityDAO dAO = new SecurityDAO();
                List<owner> ownerViewList = dAO.fetchOwnerInfo(adminId);
                //List<admin> adminList = dAO.fetchAdmin(adminId);


                ViewModel allmodel = new ViewModel();
                allmodel.owners = ownerViewList;
                //allmodel.admins = adminList;


                NameValueCollection request = Request.Form;
                string search;

                if (!string.IsNullOrEmpty(request["searchOwner"]))
                {
                    search = request["searchOwner"];
                    ownerViewList = dAO.fetchOwnerInfo(search);
                    allmodel.owners = ownerViewList;
                    //allmodel.admins = adminList;
                    return View(allmodel);
                }




                return View(allmodel);
            }
            return View();
        }
    }
}