using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyChat
{    
    [ServiceContract]
    public interface IChatContract
    {
        [OperationContract]
        bool Registration(string name, string password);

        [OperationContract]
        bool Enter(string name, string password, out int sessionID, out int userID);

        [OperationContract]
        bool Exit(int userID, int sessionID);

        [OperationContract]
        void SendMessage(int userID, string str);

        [OperationContract]
        List<Message> GetServerMessages(int sizeChat);

        [OperationContract]
        List<string> GetUsersOnline();

    }
    [DataContract]
    public class Message
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Text { get; set; }       

        [DataMember]
        public DateTime MessageDate { get; set; }

    }
 
}
