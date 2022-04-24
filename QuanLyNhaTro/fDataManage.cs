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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 formLogin = new Form1();
            formLogin.Closed += (s, args) => this.Close();
            formLogin.Show();
        }

        public fDataManage()
        {
            InitializeComponent();
            loadDate();
            loadHistory();
            loadChuNha();
        }
        void loadChuNha()
        {   
            comboBox2.DataSource = chunhaDAO.Instance.getChuNha();
            comboBox2.DisplayMember = "ten";
        }
        void LoadDanhmuc()
        {
            List<nhaTroList> nhaTroList = nhaTroDAO.Instance.getTable2();
            dgvPart.DataSource = nhaTroDAO.Instance.getTable2();
            foreach (nhaTroList item in nhaTroList)
            {
                dgvPart.Tag = item;
            }
            dgvPart.Columns["diaChiChu"].Visible = false;
        }

        private void Admin_Load_1(object sender, EventArgs e)
        {
            LoadDanhmuc();
            LoadListsinhVien();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool isSame = false;
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Chưa chọn chủ nhà", "Thông báo", MessageBoxButtons.OK);
            } else if (textBox4.Text == "")
            {
                MessageBox.Show("Chưa nhập địa chỉ", "Thông báo", MessageBoxButtons.OK);
            }
            else if (textBox8.Text == "")
            {
                MessageBox.Show("Chưa nhập giá", "Thông báo", MessageBoxButtons.OK);
            }
            else if (!float.TryParse(textBox8.Text, out float _))
            {
                MessageBox.Show("Giá chưa đúng", "Thông báo", MessageBoxButtons.OK);
            }
            else if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("Chưa chọn giới hạn nhà trọ", "Thông báo", MessageBoxButtons.OK);
            }
            else 
            {
                nhaTroDAO.Instance.getTable2().ForEach(delegate (nhaTroList item)
                {
                    if (item.DiaChi == textBox4.Text)
                    {
                        MessageBox.Show("Trùng địa chỉ", "Thông báo", MessageBoxButtons.OK);
                        isSame = true;
                    }
                });
                if (!isSame)
                {
                    int maChuNha = (comboBox2.SelectedItem as chuNha).MaChuNha;
                    string diaChi = textBox4.Text;
                    float gia = float.Parse(textBox8.Text);
                    int limit = int.Parse(numericUpDown1.Value.ToString());
                    nhaTroDAO.Instance.insertNhaTro(maChuNha, diaChi, gia, limit);
                    LoadDanhmuc();
                    textBox5.Clear();
                    textBox4.Clear();
                    textBox8.Clear();
                    numericUpDown1.Value = 0;
                    MessageBox.Show("Thêm nhà trọ thành công", "Thông báo", MessageBoxButtons.OK);
                }
            } 
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
                if (row.Cells[3].Value.ToString() == "còn chỗ")
                    comboBox1.SelectedIndex = 0;
                else
                    comboBox1.SelectedIndex = 1;
                numericUpDown1.Value = int.Parse(row.Cells[4].Value.ToString());
                comboBox2.SelectedIndex = chunhaDAO.Instance.getChuNhaIndex(row.Cells[5].Value.ToString());
                textBox2.Text = row.Cells[6].Value.ToString();
                if (int.Parse(row.Cells[8].Value.ToString()) > 0)
                    textBox3.Text = row.Cells[7].Value.ToString();
                else textBox3.Clear();
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
        {   bool isSame = false;
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Chưa chọn chủ nhà", "Thông báo", MessageBoxButtons.OK);
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("Chưa chọn mã nhà trọ", "Thông báo", MessageBoxButtons.OK);
            }
            else if (!float.TryParse(textBox5.Text, out float _))
            {
                MessageBox.Show("Mã nhà trọ chưa đúng", "Thông báo", MessageBoxButtons.OK);
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("Chưa nhập địa chỉ", "Thông báo", MessageBoxButtons.OK);
            }
            else if (textBox8.Text == "")
            {
                MessageBox.Show("Chưa nhập giá", "Thông báo", MessageBoxButtons.OK);
            }
            else if (!float.TryParse(textBox8.Text, out float _))
            {
                MessageBox.Show("Giá chưa đúng", "Thông báo", MessageBoxButtons.OK);
            }
            else if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("Chưa chọn giới hạn nhà trọ", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn cập nhật?", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    nhaTroDAO.Instance.getTable2().ForEach(delegate (nhaTroList item)
                    {
                        if (item.DiaChi == textBox4.Text && item.MaNhaTro != int.Parse(textBox5.Text))
                        {
                            MessageBox.Show("Trùng địa chỉ", "Thông báo", MessageBoxButtons.OK);
                            isSame = true;
                        }
                    });
                    if (!isSame)
                    {
                        string diaChi = textBox4.Text;
                        int maChuNha = (comboBox2.SelectedItem as chuNha).MaChuNha;
                        int limit = int.Parse(numericUpDown1.Value.ToString());
                        float gia = float.Parse(textBox8.Text);
                        int maNhaTro = int.Parse(textBox5.Text);
                        if (countDAO.Instance.getSVCount(maNhaTro) > limit)
                        {
                            MessageBox.Show("Giới hạn thấp hơn số sinh viên đang ở trọ", "Thông báo", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if (countDAO.Instance.getSVCount(maNhaTro) < limit)
                                nhaTroDAO.Instance.availStatus(maNhaTro);
                            else if (countDAO.Instance.getSVCount(maNhaTro) == limit)
                                nhaTroDAO.Instance.fullStatus(maNhaTro);
                            nhaTroDAO.Instance.UPDATEINSERT(maNhaTro, maChuNha, diaChi, gia, limit);
                            LoadDanhmuc();
                            LoadDanhmuc();
                            textBox5.Clear();
                            textBox4.Clear();
                            textBox8.Clear();
                            numericUpDown1.Value = 0;
                            MessageBox.Show("Sửa nhà trọ thành công", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                }    
                    
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Xóa nhà trọ sẽ xóa hợp đồng, thanh toán có liên quan", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (textBox5.Text == "")
                    MessageBox.Show("Chưa chọn nhà trọ", "Thông báo", MessageBoxButtons.OK);
                else
                {
                    int maNhaTro = int.Parse(textBox5.Text);
                    List<chuyenTro> chuyenTroList = chuyenTroDAO.Instance.getListTro(maNhaTro);
                    if (chuyenTroList.Count > 0) /*Nếu trong các nhà trọ xóa có sinh viên đang ở*/
                    {
                        chuyenTroList.ForEach(delegate (chuyenTro item) /*Với mỗi sv đang ở nhà trọ đang xóa*/
                        {
                            sinhVienDAO.Instance.statusKhongTro(item.MaSinhVien); /*update status sinh viên thành không có trọ*/
                        });
                    }
                    List<thanhToan> thanhToanList = thanhToanDAO.Instance.getMaThanhToanByMaNT(maNhaTro); /*Lấy danh sách thanh toán từ mã nhà trọ*/
                    if (thanhToanList.Count > 0)
                    {
                        thanhToanList.ForEach(delegate (thanhToan item) /*Với mỗi thanh toán của nhà trọ*/
                        {
                            hopDongDAO.Instance.deleteHopDongByMaThanhToan(item.MaThanhToan); /*Xóa hợp đồng theo mã thanh toán*/
                        });
                    }
                    thanhToanDAO.Instance.deleteThanhToan(maNhaTro); /*xóa tất cả thanh toán theo mã nhà trọ*/
                    nhaTroDAO.Instance.deleteNhaTro(maNhaTro); /*Xóa nhà trọ*/
                    LoadDanhmuc();
                    LoadDanhmuc();
                    textBox5.Clear();
                    textBox4.Clear();
                    textBox8.Clear();
                    numericUpDown1.Value = 0;
                    MessageBox.Show("Xóa nhà trọ thành công", "Thông báo", MessageBoxButtons.OK);
                }
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
            if (cbKhoaSV.Items.Count == 0)
            {
                MessageBox.Show("Không có khoa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else if (cbKhoaSV.Text == "")
            {
                MessageBox.Show("Chưa chọn khoa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int khoa = (cbKhoaSV.SelectedItem as khoa).MaKhoa;
                dgvSinhvien.DataSource = sinhVienDAO.Instance.getSinhVienByKhoa(khoa);
                MessageBox.Show("Liệt kê sinh viên thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void themSV_Click(object sender, EventArgs e)
        {   
            if (txtnameSV.Text == "")
            {
                MessageBox.Show("Chưa nhập sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtDTSV.Text == "")
            {
                MessageBox.Show("Chưa nhập số điện thoại sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!int.TryParse(txtDTSV.Text, out int _) || txtDTSV.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại chưa đúng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtLopSV.Text == "")
            {
                MessageBox.Show("Chưa nhập lớp sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtQueQuan.Text == "")
            {
                MessageBox.Show("Chưa nhập quê quán sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string name = txtnameSV.Text;
                int khoa = (cbKhoaSV.SelectedItem as khoa).MaKhoa;
                string dienThoai = txtDTSV.Text;
                string lop = txtLopSV.Text;
                string queQuan = txtQueQuan.Text;
                sinhVienDAO.Instance.INSERTSinhvieṇ̣̣(khoa,name,dienThoai,lop,queQuan);
                LoadListsinhVien();
                txtMSSV.Clear();
                txtnameSV.Clear();
                txtDTSV.Clear();
                txtLopSV.Clear();
                txtQueQuan.Clear();
                MessageBox.Show("Thêm sinh viên thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void tabPage4_Click_1(object sender, EventArgs e)
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
                switch (row.Cells[1].Value.ToString())
                {
                    case "khoa công nghệ thông tin":
                        cbKhoaSV.SelectedIndex = 0;
                        break;
                    case "khoa y":
                        cbKhoaSV.SelectedIndex = 1;
                        break;
                    case "khoa kỹ thuật":
                        cbKhoaSV.SelectedIndex = 2;
                        break;
                    case "khoa tài chính ngân hàng":
                        cbKhoaSV.SelectedIndex = 3;
                        break;
                }

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
                DialogResult dialogResult = MessageBox.Show("Xóa sinh viên sẽ xóa hợp đồng, thanh toán có liên quan", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int maSinhVien = int.Parse(txtMSSV.Text);
                    int maThanhToan = sinhVienDAO.Instance.getMaTTFromMaSV(maSinhVien);
                    int maNhaTro = sinhVienDAO.Instance.getMaNTFromMaSV(maSinhVien);
                    hopDongDAO.Instance.updateCountDown(maThanhToan);
                    if (countDAO.Instance.getSVCount(maNhaTro) == 0) /*Nếu nhà trọ cũ 0 có người*/
                    {
                        thanhToanDAO.Instance.checkOut(maThanhToan); /*đổi status thanh toán thành đã thanh toán*/
                        nhaTroDAO.Instance.availStatus(maNhaTro);  /*update status NT -> trống*/
                    }
                    hopDongDAO.Instance.deleteSinhvien(maSinhVien); /*Xoa hop dong cua sv*/
                    sinhVienDAO.Instance.DELETESinhVien(maSinhVien); /*xóa sinh viên*/
                    if (countDAO.Instance.getSVCount(maNhaTro) < nhaTroDAO.Instance.checkNhaTro(maNhaTro))
                        nhaTroDAO.Instance.availStatus(maNhaTro); /*update status nha tro thanh con cho*/
                    LoadListsinhVien();
                    txtMSSV.Clear();
                    txtnameSV.Clear();
                    txtDTSV.Clear();
                    txtLopSV.Clear();
                    txtQueQuan.Clear();
                    MessageBox.Show("Xóa sinh viên thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cbstatusSV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void suaTTSV_Click(object sender, EventArgs e)
        {
            if (txtMSSV.Text == "")
            {
                MessageBox.Show("Chưa nhập mã sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtnameSV.Text == "")
            {
                MessageBox.Show("Chưa nhập sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtDTSV.Text == "")
            {
                MessageBox.Show("Chưa nhập điện thoại sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtLopSV.Text == "")
            {
                MessageBox.Show("Chưa nhập lớp sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtQueQuan.Text == "")
            {
                MessageBox.Show("Chưa nhập quê quán sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn cập nhật?", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int maSinhVien = Convert.ToInt32(txtMSSV.Text);
                    string name = txtnameSV.Text;
                    int khoa = (cbKhoaSV.SelectedItem as khoa).MaKhoa;
                    string dienThoai = txtDTSV.Text;
                    string lop = txtLopSV.Text;
                    string queQuan = txtQueQuan.Text;
                    sinhVienDAO.Instance.UPDATESinhVien(maSinhVien, khoa, name, dienThoai, lop, queQuan);
                    LoadListsinhVien();
                    txtMSSV.Clear();
                    txtnameSV.Clear();
                    txtDTSV.Clear();
                    txtLopSV.Clear();
                    txtQueQuan.Clear();
                    MessageBox.Show("Cập nhật sinh viên thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void searchSV_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Chưa nhập từ khóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dgvSinhvien.DataSource = sinhVienDAO.Instance.SearchSinhVien(txtSearch.Text);
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Chưa nhập từ khóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadListsinhVien();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show
                ("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.OKCancel);
            if (h == DialogResult.OK)
                Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDanhmuc();
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {
            label13.Hide();
            label14.Hide();
            label15.Hide();
            label16.Hide();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            showMenu(label15);

        }

        private void button11_Click(object sender, EventArgs e)
        {
            showMenu(label16);
        }
        private void customizeDesign()
        {
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
        }
        private void hideMenu()
        {
            if (label13.Visible == true || label14.Visible == true || label15.Visible == true || label16.Visible == true)
                label13.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
        }
        private void showMenu(Label subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible=false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showMenu(label13);


        }

        private void label13_Click(object sender, EventArgs e)
        {
            hideMenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showMenu(label14);

        }

        private void label14_Click(object sender, EventArgs e)
        {
            hideMenu();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            hideMenu();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            hideMenu();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            showMenu(label19);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            showMenu(label20);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            showMenu(label21);
        }

        private void label19_Click(object sender, EventArgs e)
        {
            hideMenu();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            hideMenu();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            hideMenu();
        }

        private void panel21_Paint(object sender, PaintEventArgs e)
        {
            label19.Hide();
            label20.Hide();
            label21.Hide();
        }


    }
}
