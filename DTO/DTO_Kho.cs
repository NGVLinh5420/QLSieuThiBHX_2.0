using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace QLSieuThiBHX.DTO
{
    internal class DTO_Kho
    {
        private string maSP;
        private string tenSP;
        private int tonKho;
        private string donViTinh;

        public DTO_Kho(string maSP, string tenSP, int tonKho, string donViTinh)
        {
            this.maSP = maSP;
            this.tenSP = tenSP;
            this.tonKho = tonKho;
            this.donViTinh = donViTinh;
        }

        public string MaSP { get => maSP; set => maSP = value; }
        public string TenSP { get => tenSP; set => tenSP = value; }
        public int TonKho { get => tonKho; set => tonKho = value; }
        public string DonViTinh { get => donViTinh; set => donViTinh = value; }

        public DTO_Kho(DataRow row)
        {
            maSP = row["MaSP"].ToString();
            tenSP = row["TenSP"].ToString();
            tonKho = int.Parse(row["TonKho"].ToString());
            donViTinh = row["DonViTinh"].ToString();
        }
    }
}
