
using Microsoft.AspNetCore.Identity;

namespace Globitel.Domain.Models
{
    public partial class UserClaims : IdentityUserClaim<long>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
