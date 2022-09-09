using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBoperations;

public class Repository<Type> : IGenericRepository<Type> where Type : class
{

    private readonly DbContext _context;
    private readonly DbSet<Type> _dbSet;
    public Repository(BookStoreDbContext dbContext)
    {
        _context = dbContext;
        _dbSet = _context.Set<Type>();
    }
    public IQueryable<Type> GetAll()
    {
        return _dbSet;
    }
    public IQueryable<Type> GetAll(Expression<Func<Type, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }
    public Type GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public Type Get(Expression<Func<Type, bool>> predicate)
    {
        return _dbSet.Where(predicate).SingleOrDefault();
    }

    public void Add(Type entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(Type entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

}