using Globitel.Repository.DbContext;
using Globitel.Repository.Interfaces;
using Globitel.Repository.Repositories;
using System;

namespace Globitel.Repository.UnitOfWork
{
    public class RepositoryUnitOfWork : IRepositoryUnitOfWork
    {
        private GlobitelDbContext _Context;


        public Lazy<IRoleRepository> Roles { get; set; }

        public Lazy<IUserRoleRepository> UserRole { get; set; }
        public RepositoryUnitOfWork(GlobitelDbContext context)
        {
            _Context = context;
            UserRole = new Lazy<IUserRoleRepository>(() => new UserRoleRepository(_Context));
            Roles = new Lazy<IRoleRepository>(() => new RoleRepository(_Context));
        }

        public void Dispose()
        {
            _Context.Dispose();

        }
    }
}
