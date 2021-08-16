using System.Collections.Generic;

namespace Globitel.Service.Interfaces
{
    public interface ILoggedInUserService
    {
        long GetUserId();

        List<string> GetUserRoles();
    }
}
