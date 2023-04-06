using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User
    {
        public User()
        {
            this.Tickets = new HashSet<UserTicket>();
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }


        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual UserRole Role { get; set; }
        public int RoleID { get; set; }
        
        public ICollection<UserTicket> Tickets { get; set; }
    }
}
