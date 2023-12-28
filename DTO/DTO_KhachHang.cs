using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DTO
{
    internal class DTO_KhachHang
    {
        private string _MaKH;
        private string _HoTenKH;
        private string _DiaChiKH;
        private string _SDTKH;

        public DTO_KhachHang(string maKH, string hoTenKH, string diaChiKH, string sdtKH)
        {
            _MaKH = maKH;
            _HoTenKH = hoTenKH;
            _DiaChiKH = diaChiKH;
            _SDTKH = sdtKH;
        }

        public string MaKH { get => _MaKH; set => _MaKH = value; }
        public string HoTenKH { get => _HoTenKH; set => _HoTenKH = value; }
        public string DiaChiKH { get => _DiaChiKH; set => _DiaChiKH = value; }
        public string SDTKH { get => _SDTKH; set => _SDTKH = value; }

        // METHOD
        public DTO_KhachHang(DataRow row)
        {
            MaKH = row["MaKH"].ToString();
            HoTenKH = row["TenKH"].ToString();
            DiaChiKH = row["DiaChi"].ToString();
            SDTKH = row["DienThoai"].ToString();
        }
    }
}
