using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
