using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class thongTinTro
    {
        private int maSinhVien;
        private string name;
        private string dienThoai;
        private DateTime dateCheckIn;
        private float gia;
        private float tienNha;

        public string Name { get => name; set => name = value; }
        public string DienThoai { get => dienThoai; set => dienThoai = value; }
        public DateTime DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public float Gia { get => gia; set => gia = value; }
        public float TienNha { get => tienNha; set => tienNha = value; }
        public int MaSinhVien { get => maSinhVien; set => maSinhVien = value; }

        public thongTinTro(DataRow row)
        {
            this.MaSinhVien = (int)row["maSinhVien"];
            this.Name = (string)row["name"];
            this.DienThoai = (string)row["dienThoai"];
            this.DateCheckIn = (DateTime)row["DateCheckIn"];
            this.Gia = float.Parse(row["Gia"].ToString());
            this.TienNha = float.Parse(row["TienNha"].ToString());
        }
    }
}
