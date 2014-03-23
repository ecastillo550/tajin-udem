using ReservationModel;
using ReservationUI.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReservationUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.Open();
            DataSet data = client.GetAllBusiness();
            Account acc = client.GetAccount(3);
            Business business= client.GetBusiness(3);
            Client clien=client.GetClient(4);
            Direction direcion=client.GetDirection(1);
            Message message=client.GetMessage(1);
            Reservation reservation=client.GetReservation(1);
            ReservationModel.Style style=client.GetStyle(1);
            Account account = new Account();
            account.password = "543";
            account.userName = "Pepe";
            account.userTypeId = 2;
            account.userId = 6;
            account.directionId = 1;
            bool create= client.CreateAccount(account);
        }
    }
}