using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Controllers
{
    public class UserController : IController<User>
    {
        public static int IDCounter = 0;
        private FmDbContext dbContext;
        public UserController()
        {
            this.dbContext = new FmDbContext();
            
        }

        public UserController(FmDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(params User[] entities)
        {
            if (entities == null || entities.Length == 0)
            {
                return;
            }
            foreach (User user in entities)
            {
                if (EntityValidation(user))
                {
                    IDCounter++;
                    this.dbContext.Users.Add(user);
                }
            }
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<User> collection)
        {
            throw new NotImplementedException();
        }

        public bool Contains(User entity)
        {
            throw new NotImplementedException();
        }

        public bool EntityValidation(User entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetAt(int index)
        {
            throw new NotImplementedException();
        }

        public void IDsValidation()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(User entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, User entity)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(int index, params User[] entities)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(int index, IEnumerable<User> collection)
        {
            throw new NotImplementedException();
        }

        public bool Remove(User item)
        {
            throw new NotImplementedException();
        }

        public int RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(int index, int count)
        {
            throw new NotImplementedException();
        }
    }
}
