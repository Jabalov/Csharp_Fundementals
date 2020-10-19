using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    class Product
    {
        public Product() {}
        public Product(int ProductId_)
        {
            ProductId = ProductId_;
        }
        public decimal? CurrentPrice { get; set; }
        public int ProductId { get; private set; }
        public string ProductDescription { get; set; }
        public String ProductName { get; set; }

        public bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(ProductName) | CurrentPrice == null) 
                isValid = false;

            return isValid;
        }

        public bool Save()
        {
            return true;
        }

        public Product Retrieve(int ProductID)
        {
            return new Product();
        }

        public List<Product> Retrieve()
        {
            return new List<Product>();
        }
    }
}
