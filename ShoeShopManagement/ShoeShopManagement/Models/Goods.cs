using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class Goods
    {
        private int idGood;
        private string name;
        private int idLSP;
        private int idDVT;
        private long price;
        private byte[] image;
        public int IdGood { get => idGood; set => idGood = value; }
        public string Name { get => name; set => name = value; }
        public int IdLSP { get => idLSP; set => idLSP = value; }
        public int IdDVT { get => idDVT; set => idDVT = value; }
        public long Price { get => price; set => price = value; }
        public byte[] Image { get => image; set => image = value; }

        public Goods()
        {

        }
        public Goods(int idGood, string name, int idLSP, int idDVT, long price, byte[] image)
        {
            this.idGood = idGood;
            this.name = name;
            this.idLSP = idLSP;
            this.idDVT = idDVT;
            this.price = price;
            this.image = image;
        }
    }
}
