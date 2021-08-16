using Globitel.Domain.DTO;
using System.Collections.Generic;

namespace Globitel.Service.Interfaces
{
    public interface IService<Entity, TEntity>
    {
        Entity Add(Entity entity);
        IEnumerable<Entity> AddRange(IEnumerable<Entity> entities);
        Entity Update(Entity entity);
        IEnumerable<Entity> UpdateRange(IEnumerable<Entity> entities);
        bool Remove(int Id);
        IEnumerable<Entity> RemoveRange(IEnumerable<Entity> entities);
        IEnumerable<Entity> RemoveRangeByIDs(IEnumerable<long> IDs);
        Entity Get(int Id);
        IEnumerable<Entity> GetAll();
        CoreListResponse<Entity> List(TEntity entity);

    }
}
