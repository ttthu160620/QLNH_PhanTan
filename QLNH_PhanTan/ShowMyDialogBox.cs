using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNH_PhanTan
{
    public partial class ShowMyDialogBox : Form
    {
        public ShowMyDialogBox()
        {
            InitializeComponent();
        }
        public string result = "";
        private void button1_Click(object sender, EventArgs e)
        {
            result = cmbChiNhanh.Text;
            this.Close();
        }

        private void ShowMyDialogBox_Load(object sender, EventArgs e)
        {
            result = "";
            this.Get_SubscribesTableAdapter.Fill(this.dS.Get_Subscribes);
            Program.bdsDSPM = this.bdsDSPM;
            cmbChiNhanh.DataSource = Program.bdsDSPM;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mChinhanh;
            //cmbChiNhanh.SelectedIndex = 1;
            //cmbChiNhanh.SelectedIndex = 0;
        }
        public void setLabel(string label)
        {
            label1.Text = label;
        }
       
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
