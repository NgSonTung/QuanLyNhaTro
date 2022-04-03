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
            string sql = "select SV.maSinhVien, K.name as khoa, SV.name, SV.dienThoai, SV.lop,SV.queQuan from sinhVien SV, khoa K where SV.khoa = K.maKhoa and SV.status = 0 and SV.khoa = " + id ;
            List<sinhVien> sinhVienList = new List<sinhVien>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                sinhVien sinhVien = new sinhVien(item);
                sinhVienList.Add(sinhVien); 
            }
            return sinhVienList;

        }
        public void statusCoTro(int maSinhVien)
        {
            string sql = "UPDATE sinhvien SET status = 1 WHERE status = 0 and maSinhVien ="+ maSinhVien;
            providerDAO.Instance.loadDL(sql);
        }

        public void statusKhongTro(int maSinhVien)
        {
            string sql = "UPDATE sinhvien SET status = 0 WHERE status = 1 and maSinhVien =" + maSinhVien;
            providerDAO.Instance.loadDL(sql);
        }
    }
}
