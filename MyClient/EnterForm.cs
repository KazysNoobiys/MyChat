using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClient
{
    public partial class EnterForm : Form
    {
        public string UserName { get; set; }
        public string Passowrd { get; set; }
        
        public EnterForm()
        {
            InitializeComponent();            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            UserName = textBoxName.Text;
            Passowrd = textBoxPassword.Text;           
            this.Close();
        }
    }
}
