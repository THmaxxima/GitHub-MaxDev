using FoodStore.Models;
using FoodStore.Services.Core;
using FoodStore.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodStore.Services
{
    public class ProductService : ServiceBase<Product>, IProductService
    {
        public ProductService(AppDb db) : base(db)
        {
            //
        }

        public override IQueryable<Product> Query(Func<Product, bool> criteria)
        {
            return base.Query(criteria).Where(x => x.IsActive);
        }
        public IQueryable<Product> MostExpensiveProducts(int count)
        {
            return All.OrderByDescending(x => x.Price).Take(count);
        }
    }
}
