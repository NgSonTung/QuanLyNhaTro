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
        private string lop;
        private string queQuan;
        private int status;

        public int MaSinhVien { get => maSinhVien; set => maSinhVien = value; }
        public string Khoa { get => khoa; set => khoa = value; }
        public string Name { get => name; set => name = value; }
        public string DienThoai { get => dienThoai; set => dienThoai = value; }
        public string Lop { get => lop; set => lop = value; }
        public string QueQuan { get => queQuan; set => queQuan = value; }
        public int Status { get => status; set => status = value; }

        public sinhVien(DataRow row)
        {
            this.MaSinhVien = (int)row["maSinhVien"];
            this.Khoa = (string)row["khoa"];
            this.Name = (string)row["name"];
            this.DienThoai = (string)row["dienThoai"];
            this.Lop = (string)row["lop"];
            this.QueQuan = (string)row["queQuan"];
            this.status = (int)row["status"];
        }
    }
}
