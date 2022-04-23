using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class nhaTroList
    {
        private int maNhaTro;
        private string ten;
        private string diaChi;
        private float gia;
        private int limit;
        private string status;
        private string sdt;
        private string diaChiChu;
        private int soSinhVien;

        public static int TableWidth = 100;
        public static int TableHeight = 100;

        public int MaNhaTro { get => maNhaTro; set => maNhaTro = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public float Gia { get => gia; set => gia = value; }
        public string Status { get => status; set => status = value; }
        public int Limit { get => limit; set => limit = value; }
        public string Ten { get => ten; set => ten = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string DiaChiChu { get => diaChiChu; set => diaChiChu = value; }
        public int SoSinhVien { get => soSinhVien; set => soSinhVien = value; }

        public nhaTroList(DataRow row)
        {

            this.MaNhaTro = (int)row["maNhaTro"];
            this.Ten = (string)row["ten"];
            this.DiaChi = (string)row["diaChi"];
            this.Gia = float.Parse(row["gia"].ToString());
            this.Limit = (int)row["limit"];
            this.Status = (string)row["status"];
            this.Sdt = (string)row["sdt"];
            DiaChiChu = (string)row["diaChiChu"];
            var soSinhVienTemp = row["soSinhVien"];
            if (soSinhVienTemp.ToString() != "")
            {
                this.SoSinhVien = (int)row["soSinhVien"];
            } else SoSinhVien = 0;

        }
    }
}
