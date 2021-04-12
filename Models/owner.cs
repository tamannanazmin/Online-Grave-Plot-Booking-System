using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraveBooking.Models
{
    public class owner
    {
       // ownerId,fullName,gender,fatherName,motherName,profession,phone,email,address,photo,nid,dateOfBirth,plotId,totalPrice,leaseOrPermanent,leaseYear,bankName,branchName,branchAddress,dd,adminId

        public int ownerId { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string profession { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string photo { get; set; }

        public string nid { get; set; }
        public string dateOfBirth { get; set; }
        public int plotId { get; set; }
        public int totalPrice { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string branchAddress { get; set; }
        public string dd { get; set; }
        public int adminId { get; set; }
        public int userId { get; set; }

        public int graveyardId { get; set; }
    }
}