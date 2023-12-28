using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DTO
{
    internal class DTO_HoaDon
    {
        private string _MaHD;
        private string _MaKH;
        private string _MaNV;
        private string _NgayLapHD;

        public DTO_HoaDon(string maHD, string maKH, string maNV, string ngayLapHD)
        {
            _MaHD = maHD;
            _MaKH = maKH;
            _MaNV = maNV;
            _NgayLapHD = ngayLapHD;
        }

        public string MaHD { get => _MaHD; set => _MaHD = value; }
        public string MaKH { get => _MaKH; set => _MaKH = value; }
        public string MaNV { get => _MaNV; set => _MaNV = value; }
        public string NgayLapHD { get => _NgayLapHD; set => _NgayLapHD = value; }

        //METHOD
        public DTO_HoaDon(DataRow row)
        {
            MaHD = row["MaHD"].ToString();
            MaKH = row["MaKH"].ToString();
            MaNV = row["MaNV"].ToString();
            NgayLapHD = row["NgayLapHD"].ToString();
        }
    }
}
