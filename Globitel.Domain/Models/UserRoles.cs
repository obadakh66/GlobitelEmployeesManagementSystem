
using Microsoft.AspNetCore.Identity;

namespace Globitel.Domain.Models
{
    public partial class UserRoles : IdentityUserRole<long>
    {
        public virtual Roles Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
