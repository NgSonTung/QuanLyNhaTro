using QuanLyNhaTro.DAO;
using QuanLyNhaTro.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaTro
{
    public partial class Admin : Form
    {

        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
        void LoadAccount()
        {

        }

        void LoadDanhmuc()
        {
            List<nhaTro> nhatro = nhaTroDAO.Instance.getTable();
            DAO.providerDAO DP = new DAO.providerDAO();
            dgvPart.DataSource = nhatro;
        }

        private void Admin_Load_1(object sender, EventArgs e)
        {
            LoadAccount();
            LoadDanhmuc();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            int maNhaTro = (textBox5.SelectedText as maNhaTro).ID;
            

        }
        
        public void load()
        {
            
        }
        private void dgvPart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Lưu lại dòng dữ liệu vừa kích chọn
                DataGridViewRow row = this.dgvPart.Rows[e.RowIndex];
                //Đưa dữ liệu vào textbox
                textBox5.Text = row.Cells[0].Value.ToString();
                textBox4.Text = row.Cells[1].Value.ToString();
                textBox8.Text = row.Cells[2].Value.ToString();
                textBox7.Text = row.Cells[3].Value.ToString();
            }
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
