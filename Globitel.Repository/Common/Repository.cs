using Globitel.Domain.Common;
using Globitel.Repository.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Globitel.Repository.Common
{
    public class Repository<IEntity> : IRepository<IEntity> where IEntity : CoreModel, new()
    {
        #region [Context]
        protected GlobitelDbContext Context;
        #endregion

        public Repository(GlobitelDbContext context)
        {
            Context = context;
        }

        #region Get
        public IEntity Get(int Id)
        {
            IEntity result = Context.Set<IEntity>().FirstOrDefault(entity => entity.Id == Id);
            return result;
        }

        #endregion

        #region GetAll
        public IEnumerable<IEntity> GetAll()
        {
            IQueryable<IEntity> dbQuery = Context.Set<IEntity>();
            return dbQuery.AsNoTracking();
        }

        #endregion

        #region FirstOrDefault
        public IEntity FirstOrDefault(Expression<Func<IEntity, bool>> where)
        {
            IEntity result = Context.Set<IEntity>().AsNoTracking().FirstOrDefault(where);
            return result;
        }
        #endregion

        #region Any
        public bool Any(Expression<Func<IEntity, bool>> where)
        {
            bool result = Context.Set<IEntity>().Any(where);
            return result;
        }
        #endregion

        #region FirstOrDefault
        public IEntity FirstOrDefault(Expression<Func<IEntity, bool>> where, params Expression<Func<IEntity, object>>[] navigationProperties)
        {
            IQueryable<IEntity> dbQuery = Context.Set<IEntity>().AsNoTracking();

            foreach (Expression<Func<IEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<IEntity, object>(navigationProperty);
            }

            return dbQuery.Where(where).FirstOrDefault();
        }
        #endregion

        #region Find
        public IEnumerable<IEntity> Find(Expression<Func<IEntity, bool>> predicate, params Expression<Func<IEntity, object>>[] navigationProperties)
        {
            IQueryable<IEntity> dbQuery = Context.Set<IEntity>();

            foreach (Expression<Func<IEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigationProperty);
            }

            return dbQuery.Where(predicate);
        }
        #endregion

        #region List
        public List<IEntity> List(Expression<Func<IEntity, bool>> predicate, params Expression<Func<IEntity, object>>[] navigationProperties)
        {
            IQueryable<IEntity> dbQuery = Context.Set<IEntity>().Where(predicate).OrderByDescending(x => x.Id);
            int totalCount = dbQuery.Count();
            foreach (Expression<Func<IEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include(navigationProperty);
            }

            return dbQuery.Where(predicate).OrderByDescending(x => x.Id).ToList();
        }
        #endregion

        #region Add
        public IEntity Add(IEntity entity)
        {
            Context.Set<IEntity>().Add(entity);
            SaveChanges();
            Context.Entry(entity).GetDatabaseValues();
            return entity;
        }
        #endregion

        #region AddRnage
        public IEnumerable<IEntity> AddRange(IEnumerable<IEntity> entities)
        {
            Context.ChangeTracker.Entries<IEntity>();
            Context.Set<IEntity>().AddRange(entities);
            SaveChanges();
            return entities;
        }
        #endregion

        #region Update
        public IEntity Update(IEntity entity, bool disableAttach = false)
        {
            Context.Set<IEntity>().Update(entity);
            Context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
            return entity;
        }
        #endregion

        #region UpdateRange
        public IEnumerable<IEntity> UpdateRange(IEnumerable<IEntity> Entities)
        {
            Context.Set<IEntity>().UpdateRange(Entities);
            SaveChanges();
            return Entities;
        }
        #endregion

        #region Remove
        public bool Remove(int Id)
        {
            IEntity entity = Get(Id);
            if (entity != null)
            {
                Context.Set<IEntity>().Remove(entity);
                SaveChanges();
            }

            return true;
        }
        #endregion

        #region RemoveRange
        public IEnumerable<IEntity> RemoveRange(IEnumerable<IEntity> entities)
        {
            Context.Set<IEntity>().RemoveRange(entities);
            SaveChanges();
            return entities;
        }
        #endregion

        #region SaveChanges
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
        #endregion

        #region BeginTransaction
        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }
        #endregion

        #region CommitTransaction
        public void CommitTransaction()
        {
            Context.Database.CommitTransaction();
        }
        #endregion

        #region RollbackTransaction
        public void RollbackTransaction()
        {
            Context.Database.RollbackTransaction();
        }
        #endregion

    }
}
