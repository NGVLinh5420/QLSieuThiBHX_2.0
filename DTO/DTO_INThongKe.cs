using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DTO
{
    internal class DTO_INThongKe
    {
        string maHD;
        string tenKH;
        string tenNV;
        string ngayLapHD;
        string dsSP;

        public DTO_INThongKe(string maHD, string tenKH, string tenNV, string ngayLapHD, string dsSP)
        {
            this.maHD = maHD;
            this.tenKH = tenKH;
            this.tenNV = tenNV;
            this.ngayLapHD = ngayLapHD;
            this.dsSP = dsSP;
        }

        public string MaHD { get => maHD; set => maHD = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public string NgayLapHD { get => ngayLapHD; set => ngayLapHD = value; }
        public string DsSP { get => dsSP; set => dsSP = value; }

        public DTO_INThongKe(DataRow row)
        {
            maHD = row["MaHD"].ToString();
            tenKH = row["TenKH"].ToString(); ;
            tenNV = row["TenNV"].ToString();
            ngayLapHD = row["NgayLapHD"].ToString();
            dsSP = row["DanhSachSanPham"].ToString(); ;
        }
    }
}
