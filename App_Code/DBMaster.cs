using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StormWeb.App_Code
{
    public class DBMaster
    {
        private SqlConnection sqlConn;

        public SqlConnection getConnection()
        {
            sqlConn = new SqlConnection("Server=tcp:cpftestingserver.database.windows.net,1433;Initial Catalog=StormWebDB;Persist Security Info=False;User ID=cpfadmin;Password=p@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            sqlConn.Open();
            return sqlConn;
        }

        public SqlDataReader getReader(string query) {
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = this.getConnection();

            SqlDataReader reader = cmd.ExecuteReader();
            return reader;

        }

        public void closeConnection() {
            if (sqlConn != null && sqlConn.State == System.Data.ConnectionState.Open) {
                this.sqlConn.Close();
            }
        }
            
            
    }

    public class TagHandler
    {

        private Dictionary<string, string[]> URLTagSet =
        new Dictionary<string, string[]>();


        public int differenceIndex(string[] arr0, string[] arr1)
        {
            string[] DifferArray = arr0.Except(arr1).ToArray();
            return DifferArray.Length;
        }

        public void populateURLTagSet(DBMaster dbmaster)
        {


        }
    }
}