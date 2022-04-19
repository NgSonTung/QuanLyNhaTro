using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaTro.DAO
{
    public class historyDAO
    {
        private static historyDAO instance;

        public static historyDAO Instance
        {
            get { if (instance == null) instance = new historyDAO(); return historyDAO.instance; }
            private set { historyDAO.instance = value; }
        }

        public object DataProvider { get; private set; }

        public historyDAO()
        { }

        
    }
}
