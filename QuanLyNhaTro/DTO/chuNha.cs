﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class chuNha
    {
        private int maChuNha;
        private int maNhaTro;
        private string diaChiChu;
        private string ten;
        private string sdt;

        public int MaChuNha { get => maChuNha; set => maChuNha = value; }
        public int MaNhaTro { get => maNhaTro; set => maNhaTro = value; }
        public string DiaChiChu { get => diaChiChu; set => diaChiChu = value; }
        public string Ten { get => ten; set => ten = value; }
        public string Sdt { get => sdt; set => sdt = value; }

        public chuNha(DataRow row)
        {
            maChuNha = (int)row["maChuNha"];
            maNhaTro = (int)row["maNhaTro"];
            diaChiChu = (string)row["diaChiChu"];
            ten = (string)row["ten"];
            sdt = (string)row["sdt"];
        }
    }
}