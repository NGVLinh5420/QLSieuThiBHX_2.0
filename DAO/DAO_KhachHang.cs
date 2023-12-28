using QLSieuThiBHX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DAO
{
    internal class DAO_KhachHang
    {
        //Cho phép Class khác Ánh Xạ vào lớp này
        internal DAO_KhachHang() { }

        //
        private static DAO_KhachHang _instance;
        public static DAO_KhachHang Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_KhachHang();
                }
                return _instance;
            }
        }

        /*
         * Đọc CSDL, Tạo List Khách Hàng từ Table.KhachHang
         */
        public List<DTO_KhachHang> ReadDB_TableKhachHang()
        {
            List<DTO_KhachHang> listKH = new List<DTO_KhachHang>();

            string query = "EXEC SP_READ_KHACHHANG";
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in table.Rows)
            {
                DTO_KhachHang khachHang = new DTO_KhachHang(row);
                listKH.Add(khachHang);
            }

            return listKH;
        }

        /*
         * Thêm Khách Hàng cho Table.KHACHHANG
         */
        public bool AddDB_TableKhachHang(string ma, string hoten, string diachi, string sdt)
        {
            string query = "SP_ADD_KHACHHANG @MaKH , @HoTenKH , @DiaChi , @DienThoai";
            object[] param = new object[] { ma, hoten, diachi, sdt };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Cập Nhật-Update tt Khách Hàng cho Table.KHACHHANG
         */
        public bool EditDB_TableKhachHang(string ma, string hoten, string diachi, string sdt)
        {
            string query = "SP_UPDATE_KHACHHANG @MaKH , @HoTenKH , @DiaChi , @DienThoai";
            object[] param = new object[] { ma, hoten, diachi, sdt };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Xoá Khách Hàng Khỏi Table.KHACHHANG
         */
        public bool RemoveDB_TableKhachHang(string ma)
        {
            string query = "SP_DELETE_KHACHHANG @MaKH";
            object[] param = new object[] { ma };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Tìm Kiếm Khách Hàng bằng SĐT và Xuất ra DS.
         */
        public List<DTO_KhachHang> Search_SDT_KhachHang(string SDT)
        {
            string query = "EXEC CREATE PROC SP_SEARCH_KHACHHANG";
            object[] param = new object[] { SDT };
            DataTable data = DataProvider.Instance.ExecuteQuery(query, param);

            List<DTO_KhachHang> dsKhachHang = new List<DTO_KhachHang>();

            foreach (DataRow row in data.Rows)
            {
                DTO_KhachHang khachhang = new DTO_KhachHang(row);
                dsKhachHang.Add(khachhang);
            }

            return dsKhachHang;
        }

        /*
         * 
         */
    }
}
