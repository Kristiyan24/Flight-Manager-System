using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Flight
    {
        public Flight()
        {
            this.UsersFlights = new HashSet<UserFlight>();
            this.Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        [NotNull]
        public string StartLocation { get; set; }
        [NotNull]
        public string EndLocation { get; set; }
        public DateTime TakeOff { get; set; }
        public DateTime Landing { get; set; }

        [Range(0, 1000)]
        public int PlaneIdentificationNumber { get; set; }
        
        [NotNull]
        public string PlaneType { get; set; }
        
        [NotNull]
        public string PilotName { get; set; }
        [Range(1, 1000)]
        public int AllPassengersCount { get; set; }
        
        [Range(1, 1000)]
        public int BusinessClassPassengersCount { get; set; }

        [Range(0.00, 1000.00)]
        public double PriceNormal { get; set; }

        [Range(0.00, 1000.00)]
        public double PriceBusiness { get; set; }

        public ICollection<UserFlight> UsersFlights { get; set; }
        public ICollection<Reservation> Reservations { get; set; }


    }
}
