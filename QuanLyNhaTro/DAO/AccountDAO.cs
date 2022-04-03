using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class accountDAO
    {
        private static accountDAO instance;

        public static accountDAO Instance
        {
            get { if (instance == null) instance = new accountDAO(); return accountDAO.instance; }
            private set { accountDAO.instance = value; }
        }

        public accountDAO()
        { }

        public bool Login(string user, string password)
        {
            string sqlLogin = "select * from Account where UserName = '" + user + "' and PassWord = '" + password + "'";
            DataTable Result = providerDAO.Instance.loadDL(sqlLogin);
            return Result.Rows.Count > 0;
        }
    }
}
