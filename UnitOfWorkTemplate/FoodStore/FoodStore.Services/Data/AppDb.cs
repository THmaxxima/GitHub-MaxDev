using FoodStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStore.Services.Data
{
    public class AppDb : DbContext 
    {
        public AppDb(DbContextOptions<AppDb> options): base(options)
        {
            //
        }
        public DbSet<Product> Products { get; set; }
    }
}
