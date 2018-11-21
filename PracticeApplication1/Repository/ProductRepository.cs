using PracticeApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PracticeApplication1.Repository
{
    public class ProductRepository
    {
        private storeEntities fDb = null;
        public ProductRepository()
        {
            this.fDb = new storeEntities();
        }

        public IQueryable<Product> GetAll()
        {
            return fDb.Products.AsQueryable();
        }

        public Product GetById(int id)
        {
            return fDb.Products.Find(id);
        }

        public void Create(Product product)
        {
            fDb.Products.Add(product);
            fDb.SaveChanges();
        }

        public void Update(Product product)
        {
            fDb.Entry(product).State = System.Data.Entity.EntityState.Modified;
            fDb.SaveChanges();
        }

        public void Delete(Product product)
        {
            fDb.Products.Remove(product);
            fDb.SaveChanges();
        }

        

    }
}