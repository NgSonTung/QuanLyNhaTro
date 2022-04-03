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

        public int getSVCount(int maNhaTro)
        {
            string sql = "select HD.soSinhVien from hopDong HD, thanhToan TT, nhaTro NT where HD.maThanhToan = TT.maThanhToan and TT.maNhaTro = NT.maNhaTro and NT.status = N'có người' and TT.status = 0 and TT.maNhaTro = " + maNhaTro;
            if (providerDAO.Instance.executeScalar(sql) != null)
            {
                return (int)providerDAO.Instance.executeScalar(sql);
            } else
            return 0;

        }
    }
}
