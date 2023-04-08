using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserFlight
    {
        public int Id { get; set; }
        public virtual dbUser dbUser { get; set; }
        public string dbUserId { get; set; }

        public virtual Flight Flight { get; set; }
        public int FlightId{ get; set; }
    }
}