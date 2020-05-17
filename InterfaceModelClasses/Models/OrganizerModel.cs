using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceModelClasses.Models
{
    public class OrganizerModel
    {
        public OrganizerModel()
        {
            Courses = new List<CourseModel>();
            Exams = new List<ExamModel>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public int AccountId { get; set; }
        public ICollection<CourseModel> Courses { get; set; }
        public ICollection<ExamModel> Exams { get; set; }
    }
}