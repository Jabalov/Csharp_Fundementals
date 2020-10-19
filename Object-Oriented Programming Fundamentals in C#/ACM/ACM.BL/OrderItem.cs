using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    class OrderItem
    {
        public OrderItem() { }
        public OrderItem(int Id_)
        {
            OrderItemId = Id_;
        }
        public int OrderItemId { get; private set; }
        public int ProductId { get; set; }
        public decimal? PurchasePrice { get; set; }
        public int Quantity { get; set; }


        public bool Validate()
        {
            var isValid = true;

            if (Quantity <= 0  | ProductId <= 0 | PurchasePrice == null)
                isValid = false;

            return isValid;
        }

        public bool Save()
        {
            return true;
        }

        public OrderItem Retrieve(int OrderItemId)
        {
            return new OrderItem();
        }

        public List<OrderItem> Retrieve()
        {
            return new List<OrderItem>();
        }
    }

}
