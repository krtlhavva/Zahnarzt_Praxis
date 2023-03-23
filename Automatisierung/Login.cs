using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automatisierung
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if(BnameTb.Text=="" || PasswortTb.Text == "")
            {
                MessageBox.Show("Geben Sie Ihre Benuzername und Passwort ein!");
            }
            else if(BnameTb.Text == "Admin" || PasswortTb.Text == "1234")
            {
                Homepage homepage = new Homepage();
                homepage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Ihr Benutzername oder Passwort ist falsch!");
            }
        }
    }
}
