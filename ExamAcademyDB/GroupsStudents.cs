using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExamAcademyDB
{
    class GroupsStudents
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupId { get; set; }
        public Groups Group { get; set; }

        [Required]
        public int StudentId { get; set; }
        public Students Student { get; set; }
    }
}
