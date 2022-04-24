using QuanLyNhaTro.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace QuanLyNhaTro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_Click(object sender, EventArgs e)
        {
            fManager f = new fManager();
            f.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show
                ("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.OKCancel);
            if (h == DialogResult.OK)
                Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Login(textBox1.Text, textBox2.Text))
            {
                this.Hide();
                fManager fManager = new fManager();
                fManager.Closed += (s, args) => this.Close();
                fManager.Show();
            }    
            else
                MessageBox.Show("Đăng nhập lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        bool Login(string user, string pass)
        {
            return accountDAO.Instance.Login(user, pass);

        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show
                ("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.OKCancel);
            if (h == DialogResult.OK)
                Application.Exit();
        }
    }
}
