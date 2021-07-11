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
        private long price;
        private int quantity;
        private byte[] image;
        private int idColor;
        private int idType;
        private int idDvt;
        private int size;
        private int isDeleted;

        public int IdGood { get => idGood; set => idGood = value; }
        public string Name { get => name; set => name = value; }
        public long Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public byte[] Image { get => image; set => image = value; }
        public int IdColor { get => idColor; set => idColor = value; }
        public int IdType { get => idType; set => idType = value; }
        public int IdDVT { get => idDvt; set =>idDvt = value; }
        public int IdSize { get => size; set => size = value; }
        
        public int IsDeleted { get => isDeleted; set => isDeleted = value; }

        public Goods()
        {

        }
        public Goods(int idGood, string name,int idType, int quantity, int size, int idColor, long price, int idDtv, int isdeleted,byte[] image)
        {
            this.idGood = idGood;
            this.name = name;
            this.idType = idType;
            this.quantity = quantity;
            this.size = size;
            this.idColor = idColor;
            this.price = price;
            this.idDvt = idDtv;
            this.image = image;
            this.isDeleted = isdeleted;
        }
    }
}
