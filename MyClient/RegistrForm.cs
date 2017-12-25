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
    public partial class RegistrForm : Form
    {       
        public string UserName { get;private set; }
        public string Password { get;private set; }
        public RegistrForm()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserName = textBox1.Text;
            Password = textBox2.Text;
           
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }
    }
}
