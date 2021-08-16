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
    public class UserRoleRepository : IUserRoleRepository
    {
        private GlobitelDbContext _context;
        public UserRoleRepository(GlobitelDbContext context)
        {
            _context = context;
        }

        public UserRoles Add(UserRoles entity)
        {
            _context.UserRoles.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<UserRoles> AddRange(IEnumerable<UserRoles> entities)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<UserRoles, bool>> where)
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

        public IEnumerable<UserRoles> Find(Expression<Func<UserRoles, bool>> where, params Expression<Func<UserRoles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public UserRoles FirstOrDefault(Expression<Func<UserRoles, bool>> where, params Expression<Func<UserRoles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public UserRoles FirstOrDefault(Expression<Func<UserRoles, bool>> where)
        {
            UserRoles result = _context.UserRoles.FirstOrDefault(where);
            return result;
        }

        public UserRoles Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRoles> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<UserRoles> List(Expression<Func<UserRoles, bool>> predicate, int PageSize, int PageNumber, params Expression<Func<UserRoles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public List<UserRoles> List(Expression<Func<UserRoles, bool>> predicate, params Expression<Func<UserRoles, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRoles> RemoveRange(IEnumerable<UserRoles> entities)
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

        public UserRoles Update(UserRoles entity, bool disableAttach = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRoles> UpdateRange(IEnumerable<UserRoles> Entities)
        {
            throw new NotImplementedException();
        }

    }
}
