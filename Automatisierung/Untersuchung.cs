using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Automatisierung
{
    public partial class Untersuchung : Form
    {
        public Untersuchung()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            String query = "insert into UntersuchungTable values('" + UnameTb.Text + "'," +
                                                               "'" + UPreisTb.Text + "'," +
                                                               "'" + UBeschreibungTb.Text + "')";
            DbPatient dbPatient = new DbPatient();
            try
            {
                dbPatient.PatientEinfügen(query);
                MessageBox.Show("Untersuchung wurde erfolgreich hinzugefügt!");
                eingetragene();
                reset();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int key = 0;
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            DbPatient p = new DbPatient();
            if (key == 0)
            {
                MessageBox.Show("Wählen Sie den Untersuchung aus, den Sie aktuallisieren möchten");
            }
            else
            {
                try
                {
                    string query = "Update UntersuchungTable set UName='" + UnameTb.Text +
                                                        "',UPreis= '" + UPreisTb.Text +
                                                        "', UBeschreibung= '" + UBeschreibungTb.Text +
                                                        "' Where UId=" + key + ";";

                    p.LoeschePatient(query);
                    MessageBox.Show("Ausgewählte Untersuchung wurde erfolgreich aktuallisiert!");
                    eingetragene();
                    reset();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            DbPatient p = new DbPatient();
            if (key == 0)
            {
                MessageBox.Show("Wählen Sie den Untersuchung aus, den Sie löschen möchten");
            }
            else
            {
                try
                {
                    string query = "Delete  from UntersuchungTable Where UId=" + key + " ";
                    p.LoeschePatient(query);
                    MessageBox.Show("Ausgewählte Untersuchung wurde erfolgreich gelöscht!");
                    eingetragene();
                    reset();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        void eingetragene()
        {
            DbPatient p = new DbPatient();
            string query = "select * from UntersuchungTable";
            DataSet ds = p.ShowPatient(query);
            UntersuchungDGV.DataSource = ds.Tables[0];
        }
        void reset()
        {
            UnameTb.Text = "";
            UPreisTb.Text = "";
            UBeschreibungTb.Text = "";
            
        }
        private void Untersuchung_Load(object sender, EventArgs e)
        {
            eingetragene();
            reset();
        }

        private void UntersuchungDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UnameTb.Text = UntersuchungDGV.SelectedRows[0].Cells[1].Value.ToString();
            UPreisTb.Text = UntersuchungDGV.SelectedRows[0].Cells[2].Value.ToString();
            UBeschreibungTb.Text = UntersuchungDGV.SelectedRows[0].Cells[3].Value.ToString();
            

            if (UnameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UntersuchungDGV.SelectedRows[0].Cells[0].Value);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Hide();
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();
            patient.Show();
            this.Hide();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            Termin termin = new Termin();
            termin.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            Rezepte rezepte = new Rezepte();
            rezepte.Show();
            this.Hide();
        }

        private void SucheBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new ConnectionString().GetCon();
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select * from UntersuchungTable Where UName like '%" + SucheTb.Text + "%'", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            UntersuchungDGV.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            eingetragene();
            SucheTb.Text = "";
        }
    }
}
