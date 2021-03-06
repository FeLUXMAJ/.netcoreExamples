﻿using DataAccess.Core.Repositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EFRepository<TEntity>
        : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Implement basic GRUD here.
        /// For Sql queries please create a separate template to make a clear code
        /// Core version 2.0 ??
        /// </summary>

        private readonly TestContext _context;

        public EFRepository(TestContext context)
        {
            this._context = context;
        }

        protected virtual DbSet<TEntity> DbSet
        {
            get
            {
                return this._context.Set<TEntity>();
            }
        }

        public virtual void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
            this._context.SaveChanges();
        }

        public virtual Task<int> AddAsync(TEntity entity)
        {
            this._context.Add(entity);
            return this._context.SaveChangesAsync();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
            this._context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
            this._context.SaveChanges();
        }

        public virtual TEntity GetById(long id)
        {
            var entity = this.DbSet.Find(id);

            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            this.DbSet.Attach(entity);
            this._context.Entry(entity).State = EntityState.Modified;
            this._context.SaveChanges();
        }
    }
}
