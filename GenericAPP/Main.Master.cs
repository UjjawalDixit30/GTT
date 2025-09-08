using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GenericAPP
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected HtmlGenericControl PageTitle; 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoginID"] != null)
                {
                    spnRole.InnerText = Convert.ToString(Session["RoleDetails"]);
                    spnDealerName.InnerText = Convert.ToString(Session["DealerName"]);
                    SetPageTitle();
                }
                else
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg(Ex.Message.ToString());
            }
        }

        private void SetPageTitle()
        {
            string currentPage = System.IO.Path.GetFileNameWithoutExtension(Request.Url.AbsolutePath);

            if (!string.IsNullOrEmpty(currentPage))
            {
                currentPage = currentPage.Replace("_", " ");
                currentPage = Regex.Replace(currentPage, "(\\B[A-Z])", " $1");
                PageTitle.InnerText = currentPage;
            }
            else
            {
                PageTitle.InnerText = "Global Travel Telecom"; 
            }
        }
        public void SetCustomPageTitle(string title)
        {
            if (PageTitle != null && !string.IsNullOrWhiteSpace(title))
            {
                PageTitle.InnerText = title;
            }
        }
        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }
    }
}
