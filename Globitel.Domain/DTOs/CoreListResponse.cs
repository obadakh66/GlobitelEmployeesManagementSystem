using System.Collections.Generic;

namespace Globitel.Domain.DTO
{
    public class CoreListResponse<TEntity>
    {
        public List<TEntity> Entities { get; set; }
        public int TotalCount { get; set; }
    }
}
