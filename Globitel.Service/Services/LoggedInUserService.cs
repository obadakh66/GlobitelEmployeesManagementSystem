using Globitel.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Globitel.Service.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public long GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return long.Parse(userId);
        }

        public List<string> GetUserRoles()
        {
            var roles = _httpContextAccessor.HttpContext.User.FindAll(ClaimTypes.Role);
            return roles.Select(r => r.Value).ToList();
        }
    }
}
