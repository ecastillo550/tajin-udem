using ReservationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel;
using System.Web.UI.WebControls;
namespace ReservationMVC.Controllers
{
    public class CuentasController : Controller
    {
        
        //
        // GET: /Cuentas/
        [HttpGet]
        public ActionResult SignIn()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(FormCollection form)
        {
            Account account = new Account();
            ResevationService.Service1 client = new ResevationService.Service1();
            account.userName= form["userName"].ToString();
            account.mail= form["mail"].ToString();
            account.password = form["password"].ToString();
            string type = form["typeOption"].ToString();
            List<String> userNames = client.GetAllUserNames();
            foreach (string name in userNames)
            {
                if (account.userName.Equals(name))
                {
                    ViewBag.Error = "Porfavor intente otro nombre de usuario";
                return View();
                }
            } 
            if (type.Equals("client"))
            {
                account.userTypeId = 1;
                int accId = client.CreateAccount(account);
                Session["accountId"] = accId;
                return RedirectToAction("SignIn_Client", "Cuentas");
            }
            else
            {
                account.userTypeId = 2;
                int accId = client.CreateAccount(account);
                Session["accountId"] = accId;
                return RedirectToAction("SignIn_Business", "Cuentas");

            }
           
        }
        [HttpGet]
        public ActionResult SignIn_Client()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn_Client(Client clientr)
        {
            ResevationService.Service1 client = new ResevationService.Service1();
            clientr.userId = (int)Session["accountId"];
            bool created=client.CreateClientAccount(clientr);
            if (created == true)
                return RedirectToAction("Login", "Cuentas");
            else
                return View();
        }
       
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignIn_Business()
        {

             ResevationService.Service1 client = new ResevationService.Service1();
           List<ReservationModel.Style> stylelist=client.GetAllStyles();
            List<string> pricelist= new List<string>();
            pricelist.Add("-100");
            pricelist.Add("200-300");
            pricelist.Add("300-400");
            pricelist.Add("400-500");
            pricelist.Add("500-700");
            pricelist.Add("700-900");
            pricelist.Add("+900");
           ViewBag.priceList = pricelist;
            ViewBag.StyleList = stylelist;
            return View();
        }

        [HttpPost]
        public ActionResult SignIn_Business(Business business)
        {
            ResevationService.Service1 client = new ResevationService.Service1();
            //int userId = 3;
                //(int)Session["userId"];
            int directionId = client.CreateDirection(business.Direction);
            business.userId = (int)Session["userId"];
            business.directionId = directionId;
            bool created= client.CreateBusinessAccount(business);
            Session["userType"] = 2;
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            ResevationService.Service1 client = new ResevationService.Service1();
            string userName = form["userName"].ToString();
            string password = form["password"].ToString();
            if (userName!=null&&password!=null)
            {
                try
                {

                    Account account = client.Login(password, userName);

                    if (account.userId > 0)
                    {
                        if (account.userTypeId == 2)
                        {
                            //2 business
                            Session["userId"] = account.userId;
                            Session["userType"] = account.userTypeId;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            //1 client
                            Session["userId"] = account.userId;
                            Session["userType"] = account.userTypeId;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorLogin= "El usuario o la contraseña estan equivocado";
                        return View();
                    }
                }
                catch (FaultException faultEx)
                {
                    Literal ltr = new Literal();
                    ltr.Text = @"<script type='text/javascript'> alert('" + faultEx.Message + "') </script>";
                    Response.Write(ltr);
                    return RedirectToAction("Login", "Cuentas");
                }
            }
            else
                return RedirectToAction("Login", "Cuentas");
        }
        [HttpPost]
        public ActionResult UserEdit(Client clien)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    return RedirectToAction("Login", "Cuentas");
                }
                catch (FaultException faultEx)
                {
                    Literal ltr = new Literal();
                    ltr.Text = @"<script type='text/javascript'> alert('" + faultEx.Message + "') </script>";
                    Response.Write(ltr);
                    return RedirectToAction("Login", "Cuentas");
                }
            }
            else
                return RedirectToAction("Login", "Cuentas");

        }

        public ActionResult LogOut()
        {
            Session.Remove("userId");
            return RedirectToAction("Index", "Home");
        }
        }
        
     

    }
