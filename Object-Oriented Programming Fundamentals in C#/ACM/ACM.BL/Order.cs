using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    class Order
    {
        public Order() { }
        public Order(int Id_)
        {
            OrderId = Id_;
        }
        public DateTimeOffset? OrderDate { get; set; }
        public int OrderId { get; private set; }


        public bool Validate()
        {
            var isValid = true;

            if (OrderDate == null)
                isValid = false;

            return isValid;
        }

        public bool Save()
        {
            return true;
        }

        public Order Retrieve(int ID)
        {
            return new Order();
        }

        public List<Order> Retrieve()
        {
            return new List<Order>();
        }
    }
}
