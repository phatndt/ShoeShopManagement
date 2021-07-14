using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class GoodDetail
    {
        private int idGoodDetail;
        private int idGood;
        private int idSize;
        private int idColor;
        private int quantity;

        public int IdGoodDetail
        {
            get
            {
                return idGoodDetail;
            }

            set
            {
                idGoodDetail = value;
            }
        }

        public int IdGood
        {
            get
            {
                return idGood;
            }

            set
            {
                idGood = value;
            }
        }

        public int IdSize
        {
            get
            {
                return idSize;
            }

            set
            {
                idSize = value;
            }
        }

        public int IdColor
        {
            get
            {
                return idColor;
            }

            set
            {
                idColor = value;
            }
        }

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
        public GoodDetail()
        {

        }
        public GoodDetail(int a, int b, int c, int d, int e)
        {
            this.idGoodDetail = a;
            this.idGood = b;
            this.idSize = c;
            this.idColor = d;
            this.quantity = e;
        }
    }
}
