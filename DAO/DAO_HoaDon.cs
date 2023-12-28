using QLSieuThiBHX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DAO
{
    internal class DAO_HoaDon
    {
        public DAO_HoaDon()
        {
        }

        private static DAO_HoaDon _instance;

        internal static DAO_HoaDon Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_HoaDon();
                }
                return _instance;
            }
        }

        /*
         * Đọc CSDL, Tạo List Hoá Đơn từ Table.HoaDon
         */
        public List<DTO_HoaDon> ReadDB_TableHoaDon()
        {
            List<DTO_HoaDon> listHD = new List<DTO_HoaDon>();

            string query = "EXEC SP_READ_HOADON";
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in table.Rows)
            {
                DTO_HoaDon hd = new DTO_HoaDon(row);
                listHD.Add(hd);
            }
            return listHD;
        }

        /*
         * Thêm Hoá Đơn cho Table.HoaDon
         */
        public bool AddDB_TableHoaDon(string mahd, string makh, string manv, SqlDateTime ngay)
        {
            string query = "SP_ADD_HOADON @MaHD , @MaKH , @MaNV , @NgayLapHD";
            object[] param = new object[] { mahd, makh, manv, ngay };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Cập Nhật-Update tt Hoá Đơn cho Table.HoaDon
         */
        public bool EditDB_TableHoaDon(string maHD, string maKH, string maNV, SqlDateTime ngay)
        {
            string query = "SP_UPDATE_HOADON @MaHD , @MaKH , @MaNV , @NgayLapHD";
            object[] param = new object[] { maHD, maKH, maNV, ngay};
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Xoá Hoá Đơn Khỏi Table.HoaDon
         */
        public bool RemoveDB_TableHoaDon(string ma)
        {
            string query = "SP_DELETE_HOADON @MaHD";
            object[] param = new object[] { ma };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        //

    }
}
