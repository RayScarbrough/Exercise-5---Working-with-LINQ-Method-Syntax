using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
       
        IList<Student> studentList = new List<Student>()
        {
            new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major = "Hospitality", Tuition = 3500.00 },
            new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major = "Hospitality", Tuition = 4500.00 },
            new Student() { StudentID = 3, StudentName = "Cookie Crumb", Age = 21, Major = "CIT", Tuition = 2500.00 },
            new Student() { StudentID = 4, StudentName = "Ima Script", Age = 48, Major = "CIT", Tuition = 5500.00 },
            new Student() { StudentID = 5, StudentName = "Cora Coder", Age = 35, Major = "CIT", Tuition = 1500.00 },
            new Student() { StudentID = 6, StudentName = "Ura Goodchild", Age = 40, Major = "Marketing", Tuition = 500.00 },
            new Student() { StudentID = 7, StudentName = "Take Mewith", Age = 29, Major = "Aerospace Engineering", Tuition = 5500.00 }
        };

        IList<StudentGPA> studentGPAList = new List<StudentGPA>()
        {
            new StudentGPA() { StudentID = 1, GPA = 4.0 },
            new StudentGPA() { StudentID = 2, GPA = 3.5 },
            new StudentGPA() { StudentID = 3, GPA = 2.0 },
            new StudentGPA() { StudentID = 4, GPA = 1.5 },
            new StudentGPA() { StudentID = 5, GPA = 4.0 },
            new StudentGPA() { StudentID = 6, GPA = 2.5 },
            new StudentGPA() { StudentID = 7, GPA = 1.0 }
        };

        IList<StudentClubs> studentClubList = new List<StudentClubs>()
        {
            new StudentClubs() { StudentID = 1, ClubName = "Photography" },
            new StudentClubs() { StudentID = 1, ClubName = "Game" },
            new StudentClubs() { StudentID = 2, ClubName = "Game" },
            new StudentClubs() { StudentID = 5, ClubName = "Photography" },
            new StudentClubs() { StudentID = 6, ClubName = "Game" },
            new StudentClubs() { StudentID = 7, ClubName = "Photography" },
            new StudentClubs() { StudentID = 3, ClubName = "PTK" }
        };

      
        var groupByStudentID = studentGPAList.GroupBy(s => s.StudentID)
                                      .Select(g => new { StudentID = g.Key, GPAs = g.Select(x => x.GPA) });

        foreach (var group in groupByStudentID)
        {
            Console.WriteLine($"Student ID: {group.StudentID}");
            foreach (var gpa in group.GPAs)
            {
                Console.WriteLine($"GPA: {gpa}");
            }
        }

        
        Console.WriteLine("\nSort by Club, then group by Club:");
        var sortByClubGroupByClub = studentClubList.OrderBy(s => s.ClubName)
                                                   .GroupBy(s => s.ClubName)
                                                   .Select(g => new { ClubName = g.Key, StudentIDs = g.Select(x => x.StudentID) });
        foreach (var group in sortByClubGroupByClub)
        {
            Console.WriteLine($"Club: {group.ClubName}");
            foreach (var id in group.StudentIDs)
            {
                Console.WriteLine($"Student ID: {id}");
            }
        }

        
        int count = studentGPAList.Count(s => s.GPA >= 2.5 && s.GPA <= 4.0);
        Console.WriteLine($"\nNumber of students with GPA between 2.5 and 4.0: {count}");

        
        double averageTuition = studentList.Average(s => s.Tuition);
        Console.WriteLine($"\nAverage tuition: {averageTuition}");

        
        var highestTuition = studentList.Max(s => s.Tuition);
        foreach (var student in studentList)
        {
            if (student.Tuition == highestTuition)
            {
                Console.WriteLine($"\nStudent paying the most tuition: {student.StudentName}, Major: {student.Major}, Tuition: {student.Tuition}");
                break; 
            }
        }

       
        Console.WriteLine("\nJoin Student List and GPA List:");
        var joinStudentGPA = studentList.Join(studentGPAList,
                                              student => student.StudentID,
                                              gpa => gpa.StudentID,
                                              (student, gpa) => new { student.StudentName, student.Major, gpa.GPA });
        foreach (var item in joinStudentGPA)
        {
            Console.WriteLine($"Student: {item.StudentName}, Major: {item.Major}, GPA: {item.GPA}");
        }

        
        Console.WriteLine("\nStudents in Game Club:");
        var joinStudentGameClub = studentList.Join(studentClubList.Where(c => c.ClubName == "Game"),
                                                   student => student.StudentID,
                                                   club => club.StudentID,
                                                   (student, club) => student.StudentName);
        foreach (var name in joinStudentGameClub)
        {
            Console.WriteLine($"Student in Game Club: {name}");
        }
    }
}

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
