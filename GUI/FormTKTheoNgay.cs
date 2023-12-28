using CrystalDecisions.Windows.Forms;
using QLSieuThiBHX.DAO;
using QLSieuThiBHX.DTO;
using QLSieuThiBHX.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLSieuThiBHX.GUI
{
    public partial class FormTKTheoNgay : Form
    {
        public FormTKTheoNgay()
        {
            InitializeComponent();
        }

        // 0.CSDL
        List<DTO_HoaDon> lstHD = new List<DTO_HoaDon>();
        List<DTO_SP_SL_Gia> lstSP = new List<DTO_SP_SL_Gia>();
        string maHD;

        private void DisplayData(DateTime selectedDate)
        {
            // Xóa tất cả các mục trong ListView
            lvHD.Items.Clear();
            lvSP.Items.Clear();
            lstHD.Clear();
            lstHD = DAO_HoaDon.Instance.ReadDB_TableHoaDon();


            // Lọc dữ liệu theo ngày tháng được chọn
            DateTime ngay = DateTime.Now;
            var filteredData = lstHD.FindAll(item => (ngay = DateTime.Parse(item.NgayLapHD)) == selectedDate.Date);

            // Thêm dữ liệu lọc vào ListView
            foreach (var dataItem in filteredData)
            {
                string ng = DateTime.Parse(dataItem.NgayLapHD).ToShortDateString();

                var listViewItem = new ListViewItem(new[] { dataItem.MaHD, dataItem.MaKH, dataItem.MaNV, ng });
                lvHD.Items.Add(listViewItem);
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            // Khi ngày tháng được chọn thay đổi, hiển thị dữ liệu mới
            DisplayData(dtpNgay.Value);
        }

        private void FormTKTheoNgay_Load(object sender, EventArgs e)
        {

        }

        private void lvHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tongTien = 0;

            if (lvHD.SelectedItems.Count == 1)
            {
                maHD = lvHD.SelectedItems[0].SubItems[0].Text;

                lvSP.Items.Clear();
                lstSP = DAO_ChiTietHD.Instance.ReadDB_Select_SP(maHD);

                foreach (var i in lstSP)
                {
                    ListViewItem itemSP = new ListViewItem(i.MaSP.ToString());
                    lvSP.Items.Add(itemSP);

                    ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(itemSP, i.TenSP.ToString());
                    itemSP.SubItems.Add(subItem);

                    subItem = new ListViewItem.ListViewSubItem(itemSP, i.TongSoLuong.ToString());
                    itemSP.SubItems.Add(subItem);

                    subItem = new ListViewItem.ListViewSubItem(itemSP, i.ThanhTien.ToString());
                    itemSP.SubItems.Add(subItem);

                    tongTien += int.Parse(i.ThanhTien.ToString());
                }

                txtTongTien.Text = tongTien.ToString();
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            List<DTO_TKSP_Ngay> lstTKSP_Ngay = new List<DTO_TKSP_Ngay>();
            DataTable table = DataProvider.Instance.ExecuteQuery("SP_TimKiemSP_Ngay @Ngay",
                new Object[] { dtpNgay.Value.ToShortDateString() });

            foreach (DataRow i in table.Rows)
            {
                DTO_TKSP_Ngay sp = new DTO_TKSP_Ngay(i);
                lstTKSP_Ngay.Add(sp);
            }
            DataProvider.Dto_TKSP_Ngays = lstTKSP_Ngay;

            //FormIn formTKSP = new FormIn(dtpNgay.Value);

            //Test2
            







            //Test1
            //foreach (DataRow row in table.Rows)
            //{
            //    foreach (DataColumn col in table.Columns)
            //    {
            //        Console.Write(row[col] + "\t");
            //    }
            //    Console.WriteLine();
            //}

            //ReportTKSP reportTKSP = new ReportTKSP();
            //reportTKSP.SetParameterValue(0,dtpNgay.Value);

            //FormTKSP formTKSP = new FormTKSP(dtpNgay.Value);
            //formTKSP.Para(dtpNgay.Value);
            //formTKSP.crystalReportViewer1.ReportSource = reportTKSP;
            //formTKSP.ShowDialog();
        }
    }
}
