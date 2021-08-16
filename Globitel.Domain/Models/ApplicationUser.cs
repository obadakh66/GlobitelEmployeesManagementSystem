using Globitel.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Globitel.Domain.Models
{
    public partial class ApplicationUser : IdentityUser<long>
    {
        public ApplicationUser()
        {
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            UserClaims = new HashSet<UserClaims>();
            UserLogins = new HashSet<UserLogins>();
            UserRoles = new HashSet<UserRoles>();
        }

        public bool? IsActive { get; set; }
        public int Role { get; set; }
        public string FullNameEN { get; set; }
        public string FullNameAR { get; set; }
        public string PositionEN { get; set; }
        public string PositionAR { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployement { get; set; }
        public GenderEnum Gender { get; set; }
        public int Age { get; set; }

        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
    
        public virtual ICollection<UserClaims> UserClaims { get; set; }
        public virtual ICollection<UserLogins> UserLogins { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }


    }
}
