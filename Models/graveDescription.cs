using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraveBooking.Models
{
    public class graveDescription
    {
        //contact availablePlot
        public int graveyardId { get; set; }
        public string graveyardName { get; set; }
        public string authorName { get; set; }
        public int numberOfPlots { get; set; }
        public int plotPrice { get; set; }
        public string contact { get; set; }
        public int availablePlot{ get; set; }
        public string location { get; set; }
        public string posterImage { get; set; }
        public int adminId { get; set; }
        
    }
}