using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Automatisierung
{
    internal class ConnectionString
    {
        public SqlConnection GetCon()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\hkart\OneDrive - WBSEDU\Desktop\C#\Automatisierung\Automatisierung\Zahnpraxis.mdf"";Integrated Security=True";
            return connection;
        }
    }
}
