using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using MyCloudClassroom.Classes;

namespace MyCloudClassroom.Classes
{
    public class Course
    {
            public int courseId;
            public string courseName;
            private DateTime startDate;
            private DateTime endDate;
            private string classOnDays;
            private TimeSpan classStartTime;
            private TimeSpan classEndTime;

            public Course()
            { }


            public Course(string _courseName, DateTime _startDate,
            DateTime _endDate,
            string _classOnDays,
            TimeSpan _classStartTime,
            TimeSpan _classEndTime)
            {
                this.courseName = _courseName;
                this.startDate=_startDate;
                this.endDate = _endDate;
                this.classOnDays = _classOnDays;
                this.classStartTime = _classStartTime;
                this.classEndTime = _classEndTime;
            }

            public Course(int _courseid,string _courseName, DateTime _startDate,
            DateTime _endDate,
            string _classOnDays,
            TimeSpan _classStartTime,
            TimeSpan _classEndTime)
            {
                this.courseId = _courseid;
                this.courseName = _courseName;
                this.startDate = _startDate;
                this.endDate = _endDate;
                this.classOnDays = _classOnDays;
                this.classStartTime = _classStartTime;
                this.classEndTime = _classEndTime;
            }

            public int addCourse()
            {
                dbManager db = new dbManager();

                string sqlCommand;
                MySqlParameter[] param = new MySqlParameter[6];

                param[0] = new MySqlParameter("@courseName", this.courseName);
                param[1] = new MySqlParameter("@startDate", this.startDate);
                param[2] = new MySqlParameter("@endDate", this.endDate);
                param[3] = new MySqlParameter("@classOnDays", this.classOnDays);
                param[4] = new MySqlParameter("@classStartTime", this.classStartTime);
                param[5] = new MySqlParameter("@classEndTime", this.classEndTime);


                sqlCommand = "insert into Course (coursename,startdate,enddate,"+
                                "classondays,classstarttime,classendtime) "+
                       " values (@courseName,@startDate,@endDate"+
                                ",@classOnDays,@classStartTime,@classEndTime)";

                int sqlRet = db.executeDDL(sqlCommand, param);
                
                return sqlRet;
            }

        public static Course fetchCoursebyId(int _courseid)
            {
                dbManager db = new dbManager();
                Course cs = null;

                string sqlCommand;
                MySqlParameter[] param = new MySqlParameter[1];

                param[0] = new MySqlParameter("@courseid", _courseid);
                
                sqlCommand = "select * from Course where courseid=@courseid";

                DataTable ds = db.fetchRows(sqlCommand, param);
                
                int i = 0;

                foreach (DataRow dr in ds.Rows)
                {
                    cs = new Course(Convert.ToInt32(dr["courseid"].ToString()),
                                        dr["coursename"].ToString(), 
                                        (DateTime)dr["startdate"],
                                        (DateTime)dr["enddate"],
                                        dr["classondays"].ToString(),
                                        (TimeSpan)dr["classstarttime"],
                                        (TimeSpan)dr["classendtime"]);
                    i++;
                }

                return cs;
            }

        public static MyUser[] fetchUsersforCourseid(int _courseid)
        {
            dbManager db = new dbManager();
            MyUser[] users = null;

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[1];

            param[0] = new MySqlParameter("@courseid", _courseid);

            sqlCommand = "select User.* from UserCourse join User on UserCourse.Userid=user.Userid "+
                        " where Courseidy=@courseid";

            DataTable ds = db.fetchRows(sqlCommand, param);
            users = new MyUser[ds.Rows.Count];
            int i = 0;

            foreach (DataRow dr in ds.Rows)
            {
                users[i] = new MyUser((int)dr["userid"],
                                        dr["username"].ToString(),dr["nameUser"].ToString(),
                                        dr["contactno"].ToString(),
                                        (UserType)Enum.Parse(typeof(UserType), dr["usertype"].ToString()),
                                        (UserStatus)Enum.Parse(typeof(UserStatus), dr["UserStatus"].ToString()));
                i++;
            }

            return users;
        }

        public static int fetchUploadedCourse(string _coursename)
        {
            dbManager db = new dbManager();
            int csid = 0;

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[1];

            param[0] = new MySqlParameter("@coursename", _coursename);

            sqlCommand = "select courseid from Course where coursename like @coursename";

            DataTable ds = db.fetchRows(sqlCommand, param);

            foreach (DataRow dr in ds.Rows)
            {
                csid = (int)dr["courseid"];
            }

            return csid;
        }

        public int addTopicARN(string _topicarn)
        {
            dbManager db = new dbManager();

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[2];

            param[0] = new MySqlParameter("@courseid", this.courseId);
            param[1] = new MySqlParameter("@topicarn", _topicarn);

            sqlCommand = "insert into SNSTopicArn (TopicArn,CourseId) " +
                   " values (@topicarn,@courseid)";

            int sqlRet = db.executeDDL(sqlCommand, param);

            return sqlRet;
        }

        public static string fetchTopicARN(int _courseId)
        {
            dbManager db = new dbManager();
            string topic = "";

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[1];

            param[0] = new MySqlParameter("@courseid", _courseId);

            sqlCommand = "select topicarn from SNSTopicArn where courseid = @courseid";

            DataTable ds = db.fetchRows(sqlCommand, param);

            foreach (DataRow dr in ds.Rows)
            {
                topic = dr["topicarn"].ToString();
            }

            return topic;
        }
    
        
    }
}