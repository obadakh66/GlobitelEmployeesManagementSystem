
using Globitel.Repository.Interfaces;
using System;

namespace Globitel.Repository.UnitOfWork
{
    public interface IRepositoryUnitOfWork : IDisposable
    {

        Lazy<IRoleRepository> Roles { get; set; }
        Lazy<IUserRoleRepository> UserRole { get; set; }

    }
}
