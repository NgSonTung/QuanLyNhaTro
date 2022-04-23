using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public List<nhaTroList> getTable2()
        {
            string sql = "select distinct NT.maNhaTro, CN.ten, NT.diaChi, NT.gia, NT.limit , NT.status, HDTT.soSinhVien, CN.sdt, CN.diaChiChu from chuNha CN, nhaTro NT left join ( select HD.soSinhVien ,TT.maNhaTro from hopDong HD, thanhToan TT where HD.maThanhToan = TT.maNhaTro and TT.status = 0 ) HDTT on NT.maNhaTro = HDTT.maNhaTro where NT.maChuNha = CN.maChuNha";
            List<nhaTroList> listTable1 = new List<nhaTroList>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                nhaTroList table = new nhaTroList(item);
                listTable1.Add(table);
            }
            return listTable1;
        }
        public bool insertNhaTro(int maChuNha,string diaChi, float gia, int limit)
        {
            string query = "insert into nhaTro values("+maChuNha+", N'"+diaChi+"', "+gia+", "+limit+", N'còn chỗ')";
            int result = providerDAO.Instance.ExecuteQuery(query);

            return result > 0;
        }
        public bool UPDATEINSERT(int maNhaTro, int maChuNha, string diaChi, float gia, int limit)
        {
            string query = "update nhaTro set maChuNha = "+maChuNha+", diaChi = N'" + diaChi + "', gia =" + gia +", limit = "+ limit  + " WHERE maNhaTro = " + maNhaTro ;
            int result = providerDAO.Instance.ExecuteQuery(query);
            return result > 0;
        }
        public bool deleteNhaTro(int maNhaTro)
        {
            string query = "delete nhaTro where maNhaTro =" + maNhaTro;
            int result = providerDAO.Instance.ExecuteQuery(query);
            return result > 0;
        }
        public void fullStatus(int maNhaTro)
        {
            string sql = "update nhaTro set status = N'hết chỗ' where maNhaTro =" + maNhaTro;
            providerDAO.Instance.loadDL(sql);
        }
        public void availStatus(int maNhaTro)
        {
            string sql = "update nhaTro set status = N'còn chỗ' where maNhaTro =" + maNhaTro;
            providerDAO.Instance.loadDL(sql);
        }

        public List<nhaTroList> SearchNhaTro(string searchItem)
        {
            List<nhaTroList> list = new List<nhaTroList>();
            List<nhaTroList> listTemp = getTable2();
            string cmdDiaChi = "select distinct NT.maNhaTro, CN.ten, NT.diaChi, NT.gia, NT.limit , NT.status, HDTT.soSinhVien, CN.sdt from chuNha CN, nhaTro NT left join ( select HD.soSinhVien ,TT.maNhaTro from hopDong HD, thanhToan TT where HD.maThanhToan = TT.maNhaTro and TT.status = 0 ) HDTT on NT.maNhaTro = HDTT.maNhaTro where NT.maChuNha = CN.maChuNha and dbo.fuConvertToUnsign1 (nt.diachi) LIKE N'%' + dbo.fuConvertToUnsign1 (N'" + searchItem + "') + '%' ";
            string cmdChuNha = "select distinct NT.maNhaTro, CN.ten, NT.diaChi, NT.gia, NT.limit , NT.status, HDTT.soSinhVien, CN.sdt from chuNha CN, nhaTro NT left join ( select HD.soSinhVien ,TT.maNhaTro from hopDong HD, thanhToan TT where HD.maThanhToan = TT.maNhaTro and TT.status = 0 ) HDTT on NT.maNhaTro = HDTT.maNhaTro where NT.maChuNha = CN.maChuNha and dbo.fuConvertToUnsign1 (CN.ten) LIKE N'%' + dbo.fuConvertToUnsign1 (N'" + searchItem + "') + '%' ";
            string cmdSDT = "select distinct NT.maNhaTro, CN.ten, NT.diaChi, NT.gia, NT.limit , NT.status, HDTT.soSinhVien, CN.sdt from chuNha CN, nhaTro NT left join ( select HD.soSinhVien ,TT.maNhaTro from hopDong HD, thanhToan TT where HD.maThanhToan = TT.maNhaTro and TT.status = 0 ) HDTT on NT.maNhaTro = HDTT.maNhaTro where NT.maChuNha = CN.maChuNha and CN.sdt like N'%' + dbo.fuConvertToUnsign1 (" + searchItem + ") + '%' ";
            DataTable data;
            if (!int.TryParse(searchItem, out int _))
            {
                data = providerDAO.Instance.loadDL(cmdDiaChi);
                if (data.Rows.Count == 0)
                {
                    data = providerDAO.Instance.loadDL(cmdChuNha);
                    if (data.Rows.Count == 0)
                        MessageBox.Show("Không có kết quả", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                data = providerDAO.Instance.loadDL(cmdSDT);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("Không có kết quả", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (data.Rows.Count != 0)
            {
                foreach (DataRow item in data.Rows)
                {
                    nhaTroList nhaTro = new nhaTroList(item);
                    list.Add(nhaTro);
                }
                return list;
            }
            else
                return listTemp;
        }
        /*public bool deletenhatro(int maNhaTro)
        {
            thanhToanDAO.Instance.DeletethanhToan(maNhaTro); 
            string query = "delete nhaTro where maNhaTro =" + maNhaTro;

            int result = providerDAO.Instance.ExecuteQuery(query);
            return result > 0;
        }*/
    }
}
