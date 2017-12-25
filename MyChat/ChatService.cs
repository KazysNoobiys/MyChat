using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyChat
{   
    public static class ExtensionsTakeLast
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }
    }
    public class ChatService : IChatContract, IDisposable
    {
        MyChatDBTableAdapters.UsersTableAdapter usersTableAdapter;
        MyChatDBTableAdapters.SessionsLogTableAdapter sessionsLogTableAdapter;
        MyChatDBTableAdapters.ChatTableAdapter chatTableAdapter;

        bool IsEnter;
        int uID, sID;
        public ChatService()
        {
            usersTableAdapter = new MyChatDBTableAdapters.UsersTableAdapter();
            sessionsLogTableAdapter = new MyChatDBTableAdapters.SessionsLogTableAdapter();
            chatTableAdapter = new MyChatDBTableAdapters.ChatTableAdapter();

            IsEnter = false;
            uID = sID = 0;
        }
        public bool Enter(string name, string password, out int sessionID, out int userID)
        {           
            var usersDB = usersTableAdapter.GetData();           
            string str = String.Format("UserName = '{0}' AND UserPassword = '{1}' AND OnlineStatus = 0", name, password);

            var rows = usersDB.Select(str) as MyChatDB.UsersRow[];
            

            if (rows.Length > 1)
                throw new ArgumentOutOfRangeException("rows","Два пользователя с одинаковыми именами и параметрами");

            if (rows.Length == 0)
            {
                sessionID = userID = 0;
                return false;
            }
            else
            {
                rows[0].OnlineStatus = true;
                uID = userID = rows[0].UserID;
                
                usersTableAdapter.Update(usersDB);
                sID = sessionID = sessionsLogTableAdapter.Insert(rows[0].UserID, DateTime.Now, null);

                IsEnter = true;
                return true;
            }
                
        }

        public bool Exit(int userID, int sessionID)
        {           
            var usersDB = usersTableAdapter.GetData();
            var sessionDB = sessionsLogTableAdapter.GetData();          

            var userRow = usersDB.FindByUserID(userID);            

            if (userRow == null)
            {
                return false;
            }
            else
            {
                userRow.OnlineStatus = false;                
                usersTableAdapter.Update(usersDB);

                sessionDB.FindByLogID(sessionID).EndSession = DateTime.Now;
                sessionsLogTableAdapter.Update(sessionDB);

                IsEnter = false;
                return true;
            }
        }

        public List<string> GetUsersOnline()
        {           
            var usersDB = usersTableAdapter.GetData();

            var query = from user in usersDB
                        where user.OnlineStatus == true
                        select user;

            List<string> usersOnlineList = new List<string>();
            foreach (var u in query)
            {
                usersOnlineList.Add(u.UserName);
            }

            return usersOnlineList;
        }

        public bool Registration(string name, string password)
        {            
            var usersDB = usersTableAdapter.GetData();
            string str = String.Format("UserName = '{0}'", name);

            DataRow[] rows = usersDB.Select(str);

            if (rows.Length == 0)
            {
                usersTableAdapter.Insert(name, password,false);
                return true;
            }
            else
                return false;
           
        }

        public void SendMessage(int userID, string str)
        {
            chatTableAdapter.Insert(userID, str, DateTime.Now);
        }

        List<Message> IChatContract.GetServerMessages(int sizeChat)
        {
            var usersDB = usersTableAdapter.GetData();
            var chatDB = chatTableAdapter.GetData();

            var query = from m in chatDB
                        join u in usersDB on m.UserID equals u.UserID
                        orderby m.MessageDate
                        select new Message(){ UserName = u.UserName, Text = m.MessageText, MessageDate = m.MessageDate };

            List<Message> messages = query.TakeLast(sizeChat).ToList();
           
            return messages;
        }

        public void Dispose()
        {
           if(IsEnter)
            {
                Exit(uID,sID);
            }
        }
    }
}
