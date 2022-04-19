using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class history
    {
        int maSinhVien;
        string name;
        DateTime? dateCheckIn;
        DateTime? dateCheckOut;
        string diaChi;

        public int MaSinhVien { get => maSinhVien; set => maSinhVien = value; }
        public string Name { get => name; set => name = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }

        public history(DataRow row)
        {
            MaSinhVien = (int)row["maSinhVien"];
            Name = (string)row["name"];
            DateCheckIn = (DateTime?)row["dateCheckIn"];
            var dateCheckOutTemp = row["dateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
            {
                DateCheckOut = (DateTime?)dateCheckOutTemp;
            }
            DiaChi = (string)row["diaChi"];
        }
    }
}
