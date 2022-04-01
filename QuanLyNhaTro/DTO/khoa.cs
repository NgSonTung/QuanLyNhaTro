using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class khoa
    {
        private int maKhoa;
        private string name;

        public int MaKhoa { get => maKhoa; set => maKhoa = value; }
        public string Name { get => name; set => name = value; }

        public khoa (DataRow row)
        {
            maKhoa = (int)row["maKhoa"];
            name = (string)row["name"];


        }
    }
}
