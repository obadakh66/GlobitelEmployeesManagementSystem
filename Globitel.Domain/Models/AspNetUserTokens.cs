using Microsoft.AspNetCore.Identity;

namespace Globitel.Domain.Models
{
    public partial class AspNetUserTokens : IdentityUserToken<long>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
