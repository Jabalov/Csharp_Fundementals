using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class Customer
    {
        public Customer() { InsntanceCount += 1; }
        public Customer(int _CustomerId)
        {
            CustomerId = _CustomerId;
        }
        public static int InsntanceCount{ get; private set;}
        public int CustomerId { get; private set; }
        public string EmailAddress { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                string fullName = LastName;
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    if (!string.IsNullOrWhiteSpace(LastName))
                        fullName += ", ";

                    fullName += FirstName;
                }
                return fullName;
            }
        }

        public bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrEmpty(LastName))
                isValid = false;
            if (string.IsNullOrEmpty(EmailAddress))
                isValid = false;

            return isValid;
        }

        public bool Save()
        {
            return true;
        }

        public Customer Retrieve(int CustomerId)
        {
            return new Customer();
        }

        public List<Customer> Retrieve()
        {
            return new List<Customer>();
        }
    }
}
