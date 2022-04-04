using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class providerDAO
    {
        private static providerDAO instance;

        public static providerDAO Instance
        {
            get { if (instance == null) instance = new providerDAO(); return providerDAO.instance; }
            private set { providerDAO.instance = value; }
        }

        public providerDAO()
        { }

        string connectSTR = "Data Source=LAPTOP-JME65UMO\\TAMDEN123;Initial Catalog=doAn;Integrated Security=True";

        public DataTable loadDL(string command)
        {
            SqlConnection connect = new SqlConnection(connectSTR);
            connect.Open();
            SqlCommand cmd = new SqlCommand(command, connect);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            connect.Close();
            return dt;

        }

        public object executeScalar(string query)
        {
            object dt = 0;
            SqlConnection connect = new SqlConnection(connectSTR);
            connect.Open();
            SqlCommand cmd = new SqlCommand(query, connect);
            dt = cmd.ExecuteScalar();
            connect.Close();
            return dt;
        }
        public int ExecuteNonQuery(string query)
        {
            int dt = 0;
            SqlConnection connect = new SqlConnection(connectSTR);
            connect.Open();
            SqlCommand cmd = new SqlCommand(query, connect);
            dt = cmd.ExecuteNonQuery();
            connect.Close();
            return dt;
        }
    }
}
