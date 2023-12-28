using QLSieuThiBHX.DAO;
using QLSieuThiBHX.DTO;
using QLSieuThiBHX.Report;

//using QLSieuThiBHX.CrystalReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLSieuThiBHX.GUI
{
    public partial class FormHoaDon : Form
    {
        public FormHoaDon()
        {
            InitializeComponent();
        }

        // 0. CSDL
        DAO_HoaDon dao_HoaDon = new DAO_HoaDon();
        List<DTO_HoaDon> listHD = new List<DTO_HoaDon>();
        string maHD;
        List<string> lstTenNV = new List<string>();
        List<string> lstTenKH = new List<string>();

        // 1.Load----------------------------------------------------------------------------------------------------------------------
        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            Load_ListViewHD();
            Load_cobTenKH();
        }
        private void Load_ListViewHD()
        {
            lvHoaDon.Items.Clear();
            listHD.Clear();

            // load
            listHD = dao_HoaDon.ReadDB_TableHoaDon();
            foreach (var i in listHD)
            {
                ListViewItem item = new ListViewItem(i.MaHD.ToString());
                lvHoaDon.Items.Add(item);

                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(item, i.MaKH.ToString());
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem(item, i.MaNV.ToString());
                item.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem(item, DateTime.Parse(i.NgayLapHD.ToString()).ToShortDateString());
                item.SubItems.Add(subItem);
            }

            // Tự động căn đều cột
            //AutoResizeColumns(lvHoaDon);
        }
        private void Load_cobTenKH()
        {
            cobTenKH.Items.Clear();
            cobTenNV.Items.Clear();

            lstTenKH.Clear();
            lstTenNV.Clear();

            //KH
            string queryKH = "SELECT KH.TenKH FROM KHACHHANG KH";
            DataTable tableKH = DataProvider.Instance.ExecuteQuery(queryKH);

            foreach (DataRow row in tableKH.Rows)
            {
                string tenKH = row[0].ToString();
                lstTenKH.Add(tenKH);
            }

            for (int i = 0; i < lstTenKH.Count; i++)
            {
                cobTenKH.Items.Add(lstTenKH[i].ToString());
            }

            //NV
            string queryNV = "SELECT NV.TenNV FROM NHANVIEN NV";
            DataTable tableNV = DataProvider.Instance.ExecuteQuery(queryNV);

            foreach (DataRow row in tableNV.Rows)
            {
                string hoTenNV = row[0].ToString();
                lstTenNV.Add(hoTenNV);
            }

            for (int i = 0; i < lstTenNV.Count; i++)
            {
                cobTenNV.Items.Add(lstTenNV[i].ToString());
            }
        }


        // 2.Event----------------------------------------------------------------------------------------------------------------------
        private void lvHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHoaDon.SelectedItems.Count == 1)
            {
                ListViewItem item = lvHoaDon.SelectedItems[0];
                maHD = item.SubItems[0].Text;
                string ngayHD = item.SubItems[3].Text;

                txtMaHD.Text = maHD;
                dtpHoaDon.Text = DateTime.Parse(ngayHD).ToShortDateString().ToString();

                //Khách Hàng
                string queryMaKH = $"SELECT KH.MaKH  FROM KHACHHANG KH, HOADON HD WHERE HD.MaHD = '{maHD}' AND HD.MaKH=KH.MaKH";
                DataTable tableMaKH = DataProvider.Instance.ExecuteQuery(queryMaKH);
                string maKH = "";
                foreach (DataRow row in tableMaKH.Rows)
                {
                    maKH = row[0].ToString();
                    break;
                }

                string queryTenKH = $"SELECT KH.TenKH  FROM KHACHHANG KH, HOADON HD WHERE HD.MaHD = '{maHD}' AND HD.MaKH=KH.MaKH";
                DataTable tableTenKH = DataProvider.Instance.ExecuteQuery(queryTenKH);
                string tenKH = "";
                foreach (DataRow row in tableTenKH.Rows)
                {
                    tenKH = row[0].ToString();
                    break;
                }

                cobTenKH.Text = null;
                txtMaKH.Text = null;
                Load_cobTenKH();
                cobTenKH.SelectedText = tenKH;
                txtMaKH.Text = maKH;

                //Nhân Viên
                string queryMaNV = $"SELECT NV.MaNV  FROM NHANVIEN NV, HOADON HD WHERE HD.MaHD = '{maHD}' AND HD.MaNV=NV.MaNV";
                DataTable tableMaNV = DataProvider.Instance.ExecuteQuery(queryMaNV);
                string maNV = "";
                foreach (DataRow row in tableMaNV.Rows)
                {
                    maNV = row[0].ToString();
                    break;
                }

                string queryTenNV = $"SELECT NV.TenNV FROM NHANVIEN NV, HOADON HD WHERE HD.MaHD = '{maHD}' AND HD.MaNV=NV.MaNV";
                DataTable tableTenNV = DataProvider.Instance.ExecuteQuery(queryTenNV);
                string tenNV = "";
                foreach (DataRow row in tableTenNV.Rows)
                {
                    tenNV = row[0].ToString();
                    break;
                }

                cobTenNV.Text = null;
                txtMaNV.Text = null;
                Load_cobTenKH();
                cobTenNV.SelectedText = tenNV;
                txtMaNV.Text = maNV;
            }
            else
            {
                //LamMoi();
            }
        }
        private void cob_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaKH.Clear();
            txtMaNV.Clear();

            string queryMaKH = $"SELECT MaKH FROM KHACHHANG WHERE TenKH = '{cobTenKH.Text}'";
            DataTable tableMaKH = DataProvider.Instance.ExecuteQuery(queryMaKH);
            foreach (DataRow row in tableMaKH.Rows)
            {
                txtMaKH.Text = row[0].ToString();
            }

            string queryMaNV = $"SELECT MaNV FROM NHANVIEN WHERE TenNV = '{cobTenNV.Text}'";
            DataTable tableMaNV = DataProvider.Instance.ExecuteQuery(queryMaNV);
            foreach (DataRow row in tableMaNV.Rows)
            {
                txtMaNV.Text = row[0].ToString();
            }
        }
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (cobTenKH.Text != null && cobTenNV.Text != null)
            {
                string maKH = txtMaKH.Text;
                string maNV = txtMaNV.Text;

                SqlDateTime ngs = SqlDateTime.Parse(dtpHoaDon.Text);
                maHD = "HD" + AutoTaoMa();
                dao_HoaDon.AddDB_TableHoaDon(maHD, maKH, maNV, ngs);
                MessageBox.Show("Them Thanh Cong.");
                LamMoi();
            }
            else
                MessageBox.Show("Chua Nhap Du Thong Tin.");
        }
        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            SqlDateTime ng = SqlDateTime.Parse(dtpHoaDon.Text);
            dao_HoaDon.EditDB_TableHoaDon(maHD, txtMaKH.Text, txtMaNV.Text, ng);
            MessageBox.Show("Sua Thanh Cong.");
            LamMoi();

        }
        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (lvHoaDon.SelectedItems.Count == 1)
            {
                dao_HoaDon.RemoveDB_TableHoaDon(maHD);

                MessageBox.Show("Xoa Thanh Cong.");
                LamMoi();
            }
        }
        private void btnLamMoiSP_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        private void btnXuat_Click(object sender, EventArgs e)
        {
            //listHD.Clear();
            //listHD = dao_HoaDon.ReadDB_TableHoaDon();

            //ReportHD reportHD = new ReportHD();
            //reportHD.SetDataSource(listHD);

            FormIn formIN = new FormIn();
            //formIN.crystalReportViewer1.ReportSource = reportHD;
            formIN.ShowDialog();

        }

        //Bắt SK: Yêu cầu nhập-chọn đúng đối tượng có trong cob
        private void cobTenKH_Validating(object sender, CancelEventArgs e)
        {
            // Kiểm tra nếu ký tự nhập vào không tồn tại trong danh sách các mục của cobTenKH
            if (cobTenKH.FindStringExact(cobTenKH.Text) == -1)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show("Ký tự không hợp lệ. Vui lòng chọn từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Huỷ sự kiện Validating để giữ người dùng ở lại cobTenKH
                e.Cancel = true;

                // Xóa nội dung đã nhập để người dùng nhập lại
                cobTenKH.Text = string.Empty;
            }
        }
        private void cobTenNV_Validating(object sender, CancelEventArgs e)
        {
            // Kiểm tra nếu ký tự nhập vào không tồn tại trong danh sách các mục của cobTenNV
            if (cobTenNV.FindStringExact(cobTenNV.Text) == -1)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show("Ký tự không hợp lệ. Vui lòng chọn từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Huỷ sự kiện Validating để giữ người dùng ở lại cobTenNV
                e.Cancel = true;

                // Xóa nội dung đã nhập để người dùng nhập lại
                cobTenNV.Text = string.Empty;
            }
        }


        private void LamMoi()
        {
            cobTenKH.Items.Clear();
            cobTenKH.Text = string.Empty;
            cobTenNV.Items.Clear();
            cobTenNV.Text = string.Empty;


            txtMaHD.Clear();
            txtMaKH.Clear();
            txtMaNV.Clear();
            dtpHoaDon.Text = DateTime.Now.ToString();

            Load_ListViewHD();
            Load_cobTenKH();
        }

        //Tự tạo mã mới mỗi khi thêm mới
        private int AutoTaoMa()
        {
            listHD.Clear();
            listHD = dao_HoaDon.ReadDB_TableHoaDon();
            List<int> numbers = new List<int>();

            for (int i = 0; i < listHD.Count; i++)
            {
                int numericPart = int.Parse(ExtractNumericPart(listHD[i].MaHD));
                numbers.Add(numericPart);
            }

            int index = 1;
            foreach (int i in numbers)
            {
                if (index != i) { return index; }
                index++;
            }

            return index = listHD.Count;
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
    }
}
