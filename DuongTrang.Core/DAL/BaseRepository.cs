using DuongTrang.Core.IServices;
using DuongTrang.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.DAL
{
    public abstract class BaseRepository<D, TEntity> : IBaseRepository<TEntity> where TEntity : class where D : DbContext, new()
    {
        private D _entities = new D();
        internal DbSet<TEntity> dbSet;
        public D Context
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public BaseRepository()
        {
            dbSet = _entities.Set<TEntity>();
        }

        public virtual void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }   

        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = dbSet;
            return query;
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public Task<int> SaveChangesAsync()
        {
            return _entities.SaveChangesAsync();
        }


        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_entities != null)
                {
                    _entities.Dispose();
                    _entities = null;
                }
            }
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = dbSet.Where(predicate);
            return query;
        }


        #endregion
    }
}

