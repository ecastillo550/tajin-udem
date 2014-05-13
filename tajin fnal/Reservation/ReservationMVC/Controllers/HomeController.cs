using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationMVC;
using System.ServiceModel;
using ReservationModel;
namespace ReservationMVC.Controllers
{
    public class HomeController : Controller
    {
        ResevationService.Service1 client = new ResevationService.Service1();
        
        [HttpGet]
        
        public ActionResult Index()
        {
            if(Session["userId"]!=null){
                ViewBag.Login = "login";
            }
            ViewBag.Message = "";
            return View();
        }

        
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {   
            string searchText = form["searchTxt"].ToString();
            if (searchText != null) {
                Business business = client.SearchBusiness(searchText);
                if(business!=null)
                    return RedirectToAction("BusinessConsult", "Home", business);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult BusinessConsult(Business business)
        {
            
            //Business business = client.GetBusiness(bid);
            if (Session["userId"] != null)
            {
                ViewBag.Login = "login";
            }
            Direction direction = client.GetDirection(business.directionId);
            business.Direction = direction;
            int id = Convert.ToInt32(business.syleId);
            business.Style=client.GetStyle(id);
            return View(business);
        }
        [HttpPost]
        public ActionResult BusinessConsult(Business business,int? x)
        {
            return RedirectToAction("CreateReservation", "Client", business);
        }
    }
}
