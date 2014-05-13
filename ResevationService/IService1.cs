using ReservationModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace ResevationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Business> GetAllBusiness(int typeId, int directionId);
        [OperationContract]
        Account GetAccount(int id);
        [OperationContract]
        Reservation GetReservation(int id);
        [OperationContract]
        Message GetMessage(int id);
        [OperationContract]
        bool DeleteMessage(Message message);
        [OperationContract]
        Client GetClient(int id);
        [OperationContract]
        Style GetStyle(int id);
        [OperationContract]
        Direction GetDirection(int id);
        [OperationContract]
        Business GetBusiness(int id);
        [OperationContract]
        int CreateAccount(Account account);
        [OperationContract]
        bool CreateClientAccount(Client client);
        [OperationContract]
        bool CreateBusinessAccount(Business business);
        [OperationContract]
        int CreateDirection(Direction direction);
        [OperationContract]
        bool EditAccount(Account account);
        [OperationContract]
        List<Reservation> GetAllBusinessReservations(int id);
        [OperationContract]
        List<Reservation> GetAllClientReservations(int id);
        [OperationContract]
        Account Login(String password, String userName);
        [OperationContract]
        bool EditClientAccount(Client client);
        [OperationContract]
        bool EditBusiness(Business business);
        [OperationContract]
        bool CreateReservation(Reservation reservation);
        [OperationContract]
        bool EditReservation(Reservation reservation);
        [OperationContract]
        bool DeleteReservation(int reservationId);
        [OperationContract]
        Business SearchBusiness(string name);
        [OperationContract]
        List<Style> GetAllStyles();
        [OperationContract]
        bool CreateMessage(Message message);
        [OperationContract]
        bool EndReservation(int reservationId);
        [OperationContract]
        List<Business> RandomBusiness();
        [OperationContract]
        List<Business> GetAllUserBusiness(int userId);
        [OperationContract]
        Client GetBusinessClient(int id);
        [OperationContract]
        List<String> GetAllUserNames();
        // TODO: Add your service operations here
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}
