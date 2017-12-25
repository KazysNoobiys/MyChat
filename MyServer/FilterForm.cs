using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyServer
{
    public partial class FilterForm : Form
    {
        string commandStr;
        DataView dataView;
        DataGridView dataGridViewChat;
        public DataView DataViewChat { get { return dataView; } }
        public FilterForm(DataGridView dataGridViewChat)
        {
            InitializeComponent();
            commandStr = String.Empty;
            dataView = null;
            this.dataGridViewChat = dataGridViewChat;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var chatAdapter = new MyChatTableAdapters.ChatTableAdapter();
            var userAdapter = new MyChatTableAdapters.UsersTableAdapter();
            var userDB = userAdapter.GetData();
            var chatDB = chatAdapter.GetData();            

            var dateStart = DateTime.Parse(dateTimePicker1.Text);
            var dateEnd = DateTime.Parse(dateTimePicker2.Text);

           

            if (checkBoxDate.Checked && checkBoxName.Checked)
            {
                var query = from u in userDB
                            where u.UserName == textBox1.Text
                            join m in chatDB on u.UserID equals m.UserID
                            where m.MessageDate >= dateStart && m.MessageDate <= dateEnd
                            select new { UserID = u.UserID, UserName = u.UserName, MessageText = m.MessageText, MessageDate = m.MessageDate };

                dataGridViewChat.DataSource = query.ToList();
              
            }
            else if(checkBoxDate.Checked && !checkBoxName.Checked)
            {
                var query = from u in userDB                            
                            join m in chatDB on u.UserID equals m.UserID
                            where m.MessageDate >= dateStart && m.MessageDate <= dateEnd
                            select new { UserID = u.UserID, UserName = u.UserName, MessageText = m.MessageText, MessageDate = m.MessageDate };

                dataGridViewChat.DataSource = query.ToList();
               
            }
            else if(!checkBoxDate.Checked && checkBoxName.Checked)
            {
                var query = from u in userDB
                            where u.UserName == textBox1.Text
                            join m in chatDB on u.UserID equals m.UserID
                            select new { UserID = u.UserID, UserName = u.UserName, MessageText = m.MessageText, MessageDate = m.MessageDate };

                dataGridViewChat.DataSource = query.ToList();
              
            }
            this.Close();
             
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxDate_CheckedChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Enabled)
                dateTimePicker1.Enabled = false; 
            else
                dateTimePicker1.Enabled = true;

            if (dateTimePicker2.Enabled)
                dateTimePicker2.Enabled = false;
            else
                dateTimePicker2.Enabled = true;
        }

        private void checkBoxName_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox1.Enabled)
                textBox1.Enabled = false;
            else
                textBox1.Enabled = true;
        }
    }
}
