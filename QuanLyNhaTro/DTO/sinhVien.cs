using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class sinhVien
    {
        private int maSinhVien;
        private string khoa;
        private string name;
        private string dienThoai;
        private int lop;
        private string queQuan;

        public int MaSinhVien { get => maSinhVien; set => maSinhVien = value; }
        public string Khoa { get => khoa; set => khoa = value; }
        public string Name { get => name; set => name = value; }
        public string DienThoai { get => dienThoai; set => dienThoai = value; }
        public int Lop { get => lop; set => lop = value; }
        public string QueQuan { get => queQuan; set => queQuan = value; }

        public sinhVien(DataRow row)
        {
            maSinhVien = (int)row["maSinhVien"];
            khoa = (string)row["khoa"];
            name = (string)row["name"];
            dienThoai = (string)row["dienThoai"];
            lop = (int)row["lop"];
            queQuan = (string)row["queQuan"];

        }
    }
}
