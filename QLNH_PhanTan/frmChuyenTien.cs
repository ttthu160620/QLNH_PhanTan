using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace QLNH_PhanTan
{
    public partial class frmChuyenTien : DevExpress.XtraEditors.XtraForm
    {
        public frmChuyenTien()
        {
            InitializeComponent();
        }

        private void chiNhanhBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {

        }

        private void frmChuyenTien_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.GD_CHUYENTIEN' table. You can move, or remove it, as needed.
            this.gD_CHUYENTIENTableAdapter.Fill(this.dS.GD_CHUYENTIEN);
            txtMaNV.Text = Program.username;
            txtHoTen.Text = Program.mHoten;
            
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {

            if (txtSoTKChuyen.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số tài khoản chuyển", "Thông báo !", MessageBoxButtons.OK);
                txtSoTKChuyen.Focus();
                return;
            }
            else if (txtSoTKNhan.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số tài khoản nhận", "Thông báo !", MessageBoxButtons.OK);
                txtSoTKNhan.Focus();
                return;
            }
            else if (txtSoTKChuyen.Text.Trim()==txtSoTKNhan.Text.Trim())
            {
                MessageBox.Show("Tài khoản nhận và tài khoản chuyển không được trùng nhau", "Thông báo !", MessageBoxButtons.OK);
                txtSoTKNhan.Focus();
                return;
            }
            else if (txtSoTien.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số tiền cần chuyển", "Thông báo !", MessageBoxButtons.OK);
                txtSoTien.Focus();
                return;
            }                  
            else
            {
               
                try
                {
                    if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
                    String str_sp = "SP_CHUYENTIEN";
                    Program.Sqlcmd = Program.conn.CreateCommand();
                    Program.Sqlcmd.CommandType = CommandType.StoredProcedure;
                    Program.Sqlcmd.CommandText = str_sp;
                    Program.Sqlcmd.Parameters.Add("@STKCHUYEN", SqlDbType.NChar).Value = txtSoTKChuyen.Text.Trim();
                    Program.Sqlcmd.Parameters.Add("@STKNHAN", SqlDbType.NChar).Value = txtSoTKNhan.Text.Trim();
                    Program.Sqlcmd.Parameters.Add("@SOTIEN", SqlDbType.Money).Value = txtSoTien.Text.Trim();
                    Program.Sqlcmd.Parameters.Add("@MANV", SqlDbType.NChar).Value = txtMaNV.Text.Trim();
                    Program.Sqlcmd.Parameters.Add("@Ret", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    Program.Sqlcmd.ExecuteNonQuery();
                    String ret = Program.Sqlcmd.Parameters["@RET"].Value.ToString();                   
                    if (ret == "0")
                    {
                        MessageBox.Show("Tài khoản chuyển không tồn tại", "Thông báo !", MessageBoxButtons.OK);
                        return;
                    }
                    else if (ret == "1")
                    {
                        MessageBox.Show("Tài khoản nhận không tồn tại", "Thông báo !", MessageBoxButtons.OK);
                        return;

                    }
                    if (ret == "2")
                    {
                        MessageBox.Show("Số dư không đủ.",
                             "Xác thực", MessageBoxButtons.OK);
                        return;
                    }
                    if (ret == "3")
                    {

                        txtSoTKChuyen.Text = "";
                        txtSoTKNhan.Text = "";
                        txtSoTien.Text = "";
                        this.gD_CHUYENTIENTableAdapter.Connection.ConnectionString = Program.connstr;
                        this.gD_CHUYENTIENTableAdapter.Fill(this.dS.GD_CHUYENTIEN);
                        MessageBox.Show("Chuyển tiền thành công", "Thông báo !", MessageBoxButtons.OK);
                        return;
                    }
                    if (ret == "4")
                    {
                        MessageBox.Show("Lỗi chuyển tiền", "Thông báo !", MessageBoxButtons.OK);
                        return;
                    }
                    

                }
                catch (Exception E)
                {
                    MessageBox.Show("Chuyển tiền thất bại: "+E.Message,
                    "Xác thực", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn thật sự muốn hủy thao tác chuyển tiền?",
                     "Xác thực", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }
            else if (dr == DialogResult.Yes)
            {
                this.Close();

            }
        }

        private void btnThoat_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gD_CHUYENTIENBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.gD_CHUYENTIENBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }
    }
}