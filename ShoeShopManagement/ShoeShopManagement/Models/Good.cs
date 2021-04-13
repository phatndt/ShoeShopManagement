using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class Good
    {
        private int idGood;
        private string name;
        private long price;
        private int quantity;
        private byte[] image;
        private string color;
        private int size;

        public int IdGood { get => idGood; set => idGood = value; }
        public string Name { get => name; set => name = value; }
        public long Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public byte[] Image { get => image; set => image = value; }
        public string Color { get => color; set => color = value; }
        public int Size { get => size; set => size = value; }
        
        public Good()
        {

        }
        public Good(int idGood, string name, long price, int quantity, byte[] image, string color, int size)
        {
            this.idGood = idGood;
            this.name = name;
            this.price = price;
            this.quantity = quantity;
            this.image = image;
            this.color = color;
            this.size = size;
        }
    }
}
