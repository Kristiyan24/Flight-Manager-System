using System.Collections.Generic;

namespace Data.Entities
{
    public class Ticket
    {
        public int ID { get; set; }
        
        public virtual TicketType Type { get; set; }
        public int TypeID { get; set; }

        public virtual Flight Flight { get; set; }
        public int FlightID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }


        ////////////////////////////////////////////////////////
        public UserTicket UserTicket { get; set; }
        public int? UserTicketID { get; set; }

        public ReservationTicket ReservationTicket { get; set; }
        public int? ReservationTicketID { get; set; }
        ////////////////////////////////////////////////////////
    }
}