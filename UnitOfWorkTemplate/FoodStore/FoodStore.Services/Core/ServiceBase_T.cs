using FoodStore.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodStore.Services.Core
{
    public abstract class ServiceBase<T> : IService<T> where T : class
    {
        private readonly AppDb db;

        public ServiceBase(AppDb db) => this.db = db;

        public virtual IQueryable<T> Query(Func<T, bool> criteria)
          => db.Set<T>().Where(criteria).AsQueryable();

        public virtual IQueryable<T> All => Query(x => true);
        public virtual T Find(params object[] keys) => db.Set<T>().Find(keys);

        public virtual T Add(T item) => db.Set<T>().Add(item).Entity;
        public virtual T Remove(T item) => db.Set<T>().Remove(item).Entity;

    }
}