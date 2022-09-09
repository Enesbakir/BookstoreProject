using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetAll();
    T GetById(int id);
    T Get(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Update(T entity);

}