using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAcademyDB
{
    class Lectures
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int SubjectId { get; set; }
        public Subjects Subject { get; set; }

        [Required]
        public int TeacherId { get; set; }
        public Teachers Teacher { get; set; }
    }
}
