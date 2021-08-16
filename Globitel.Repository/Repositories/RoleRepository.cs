using Globitel.Domain.Models;
using Globitel.Repository.DbContext;
using Globitel.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Globitel.Repository.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private GlobitelDbContext _context;
        public RoleRepository(GlobitelDbContext context)
        {
            _context = context;
        }

        public Roles Add(Roles entity)
        {
            _context.Roles.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<Roles> AddRange(IEnumerable<Roles> entities)
        {
            throw new NotImplementedException();
        }

        public IDbContextTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> Find(Expression<Func<Roles, bool>> where, params Expression<Func<Roles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public Roles FirstOrDefault(Expression<Func<Roles, bool>> where, params Expression<Func<Roles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public Roles FirstOrDefault(Expression<Func<Roles, bool>> where)
        {
            Roles result = _context.Roles.FirstOrDefault(where);
            return result;
        }

        public bool Any(Expression<Func<Roles, bool>> where)
        {
            bool result = _context.Roles.Any(where);
            return result;

        }

        public Roles Get(long Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> GetAll()
        {
            throw new NotImplementedException();
        }

        public Roles Remove(Roles entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> RemoveRange(IEnumerable<Roles> entities)
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Roles Update(Roles entity, bool disableAttach = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> UpdateRange(IEnumerable<Roles> Entities)
        {
            throw new NotImplementedException();
        }

        public Roles Get(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Roles> List(Expression<Func<Roles, bool>> predicate, int PageSize, int PageNumber, params Expression<Func<Roles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public List<Roles> List(Expression<Func<Roles, bool>> predicate, params Expression<Func<Roles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
