using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using QLSieuThiBHX.DAO;
using QLSieuThiBHX.DTO;
using QLSieuThiBHX.GUI;
using QLSieuThiBHX.Report;

//using QLSieuThiBHX.CrystalReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSieuThiBHX
{
    public partial class FormSanPham : Form
    {
        public FormSanPham()
        {
            InitializeComponent();
        }

        // 0.CSDL
        DAO_SanPham dao_SanPham = new DAO_SanPham();
        DAO_Kho dao_Kho = new DAO_Kho();
        List<DTO_SanPham> listSP = new List<DTO_SanPham>();
        List<String> listDonVi = new List<String>();

        // 1.Load
        private void FormSanPham_Load(object sender, EventArgs e)
        {
            //Đơn Vị mặc định: CÁI
            cobDonVi.SelectedItem = "CAI";

            Load_ListView_ComboBox();
        }
        private void Load_ListView_ComboBox()
        {
            lvSanPham.Items.Clear();
            cobDonVi.Items.Clear();
            listSP.Clear();
            listDonVi.Clear();

            listSP = dao_SanPham.Read_SanPham();
            foreach (var i in listSP)
            {
                ListViewItem item = new ListViewItem(i.MaSP.ToString());
                item.SubItems.Add(i.TenSP.ToString());
                item.SubItems.Add(i.TonKho.ToString());
                item.SubItems.Add(i.DonViTinhSP.ToString());
                item.SubItems.Add(i.DonGiaSP.ToString());

                listDonVi.Add(i.DonViTinhSP.ToString());
                lvSanPham.Items.Add(item);
            }
            cobDonVi.Items.AddRange(listDonVi.ToArray());
            cobDonVi.SelectedIndex = 0;
        }

        // 2.Event
        private void lvSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSanPham.SelectedItems.Count == 1)
            {
                ListViewItem item = lvSanPham.SelectedItems[0];
                txtMaSP.Text = item.SubItems[0].Text;
                txtTenSP.Text = item.SubItems[1].Text;
                txtTonKho.Text = item.SubItems[2].Text;
                cobDonVi.Text = item.SubItems[3].Text;
                txtDonGia.Text = item.SubItems[4].Text;

                int tongTien = 0;
                tongTien = int.Parse(txtDonGia.Text) * int.Parse(txtTonKho.Text);
                txtTongTien.Text = tongTien.ToString();
            }
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (BatLoi("none"))
            {
                string maSP = "SP" + AutoTaoMa();
                int gia = int.Parse(txtDonGia.Text);
                int tonKho = int.Parse(txtTonKho.Text);

                dao_SanPham.AddDB_TableSanPham(maSP, txtTenSP.Text, tonKho, cobDonVi.Text, gia);

                MessageBox.Show("Thêm Thành Công.", "THÊM", MessageBoxButtons.OK, MessageBoxIcon.None);
                LamMoi();
            }
            else return;
        }
        private void btnThemSL_Click(object sender, EventArgs e)
        {
            if (txtTonKho.Text.Length > 0 && lvSanPham.SelectedItems.Count > 0)
            {
                if (!lvSanPham.SelectedItems[0].Text.Equals("SP0"))
                {
                    string maSP = txtMaSP.Text;
                    int tonKhoOld = int.Parse(lvSanPham.SelectedItems[0].SubItems[2].Text);
                    int tonKhoNew = int.Parse(txtTonKho.Text);

                    if ((tonKhoOld + tonKhoNew) > 1000)
                    {
                        MessageBox.Show("Số Lượng Sản Phẩm Tồn Kho Không Quá 1000.");
                        txtTonKho.Text = "" + (1000 - tonKhoOld);
                        return;
                    }
                    else
                    {
                        dao_Kho.Edit_Kho(maSP, tonKhoOld + tonKhoNew);
                        MessageBox.Show("Đã Thêm Số Lượng Sản Phẩm.");

                        LamMoi();
                    }
                }
                else
                    MessageBox.Show("Không sửa sản phẩm số 0.");
            }
            else
            {
                MessageBox.Show("Phải chọn Sản Phẩm hoặc Nhập Số Lượng để Thêm.");
                return;
            }
        }
        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            if (BatLoi("all"))
            {
                int gia = int.Parse(txtDonGia.Text);
                int tonKho = int.Parse(txtTonKho.Text);

                dao_SanPham.EditDB_TableSanPham(txtMaSP.Text, txtTenSP.Text, tonKho, cobDonVi.Text, gia);

                MessageBox.Show("Sửa Thành Công.", "SỬA", MessageBoxButtons.OK, MessageBoxIcon.None);
                LamMoi();
            }
        }
        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (lvSanPham.SelectedItems.Count > 0 && !txtMaSP.Text.Equals("SP0"))
            {
                dao_SanPham.RemoveDB_TableSanPham(txtMaSP.Text);
                MessageBox.Show("Xoá Thành Công.", "XOÁ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LamMoi();
            }
            else
            {
                if (lvSanPham.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Bạn chưa chọn sản phẩm!!", "Warning");
                }
                else
                    MessageBox.Show("Không Được Xoá Sản Phẩm Số 0!!", "Warning");
            }
        }
        private void btnLamMoiSP_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        private void btnXuat_Click(object sender, EventArgs e)
        {
            //listSP.Clear();
            //listSP = dao_SanPham.Read_SanPham();

            //ReportSP reportSP = new ReportSP();
            //reportSP.SetDataSource(listSP);

            FormIn formIN = new FormIn();
            //formIN.crystalReportViewer1.ReportSource = reportSP;
            formIN.ShowDialog();
        }



        //Bắt sự kiện khi Nhập
        private void txtDonGiaSP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsNumber(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra giá trị của TextBox khi người dùng thay đổi
            if (!string.IsNullOrEmpty(txtDonGia.Text))
            {
                int value = int.Parse(txtDonGia.Text);

                // Giới hạn giá trị trên 0
                if (value <= 0 && !lvSanPham.SelectedItems[0].SubItems[0].Text.Equals("SP0"))
                {
                    MessageBox.Show("Vui lòng nhập giá trị trên 0.");
                    txtDonGia.Text = lvSanPham.SelectedItems[0].SubItems[4].Text;
                }
            }
        }
        private void txtTonKho_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự nhập vào có phải là số hoặc phím backspace không
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Nếu không phải số hoặc backspace, không cho phép nhập
            }
        }
        private void txtTonKho_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra giá trị của TextBox khi người dùng thay đổi
            if (!string.IsNullOrEmpty(txtTonKho.Text) && lvSanPham.SelectedItems.Count > 0)
            {
                int value = int.Parse(txtTonKho.Text);

                // Giới hạn giá trị dưới 1001
                if (value >= 1001)
                {
                    MessageBox.Show("Vui lòng nhập giá trị dưới 1001.");
                    txtTonKho.Text = "1000";
                }
            }
            else
            {
                if (lvSanPham.SelectedItems.Count > 0)
                {
                    MessageBox.Show("Vui lòng nhập tồn kho cho sản phẩm.");
                }
                else
                    MessageBox.Show("Vui lòng chọn sản phẩm.");
            }
        }

        //Tự tạo mã mới mỗi khi thêm mới
        private int AutoTaoMa()
        {
            listSP.Clear();
            listSP = dao_SanPham.Read_SanPham();
            List<int> numbers = new List<int>();

            for (int i = 0; i < listSP.Count; i++)
            {
                int numericPart = int.Parse(ExtractNumericPart(listSP[i].MaSP));
                numbers.Add(numericPart);
            }

            int index = 0;
            foreach (int i in numbers)
            {
                if (index != i) { break; }
                index++;
            }

            return index;
        }
        static string ExtractNumericPart(string input)
        {
            // Sử dụng Regex để tìm các chữ số trong chuỗi
            Match match = Regex.Match(input, @"\d+");

            // Kiểm tra xem có kết quả nào được tìm thấy hay không
            if (match.Success)
            {
                return match.Value;
            }

            // Trả về một giá trị mặc định nếu không tìm thấy chữ số
            return "Không tìm thấy số";
        }


        //--------------
        public void LamMoi()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtTonKho.Text = "0";
            txtDonGia.Clear();
            txtTongTien.Text = "0";
            cobDonVi.SelectedIndex = 0;

            Load_ListView_ComboBox();
        }

        /// <summary>
        /// Sửa: key = all. Thêm: key = none
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool BatLoi(string key)
        {
            bool flag = true;

            // 1.
            if (key.Equals("all") && lvSanPham.SelectedItems.Count == 0)
            {
                MessageBox.Show("CHƯA CHỌN SẢN PHẨM NÀO !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (lvSanPham.SelectedItems.Count > 0)
            {
                if (lvSanPham.SelectedIndices[0] == 0)
                {
                    MessageBox.Show("KHÔNG ĐƯỢC THÊM-XOÁ-SỬA SẢN PHẨM SỐ 0 !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // 2.
            if (txtTenSP.TextLength == 0)
            {
                MessageBox.Show("CHƯA NHẬP TÊN !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
            }
            if (txtDonGia.TextLength == 0)
            {
                MessageBox.Show("CHƯA NHẬP ĐƠN GIÁ !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
            }
            if (txtTonKho.TextLength == 0)
            {
                MessageBox.Show("CHƯA NHẬP SỐ LƯỢNG TỒN KHO !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
            }

            if (cobDonVi.SelectedIndex == -1)
            {
                MessageBox.Show("CHƯA CHỌN ĐƠN VỊ TÍNH !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
            }

            return flag;
        }

        private void btnSuaSL_Click(object sender, EventArgs e)
        {
            if (txtMaSP.Text.Length > 0 && txtTonKho.Text.Length > 0)
            {
                string maSP = txtMaSP.Text;
                int tonKho = int.Parse(txtTonKho.Text);

                // Người dùng chọn No hoặc đóng MessageBox
                dao_Kho.Edit_Kho(maSP, tonKho);
                MessageBox.Show("Đã Sửa Số Lượng Sản Phẩm.");

                Load_ListView_ComboBox();
                LamMoi();
            }
            else
            {
                MessageBox.Show("Phải chọn Sản Phẩm hoặc Nhập Số Lượng để Thêm.");
                return;
            }
        }
    }
}
