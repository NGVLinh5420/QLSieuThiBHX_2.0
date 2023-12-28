using QLSieuThiBHX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DAO
{
    internal class DAO_ChiTietHD
    {
        public DAO_ChiTietHD() { }

        private static DAO_ChiTietHD _instance;
        public static DAO_ChiTietHD Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_ChiTietHD();
                }
                return _instance;
            }
        }

        /*
         * Danh sách chi tiết HĐ: Mã HĐ, Ngày lập, Giá
         */
        //public List<DTO_HD_Gia> ReadDB_Select_HD()
        //{
        //    List<DTO_HD_Gia> listHD_Gia = new List<DTO_HD_Gia>();

        //    string query = "EXEC SP_READ_CHITIETHD";
        //    DataTable table = DataProvider.Instance.ExecuteQuery(query);

        //    foreach (DataRow row in table.Rows)
        //    {
        //        DTO_HD_Gia hd_gia = new DTO_HD_Gia(row);
        //        listHD_Gia.Add(hd_gia);
        //    }
        //    return listHD_Gia;
        //}


        public List<DTO_HoaDon> Read_HoaDon()
        {
            List<DTO_HoaDon> lstHD = new List<DTO_HoaDon>();

            string query = "EXEC SP_READ_HOADON";
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in table.Rows)
            {
                DTO_HoaDon hd = new DTO_HoaDon(row);
                lstHD.Add(hd);
            }
            return lstHD;
        }

        //--Xuất ds sản phẩm có trong hoá đơn
        public List<DTO_SP_SL_Gia> ReadDB_Select_SP(String maHD)
        {
            List<DTO_SP_SL_Gia> listSP_SL = new List<DTO_SP_SL_Gia>();

            string query = "EXEC SP_SELECT_CTHD_SANPHAM_of_HD @MaHD";
            object[] param = new object[] { maHD };
            DataTable table = DataProvider.Instance.ExecuteQuery(query, param);

            foreach (DataRow row in table.Rows)
            {
                DTO_SP_SL_Gia sp_sl = new DTO_SP_SL_Gia(row);
                listSP_SL.Add(sp_sl);
            }
            return listSP_SL;
        }

        /*
         * Thêm Chi Tiet Hoá Đơn cho Table.HoaDon
         */
        public bool AddDB_TableCTHD(string maHD, string maSP, int soLuong)
        {
            string query = "SP_ADD_CHITIETHD @MaHD , @MaSP , @SoLuong";
            object[] param = new object[] { maHD, maSP, soLuong };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Cập Nhật-Update tt Hoá Đơn cho Table.HoaDon
         */
        public bool EditDB_CTHD_ThemMoi(string maHD, string maSP, int soLuong)
        {
            string query = "SP_UPDATE_CHITIETHD_THEMMOI @MaHD , @MaSP , @SoLuong";
            object[] param = new object[] { maHD, maSP, soLuong };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        public bool EditDB_CTHD_ThemVao(string maHD, string maSP, int soLuong)
        {
            string query = "SP_UPDATE_CHITIETHD_THEMVAO @MaHD , @MaSP , @SoLuong";
            object[] param = new object[] { maHD, maSP, soLuong };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        //Xoa
        public bool Remove_SanPham(string maHD, string maSP)
        {
            string query = "SP_DELETE_CHITIETHD @MaHD , @MaSP";
            object[] param = new object[] { maHD, maSP };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }
    }
}
