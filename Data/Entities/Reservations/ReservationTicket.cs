using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ReservationTicket
    {
        public virtual Reservation Reservation { get; set; }
        public int ReservationID { get; set; }

        public virtual Ticket Ticket { get; set; }
        public int TicketID { get; set; }
    }
}