using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class StockInDetail
    {
        private int MaPNH;
        public int mAPNH { get => MaPNH; set => MaPNH = value; }

        private int MaSP;
        public int mASP { get => MaSP; set => MaSP = value; }

        private int SoLuong;
        public int sOLuong { get => SoLuong; set => SoLuong = value; }

        private long DonGia;
        public long donGia { get => DonGia; set => DonGia = value; }
        public StockInDetail() { 

        }
        public StockInDetail( int MaPNH, int MaSP, int SoLuong, long DonGia)
        {
            
            this.MaPNH = MaPNH;
            this.MaSP = MaSP;
            this.SoLuong = SoLuong;
            this.DonGia= DonGia;
        }
        
    }
    
}
