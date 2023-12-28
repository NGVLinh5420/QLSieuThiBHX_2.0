using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DTO
{
    internal class DTO_TKSP_Ngay
    {

        private string maHD;
        private string maSP;
        private string tenSP;
        private string maKH;
        private string tenKH;
        private string maNV;
        private string tenNV;
        private int soLuong;
        private int ThanhTien;

        public DTO_TKSP_Ngay(string maHD, string maSP, string tenSP, string maKH, string tenKH, string maNV, string tenNV, int soLuong, int thanhTien)
        {
            this.maHD = maHD;
            this.maSP = maSP;
            this.tenSP = tenSP;
            this.maKH = maKH;
            this.tenKH = tenKH;
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.soLuong = soLuong;
            ThanhTien = thanhTien;
        }

        public string MaHD { get => maHD; set => maHD = value; }
        public string MaSP { get => maSP; set => maSP = value; }
        public string TenSP { get => tenSP; set => tenSP = value; }
        public string MaKH { get => maKH; set => maKH = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int ThanhTien1 { get => ThanhTien; set => ThanhTien = value; }

        //METHOD
        public DTO_TKSP_Ngay(DataRow row)
        {
            MaHD = row["MaHD"].ToString();
            MaSP = row["MaSP"].ToString();
            TenSP = row["TenSP"].ToString();
            MaKH = row["MaKH"].ToString();
            TenKH = row["TenKH"].ToString();
            MaNV = row["MaNV"].ToString();
            TenNV = row["TenNV"].ToString();
            SoLuong = int.Parse(row["SoLuong"].ToString());
            ThanhTien = int.Parse(row["ThanhTien"].ToString());
        }
    }
}
