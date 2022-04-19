using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class chunhaDAO
    {
        private static chunhaDAO instance;

        public static chunhaDAO Instance
        {
            get { if (instance == null) instance = new chunhaDAO(); return chunhaDAO.instance; }
            private set { chunhaDAO.instance = value; }
        }
        public object DataProvider { get; private set; }
        public chunhaDAO()
        { }
        public List<chuNha> chunha()
        {
            string query = "select * from chuNha";
            List<chuNha> chuNhaList = new List<chuNha>();
            DataTable data = providerDAO.Instance.loadDL(query);
            foreach (DataRow item in data.Rows)
            {
                chuNha chuNha = new chuNha(item);
                chuNhaList.Add(chuNha);
            }
            return chuNhaList;

        }
    }
    
}
