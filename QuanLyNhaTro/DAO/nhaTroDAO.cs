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

        public int checkNhaTro(int maNhaTro)
        {
            string sql = "select limit from nhaTro where maNhaTro = " + maNhaTro;
            if (providerDAO.Instance.executeScalar(sql) != null)
            {
                return (int)providerDAO.Instance.executeScalar(sql);
            }
            else
                return 0;
        }

        public List<nhaTro> getTable()
        {
            string sql = "select distinct NT.maNhaTro, NT.maChuNha, NT.diaChi, NT.gia, NT.limit , NT.status, HDTT.soSinhVien from nhaTro NT left join ( select HD.soSinhVien ,TT.maNhaTro from hopDong HD, thanhToan TT where HD.maThanhToan = TT.maNhaTro and TT.status = 0 ) HDTT on NT.maNhaTro = HDTT.maNhaTro";
            List<nhaTro> listTable1 = new List<nhaTro>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                nhaTro table = new nhaTro(item);
                listTable1.Add(table);
            }
            return listTable1;

        }
        public bool INSERT(string diaChi, float gia, string status)
        {
            string query = "INSERT nhaTro VALUES (N'" + diaChi + "'," + gia + ", N'"+ status +"')";
            int result = providerDAO.Instance.ExecuteQuery(query);
            return result > 0;
        }
        public bool UPDATEINSERT(int maNhaTro, string diaChi, float gia, string status)
        {
            string query = "update nhaTro set diaChi = N'" + diaChi + "', gia =" + gia + ",status = N'" + status + "' WHERE maNhaTro = " + maNhaTro ;
            
            int result = providerDAO.Instance.ExecuteQuery(query);
            return result > 0;
        }
        public bool DELETEINSERT(int maNhaTro)
        {
            hopDongDAO.Instance.deleteinsert(maNhaTro); 
            string query = "delete nhaTro where maNhaTro =" + maNhaTro;

            int result = providerDAO.Instance.ExecuteQuery(query);
            return result > 0;
        }
        public void fullStatus(int id)
        {
            string sql = "update nhaTro set status = N'hết chỗ' where maNhaTro =" + id;
            providerDAO.Instance.loadDL(sql);
        }
        public void availStatus(int id)
        {
            string sql = "update nhaTro set status = N'còn chỗ' where maNhaTro =" + id;
            providerDAO.Instance.loadDL(sql);
        }
    }
}
