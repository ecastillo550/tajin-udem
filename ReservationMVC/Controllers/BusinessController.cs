using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationModel;
namespace ReservationMVC.Controllers
{
    public class BusinessController : Controller
    {
        //
        // GET: /Business/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ReservationConsult(Business business)
        {
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            int userId = int.Parse(Session["userId"].ToString());
            ViewBag.UserType = (int)Session["userType"];
            List<ReservationModel.Reservation> reservationList = serviceClient.GetAllBusinessReservations(business.businessId);
            business = serviceClient.GetBusiness(business.businessId);
            foreach (ReservationModel.Reservation reservation in reservationList)
            {
                reservation.Business = business;
                reservation.Business.Style = serviceClient.GetStyle(Convert.ToInt32(business.syleId));
                reservation.Client = serviceClient.GetBusinessClient(reservation.cUserId);
                reservation.Client.Account = serviceClient.GetAccount(reservation.Client.userId);
            }
            
            ViewBag.ReservationsList = reservationList;
            return View();
        }

        [HttpPost]
        public ActionResult ReservationConsult(ReservationModel.Reservation reservation)
        {
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            serviceClient.DeleteReservation(reservation.reservationId);
            return RedirectToAction("ReservationConsult", "Client");
        }

        [HttpGet]
        public ActionResult SearchBusiness()
        {
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            int userId = (int)Session["userId"];
            if (Session["userId"] != null)
            {
                ViewBag.Login = "ok";
            }
            ViewBag.UserType = (int)Session["userType"];
            
            List<Business> userBusinessList=serviceClient.GetAllUserBusiness(userId);
            ViewBag.BusinessList = userBusinessList;
            return View();
        }
        [HttpPost]
        public ActionResult SearchBusiness(Business business,string submit)
        {
            
            string sub = Convert.ToString(submit);
            if (Session["userType"] != null)
            {
                ViewBag.Login = (int)Session["userType"];
                if (submit.Equals("Editar restaurante"))
                {
                    int id=business.businessId;
                    Session["BusinessId"] = business.businessId;
                    return RedirectToAction("EditBusiness", "Business");
                }
            }
            return RedirectToAction("ReservationConsult", "Business", business);
        }

        [HttpGet]
        public ActionResult EditBusiness()
        {
            int id = (int)Session["BusinessId"];
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            List<ReservationModel.Style> stylelist = serviceClient.GetAllStyles();
            List<string> pricelist = new List<string>();
            pricelist.Add("-100");
            pricelist.Add("200-300");
            pricelist.Add("300-400");
            pricelist.Add("400-500");
            pricelist.Add("500-700");
            pricelist.Add("700-900");
            pricelist.Add("+900");
            ViewBag.priceList = pricelist;
            ViewBag.StyleList = stylelist;
            Business busines = serviceClient.GetBusiness(id);
            busines.businessId =id;
            busines.Direction = serviceClient.GetDirection(busines.directionId);
            return View(busines);
        }
        [HttpPost]
        public ActionResult EditBusiness(Business business,int? x)
        {
            if (business.priceRange == null)
                business.priceRange = "No definido";
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
           bool edited= serviceClient.EditBusiness(business);
            return RedirectToAction("UserInformation","Client");
        }


    }
}
