using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class thanhToanDAO
    {
        private static thanhToanDAO instance;

        public static thanhToanDAO Instance
        {
            get { if (instance == null) instance = new thanhToanDAO(); return thanhToanDAO.instance; }
            private set { thanhToanDAO.instance = value; }
        }

        public object DataProvider { get; private set; }

        public thanhToanDAO()
        { }

        public int getUncheckBill(int id)
        {
            string sql = "select * from thanhToan where status = 0 and maNhaTro = " + id;
            DataTable data = providerDAO.Instance.loadDL(sql);
            if (data.Rows.Count > 0)
            {
                thanhToan bill = new thanhToan(data.Rows[0]);
                return bill.MaThanhToan;
            } else
            return -1;
        }
        public int getMaxID()
        {
            string sql = "select max(maThanhToan) from thanhToan";
            return (int)providerDAO.Instance.executeScalar(sql);
        }
        public int getMaThanhToan(int maNhaTro)
        {
            string sql = "select maThanhToan from thanhToan where status = 0 and maNhaTro = " + maNhaTro;
            return (int)providerDAO.Instance.executeScalar(sql);
        }
        public void insertBill(int id)
        {
            string sql = "insert into thanhToan(datecheckin,maNhaTro,status) values (getdate()," + id + ",0)";
            providerDAO.Instance.loadDL(sql);
        }
        public void chuyenBill (int maNhaTroMoi, int maThanhToan)
        {
            string sql = "UPDATE thanhToan SET maNhaTro = "+ maNhaTroMoi + " WHERE status = 0 and maThanhToan = " + maThanhToan;
            providerDAO.Instance.loadDL(sql);
        }
        public void checkOut(int id)
        {
            string sql = "Update thanhToan set status = 1, dateCheckOut = GETDATE()  where maThanhToan =" + id;
            providerDAO.Instance.loadDL(sql);
        }
        public void DeletethanhToan(int maNhaTro)
        {
/*            hopDongDAO.Instance.deleteSinhvien(maThanhToan); 
            string query = "delete thanhToan where maNhaTro =" + maNhaTro;

            int result = providerDAO.Instance.ExecuteQuery(query);
            return result > 0;*/
        }

    }
}
