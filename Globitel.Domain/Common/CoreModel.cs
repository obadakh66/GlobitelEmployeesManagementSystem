using Globitel.Domain.Models;
using System;

namespace Globitel.Domain.Common
{
    public class CoreModel
    {
        public int Id { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedById { get; set; }
        public ApplicationUser ModifiedBy { get; set; }
    }
}
