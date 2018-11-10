using System;
using System.Linq;

namespace FoodStore.Services.Core
{
    public interface IServiceBase<T> where T : class
    {
        IQueryable<T> All { get; }

        T Add(T item);
        T Find(params object[] keys);
        IQueryable<T> Query(Func<T, bool> criteria);
        T Remove(T item);
    }
}