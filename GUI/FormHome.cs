using QLSieuThiBHX.DAO;
using QLSieuThiBHX.DTO;
using QLSieuThiBHX.GUI;
using QLSieuThiBHX.Report;

//using QLSieuThiBHX.CrystalReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSieuThiBHX
{
    public partial class FormHome : Form
    {
        public FormHome()
        {
            InitializeComponent();
        }

        //Đầu Số ĐT
        public static List<string> dausoDT = new List<string> { "01", "02", "03", "04", "05" };

        // Event
        Form formOpenning; //Đối tượng lưu một-hoặc-nhiều form đang mở ở hiện tại
        private void msiNhanVien_Click(object sender, EventArgs e)
        {
            // GUI
            FormNhanVien formNhanVien = new FormNhanVien();

            //Bắt lỗi
            if (Application.OpenForms.Count > 1) //Có form khác đang mở
            {
                formOpenning = Application.OpenForms[1];
                if (formOpenning.GetType() != formNhanVien.GetType())
                {
                    string msg = "Có Cửa Sổ Khác Đang Mở. \nĐóng Nó Lại Và Mở Cửa Sổ Mới?";
                    DialogResult result = MessageBox.Show(msg, "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        formOpenning.Close();
                    }
                }
                else return;
            }

            formNhanVien.MdiParent = this;
            formNhanVien.Show();
        }
        private void msiKhachHang_Click(object sender, EventArgs e)
        {
            //GUI
            FormKhachHang formKhachHang = new FormKhachHang();

            //Bắt lỗi
            if (Application.OpenForms.Count > 1)
            {
                formOpenning = Application.OpenForms[1];
                if (formOpenning.GetType() != formKhachHang.GetType())
                {
                    string msg = "Có Cửa Sổ Khác Đang Mở. \nĐóng Nó Lại Và Mở Cửa Sổ Mới?";
                    DialogResult result = MessageBox.Show(msg, "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        formOpenning.Close();
                    }
                }
                else return;
            }

            formKhachHang.MdiParent = this;
            formKhachHang.Show();
        }
        private void msSPKho_Click(object sender, EventArgs e)
        {
            //GUI
            FormSanPham formSanPham = new FormSanPham();

            //Bắt lỗi
            if (Application.OpenForms.Count > 1)
            {
                formOpenning = Application.OpenForms[1];
                if (formOpenning.GetType() != formSanPham.GetType())
                {
                    string msg = "Có Cửa Sổ Khác Đang Mở. \nĐóng Nó Lại Và Mở Cửa Sổ Mới?";
                    DialogResult result = MessageBox.Show(msg, "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        formOpenning.Close();
                    }
                }
                else return;
            }

            formSanPham.MdiParent = this;
            formSanPham.Show();
        }
        private void msiHoaDon_Click(object sender, EventArgs e)
        {
            //GUI
            FormHoaDon FormHoaDon = new FormHoaDon();

            //Bắt lỗi
            if (Application.OpenForms.Count > 1)
            {
                formOpenning = Application.OpenForms[1];
                if (formOpenning.GetType() != FormHoaDon.GetType())
                {
                    string msg = "Có Cửa Sổ Khác Đang Mở. \nĐóng Nó Lại Và Mở Cửa Sổ Mới?";
                    DialogResult result = MessageBox.Show(msg, "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        formOpenning.Close();
                    }
                }
                else return;
            }

            FormHoaDon.MdiParent = this;
            FormHoaDon.Show();
        }
        private void msiChiTietHD_Click(object sender, EventArgs e)
        {
            FormChiTietHD formCTHD = new FormChiTietHD();

            //Bắt lỗi
            if (Application.OpenForms.Count > 1)
            {
                formOpenning = Application.OpenForms[1];
                if (formOpenning.GetType() != formCTHD.GetType())
                {
                    string msg = "Có Cửa Sổ Khác Đang Mở. \nĐóng Nó Lại Và Mở Cửa Sổ Mới?";
                    DialogResult result = MessageBox.Show(msg, "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        formOpenning.Close();
                    }
                    else return;
                }
            }

            formCTHD.MdiParent = this;
            formCTHD.Show();
        }
        private void msINChiTietHD_Click(object sender, EventArgs e)
        {
            //string query = "EXEC SP_READ_SELECT_XUATTK; EXEC SP_READ_SELECT_DEMKH_MUA; " +
            //    "EXEC SP_READ_SELECT_DEMSP_MUA; EXEC SP_TK_TongDoanhThu_Nam";
            //DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);

            ReportTK rpt = new ReportTK();
            //rpt.SetDataSource(dataTable);

            //foreach (DataRow r in dataTable.Rows)
            //{
            //    Console.WriteLine(r);
            //}

            FormIn formIN = new FormIn();
            formIN.inNV.ReportSource = rpt;
            formIN.ShowDialog();
        }

        //Exit APP
        private void FormHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.Count > 1)
            {
                string msg = "Có cửa sổ chưa đóng!! \nNếu tắt chương trình ngay, bạn sẽ có thể sẽ mất dữ liệu (!) \nBạn có chắc chắn muốn thoát chương trình?";
                DialogResult result = MessageBox.Show(msg, "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    e.Cancel = true; // Event huỷ đóng ứng dụng = true
                else return;
            }
            else
            {
                string msg = "Bạn có chắc chắn muốn thoát chương trình?";
                DialogResult result = MessageBox.Show(msg, "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    e.Cancel = true; // Event huỷ đóng ứng dụng = true
            }
        }

        private void msTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void msiTheoNgay_Click(object sender, EventArgs e)
        {
            FormTKTheoNgay formTK = new FormTKTheoNgay();

            //Bắt lỗi
            if (Application.OpenForms.Count > 1)
            {
                formOpenning = Application.OpenForms[1];
                if (formOpenning.GetType() != formTK.GetType())
                {
                    string msg = "Có Cửa Sổ Khác Đang Mở. \nĐóng Nó Lại Và Mở Cửa Sổ Mới?";
                    DialogResult result = MessageBox.Show(msg, "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        formOpenning.Close();
                    }
                    else return;
                }
            }

            formTK.MdiParent = this;
            formTK.Show();
        }
    }
}
