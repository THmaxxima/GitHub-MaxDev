using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodStore.Services.Core
{
    public interface IService<T> where T: class
    {
        IQueryable<T> Query(Func<T, bool> criteia);
        IQueryable<T> All { get; }
        T Find(params object[] keys);
        T Add(T item);
        T Remove(T item);
    }
}
