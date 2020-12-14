using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using BookStore.WebUI.Controllers;

namespace BookStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void index_contains_all_products()
        {
            Mock<IProductRepository> _m = new Mock<IProductRepository>();
            _m.Setup(m => m.Products).Returns(

                                       new List<Product>
                                                   { new Product(){ProductID=1,Name="Product1"},
                                                     new Product(){ProductID=1,Name="Product2"},
                                                     new Product(){ProductID=1,Name="Product3"},
                                                   }.AsQueryable()
           );
            AdminController target = new AdminController(_m.Object);

            IQueryable<Product> p = (IQueryable<Product>)target.Index().Model;
            Product[] _p = p.ToArray();

            Assert.AreEqual(3, _p.Length);
            Assert.AreEqual("Product1", _p[0].Name);
            Assert.AreEqual("Product2", _p[1].Name);
            Assert.AreEqual("Product3", _p[2].Name);
        }
    }
}
