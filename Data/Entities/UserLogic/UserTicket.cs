using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UserTicket
    {
        public virtual User User { get; set; }
        public int UserID { get; set; }

        public virtual Ticket Ticket { get; set; }
        public int TicketID { get; set; }
    }
}