using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class nhaTroDAO
    {
        private static nhaTroDAO instance;

        public static nhaTroDAO Instance
        {
            get { if (instance == null) instance = new nhaTroDAO(); return nhaTroDAO.instance; }
            private set { nhaTroDAO.instance = value; }
        }

        public object DataProvider { get; private set; }

        public nhaTroDAO()
        { }

        public List<nhaTro> getTable()
        {
            string sql = "select * from nhaTro";
            List<nhaTro> listTable1 = new List<nhaTro>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                nhaTro table = new nhaTro(item);
                listTable1.Add(table);
            }
            return listTable1;

        }
        public void checkInStatus(int id)
        {
            string sql = "update nhaTro set status = N'có người' where id =" + id;
            providerDAO.Instance.loadDL(sql);
        }
        public void checkOutStatus(int id)
        {
            string sql = "update nhaTro set status = N'trống' where id =" + id;
            providerDAO.Instance.loadDL(sql);
        }
    }
}
