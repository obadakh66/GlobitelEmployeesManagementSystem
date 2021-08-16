

using Microsoft.AspNetCore.Identity;

namespace Globitel.Domain.Models
{
    public partial class UserLogins : IdentityUserLogin<long>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
