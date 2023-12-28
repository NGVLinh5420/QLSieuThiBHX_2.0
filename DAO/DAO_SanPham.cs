using QLSieuThiBHX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DAO
{
    internal class DAO_SanPham
    {
        internal DAO_SanPham() { }

        //
        private static DAO_SanPham _instance;
        public static DAO_SanPham Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DAO_SanPham();
                }
                return _instance;
            }
        }

        /*
         * Đọc CSDL, Tạo List Sản Phẩm từ Table.SanPham
         */
        public List<DTO_SanPham> Read_SanPham()
        {
            List<DTO_SanPham> listSP = new List<DTO_SanPham>();

            string query = "EXEC SP_READ_SANPHAM";
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in table.Rows)
            {
                DTO_SanPham sp = new DTO_SanPham(row);
                listSP.Add(sp);
            }
            return listSP;
        }

        /*
         * Thêm Sản Phẩm cho Table.SanPham
         */
        public bool AddDB_TableSanPham(string ma, string ten, int tonKho, string dvt, int dg)
        {
            string query = "SP_ADD_SANPHAM @MaSP , @TenSP , @TonKho , @DonViTinh , @DonGia";
            object[] param = new object[] { ma, ten, tonKho, dvt, dg };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Cập Nhật-Update tt Sản Phẩm cho Table.SanPham
         */
        public bool EditDB_TableSanPham(string ma, string ten, int tonKho, string dvt, int dg)
        {
            string query = "SP_UPDATE_SANPHAM @MaSP , @TenSP , @TonKho , @DonViTinh , @DonGia";
            object[] param = new object[] { ma, ten, tonKho, dvt, dg };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        /*
         * Xoá Sản Phẩm Khỏi Table.SanPham
         */
        public bool RemoveDB_TableSanPham(string ma)
        {
            string query = "SP_DELETE_SANPHAM @MaSP";
            object[] param = new object[] { ma };
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }
    }
}
