using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DTO
{
    internal class DTO_SanPham
    {
        private string _MaSP;
        private string _TenSP;
        private int _TonKho;
        private string _DonViTinhSP;
        private int _DonGiaSP;

        public string MaSP { get => _MaSP; set => _MaSP = value; }
        public string TenSP { get => _TenSP; set => _TenSP = value; }
        public int TonKho { get => _TonKho; set => _TonKho = value; }
        public string DonViTinhSP { get => _DonViTinhSP; set => _DonViTinhSP = value; }
        public int DonGiaSP { get => _DonGiaSP; set => _DonGiaSP = value; }

        public DTO_SanPham(string maSP, string tenSP, int tonKho, string donViTinhSP, int donGiaSP)
        {
            _MaSP = maSP;
            _TenSP = tenSP;
            _TonKho = tonKho;
            _DonViTinhSP = donViTinhSP;
            _DonGiaSP = donGiaSP;
        }

        //METHOD
        public DTO_SanPham(DataRow row)
        {
            MaSP = row["MaSP"].ToString();
            TenSP = row["TenSP"].ToString();
            TonKho = int.Parse(row["TonKho"].ToString());
            DonViTinhSP = row["DonViTinh"].ToString();
            DonGiaSP = int.Parse(row["DonGia"].ToString());
        }
    }
}
