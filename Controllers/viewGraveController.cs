using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraveBooking.Models;
using GraveBooking.Services.Data;
using System.Collections.Specialized;
using System.Dynamic;
using System.IO;

namespace GraveBooking.Controllers
{
    public class viewGraveController : Controller
    {
        // GET: viewGrave
        public ActionResult viewGravePost()
        {
            SecurityDAO dAO = new SecurityDAO();
            List<graveDescription> graveList = dAO.fetchgrave();
            List<admin> adminList = dAO.fetchAdmin();
            ViewModel allmodel = new ViewModel();
            allmodel.graveDescriptions = graveList;
            allmodel.admins = adminList;

            NameValueCollection request = Request.Form;
            string search;

            if (!string.IsNullOrEmpty(request["search"]))
            {
                search = request["search"];
                graveList = dAO.fetchgrave(search);
                allmodel.graveDescriptions = graveList;
                allmodel.admins = adminList;
                return View(allmodel);
            }
            return View(allmodel);
        }
        public ActionResult DisplayGraveTable()
        {
            int gId = 0;
            if (Session["adminId"] != null)
            {
                // Value is not null, read actual value from session
                gId = (int)Session["adminId"];
            }
            SecurityDAO dAO = new SecurityDAO();
            List<graveDescription> graveList = dAO.fetchgraveAsPerAdminId(gId);
            
            ViewModel allmodel = new ViewModel();
            allmodel.graveDescriptions = graveList;
            
            return View(allmodel);
        }
        public ActionResult adminProfile()
        {
            int adminId = 0;
            if (Session["adminId"] != null)
            {
                // Value is not null, read actual value from session
                adminId = (int)Session["adminId"];
            }

            SecurityDAO dAO = new SecurityDAO();
            List<graveDescription> graveList = dAO.fetchgrave();
            List<admin> adminList = dAO.fetchAdmin(adminId);
            ViewModel allmodel = new ViewModel();
            allmodel.graveDescriptions = graveList;
            allmodel.admins = adminList;

            NameValueCollection request = Request.Form;
            string search;

            if (!string.IsNullOrEmpty(request["search"]))
            {
                search = request["search"];
                graveList = dAO.fetchgrave(search);
                allmodel.graveDescriptions = graveList;
                allmodel.admins = adminList;
                return View(allmodel);
            }
            
            return View(allmodel);
        }

        public ActionResult detailsPage()
        {
            string id = Request.Url.Segments.Last();
            int gId = Convert.ToInt32(id);
         /*   if (Session["graveyardId"] != null)
            {
                // Value is not null, read actual value from session
                gId = (int)Session["graveyardId"];
            }*/
            SecurityDAO dAO = new SecurityDAO();
            dAO.DeleteTempData();
            var list1 = new List<string>();
            list1 = dAO.fetchBookedPlots(gId);
            dAO.addUnbookedPlots(list1);
            List<graveDescription> graveList = dAO.fetchgrave(gId);
            
            ViewModel allmodel = new ViewModel();
            allmodel.graveDescriptions = graveList;

            Session["graveyardId"] = gId;
            return View(allmodel);
        }

        public ActionResult OwnerInfoIntermediate()
        {
            int gId = 0;
            if (Session["graveyardId"] != null)
            {
                // Value is not null, read actual value from session
                gId = (int)Session["graveyardId"];
            }
            SecurityDAO dAO = new SecurityDAO();
            dAO.DeleteTempData();
            var list1 = new List<string>();
            list1 = dAO.fetchBookedPlots(gId);
            dAO.addUnbookedPlots(list1);
            List<Temp> plotList = dAO.showUnbookedPlots();
            ViewModel allmodel = new ViewModel();
            allmodel.Temps = plotList;
            return View(allmodel);

        }
        public ActionResult updatePlotNo()
        {
            int gId = 0;
            if (Session["graveyardId"] != null)
            {
                // Value is not null, read actual value from session
                gId = (int)Session["graveyardId"];
            }
            
            string id = Request.Url.Segments.Last();
            int pId = Convert.ToInt32(id);
            SecurityDAO dao = new SecurityDAO();
           
            int available = dao.getAvailableNoOfPlots(gId);
            int cnt = dao.getPlotCount(gId);
            dao.updateAvailablePlots(gId, cnt, available);
            dao.updatePlotNoInBooking(pId);
            dao.updatePlotNoInOwner(pId);

            return View();


        }

    }
}