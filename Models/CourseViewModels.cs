using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interface.Models
{
    public class CourseViewModel
    {
        [Required(ErrorMessage = "Denumirea cursului este necesară!")]
        [Display(Name = "Denumirea cursului")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Ziua de desfășurare a cursului este necesară!")]
        [Display(Name = "Ziua de desfășurare a cursului")]
        public string CourseDay { get; set; }

        [Required(ErrorMessage = "Ora de desfășurare a cursului este necesară!")]
        [Display(Name = "Ora de desfășurare a cursului")]
        public double CourseHour { get; set; }
        
        [Display(Name = "Profesorul ce ține cursul")]
        public string CourseProfessor { get; set; }
    }
    public class EditCourseViewModel
    {
        [Display(Name = "Denumirea cursului")]
        public string CourseName { get; set; }
        [Display(Name = "Ziua de desfășurare a cursului")]
        public string CourseDay { get; set; }
        [Display(Name = "Ora de desfășurare a cursului")]
        public double? CourseHour { get; set; }
        [Display(Name = "Profesorul ce ține cursul")]
        public string CourseProfessor { get; set; }
    }
}