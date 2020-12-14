using BookStore.Domain.Abstract;
using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        private int _pageSize = 4;
        public int pageSize { get { return _pageSize; } set { _pageSize = value; } }

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(string category,int page=1)
        {
            //return View(repository.Products
            //                      .OrderBy(m=>m.ProductID)
            //                      .Skip((page-1)*pageSize)
            //                      .Take(pageSize));

            ProductsListViewModel _m = new ProductsListViewModel();
            _m.Products = repository.Products
                                  .Where(m => category == null || m.Category == category)
                                  .OrderBy(m=>m.ProductID)
                                  .Skip((page-1)*pageSize)
                                  .Take(pageSize);

            _m.PagingInfo = new PagingInfo()
            {
                TotalItems = repository.Products
                                  .Where(m => category == null || m.Category == category)
                                  .Count(),
                    
                ItemsPerPage = pageSize,
                CurrentPage = page
            };
            _m.CurrentCategory = category;
            return View(_m);
        }

    }
}