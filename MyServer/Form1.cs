using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;


namespace MyServer
{
    public partial class Form1 : Form
    {

        ServiceHost host;
        DataView chatView;
        DataView usersView;

        MyChatTableAdapters.UsersTableAdapter userAdapter;
        MyChatTableAdapters.SessionsLogTableAdapter sessionAdapter;
        MyChatTableAdapters.ChatTableAdapter chatAdapter;

        MyChatDB.UsersDataTable userDB;
        MyChatDB.SessionsLogDataTable sessionDB;
        MyChatDB.ChatDataTable chatDB;
        public Form1()
        {
            InitializeComponent();
            //comboBox1.SelectedIndex = 0;

            chatAdapter = new MyChatTableAdapters.ChatTableAdapter();
            userAdapter = new MyChatTableAdapters.UsersTableAdapter();
            sessionAdapter = new MyChatTableAdapters.SessionsLogTableAdapter();

            userDB = userAdapter.GetData();
            chatDB = chatAdapter.GetData();
            sessionDB = sessionAdapter.GetData();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            host = new ServiceHost(typeof(MyChat.ChatService));
            host.Open();
            textBoxLog.Text += "Сервер начал работу" + Environment.NewLine;


            chatView = new DataView(chatDB);
            usersView = new DataView(userDB);

            var queryChat = from u in userDB
                            join m in chatDB on u.UserID equals m.UserID
                            select new { UserID = u.UserID, UserName = u.UserName, MessageText = m.MessageText, MessageDate = m.MessageDate };

            dataGridViewChat.DataSource = queryChat.ToList();

            comboBox1.SelectedIndex = 0;
            var queryUser = from u in userDB
                            where u.OnlineStatus == true
                            join s in sessionDB on u.UserID equals s.UserID into countSes
                            from ses in countSes
                            where ses.StartSession == countSes.Max(r => r.StartSession)
                            select new { UserID = u.UserID, UserName = u.UserName, Password = u.UserPassword, SessionsCount = countSes.Count(), LastEnter = ses.StartSession };


            dataGridViewUsers.DataSource = queryUser.ToList();

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            buttonFilter.Enabled = true;
            buttonUpdateUsers.Enabled = true;
            dataGridViewChat.Enabled = true;
            dataGridViewUsers.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            host.Close();
            textBoxLog.Text += "Сервер прекратил работу" + Environment.NewLine;

            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            buttonFilter.Enabled = false;
            buttonUpdateUsers.Enabled = false;
            dataGridViewChat.Enabled = false;
            dataGridViewUsers.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            FilterForm ff = new FilterForm(dataGridViewChat);           
            ff.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            userDB = userAdapter.GetData();
            sessionDB = sessionAdapter.GetData();
            if (comboBox1.SelectedIndex == 0)
            {
                var query = from u in userDB
                            where u.OnlineStatus == true
                            join s in sessionDB on u.UserID equals s.UserID into countSes
                            from ses in countSes
                            where ses.StartSession == countSes.Max(r => r.StartSession)
                            select new { UserID = u.UserID, UserName = u.UserName, Password = u.UserPassword, SessionsCount = countSes.Count(), LastEnter = ses.StartSession };


                dataGridViewUsers.DataSource = query.ToList();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                var query = from u in userDB
                            where u.OnlineStatus == false
                            join s in sessionDB on u.UserID equals s.UserID into countSes
                            from ses in countSes
                            where ses.StartSession == countSes.Max(r => r.StartSession)
                            select new { UserID = u.UserID, UserName = u.UserName, Password = u.UserPassword, SessionsCount = countSes.Count(), LastEnter = ses.StartSession };


                dataGridViewUsers.DataSource = query.ToList();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            host.Close();
        }

        private void buttonUpdateUsers_Click(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);
        }
    }
}
