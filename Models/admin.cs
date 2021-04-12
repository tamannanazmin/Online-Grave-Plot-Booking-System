using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace GraveBooking.Models
{
    [Table("admin")]
    public class admin
    {
             [Key]
            public int adminId { get; set; }

            public string adminName { get; set; }
            public string adminEmail { get; set; }
            public string adminPhone { get; set; }
            public string adminPassword { get; set; }
          
        
    }
}