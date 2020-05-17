using ModelProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceModelClasses.Models
{
    public class CourseModel
    {
        public CourseModel()
        {
            Notes = new List<NoteModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Professor { get; set; }
        public WeekDay Day { get; set; }
        public double Hour { get; set; }
        public int OrganizerId {get;set;}
        public ICollection<NoteModel> Notes { get; set; }
    }
}