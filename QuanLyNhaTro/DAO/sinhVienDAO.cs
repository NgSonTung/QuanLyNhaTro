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

        public List<lop> getLopSV(int maKhoa)
        {
            string sql = "select lop from sinhvien where status = 0 and khoa = "+ maKhoa +" group by lop order by cast(lop as int) asc ";
            DataTable data = providerDAO.Instance.loadDL(sql);
            List<lop> lopList = new List<lop>();
            foreach (DataRow item in data.Rows)
            {
                lop lop = new lop(item);
                lopList.Add(lop);
            }
            return lopList;
        }

        public List<sinhVien> getFood(int lop, int khoa)
        {
            string sql = "select SV.maSinhVien, K.name as khoa, SV.name, SV.dienThoai, SV.lop,SV.queQuan from sinhVien SV, khoa K where SV.khoa = K.maKhoa and SV.status = 0 and SV.lop = '"+ lop +"' and SV.khoa = " + khoa;
            List<sinhVien> sinhVienList = new List<sinhVien>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                sinhVien sinhVien = new sinhVien(item);
                sinhVienList.Add(sinhVien);
            }
            return sinhVienList;
        }
        public List<sinhVienList> getSinhVienByKhoa(int id)
        {
            string sql = "select SV.maSinhVien, K.name as khoa, SV.name, SV.dienThoai, SV.lop,SV.queQuan, SV.status from sinhVien SV, khoa K where SV.khoa = K.maKhoa and SV.khoa = " + id;
            List<sinhVienList> sinhVienList = new List<sinhVienList>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                sinhVienList sinhVienListItem = new sinhVienList(item);
                sinhVienList.Add(sinhVienListItem);
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

        public List<sinhVienList> GetListSinhVien()
        {
            string sql = "select SV.maSinhVien, K.name as khoa, SV.name, SV.dienThoai, SV.lop,SV.queQuan, SV.status from sinhVien SV, khoa K where SV.Khoa = K.maKhoa ";
            List<sinhVienList> sinhVienList = new List<sinhVienList>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                sinhVienList sinhVienListitem = new sinhVienList(item);
                sinhVienList.Add(sinhVienListitem);
            }
            return sinhVienList;
        }
         public bool INSERTSinhvieṇ̣̣(int khoa,string name,string dienThoai,string lop,string queQuan)
        {
            string query = string.Format ("Insert dbo.sinhVien (khoa,name,dienThoai,lop,queQuan,status) Values ({0},N'{1}',N'{2}',N'{3}',N'{4}',{5})",khoa,name,dienThoai,lop,queQuan,0);
            int result = providerDAO.Instance.ExecuteQuery(query);

            return result > 0;

        }
        public bool DELETESinhVien(int maSinhVien)
        {
            hopDongDAO.Instance.deleteinsert(maSinhVien);
            string query = "delete sinhVien where maSinhVien =" + maSinhVien;

            int result = providerDAO.Instance.ExecuteQuery(query);
            return result > 0;
        }
        public bool UPDATESinhVien(int maSinhVien, int khoa, string name, string dienThoai, string lop, string queQuan)
        {
            string query = string.Format("update SINHVIEN set khoa  = " + khoa + ",  NAME = N'" + name + "' ,dienThoai = '" + dienThoai + "',lop = N'" + lop + "',queQuan = N'" + queQuan + "' WHERE maSinhVien = ") + maSinhVien;

            int result = providerDAO.Instance.ExecuteQuery(query);
            return result > 0;
        }
        public List<sinhVien> SearchSinhVien(int maSoSinhVien, string name,string lop,string queQuan)
        {
            List<sinhVien> list = new List<sinhVien>();
            string sql =string.Format("select SV.maSinhVien, K.name as khoa, SV.name, SV.dienThoai, SV.lop,SV.queQuan, SV.status from sinhVien SV, khoa K where SV.Khoa = K.maKhoa  and SV.maSinhVien = "+ maSinhVien +" or SV.name = N'" + name +"' or SV.lop = N'"+ lop +"' or SV.queQuan = N'"+ queQuan"'");
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach(DataRow item in data.Rows)
            {
                sinhVien SinhVien = new sinhVien(item);
                list.Add(SinhVien);
            }
            return list;
        }

    }
}
