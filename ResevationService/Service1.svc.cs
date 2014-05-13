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
        public List<Business> GetAllBusiness(int typeId, int directionId)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Business", con); ;
                char option;
                try
                {
                    DataSet data = new DataSet();
                    if (typeId == -1 && directionId == -1)
                        option = 'a';
                    else
                    {
                        if (typeId != -1 && directionId == -1)
                            option = 'b';
                        else
                        {
                            if (typeId == -1 && directionId != -1)
                                option = 'c';
                            else
                                option = 'd';
                        }
                        switch (option)
                        {
                            case 'a': { adapter = new SqlDataAdapter("Select * from Business", con); } break;
                            case 'b': { adapter = new SqlDataAdapter("Select * from Business where syleId=" + typeId, con); } break;
                            case 'c': { adapter = new SqlDataAdapter("Select * from Business where directionId=" + directionId, con); } break;
                            case 'd': { adapter = new SqlDataAdapter("Select * from Business where directionId=" + directionId + " and syleId=" + typeId, con); } break;
                        }
                    }
                    con.Open();
                    adapter.Fill(data, "Business");
                    List<Business> businessList = new List<Business>();

                    var table = data.Tables["Business"];
                    foreach (DataRow row in table.Rows)
                    {
                        Business business = new Business();
                        business.businessId = (int)row["businessId"];
                        business.businessName = (String)row["businessName"];
                        business.description = (String)row["description"];
                        business.directionId = (int)row["directionId"];
                        business.mail = (String)row["mail"];
                        business.numSpaces = (int)row["numSpaces"];
                        business.priceRange = (String)row["priceRange"];
                        business.syleId = (int)row["syleId"];
                        business.telephone = (String)row["telephone"];
                        business.userId = (int)row["userId"];
                        businessList.Add(business);
                    }

                    return businessList;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get All Business " + e);
                }
            }
        }
        public Account GetAccount(int id)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                Account account = new Account();
                DataSet data = new DataSet();
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
                        account.mail = data.Tables[0].Rows[0]["mail"].ToString();
                        account.userId =Convert.ToInt32(data.Tables[0].Rows[0]["userId"].ToString());
                    }
                    con.Close();
                    return account;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Account " + e);
                }
            }
        }
        public Reservation GetReservation(int id)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                Reservation reservation = new Reservation();
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Reservation where reservationId=" + id, con);
                    DataSet data = new DataSet();
                    adapter.Fill(data, "reservation");
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        reservation.bUserId = Convert.ToInt32(data.Tables[0].Rows[0]["bUserId"].ToString());
                        reservation.cUserId = Convert.ToInt32(data.Tables[0].Rows[0]["cUserId"].ToString());
                        reservation.people = Convert.ToInt32(data.Tables[0].Rows[0]["people"].ToString());
                        reservation.rDay = Convert.ToDateTime(data.Tables[0].Rows[0]["rDay"].ToString());
                        reservation.rTime = TimeSpan.Parse(data.Tables[0].Rows[0]["rTime"].ToString());
                    }

                    return reservation;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Reservation " + e);
                }
            }

        }
        public Message GetMessage(int id)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                Message message = new Message();
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Messages where messageId=" + id, con);
                    DataSet data = new DataSet();
                    adapter.Fill(data, "Messages");
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        message.bUserId = Convert.ToInt32(data.Tables[0].Rows[0]["bUserId"].ToString());
                        message.cUserId = Convert.ToInt32(data.Tables[0].Rows[0]["cUserId"].ToString());
                        message.mDay = Convert.ToDateTime(data.Tables[0].Rows[0]["mDay"].ToString());
                        message.messageText = data.Tables[0].Rows[0]["messageText"].ToString();
                        message.mTime = TimeSpan.Parse(data.Tables[0].Rows[0]["mTime"].ToString());
                    }
                    return message;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Message " + e);
                }
            }
        }
        public bool DeleteMessage(Message message)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    SqlCommand comnd = new SqlCommand("DELETE FROM Messages WHERE messageId=@messageId", con);
                    comnd.Parameters.AddWithValue("@messageId", message.messageId);
                    con.Open();
                    comnd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Delete Message " + e);
                }
            }
        }
        public Client GetClient(int id)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                Client client = new Client();
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Client where userId=" + id, con);
                    DataSet data = new DataSet();
                    adapter.Fill(data, "Client");
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        client.clientId = Convert.ToInt32(data.Tables[0].Rows[0]["clientId"].ToString());
                        client.firstName = data.Tables[0].Rows[0]["firstName"].ToString();
                        client.mLastName = data.Tables[0].Rows[0]["mLastName"].ToString();
                        client.pLastName = data.Tables[0].Rows[0]["pLastName"].ToString();
                        client.telephone = data.Tables[0].Rows[0]["telephone"].ToString();
                    }
                    return client;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Client " + e);
                }
            }
        }
        public Client GetBusinessClient(int id)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                Client client = new Client();
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Client where clientId=" + id, con);
                    DataSet data = new DataSet();
                    adapter.Fill(data, "Client");
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        client.clientId = Convert.ToInt32(data.Tables[0].Rows[0]["clientId"].ToString());
                        client.firstName = data.Tables[0].Rows[0]["firstName"].ToString();
                        client.mLastName = data.Tables[0].Rows[0]["mLastName"].ToString();
                        client.pLastName = data.Tables[0].Rows[0]["pLastName"].ToString();
                        client.telephone = data.Tables[0].Rows[0]["telephone"].ToString();
                        client.userId = Convert.ToInt32(data.Tables[0].Rows[0]["userId"].ToString());
                    }
                    return client;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Client " + e);
                }
            }
        }
        public Style GetStyle(int id)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
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
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Style " + e);
                }
            }
        }
        public Direction GetDirection(int id)
        {
            Direction direction = new Direction();
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                DataSet data = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Direction where directionId=" + id, con);
                try
                {
                    con.Open();
                    adapter.Fill(data, "Direction");
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        direction.directionId = Convert.ToInt32(data.Tables[0].Rows[0]["directionId"].ToString());
                        direction.city = data.Tables[0].Rows[0]["city"].ToString();
                        direction.colony = data.Tables[0].Rows[0]["colony"].ToString();
                        direction.number = data.Tables[0].Rows[0]["number"].ToString();
                        direction.postalCod = Convert.ToInt32(data.Tables[0].Rows[0]["postalCod"].ToString());
                        direction.street = data.Tables[0].Rows[0]["street"].ToString();
                    }
                    return direction;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Direction " + e);
                }
            }
        }
        public Business GetBusiness(int id)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                Business business = new Business();
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Business where businessId=" + id, con);
                    DataSet data = new DataSet();
                    con.Open();
                    adapter.Fill(data, "Business");
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        
                        business.businessName = data.Tables[0].Rows[0]["businessName"].ToString();
                        business.description = data.Tables[0].Rows[0]["description"].ToString();
                        business.directionId = Convert.ToInt32(data.Tables[0].Rows[0]["directionId"].ToString());
                        business.mail = data.Tables[0].Rows[0]["mail"].ToString();
                        business.telephone = data.Tables[0].Rows[0]["telephone"].ToString();
                        business.numSpaces = Convert.ToInt32(data.Tables[0].Rows[0]["numSpaces"].ToString());
                        business.priceRange = data.Tables[0].Rows[0]["priceRange"].ToString();
                        business.syleId = Convert.ToInt32(data.Tables[0].Rows[0]["syleId"].ToString());
                        business.directionId = Convert.ToInt32(data.Tables[0].Rows[0]["directionId"].ToString());
                        GetStyle(business.syleId.Value);
                    }
                    return business;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Business " + e);
                }
            }
        }
        public int CreateAccount(Account account)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Account (password,userName,userTypeId,mail) OUTPUT INSERTED.userId  VALUES (@password,@userName,@userTypeId,@mail)", con);
                    command.Parameters.AddWithValue("@password", account.password);
                    command.Parameters.AddWithValue("@userName", account.userName);
                    command.Parameters.AddWithValue("@userTypeId", account.userTypeId);
                    command.Parameters.AddWithValue("@mail", account.mail);
                    return (int)command.ExecuteScalar();
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Creating Account " + e);
                }
            }
        }
        public bool CreateClientAccount(Client client)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Client (userId,firstName,pLastName,mLastName,telephone) VALUES (@userId,@firstName,@pLastName,@mLastName,@telephone)", con);
                    command.Parameters.AddWithValue("@userId", client.userId);
                    command.Parameters.AddWithValue("@firstName", client.firstName);
                    command.Parameters.AddWithValue("@pLastName", client.pLastName);
                    command.Parameters.AddWithValue("@mLastName", client.mLastName);
                    command.Parameters.AddWithValue("@telephone", client.telephone);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Creating Client Account " + e);
                }
            }
        }
        public bool EditClientAccount(Client client)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("UPDATE Client SET firstName=@firstName,pLastName=@pLastName,telephone=@telephone WHERE clientId=@clientId ", con);
                    command.Parameters.AddWithValue("@firstName", client.firstName);
                    command.Parameters.AddWithValue("@pLastName", client.pLastName);
                    command.Parameters.AddWithValue("@mLastName", client.mLastName);
                    command.Parameters.AddWithValue("@telephone", client.telephone);
                    command.Parameters.AddWithValue("@clientId", client.clientId);
                    command.ExecuteNonQuery();

                    EditAccount(client.Account);

                    return true;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Edit CLient Account " + e);
                }
            }
        }
        public bool CreateBusinessAccount(Business business)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Business (userId,businessName,telephone,description,syleId,numSpaces,mail,priceRange,directionId) VALUES (@userId,@businessName,@telephone,@description,@styleId,@numSpaces,@mail,@priceRange,@directionId)", con);
                    command.Parameters.AddWithValue("@userId", business.userId);
                    command.Parameters.AddWithValue("@businessName", business.businessName);
                    command.Parameters.AddWithValue("@telephone", business.telephone);
                    command.Parameters.AddWithValue("@description", business.description);
                    command.Parameters.AddWithValue("@styleId", business.syleId);
                    command.Parameters.AddWithValue("@numSpaces", business.numSpaces);
                    command.Parameters.AddWithValue("@mail", business.mail);
                    command.Parameters.AddWithValue("@priceRange", business.priceRange);
                    command.Parameters.AddWithValue("@directionId", business.directionId);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Creating Business Account " + e);
                }
            }
        }
        public int CreateDirection(Direction direction)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Direction (number,street,colony,city,postalCod) OUTPUT INSERTED.directionId VALUES (@number,@street,@colony,@city,@postalCod)", con);
                    command.Parameters.AddWithValue("@number", direction.number);
                    command.Parameters.AddWithValue("@street", direction.street);
                    command.Parameters.AddWithValue("@colony", direction.colony);
                    command.Parameters.AddWithValue("@city", direction.city);
                    command.Parameters.AddWithValue("@postalCod", direction.postalCod);
                    int directionId = (Int32)command.ExecuteScalar();
                    return directionId;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Creating Direction " + e);
                }
            }
        }
        public bool EditAccount(Account account)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("UPDATE Account SET password=@password, mail=@mail WHERE userId=@userId", con);
                    command.Parameters.AddWithValue("@password", account.password);
                    command.Parameters.AddWithValue("@mail", account.mail);
                    command.Parameters.AddWithValue("@userId", account.userId);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Editing Account " + e);
                }
            }
        }
        public List<Reservation> GetAllClientReservations(int id)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Reservation where cUserId="+id, con);
                    DataSet data = new DataSet();
                    adapter.Fill(data, "reservation");
                    List<Reservation> reservationList = new List<Reservation>();
                    var table = data.Tables["Reservation"];
                    foreach (DataRow row in table.Rows)
                    {
                        Reservation reservation = new Reservation();
                        reservation.reservationId = (int)row["reservationId"];
                        reservation.bUserId = (int)row["bUserId"];
                        reservation.cUserId = (int)row["cUserId"];
                        reservation.people = (int)row["people"];
                        reservation.rDay = (DateTime)row["rDay"];
                        reservation.reservationId = (int)row["reservationId"];
                        reservation.rTime = (TimeSpan)row["rTime"];
                        reservationList.Add(reservation);
                    }
                    return reservationList;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get All Client Reservation " + e);
                }
            }
        }
        public List<Reservation> GetAllBusinessReservations(int id)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {

                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Reservation where bUserId="+id, con);
                    DataSet data = new DataSet();
                    adapter.Fill(data, "Reservation");
                    List<Reservation> reservationList = new List<Reservation>();

                    var table = data.Tables["Reservation"];
                    foreach (DataRow row in table.Rows)
                    {
                        Reservation reservation = new Reservation();
                        reservation.bUserId = (int)row["bUserId"];
                        reservation.cUserId = (int)row["cUserId"];
                        reservation.people = (int)row["people"];
                        reservation.rDay = (DateTime)row["rDay"];
                        reservation.reservationId = (int)row["reservationId"];
                        reservation.rTime = (TimeSpan)row["rTime"];
                        reservationList.Add(reservation);
                    }
                    return reservationList;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get All Business Reservation " + e);
                }
            }
        }
        public bool EditBusiness(Business business)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("UPDATE Business SET businessName=@businessName,telephone=@telephone,description=@description,syleId=@styleId,numSpaces=@numSPaces,mail=@mail,priceRange=@priceRange,directionId=@directionId WHERE businessId=@businessId", con);
                    command.Parameters.AddWithValue("@businessName", business.businessName);
                    command.Parameters.AddWithValue("@telephone", business.telephone);
                    command.Parameters.AddWithValue("@description", business.description);
                    command.Parameters.AddWithValue("@styleId", business.syleId);
                    command.Parameters.AddWithValue("@numSpaces", business.numSpaces);
                    command.Parameters.AddWithValue("@mail", business.mail);
                    command.Parameters.AddWithValue("@priceRange", business.priceRange);
                    command.Parameters.AddWithValue("@directionId", business.directionId);
                    command.Parameters.AddWithValue("@businessId", business.businessId);
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in edit Business " + e);
                }
            }
        }
        public Account Login(String password, String userName)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                Account account = new Account();
                DataSet data = new DataSet();
                SqlDataAdapter adapter;
                try
                {
                    con.Open();
                    adapter = new SqlDataAdapter("Select * from Account where password='"+password+"' and userName='"+userName+"'", con);
                    adapter.Fill(data, "Account");
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        account.password = data.Tables[0].Rows[0]["password"].ToString();
                        account.userTypeId = Convert.ToInt32(data.Tables[0].Rows[0]["userTypeId"].ToString());
                        account.userName = data.Tables[0].Rows[0]["userName"].ToString();
                        account.userId = Convert.ToInt32(data.Tables[0].Rows[0]["userId"].ToString());
                        account.mail = data.Tables[0].Rows[0]["mail"].ToString();
                    }
                    return account;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Account " + e);
                }
            }
        }
        public bool CreateReservation(Reservation reservation)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Reservation (bUserId,cUserId,rDay,rTime,people,status) VALUES (@bUserId,@cUserId,@rDay,@rTime,@people,@status)", con);
                    command.Parameters.AddWithValue("@bUserId", reservation.bUserId);
                    command.Parameters.AddWithValue("@rDay", reservation.rDay);
                    command.Parameters.AddWithValue("@cUserId", reservation.cUserId);
                    command.Parameters.AddWithValue("@rTime", reservation.rTime);
                    command.Parameters.AddWithValue("@people", reservation.people);
                    command.Parameters.AddWithValue("@status", true);
                    con.Open();
                    command.ExecuteNonQuery();
                    RestBusinessSpaces(reservation.bUserId);
                    return true;
                }
                catch (Exception e)
                {

                    throw new FaultException("Error in Creating Reservation " + e);
                }
            }
        }
        public void RestBusinessSpaces(int businessId)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    Business business = GetBusiness(businessId);
                    SqlCommand cmnd = new SqlCommand("UPDATE Business SET numSpaces=@numSpaces WHERE businessId=@businessId", con);
                    int numSpaces = business.numSpaces - 1;
                    cmnd.Parameters.AddWithValue("@numSpaces", numSpaces);
                    cmnd.Parameters.AddWithValue("@businessId", businessId);
                    con.Open();
                    cmnd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Resting business spaces " + e);
                }
            }
        }
        public bool EditReservation(Reservation reservation)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Reservation SET rDay=@rDay,rTime=@rTime,people=@people WHERE reservationId=@reservationId", con);
                    command.Parameters.AddWithValue("@rDay", reservation.rDay);
                    command.Parameters.AddWithValue("@rTime", reservation.rTime);
                    command.Parameters.AddWithValue("@people", reservation.people);
                    command.Parameters.AddWithValue("@reservationId", reservation.reservationId);
                    con.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Editing Reservation " + e);
                }
            }
        }
        public bool DeleteReservation(int reservationId)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Reservation WHERE reservationId=@reservationId", con);
                    command.Parameters.AddWithValue("@reservationId", reservationId);
                    con.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Deleting Reservation " + e);
                }
            }
        }
        public Business SearchBusiness(string name)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                Business business = new Business();
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Business where businessName='" + name + "'", con);
                    DataSet data = new DataSet();
                    con.Open();
                    adapter.Fill(data, "Business");
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        business.businessId = Convert.ToInt32(data.Tables[0].Rows[0]["businessId"].ToString());
                        business.businessName = data.Tables[0].Rows[0]["businessName"].ToString();
                        business.description = data.Tables[0].Rows[0]["description"].ToString();
                        business.mail = data.Tables[0].Rows[0]["mail"].ToString();
                        business.telephone = data.Tables[0].Rows[0]["telephone"].ToString();
                        business.numSpaces = Convert.ToInt32(data.Tables[0].Rows[0]["numSpaces"].ToString());
                        business.priceRange = data.Tables[0].Rows[0]["priceRange"].ToString();
                        business.syleId = Convert.ToInt32(data.Tables[0].Rows[0]["syleId"].ToString());
                        business.directionId = Convert.ToInt32(data.Tables[0].Rows[0]["directionId"].ToString());
                    }
                    return business;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Business " + e);
                }
            }
        }
        public List<Style> GetAllStyles()
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {

                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Style", con);
                    DataSet data = new DataSet();
                    adapter.Fill(data, "Style");
                    List<Style> styleList = new List<Style>();

                    var table = data.Tables["Style"];
                    foreach (DataRow row in table.Rows)
                    {
                        Style style = new Style();
                        style.style1 = (string)row["style"];
                        style.styleId = (int)row["styleId"];
                        styleList.Add(style);
                    }
                    return styleList;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get All Business Reservation " + e);
                }
            }
        }
        public Business GetBusinessbId(int id)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                Business business = new Business();
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * from Business where businessId=" + id, con);
                    DataSet data = new DataSet();
                    con.Open();
                    adapter.Fill(data, "Business");
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        business.businessName = data.Tables[0].Rows[0]["bussinesName"].ToString();
                        business.description = data.Tables[0].Rows[0]["description"].ToString();
                        business.mail = data.Tables[0].Rows[0]["mail"].ToString();
                        business.telephone = data.Tables[0].Rows[0]["telephone"].ToString();
                        business.numSpaces = Convert.ToInt32(data.Tables[0].Rows[0]["numSpaces"].ToString());
                        business.priceRange = data.Tables[0].Rows[0]["priceRange"].ToString();
                        business.syleId = Convert.ToInt32(data.Tables[0].Rows[0]["syleId"].ToString());
                        business.directionId = Convert.ToInt32(data.Tables[0].Rows[0]["directionId"].ToString());
                        GetStyle(business.syleId.Value);
                    }
                    return business;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get Business " + e);
                }
            }
        }
        public bool CreateMessage(Message message)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    SqlCommand cmnd = new SqlCommand("INSERT INTO Messages (bUserId,messageText,cUserId,mDay) VALUES (" +
                        "@bUserId,@messageText,@cUserId,@mDay)", con);
                    cmnd.Parameters.AddWithValue("@bUserId", message.bUserId);
                    cmnd.Parameters.AddWithValue("@messageText", message.messageText);
                    cmnd.Parameters.AddWithValue("@mTime", DateTime.Now);
                    cmnd.Parameters.AddWithValue("@cUserId", message.cUserId);
                    cmnd.Parameters.AddWithValue("@mDay", message.mDay);
                    con.Open();
                    cmnd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Create Message " + e);
                }
            }
        }
        public bool EndReservation(int reservationId)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Reservation SET status=@status WHERE reservationId=@reservationId", con);
                    command.Parameters.AddWithValue("@status", false);
                    command.Parameters.AddWithValue("reservationId", reservationId);
                    con.Open();
                    command.ExecuteNonQuery();
                    Reservation reservation = GetReservation(reservationId);
                    LiberateSpace(reservation.bUserId);
                    return true;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Editing Reservation " + e);
                }
            }
        }
        public void LiberateSpace(int businessId)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    Business business= GetBusiness(businessId);
                    SqlCommand command = new SqlCommand("UPDATE Business SET numSpaces=@numSpaces WHERE businessId=@businessId", con);
                    business.numSpaces=business.numSpaces+1;
                    command.Parameters.AddWithValue("@numSpaces", business.numSpaces);
                    command.Parameters.AddWithValue("@businessId", businessId);
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in liberating space " + e);
                }
            }
        }
        public List<Business> RandomBusiness()
        { 
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("select top 50 percent * from Business order by newid()", con);
                    DataSet data = new DataSet();
                    adapter.Fill(data, "Business");
                    List<Business> businessList = new List<Business>();
                    var table = data.Tables["Business"];
                    foreach (DataRow row in table.Rows)
                    {
                        Business business = new Business();
                        business.businessId = (int)row["businessId"];
                        business.businessName = (String)row["businessName"];
                        business.description = (String)row["description"];
                        business.directionId = (int)row["directionId"];
                        business.mail = (String)row["mail"];
                        business.numSpaces = (int)row["numSpaces"];
                        business.priceRange = (String)row["priceRange"];
                        business.syleId = (int)row["syleId"];
                        business.telephone = (String)row["telephone"];
                        business.userId = (int)row["userId"];
                        businessList.Add(business);
                    }

                    return businessList;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in selecting random business " + e);
                }
            }
            
        }
        public List<Business> GetAllUserBusiness(int userId)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Business WHERE userId="+userId, con); ;
                try
                {
                    DataSet data = new DataSet();
                    con.Open();
                    adapter.Fill(data, "Business");
                    List<Business> businessList = new List<Business>();
                    var table = data.Tables["Business"];
                    foreach (DataRow row in table.Rows)
                    {
                        Business business = new Business();
                        business.businessId = (int)row["businessId"];
                        business.businessName = (String)row["businessName"];
                        business.description = (String)row["description"];
                        business.directionId = (int)row["directionId"];
                        business.mail = (String)row["mail"];
                        business.numSpaces = (int)row["numSpaces"];
                        business.priceRange = (String)row["priceRange"];
                        business.syleId = (int)row["syleId"];
                        business.telephone = (String)row["telephone"];
                        business.userId = (int)row["userId"];
                        business.Style = GetStyle((int)business.syleId);
                        businessList.Add(business);
                    }

                    return businessList;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in Get User Business " + e);
                }
            }
        }
        public List<String> GetAllUserNames()
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Account", con);
                    DataSet data = new DataSet();
                    adapter.Fill(data, "Account");
                    List<String> UserNameList = new List<String>();
                    var table = data.Tables["Account"];
                    string name = "";
                    foreach (DataRow row in table.Rows)
                    {
                        name =(String) row["userName"];
                        UserNameList.Add(name);
                    }

                    return UserNameList;
                }
                catch (Exception e)
                {
                    throw new FaultException("Error in getting all user names " + e);
                }
            }  
        }
    }
}
