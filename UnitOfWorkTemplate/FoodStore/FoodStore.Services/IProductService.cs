using FoodStore.Models;
using FoodStore.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodStore.Services
{
    public interface IProductService : IService<Product>
    {
        //
        IQueryable<Product> MostExpensiveProducts(int count);
    }
}
