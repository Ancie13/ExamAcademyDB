using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamAcademyDB
{
    class Groups
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public int Year { get; set; }

        public int DepartmentId { get; set; }
        public Departments Department { get; set; }

        public ICollection<GroupsStudents> GroupsStudents { get; set; }
    }
}
