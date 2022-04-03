using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class nhaTro
    {
        private int maNhaTro;
        private string diaChi;
        private float gia;
        private string status;
        public static int TableWidth = 100;
        public static int TableHeight = 100;

        public int MaNhaTro { get => maNhaTro; set => maNhaTro = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public float Gia { get => gia; set => gia = value; }
        public string Status { get => status; set => status = value; }

        public nhaTro(DataRow row)
        {
            this.maNhaTro = (int)row["maNhaTro"];
            this.diaChi = (string)row["diaChi"];
            this.gia = float.Parse(row["gia"].ToString());
            this.status = (string)row["status"];

        }
    }
}
