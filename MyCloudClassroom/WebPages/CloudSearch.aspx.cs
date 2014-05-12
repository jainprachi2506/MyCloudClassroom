using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyCloudClassroom.WebPages
{
    public partial class CloudSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {   
            base.Context.Response.Redirect("https://co​nsole.aws.​amazon.com​/cloudsear​ch/home?&r​egion=us-w​est-2#sear​ch,filesea​rch");
            base.Context.Response.End();
        }
    }
}