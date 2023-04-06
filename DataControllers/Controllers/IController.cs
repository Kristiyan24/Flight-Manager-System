using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Controllers
{
    public interface IController<Entity>
    {
        public void Add(Entity entity);
        public void Add(params Entity[] entities);
        public void AddRange(IEnumerable<Entity> collection);
        public void Insert(int index, Entity entity);
        public void InsertRange(int index, params Entity[] entities);
        public void InsertRange(int index, IEnumerable<Entity> collection);
        public bool Remove(Entity item);
        public void RemoveAt(int index);
        public void RemoveRange(int index, int count);
        public int RemoveAll();
        public bool Contains(Entity entity);
        public int IndexOf(Entity entity);
        public Entity GetAt(int index);
        public ICollection<Entity> GetAll();
        public void IDsValidation();
        public bool EntityValidation(Entity entity);
        // protected void MakeEntityConnections(Entity entity);
        // protected void MakeEntitiesConnections(Entity entity);
    }
}
