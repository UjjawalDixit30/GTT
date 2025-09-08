using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GenericAPP.Models;
using Newtonsoft.Json;
using System.IO;

namespace GenericAPP
{
    public partial class LoginHistoryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["LoginID"] != null)
                    {
                        if (Session["Message"] != null)
                        {
                            ShowPopUpMsg(Convert.ToString(Session["Message"]));
                            Session["Message"] = null;
                        }
                        BindDealerDropdown();
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
        }
        private void BindDealerDropdown()
        {
            try
            {
                var result = "";
                int DealerID = 0;
                int ActionType = 6;
                string API = "GetDealer?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string Data = "LoginID=" + LoginID + "&ActionType=" + ActionType + "&DealerID=" + DealerID;
                result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    ddlDealer.DataSource = ds.Tables[1];
                    ddlDealer.DataTextField = "DealerName";
                    ddlDealer.DataValueField = "DealerID";
                    ddlDealer.DataBind();
                    ddlDealer.Items.Insert(0, new ListItem("--All--", "0"));
                }
                else
                {
                    ddlDealer.Items.Clear();
                    ddlDealer.Items.Add(new ListItem("--All--", "0"));
                }
            }
            catch (Exception ex)
            {
                ddlDealer.Items.Clear();
                ddlDealer.Items.Add(new ListItem("--All--", "0"));
                ddlDealer.Items.Add(new ListItem("Error: " + ex.Message, "-1"));
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                string API = "GetLoginDetails?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
                int ActionType = DealerID == 0 ? 1 : 2;
                string fromDate = fromdate.Text;
                string toDate = todate.Text;
                if (string.IsNullOrWhiteSpace(fromDate))
                    fromDate = "1990-01-01";
                else
                    fromDate += " 00:00:00";
                string data = $"LoginID={LoginID}&DealerID={DealerID}&ActionType={ActionType}&FromDate={fromDate}";
                if (!string.IsNullOrWhiteSpace(toDate))
                    toDate += " 23:59:59";
                result = cGeneral.fnGetAPICall(API, data);
                System.Data.DataSet ds = JsonConvert.DeserializeObject<System.Data.DataSet>(result);
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    rptLoginHistory.DataSource = ds.Tables[1];
                    rptLoginHistory.DataBind();
                    lblNoRecords.Visible = false;
                }
                else
                {
                    rptLoginHistory.DataSource = null;
                    rptLoginHistory.DataBind();
                    ShowPopUpMsg("No data found.");
                }
            }
            catch (Exception ex)
            {
                rptLoginHistory.DataSource = null;
                rptLoginHistory.DataBind();
                ShowPopUpMsg("No data found, please try again. " + ex.Message.ToString());
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