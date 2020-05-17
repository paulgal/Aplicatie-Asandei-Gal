using ModelProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceModelClasses.Models
{
    public class SettingsModel
    {
        public SettingsModel()
        {
            DefaultStartHour = new DateTime(1753, 1, 1);
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public int AccountId { get; set; }
        public WeekDay StartDay { get; set; }
        public double CourseDuration { get; set; }
        public DateTime DefaultStartHour { get; set; }

    }
}