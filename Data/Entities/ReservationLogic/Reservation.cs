using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Reservation
    {
        public Reservation()
        {
            this.Tickets = new HashSet<ReservationTicket>();
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public string Mobile { get; set; }
        public string Nationality { get; set; }
        public virtual ICollection<ReservationTicket> Tickets { get; set; }
    }
}
