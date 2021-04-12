using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraveBooking.Models
{
    public class ViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<graveDescription> graveDescriptions { get; set; }
        public IEnumerable<admin> admins { get; set; }
        public IEnumerable<owner> owners { get; set; }
        public IEnumerable<Temp> Temps { get;  set; }
        public IEnumerable<Contact> contact { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
    }
}