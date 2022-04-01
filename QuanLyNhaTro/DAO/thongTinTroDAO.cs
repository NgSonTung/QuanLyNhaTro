using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class thongTinTroDAO
    {
        private static thongTinTroDAO instance;

        public static thongTinTroDAO Instance
        {
            get { if (instance == null) instance = new thongTinTroDAO(); return thongTinTroDAO.instance; }
            private set { thongTinTroDAO.instance = value; }
        }

        public List<thongTinTro> getMenu(int id)
        {
            string sql = "select SV.name, SV.dienThoai, NT.gia, TT.dateCheckIn, NT.gia / HD.soSinhVien as tienNha from sinhVien SV, hopDong HD, thanhToan TT, nhaTro NT where SV.maSinhVien = HD.maSinhVien and HD.maThanhToan = TT.maThanhToan and TT.status = 0 and TT.maThanhToan = NT.maNhaTro and NT.maNhaTro = " + id;
            List<thongTinTro> thongTinTroList = new List<thongTinTro>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                thongTinTro thongTinTro = new thongTinTro(item);
                thongTinTroList.Add(thongTinTro);
            }
            return thongTinTroList;

        }
    }
}
