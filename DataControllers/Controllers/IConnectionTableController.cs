using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataControllers.Controllers
{
    public interface IConnectionTableController<Entity>
    {
        public abstract FmDbContext FmDbContext { get; set; }
        public int EntityCount();
        public void AddEntity(Entity entity);
        public void AddEntities(params Entity[] entities);
        public void AddEntitiesRange(IEnumerable<Entity> collection);

        public void InsertEntity(int index, Entity entity);
        public void RemoveEntity(Entity entity);
        public void RemoveEntity(int index);
        public void RemoveAllEntities();
        public bool ContainsEntity(Entity entity);
        public int IndexOfEntity(Entity entity);
        public Entity GetEntity(int index);
        public ICollection<Entity> GetAllEntities();
        public bool EntityObjectValidation(Entity entity);
        public bool EntityObjectIDsValidation(int id1, int id2);
    }
}
