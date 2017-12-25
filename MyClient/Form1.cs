using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace MyClient
{
    public partial class Form1 : Form
    {
        int userID, sessionID;
        ChatServiceReference.ChatContractClient proxy;
        CancellationTokenSource cancellationTokenSource;
        CancellationToken token;

        public Form1()
        {
            InitializeComponent();
            cancellationTokenSource = new CancellationTokenSource();
            token = cancellationTokenSource.Token;
            proxy = new ChatServiceReference.ChatContractClient();            
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            RegistrForm regForm = new RegistrForm();
            if(regForm.ShowDialog() == DialogResult.OK)
            {
                if(proxy.Registration(regForm.UserName, regForm.Password))
                {
                    MessageBox.Show("Регистрация преведена успешно", "Регистрация", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Ошибка входа", "Вход", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                
            }
        }

        private  void buttonEnter_Click(object sender, EventArgs e)
        {
            EnterForm enterForm = new EnterForm();
            if(enterForm.ShowDialog() == DialogResult.OK)
            {
                if(proxy.Enter(enterForm.UserName, enterForm.Passowrd, out sessionID, out userID))
                {
                    MessageBox.Show("Вход преведён успешно", "Вход", MessageBoxButtons.OK);
                    textBoxMessage.Enabled = true;
                    buttonSend.Enabled = true;
                    buttonExit.Enabled = true;

                    buttonEnter.Enabled = false;
                    if (cancellationTokenSource.IsCancellationRequested )
                    {
                        cancellationTokenSource = new CancellationTokenSource();
                        token = cancellationTokenSource.Token;
                    }


                    UpdateChat(token);
                    UpdateUsersOnline(token);
                } 
                else
                {
                    MessageBox.Show("Ошибка входа", "Вход", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }    
            }
           
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            proxy.SendMessage(userID, textBoxMessage.Text);
            var messages = proxy.GetServerMessages(20);
            foreach (var m in messages)
            {
                textBoxChat.Text += string.Format("[{0}]: {1}/n", m.UserName, m.Text);
            }
        }
        
        private void buttonExit_Click(object sender, EventArgs e)
        {            
            if (proxy.Exit(userID, sessionID))
            {                
                cancellationTokenSource.Cancel();
                
                textBoxMessage.Enabled = false;
                buttonSend.Enabled = false;
                buttonExit.Enabled = false;

                buttonEnter.Enabled = true;

                textBoxChat.Clear();
                listUsersOnline.Items.Clear();

                MessageBox.Show("Выход преведён успешно", "Выход", MessageBoxButtons.OK);

                textBoxMessage.Enabled = false;
                buttonSend.Enabled = false;
                buttonExit.Enabled = false;

                buttonEnter.Enabled = true;
            }
        }
        async Task UpdateChat(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var task =  proxy.GetServerMessagesAsync(20);
                var messages = await task;
                textBoxChat.Clear();
                foreach (var m in messages)
                {
                    textBoxChat.Text += string.Format("[{0}]: {1}", m.UserName, m.Text);
                    textBoxChat.Text += Environment.NewLine;
                }

                await Task.Delay(200, token);
            }
           
        }      

        async Task UpdateUsersOnline(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var task = proxy.GetUsersOnlineAsync();
                var usersOnline = await task;
                listUsersOnline.Items.Clear();
                listUsersOnline.Items.AddRange(usersOnline);

                await Task.Delay(500, token);
            }
            
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(userID != 0 && sessionID != 0)
                proxy.Exit(userID, sessionID);
            proxy.Close();
            proxy = null;
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
