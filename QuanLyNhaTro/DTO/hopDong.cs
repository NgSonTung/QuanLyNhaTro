using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class hopDong
    {
        private int maHopDong;
        private int maThanhToan;
        private int maSinhVien;
        private int soSinhVien;

        public int MaHopDong { get => maHopDong; set => maHopDong = value; }
        public int MaThanhToan { get => maThanhToan; set => maThanhToan = value; }
        public int MaSinhVien { get => maSinhVien; set => maSinhVien = value; }
        public int SoSinhVien { get => soSinhVien; set => soSinhVien = value; }

        public hopDong(DataRow row)
        {
            this.maHopDong = (int)row["maHopDong"];
            this.maThanhToan = (int)row["maThanhToan"];
            this.maSinhVien = (int)row["maSinhVien"];
            this.soSinhVien = (int)row["soSinhVien"];
        }
    }
}
