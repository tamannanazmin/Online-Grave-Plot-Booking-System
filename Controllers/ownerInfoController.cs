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
    public class ownerInfoController : Controller
    {
        List<int> unbookedPlotList = new List<int>();

        // GET: ownerInfo
        public ActionResult addOwnerInfo()
        {
            SecurityDAO dAO = new SecurityDAO();
            int gId = 0;
            if (Session["graveyardId"] != null)
            {
                // Value is not null, read actual value from session
                gId = (int)Session["graveyardId"];
            }
            dAO.DeleteTempData();
            var list1 = new List<string>();
            list1 = dAO.fetchBookedPlots(gId);
            dAO.addUnbookedPlots(list1);
            List<Temp> plotList = dAO.showUnbookedPlots();
            //List<graveDescription> grave = dAO.fetchgrave(gId);

            ViewModel allmodel = new ViewModel();
           
            allmodel.Temps = plotList;
            //allmodel.graveDescriptions = grave;
            return View(allmodel);
        }
        public ActionResult choosePlot()
        {
            /*string id = Request.Url.Segments.Last();
            int price = Convert.ToInt32(id);
            Session["plotPrice"] = price;*/
           
            int userId = 0;
            if (Session["userId"] != null)
            {
                // Value is not null, read actual value from session
                userId = (int)Session["userId"];
                SecurityDAO dAO = new SecurityDAO();
                bool check = dAO.IfAlreadyAnOwner(userId);
                if (check == true) { 
                    return RedirectToAction("ShowExistingOwnerInfo", "ownerInfo"); }
                else {
                    return RedirectToAction("addOwnerInfo", "ownerInfo");
                }

            }
            else
            {
                TaskDialog.Show("Please login first");
                return RedirectToAction("Login", "Authentication");
            }
           
        }
        public ActionResult ShowExistingOwnerInfo()
        {
            int gId = 0;
            
            if (Session["graveyardId"] != null)
            {
                // Value is not null, read actual value from session
                gId = (int)Session["graveyardId"];
               
            }
            int userId = 0;
            if (Session["userId"] != null)
            {
                // Value is not null, read actual value from session
                userId = (int)Session["userId"];
            }
            SecurityDAO dAO = new SecurityDAO();
            List<owner> ownerInfo = dAO.fetchOwnerInfoAsPerUserId(userId);
            List<graveDescription> grave = dAO.fetchgrave(userId);

            ViewModel allmodel = new ViewModel();
            allmodel.owners = ownerInfo;
            allmodel.graveDescriptions = grave;

            return View(allmodel);

        }
        public ActionResult addInfoToExistingOwner()
        {
            SecurityDAO dao = new SecurityDAO();
            int gId = 0;
            int adminId = 0;
            if (Session["graveyardId"] != null)
            {
                // Value is not null, read actual value from session
                gId = (int)Session["graveyardId"];
                adminId = dao.getAdminIdFromGrave(gId);
            }
            int userId = 0;
            if (Session["userId"] != null)
            {
                // Value is not null, read actual value from session
                userId = (int)Session["userId"];
            }
                NameValueCollection request = Request.Form;
            string bankName, branchName, branchAddress, dd;
            string gender, name, fName, mName, profession, phone, email, add, nid, dob;
            name = dao.getOwnerName(userId);
            gender = dao.getOwnerGender(userId);
            fName = dao.getOwnerFatherName(userId);
            mName = dao.getOwnerMotherName(userId);
            profession = dao.getOwnerProfession(userId);
            phone = dao.getOwnerPhone(userId);
            email = dao.getOwnerEmail(userId);
            add = dao.getOwnerAddress(userId);
            
            nid = dao.getOwnerNid(userId);
            dob = dao.getOwnerDateOfBirth(userId);
            ViewBag.verification = false;
            //ViewBag.verification = true;
            if (!string.IsNullOrEmpty(request["bankName"]) &&
                !string.IsNullOrEmpty(request["branchName"]) &&
                !string.IsNullOrEmpty(request["branchAddress"]) &&
                !string.IsNullOrEmpty(request["dd"]))
            {
                bankName = request["bankName"];
                branchName = request["branchName"];
                branchAddress = request["branchAddress"];
                dd = request["dd"];
                int y = -1;  //the plotno initially
                int x = dao.getPlotPrice(gId);
                try
                {
                    owner ow = new owner();
                    Booking b = new Booking();
                    ow.fullName = name;
                    ow.gender = gender;
                    ow.fatherName = fName;
                    ow.motherName = mName;
                    ow.profession = profession;
                    ow.phone = phone;
                    ow.email = email;
                    ow.address = add;
                    //ow.photo = "~/Media/Images/owner/" + image.FileName;
                    ow.nid = nid;
                    ow.dateOfBirth = dob;
                    ow.plotId = y;
                    ow.totalPrice = x;
                    ow.bankName = bankName;
                    ow.branchName = branchName;
                    ow.branchAddress = branchAddress;
                    ow.dd = dd;
                    ow.adminId = adminId;
                    ow.userId = userId;
                    ow.graveyardId = gId;

                    b.OwnerId = "null";
                    b.GraveyardId = gId;
                    b.userId = userId;
                    b.GravePlotId = y;
                    //b.GravePlotId = -1;
                    b.Date = DateTime.Today.ToString();

                    dao.addOwner(ow);
                    dao.addBooking(b);
                  
                    return RedirectToAction("OwnerInfoIntermediate", "viewGrave");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    //return Content(ex.Message.ToString());
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else return View();
        }
          

        [HttpPost]
        public ActionResult addOwnerInfo(HttpPostedFileBase image)
        {
            SecurityDAO dao = new SecurityDAO();
 
            int gId = 0;
            int adminId = 0;
            if (Session["graveyardId"] != null)
            {
                // Value is not null, read actual value from session
                gId = (int)Session["graveyardId"];
                adminId = dao.getAdminIdFromGrave(gId);
            }

            int userId = 0;
            if (Session["userId"] != null)
            {
                // Value is not null, read actual value from session
                userId = (int)Session["userId"];
                bool check = dao.IfAlreadyAnOwner(userId);
                if (check == false)
                {
                    //string uId = Convert.ToString(userId);
                    NameValueCollection request = Request.Form;
                    string fullName, gender, fatherName, motherName;
                    string profession, phone, email, address, photo, nid;
                    string dateOfBirth,plotId, totalPrice;
                    string bankName, branchName, branchAddress, dd;

                    ViewBag.verification = false;
                    //ViewBag.verification = true;
                    if (!string.IsNullOrEmpty(request["fullName"]) &&
                        !string.IsNullOrEmpty(request["gender"]) &&
                        !string.IsNullOrEmpty(request["fatherName"]) &&
                         !string.IsNullOrEmpty(request["motherName"]) &&
                        !string.IsNullOrEmpty(request["profession"]) &&
                        !string.IsNullOrEmpty(request["phone"]) &&
                        !string.IsNullOrEmpty(request["email"]) &&
                        !string.IsNullOrEmpty(request["address"]) &&
                        image != null && image.ContentLength > 0 &&
                        !string.IsNullOrEmpty(request["nid"]) &&
                        !string.IsNullOrEmpty(request["dateOfBirth"]) &&
                        /*!string.IsNullOrEmpty(request["plotId"]) && */
                       /* !string.IsNullOrEmpty(request["totalPrice"]) &&*/
                        !string.IsNullOrEmpty(request["bankName"]) &&
                        !string.IsNullOrEmpty(request["branchName"]) &&
                        !string.IsNullOrEmpty(request["branchAddress"]) &&
                        !string.IsNullOrEmpty(request["dd"]))
                    {
                        fullName = request["fullName"];
                        gender = request["gender"];
                        fatherName = request["fatherName"];
                        motherName = request["motherName"];
                        profession = request["profession"];
                        phone = request["phone"];
                        email = request["email"];
                        address = request["address"];

                        try
                        {
                            string path = Path.Combine(Server.MapPath("~/Media/Images/owner"),
                                                       Path.GetFileName(image.FileName));
                            image.SaveAs(path);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = "ERROR:" + ex.Message.ToString();
                            return Content(ex.Message.ToString());
                        }

                        nid = request["nid"];
                        dateOfBirth = request["dateOfBirth"];
                        //plotId = request["plotId"];
                        //totalPrice = request["totalPrice"];
                        //System.Diagnostics.WriteLine(totalPrice);


                        bankName = request["bankName"];
                        branchName = request["branchName"];
                        branchAddress = request["branchAddress"];
                        dd = request["dd"];

                        int y = -1;
                        
                        int x = dao.getPlotPrice(gId);
                      
                        try
                        {
                            owner ow = new owner();
                            Booking b = new Booking();
                            ow.fullName = fullName;
                            ow.gender = gender;
                            ow.fatherName = fatherName;
                            ow.motherName = motherName;
                            ow.profession = profession;
                            ow.phone = phone;
                            ow.email = email;
                            ow.address = address;
                            ow.photo = "~/Media/Images/owner/" + image.FileName;
                            ow.nid = nid;
                            ow.dateOfBirth = dateOfBirth;
                            //ow.plotId = Int32.Parse(plotId);
                            //ow.totalPrice = Int32.Parse(totalPrice);
                            ow.plotId = y;
                            ow.totalPrice = x;
                            ow.bankName = bankName;
                            ow.branchName = branchName;
                            ow.branchAddress = branchAddress;
                            ow.dd = dd;
                            ow.adminId = adminId;
                            ow.userId = userId;
                            ow.graveyardId = gId;

                            b.OwnerId = "null";
                            b.GraveyardId = gId;
                            b.userId = userId;
                            b.GravePlotId = y;
                            //b.GravePlotId = -1;
                            b.Date = DateTime.Today.ToString();

                            dao.addOwner(ow);
                            dao.addBooking(b);
                            //int available = dao.getAvailableNoOfPlots(gId);
                            //int cnt = dao.getPlotCount(gId);
                            //dao.updateAvailablePlots(gId, cnt, available);

                            return RedirectToAction("OwnerInfoIntermediate", "viewGrave");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = "ERROR:" + ex.Message.ToString();
                            return Content(ex.Message.ToString());
                            //return RedirectToAction("Login", "Authentication");
                        }
                    }
                     else return View();
                    
                }
                else
                {
                    return RedirectToAction("addInfoToExistingOwner", "ownerInfo");

                }
            }
            else
            {
                ViewBag.Message = "Please login first";
                return RedirectToAction("Login", "Authentication");


            }
        }

       
    }
}