using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DTO
{
    internal class DTO_HD_Gia
    {
        private string _MaHD;
        private string _NgayLapHD;
        private int _TongGiaTien;

        public DTO_HD_Gia(string maHD, string ngayLapHD, int tongGia)
        {
            _MaHD = maHD;
            _NgayLapHD = ngayLapHD;
            _TongGiaTien = tongGia;
        }

        public string MaHD { get => _MaHD; set => _MaHD = value; }
        public string NgayLapHD { get => _NgayLapHD; set => _NgayLapHD = value; }
        public int TongGiaTien { get => _TongGiaTien; set => _TongGiaTien = value; }

        //METHOD
        public DTO_HD_Gia(DataRow row)
        {
            MaHD = row["MaHD"].ToString();
            NgayLapHD = row["NgayLapHD"].ToString();
            TongGiaTien = int.Parse(row["TongGiaTien"].ToString());
        }
    }
}
