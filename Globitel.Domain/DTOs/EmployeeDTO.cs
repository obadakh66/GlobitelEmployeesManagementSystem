using Globitel.Domain.Enums;
using System;

namespace Globitel.Domain.DTO
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string FullNameEN { get; set; }
        public string FullNameAR { get; set; }
        public string PositionEN { get; set; }
        public string PositionAR { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployement { get; set; }
        public GenderEnum Gender { get; set; }
        public int Age { get; set; }

    }
}
