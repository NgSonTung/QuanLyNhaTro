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
        private string name;
        private string dienThoai;
        private float gia;
        private DateTime dateCheckIn;
        private float tienNha;

        public string Name { get => name; set => name = value; }
        public string DienThoai { get => dienThoai; set => dienThoai = value; }
        public float Gia { get => gia; set => gia = value; }
        public DateTime DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public float TienNha { get => tienNha; set => tienNha = value; }

        public thongTinTro(DataRow row)
        {
            this.Name = (string)row["name"];
            this.DienThoai = (string)row["dienThoai"];
            this.Gia = float.Parse(row["Gia"].ToString());
            this.DateCheckIn = (DateTime)row["DateCheckIn"];
            this.TienNha = float.Parse(row["TienNha"].ToString());
        }
    }
}
