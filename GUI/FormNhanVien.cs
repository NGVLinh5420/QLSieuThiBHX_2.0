using QLSieuThiBHX.DAO;
using QLSieuThiBHX.DTO;
//using QLSieuThiBHX.CrystalReport;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QLSieuThiBHX.GUI;
using QLSieuThiBHX.Report;
using QLSieuThiBHX.Report2;
using System.Data;

namespace QLSieuThiBHX
{
    public partial class FormNhanVien : Form
    {
        public FormNhanVien()
        {
            InitializeComponent();
        }

        // 0.CSDL
        DAO_NhanVien nhanVien = new DAO_NhanVien();
        List<DTO_NhanVien> listNV = new List<DTO_NhanVien>();
        List<string> listSDT = new List<string>();
        string oldSDT = "";

        // 1.Load
        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            //Giới tính mặc định là NAM
            cobGioiTinh.SelectedIndex = 0;

            LoadListView();

        }
        private void LoadListView()
        {
            lvNhanVien.Items.Clear();
            listSDT.Clear();

            listNV.Clear();
            listNV = nhanVien.ReadDB_TableNhanVien();
            foreach (var i in listNV)
            {
                ListViewItem item = new ListViewItem(i.MaNV.ToString());
                lvNhanVien.Items.Add(item);

                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(item, i.TenNV.ToString());
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem(item, i.GioiTinhNV.ToString());
                item.SubItems.Add(subItem);

                string ngS = DateTime.Parse(i.NgSNV.ToString()).ToShortDateString();

                subItem = new ListViewItem.ListViewSubItem(item, ngS);
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem(item, i.DiaChiNV.ToString());
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem(item, i.SDTNV.ToString());
                item.SubItems.Add(subItem);
                listSDT.Add(i.SDTNV.ToString());
            }
        }
        private void lvNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            ep.Clear();

            if (lvNhanVien.SelectedItems.Count == 1)
            {
                ListViewItem item = lvNhanVien.SelectedItems[0];
                txtMaNV.Text = item.SubItems[0].Text;
                txtTenNV.Text = item.SubItems[1].Text;
                dtpNhanVien.Text = item.SubItems[3].Text;
                txtDiaChiNV.Text = item.SubItems[4].Text;
                txtSDT.Text = item.SubItems[5].Text;
                oldSDT = item.SubItems[5].Text;

                if (item.SubItems[2].Text.ToString().Equals("NAM"))
                {
                    cobGioiTinh.SelectedItem = "NAM";
                }
                else cobGioiTinh.SelectedItem = "NU";

                if (item.Text.Equals("NV0"))
                {
                    MessageBox.Show("Không Thể Sửa hay Xoá Nhân Viên Trainer");
                }
            }
            else
            {
                LamMoi();
            }
        }

        // 2.Event
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            string maNV = "NV" + AutoTaoMa();

            if (BatLoi("none") && !Them_KiemTraTrungSDT())
            {
                //thêm nv
                SqlDateTime ngs = SqlDateTime.Parse(dtpNhanVien.Text);
                nhanVien.AddDB_TableNhanVien(maNV, txtTenNV.Text.ToString(), cobGioiTinh.Text.ToString(), ngs, txtDiaChiNV.Text.ToString(), txtSDT.Text.ToString());

                txtMaNV.Text = maNV;
                MessageBox.Show("THÊM THÀNH CÔNG.", "THÊM", MessageBoxButtons.OK, MessageBoxIcon.None);
                LoadListView();
                LamMoi();
            }
        }
        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if (BatLoi("all") && !Sua_KiemTraTrungSDT())
            {
                SqlDateTime ngs = SqlDateTime.Parse(dtpNhanVien.Text);
                nhanVien.EditDB_TableNhanVien(txtMaNV.Text.ToString(), txtTenNV.Text.ToString(), cobGioiTinh.Text.ToString(),
                    ngs, txtDiaChiNV.Text.ToString(), txtSDT.Text.ToString());

                MessageBox.Show("SỬA THÀNH CÔNG.", "SỬA NHÂN VIÊN.", MessageBoxButtons.OK, MessageBoxIcon.None);
                LoadListView();
            }
        }
        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (BatLoi("all"))
            {
                string maNV = txtMaNV.Text.ToString();
                nhanVien.RemoveDB_TableNhanVien(maNV);
                MessageBox.Show("Xoá Thành Công.", "Xoá Nhân Viên.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                LamMoi();
                LoadListView();
            }
        }
        private void btnLamMoiNV_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        private void btnXuat_Click(object sender, EventArgs e)
        {
            //listNV.Clear();
            //listNV = nhanVien.ReadDB_TableNhanVien();

            string query = "EXEC SP_READ_NHANVIEN";
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            ReportNV rpt = new ReportNV();
            rpt.SetDataSource(table); //Do Not: rpt.SetDataSource(listNV);

            FormIn form = new FormIn();
            form.inNV.ReportSource = rpt;
            form.ShowDialog();
        }


        //Ràng Buộc SDT: Chỉ nhận phím số
        private void txtSDTNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Tự tạo mã mới mỗi khi thêm mới
        private int AutoTaoMa()
        {
            listNV.Clear();
            listNV = nhanVien.ReadDB_TableNhanVien();
            List<int> numbers = new List<int>();

            for (int i = 0; i < listNV.Count; i++)
            {
                int numericPart = int.Parse(ExtractNumericPart(listNV[i].MaNV));
                numbers.Add(numericPart);
            }

            int index = 0;
            foreach (int i in numbers)
            {
                if (index != i) { return index; }
                index++;
            }

            return index = listNV.Count;
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

        //kiểm tra xem SDT có bị trùng hay không: trùng-true
        private bool Sua_KiemTraTrungSDT()
        {
            LoadListView();
            string newSDT = txtSDT.Text;
            listSDT.Remove(oldSDT);

            bool flag = listSDT.Any(sdt => sdt.Equals(newSDT));

            if (flag) MessageBox.Show("SĐT ĐÃ TỒN TẠI !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return flag;
        }
        private bool Them_KiemTraTrungSDT()
        {
            LoadListView();
            string newSDT = txtSDT.Text;

            bool flag = listSDT.Any(sdt => sdt.Equals(newSDT));

            if (flag) MessageBox.Show("SĐT ĐÃ TỒN TẠI !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return flag;
        }

        //----------------
        public void LamMoi()
        {
            ep.Clear();
            txtMaNV.Clear();
            txtTenNV.Clear();
            cobGioiTinh.SelectedIndex = 0;
            dtpNhanVien.Text = "1/1/2000";
            txtDiaChiNV.Clear();
            txtSDT.Clear();

            LoadListView();
        }

        /// <summary>
        /// Nếu key="all" thì sẽ kiểm tra lv có chọn đối tượng nào hay chưa - khi sửa.
        /// key = none thì sẽ không kiểm tra - khi thêm.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true / false</returns>
        /// 
        public bool BatLoi(string key)
        {
            bool flag = true;

            // 1.
            //if (key.Equals("all") && lvNhanVien.SelectedItems.Count == 0)
            //{
            //    MessageBox.Show("CHƯA CHỌN NHÂN VIÊN NÀO !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    ep.SetError(lvNhanVien, "CHƯA CHỌN NHÂN VIÊN NÀO.");
            //    return false;
            //}

            if (lvNhanVien.SelectedItems.Count > 0)
            {
                if (lvNhanVien.SelectedIndices[0] == 0)
                {
                    MessageBox.Show("KHÔNG ĐƯỢC THÊM-XOÁ-SỬA NHÂN VIÊN TRAINER !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ep.SetError(txtMaNV, "KHÔNG ĐƯỢC THÊM-XOÁ-SỬA NHÂN VIÊN TRAINER.");
                    return false;
                }
            }

            // 2.
            if (txtTenNV.TextLength == 0 || txtDiaChiNV.TextLength == 0)
            {
                MessageBox.Show("CHƯA NHẬP TÊN VÀ ĐỊA CHỈ !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
            }

            // KT hai số đầu của số điện thoại
            if (txtSDT.Text.Length == 10)
            {
                string dauso = txtSDT.Text.Substring(0, 2);
                bool ktDauSo = FormHome.dausoDT.Contains(dauso);

                if (!ktDauSo)
                {
                    MessageBox.Show("NHẬP SAI ĐẦU SĐT !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
            }

            if (txtSDT.Text.Length < 10)
            {
                MessageBox.Show("SĐT > 10 SỐ !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
            }

            if (cobGioiTinh.SelectedIndex == -1)
            {
                MessageBox.Show("CHƯA CHỌN GIỚI TÍNH: NAM/NỮ !!", "LỖI!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
            }

            return flag;
        }
    }
}
