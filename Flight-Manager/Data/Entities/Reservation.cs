using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Reservation
    {
        public Reservation()
        {

        }

        public int Id { get; set; }
        [NotNull]
        public string FirstName { get; set; }
        [NotNull]
        public string MiddleName { get; set; }
        [NotNull]
        public string LastName { get; set; }
        [NotNull]
        [RegularExpression("^[0-9]", ErrorMessage = "EGN do not have correct format!")]
        [MaxLength(10)]
        public string EGN { get; set; }
        [NotNull]
        [RegularExpression("^[0-9]", ErrorMessage = "Phone Number do not have correct format!")]
        [MaxLength(9)]
        public string PhoneNumber { get; set; }
        [NotNull]
        public string Nationality { get; set; }

        [NotNull]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
