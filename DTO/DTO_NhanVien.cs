using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSieuThiBHX.DTO
{
    internal class DTO_NhanVien
    {
        private string _MaNV;
        private string _TenNV;
        private string _GioiTinh;
        private string _NgaySinh;
        private string _DiaChiNV;
        private string _SDT;

        public DTO_NhanVien(string maNV, string tenNV, string gioiTinhNV, string ngSNV, string diaChiNV, string sDTNV)
        {
            _MaNV = maNV;
            _TenNV = tenNV;
            _GioiTinh = gioiTinhNV;
            _NgaySinh = ngSNV;
            _DiaChiNV = diaChiNV;
            _SDT = sDTNV;
        }

        public string MaNV { get => _MaNV; set => _MaNV = value; }
        public string TenNV { get => _TenNV; set => _TenNV = value; }
        public string GioiTinhNV { get => _GioiTinh; set => _GioiTinh = value; }
        public string NgSNV { get => _NgaySinh; set => _NgaySinh = value; }
        public string DiaChiNV { get => _DiaChiNV; set => _DiaChiNV = value; }
        public string SDTNV { get => _SDT; set => _SDT = value; }

        //METHOD
        public DTO_NhanVien(DataRow row)
        {
            MaNV = row["MaNV"].ToString();
            TenNV = row["TenNV"].ToString();
            GioiTinhNV = row["GioiTinh"].ToString();
            NgSNV = row["NgaySinh"].ToString();
            DiaChiNV = row["DiaChi"].ToString();
            SDTNV = row["DienThoai"].ToString();
        }
    }
}
