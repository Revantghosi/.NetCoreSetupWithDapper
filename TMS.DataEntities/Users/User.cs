using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.DataEntities.Users
{
   public  class User
    {
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Token { get; set; }
    }

    public class UserRegistration
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string OrgName { get; set; }
        public string OrgAddress { get; set; }
        public string OrgContactNumber { get; set; }
    }
}
