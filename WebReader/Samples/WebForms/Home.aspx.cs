using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebReader.Samples
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var date = DateTime.Now;
            lblMessage.Text = string.Format("The current server time is {0}, {1}", date.ToString("dd MMMM yyyy"), date.ToString("HH:mm:ss"));
        }

        protected void btnName_Click(object sender, EventArgs e)
        {
            lblName.Text = "Welcome " + txtName.Text;
            lblName.Visible = true;
        }
    }
}