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
    public partial class fDataManage : Form
    {
        private void managerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            fManager manager = new fManager();
            manager.Closed += (s, args) => this.Close();
            manager.Show();
        }
        public fDataManage()
        {
            InitializeComponent();
            loadDate();
            loadHistory();
        }
        void LoadDanhmuc()
        {
            List<nhaTro> nhatro = nhaTroDAO.Instance.getTable();
            DAO.providerDAO DP = new DAO.providerDAO();
            dgvPart.DataSource = nhatro;
        }

        private void Admin_Load_1(object sender, EventArgs e)
        {
            LoadDanhmuc();
            LoadListsinhVien();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string diaChi = textBox4.Text;
            float gia = float.Parse(textBox8.Text);
            nhaTroDAO.Instance.INSERT(diaChi, gia);
            LoadDanhmuc();

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
                cbstatusSV.Text = row.Cells[3].Value.ToString();
            }
        }

        void loadKhoa()
        {
            cbKhoaSV.DataSource = khoaDAO.Instance.getCategory();
            cbKhoaSV.DisplayMember = "name";
;        }
        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string diaChi = textBox4.Text;
            float gia = float.Parse(textBox8.Text);
            string status = cbstatusSV.Text;
            int maNhaTro = int.Parse(textBox5.Text);
            nhaTroDAO.Instance.UPDATEINSERT(maNhaTro,diaChi, gia, status);
            LoadDanhmuc();
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            /*int maNhaTro = int.Parse(textBox5.Text);
            nhaTroDAO.Instance.DELETEINSERT(maNhaTro);
            LoadDanhmuc();*/
            if (textBox5.Text == "")
            {
                MessageBox.Show("Chưa chọn ID", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                int maNhaTro = int.Parse(textBox5.Text);
                nhaTroDAO.Instance.deletenhatro(maNhaTro);
                LoadListsinhVien();
            }

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void LoadListsinhVien()
        {
            dgvSinhvien.DataSource = sinhVienDAO.Instance.GetListSinhVien();
            loadKhoa();
        }
        private void xemSV_Click(object sender, EventArgs e)
        {
            int khoa = (cbKhoaSV.SelectedItem as khoa).MaKhoa;
            dgvSinhvien.DataSource = sinhVienDAO.Instance.getSinhVienByKhoa(khoa);
        }

        private void themSV_Click(object sender, EventArgs e)
        {
            
            string name = txtnameSV.Text;
            int khoa = (cbKhoaSV.SelectedItem as khoa).MaKhoa;
            string dienThoai = txtDTSV.Text;
            string lop = txtLopSV.Text;
            string queQuan = txtQueQuan.Text;
            
            sinhVienDAO.Instance.INSERTSinhvieṇ̣̣(khoa,name,dienThoai,lop,queQuan);
            LoadListsinhVien();
        }

        private void tabPage4_Click_1(object sender, EventArgs e)
        {

        }

        private void dgvSinhvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSinhvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Lưu lại dòng dữ liệu vừa kích chọn
                DataGridViewRow row = dgvSinhvien.Rows[e.RowIndex];
                //Đưa dữ liệu vào textbox
                txtMSSV.Text = row.Cells[0].Value.ToString();
                cbKhoaSV.SelectedItem = row.Cells[1].Value.ToString();
                txtnameSV.Text = row.Cells[2].Value.ToString();
                txtDTSV.Text = row.Cells[3].Value.ToString();
                txtLopSV.Text = row.Cells[4].Value.ToString();
                txtQueQuan.Text = row.Cells[5].Value.ToString();
                if (int.Parse(row.Cells[6].Value.ToString()) == 1)
                {
                    cbstatusSV.SelectedIndex = 0;
                }
                else
                    cbstatusSV.SelectedIndex = 1;
            }
        }

        private void xoaSV_Click(object sender, EventArgs e)
        {
            
            if(txtMSSV.Text == "") 
            {
                MessageBox.Show("Chưa chọn Mã Sinh Viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else {
                int maSinhVien = int.Parse(txtMSSV.Text);
                sinhVienDAO.Instance.DELETESinhVien(maSinhVien);
                LoadListsinhVien();
            }

            
        }

        private void cbstatusSV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void suaTTSV_Click(object sender, EventArgs e)
        {
            int maSinhVien = Convert.ToInt32(txtMSSV.Text);
            string name = txtnameSV.Text;
            int khoa = (cbKhoaSV.SelectedItem as khoa).MaKhoa;
            string dienThoai = txtDTSV.Text;
            string lop = txtLopSV.Text;
            string queQuan = txtQueQuan.Text;
            
            
            sinhVienDAO.Instance.UPDATESinhVien(maSinhVien,khoa, name, dienThoai, lop, queQuan);
            LoadListsinhVien();
        }

        private void searchSV_Click(object sender, EventArgs e)
        {
           dgvSinhvien.DataSource = sinhVienDAO.Instance.SearchSinhVien(txtSearch.Text);
        }
        private event EventHandler deleteSinhVien;
        public event EventHandler DeleteSinhVien
        {
            add { deleteSinhVien += value; }
            remove { deleteSinhVien -= value; }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            dgvPart.DataSource = nhaTroDAO.Instance.SearchNhaTro(textBox6.Text);
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvSinhvien_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        void loadDate()
        {
            DateTime today = DateTime.Now;
            dateFrom.Value = new DateTime(today.Year, today.Month, 1);
            dateTo.Value = dateFrom.Value.AddMonths(1).AddDays(-1);
        }
        void loadHistory()
        {   
            if (dateFrom.Value < dateTo.Value)
            {
                List<history> historyList = historyDAO.Instance.getHistory(dateFrom.Value.Date.ToString("yyyyMMdd"), dateTo.Value.Date.ToString("yyyyMMdd"));
                historyDGV.DataSource = historyList;
            }
            else
                MessageBox.Show("chọn lại ngày", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHopDong_Click(object sender, EventArgs e)
        {
            loadHistory();
        }

        void checkDate()
        {
            if (dateFrom.Value > dateTo.Value)
            {
                MessageBox.Show("chọn lại ngày", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDate();
            }

        }

        private void dateFrom_ValueChanged(object sender, EventArgs e)
        {
            checkDate();
        }

        private void dateTo_ValueChanged(object sender, EventArgs e)
        {
            checkDate();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
