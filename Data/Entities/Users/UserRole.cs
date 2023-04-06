using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UserRole
    {
        public UserRole()
        {
            this.Users = new HashSet<User>();
        }

        public UserRole(string name)
        {
            this.Name = name;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
