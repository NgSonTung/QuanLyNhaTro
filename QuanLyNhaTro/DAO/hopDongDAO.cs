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
        public void deleteinsert(int maHopDong)
        {
            providerDAO.Instance.loadDL("delete hopDong WHERE maHopDong =" + maHopDong);
        }

        public void deleteSinhvien(int maThanhToan)
        {
            providerDAO.Instance.loadDL("delete hopDong WHERE maThanhToan =" + maThanhToan);
        }
        public List<hopDong> getBillInfo()
        {
            string query = "select * from hopDong";
            List<hopDong> hopDongList = new List<hopDong>();
            DataTable data = providerDAO.Instance.loadDL(query);
            foreach (DataRow item in data.Rows)
            {
                hopDong hopDong = new hopDong(item);
                hopDongList.Add(hopDong);
            }
            return hopDongList;

        }
        public void insertBillInfo(int maThanhToan, int maSinhVien, int soSinhVien)
        {
            string query = "insert into hopDong values('" + maThanhToan + "','" + maSinhVien + "','" + soSinhVien + "')";
            providerDAO.Instance.loadDL(query);
        }

        public void updateHopDong1(int maThanhToan,int maSinhVien)
        {
            string query = "insert into hopDong values(" + maThanhToan + ",'" + maSinhVien + "',1)";
            providerDAO.Instance.loadDL(query);
        }

        public void updateHopDong2(int maThanhToan, int maSinhVien, int soSinhVien)
        {
            string query = "insert into hopDong values (" + maThanhToan + ","+ maSinhVien +", " + soSinhVien + ")";
            providerDAO.Instance.loadDL(query);
        }

        public void updateCountUp(int maThanhToan)
        {
            string query = "update hopDong set soSinhVien = soSinhVien + 1  where maThanhToan = " + maThanhToan;
            providerDAO.Instance.loadDL(query);
        }

        public void updateCountDown(int maThanhToan)
        {
            string query = "update hopDong set soSinhVien = soSinhVien - 1  where maThanhToan = " + maThanhToan;
            providerDAO.Instance.loadDL(query);
        }
        public void count(int maHopDong)
        {
            string query = "select soLuongSinhVien from hopDong where maHopDong = "+ maHopDong;
            providerDAO.Instance.loadDL(query);
        }

        public int checkIdFoodCount(int maThanhToan, int maSinhVien)
        {
            string query = "select * from hopDong where maSinhVien = '" + maSinhVien + "' and maThanhToan = '" + maThanhToan + "'";
            DataTable data = providerDAO.Instance.loadDL(query);
            return data.Rows.Count; /* kiểm tra xem nếu có sẽ trả dữ liệu => số dòng > 0 */
        }

        public void autoDeleteHopDong()
        {
            string query = "DELETE FROM hopDong WHERE soSinhVien = 0 ";
            providerDAO.Instance.loadDL(query);
        }

    }
}
