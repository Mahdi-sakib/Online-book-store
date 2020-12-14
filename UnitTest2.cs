using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.UnitTests
{
    [TestClass]
    public class CartsTests
    {
        [TestMethod]
        public void can_add_new_lines()
        {
            //add the product for the first time
            Cart target = new Cart();
            target.AddItem(new Product{ ProductID=1, Name="p1", Category="c1", Price=10}, 10);

            CartLine[] actual = ((List<CartLine>)target.Lines).ToArray();
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(1, actual[0].Product.ProductID);
        }

        [TestMethod]
        public void can_add_quantity_to_the_existing_lines()
        {
            Cart target = new Cart();
            target.AddItem(new Product{ ProductID=1, Name="p1", Category="c1", Price=10}, 10);
            target.AddItem(new Product { ProductID = 1, Name = "p1", Category = "c1", Price = 10 }, 10);

            CartLine[] actual = ((List<CartLine>)target.Lines).ToArray();
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(20, actual[0].Quantity);
        }

        [TestMethod]
        public void remove_line()
        {
            Cart target = new Cart();
            target.AddItem(new Product{ ProductID=1, Name="p1", Category="c1", Price=10}, 10);
            target.AddItem(new Product{ ProductID=2, Name="p2", Category="c1", Price=10}, 10);
            target.AddItem(new Product{ ProductID=3, Name="p3", Category="c1", Price=10}, 10);
            target.RemoveLine(new Product { ProductID = 3, Name = "p3", Category = "c1", Price = 10 });
            CartLine[] actual = ((List<CartLine>)target.Lines).ToArray();
            Assert.AreEqual(2, actual.Length);
        }

        [TestMethod]
        public void calculate_cart_total()
        {
            Cart target = new Cart();
            target.AddItem(new Product{ ProductID=1, Name="p1", Category="c1", Price=10}, 10);
            target.AddItem(new Product{ ProductID=2, Name="p2", Category="c1", Price=20}, 20);
            target.AddItem(new Product{ ProductID=3, Name="p3", Category="c1", Price=30}, 30);
            decimal actual = target.ComputeTotalValue();
            Assert.AreEqual(1400M, actual);
        }

        [TestMethod]
        public void can_clear_contents()
        {
            Cart target = new Cart();
            target.AddItem(new Product{ ProductID=1, Name="p1", Category="c1", Price=10}, 10);
            target.AddItem(new Product{ ProductID=2, Name="p2", Category="c1", Price=10}, 10);
            target.AddItem(new Product{ ProductID=3, Name="p3", Category="c1", Price=10}, 10);
            target.Clear();
           
            Assert.AreEqual(0, target.Lines.Count());
        }
    }
}
