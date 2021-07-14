using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class ServicesBillDetail
    {
        private int MaCTPDV;
        private int MaPDV;
        private int MaDV;
        private int SoLuong;
        private long DonGiaDuocTinh;
        private int TinhTrang;

        public ServicesBillDetail(int maCTPDV, int maPDV, int maDV, int soLuong, long donGiaDuocTinh, int tinhTrang)
        {
            MaCTPDV1 = maCTPDV;
            MaPDV1 = maPDV;
            MaDV1 = maDV;
            SoLuong1 = soLuong;
            DonGiaDuocTinh1 = donGiaDuocTinh;
            TinhTrang1 = tinhTrang;
        }

        public int MaCTPDV1 { get => MaCTPDV; set => MaCTPDV = value; }
        public int MaPDV1 { get => MaPDV; set => MaPDV = value; }
        public int MaDV1 { get => MaDV; set => MaDV = value; }
        public int SoLuong1 { get => SoLuong; set => SoLuong = value; }
        public long DonGiaDuocTinh1 { get => DonGiaDuocTinh; set => DonGiaDuocTinh = value; }
        public int TinhTrang1 { get => TinhTrang; set => TinhTrang = value; }
    }
}
