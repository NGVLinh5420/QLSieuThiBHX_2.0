using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DTO
{
    internal class DTO_ChiTietHD
    {
        private string _MaHD;
        private string _MaSP;
        private int _SoLuong;

        public DTO_ChiTietHD(string maHD, string maSP, int soLuong)
        {
            _MaHD = maHD;
            _MaSP = maSP;
            _SoLuong = soLuong;
        }

        public string MaHD { get => _MaHD; set => _MaHD = value; }
        public string MaSP { get => _MaSP; set => _MaSP = value; }
        public int SoLuong { get => _SoLuong; set => _SoLuong = value; }

        //METHOD
        public DTO_ChiTietHD(DataRow row)
        {
            MaHD = row["MaHD"].ToString();
            MaSP = row["MaSP"].ToString();
            SoLuong = int.Parse(row["SoLuong"].ToString());
        }
    }
}
