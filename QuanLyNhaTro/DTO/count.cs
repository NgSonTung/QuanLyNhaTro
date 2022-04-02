using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class count
    {
        private int svCount;

        public int SvCount { get => svCount; set => svCount = value; }

        public count(DataRow row)
        {
            this.SvCount = (int)row["soSinhVien"];
        }
    }
}
