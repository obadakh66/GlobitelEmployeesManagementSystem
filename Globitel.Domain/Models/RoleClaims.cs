using Microsoft.AspNetCore.Identity;

namespace Globitel.Domain.Models
{
    public partial class RoleClaims : IdentityRoleClaim<long>
    {
        public virtual Roles Role { get; set; }
    }
}
