using GraveBooking.Models;
using GraveBooking.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraveBooking.Services.Operation
{
    public class SecurityServices
    {
        SecurityDAO daoService = new SecurityDAO();
        SecurityDAO daoServiceAdmin = new SecurityDAO();

        public bool Authenticate(User user)
        {
            return daoService.FindByUser(user);
        }
        public bool AuthenticateAdmin(admin admin)
        {
            return daoService.FindByAdmin(admin);
        }
    }
}