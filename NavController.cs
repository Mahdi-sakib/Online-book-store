using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public PartialViewResult Menu(string category=null)
        {
            ViewBag.selectedcategory = category;
            IEnumerable<string> _m = repository.Products
                                          .Select(p => p.Category)
                                          .Distinct()
                                          .OrderBy(p => p);
            return PartialView(_m);
        }
    }
}