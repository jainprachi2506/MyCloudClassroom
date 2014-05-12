using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCloudClassroom.Classes;
using MySql.Data.MySqlClient;


namespace MyCloudClassroom.WebPages
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          if(!IsPostBack)
          { lblSuccess.Visible = false; }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int courseid = Convert.ToInt32(drpCourse.SelectedValue.ToString());
            DateTime UploadedAt = DateTime.UtcNow;
            if (fileResource.HasFile)
            {
                Resource rc = new Resource((ResourceType)Enum.Parse(typeof(ResourceType), drpResourceType.SelectedValue),
                                            txtResourceName.Text,
                                            (int)Session["UserId"],
                                            UploadedAt,
                                            courseid,
                                            fileResource.FileName);
                int sqlret = rc.addResource();

                rc.resourceId = Resource.fetchUploadedResource((int)Session["UserId"]);


                fileResource.SaveAs(HttpContext.Current.Request.PhysicalApplicationPath +
                    "\\input\\Resource_" + rc.resourceId.ToString() +
                                    System.IO.Path.GetExtension(fileResource.FileName));

                ContentType = System.Web.MimeMapping.GetMimeMapping(fileResource.FileName);

                AWSClass.UploadToS3("course-" + courseid.ToString(),
                             "Resource_" + rc.resourceId.ToString() + System.IO.Path.GetExtension(fileResource.FileName),
                             HttpContext.Current.Request.PhysicalApplicationPath + "\\input\\Resource_" + rc.resourceId.ToString() + System.IO.Path.GetExtension(fileResource.FileName),
                             ContentType,
                             System.IO.Path.GetFileName(fileResource.FileName));

                Course course = Course.fetchCoursebyId(courseid);
                if (rc.resourceType == ResourceType.Lectures || rc.resourceType == ResourceType.References)
                {
                    string topicarn = Course.fetchTopicARN(courseid);
                    AWSClass.publishToTopic(topicarn, "New File Uploaded on Classroom",
                        "Hi User!<p>A new document '" + txtResourceName.Text + "' has been uploaded for the course " + course.courseName + ". Please visit the website to download the document.<p>Thanks!<p>Your Classroom");
                }
                lblSuccess.Text = "File uploaded successfully.";
                lblSuccess.Visible = true;
                base.Context.Response.Redirect("MyFiles.aspx", false);
            }
            else
            {
                lblSuccess.Text = "No file found.";
                lblSuccess.Visible = true;
            }                          
        }
    }
}