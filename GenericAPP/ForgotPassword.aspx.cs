using GenericAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GenericAPP
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                }
                catch (Exception Ex)
                {
                    ShowPopUpMsg(Ex.Message.ToString());
                }
            }
        }
        protected void btnRecover_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string jsonRequest = "Username=" + username;
                string apiName = "ForgotPassword?";
                string result = cGeneral.fnGetAPICall(apiName, jsonRequest);
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(result);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                            {
                                string Name = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                                string Subject = "Recover Your Password - " + cGeneral.CompanyName;
                                string EmailID = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                                string Username = Convert.ToString(ds.Tables[0].Rows[0]["Username"]);
                                string Password = cGeneral.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["Password"]));
                                string BodyEmail = string.Empty;
                                using (StreamReader reader = new StreamReader(Server.MapPath("~/Design/EmailTemplates/ForgotPassword.html")))
                                {
                                    BodyEmail = reader.ReadToEnd();
                                }
                                BodyEmail = BodyEmail.Replace("{Name}", Name);
                                BodyEmail = BodyEmail.Replace("{Username}", Username);
                                BodyEmail = BodyEmail.Replace("{Password}", Password);
                                BodyEmail = BodyEmail.Replace("{LoginLink}", cGeneral.LoginURL);
                                BodyEmail = BodyEmail.Replace("{CompanyName}", Convert.ToString(cGeneral.CompanyName));
                                cGeneral.SendMail(EmailID, Subject, BodyEmail);
                                ShowPopUpMsg("Password has been sent no your registered email.");
                            }
                            else
                            {
                                ShowPopUpMsg(Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]));
                            }
                        }
                        else
                        {
                            ShowPopUpMsg("Invalid user or no data found.");
                        }
                    }
                    else
                    {
                        ShowPopUpMsg("Invalid user or no data found.");
                    }
                }
                else
                {
                    ShowPopUpMsg("Invalid user or no data found.");
                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg("Something went wrong: " + ex.Message);
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