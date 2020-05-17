using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceModelClasses.Models
{
    public class ExamModel
    {
        public ExamModel()
        {
            Date = new DateTime(1753, 1, 1);
        }
        public int Id { get; set; }
        public string CourseName { get; set; }
        public DateTime Date { get; set; }
        public double Hour { get; set; }
        public string Room { get; set; }
        public int Difficulty { get; set; }
        public int OrganizerId { get; set; }

    }
}