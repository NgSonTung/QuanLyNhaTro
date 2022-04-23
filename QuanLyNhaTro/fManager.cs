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
    public partial class fManager : Form
    {
        public fManager()
        {
            InitializeComponent();
            loadNhaTro();
            loadKhoa();
            loadLop(tempMaKhoa);
        }
        public int tempMaKhoa;
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            fDataManage admin = new fDataManage();
            admin.Closed += (s, args) => this.Close();
            admin.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 fLogin = new Form1();
            fLogin.Closed += (s, args) => this.Close();
            fLogin.Show();
        }
        void loadNhaTro() /*show table button*/
        {
            List<nhaTro> tableList = nhaTroDAO.Instance.getTable();
            flowLayoutPanel1.Controls.Clear();
            foreach (nhaTro item in tableList)
            {
                Button btn = new Button() { Width = nhaTro.TableWidth, Height = nhaTro.TableHeight };
                btn.Text = item.DiaChi + Environment.NewLine + "số người: " + countDAO.Instance.getSVCount(item.MaNhaTro) + "/" + item.Limit + Environment.NewLine;
                btn.Click += Btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "còn chỗ":
                        btn.BackColor = Color.FromArgb(90, 176, 127);
                        break;
                    default:
                        btn.BackColor = Color.FromArgb(194, 60, 60);
                        break;
                }
                flowLayoutPanel1.Controls.Add(btn);
            }
            comboBox6.Text = null;
        }

        void loadThongTin(int id)
        {
            List<thongTinTro> thongTinTroList = thongTinTroDAO.Instance.getMenu(id);
            dataGridView1.DataSource = thongTinTroList;
            dataGridView1.Columns[0].HeaderText = "Tên sinh viên";
            dataGridView1.Columns[1].HeaderText = "Số điện thoại";
            dataGridView1.Columns[2].HeaderText = "Ngày checkin";
            dataGridView1.Columns[3].HeaderText = "Tiền nhà";
            dataGridView1.Columns[4].HeaderText = "Phải trả";
            dataGridView1.Columns[5].Visible = false;
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            comboBox6.Text = "";
            int tableID = ((sender as Button).Tag as nhaTro).MaNhaTro;
            dataGridView1.Tag = (sender as Button).Tag;
            loadThongTin(tableID);
            List<chuyenTro> listChuyenTro = chuyenTroDAO.Instance.getChuyenTro(tableID);
            comboBox6.DataSource = listChuyenTro;
            comboBox6.DisplayMember = "Name";
            List<nhaTro> nhaTroList = nhaTroDAO.Instance.getTable();
            comboBox3.DataSource = nhaTroList;
            comboBox3.DisplayMember = "diachi";
        }

        void loadLop(int maKhoa)
        {   
            List<lop> lopList = sinhVienDAO.Instance.getLopSV(maKhoa);
            comboBox4.DataSource = lopList;
            if (comboBox4.Items.Count == 0)
            {
                comboBox4.Text = "trống";
                loadSinhVienByKhoa(1, maKhoa);
            }
            else
            {
                comboBox4.DisplayMember = "lopSV";
                comboBox4.SelectedIndex = 0;
            }
        }

        void loadKhoa()
        {
            List<khoa> listCategory = khoaDAO.Instance.getCategory();
            comboBox1.DataSource = listCategory;
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Text = "trống";
            }
            else
            {
                comboBox1.DisplayMember = "name";
                comboBox1.SelectedIndex = 0;
            }
        }

        void loadSinhVienByKhoa(int lop, int khoa)
        {
            List<sinhVien> listSinhVien = sinhVienDAO.Instance.getFood(lop,khoa);
            comboBox2.DataSource = listSinhVien;
            if (comboBox2.Items.Count == 0)
            {
                comboBox2.Text = "trống";
            }
            else
            comboBox2.DisplayMember = "Name";
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (comboBox6.Text == null)
            {
                MessageBox.Show("Chưa chọn sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (comboBox6.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (comboBox6.Items.Count == 0)
            {
                MessageBox.Show("Không có sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dataGridView1.Tag as nhaTro == null)
            {
                MessageBox.Show("Chưa chọn nhà trọ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else /*bat loi*/
            {
                int maSinhVien = (comboBox6.SelectedItem as chuyenTro).MaSinhVien;
                int maNhaTro = sinhVienDAO.Instance.getMaNTFromMaSV(maSinhVien);
                int maThanhToan = sinhVienDAO.Instance.getMaTTFromMaSV(maSinhVien);
                sinhVienDAO.Instance.statusKhongTro(maSinhVien);
                hopDongDAO.Instance.updateCountDown(maThanhToan);
                if (countDAO.Instance.getSVCount(maNhaTro) == 0) /*Nếu nhà trọ cũ 0 có người*/
                {
                    thanhToanDAO.Instance.checkOut(maThanhToan); /*đổi status thanh toán thành đã thanh toán*/
                    nhaTroDAO.Instance.availStatus(maNhaTro);  /*update status NT -> trống*/
                }
                if (countDAO.Instance.getSVCount(maNhaTro) < nhaTroDAO.Instance.checkNhaTro(maNhaTro))
                    nhaTroDAO.Instance.availStatus(maNhaTro);
                if (comboBox4.SelectedItem != null)
                {
                    int lopSinhVien = (comboBox4.SelectedItem as lop).LopSV;
                    loadSinhVienByKhoa(lopSinhVien, tempMaKhoa);
                }
                loadLop(tempMaKhoa);
                loadThongTin(maNhaTro);
                loadNhaTro();
                List<chuyenTro> listChuyenTro = chuyenTroDAO.Instance.getChuyenTro(maNhaTro);
                comboBox6.DataSource = listChuyenTro;
                comboBox6.DisplayMember = "Name";
            }
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {   
            int maKhoa;
            int lopSinhVien;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            else
            {
                khoa select = cb.SelectedItem as khoa;
                maKhoa = select.MaKhoa;
                tempMaKhoa = maKhoa;
                loadLop(maKhoa);
                if (comboBox4.SelectedItem != null)
                {
                    lopSinhVien = (comboBox4.SelectedItem as lop).LopSV;
                    loadSinhVienByKhoa(lopSinhVien, maKhoa);
                }
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int maKhoa;
            int lopSinhVien;
            ComboBox cb = comboBox1 as ComboBox;
            if (cb.SelectedItem == null)
                return;
            else if (comboBox4.SelectedItem == null)
            {
                return;
            }
            else
            {
                lopSinhVien = (comboBox4.SelectedItem as lop).LopSV;
                khoa select = cb.SelectedItem as khoa;
                maKhoa = select.MaKhoa;
                loadSinhVienByKhoa(lopSinhVien, maKhoa);
                tempMaKhoa = maKhoa;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<nhaTro> nhaTroList = nhaTroDAO.Instance.getTable();
            comboBox3.DataSource = nhaTroList;
            comboBox3.DisplayMember = "diachi";
            int maNhaTro = (dataGridView1.Tag as nhaTro).MaNhaTro;
            List<chuyenTro> listChuyenTro = chuyenTroDAO.Instance.getChuyenTro(maNhaTro);
            comboBox6.DataSource = listChuyenTro;
            comboBox6.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e) /*CheckIn*/
        {       
            if (dataGridView1.Tag as nhaTro == null)
            {
                MessageBox.Show("Chưa chọn nhà trọ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (comboBox2.Text == "trống")
            {
                MessageBox.Show("Không có sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Không có lớp", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else /*bat loi*/
            {
                nhaTro nhaTro = dataGridView1.Tag as nhaTro;
                int maThanhToan = thanhToanDAO.Instance.getUncheckBill(nhaTro.MaNhaTro);
                int maSinhVien = (comboBox2.SelectedItem as sinhVien).MaSinhVien;
                int maNhaTro = (dataGridView1.Tag as nhaTro).MaNhaTro;
                int lopSinhVien = (comboBox4.SelectedItem as lop).LopSV;
                if (countDAO.Instance.getSVCount(maNhaTro) + 1 <= nhaTroDAO.Instance.checkNhaTro(maNhaTro)) /*Nếu nhà trọ mới còn chỗ*/
                {
                    if (maThanhToan == -1) /*nếu nhà trọ trống ~ chưa có bill -> thêm bill*/
                    {
                        thanhToanDAO.Instance.insertBill(nhaTro.MaNhaTro); /* Tạo thanh toán */
                        hopDongDAO.Instance.insertBillInfo(thanhToanDAO.Instance.getMaxID(), maSinhVien, 1); /* tạo hợp đồng*/
                        sinhVienDAO.Instance.statusCoTro(maSinhVien);  /*update status sinh viên thành có trọ*/
                    }
                    else /*nếu nhà trọ mới không trống*/
                    {
                        hopDongDAO.Instance.insertBillInfo(maThanhToan, maSinhVien, countDAO.Instance.getSVCount(nhaTro.MaNhaTro));
                        sinhVienDAO.Instance.statusCoTro(maSinhVien);
                        hopDongDAO.Instance.updateCountUp(maThanhToan);
                    }
                    if (countDAO.Instance.getSVCount(maNhaTro) == nhaTroDAO.Instance.checkNhaTro(maNhaTro))/*nếu nhà trọ mới + 1 = full*/
                        nhaTroDAO.Instance.fullStatus(maNhaTro); /* update status nhà trọ mới thành full*/
                    loadThongTin(nhaTro.MaNhaTro);
                    loadNhaTro();
                    loadLop(tempMaKhoa);
                    List<chuyenTro> listChuyenTro = chuyenTroDAO.Instance.getChuyenTro(maNhaTro);
                    comboBox6.DataSource = listChuyenTro;
                    comboBox6.DisplayMember = "Name";
                }
                else /*Nếu nhà trọ mới 0 còn chỗ*/
                    MessageBox.Show("Nhà trọ đã hết chỗ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox6.Text == null)
            {
                MessageBox.Show("Chưa chọn sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if (comboBox6.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (comboBox6.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (comboBox6.Items.Count == 0)
            {
                MessageBox.Show("Không có sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dataGridView1.Tag as nhaTro == null)
            {
                MessageBox.Show("Chưa chọn nhà trọ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if ((dataGridView1.Tag as nhaTro).MaNhaTro == (comboBox3.SelectedItem as nhaTro).MaNhaTro)
            {
                MessageBox.Show("Trùng nhà trọ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else /*bat loi*/
            {
                int maHopDong = (comboBox6.SelectedItem as chuyenTro).MaHopDong;
                int maSinhVien = (comboBox6.SelectedItem as chuyenTro).MaSinhVien;
                nhaTro nhaTro = dataGridView1.Tag as nhaTro; /*Lấy mã nhà trọ cũ & status*/
                int maThanhToanCu = thanhToanDAO.Instance.getUncheckBill(nhaTro.MaNhaTro);
                int maNhaTroMoi = (comboBox3.SelectedItem as nhaTro).MaNhaTro;
                int checkThanhToan = thanhToanDAO.Instance.getUncheckBill(maNhaTroMoi);
                int maThanhToanMoi = ((comboBox3.SelectedItem as nhaTro).MaNhaTro);
                /*int lopSinhVien = (comboBox4.SelectedItem as lop).LopSV;*/
                if (countDAO.Instance.getSVCount(maNhaTroMoi) + 1 <= nhaTroDAO.Instance.checkNhaTro(maNhaTroMoi)) /*Nếu nhà trọ mới còn chỗ*/
                {
                    if (checkThanhToan == -1) /*nếu nhà trọ mới trống ~ chưa có bill -> thêm bill*/
                    {
                        thanhToanDAO.Instance.insertBill(maNhaTroMoi); /* tạo thanh toán mới */
                        hopDongDAO.Instance.updateHopDong1(thanhToanDAO.Instance.getMaxID(), maSinhVien); /* tạo hợp đồng mới */
                        hopDongDAO.Instance.updateChuyenTro(maThanhToanCu, maSinhVien);/* Doi soSV o hop dong cu thanh -1*/
                        hopDongDAO.Instance.updateCountDown(maThanhToanCu); /*giảm count ở nhà cũ*/
                        if (countDAO.Instance.getSVCount(nhaTro.MaNhaTro) == 0) /*Nếu nhà trọ cũ 0 có người*/
                        {
                            nhaTroDAO.Instance.availStatus(nhaTro.MaNhaTro);  /*update status NTcu -> trống*/
                            thanhToanDAO.Instance.checkOut(maThanhToanCu); /*update status TTcu -> đã thanh toán*/
                        }
                        hopDongDAO.Instance.updateCountUp(maThanhToanMoi);/*tăng count ở hợp đồng nhà mới*/
                    }
                    else /*nếu nhà trọ mới không trống*/
                    {
                        hopDongDAO.Instance.updateHopDong2(thanhToanDAO.Instance.getMaThanhToan(maNhaTroMoi), maSinhVien, countDAO.Instance.getSVCount(maNhaTroMoi)); /* update hợp đồng*/
                        hopDongDAO.Instance.updateCountUp(thanhToanDAO.Instance.getMaThanhToan(maNhaTroMoi)); /*tăng count ở hợp đồng nhà mới*/
                        hopDongDAO.Instance.updateChuyenTro(maThanhToanCu, maSinhVien);/* Doi soSV o hop dong cu thanh -1*/
                        hopDongDAO.Instance.updateCountDown(maThanhToanCu); /*giảm count ở nhà cũ*/
                        if (countDAO.Instance.getSVCount(nhaTro.MaNhaTro) == 0) /*Nếu nhà trọ cũ 0 có người*/
                        {
                            nhaTroDAO.Instance.availStatus(nhaTro.MaNhaTro);  /*update status NT -> trống*/
                            thanhToanDAO.Instance.checkOut(maThanhToanCu); /*update status TT -> đã thanh toán*/
                        }
                    }
                    if (countDAO.Instance.getSVCount(maNhaTroMoi) == nhaTroDAO.Instance.checkNhaTro(maNhaTroMoi))/*nếu nhà trọ mới + 1 = full*/
                        nhaTroDAO.Instance.fullStatus(maNhaTroMoi); /* update status nhà trọ mới thành full*/
                    if (countDAO.Instance.getSVCount(nhaTro.MaNhaTro) < nhaTroDAO.Instance.checkNhaTro(nhaTro.MaNhaTro))
                        nhaTroDAO.Instance.availStatus(nhaTro.MaNhaTro);
                    List<chuyenTro> listChuyenTro = chuyenTroDAO.Instance.getChuyenTro(nhaTro.MaNhaTro);
                    comboBox6.DataSource = listChuyenTro;
                    comboBox6.DisplayMember = "Name";
                    loadThongTin(nhaTro.MaNhaTro);
                    loadNhaTro();
                }
                else /*Nếu nhà trọ mới 0 còn chỗ*/
                    MessageBox.Show("Nhà trọ đã hết chỗ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fManager_Load(object sender, EventArgs e)
        {

        }
    }
} 
