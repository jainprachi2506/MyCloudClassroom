using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCloudClassroom.Classes;

namespace MyCloudClassroom.WebPages
{
    public partial class CourseRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {   int userid=Convert.ToInt32(Session["UserId"].ToString());
            int courseid=Convert.ToInt32(drpCourse.SelectedValue.ToString());
            MyUser.registerInCourse(userid,courseid);
            string topicarn = Course.fetchTopicARN(courseid);
            MyUser user= MyUser.getUser(userid);
            AWSClass.subscribeToTopic(topicarn, user.username);
            lblSuccess.Text = "You have been registered. Please check your email and subscribe to the notification service.";
            lblSuccess.Visible = true;
            
        }
    }
}