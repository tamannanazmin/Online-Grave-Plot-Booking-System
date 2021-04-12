using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraveBooking.Models;
using GraveBooking.Services.Data;
using System.Collections.Specialized;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GraveBooking.Controllers
{
    public class graveDescriptionController : Controller
    {
        // GET: graveDescription
        
        public ActionResult addDescription()
        {
            return View();
        }
        public ActionResult adminProfile()
        {
            return View();
        }


        [HttpPost]
        public ActionResult addDescription(HttpPostedFileBase image)
        {
            //int adminId = 0;
            if (Session["adminId"] != null)
            {
                // Value is not null, read actual value from session
                int adminId = (int)Session["adminId"];


                NameValueCollection request = Request.Form;
                string graveyardName, authorName, numberOfPlots, plotPrice, contact, availablePlot, location;
                ViewBag.verification = false;
                if (!string.IsNullOrEmpty(request["graveyardName"]) && !string.IsNullOrEmpty(request["authorName"]) && !string.IsNullOrEmpty(request["numberOfPlots"]) && !string.IsNullOrEmpty(request["plotPrice"]) && !string.IsNullOrEmpty(request["contact"]) && !string.IsNullOrEmpty(request["availablePlot"]) && !string.IsNullOrEmpty(request["location"]) && image != null && image.ContentLength > 0)
                {
                    graveyardName = request["graveyardName"];
                    authorName = request["authorName"];
                    numberOfPlots = request["numberOfPlots"];
                    plotPrice = request["plotPrice"];
                    contact = request["contact"];
                    availablePlot = request["availablePlot"];
                    location = request["location"];



                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Media/Images/poster"),
                                                   Path.GetFileName(image.FileName));
                        image.SaveAs(path);

                        graveDescription grave = new graveDescription();
                        grave.graveyardName = graveyardName;
                        grave.authorName = authorName;
                        grave.numberOfPlots = Int32.Parse(numberOfPlots);
                        grave.plotPrice = Int32.Parse(plotPrice);
                        grave.contact = contact;
                        grave.availablePlot = Int32.Parse(availablePlot);
                        grave.location = location;
                        grave.posterImage = "~/Media/Images/poster/" + image.FileName;
                        grave.adminId = adminId;
                        SecurityDAO dao = new SecurityDAO();
                        dao.addgraveDescription(grave);
                        return RedirectToAction("adminProfile", "viewGrave");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                        return Content(ex.Message.ToString());
                    }
                }
                else return View();
            }
            else
            {
                ViewBag.Message = "Your application description page.";
                TaskDialog.Show("Graveyard added successfully");
                return RedirectToAction("adminProfile", "viewGrave");
            }

        }
        
        public ActionResult map()
        {
            return View();
        }
        public ActionResult ReturnToGraveDes()
        {
            return RedirectToAction("addDescription", "graveDescription");
        }

        public ActionResult showOnMap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowOnMap()
        {                
                NameValueCollection request = Request.Form;
               string location;
                ViewBag.verification = false;
            if (!string.IsNullOrEmpty(request["location"]))
            {
                location = request["location"];
                Session["MapLoc"] = location;
                return RedirectToAction("map", "graveDescription");
            }
           
            else
            {
                TaskDialog.Show("Enter a location first");
                return RedirectToAction("showOnMap", "graveDescription");
            }

        }

    }
}