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
    public class historyDAO
    {
        private static historyDAO instance;

        public static historyDAO Instance
        {
            get { if (instance == null) instance = new historyDAO(); return historyDAO.instance; }
            private set { historyDAO.instance = value; }
        }

        public object DataProvider { get; private set; }

        public historyDAO()
        { }

        public List<history> getHistory(string checkIn, string checkOut)
        {
            string query = "select SV.maSinhVien, SV.name, TT.dateCheckIn, TT.dateCheckOut, NT.diaChi from thanhtoan TT, hopDong HD, sinhvien SV, nhaTro NT where SV.maSinhVien = HD.maSinhVien and HD.maThanhToan = TT.maThanhToan and TT.maNhaTro = NT.maNhaTro and TT.dateCheckIn >= '"+ checkIn +"' and TT.dateCheckOut <= '"+checkOut+ "' or SV.maSinhVien = HD.maSinhVien and HD.maThanhToan = TT.maThanhToan and TT.maNhaTro = NT.maNhaTro and TT.dateCheckIn >= '" + checkIn + "' and TT.dateCheckOut is null order by TT.dateCheckOut desc";
            List<history> historyList = new List<history>();
            DataTable data = providerDAO.Instance.loadDL(query);  
            foreach (DataRow item in data.Rows)
            {   
                history history = new history(item);
                historyList.Add(history);
            }
            return historyList;

        }

        public List<history> searchHistory(string searchItem,string checkIn, string checkOut)
        {
            List<history> list = new List<history>();
            List<history> listTemp = getHistory(checkIn, checkOut);
            string cmdTenSV = "select SV.maSinhVien, SV.name, TT.dateCheckIn, TT.dateCheckOut, NT.diaChi from thanhtoan TT, hopDong HD, sinhvien SV, nhaTro NT where SV.maSinhVien = HD.maSinhVien and HD.maThanhToan = TT.maThanhToan and TT.maNhaTro = NT.maNhaTro and dbo.fuConvertToUnsign1(sv.name) LIKE N'%' + dbo.fuConvertToUnsign1 (N'"+searchItem+"') + '%' order by TT.dateCheckOut desc ";
            string cmdMSSV = "select SV.maSinhVien, SV.name, TT.dateCheckIn, TT.dateCheckOut, NT.diaChi from thanhtoan TT, hopDong HD, sinhvien SV, nhaTro NT where SV.maSinhVien = HD.maSinhVien and HD.maThanhToan = TT.maThanhToan and TT.maNhaTro = NT.maNhaTro and dbo.fuConvertToUnsign1(sv.maSinhVien) LIKE N'%' + dbo.fuConvertToUnsign1 (N'" + searchItem + "') + '%' order by TT.dateCheckOut desc ";
            DataTable data;
            if (!int.TryParse(searchItem, out int _))
            {
                data = providerDAO.Instance.loadDL(cmdTenSV);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("Không có kết quả", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                data = providerDAO.Instance.loadDL(cmdMSSV);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("Không có kết quả", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (data.Rows.Count != 0)
            {
                foreach (DataRow item in data.Rows)
                {
                    history history = new history(item);
                    list.Add(history);
                }
                return list;
            }
            else
                return listTemp;
        }
    }
}
