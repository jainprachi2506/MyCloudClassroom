using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyCloudClassroom.Classes
{
    public enum UserStatus
    {
        NotValidated, Active, Deleted
    }

    public enum UserType
    {
        Student, Teacher
    }

    public class MyUser
    {

        public int userId;
        public String username;
        private String password;
        private String name;
        private String contactNo;
        public UserType usertype;
        private UserStatus status;

        public MyUser()
        {

        }

        public MyUser(String _username, String _password, String _name, String _contactno, UserType _usertype)
        {
            this.username = _username;
            this.password = _password;
            this.name = _name;
            this.contactNo = _contactno;
            this.usertype = _usertype;

        }

       public MyUser(int _userId, String _username, String _name, String _contactno, UserType _usertype, UserStatus _status)
        {
            this.userId=_userId;
            this.username = _username;
            this.status = _status;
            this.name = _name;
            this.contactNo = _contactno;
            this.usertype = _usertype;

        }

        public int addUser()
        {
            dbManager db = new dbManager();
            
            string sqlCommand;
            MySqlParameter [] param = new MySqlParameter[6];
            
             param[0] =  new MySqlParameter( "@Username", this.username);
             param[1] = new MySqlParameter("@Psswrd", this.password);
             param[2] = new MySqlParameter("@NameUser", this.name);
             param[3] = new MySqlParameter("@ContactNo", this.contactNo);
             param[4] = new MySqlParameter("@UserType", (int)this.usertype);
             param[5] = new MySqlParameter("@UserStatus", (int)this.status);


            sqlCommand="insert into user (Username,Psswrd,NameUser,ContactNo,UserType,UserStatus) "+
                   " values (@Username,@Psswrd,@NameUser,@ContactNo,@UserType,@UserStatus)";

            int sqlRet = db.executeDDL(sqlCommand, param);
            try
            {
                //Mailer.Send("priyanka.gupta.email", "aspirations", this.username, "Test Message", "Hi");
            }
            catch (Exception ex)
            { }
            return sqlRet;
        }

        public static MyUser getUser(string username, string password)
        {
            dbManager db = new dbManager();
            MyUser usr=null;

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[2];

            param[0] = new MySqlParameter("@Username", username);
            param[1] = new MySqlParameter("@Psswrd", password);

            sqlCommand = "select * from user where Username=@Username and Psswrd=@Psswrd";

            DataTable ds = db.fetchRows(sqlCommand, param);

            foreach (DataRow dr in ds.Rows)
            {
                usr = new MyUser((int)dr["userid"], dr["Username"].ToString(),
                    dr["nameuser"].ToString(), dr["contactno"].ToString(),
                    (UserType)Enum.Parse(typeof(UserType), dr["usertype"].ToString()),
                    (UserStatus)Enum.Parse(typeof(UserStatus), dr["userstatus"].ToString()));
            }

            return usr;
        }

        public static int registerInCourse(int _userid,int _courseid)
        {
            dbManager db = new dbManager();

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[2];

            param[0] = new MySqlParameter("@UserID", _userid);
            param[1] = new MySqlParameter("@CourseId", _courseid);
            
            sqlCommand = "insert into UserCourse (UserId,CourseId) " +
                   " values (@UserID,@CourseId)";

            int sqlRet = db.executeDDL(sqlCommand, param);
            try
            {
                //Mailer.Send("priyanka.gupta.email", "aspirations", this.username, "Test Message", "Hi");
            }
            catch (Exception ex)
            { }
            return sqlRet;
        }

        public static MyUser getUser(int userid)
        {
            dbManager db = new dbManager();
            MyUser usr = null;

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[1];

            param[0] = new MySqlParameter("@userid", userid);
            
            sqlCommand = "select * from user where Userid=@userid";

            DataTable ds = db.fetchRows(sqlCommand, param);

            foreach (DataRow dr in ds.Rows)
            {
                usr = new MyUser((int)dr["userid"], dr["Username"].ToString(),
                    dr["nameuser"].ToString(), dr["contactno"].ToString(),
                    (UserType)Enum.Parse(typeof(UserType), dr["usertype"].ToString()),
                    (UserStatus)Enum.Parse(typeof(UserStatus), dr["userstatus"].ToString()));
            }

            return usr;
        }
    }
}