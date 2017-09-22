using SportStore.Domain.Entities;
using SportStore.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace SportStore.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private IGenericRepository<Product> productRepository;

        public ProductController()
        {
            
        }
        public ProductController(IGenericRepository<Product> repositoryParam)
        {
            productRepository = repositoryParam;
        }

        public ActionResult List()
        {
            return View(productRepository.GetAll());
        }
    }
}