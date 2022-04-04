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
            string sql = "select SV.maSinhVien, K.name as khoa, SV.name, SV.dienThoai, SV.lop,SV.queQuan from sinhVien SV, khoa K where SV.khoa = K.maKhoa and SV.status = 0 and SV.khoa = " + id;
            List<sinhVien> sinhVienList = new List<sinhVien>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                sinhVien sinhVien = new sinhVien(item);
                sinhVienList.Add(sinhVien);
            }
            return sinhVienList;
        }
        public List<sinhVien> getSinhVienByKhoa(int id)
        {
            string sql = "select SV.maSinhVien, K.name as khoa, SV.name, SV.dienThoai, SV.lop,SV.queQuan from sinhVien SV, khoa K where SV.khoa = K.maKhoa and SV.khoa = " + id;
            List<sinhVien> sinhVienList = new List<sinhVien>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                sinhVien sinhVien = new sinhVien(item);
                sinhVienList.Add(sinhVien);
            }
            return sinhVienList;
        }
        public int getMaTTFromMaSV(int maSinhVien)
        {
            string sql = "select TT.maThanhToan from sinhVien SV, hopDong HD, thanhToan TT, nhaTro NT where SV.status = 1 and SV.maSinhVien = HD.maSinhVien and HD.maThanhToan = TT.maThanhToan and TT.maNhaTro = NT.maNhaTro and TT.status = 0 and SV.maSinhVien =" + maSinhVien;
            if (providerDAO.Instance.executeScalar(sql) != null)
            {
                return (int)providerDAO.Instance.executeScalar(sql);
            }
            else return -1;
        }

        public int getMaNTFromMaSV(int maSinhVien)
        {
            string sql = "select TT.maNhaTro from sinhVien SV, hopDong HD, thanhToan TT, nhaTro NT where SV.status = 1 and SV.maSinhVien = HD.maSinhVien and HD.maThanhToan = TT.maThanhToan and TT.maNhaTro = NT.maNhaTro and TT.status = 0 and SV.maSinhVien =" + maSinhVien;
            if (providerDAO.Instance.executeScalar(sql) != null)
            {
                return (int)providerDAO.Instance.executeScalar(sql);
            }
            else return -1;
        }

        public void statusCoTro(int maSinhVien)
        {
            string sql = "UPDATE sinhvien SET status = 1 WHERE status = 0 and maSinhVien =" + maSinhVien;
            providerDAO.Instance.loadDL(sql);
        }

        public void statusKhongTro(int maSinhVien)
        {
            string sql = "UPDATE sinhvien SET status = 0 WHERE status = 1 and maSinhVien =" + maSinhVien;
            providerDAO.Instance.loadDL(sql);
        }

        public List<sinhVien> GetListSinhVien()
        {
            string sql = "select SV.maSinhVien, K.name as khoa, SV.name, SV.dienThoai, SV.lop,SV.queQuan from sinhVien SV, khoa K where SV.Khoa = K.maKhoa ";
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
