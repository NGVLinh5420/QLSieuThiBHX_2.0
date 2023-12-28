using QLSieuThiBHX.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DAO
{
    internal class DAO_Kho
    {
        public DAO_Kho() { }

        private static DAO_Kho instance;

        internal static DAO_Kho Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAO_Kho();
                return instance;
            }

        }

        /*
         * Danh sách chi tiết SP: Mã SP, Tên, Tồn Kho, Đơn vị tính.
         */
        public List<DTO_Kho> Read_Kho()
        {
            List<DTO_Kho> listKho = new List<DTO_Kho>();

            string query = "EXEC SP_READ_KHO";
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in table.Rows)
            {
                DTO_Kho kho = new DTO_Kho(row);
                listKho.Add(kho);
            }
            return listKho;
        }

        /*
         * Cập Nhật
         */
        public bool Edit_Kho(string ma, int tonKho)
        {
            string query = "SP_EDIT_KHO @MaSP , @TonKho";
            object[] param = new object[] { ma, tonKho};
            int result = DataProvider.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

    }
}
