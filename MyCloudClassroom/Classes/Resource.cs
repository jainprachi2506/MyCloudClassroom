using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyCloudClassroom.Classes
{
    public enum ResourceType
    {Lectures,Assignments,References}

    public class Resource
    {
            public int resourceId;
            public ResourceType resourceType;
            private string resourceName;
            private int uploadedBy;
            private DateTime uploadedAt;
            private int courseId;
            private string resourceFile;

            public Resource()
            { }


            public Resource(ResourceType _resourceType, string _resourceName, int _uploadedBy,
                                DateTime _uploadedAt, int _courseId, string _resourceFile)
            {
                this.resourceType = _resourceType;
                this.resourceName = _resourceName;
                this.uploadedBy = _uploadedBy;
                this.uploadedAt = _uploadedAt;
                this.courseId = _courseId;
                this.resourceFile = _resourceFile;
            }

            public Resource( int _resourceId, ResourceType _resourceType,
                            string _resourceName,int _uploadedBy,
                            DateTime _uploadedAt,int _courseId,
                            string _resourceFile) 
            {
                this.resourceId = _resourceId;
                this.resourceType = _resourceType;
                this.resourceName = _resourceName;
                this.uploadedBy = _uploadedBy;
                this.uploadedAt = _uploadedAt;
                this.courseId = _courseId;
                this.resourceFile = _resourceFile;
            }

            public int addResource()
            {
                dbManager db = new dbManager();

                string sqlCommand;
                MySqlParameter[] param = new MySqlParameter[6];

                param[0] = new MySqlParameter("@resourceType", this.resourceType);
                param[1] = new MySqlParameter("@resourcename", this.resourceName);
                param[2] = new MySqlParameter("@uploadedby", this.uploadedBy);
                param[3] = new MySqlParameter("@uploadedat", this.uploadedAt);
                param[4] = new MySqlParameter("@courseid", this.courseId);
                param[5] = new MySqlParameter("@resourcefile", this.resourceFile);


                sqlCommand = "insert into Resource (ResourceType,ResourceName,UploadedBy,"+
                                "UploadedAt,CourseId,ResourceFile) "+
                       " values (@resourceType,@resourcename,@uploadedby"+
                                ",@uploadedat,@courseid,@resourcefile)";

                int sqlRet = db.executeDDL(sqlCommand, param);

                return sqlRet;
            }

        public static Resource[] fetchResourcesbyCourse(int _courseid)
            {
                dbManager db = new dbManager();
                Resource[] rsc = null;

                string sqlCommand;
                MySqlParameter[] param = new MySqlParameter[1];

                param[0] = new MySqlParameter("@courseid", _courseid);
                
                sqlCommand = "select * from Resource where courseid=@courseid";

                DataTable ds = db.fetchRows(sqlCommand, param);
                rsc = new Resource[ds.Rows.Count];
                int i = 0;

                foreach (DataRow dr in ds.Rows)
                {
                    rsc[i] = new Resource((int)dr["resourceid"],
                                        (ResourceType)Enum.Parse(typeof(ResourceType),dr["resourcetype"].ToString()),
                                        dr["resourcename"].ToString(), 
                                        (int)dr["uploadedby"],
                                        (DateTime)dr["uploadedat"],
                                        (int)dr["Courseid"],
                                        dr["filename"].ToString());
                    i++;
                }

                return rsc;
            }

        public static Resource[] fetchResourcesbyUser(int _userid)
        {
            dbManager db = new dbManager();
            Resource[] rsc = null;

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[1];

            param[0] = new MySqlParameter("@userid", _userid);

            sqlCommand = "select * from resource where uploadedby=@userid";

            DataTable ds = db.fetchRows(sqlCommand, param);
            rsc = new Resource[ds.Rows.Count];
            int i = 0;

            foreach (DataRow dr in ds.Rows)
            {
                rsc[i] = new Resource((int)dr["resourceid"],
                                    (ResourceType)Enum.Parse(typeof(ResourceType), dr["resourcetype"].ToString()),
                                    dr["resourcename"].ToString(),
                                    (int)dr["uploadedby"],
                                    (DateTime)dr["uploadedat"],
                                    (int)dr["Courseid"],
                                    dr["filename"].ToString());
                i++;
            }

            return rsc;
        }

        public static int fetchUploadedResource(int _uploadedby)
        {
            dbManager db = new dbManager();
            int rsid = 0;

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[1];

            param[0] = new MySqlParameter("@uploadedby", _uploadedby);
            
            sqlCommand = "select max(resourceid) as resourceid from resource where uploadedby=@uploadedby";

            DataTable ds = db.fetchRows(sqlCommand, param);
            
            foreach (DataRow dr in ds.Rows)
            {
                rsid = (int)dr["resourceid"];
            }

            db = new dbManager();
            sqlCommand = "INSERT INTO mccdb.resourcefeedback(resourceid) values (@resourceid)";
            param = new MySqlParameter[1];
            param[0] = new MySqlParameter("@resourceid", rsid);
            db.executeDDL(sqlCommand, param);

            return rsid;
        }

        public static int updateComments(int _resourceid, string _comments)
        {
            dbManager db = new dbManager();

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[2];

            param[0] = new MySqlParameter("@resourceId", _resourceid);
            param[1] = new MySqlParameter("@comments", _comments);

            sqlCommand = "update resourcefeedback set comments=@comments where resourceid=@resourceId";

            int sqlRet = db.executeDDL(sqlCommand, param);
            return sqlRet;
        }

        public static int insertComments(int _resourceid, string _comments)
        {
            dbManager db = new dbManager();

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[2];

            param[0] = new MySqlParameter("@resourceId", _resourceid);
            param[1] = new MySqlParameter("@comments", _comments);

            sqlCommand = "insert into resourcefeedback(resourceid,comments) values (@resourceId,@comments)";

            int sqlRet = db.executeDDL(sqlCommand, param);
            return sqlRet;
        }

        public static int updateGrade(int _resourceid, string _grade)
        {
            dbManager db = new dbManager();

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[2];

            param[0] = new MySqlParameter("@resourceId", _resourceid);
            param[1] = new MySqlParameter("@grade", _grade);

            sqlCommand = "update resourcefeedback set grade=@grade where resourceid=@resourceId";

            int sqlRet = db.executeDDL(sqlCommand, param);
            return sqlRet;
        }

        public static int insertGrade(int _resourceid, string _grade)
        {
            dbManager db = new dbManager();

            string sqlCommand;
            MySqlParameter[] param = new MySqlParameter[2];

            param[0] = new MySqlParameter("@resourceId", _resourceid);
            param[1] = new MySqlParameter("@grade", _grade);

            sqlCommand = "insert into resourcefeedback(resourceid,grade) values (@resourceId,@grade)";

            int sqlRet = db.executeDDL(sqlCommand, param);
            return sqlRet;
        }
    }
}