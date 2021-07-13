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
        private int idColor;
        private int idType;
        private int idDvt;
        private int size;
        private int isDeleted;
        private int quantity;

        public int IdGood { get => idGood; set => idGood = value; }
        public string Name { get => name; set => name = value; }
        public int IdLSP { get => idLSP; set => idLSP = value; }
        public int IdDVT { get => idDVT; set => idDVT = value; }
        public long Price { get => price; set => price = value; }
        public byte[] Image { get => image; set => image = value; }

        public int IdColor { get => idColor; set => idColor = value; }
        public int IdType { get => idType; set => idType = value; }
        public int IdDvt { get => idDvt; set =>idDvt = value; }
        public int IdSize { get => size; set => size = value; }
        
        public int IsDeleted { get => isDeleted; set => isDeleted = value; }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        public Goods()
        {

        }
        public Goods(int idGood, string name, int idType, int quantity, int size, int idColor, long price, int idDtv, int isdeleted, byte[] image)
        {
            this.idGood = idGood;
            this.name = name;
            this.idType = idType;
            this.Quantity = quantity;
            this.size = size;
            this.idColor = idColor;
            this.price = price;
            this.idDvt = idDtv;
            this.image = image;
            this.isDeleted = isdeleted;
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
