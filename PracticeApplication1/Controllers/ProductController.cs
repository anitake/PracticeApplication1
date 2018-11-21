using PracticeApplication1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PracticeApplication1.Models;

namespace PracticeApplication1.Controllers
{
    public class ProductController : Controller
    {
        public const int PAGE_NUMBER = 7;
        private ProductRepository fProductRepository = null;

        public ProductController()
        {
            this.fProductRepository = new ProductRepository();
        }
        // GET: Product
        public ActionResult Index(int page=1)
        {
            int currentPage = page < 1 ?1: page;

            var products = fProductRepository.GetAll().OrderBy(x => x.ProductID);

            var result = products.ToPagedList(currentPage, PAGE_NUMBER);

            return View(result);
        }

        public ActionResult GetImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "bad-grades";
            }
            return File("~/ProductImages/" + fileName, "image/jpeg|png");
        }

        public ActionResult GetImageById(int id)
        {
            Product product = fProductRepository.GetAll().ToList().Where(x => x.ProductID == id).FirstOrDefault();

            byte[] img = product.BytesImage;

            ///TODO 有空值先帶預設，待修
            if (img ==null)
            {
               product = fProductRepository.GetAll().ToList().Where(x => x.ProductID == 14).FirstOrDefault();

                img = product.BytesImage;
            }  
            return File(img, "image/jpeg");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.category = fProductRepository.GetAll().GroupBy(x=>x.CategoryID).Select(y => y.Key).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            
            fProductRepository.Create(product);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = fProductRepository.GetAll();

            ViewBag.category = db.GroupBy(x => x.CategoryID).Select(y => y.Key).ToList();

            Product product = db.Where(x => x.ProductID == id).FirstOrDefault();

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            fProductRepository.Update(product);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Product product = fProductRepository.GetAll().Where(x => x.ProductID == id).FirstOrDefault();

            fProductRepository.Delete(product);

            return RedirectToAction("Index");

        }


    }
}