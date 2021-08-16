using Microsoft.AspNetCore.Authorization;

namespace Globitel.Service.Helpers
{

    public class Policies
    {
        public const string Admin = "Admin";
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        } 
        public const string Employee = "Employee";
        public static AuthorizationPolicy EmployeePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Employee).Build();
        }
    }

}
