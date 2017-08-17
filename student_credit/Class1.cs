using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace student_credit
{

    

   
    class CoursesCredit
    {
        /***********************************************
         *          VARIABLE DECLARATIONS                               
         ***********************************************/
        private const String _DataBasePath = "C:\\myf\\flatData.txt";
        private const char _DBColDel = '\t';    /* Data base column delimiter */
        private const String _SyllabusPath = "C:\\סילבוסים";
        private String _courseName, _year, _institute, _courseCredit,_courseCreditSubArea,_link;
        private int _hours, _hoursCredit,_creditPoints;
        private static ObservableCollection<CoursesCredit> AllCourseCredit;
        private Subject _subject;
        private Area _area;

        enum ColumnNum
        {
            COURSE_NAME,
            INSTITUTE,
            YEAR,
            HOURS,
            CREDIT_POINTS,
            SUBJECT,
            COURSE_CREDIT,
            COURSE_CREDIT_AREA,
            COURSE_CREDIT_SUB_AREA,
            HOURS_CREDIT,
            LINK,

        }

        

        /***********************************************
         *            GET/SET FUNCTIONS                             
         ***********************************************/

        public string CourseName { get => _courseName; set => _courseName = value; }
        public string Institute { get => _institute; set => _institute = value; }
        public string CourseCredit { get => _courseCredit; set => _courseCredit = value; }
        public int Hours { get => _hours; set => _hours = value; }
        public int HoursCredit { get => _hoursCredit; set => _hoursCredit = value; }
        public String Year { get => _year; set => _year = value; }
        public string Link { get => _link; set => _link = value; }
        public int CreditPoints { get => _creditPoints; set => _creditPoints = value; }
        public string CourseCreditSubArea { get => _courseCreditSubArea; set => _courseCreditSubArea = value; }
        public Subject Subject { get => _subject; set => _subject = value; }
        public Area Area { get => _area; set => _area = value; }


        /***********************************************
        *         STATIC PUBLIC FUNCTIONS                             
        ***********************************************/

        /// <summary> CourseToLine converts a course object into a string ready to be pushed to the flat data </summary>
        /// <param name="course">A CourseCredit object to convert</param>
        /// <returns>A string delimited properly to be inserted into a flat data</returns>
        static String CourseToLine(CoursesCredit course)
        {
            return course.CourseName + _DBColDel + 
                   course.Institute + _DBColDel + 
                   course.Year + _DBColDel +
                   course.Hours + _DBColDel + 
                   course.CreditPoints + _DBColDel +
                   ((int)course.Subject) + _DBColDel +  
                   course.CourseCredit + _DBColDel +
                   course.Area + _DBColDel +
                   course.CourseCreditSubArea + _DBColDel +
                   course.HoursCredit +_DBColDel + 
                   course.Link;
        }

        

        public static void UpdateCourseCreditFile()
        {
            File.WriteAllText(@_DataBasePath, string.Join("\r\n",AllCourseCredit.Select(CourseToLine)), Encoding.Default);
        }


        



        public static ObservableCollection<CoursesCredit> GetAllCoursesCredit()
        {
            AllCourseCredit = new ObservableCollection<CoursesCredit>();
            
            int hours, hoursCredit, creditPoints,subject,area;
            var fileLines = File.ReadAllLines(@_DataBasePath, Encoding.Default);
            
            var lineLength = fileLines[0].Length;
            var lineNum = fileLines.Length;
            String[] line = new String[lineLength];
            var _linkAdd = "";
            for (var i = 0; i < 15; i++)
            {
                fileLines[i].Split(_DBColDel).CopyTo(line, 0);

                Int32.TryParse(line[(int)ColumnNum.HOURS], out hours);
                Int32.TryParse(line[(int)ColumnNum.HOURS_CREDIT], out hoursCredit);
                Int32.TryParse(line[(int)ColumnNum.CREDIT_POINTS], out creditPoints);
                Int32.TryParse(line[(int)ColumnNum.SUBJECT], out subject);
                Int32.TryParse(line[(int)ColumnNum.COURSE_CREDIT_AREA], out area);
                var str = Directory.GetDirectories("C:\\");
                if (!line[6].Contains("לא הוכר"))
                    _linkAdd = _SyllabusPath + "\\" + line[(int)ColumnNum.INSTITUTE] + "\\" + line[(int)ColumnNum.COURSE_NAME];
                else
                    _linkAdd = "";
                
                AllCourseCredit.Add(new CoursesCredit()
                {
                    
                    CourseName = line[(int)ColumnNum.COURSE_NAME],
                    Institute = line[(int)ColumnNum.INSTITUTE],
                    Year = line[(int)ColumnNum.YEAR],
                    Hours = hours,
                    CreditPoints = creditPoints,
                    Subject = (Subject)subject,
                    CourseCredit = line[(int)ColumnNum.COURSE_CREDIT],
                    Area = (Area)area,
                    CourseCreditSubArea = line[(int)ColumnNum.COURSE_CREDIT_SUB_AREA],
                    HoursCredit = hoursCredit,
                    Link = _linkAdd,
                });
            }
            return AllCourseCredit;
        }
    }

    public enum Subject
    {
        חינוך,
        תנך,
        תושבע,
        מחשבת_ישראל,
        אזרחות,
        אנגלית,
        גיאוגרפיה,
        היסטוריה,
        חינוך_מיוחד,
        לימודי_ארץ,
        לשון_עברית,
        מדעי_הטבע,
        מתמטיקה,
        מדעי_המחשב,
        ספרות,
        תקשורת,
    }

    public enum Area
    {
        [Description("לימודי חובה")]
        לימודי_חובה,
        [Description("לימודי בחירה")]
        לימודי_בחירה,
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