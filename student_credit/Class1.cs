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
        private const String _DataBasePath = "C:\\myf\\flatData.txt";
        private const char _DBColDel = '\t';    /* Data base column delimiter */
        private const String _SyllabusPath = "C:\\סילבוסים";
        private String _courseName, _year, _institute, _courseCredit, _courseCreditArea,_link;
        private int _hours, _hoursCredit, _grade,_creditPoints;

        private static ObservableCollection<CoursesCredit> AllCourseCredit;
        public string CourseName { get => _courseName; set => _courseName = value; }
        public string Institute { get => _institute; set => _institute = value; }
        public string CourseCredit { get => _courseCredit; set => _courseCredit = value; }
        public string CourseCreditArea { get => _courseCreditArea; set => _courseCreditArea = value; }
        public int Hours { get => _hours; set => _hours = value; }
        public int HoursCredit { get => _hoursCredit; set => _hoursCredit = value; }
        public String Year { get => _year; set => _year = value; }
        public int Grade { get => _grade; set => _grade = value; }
        public string Link { get => _link; set => _link = value; }
        public int CreditPoints { get => _creditPoints; set => _creditPoints = value; }

        static String CourseToLine(CoursesCredit course)
        {
            return course._courseName+ _DBColDel + course.Institute + _DBColDel + course.Year + _DBColDel
                  +course.Hours + _DBColDel+ course.CreditPoints + _DBColDel +course.Grade + _DBColDel +course.CourseCredit + _DBColDel +
                course.CourseCreditArea + _DBColDel +course.HoursCredit +_DBColDel + course.Link;
        }


        public static void UpdateCourseCreditFile()
        {
            File.WriteAllText(@_DataBasePath, string.Join("\r\n",AllCourseCredit.Select(CourseToLine)), Encoding.Default);
            
        }

       
        enum ColumnNum
        {
            COURSE_NAME,
            INSTITUTE,
            YEAR,
            HOURS,
            CREDIT_POINTS,
            GRADE,
            COURSE_CREDIT,
            COURSE_CREDIT_AREA,
            COURSE_CREDIT_SUB_AREA,
            HOURS_CREDIT,
            LINK,

        }

        public static ObservableCollection<CoursesCredit> GetAllCoursesCredit()
        {
            AllCourseCredit = new ObservableCollection<CoursesCredit>();
            int hours, grades, hoursCredit, creditPoints;
            var fileLines = File.ReadAllLines(@_DataBasePath, Encoding.Default);
            
            var lineLength = fileLines[0].Length;
            var lineNum = fileLines.Length;
            String[] line = new String[lineLength];
            var _linkAdd = "";
            for (var i = 0; i < lineNum; i++)
            {
                fileLines[i].Split(_DBColDel).CopyTo(line, 0);

                Int32.TryParse(line[(int)ColumnNum.HOURS], out hours);
                Int32.TryParse(line[(int)ColumnNum.GRADE], out grades);
                Int32.TryParse(line[(int)ColumnNum.HOURS_CREDIT], out hoursCredit);
                Int32.TryParse(line[(int)ColumnNum.CREDIT_POINTS], out creditPoints);

                if (!line[6].Contains("לא הוכר"))
                    _linkAdd = "C:\\";
                else
                    _linkAdd = "";
                
                AllCourseCredit.Add(new CoursesCredit()
                {
                    
                    CourseName = line[(int)ColumnNum.COURSE_NAME],
                    Institute = line[(int)ColumnNum.INSTITUTE],
                    Year = line[(int)ColumnNum.YEAR],
                    Hours = hours,
                    CreditPoints = creditPoints,
                    Grade = grades,
                    CourseCredit = line[6],
                    CourseCreditArea = line[7],
                    HoursCredit = hoursCredit,
                    Link = _linkAdd,
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