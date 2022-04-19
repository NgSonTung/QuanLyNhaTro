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
        private int maChuNha;
        private string diaChi;
        private float gia;
        private int limit;
        private string status;
        public static int TableWidth = 100;
        public static int TableHeight = 100;

        public int MaNhaTro { get => maNhaTro; set => maNhaTro = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public float Gia { get => gia; set => gia = value; }
        public string Status { get => status; set => status = value; }
        public int Limit { get => limit; set => limit = value; }
        public int MaChuNha { get => maChuNha; set => maChuNha = value; }

        public nhaTro(DataRow row)
        {   

            this.MaNhaTro = (int)row["maNhaTro"];
            this.maChuNha = (int)row["maChuNha"];
            this.DiaChi = (string)row["diaChi"];
            this.Gia = float.Parse(row["gia"].ToString());
            this.Limit = (int)row["limit"];
            this.Status = (string)row["status"];
        }
    }
}
