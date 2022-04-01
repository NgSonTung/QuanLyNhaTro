using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class thanhToan
    {
        private int maThanhToan;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int maNhaTro;
        private int status;

        public int MaThanhToan { get => maThanhToan; set => maThanhToan = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int MaNhaTro { get => maNhaTro; set => maNhaTro = value; }
        public int Status { get => status; set => status = value; }

        public thanhToan(DataRow row)
        {
            maThanhToan = (int)row["maThanhToan"];
            DateCheckIn = (DateTime?)row["DateCheckIn"];
            var dateCheckOutTemp = row["dateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
            {
                this.DateCheckOut = (DateTime?)DateCheckOut;
            }
            maNhaTro = (int)row["maNhaTro"];
            status = (int)row["status"];
        }
    }
}
