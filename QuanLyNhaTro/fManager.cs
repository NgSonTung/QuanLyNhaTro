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
        }
        public int tempMaKhoa;

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin admin = new Admin();
            admin.Closed += (s, args) => this.Close();
            admin.Show();
        }
        void loadNhaTro() /*show table button*/
        {
            List<nhaTro> tableList = nhaTroDAO.Instance.getTable();
            flowLayoutPanel1.Controls.Clear();
            foreach (nhaTro item in tableList)
            {
                Button btn = new Button() { Width = nhaTro.TableWidth, Height = nhaTro.TableHeight };
                btn.Text = item.DiaChi + Environment.NewLine + item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "trống":
                        btn.BackColor = Color.Green;
                        break;
                    default:
                        btn.BackColor = Color.Red;
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

        void loadKhoa()
        {
            List<khoa> listCategory = khoaDAO.Instance.getCategory();
            comboBox1.DataSource = listCategory;
            comboBox1.DisplayMember = "Name";
        }

        void loadSinhVienByKhoa(int id)
        {
            List<sinhVien> listSinhVien = sinhVienDAO.Instance.getFood(id);
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
            if (comboBox6.Items.Count == 0)
            {
                MessageBox.Show("Không có sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dataGridView1.Tag as nhaTro == null)
            {
                MessageBox.Show("Chưa chọn nhà trọ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                nhaTro nhaTro = dataGridView1.Tag as nhaTro; /*Lấy mã nhà trọ cũ & status*/

                int maSinhVien = (comboBox6.SelectedItem as chuyenTro).MaSinhVien;
                int maNhaTro = sinhVienDAO.Instance.getMaNTFromMaSV(maSinhVien);
                int maThanhToan = sinhVienDAO.Instance.getMaTTFromMaSV(maSinhVien);
                sinhVienDAO.Instance.statusKhongTro(maSinhVien);
                hopDongDAO.Instance.updateCountDown(maThanhToan);
                if (countDAO.Instance.getSVCount(maNhaTro) == 0) /*Nếu nhà trọ cũ 0 có người*/
                {
                    /*hopDongDAO.Instance.autoDeleteHopDong();*/
                    thanhToanDAO.Instance.checkOut(maNhaTro); /*đổi status thanh toán thành đã thanh toán*/
                    nhaTroDAO.Instance.checkOutStatus(maNhaTro);  /*update status NT -> trống*/
                }
                loadThongTin(maNhaTro);
                loadNhaTro();
            }
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {   
            int maKhoa;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            khoa select = cb.SelectedItem as khoa;
            maKhoa = select.MaKhoa;
            loadSinhVienByKhoa(maKhoa);
            tempMaKhoa = maKhoa;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<nhaTro> nhaTroList = nhaTroDAO.Instance.getTable();
            comboBox3.DataSource = nhaTroList;
            comboBox3.DisplayMember = "diachi";
            int tableID = (dataGridView1.Tag as nhaTro).MaNhaTro;
            List<chuyenTro> listChuyenTro = chuyenTroDAO.Instance.getChuyenTro(tableID);
            comboBox6.DataSource = listChuyenTro;
            comboBox6.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e) /*CheckIn*/
        {
            
            /*int count = countDAO.Instance.getSVCount(nhaTro.MaNhaTro);*/
            if (dataGridView1.Tag as nhaTro == null)
            {
                MessageBox.Show("Chưa chọn nhà trọ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (comboBox2.Text == "trống")
            {
                MessageBox.Show("Không có sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                nhaTro nhaTro = dataGridView1.Tag as nhaTro;
                int maThanhToan = thanhToanDAO.Instance.getUncheckBill(nhaTro.MaNhaTro);
                int maSinhVien = (comboBox2.SelectedItem as sinhVien).MaSinhVien;
                if (maThanhToan == -1) /*nếu nhà trọ trống ~ chưa có bill -> thêm bill*/
                {
                    thanhToanDAO.Instance.insertBill(nhaTro.MaNhaTro); /* Tạo thanh toán */
                    hopDongDAO.Instance.insertBillInfo(thanhToanDAO.Instance.getMaxID(), maSinhVien, 1); /* tạo hợp đồng*/
                    nhaTroDAO.Instance.checkInStatus(nhaTro.MaNhaTro); /* update status nhà trọ thành có người*/
                    sinhVienDAO.Instance.statusCoTro(maSinhVien);  /*update status sinh viên thành có trọ*/
                }
                else
                {
                    hopDongDAO.Instance.insertBillInfo(maThanhToan, maSinhVien, countDAO.Instance.getSVCount(nhaTro.MaNhaTro));
                    sinhVienDAO.Instance.statusCoTro(maSinhVien);
                    hopDongDAO.Instance.updateCountUp(maThanhToan);
                }
                loadThongTin(nhaTro.MaNhaTro);
                loadNhaTro();
                loadSinhVienByKhoa(tempMaKhoa);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            if (comboBox6.Text == null)
            {
                MessageBox.Show("Chưa chọn sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (comboBox6.Items.Count == 0)
            {
                MessageBox.Show("Không có sinh viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dataGridView1.Tag as nhaTro == null)
            {
                MessageBox.Show("Chưa chọn nhà trọ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else
            {
                int maHopDong = (comboBox6.SelectedItem as chuyenTro).MaHopDong;
                nhaTro nhaTro = dataGridView1.Tag as nhaTro; /*Lấy mã nhà trọ cũ & status*/
                int maThanhToanCu = thanhToanDAO.Instance.getUncheckBill(nhaTro.MaNhaTro);
                int maNhaTroMoi = (comboBox3.SelectedItem as nhaTro).MaNhaTro;
                int checkThanhToan = thanhToanDAO.Instance.getUncheckBill(maNhaTroMoi);
                int maThanhToanMoi = ((comboBox3.SelectedItem as nhaTro).MaNhaTro);

                if (checkThanhToan == -1) /*nếu nhà trọ mới trống ~ chưa có bill -> thêm bill*/
                {
                    thanhToanDAO.Instance.insertBill(maNhaTroMoi); /* tạo thanh toán mới */
                    hopDongDAO.Instance.updateHopDong1(thanhToanDAO.Instance.getMaxID(), maHopDong); /* update hợp đồng*/
                    nhaTroDAO.Instance.checkInStatus(maNhaTroMoi); /* update status nhà trọ mới thành có người*/
                    if (countDAO.Instance.getSVCount(nhaTro.MaNhaTro) == 0) /*Nếu nhà trọ cũ 0 có người*/
                    {
                        nhaTroDAO.Instance.checkOutStatus(nhaTro.MaNhaTro);  /*update status NT -> trống*/
                        thanhToanDAO.Instance.checkOut(maThanhToanMoi); /*update status TT -> đã thanh toán*/
                    }
                    else
                        hopDongDAO.Instance.updateCountDown(maThanhToanCu);/*giảm count ở hợp đồng nhà cũ*/
                    hopDongDAO.Instance.updateCountUp(maThanhToanMoi);/*tăng count ở hợp đồng nhà mới*/
                }
                else
                {
                    hopDongDAO.Instance.updateHopDong2(thanhToanDAO.Instance.getMaThanhToan(maNhaTroMoi), maHopDong, countDAO.Instance.getSVCount(maNhaTroMoi)); /* update hợp đồng*/
                    hopDongDAO.Instance.updateCountUp(thanhToanDAO.Instance.getMaThanhToan(maNhaTroMoi));
                    if (countDAO.Instance.getSVCount(nhaTro.MaNhaTro) == 0) /*Nếu nhà trọ cũ 0 có người*/
                    {
                        nhaTroDAO.Instance.checkOutStatus(nhaTro.MaNhaTro);  /*update status NT -> trống*/
                        thanhToanDAO.Instance.checkOut(maThanhToanCu); /*update status TT -> đã thanh toán*/
                    }
                    else
                        hopDongDAO.Instance.updateCountDown(maThanhToanCu);
                }
               /* hopDongDAO.Instance.autoDeleteHopDong();*/
                loadThongTin(maNhaTroMoi);
                loadNhaTro();
                loadSinhVienByKhoa(tempMaKhoa);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

    }
}
