using ModelProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceModelClasses.Models
{
    public class ReminderModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ReminderInfo { get; set; }
        public int ActivityId { get; set; }
        public ActivityType Type { get; set; } 
    }
    public enum ActivityType
        {
            Course,
            Exam
        }
}