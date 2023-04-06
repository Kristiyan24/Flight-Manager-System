using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class TicketType
    {
        public TicketType()
        {
            this.Tickets = new HashSet<Ticket>();
        }

        public TicketType(string name)
        {
            this.Name = name;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
