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

        void showThongTin(int id)
        {
            listView1.Items.Clear();
            List<thongTinTro> thongTinTroList = thongTinTroDAO.Instance.getMenu(id);
            foreach (thongTinTro item in thongTinTroList)
            {
                ListViewItem Item = new ListViewItem(item.Name.ToString());
                Item.SubItems.Add(item.DienThoai.ToString());
                Item.SubItems.Add(item.Gia.ToString());
                Item.SubItems.Add(item.DateCheckIn.ToShortDateString());
                Item.SubItems.Add(item.TienNha.ToString());
                listView1.Items.Add(Item);
            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            List<nhaTro> nhaTroList = nhaTroDAO.Instance.getTable();
            int tableID = ((sender as Button).Tag as nhaTro).MaNhaTro;
            showThongTin(tableID);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
