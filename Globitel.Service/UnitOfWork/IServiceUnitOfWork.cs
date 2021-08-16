
using Globitel.Service.Interfaces;
using System;

namespace Globitel.Service.UnitOfWork
{
    public interface IServiceUnitOfWork : IDisposable
    {
        Lazy<IAuthService> Auth { get; set; }
        Lazy<IEmployeeService> Employee { get; set; }
    }
}
