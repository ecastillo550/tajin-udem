using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationModel;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Reservation.Test;
using ResevationService;

namespace Reservation.Test
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void TestGetAllBusiness()
        {


            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            Business[] actual = service.GetAllBusiness(-1, -1);
            int expected = actual.Length;
            Assert.AreNotEqual(0, expected, "Didnt get business from query");

        }
        [TestMethod]
        public void GetAccount()
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            Account account=service.GetAccount(3);
            Assert.AreNotEqual(null, account, "Didn't get an account");
        }
        [TestMethod]
        public void GetReservation()
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client(); ;
            ReservationModel.Reservation reservation = service.GetReservation(1);
            Assert.AreNotEqual(null,reservation , "Didn't get a reservation");
        }
        [TestMethod]
        public void CreateMessage()
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            Message message = new Message();
            message.bUserId = 1;
            message.cUserId = 1;
            message.mDay = DateTime.Now;
            message.messageText = "Test message";
            message.mTime = DateTime.Now.TimeOfDay;
            bool created=service.CreateMessage(message);
            Assert.AreEqual(true, created, "Didn't create message");
        }
        [TestMethod]
        public void GetMessage()
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            Message message = service.GetMessage(1);
            Assert.AreNotEqual(null, message, "Didn't get a message");
        }
        [TestMethod]
        public void DeleteMessage()
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            Message message = new Message();
            message.messageId = 1;
            bool test = service.DeleteMessage(message);
            Assert.AreNotEqual(false,test , "Didn't delete message");
        }
        [TestMethod]
        public void GetClient()
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            Client client = service.GetClient(3);   
            Assert.AreNotEqual(null, client, "Didn't get a client");
        }
        [TestMethod]
        public void GetStyle()
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            Style style = service.GetStyle(1);
            Assert.AreNotEqual(null, style, "Didn't get a style");
        }
        [TestMethod]
        public void GetDirection()
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            Direction direction = service.GetDirection(1);
            Assert.AreNotEqual(null, direction, "Didn't get a client");
        }
        [TestMethod]
        public void GetBusiness()
        {
            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            Business business = service.GetBusiness(3);
            Assert.AreNotEqual(null, business, "Didn't get a business");
        }
        [TestMethod]
        public void CreateAccount()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Account account = new Account();
            account.mail = "test";
            account.password = "test";
            account.userName = "testmss";
            account.userTypeId = 1;
           int created=client.CreateAccount(account);
           

            Assert.AreNotEqual(null, created, "Didn't create account");
        }
        [TestMethod]
        public void CreateReservation()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            ReservationModel.Reservation reservation = new ReservationModel.Reservation();
            reservation.bUserId = 1;
            reservation.cUserId = 1;
            reservation.people = 4;
            reservation.rDay = DateTime.Now;
            reservation.rTime = DateTime.Now.TimeOfDay;
            bool created=client.CreateReservation(reservation);
            Assert.AreNotEqual(null, created, "Didn't create reservation");
        }
        [TestMethod]
        public void EndReservation()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            bool created = client.EndReservation(4);
            Assert.AreNotEqual(null, created, "Didn't create reservation");
        }
        [TestMethod]
        public void DeleteReservation()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            bool deleted = client.DeleteReservation(1);
            Assert.AreNotEqual(null, deleted, "Didn't delted reservation");
        }
        [TestMethod]
        public void Login()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Account account=client.Login("122", "Daniel");
            Assert.AreNotEqual(null,account, "Didn't make login");

        }
        [TestMethod]
        public void GetAllStyles()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Style[] styles = client.GetAllStyles();
            Assert.AreNotEqual(null, styles, "Didn't get all styles");
        }
        [TestMethod]
        public void CreateClientAccount()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Client cliet = new Client();
            cliet.firstName = "test";
            cliet.mLastName = "test";
            cliet.pLastName = "test";
            cliet.telephone = "812893";
            cliet.userId = 3;
            bool created=client.CreateClientAccount(cliet);
            Assert.AreNotEqual(null, created, "Didn't create client account");
            
            
        }
        [TestMethod]
        public void EditClientAccount()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Client cliet = new Client();
            cliet.clientId = 3;
            cliet.firstName = "test";
            cliet.mLastName = "test";
            cliet.pLastName = "test";
            cliet.telephone = "812893";
            cliet.userId = 3;
            cliet.Account = client.GetAccount(3);
            bool edited = client.EditClientAccount(cliet);
            Assert.AreNotEqual(null, edited, "Didn't create client account");   
        }
        [TestMethod]
        public void CreateBusinessAccount()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Business business = new Business();
            business.userId = 3;
            business.businessName = "Test";
            business.telephone = "test";
            business.description = "test";
            business.syleId = 1;
            business.numSpaces = 30;
            business.mail = "test";
            business.priceRange = "test";
            business.directionId = 1;
            bool created=client.CreateBusinessAccount(business);
            Assert.AreNotEqual(null, created, "Didn't create business account");   
        }
        [TestMethod]
        public void CreateDirection()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Direction direction = new Direction();
            direction.city = "test";
            direction.colony = "test";
            direction.number = "test";
            direction.postalCod = 123;
            direction.street = "test";
            int created=client.CreateDirection(direction);
            Assert.AreNotEqual(null, created, "Didn't create Direction");   
        }
        [TestMethod]
        public void EditAccount()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Account account = new Account();
            account.userId = 7;
            account.password = "test";
            account.mail = "test";
            bool edited=client.EditAccount(account);
            Assert.AreNotEqual(null, edited, "Didn't create Direction");    
        }
        [TestMethod]
        public void GetAllClientReservation()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            ReservationModel.Reservation [] reservations=client.GetAllClientReservations(1);
            Assert.AreNotEqual(null, reservations, "Didn't get reservations");    

        }
        [TestMethod]
        public void GetAllBusinessReservation()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            ReservationModel.Reservation[] reservations = client.GetAllBusinessReservations(1);
            Assert.AreNotEqual(null, reservations, "Didn't get reservations");

        }
        [TestMethod]
        public void EditBusiness()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            Business business = new Business();
            business.directionId = 1;
            business.businessName = "test";
            business.telephone = "test";
            business.description = "test";
            business.syleId = 1;
            business.numSpaces = 20;
            business.priceRange = "test";
            business.mail = "test";
            business.businessId = 6;
            bool edited=client.EditBusiness(business);
            Assert.AreNotEqual(null, edited, "Didn't get reservations");
        }
    }
}
