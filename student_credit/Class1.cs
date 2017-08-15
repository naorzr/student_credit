using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_credit
{
    class CoursesCredit
    {
        String courseName, year, institute, lecturer, courseCredit, courseCreditArea,link;
        int hours, hoursCredit, grade;
        private static ObservableCollection<CoursesCredit> AllCourseCredit;
        public string CourseName { get => courseName; set => courseName = value; }
        public string Institute { get => institute; set => institute = value; }
        public string Lecturer { get => lecturer; set => lecturer = value; }
        public string CourseCredit { get => courseCredit; set => courseCredit = value; }
        public string CourseCreditArea { get => courseCreditArea; set => courseCreditArea = value; }
        public int Hours { get => hours; set => hours = value; }
        public int HoursCredit { get => hoursCredit; set => hoursCredit = value; }
        public String Year { get => year; set => year = value; }
        public int Grade { get => grade; set => grade = value; }
        public string Link { get => link; set => link = value; }

        static String CourseToLine(CoursesCredit course)
        {
            return course.courseName+ "\t" + course.Institute + "\t" + course.Year + "\t"
                + course.Lecturer + "\t" +course.Hours + "\t" +course.Grade + "\t" +course.CourseCredit + "\t" +
                course.CourseCreditArea + "\t" +course.HoursCredit +"\t" + course.Link;
        }


        public static void UpdateCourseCreditFile()
        {
            File.WriteAllText(@"C:\myf\flatData.txt", string.Join("\r\n",AllCourseCredit.Select(CourseToLine)), Encoding.Default);
            
        }

       
        public static ObservableCollection<CoursesCredit> GetAllCoursesCredit()
        {
            AllCourseCredit = new ObservableCollection<CoursesCredit>();
            
            var fileLines = File.ReadAllLines(@"C:\myf\flatData.txt", Encoding.Default);
            
            var lineLength = fileLines[0].Length;
            var lineNum = fileLines.Length;
            String[] line = new String[lineLength];
            
            for (var i = 0; i < lineNum; i++)
            {
                fileLines[i].Split('\t').CopyTo(line, 0);
                AllCourseCredit.Add(new CoursesCredit()
                {
                    
                    CourseName = line[0],
                    Institute = line[1],
                    Year = line[2],
                    Lecturer = line[3],
                    Hours = Int32.Parse("  3 \t"),
                    Grade = Int32.Parse("4"),
                    CourseCredit = line[6],
                    CourseCreditArea = line[7],
                    HoursCredit = Int32.Parse("5"),
                    Link = "C:\\סילבוסים\\" + line[1] + "\\" + line[0],
                });
            }
            return AllCourseCredit;
        }
    }
}

/*
 * 
 * Hours = Int32.Parse(line[3]),
                    Grade = Int32.Parse(line[4]),
                    CourseCredit = line[5],
                    CourseCreditArea = line[6],
                    HoursCredit = Int32.Parse(line[7])
*/