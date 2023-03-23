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
    public partial class Rezepte : Form
    {
        public Rezepte()
        {
            InitializeComponent();
        }
        ConnectionString MyCon = new ConnectionString();

        private void RufPatient()
        {
            SqlConnection connection = MyCon.GetCon();
            connection.Open();
            SqlCommand cmd = new SqlCommand("select PName from PatientTable", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PName", typeof(string));
            dt.Load(reader);

            RNameCb.ValueMember = "PName";
            RNameCb.DataSource = dt;
            connection.Close();
        }
        private void RufUntersuchung()
        {
            SqlConnection connection = MyCon.GetCon();
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from TerminTable where Patient= '"+RNameCb.SelectedValue.ToString()+"'", connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                RUntersuchungTb.Text= dr["Untersuchung"].ToString();
            }
            connection.Close();
        }
        private void RufPreis()
        {
            SqlConnection connection = MyCon.GetCon();
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from UntersuchungTable where UName= '" + RUntersuchungTb.Text+ "'", connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                RPreisTb.Text = dr["UPreis"].ToString();
            }
            connection.Close();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        void eingetragene()
        {
            DbPatient p = new DbPatient();
            string query = "select * from RezepteTable";
            DataSet ds = p.ShowPatient(query);
            RezeptDGV.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            RUntersuchungTb.Text = " ";
            RPreisTb.Text = " ";
            RMedikamenteTb.Text = " ";
            RMengeTb.Text = " ";
            RNameCb.SelectedValue = "  ";
        }
        private void Rezepte_Load(object sender, EventArgs e)
        {
            RufPatient();
            eingetragene();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RNameCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RufUntersuchung();
            RufPreis();
        }

        private void label1_Click(object sender, EventArgs e)
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

        private void label4_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Hide();
        }

        private void guna2GradientButton10_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();
            patient.Show();
            this.Hide();
        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            Termin termin = new Termin();
            termin.Show();
            this.Hide();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            Untersuchung untersuchung = new Untersuchung();
            untersuchung.Show();
            this.Hide();
        }

       

        private void SucheBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new ConnectionString().GetCon();
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select * from RezepteTable Where PatientName like '%" + SucheTb.Text + "%'", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            RezeptDGV.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            eingetragene();
            SucheTb.Text = "";
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            String query = "insert into RezepteTable values('" + RNameCb.SelectedValue.ToString() + "'," +
                                                             "'" + RUntersuchungTb.Text+ "'," +
                                                             "'" + RPreisTb.Text + "'," +
                                                             "'" + RMedikamenteTb.Text + "'," +
                                                             "'" + RMengeTb.Text + "')";
            DbPatient dbPatient = new DbPatient();
            try
            {
                dbPatient.PatientEinfügen(query);
                MessageBox.Show("Rezept wurde erfolgreich hinzugefügt!");
                eingetragene();
                Reset();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int key = 0;
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            DbPatient p = new DbPatient();
            if (key == 0)
            {
                MessageBox.Show("Wählen Sie den Rezept aus, den Sie aktuallisieren möchten");
            }
            else
            {
                try
                {
                    string query = "Update RezepteTable set PatientName='" + RNameCb.SelectedValue.ToString() +
                                                        "', Untersuchung= '" + RUntersuchungTb.Text+
                                                        "', UntersuchungPreis= '" + RPreisTb.Text +
                                                        "', Medikamente= '" + RMedikamenteTb.Text +
                                                        "', Menge= '" + RMengeTb.Text +
                                                        "' Where Rez_Id=" + key + ";";

                    p.LoeschePatient(query);
                    MessageBox.Show("Ausgewählte Rezept wurde erfolgreich aktuallisiert!");
                    eingetragene();
                    Reset();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            DbPatient p = new DbPatient();
            if (key == 0)
            {
                MessageBox.Show("Wählen Sie den Rezept aus, den Sie löschen möchten");
            }
            else
            {
                try
                {
                    string query = "Delete  from RezepteTable Where Rez_Id=" + key + " ";
                    p.LoeschePatient(query);
                    MessageBox.Show("Ausgewählte Rezept wurde erfolgreich gelöscht!");
                    eingetragene();
                    Reset();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void RezeptDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RNameCb.SelectedValue = RezeptDGV.SelectedRows[0].Cells[1].Value.ToString();
            RUntersuchungTb.Text = RezeptDGV.SelectedRows[0].Cells[2].Value.ToString();
            RPreisTb.Text = RezeptDGV.SelectedRows[0].Cells[3].Value.ToString();
            RMedikamenteTb.Text = RezeptDGV.SelectedRows[0].Cells[4].Value.ToString();
            RMengeTb.Text = RezeptDGV.SelectedRows[0].Cells[5].Value.ToString();
            

            if (RNameCb.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(RezeptDGV.SelectedRows[0].Cells[0].Value);
            }
        }

        private void RUntersuchungTb_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bitmap = new Bitmap(this.RezeptDGV.Width,this.RezeptDGV.Height);
            RezeptDGV.DrawToBitmap(bitmap, new Rectangle(0,0,this.RezeptDGV.Width, this.RezeptDGV.Height));
            e.Graphics.DrawImage(bitmap, 90, 100);
            e.Graphics.DrawString(label4.Text, new Font("Verdana", 16, FontStyle.Bold), Brushes.CadetBlue, new Point(250, 50));
        }
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
    }
}
