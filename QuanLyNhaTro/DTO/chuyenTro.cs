using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class chuyenTro
    {
        int maSinhVien;
        string name;
        int maHopDong;
        int maThanhToan;
        int maNhaTro;

        public int MaSinhVien { get => maSinhVien; set => maSinhVien = value; }
        public string Name { get => name; set => name = value; }
        public int MaHopDong { get => maHopDong; set => maHopDong = value; }
        public int MaThanhToan { get => maThanhToan; set => maThanhToan = value; }
        public int MaNhaTro { get => maNhaTro; set => maNhaTro = value; }

        public chuyenTro(DataRow row)
        {
            MaSinhVien = (int)row["maSinhVien"];
            Name = (string)row["name"];
            MaHopDong = (int)row["maHopDong"];
            MaThanhToan = (int)row["maThanhToan"];
            MaNhaTro = (int)row["maNhaTro"];
        }
    }
}
