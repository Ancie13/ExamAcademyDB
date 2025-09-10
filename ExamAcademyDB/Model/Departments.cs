using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAcademyDB
{
    public class Departments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public int Building { get; set; }

        [Required]
        public decimal Financing { get; set; }

        public int FacultyId { get; set; }
        public Faculties Faculty { get; set; }
    }
}
