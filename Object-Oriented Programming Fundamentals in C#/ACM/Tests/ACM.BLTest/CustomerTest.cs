using System;
using ACM.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACM.BLTest
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void FullNameTest()
        {
            Customer customer = new Customer();

            customer.FirstName = "Muhammed";
            customer.LastName = "Abojabal";

            string RightName = "Abojabal, Muhammed";

            Assert.AreEqual(RightName, customer.FullName);
        }

        [TestMethod]
        public void FullNameFirstNameEmpty()
        {
            Customer customer = new Customer()
            {
                LastName = "Abujabal"
            };

            Assert.AreEqual(customer.FullName, "Abujabal");
        }

        [TestMethod]
        public void CounterTest()
        {
            var c1 = new Customer();
            var c2 = new Customer();
            Assert.AreEqual(Customer.InsntanceCount, 2);
        }

        [TestMethod]
        public void ValidateValidData()
        {
            var customer = new Customer()
            {
                LastName = "Abojabal",
                EmailAddress = "Aboajabl@gmail.com"
            };

            Assert.AreEqual(true, customer.Validate());
        }
    }
}
