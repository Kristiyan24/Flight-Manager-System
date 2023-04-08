using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class dbUser : IdentityUser<string>
    {
        public dbUser()
        {
            this.Flights = new HashSet<UserFlight>();
        }

        [NotNull]
        public string FirstName { get; set; }
        [NotNull]
        public string LastName { get; set; }
        [NotNull]
        public string EGN { get; set; }
        [NotNull]
        public string Address { get; set; }

        public ICollection<UserFlight> Flights { get; set; }

    }
}
