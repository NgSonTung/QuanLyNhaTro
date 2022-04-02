using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class countDAO
    {
        private static countDAO instance;

        public static countDAO Instance
        {
            get { if (instance == null) instance = new countDAO(); return countDAO.instance; }
            private set { countDAO.instance = value; }
        }

        public object DataProvider { get; private set; }

        public countDAO()
        { }

        public List<count> getSVCount(int maNhaTro)
        {
            string sql = "select HD.soSinhVien from hopDong HD, thanhToan TT, nhaTro NT where HD.maThanhToan = TT.maThanhToan and TT.maNhaTro = NT.maNhaTro and NT.status = N'có người' and TT.status = 0 and NT.maNhaTro = " + maNhaTro;
            List<count> countList = new List<count>();
            DataTable data = providerDAO.Instance.loadDL(sql);
            foreach (DataRow item in data.Rows)
            {
                count count = new count(item);
                countList.Add(count);
            }
            return countList;

        }
    }
}
