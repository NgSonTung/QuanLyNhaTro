using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class sinhVienDAO
    {
        private static sinhVienDAO instance;

        public static sinhVienDAO Instance
        {
            get { if (instance == null) instance = new sinhVienDAO(); return sinhVienDAO.instance; }
            private set { sinhVienDAO.instance = value; }
        }

        public object DataProvider { get; private set; }

        public sinhVienDAO()
        { }

        public List<sinhVien> getFood(int id)
        {
            string sql = "select * from sinhVien where khoa = " + id;
            List<sinhVien> sinhVienList = new List<sinhVien>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                sinhVien sinhVien = new sinhVien(item);
                sinhVienList.Add(sinhVien);
            }
            return sinhVienList;

        }
    }
}
