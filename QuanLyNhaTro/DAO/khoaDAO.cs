using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class khoaDAO
    {
        private static khoaDAO instance;

        public static khoaDAO Instance
        {
            get { if (instance == null) instance = new khoaDAO(); return khoaDAO.instance; }
            private set { khoaDAO.instance = value; }
        }

        public object DataProvider { get; private set; }

        public khoaDAO()
        { }

        public List<khoa> getCategory()
        {
            string sql = "select * from khoa";
            List<khoa> khoaList = new List<khoa>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                khoa khoa = new khoa(item);
                khoaList.Add(khoa);
            }
            return khoaList;

        }
    }
}
