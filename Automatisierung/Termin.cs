using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automatisierung
{
    public partial class Termin : Form
    {
        public Termin()
        {
            InitializeComponent();
        }
        ConnectionString MyCon = new ConnectionString();

        private void RufPatient()
        {
            SqlConnection connection = MyCon.GetCon();
            connection.Open();
            SqlCommand cmd = new SqlCommand("select PName from PatientTable",connection);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PName",typeof(string));
            dt.Load(reader);

            TNameCb.ValueMember = "PName";
            TNameCb.DataSource = dt;
            connection.Close();
        }
        private void RufUntersuchung()
        {
            SqlConnection connection = MyCon.GetCon();
            connection.Open();
            SqlCommand cmd = new SqlCommand("select UName from UntersuchungTable", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("UName", typeof(string));
            dt.Load(reader);

            TUntersuchungCb.ValueMember = "UName";
            TUntersuchungCb.DataSource = dt;
            connection.Close();
        }

        private void Termin_Load(object sender, EventArgs e)
        {
            RufPatient();
            RufUntersuchung();
            Eingetragene();
        }
        void Eingetragene()
        {
            DbPatient p = new DbPatient();
            string query = "select * from TerminTable";
            DataSet ds = p.ShowPatient(query);
            TerminDGV.DataSource = ds.Tables[0];
        }
        void Reset()
        {
            TNameCb.SelectedValue = "";
            TUntersuchungCb.SelectedValue = "";
            TStundeCb.Text = "";
            TDatumCb.Text = "";

        }
        int key = 0;
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            DbPatient p = new DbPatient();
            if (key == 0)
            {
                MessageBox.Show("Wählen Sie den Termin aus, den Sie aktuallisieren möchten");
            }
            else
            {
                try
                {
                    string query = "Update TerminTable set Patient='" + TNameCb.SelectedValue.ToString() +
                                                        "',Untersuchung= '" + TUntersuchungCb.SelectedValue.ToString() +
                                                        "', TDatum= '" + TDatumCb.Text +
                                                        "', TStunde= '" + TStundeCb.Text +
                                                        "' Where TId=" + key + ";";

                    p.LoeschePatient(query);
                    MessageBox.Show("Ausgewählte Termin wurde erfolgreich aktuallisiert!");
                    Eingetragene();
                    Reset();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            String query = "insert into TerminTable values('" + TNameCb.SelectedValue.ToString() + "'," +
                                                              "'" + TUntersuchungCb.SelectedValue.ToString() + "'," +
                                                              "'" + TDatumCb.Text + "'," +
                                                              "'" + TStundeCb.Text
                                                              + "')";
            DbPatient dbPatient = new DbPatient();
            try
            {
                dbPatient.PatientEinfügen(query);
                MessageBox.Show("Termin wurde erfolgreich hinzugefügt!");
                Eingetragene();
                Reset();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TerminDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TNameCb.SelectedValue = TerminDGV.SelectedRows[0].Cells[1].Value.ToString();
            TUntersuchungCb.SelectedValue = TerminDGV.SelectedRows[0].Cells[2].Value.ToString();
            TDatumCb.Text = TerminDGV.SelectedRows[0].Cells[3].Value.ToString();
            TStundeCb.Text = TerminDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (TNameCb.SelectedIndex == -1)
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(TerminDGV.SelectedRows[0].Cells[0].Value);
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            DbPatient p = new DbPatient();
            if (key == 0)
            {
                MessageBox.Show("Wählen Sie den Termin aus, den Sie löschen möchten");
            }
            else
            {
                try
                {
                    string query = "Delete  from TerminTable Where TId=" + key + " ";
                    p.LoeschePatient(query);
                    MessageBox.Show("Ausgewählte Termin wurde erfolgreich gelöscht!");
                    Eingetragene();
                    Reset();
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

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            Patient patient = new Patient();
            patient.Show();
            this.Hide();
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            Untersuchung untersuchung = new Untersuchung();
            untersuchung.Show();
            this.Hide();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            Rezepte rezepte = new Rezepte();
            rezepte.Show();
            this.Hide();
        }

        private void SucheBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new ConnectionString().GetCon();
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select * from TerminTable Where Patient like '%" + SucheTb.Text + "%'", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            TerminDGV.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            Eingetragene();
            SucheTb.Text = "";
        }
    }
}
