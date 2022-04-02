using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class hopDongDAO
    {
        private static hopDongDAO instance;

        public static hopDongDAO Instance
        {
            get { if (instance == null) instance = new hopDongDAO(); return hopDongDAO.instance; }
            private set { hopDongDAO.instance = value; }
        }

        public object DataProvider { get; private set; }

        public hopDongDAO()
        { }

        public List<hopDong> getBillInfo()
        {
            string sql = "select * from hopDong";
            List<hopDong> hopDongList = new List<hopDong>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                hopDong hopDong = new hopDong(item);
                hopDongList.Add(hopDong);
            }
            return hopDongList;

        }
        public void insertBillInfo(int maThanhToan, int maSinhVien, int soSinhVien)
        {
            string sql = "insert into hopDong values('" + maThanhToan + "','" + maSinhVien + "','" + soSinhVien + "')";
            providerDAO.Instance.loadDL(sql);
        }

        public void updateCount(int maThanhToan)
        {
            string sql = "update hopDong set soSinhVien = soSinhVien + 1  where maThanhToan = " + maThanhToan;
            providerDAO.Instance.loadDL(sql);
        }

        public int checkIdFoodCount(int maThanhToan, int maSinhVien)
        {
            string sql = "select * from hopDong where maSinhVien = '" + maSinhVien + "' and maThanhToan = '" + maThanhToan + "'";
            DataTable data = providerDAO.Instance.loadDL(sql);
            return data.Rows.Count; /* kiểm tra xem nếu có sẽ trả dữ liệu => số dòng > 0 */
        }

    }
}
