
using ModelProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceModelClasses.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public int OrganizerId { get; set; }
        public int SettingsId { get; set; }

    }
}