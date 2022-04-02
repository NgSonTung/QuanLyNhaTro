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

        public int MaSinhVien { get => maSinhVien; set => maSinhVien = value; }
        public string Name { get => name; set => name = value; }

        public chuyenTro(DataRow row)
        {
            this.MaSinhVien = (int)row["maSinhVien"];
            this.Name = (string)row["name"];
        }
    }
}
