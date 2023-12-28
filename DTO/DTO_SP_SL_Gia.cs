using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DTO
{
    internal class DTO_SP_SL_Gia
    {
        private string _MaSP;
        private string _TenSP;
        private int _TongSoLuong;
        private long _ThanhTien;

        public string MaSP { get => _MaSP; set => _MaSP = value; }
        public string TenSP { get => _TenSP; set => _TenSP = value; }
        public int TongSoLuong { get => _TongSoLuong; set => _TongSoLuong = value; }
        public long ThanhTien { get => _ThanhTien; set => _ThanhTien = value; }

        public DTO_SP_SL_Gia(string maSP, string tenSP, int tongSoLuong, long thanhTien)
        {
            _MaSP = maSP;
            _TenSP = tenSP;
            _TongSoLuong = tongSoLuong;
            _ThanhTien = thanhTien;
        }



        //METHOD
        public DTO_SP_SL_Gia(DataRow row)
        {
            MaSP = row["MaSP"].ToString();
            TenSP = row["TenSP"].ToString();
            TongSoLuong = int.Parse(row["TongSoLuong"].ToString());
            ThanhTien = long.Parse(row["ThanhTien"].ToString());
        }
    }
}
