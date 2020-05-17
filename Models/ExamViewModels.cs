using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interface.Models
{
    public class ExamViewModel
    {
        [Required(ErrorMessage = "Denumirea materiei este necesară!")]
        [Display(Name = "Denumirea materiei examenului")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Ziua de desfășurare a examenului este necesară!")]
        [Display(Name = "Ziua de desfășurare a examenului")]
        public DateTime ExamDay { get; set; }

        [Required(ErrorMessage = "Ora de desfășurare a examenului este necesară!")]
        [Display(Name = "Ora de desfășurare a examenului")]
        [Range(1, 24, ErrorMessage = "Ora trebuie sa fie cuprinsa intre 1 si 24")]
        public double Hour { get; set; }

        [Required(ErrorMessage = "Sala in care se va desfasura examenul este necesară!")]
        [Display(Name = "Sala")]
        public string ExamRoom { get; set; }

        [Required(ErrorMessage = "Dificultatea examenului este necesara!")]
        [Range(1, 10, ErrorMessage = "Dificultatea trebuie sa fie intre 1 si 10")]
        [Display(Name = "Dificultatea examenului (1-10)")]
        public int Difficulty { get; set; }
    }
    public class ExamEditViewModel
    {
        [Display(Name = "Denumirea materiei examenului")]
        public string CourseName { get; set; }

        [Display(Name = "Ziua de desfășurare a examenului")]
        public DateTime? ExamDay { get; set; }

        [Display(Name = "Ora de desfășurare a examenului")]
        [Range(1, 24, ErrorMessage = "Ora trebuie sa fie cuprinsa intre 1 si 24")]
        public double? Hour { get; set; }

        [Display(Name = "Sala")]
        public string ExamRoom { get; set; }

        [Range(1, 10, ErrorMessage = "Dificultatea trebuie sa fie intre 1 si 10")]
        [Display(Name = "Dificultatea examenului (1-10)")]
        public int? Difficulty { get; set; }
    }
}