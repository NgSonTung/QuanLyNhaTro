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
            comboBox6.DataSource = null;
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
            List<count> count = countDAO.Instance.getSVCount(2);
            comboBox6.Items.Add(count.Count);
        }
        public int tempMaKhoa;
        
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

        private void button2_Click(object sender, EventArgs e)
        {
            int oldId = (dataGridView1.Tag as nhaTro).MaNhaTro;
            int newId = (comboBox3.SelectedItem as nhaTro).MaNhaTro;
            nhaTroDAO.Instance.checkOutStatus(oldId);
            thanhToanDAO.Instance.chuyenStatus(newId, oldId);
            nhaTroDAO.Instance.checkInStatus(newId);
            loadNhaTro();
            loadThongTin(newId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nhaTro nhaTro = dataGridView1.Tag as nhaTro;
            List<count> count = countDAO.Instance.getSVCount(nhaTro.MaNhaTro);
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
                hopDongDAO.Instance.insertBillInfo(maThanhToan, maSinhVien, count.Count);
                sinhVienDAO.Instance.statusCoTro(maSinhVien);
                hopDongDAO.Instance.updateCount(maThanhToan);

            }
            loadThongTin(nhaTro.MaNhaTro);
            loadNhaTro();
            loadSinhVienByKhoa(tempMaKhoa);
        }
    }
}
