using ReservationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationMVC.Controllers
{
    public class ClientController : Controller
    {
        //
        // GET: /Client/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SearchBusiness()
        {
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            int styleId=int.Parse(Request.QueryString["styleId"]);
            if (Session["userId"] != null)
            {
                ViewBag.Login = "login";
            }
            ViewBag.UserType = (int)Session["userType"];
            Style style = serviceClient.GetStyle(styleId);
            ViewBag.StyleName = style.style1;
            ViewBag.BusinessList = serviceClient.GetAllBusiness(styleId, -1);
            return View();
        }
        [HttpPost]
        public ActionResult SearchBusiness(Business business)
        {
            if (Session["userType"] != null)
            {
                ViewBag.Login = (int)Session["userType"];
            }
          return RedirectToAction("CreateReservation", "Client", business);
        }
        public ActionResult UserInformation()
        {
            ResevationService.Service1 serviceClient= new ResevationService.Service1();
            if (Session["userId"] != null)
            {
                ViewBag.Login = "login";
                ViewBag.UserType = (int)Session["userType"];
            }
            int userId = int.Parse(Session["userId"].ToString());
            Client client = serviceClient.GetClient(userId);
            client.Account = serviceClient.GetAccount(userId);
            return View(client);
        }
        
        [HttpGet]
        public ActionResult UserEdit()
        {
            int userId = int.Parse(Session["userId"].ToString());
            if (Session["userId"] != null)
            {
                ViewBag.Login = "login";
                ViewBag.UserType = (int)Session["userType"];
            }
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            Client client = serviceClient.GetClient(userId);
            client.Account = serviceClient.GetAccount(userId);
            return View(client);
            
        }
        [HttpPost]
        public ActionResult UserEdit(Client client)
        {

            client.userId = int.Parse(Session["userId"].ToString());
            client.Account.userId = client.userId;
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            bool transaction= serviceClient.EditClientAccount(client);
            return RedirectToAction("UserInformation","Client");

        }
        [HttpGet]
        public ActionResult CreateReservation(Business business)
        {
            if (Session["userId"] != null)
            {
                ViewBag.Login = "login";
            }
            if (Session["userType"] != null)
            {
                ViewBag.UserType = (int)Session["userType"];
            }
            int businessid = business.businessId;
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            business = serviceClient.GetBusiness(business.businessId);
            business.businessId = businessid;
            business.Direction = serviceClient.GetDirection(business.directionId);
            business.Style = serviceClient.GetStyle(business.businessId);
            
            return View(business);
        }
        [HttpPost]
        public ActionResult CreateReservation(Business business,int? id)
        {
            int idb = business.businessId;

            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            Business business2 = serviceClient.GetBusiness(business.businessId);
            business.numSpaces = business2.numSpaces;
           
            if (business.numSpaces > 0)
            {
                Reservation reservation = new Reservation();
                try
                {
                    int accoutId = (int)Session["userId"];
                    Client client = serviceClient.GetClient(accoutId);
                    reservation.cUserId = client.clientId;
                }
                catch (Exception)
                {
                    ViewBag.ErrorNoLogin = "Se necesita estar registrado para crear una reservacion";
                    return View();
                }
                reservation.bUserId = business.businessId;
                reservation.people = business.people;
                reservation.rDay = business.rDay;
                reservation.rTime = business.rTime;
                serviceClient.CreateReservation(reservation);
                return RedirectToAction("SuccessReservation","Client");
            }
            else 
                return RedirectToAction("Index", "Home");
        }

        public ActionResult SuccessReservation()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ReservationConsult()
        {
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            int userId = int.Parse(Session["userId"].ToString());
            ViewBag.UserType = (int)Session["userType"];
            Client client = serviceClient.GetClient(userId);
            List<ReservationModel.Reservation> reservationList = serviceClient.GetAllClientReservations(client.clientId);
            foreach (ReservationModel.Reservation reservation in reservationList)
            {
                reservation.Business = serviceClient.GetBusiness(reservation.bUserId);
                reservation.Business.Style = serviceClient.GetStyle(Convert.ToInt32(reservation.Business.syleId));
            }
            ViewBag.ReservationsList = reservationList;
            return View();
        }

        [HttpPost]
        public ActionResult ReservationConsult(ReservationModel.Reservation reservation)
        {
            ResevationService.Service1 serviceClient = new ResevationService.Service1();
            serviceClient.DeleteReservation(reservation.reservationId);
            return RedirectToAction("ReservationConsult","Client");
        }
    }
}
