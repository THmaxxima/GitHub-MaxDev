using FoodStore.Services.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStore.Services
{
    public class App
    {
        private readonly AppDb db;
        private ProductService _productService;
        public App(AppDb db)
        {
            this.db = db;
            Products = new ProductService(db);
        }

        public ProductService Products { get; set; }

        public int SaveChanges()
        {
            foreach (var p in db.Products.Local)
            {
                if (db.Entry(p).State == EntityState.Modified)
                {
                    p.UpdatedDate = DateTime.Now;
                    p.UpdatedBy = "";
                }
            }
            return db.SaveChanges();
        }
    }
}
