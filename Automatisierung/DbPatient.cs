using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatisierung
{
    internal class DbPatient
    {
        public void PatientEinfügen(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection connection= MyConnection.GetCon();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void LoeschePatient(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection connection = MyConnection.GetCon();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdatePatient(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection connection = MyConnection.GetCon();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public DataSet ShowPatient(string query)
        {
            ConnectionString MyConnection = new ConnectionString();
            SqlConnection connection = MyConnection.GetCon();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }
    }
}
