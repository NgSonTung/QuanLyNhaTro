using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DTO
{
    public class lop
    {
        private int lopSV;

        public int LopSV { get => lopSV; set => lopSV = value; }

        public lop(DataRow row)
        {
            lopSV = int.Parse((string)row["lop"]);
        }
    }
}
