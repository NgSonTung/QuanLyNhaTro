using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class chuyenTroDAO
    {
        private static chuyenTroDAO instance;

        public static chuyenTroDAO Instance
        {
            get { if (instance == null) instance = new chuyenTroDAO(); return chuyenTroDAO.instance; }
            private set { chuyenTroDAO.instance = value; }
        }

        public object DataProvider { get; private set; }

        public chuyenTroDAO()
        { }

        public List<chuyenTro> getChuyenTro(int id)
        {
            string sql = "select SV.maSinhVien, SV.name, HD.mahopdong, TT.mathanhtoan,TT.manhatro from sinhVien SV, hopDong HD, thanhToan TT where SV.maSinhVien = HD.maSinhVien and SV.status = 1 and TT.status = 0 and HD.maThanhToan = TT.maThanhToan and TT.maNhaTro =" + id;
            List<chuyenTro> chuyenTroList = new List<chuyenTro>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                chuyenTro chuyenTro = new chuyenTro(item);
                chuyenTroList.Add(chuyenTro);
            }
            return chuyenTroList;

        }
    }
}
