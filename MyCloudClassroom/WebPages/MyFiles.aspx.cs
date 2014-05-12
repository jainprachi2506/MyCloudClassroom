using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCloudClassroom.Classes;

namespace MyCloudClassroom.WebPages
{
    public partial class MyFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          if(!IsPostBack)
          {
              if (Session["UserId"] != null)
              {
                  MyUser user = MyUser.getUser(Convert.ToInt32(Session["UserId"].ToString()));
                  if (user.usertype == UserType.Student)
                  {
                      Response.Redirect("StudentFiles.aspx");
                  }
              }
          }
        }

        

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument); 
            GridViewRow selectedRow = GridView1.Rows[index];
            TableCell courseidCell = selectedRow.Cells[0];
            int courseid = Convert.ToInt32(courseidCell.Text);
            TableCell resourceidCell = selectedRow.Cells[2];
            int resourceid = Convert.ToInt32(resourceidCell.Text);
            TableCell resourcefileCell = selectedRow.Cells[4];
            string resourcefile = resourcefileCell.Text;
            string path = HttpContext.Current.Request.PhysicalApplicationPath + "output\\Resource_" + resourceid.ToString()+ "\\";
            AWSClass.ReadObjectData("course-"+courseid,
                                    "Resource_" + resourceid.ToString() + System.IO.Path.GetExtension(resourcefile),
                                    path);
            base.Context.Response.AppendHeader("content-disposition",
        "attachment; filename=" + resourcefile);
            base.Context.Response.ContentType = System.Web.MimeMapping.GetMimeMapping(resourcefile);
            base.Context.Response.WriteFile(path + resourcefile);
            base.Context.Response.End();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count >= 4)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[4].Visible = false;
            }

            MyUser myuser = MyUser.getUser(Convert.ToInt32(Session["UserId"].ToString()));

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drpGrades = (DropDownList)e.Row.Cells[6].FindControl("drpGrades");
                drpGrades.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Grade").ToString();
                
                TextBox txtComments = (TextBox)e.Row.Cells[7].FindControl("txtComments");
                txtComments.Text = DataBinder.Eval(e.Row.DataItem, "Comments").ToString();
                
                if (e.Row.Cells[3].Text == ResourceType.Assignments.ToString())
                {
                    drpGrades.Enabled = true;
                    txtComments.Enabled = true;
                }
                else
                {
                    drpGrades.Enabled = false;
                    txtComments.Enabled = false;
                }
            }
        }

        protected void drpGrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList thisDropdown = (DropDownList)sender;
            GridViewRow thisGridViewRow = (GridViewRow)thisDropdown.Parent.Parent;
            int resourceid = Convert.ToInt32(thisGridViewRow.Cells[2].Text);
            Resource.updateGrade(resourceid, thisDropdown.SelectedValue.ToString());
        }

        protected void txtComments_TextChanged(object sender, EventArgs e)
        {
            TextBox thisTextBox = (TextBox)sender;
            GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
            int resourceid = Convert.ToInt32(thisGridViewRow.Cells[2].Text);
            Resource.updateComments(resourceid, thisTextBox.Text);
           
        }
    }
}