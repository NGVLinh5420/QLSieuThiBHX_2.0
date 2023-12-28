using QLSieuThiBHX.DAO;
using QLSieuThiBHX.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSieuThiBHX.GUI
{
    public partial class FormChiTietHD : Form
    {
        public FormChiTietHD()
        {
            InitializeComponent();
        }

        //0.CSDL-------------------------------------------------------------------------------------------------------------------
        DAO_ChiTietHD dao_ChiTietHD = new DAO_ChiTietHD();
        List<DTO_HoaDon> lstHD = new List<DTO_HoaDon>();
        List<DTO_SP_SL_Gia> lstSP = new List<DTO_SP_SL_Gia>();
        string maHD;
        string maSP;
        long tongTien;
        int yearMin = 0;
        int yearMax = 0;


        //1.Load-------------------------------------------------------------------------------------------------------------------
        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            Load_ListViewHD();
            LoadComboBox();
        }
        private void Load_ListViewHD()
        {
            lvHD.Items.Clear();
            lvSanPham.Items.Clear();

            // load
            lstHD.Clear();
            lstHD = dao_ChiTietHD.Read_HoaDon();
            foreach (var i in lstHD)
            {
                ListViewItem item = new ListViewItem(i.MaHD.ToString());
                lvHD.Items.Add(item);

                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(item, DateTime.Parse(i.NgayLapHD.ToString()).ToShortDateString());
                item.SubItems.Add(subItem);
            }
        }
        private void Load_ListViewSP(String maHD)
        {
            tongTien = 0;
            txtTongTien.Clear();

            lvSanPham.Items.Clear();
            lstSP = dao_ChiTietHD.ReadDB_Select_SP(maHD);

            foreach (var i in lstSP)
            {
                ListViewItem itemSP = new ListViewItem(i.MaSP.ToString());
                lvSanPham.Items.Add(itemSP);

                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(itemSP, i.TenSP.ToString());
                itemSP.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem(itemSP, i.TongSoLuong.ToString());
                itemSP.SubItems.Add(subItem);

                subItem = new ListViewItem.ListViewSubItem(itemSP, i.ThanhTien.ToString());
                itemSP.SubItems.Add(subItem);
                tongTien += i.ThanhTien;
            }

            txtTongTien.Text = tongTien.ToString();
        }
        private void LoadComboBox()
        {
            cobTenKH.Items.Clear();
            cobTenNV.Items.Clear();
            cobTenSP.Items.Clear();

            //DS chứa tên cho cob
            List<String> listTenKH = new List<String>();
            List<String> listHoTenNV = new List<String>();
            List<String> listTenSP = new List<string>();

            //KH
            string queryKH = "SELECT KH.TenKH FROM KHACHHANG KH";
            DataTable tableKH = DataProvider.Instance.ExecuteQuery(queryKH);

            foreach (DataRow row in tableKH.Rows)
            {
                string TenKH = row[0].ToString();
                listTenKH.Add(TenKH);
            }

            for (int i = 0; i < listTenKH.Count; i++)
            {
                cobTenKH.Items.Add(listTenKH[i].ToString());
            }

            //NV
            string queryNV = "SELECT NV.TenNV FROM NHANVIEN NV";
            DataTable tableNV = DataProvider.Instance.ExecuteQuery(queryNV);

            foreach (DataRow row in tableNV.Rows)
            {
                string hoTenNV = row[0].ToString();
                listHoTenNV.Add(hoTenNV);
            }

            for (int i = 0; i < listHoTenNV.Count; i++)
            {
                cobTenNV.Items.Add(listHoTenNV[i].ToString());
            }

            //SP
            string querySP = "SELECT SP.TenSP FROM SANPHAM SP";
            DataTable tableSP = DataProvider.Instance.ExecuteQuery(querySP);

            foreach (DataRow row in tableSP.Rows)
            {
                string tenSP = row[0].ToString();
                listTenSP.Add(tenSP);
            }

            for (int i = 0; i < listTenSP.Count; i++)
            {
                cobTenSP.Items.Add(listTenSP[i].ToString());
            }

            // cobThang, cobNam
            DataTable table = DataProvider.Instance.ExecuteQuery("EXEC SP_HoaDon_YearMin_Max");
            foreach (DataRow row in table.Rows)
            {
                yearMin = Convert.ToInt32(row["NamMin"]);
                yearMax = Convert.ToInt32(row["NamMax"]);
            }

            for (int i = yearMin; i <= yearMax; i++)
            {
                cobNam.Items.Add(i.ToString());
            }

        }


        //2.Event------------------------------------------------------------------------------------------------------------------
        private void lvChiTietHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHD.SelectedItems.Count == 1)
            {
                ListViewItem item = lvHD.SelectedItems[0];
                maHD = item.SubItems[0].Text;

                //Nhân Viên
                string queryTenNV = $"SELECT NV.TenNV FROM NHANVIEN NV, HOADON HD WHERE HD.MaHD = '{maHD}' AND HD.MaNV=NV.MaNV";
                DataTable tableTenNV = DataProvider.Instance.ExecuteQuery(queryTenNV);
                string tenNV = "";
                foreach (DataRow row in tableTenNV.Rows)
                {
                    tenNV = row[0].ToString();
                    break;
                }

                string queryMaNV = $"SELECT NV.MaNV  FROM NHANVIEN NV, HOADON HD WHERE HD.MaHD = '{maHD}' AND HD.MaNV=NV.MaNV";
                DataTable tableMaNV = DataProvider.Instance.ExecuteQuery(queryMaNV);
                string maNV = "";
                foreach (DataRow row in tableMaNV.Rows)
                {
                    maNV = row[0].ToString();
                    break;
                }

                cobTenNV.Text = null;
                txtMaNV.Text = null;
                LoadComboBox();
                cobTenNV.SelectedText = tenNV;
                txtMaNV.Text = maNV;

                //Khách Hàng
                string queryTenKH = $"SELECT KH.TenKH  FROM KHACHHANG KH, HOADON HD WHERE HD.MaHD = '{maHD}' AND HD.MaKH=KH.MaKH";
                DataTable tableTenKH = DataProvider.Instance.ExecuteQuery(queryTenKH);
                string tenKH = "";
                foreach (DataRow row in tableTenKH.Rows)
                {
                    tenKH = row[0].ToString();
                    break;
                }

                string queryMaKH = $"SELECT KH.MaKH  FROM KHACHHANG KH, HOADON HD WHERE HD.MaHD = '{maHD}' AND HD.MaKH=KH.MaKH";
                DataTable tableMaKH = DataProvider.Instance.ExecuteQuery(queryMaKH);
                string maKH = "";
                foreach (DataRow row in tableMaKH.Rows)
                {
                    maKH = row[0].ToString();
                    break;
                }
                cobTenKH.Text = null;
                txtMaKH.Text = null;
                LoadComboBox();
                cobTenKH.SelectedText = tenKH;
                txtMaKH.Text = maKH;

                //Load ListView Sản phẩm sau khi click vào Hoá Đơn
                Load_ListViewSP(maHD);
            }
        }
        private void lvSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            cobTenSP.Text = null;
            txtMaSP.Text = null;
            cobTenSP.Text = null;
            txtSoLuong.Text = null;
            txtTonKho.Text = null;

            if (lvSanPham.SelectedItems.Count == 1)
            {
                ListViewItem item = lvSanPham.SelectedItems[0];
                maSP = item.SubItems[0].Text;

                //Sản Phẩm
                string queryTenSP = $"SELECT TenSP FROM SANPHAM WHERE MaSP = '{maSP}'";

                DataTable tableTenSP = DataProvider.Instance.ExecuteQuery(queryTenSP);
                string tenSP = "";
                foreach (DataRow row in tableTenSP.Rows)
                {
                    tenSP = row[0].ToString();
                    break;
                }

                LoadComboBox();
                cobTenSP.SelectedText = tenSP;
                txtMaSP.Text = maSP;

                //Số Lượng TonKho
                Load_TonKho(maSP);

                //Số Lượng SP
                string querySoLuongSP = $"SELECT SoLuong FROM CHITIETHD WHERE MaSP = '{maSP}' AND MaHD = '{maHD}'";

                DataTable tableSLSP = DataProvider.Instance.ExecuteQuery(querySoLuongSP);
                String soLuongSP = "-1";
                foreach (DataRow row in tableSLSP.Rows)
                {
                    soLuongSP = row[0].ToString();
                    break;
                }

                txtSoLuong.Text = soLuongSP;
            }
        }

        private void Load_TonKho(string maSP)
        {
            //Số Lượng TonKho
            string queryTonKho = $"SELECT TonKho FROM KHO WHERE MaSP = '{maSP}'";

            DataTable tableTK = DataProvider.Instance.ExecuteQuery(queryTonKho);
            String tonKho = "-1";
            foreach (DataRow row in tableTK.Rows)
            {
                tonKho = row[0].ToString();
                break;
            }

            txtTonKho.Text = tonKho;
        }

        private void cob_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaKH.Clear();
            txtMaNV.Clear();
            txtMaSP.Clear();
            txtTonKho.Clear();

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

            string queryMaSP = $"SELECT MaSP FROM SANPHAM WHERE TenSP = '{cobTenSP.Text}'";
            DataTable tableMaSP = DataProvider.Instance.ExecuteQuery(queryMaSP);
            foreach (DataRow row in tableMaSP.Rows)
            {
                txtMaSP.Text = row[0].ToString();
            }

            string queryTonKho = $"SELECT TonKho FROM KHO WHERE MaSP = '{txtMaSP.Text}'";
            DataTable tableTonKho = DataProvider.Instance.ExecuteQuery(queryTonKho);
            foreach (DataRow row in tableTonKho.Rows)
            {
                txtTonKho.Text = row[0].ToString();
            }
        }

        private void btnLamMoiSP_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (txtMaSP.Text.Length > 0 && txtSoLuong.Text.Length > 0)
            {
                string maSP = txtMaSP.Text;
                int soluong = -1;
                int.TryParse(txtSoLuong.Text, out soluong);

                // Tìm xem sản phẩm này đã có trong hd chưa?
                DTO_SP_SL_Gia foundSP = lstSP.Find(sp => sp.MaSP == maSP);

                if (foundSP != null)
                {
                    // Sản phẩm đã được tìm thấy
                    DialogResult result = MessageBox.Show("Sản phẩm đã có trong Hoá Đơn. \n\n Thay Mới (Yes) \n\n Thêm Vào (No)", "Thay hay Thêm", MessageBoxButtons.YesNoCancel);
                    // Kiểm tra kết quả
                    if (result == DialogResult.Yes)
                    {
                        // Người dùng chọn Yes
                        dao_ChiTietHD.EditDB_CTHD_ThemMoi(maHD, maSP, soluong);

                        int newTonKho = int.Parse(txtTonKho.Text) - int.Parse(txtSoLuong.Text);
                        DAO_Kho.Instance.Edit_Kho(maSP, newTonKho);

                        Load_TonKho(maSP);
                        MessageBox.Show("Đã Thay Mới Sản Phẩm.");
                    }
                    if (result == DialogResult.No)
                    {
                        // Người dùng chọn No hoặc đóng MessageBox
                        dao_ChiTietHD.EditDB_CTHD_ThemVao(maHD, maSP, soluong);

                        int newTonKho = int.Parse(txtTonKho.Text) - int.Parse(txtSoLuong.ToString());
                        DAO_Kho.Instance.Edit_Kho(maSP, newTonKho);

                        Load_TonKho(maSP);
                        MessageBox.Show("Đã Thêm Số Lượng Sản Phẩm.");
                    }
                }
                else
                {
                    // Sản phẩm không tồn tại trong danh sách, Thêm Mới
                    dao_ChiTietHD.AddDB_TableCTHD(maHD, maSP, soluong);

                }

                Load_ListViewSP(maHD);
            }
            else
            {
                MessageBox.Show("Phải chọn Sản Phẩm hoặc Nhập Số Lượng để Thêm.");
                return;
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maHD != null && lvSanPham.SelectedItems.Count == 1)
            {
                string maSP = lvSanPham.SelectedItems[0].SubItems[0].Text;
                dao_ChiTietHD.Remove_SanPham(maHD, maSP);

                Load_ListViewSP(maHD);
            }
        }

        public void LamMoi()
        {
            cobTenNV.Items.Clear();
            cobTenNV.Text = string.Empty;
            cobTenKH.Items.Clear();
            cobTenKH.Text = string.Empty;
            cobTenSP.Items.Clear();
            cobTenSP.Text = string.Empty;
            txtMaKH.Clear();
            txtMaNV.Clear();
            txtSoLuong.Clear();
            txtMaSP.Clear();
            txtTonKho.Clear();

            Load_ListViewHD();
            LoadComboBox();
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            int tonKho = int.Parse(txtTonKho.Text);

            // Kiểm tra giá trị của TextBox khi người dùng thay đổi
            if (!string.IsNullOrEmpty(txtSoLuong.Text) && lvSanPham.SelectedItems.Count > 0)
            {
                int value = int.Parse(txtSoLuong.Text);

                // Giới hạn giá trị dưới 1001
                if (value > tonKho)
                {
                    MessageBox.Show($"Vui lòng nhập giá trị từ dưới {tonKho}.");
                    txtSoLuong.Text = tonKho.ToString();
                }
            }
            //else
            //{
            //    if (lvSanPham.SelectedItems.Count > 0)
            //    {
            //        MessageBox.Show("Vui lòng nhập số lượng cho sản phẩm.");
            //    }
            //    else
            //        MessageBox.Show("Vui lòng chọn sản phẩm.");
            //}
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự nhập vào có phải là số hoặc phím backspace không
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Nếu không phải số hoặc backspace, không cho phép nhập
            }
        }
    }
}
