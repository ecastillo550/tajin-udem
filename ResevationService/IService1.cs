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
        string GetData(int value);
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        [OperationContract]
        DataSet GetAllBusiness();
        [OperationContract]
        Account GetAccount(int id);
        [OperationContract]
        Reservation GetReservation(int id);
        [OperationContract]
        Message GetMessage(int id);
        [OperationContract]
        Client GetClient(int id);
        [OperationContract]
        Style GetStyle(int id);
        [OperationContract]
        Direction GetDirection(int id);
        [OperationContract]
        Business GetBusiness(int id);
        [OperationContract]
        bool CreateAccount(Account account);
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
