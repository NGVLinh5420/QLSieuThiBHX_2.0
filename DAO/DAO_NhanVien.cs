using QLSieuThiBHX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DAO
{
    internal class DAO_NhanVien
    {
        //Cho phép Class khác Ánh Xạ vào lớp này
        internal DAO_NhanVien() { }

        //Số Lượng NV
        //int soLuongNV = 0;

        //
        private static DAO_NhanVien _instance;
        public static DAO_NhanVien Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_NhanVien();
                }
                return _instance;
            }
        }

        /*
         * Đọc CSDL, Tạo List Nhân Viên từ Table.NHANVIEN
         */
        public List<DTO_NhanVien> ReadDB_TableNhanVien()
        {
            List<DTO_NhanVien> listNV = new List<DTO_NhanVien>();

            string query = "EXEC SP_READ_NHANVIEN";
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in table.Rows)
            {
                DTO_NhanVien nv = new DTO_NhanVien(row);
                listNV.Add(nv);
            }
            return listNV;
        }

        /*
         * Thêm Nhân Viên cho Table.NHANVIEN
         */
        public bool AddDB_TableNhanVien(string ma, string ten, string gt, SqlDateTime ngs, string diachi, string sdt)
        {
            string query = "SP_ADD_NHANVIEN @MaNV , @TenNV , @GioiTinh , @NgaySinh , @DiaChi , @DienThoai";
            object[] param = new object[] { ma, ten, gt, ngs, diachi, sdt };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Cập Nhật-Update tt Nhân Viên cho Table.NHANVIEN
         */
        public bool EditDB_TableNhanVien(string ma, string ten, string gt, SqlDateTime ngs, string diachi, string sdt)
        {
            string query = "SP_UPDATE_NHANVIEN @MaNV , @TenNV , @GioiTinh , @NgaySinh , @DiaChi , @DienThoai";
            object[] param = new object[] { ma, ten, gt, ngs, diachi, sdt };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Xoá Nhân Viên Khỏi Table.NHANVIEN
         */
        public bool RemoveDB_TableNhanVien(string ma)
        {
            string query = "SP_DELETE_NHANVIEN @MaNV";
            object[] param = new object[] { ma };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Tìm Kiếm Nhân Viên bằng SĐT và Xuất ra DS.
         */
        //public List<DTO_NhanVien> Search_SDT_KhachHang(string SDT)
        //{
        //    string query = "EXEC CREATE PROC SP_SEARCH_KHACHHANG";
        //    object[] param = new object[] { SDT };
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query, param);

        //    List<DTO_NhanVien> dsKhachHang = new List<DTO_NhanVien>();

        //    foreach (DataRow row in data.Rows)
        //    {
        //        DTO_NhanVien khachhang = new DTO_NhanVien(row);
        //        dsKhachHang.Add(khachhang);
        //    }

        //    return dsKhachHang;
        //}

        //Số Lượng Nhân Viên
        //public int SoLuongNV()
        //{
        //    return soLuongNV;
        //}
    }
}
