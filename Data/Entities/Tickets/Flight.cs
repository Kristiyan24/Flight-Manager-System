using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Flight
    {
        public Flight()
        {
            this.Tickets = new HashSet<Ticket>();
        }

        public int ID { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime TakeOff { get; set; }
        public DateTime Landing { get; set; }
        
        public int PlaneID { get; set; }
        public string PlaneType { get; set; }
        public string PilotName { get; set; }
        public int AllPassengersCount { get; set; }
        public int BusinessClassPassengersCount { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
