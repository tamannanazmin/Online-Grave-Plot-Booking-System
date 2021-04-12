using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraveBooking.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public string OwnerId { get; set; }

        public int GraveyardId { get; set; }

        public int GravePlotId { get; set; }
        public string Date { get; set; }

        public int userId { get; set; }

    }
}