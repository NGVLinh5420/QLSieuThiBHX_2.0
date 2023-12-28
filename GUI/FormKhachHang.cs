using QLSieuThiBHX.DAO;
using QLSieuThiBHX.DTO;
using QLSieuThiBHX.GUI;
using QLSieuThiBHX.Report;

//using QLSieuThiBHX.CrystalReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QLSieuThiBHX
{
    public partial class FormKhachHang : Form
    {
        //--Ctrl M + O
        //--Ctrl M + P
        public FormKhachHang()
        {
            InitializeComponent();
        }

        // 0. CSDL
        DAO_KhachHang khachHang = new DAO_KhachHang();
        List<DTO_KhachHang> listKH = new List<DTO_KhachHang>();
        List<String> listSDT = new List<String>();
        String oldSDT = "";

        // 1.Load
        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            LoadLVKhachHang();
        }
        private void LoadLVKhachHang()
        {
            lvKhachHang.Items.Clear();

            listKH.Clear();
            listSDT.Clear();

            listKH = khachHang.ReadDB_TableKhachHang();
            foreach (var i in listKH)
            {
                ListViewItem item = new ListViewItem(i.MaKH.ToString());
                lvKhachHang.Items.Add(item);

                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(item, i.HoTenKH.ToString());
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem(item, i.DiaChiKH.ToString());
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem(item, i.SDTKH.ToString());
                item.SubItems.Add(subItem);
                listSDT.Add(i.SDTKH.ToString());
            }
        }
        private void lvKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvKhachHang.SelectedItems.Count == 1)
            {
                ListViewItem item = lvKhachHang.SelectedItems[0];
                txtMaKH.Text = item.SubItems[0].Text;
                txtHoTenKH.Text = item.SubItems[1].Text;
                txtDiaChiKH.Text = item.SubItems[2].Text;
                txtSDT.Text = item.SubItems[3].Text;

                if (item.Text.Equals("KH0"))
                {
                    MessageBox.Show("Không Thể Sửa hay Xoá Khách Hàng Lạ");
                }

                oldSDT = txtSDT.Text;
            }
            else
            {
                LamMoi();
            }
        }

        // 2.Event
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            if (txtHoTenKH.TextLength > 0 && txtDiaChiKH.TextLength > 0 && txtSDT.TextLength == 10 && txtMaKH.Text != "KH0")
            {
                if (!Them_KiemTraTrungSDT())
                {
                    string maKH = "KH" + AutoTaoMa();

                    khachHang.AddDB_TableKhachHang(maKH, txtHoTenKH.Text.ToString(),
                    txtDiaChiKH.Text.ToString(), txtSDT.Text.ToString());

                    MessageBox.Show("Thêm Thành Công.", "ĐÃ THÊM", MessageBoxButtons.OK);
                    LoadLVKhachHang();
                }
                else
                {
                    MessageBox.Show("SDT ĐÃ TỒN TẠI \nHÃY ĐỔI SỐ KHÁC.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Yêu cầu nhập Đủ và Đúng thông tin!! \nVà không chỉnh sửa Khách Hàng Lạ.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            if (txtHoTenKH.TextLength > 0 && txtDiaChiKH.TextLength > 0 && txtSDT.TextLength == 10 && txtMaKH.Text != "KH0")
            {
                if (!Sua_KiemTraTrungSDT())
                {
                    khachHang.EditDB_TableKhachHang(txtMaKH.Text.ToString(), txtHoTenKH.Text.ToString(), txtDiaChiKH.Text.ToString(), txtSDT.Text.ToString());

                    MessageBox.Show("SỬA THÀNH CÔNG.", "SỬA", MessageBoxButtons.OK);
                    LoadLVKhachHang();
                }
                else
                {
                    MessageBox.Show("SDT ĐÃ TỒN TẠI \nHÃY ĐỔI SỐ KHÁC.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Yêu cầu nhập Đủ và Đúng thông tin.!! \nVà không chỉnh sửa Khách Hàng Lạ.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text != string.Empty && txtMaKH.Text != "KH0")
            {
                khachHang.RemoveDB_TableKhachHang(txtMaKH.Text.ToString());

                MessageBox.Show("Xoá Thành Công.", "XOÁ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LoadLVKhachHang();
            }
            else
            {
                MessageBox.Show("Yêu cầu chọn khách hàng!! \nVà không chỉnh sửa Khách Hàng Lạ.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            LamMoi();
        }


        public void LamMoi()
        {
            txtMaKH.Clear();
            txtHoTenKH.Clear();
            txtDiaChiKH.Clear();
            txtSDT.Clear();

            LoadLVKhachHang();
        }
        private void txtSDTKH_KeyPress(object sender, KeyPressEventArgs e) //Chỉ nhận số cho txtSDT

        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnXuat_Click(object sender, EventArgs e)
        {
            //    listKH.Clear();
            //    listKH = khachHang.ReadDB_TableKhachHang();

            //    ReportKH reportKH = new ReportKH();
            //    reportKH.SetDataSource(listKH);

            FormIn formIN = new FormIn();
            //    formIN.crystalReportViewer1.ReportSource = reportKH;
            formIN.ShowDialog();

        }

        //Tự tạo mã mới mỗi khi thêm mới
        private int AutoTaoMa()
        {
            listKH.Clear();
            listKH = khachHang.ReadDB_TableKhachHang();
            List<int> numbers = new List<int>();

            for (int i = 0; i < listKH.Count; i++)
            {
                int numericPart = int.Parse(ExtractNumericPart(listKH[i].MaKH));
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

        //kiểm tra xem SDT có bị trùng hay không.
        private bool Sua_KiemTraTrungSDT()
        {
            LoadLVKhachHang();
            string newSDT = txtSDT.Text;
            listSDT.Remove(oldSDT);

            bool flag = listSDT.Any(sdt => sdt.Equals(newSDT));
            return flag;
        }
        private bool Them_KiemTraTrungSDT()
        {
            LoadLVKhachHang();
            string newSDT = txtSDT.Text;

            bool flag = listSDT.Any(sdt => sdt.Equals(newSDT));
            return flag;
        }
    }
}
