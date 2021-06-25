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
        private string color;
        private string type;
        private string dvt;
        private int size;

        public int IdGood { get => idGood; set => idGood = value; }
        public string Name { get => name; set => name = value; }
        public long Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public byte[] Image { get => image; set => image = value; }
        public string Color { get => color; set => color = value; }
        public string Type { get => type; set => type = value; }
        public string DVT { get => dvt; set =>dvt = value; }
        public int Size { get => size; set => size = value; }
        
        public Goods()
        {

        }
        public Goods(int idGood, string name,string type, int quantity, long price, string dtv, string color, int size ,byte[] image)
        {
            this.idGood = idGood;
            this.name = name;
            this.type = type;
            this.quantity = quantity;
            this.size = size;
            this.color = color;
            this.price = price;
            this.dvt = dtv;
            this.image = image;

        }
    }
}
