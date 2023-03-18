using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LinqMehtod
{
    public class Program
    {
        public class Student
        {
            public int StudentID { get; set; }
            public string StudentName { get; set; }
            public int Age { get; set; }
            public string Major { get; set; }
            public double Tuition { get; set; }
        }
        public class StudentClubs
        {
            public int StudentID { get; set; }
            public string ClubName { get; set; }
        }
        public class StudentGPA
        {
            public int StudentID { get; set; }
            public double GPA { get; set; }
        }

        public class StudentComparerRow : IEqualityComparer < StudentClubs >
{
    public bool Equals(StudentClubs x, StudentClubs y)
    {
        if (x.StudentID == y.StudentID
                && x.ClubName.ToLower() == y.ClubName.ToLower())
            return true;

        return false;
    }

    public int GetHashCode(StudentClubs obj)
    {
        return obj.StudentID.GetHashCode();
    }
}

        public static void Main(string[] args)
        {
            // Student collection
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 1, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 2, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 3, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 5, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
            };
            // Student GPA Collection
            IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
            // Club collection
            IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
            };

            var groupedGPA = studentGPAList.OrderBy(i => i.StudentID).GroupBy(x => x.GPA);
            Console.WriteLine("Students grouped by GPA: ");
            foreach (var group in groupedGPA)
            {
                Console.WriteLine("GPA: " + String.Format("{0:F1}", group.Key));
                foreach (StudentGPA s in group)
                { 
                    Console.WriteLine("Student ID: " + s.StudentID); 
                }
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");

            var groupedClub = studentClubList.OrderBy(c => c.ClubName).GroupBy(x => x.ClubName);
            Console.WriteLine("Students orded and grouped by club: ");
            foreach(var group in groupedClub)
            {
                Console.WriteLine("Club Name: " + group.Key);
                foreach (StudentClubs s in group)
                {
                    Console.WriteLine("Student ID: " + s.StudentID);
                }
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");

            var countGPA = studentGPAList.Count(s => s.GPA >= 2.5 && s.GPA <= 4.0);
            Console.WriteLine("There are " +  countGPA + " students that have a GPA between 2.5 and 4.0");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");

            var avgTuition = studentList.Average(s => s.Tuition);
            Console.WriteLine("The average tuition across all students is " + String.Format("{0:C}", avgTuition));
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");

            Console.WriteLine("The student(s) with the highest tuition: ");
            var maxTuition = studentList.Max(s => s.Tuition);
            foreach(var s in studentList) 
            {
                if (maxTuition == s.Tuition)
                {
                    Console.WriteLine("Student Name: " + s.StudentName);
                    Console.WriteLine("Major: " + s.Major);
                    Console.WriteLine("Tution: " + String.Format("{0:C}",s.Tuition));
                }
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");

            var studentJoin = studentList.Join(studentGPAList,
                student => student.StudentID,
                gpa => gpa.StudentID,
                (student, gpa) => new
                {
                    StudentName = student.StudentName,
                    Major = student.Major,
                    GPA = gpa.GPA
                });
            Console.WriteLine("Here is the students current GPA: ");
            foreach(var s in studentJoin)
            {
                Console.WriteLine($"Name: {s.StudentName}");
                Console.WriteLine($"Major: {s.Major}");
                Console.WriteLine("GPA: " + String.Format("{0:F1}", s.GPA));
                Console.WriteLine();
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");

            var clubJoin = studentList.Join(studentClubList,
                student => student.StudentID,
                club => club.StudentID,
                (student, club) => new
                {
                    StudentName = student.StudentName,
                    ClubName = club.ClubName
                });
            var clubList = clubJoin.Where(s => s.ClubName == "Game"); 
            Console.WriteLine("These students are in the Game Club: ");
            foreach (var s in clubList)
            {
                Console.WriteLine(s.StudentName);
            }
        }
    }
}