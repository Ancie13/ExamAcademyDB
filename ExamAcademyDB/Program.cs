using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ExamAcademyDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //using (var db = new MyDbContext())
            //{
            //    var f1 = new Faculties { Name = "Engineering" };
            //    var f2 = new Faculties { Name = "Computer Science" };
            //    var f3 = new Faculties { Name = "Mathematics" };
            //    var f4 = new Faculties { Name = "Physics" };
            //    db.Faculties.AddRange(f1, f2, f3, f4);

            //    var d1 = new Departments { Name = "Mechanical Dept", Building = 1, Financing = 100000, Faculty = f1 };
            //    var d2 = new Departments { Name = "Software Dept", Building = 2, Financing = 200000, Faculty = f2 };
            //    var d3 = new Departments { Name = "Applied Math Dept", Building = 3, Financing = 150000, Faculty = f3 };
            //    var d4 = new Departments { Name = "Theoretical Physics Dept", Building = 4, Financing = 250000, Faculty = f4 };
            //    db.Departments.AddRange(d1, d2, d3, d4);

            //    var g1 = new Groups { Name = "ME-101", Year = 1, Department = d1 };
            //    var g2 = new Groups { Name = "CS-201", Year = 2, Department = d2 };
            //    var g3 = new Groups { Name = "MATH-301", Year = 3, Department = d3 };
            //    var g4 = new Groups { Name = "PH-401", Year = 4, Department = d4 };
            //    db.Groups.AddRange(g1, g2, g3, g4);

            //    var s1 = new Students { Name = "John", Surname = "Smith", Rating = 85 };
            //    var s2 = new Students { Name = "Alice", Surname = "Brown", Rating = 92 };
            //    var s3 = new Students { Name = "Bob", Surname = "Taylor", Rating = 76 };
            //    var s4 = new Students { Name = "Emma", Surname = "Wilson", Rating = 88 };
            //    var s5 = new Students { Name = "Daniel", Surname = "Clark", Rating = 95 };
            //    db.Students.AddRange(s1, s2, s3, s4, s5);

            //    var t1 = new Teachers { Name = "Mark", Surname = "Davis", Salary = 3000, IsProfessor = true };
            //    var t2 = new Teachers { Name = "Sophia", Surname = "Lee", Salary = 2500, IsProfessor = false };
            //    var t3 = new Teachers { Name = "Michael", Surname = "Miller", Salary = 2800, IsProfessor = true };
            //    db.Teachers.AddRange(t1, t2, t3);

            //    var c1 = new Curators { Name = "Olivia", Surname = "Adams" };
            //    var c2 = new Curators { Name = "Liam", Surname = "Johnson" };
            //    db.Curators.AddRange(c1, c2);

            //    var sub1 = new Subjects { Name = "Physics" };
            //    var sub2 = new Subjects { Name = "Mathematics" };
            //    var sub3 = new Subjects { Name = "Programming" };
            //    var sub4 = new Subjects { Name = "Mechanics" };
            //    db.Subjects.AddRange(sub1, sub2, sub3, sub4);

            //    var l1 = new Lectures { Date = new DateTime(2025, 9, 1), Subject = sub1, Teacher = t1 };
            //    var l2 = new Lectures { Date = new DateTime(2025, 9, 2), Subject = sub2, Teacher = t2 };
            //    var l3 = new Lectures { Date = new DateTime(2025, 9, 3), Subject = sub3, Teacher = t3 };
            //    db.Lectures.AddRange(l1, l2, l3);

            //    db.GroupStudents.AddRange(
            //        new GroupsStudents { Group = g1, Student = s1 },
            //        new GroupsStudents { Group = g2, Student = s2 },
            //        new GroupsStudents { Group = g3, Student = s3 },
            //        new GroupsStudents { Group = g4, Student = s4 },
            //        new GroupsStudents { Group = g2, Student = s5 }
            //    );

            //    db.GroupCurators.AddRange(
            //        new GroupsCurators { Group = g1, Curator = c1 },
            //        new GroupsCurators { Group = g2, Curator = c2 }
            //    );

            //    db.GroupLectures.AddRange(
            //        new GroupsLectures { Group = g1, Lecture = l1 },
            //        new GroupsLectures { Group = g2, Lecture = l2 },
            //        new GroupsLectures { Group = g3, Lecture = l3 }
            //    );

            //    db.SaveChanges();
            //}

            using (var db = new MyDbContext())
            {
                //------------------------------------------------------------------------------------------------task0
                //Вывести номера корпусов, если суммарный фонд финансирования расположенных в них кафедр превышает 100000.
                var query1 = db.Departments
                    .GroupBy(d => d.Building)
                    .Where(g => g.Sum(x => x.Financing) > 100000)
                    .Select(g => g.Key);

                foreach (var b in query1)
                    Console.WriteLine("task0: " + b);

                //------------------------------------------------------------------------------------------------task1
                //Вывести названия групп 5 - го курса кафедры «Software Development», которые имеют более 10 пар в первую неделю.
                var query2 =
                    from g in db.Groups
                    where g.Year == 4 && g.Department.Name == "Software Development"
                    let lectureCount = db.GroupLectures
                        .Count(gl => gl.GroupId == g.Id && gl.Lecture.Date >= new DateTime(2025, 9, 1) && gl.Lecture.Date < new DateTime(2025, 9, 8))
                    where lectureCount > 10
                    select g.Name;

                foreach (var g in query2)
                    Console.WriteLine("task1: " + g);

                //------------------------------------------------------------------------------------------------task2
                //Вывести названия групп, имеющих рейтинг (средний рейтинг всех студентов группы) больше, чем рейтинг группы «D221».
                var rating = db.Groups
                    .Where(g => g.Name == "MATH-301")
                    .Select(g => g.GroupsStudents.Average(gs => gs.Student.Rating))
                    .FirstOrDefault();

                var result = db.Groups
                    .Where(g => g.GroupsStudents.Average(gs => gs.Student.Rating) > rating)
                    .Select(g => g.Name);

                foreach (var g in result)
                    Console.WriteLine("task2: " + g);
                //------------------------------------------------------------------------------------------------task3
                //Вывести фамилии и имена преподавателей, ставка которых выше средней ставки профессоров.
                var avgProfessorSalary = db.Teachers.Where(t => t.IsProfessor).Average(t => t.Salary);

                var query3 = db.Teachers
                    .Where(t => t.Salary > avgProfessorSalary)
                    .Select(t => t.Name + " " + t.Surname);

                foreach (var t in query3)
                    Console.WriteLine("task3: " + t);
                //------------------------------------------------------------------------------------------------task4
                //Вывести названия групп, у которых больше одного куратора.
                var query4 =
                    from g in db.Groups
                    where db.GroupCurators.Count(gc => gc.GroupId == g.Id) > 1
                    select g.Name;

                foreach (var g in query4)
                    Console.WriteLine("task4: " + g);
                //------------------------------------------------------------------------------------------------task5
                //Вывести названия групп, имеющих рейтинг (средний рейтинг всех студентов группы) меньше, чем минимальный рейтинг групп 5-го курса.
                var minRating5 = db.Groups
                    .Where(g => g.Year == 4)
                    .Select(g => g.GroupsStudents.Average(gs => gs.Student.Rating))
                    .Min();

                var query5 = db.Groups
                    .Where(g => g.GroupsStudents.Average(gs => gs.Student.Rating) < minRating5)
                    .Select(g => g.Name);

                foreach (var g in query5)
                    Console.WriteLine("task5: " + g);
            }
        }
    }
}
