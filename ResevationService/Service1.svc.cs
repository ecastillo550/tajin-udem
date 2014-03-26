using ReservationModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ResevationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
           if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public DataSet GetAllBusiness(int typeId, int directionId)
        {
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            SqlDataAdapter adapter;
            try
            {
                DataSet business = new DataSet();
                if (typeId == -1 && directionId == -1)
                    adapter = new SqlDataAdapter("Select * from Business", con);
                else
                {
                    if (typeId != -1 && directionId == -1)
                        adapter = new SqlDataAdapter("Select * from Business where typeId=" + typeId, con);
                    else
                    {
                        if (typeId == -1 && directionId != -1)
                            adapter = new SqlDataAdapter("Select * from Business where directionId=" + directionId, con);
                        else
                            adapter = new SqlDataAdapter("Select * from Business where directionId=" + directionId + " and typeId=" + typeId, con);
                    }
                }
                con.Open();
                adapter.Fill(business, "Business");
                con.Close();
                return business;
            }
            catch (Exception)
            {
                con.Close();
                throw new System.ArgumentException("GetAllBusiness error");
            }

        }
        public Account GetAccount(int id)
        {
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            Account account = new Account();
            DataSet data = new DataSet();
            DataSet data2 = new DataSet();
            SqlDataAdapter adapter;
            try
            {
                con.Open();
                adapter = new SqlDataAdapter("Select * from Account where userId=" + id, con);
                adapter.Fill(data, "Account");
                if (data.Tables[0].Rows.Count > 0)
                {
                    account.password = data.Tables[0].Rows[0]["password"].ToString();
                    account.userTypeId = Convert.ToInt32(data.Tables[0].Rows[0]["userTypeId"].ToString());
                    account.userName = data.Tables[0].Rows[0]["userName"].ToString();
                }
                con.Close();
                return account;
            }
            catch (Exception)
            {
                con.Close();
                throw new System.ArgumentException("Get Account error");
            }
        }
        public Direction GetDirection(int id)
        {
            Direction direction = new Direction();
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            DataSet data = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Direction where directionId=" + id, con);
            try
            {
                con.Open();
                adapter.Fill(data, "Direction");
                if (data.Tables[0].Rows.Count > 0)
                {
                    direction.city = data.Tables[0].Rows[0]["city"].ToString();
                    direction.colony = data.Tables[0].Rows[0]["colony"].ToString();
                    direction.number = data.Tables[0].Rows[0]["number"].ToString();
                    direction.postalCod = Convert.ToInt32(data.Tables[0].Rows[0]["postalCod"].ToString());
                    direction.street = data.Tables[0].Rows[0]["street"].ToString();
                }
                con.Close();
                return direction;
            }
            catch (Exception)
            {
                con.Close();
                throw new System.ArgumentException("CGetDirection error");
            }
        }
        public Business GetBusiness(int id)
        {
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            Business business = new Business();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Business where userId="+ id, con);
                DataSet data = new DataSet();
                
                con.Open();
                adapter.Fill(data, "Business");
                if (data.Tables[0].Rows.Count > 0)
                {
                    business.bussinesName = data.Tables[0].Rows[0]["bussinesName"].ToString();
                    business.description = data.Tables[0].Rows[0]["description"].ToString();
                    business.mail = data.Tables[0].Rows[0]["mail"].ToString();
                    business.telephone = data.Tables[0].Rows[0]["telephone"].ToString();
                    business.numSpaces = Convert.ToInt32(data.Tables[0].Rows[0]["numSpaces"].ToString());
                    business.priceRangeId = Convert.ToInt32(data.Tables[0].Rows[0]["priceRangeId"].ToString());
                    business.syleId = Convert.ToInt32(data.Tables[0].Rows[0]["syleId"].ToString());
                    business.directionId = Convert.ToInt32(data.Tables[0].Rows[0]["directionId"].ToString());
                    GetStyle(business.syleId.Value);
                }
                con.Close();
                return business;
            }
            catch (Exception)
            {
                con.Close();
                throw new System.ArgumentException("GetBusiness error");
            }
        }
        public Style GetStyle(int id)
        {
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            Style style = new Style();
            try
            {

                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Style where styleId=" + id, con);
                DataSet data = new DataSet();
                adapter.Fill(data, "Business");
                if (data.Tables[0].Rows.Count > 0)
                {
                    style.style1 = data.Tables[0].Rows[0]["style"].ToString();
                }
                return style;
            }
            catch (Exception)
            {

                throw new System.ArgumentException("GetStyle error");
            }
        }
        public Client GetClient(int id)
        {
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            Client client = new Client();
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Client where userId=" + id, con);
                DataSet data = new DataSet();
                adapter.Fill(data, "Client");
                if (data.Tables[0].Rows.Count > 0)
                {
                    client.firstName = data.Tables[0].Rows[0]["firstName"].ToString();
                    client.mLastName = data.Tables[0].Rows[0]["mLastName"].ToString();
                    client.pLastName = data.Tables[0].Rows[0]["pLastName"].ToString();
                    client.telephone = data.Tables[0].Rows[0]["telephone"].ToString();
                }
                con.Close();
                return client;
            }
            catch (Exception)
            {
                con.Close();
                throw new System.ArgumentException("GetClient error");
            }
        }
        public Message GetMessage(int id)
        {
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            Message message = new Message();
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Message where messageId=" + id, con);
                DataSet data = new DataSet();
                adapter.Fill(data, "Client");
                if (data.Tables[0].Rows.Count > 0)
                {
                    message.bUserId = Convert.ToInt32(data.Tables[0].Rows[0]["bUserId"].ToString());
                    message.cUserId = Convert.ToInt32(data.Tables[0].Rows[0]["cUserId"].ToString());
                    message.mDay = Convert.ToDateTime(data.Tables[0].Rows[0]["mDay"].ToString());
                    message.messageText = data.Tables[0].Rows[0]["messageText"].ToString();
                    message.mTime = TimeSpan.Parse(data.Tables[0].Rows[0]["mTime"].ToString());
                }
                con.Close();
                return message;
            }
            catch (Exception)
            {
                con.Close();
                throw new System.ArgumentException(" GetMessage error");
            }
        }
        //Reservacion especifica
        public Reservation GetReservation(int id)
        {
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            Reservation reservation = new Reservation();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Reservation where reservationId=" + id, con);
                DataSet data = new DataSet();
                adapter.Fill(data, "reservation");
                if (data.Tables[0].Rows.Count > 0)
                {
                    reservation.bUserId = Convert.ToInt32(data.Tables[0].Rows[0]["bUserId"].ToString());
                    reservation.cUserId=Convert.ToInt32(data.Tables[0].Rows[0]["bcUserId"].ToString());
                    reservation.people = Convert.ToInt32(data.Tables[0].Rows[0]["people"].ToString());
                    reservation.rDay = Convert.ToDateTime(data.Tables[0].Rows[0]["rDay"].ToString());
                    reservation.rTime = TimeSpan.Parse(data.Tables[0].Rows[0]["rTime"].ToString());
                }

                return reservation;
            }
            catch (Exception)
            {
                con.Close();
                throw new System.ArgumentException("GetReservation error");
            }
            //Obtener Todas las reservaciones
        }

        ////////////////////
        /////////////////
        ///////////
        //
        //
        public bool CreateAccount(Account account)
        {
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Account (password,userName,userTypeId) VALUES (@password,@userName,@userTypeId)", con);
                command.Parameters.AddWithValue("@password", account.password);
                command.Parameters.AddWithValue("@userName", account.userName);
                command.Parameters.AddWithValue("@userTypeId", account.userTypeId);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                con.Close();
                return false;
            }
        }
        public bool CreateBusinessAccount(Business business)
        {
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            try
            {
                
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Business (businessName,telephone,description,styleId,numSpaces,mail,priceRangeId,directionId) VALUES (@businessName,@telephone,@description,@styleId,@numSpaces,@mail,@priceRangeId,@directionId)", con);
                command.Parameters.AddWithValue("@businessName", business.bussinesName);
                command.Parameters.AddWithValue("@telephone", business.telephone);
                command.Parameters.AddWithValue("@description", business.description);
                command.Parameters.AddWithValue("@styleId", business.syleId);
                command.Parameters.AddWithValue("@numSpaces", business.numSpaces);
                command.Parameters.AddWithValue("@mail", business.mail);
                command.Parameters.AddWithValue("@priceRangeId", business.priceRangeId);
                command.Parameters.AddWithValue("@directionId", business.directionId);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                con.Close();
                return false;
            }
        }
        public bool CreateClientAccount(Client client)
        {
            SqlConnection con = new SqlConnection("Data Source= localhost,18687; Integrated Security= SSPI; Initial Catalog=ReservationApp");
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Client (firstName,pLastName,mLastName,telephone,mail) VALUES (@fistName,@pLastName,@mLastName,@telephone,@mail)", con);
                command.Parameters.AddWithValue("@firstName", client.firstName);
                command.Parameters.AddWithValue("@pLastName", client.pLastName);
                command.Parameters.AddWithValue("@mLastName", client.mLastName);
                command.Parameters.AddWithValue("@telephone", client.telephone);
                command.Parameters.AddWithValue("@mail", client.mail);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                con.Close();
                return false;
            }   
        }     
    }
}
