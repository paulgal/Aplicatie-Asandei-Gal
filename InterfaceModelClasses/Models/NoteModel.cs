using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceModelClasses.Models
{
    public class NoteModel
    {
        public int Id { get; set; }
        public virtual string CourseName { get; set; }
        public string Information { get; set; }
        public int CourseId { get; set; }
    }
}