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
        public bool INSERT(string diaChi, float gia)
        {
            string query = string.Format("Insert dbo.nhaTro (diaChi,gia,status) Values (N'{0}',{1},{2})", diaChi,gia,0);
            int result = providerDAO.Instance.ExecuteQuery(query);

            return result > 0;
            /*
            string query = string.Format ("Insert dbo.sinhVien (khoa,name,dienThoai,lop,queQuan,status) Values ({0},N'{1}',N'{2}',N'{3}',N'{4}',{5})",khoa,name,dienThoai,lop,queQuan,0);
            int result = providerDAO.Instance.ExecuteQuery(query);

            return result > 0;*/
        }
        public bool UPDATEINSERT(int maNhaTro, string diaChi, float gia, string status)
        {
            string query = "update nhaTro set diaChi = N'" + diaChi + "', gia =" + gia + ",status = N'" + status + "' WHERE maNhaTro = " + maNhaTro ;
            
            int result = providerDAO.Instance.ExecuteQuery(query);
            return result > 0;
        }
        
        public void checkInStatus(int id)
        {
            string sql = "update nhaTro set status = N'có người' where maNhaTro =" + id;
            providerDAO.Instance.loadDL(sql);
        }
        public void checkOutStatus(int id)
        {
            string sql = "update nhaTro set status = N'trống' where maNhaTro =" + id;
            providerDAO.Instance.loadDL(sql);
        }

        public List<nhaTro> SearchNhaTro(string name)
        {
            List<nhaTro> list = new List<nhaTro>();
            string sql = string.Format("select SV.maSinhVien, K.name as khoa, SV.name, SV.dienThoai, SV.lop,SV.queQuan, SV.status from sinhVien SV, khoa K where SV.Khoa = K.maKhoa  and SV.name = N'" + name + "'");
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                nhaTro NhaTro = new nhaTro(item);
                list.Add(NhaTro);
            }
            return list;
        }
        public void deletenhatro(int maNhaTro)
        {
            providerDAO.Instance.loadDL("delete nhaTro WHERE maNhaTro =" + maNhaTro);
        }
    }
}
