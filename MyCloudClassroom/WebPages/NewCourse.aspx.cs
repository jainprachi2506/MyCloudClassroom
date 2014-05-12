using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCloudClassroom.Classes;

namespace MyCloudClassroom.WebPages
{
    public partial class NewCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string classondays = "";
            for (int i = 0; i < chkDays.Items.Count; i++)
            {
                if (chkDays.Items[i].Selected)
                    classondays += chkDays.Items[i].Value.ToString() + " ";
            }
            classondays.TrimEnd();
            Course course = new Course(txtCourseName.Text, calStartDate.SelectedDate, calEndDate.SelectedDate,
                    classondays, new TimeSpan(Convert.ToInt32(txtStartHH.Text), Convert.ToInt32(txtStartMM.Text), 0),
                    new TimeSpan(Convert.ToInt32(txtEndHH.Text), Convert.ToInt32(txtEndMM.Text), 0));
            course.addCourse();
            course.courseId = Course.fetchUploadedCourse(txtCourseName.Text);
            AWSClass.PutBucketToS3("course-" + course.courseId.ToString());
            string topicarn = AWSClass.createSNSTopic(txtCourseName.Text.Replace(" ", "_"));
            course.addTopicARN(topicarn);
        }


    }
}