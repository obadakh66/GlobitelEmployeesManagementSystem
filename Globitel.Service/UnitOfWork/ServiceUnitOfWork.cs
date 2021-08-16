using Globitel.Domain.Models;
using Globitel.Repository.DbContext;
using Globitel.Repository.UnitOfWork;
using Globitel.Service.Interfaces;
using Globitel.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;

namespace Globitel.Service.UnitOfWork
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;
        private readonly LoggedInUserService _loggedInUserService;

        public Lazy<IAuthService> Auth { get; set; }
        public Lazy<IEmployeeService> Employee { get; set; }
        public ServiceUnitOfWork(
           GlobitelDbContext context,
           UserManager<ApplicationUser> userManager,
           IHttpContextAccessor httpContextAccessor,
           SignInManager<ApplicationUser> signInManager
           )
        {
            _loggedInUserService = new LoggedInUserService(httpContextAccessor);
            _repositoryUnitOfWork = new RepositoryUnitOfWork(context);
            Auth = new Lazy<IAuthService>(() => new AuthService(userManager, signInManager));
            Employee = new Lazy<IEmployeeService>(() => new EmployeeService(userManager,context));
        }


        public void Dispose() { }
    }
}
