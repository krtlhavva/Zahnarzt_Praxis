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
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Termin termin = new Termin();
            termin.Show();
            this.Hide();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            Rezepte rezepte = new Rezepte();
            rezepte.Show();
            this.Hide();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();    
            patient.Show();
            this.Hide();    
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Untersuchung untersuchung = new Untersuchung();
            untersuchung.Show();
            this.Hide();
        }
    }
}
