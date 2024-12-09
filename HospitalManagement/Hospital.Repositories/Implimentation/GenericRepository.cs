﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Repositories.Interface;
using System.Linq.Expressions;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;


namespace Hospital.Repositories.Implimentation
{
    public class GenericRepository<T>:IDisposable, IGenericRepository<T> where T:class
    {


          private readonly ApplicationDbContext _context;
          internal DbSet<T> dbSet;

       public GenericRepository(ApplicationDbContext context)
       {
        _context = context;
         dbSet = _context.Set<T>();
       }


        public  IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
          string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);

            }
            if (orderby != null)
            {
                return orderby(query).ToList();

            }
            else
            {
                return query.ToList();


            }
        }

        public void Add( T entity)
        { 
         dbSet.Add(entity);
        
        }


        public async Task<T> AddAsync(T entity)
        {
            dbSet.Add(entity);
            return entity;

        }

        public void Delete(T entity) 
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        
        
        }


        public async Task<T> DeleteAsync(T entity)
        {


            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
            return entity;
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        
        }

        private void Dispose( bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                
                }
            
            }

            this.disposed= true;
        
        }


     

       public  T  GetById( object id)
       {
                return dbSet.Find(id);
            
       }

        public async Task<T> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);

        }

        public void Update(T entity)
        { 
             dbSet.Update(entity);
            _context.Entry(entity).State= EntityState.Modified; 
        
        }


        public async Task<T> UpdateAsync(T entity)
        { 
           dbSet.Attach(entity);
           _context.Entry(entity).State= EntityState.Modified;
            return entity;
        
        }

      
    }


}