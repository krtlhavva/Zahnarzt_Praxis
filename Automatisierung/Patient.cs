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

namespace Automatisierung
{
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            String query = "insert into PatientTable values('"+PNameTb.Text+"'," +
                                                            "'"+PTelefonTb.Text+"'," +
                                                            "'"+PGeburtsdatumTb.Text+"'," +
                                                            "'"+PGeschlechtTb.SelectedItem.ToString()+"'," +
                                                            "'"+PAdresseTb.Text+"'," +
                                                            "'"+PAllergieTb.Text+"')";
            DbPatient dbPatient = new DbPatient();
            try
            {
                dbPatient.PatientEinfügen(query);
                MessageBox.Show("Patient wurde erfolgreich hinzugefügt!");
                eingetragene();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void eingetragene()
        {
            DbPatient p = new DbPatient();
            string query = "select * from PatientTable";
            DataSet ds = p.ShowPatient(query);
            PatientDGV.DataSource = ds.Tables[0];
        }

        void reset()
        {
            PNameTb.Text = "";
            PTelefonTb.Text = "";
            PGeburtsdatumTb.Text = "";
            PGeschlechtTb.SelectedItem = "";
            PAdresseTb.Text = "";
            PAllergieTb.Text = "";

        }
        private void Patient_Load(object sender, EventArgs e)
        {
            eingetragene();
        }

        private void PatientDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PGeschlechtTb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int key = 0;
        private void PatientDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            PNameTb.Text = PatientDGV.SelectedRows[0].Cells[1].Value.ToString();
            PTelefonTb.Text= PatientDGV.SelectedRows[0].Cells[2].Value.ToString();
            PGeburtsdatumTb.Text= PatientDGV.SelectedRows[0].Cells[3].Value.ToString();
            PGeschlechtTb.SelectedItem= PatientDGV.SelectedRows[0].Cells[4].Value.ToString();
            PAdresseTb.Text= PatientDGV.SelectedRows[0].Cells[5].Value.ToString();
            PAllergieTb.Text= PatientDGV.SelectedRows[0].Cells[6].Value.ToString(); 
                
            if (PNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key= Convert.ToInt32(PatientDGV.SelectedRows[0].Cells[0].Value);
            }

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            DbPatient p = new DbPatient();
            if (key == 0)
            {
                MessageBox.Show("Wählen Sie den Patienten aus, den Sie löschen möchten");
            }
            else
            {
                try
                {
                    string query = "Delete  from PatientTable Where Id="+key+" ";
                    p.LoeschePatient(query);
                    MessageBox.Show("Ausgewählte Patient wurde erfolgreich gelöscht!");
                    eingetragene();
                    reset();
                }
                catch(Exception ex) {MessageBox.Show(ex.Message); }
            }
            
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            DbPatient p = new DbPatient();
            if (key == 0)
            {
                MessageBox.Show("Wählen Sie den Patienten aus, den Sie aktuallisieren möchten");
            }
            else
            {
                try
                {
                    string query = "Update PatientTable set PName='"+PNameTb.Text+
                                                        "',PTelefon= '"+PTelefonTb.Text+
                                                        "', PGeburtsdatum= '"+PGeburtsdatumTb.Text+
                                                        "', PGeschlecht= '"+PGeschlechtTb.SelectedItem.ToString()+
                                                        "', PAdresse= '"+PAdresseTb.Text+
                                                        "', PAlergie= '"+PAllergieTb.Text+
                                                        "' Where Id=" + key + ";";

                    p.LoeschePatient(query);
                    MessageBox.Show("Ausgewählte Patient wurde erfolgreich aktuallisiert!");
                    eingetragene();
                    reset();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Hide();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            Termin termin = new Termin();
            termin.Show();
            this.Hide();
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            Untersuchung untersuchung = new Untersuchung();
            untersuchung.Show();
            this.Hide();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            Rezepte rezepte = new Rezepte();
            rezepte.Show();
            this.Hide();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new ConnectionString().GetCon();
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select * from PatientTable Where PName like '%"+SucheTb.Text+"%'",connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            PatientDGV.DataSource= ds.Tables[0];
            connection.Close();
        }

        private void UpdateTb_Click(object sender, EventArgs e)
        {
            eingetragene();
            SucheTb.Text = "";
        }
    }
}
