using GenericAPP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GenericAPP
{
    public partial class PaymentHistoryReport : System.Web.UI.Page
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
                        GetDealerList();
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
        public void GetDealerList()
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
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                ddlDealer.DataSource = ds.Tables[1];
                                ddlDealer.DataTextField = "DealerName";
                                ddlDealer.DataValueField = "DealerID";
                                ddlDealer.DataBind();
                                ddlDealer.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                            else
                            {
                                ddlDealer.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                        }
                        else
                        {
                            ddlDealer.Items.Insert(0, new ListItem("---Select---", "0"));
                        }
                    }
                    else
                    {
                        ddlDealer.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                }
                else
                {
                    ddlDealer.Items.Insert(0, new ListItem("---Select---", "0"));
                }
            }
            catch (Exception Ex)
            {
                ddlDealer.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var result = "";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string dealerID = Convert.ToString(ddlDealer.SelectedValue);
                string fromDate = fromdate.Text;
                string toDate = todate.Text;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(cGeneral.BaseURL + "PaymentHistory?LoginID=" + LoginID + "&DealerID=" + dealerID + "&FromDate=" + fromDate + "&ToDate=" + toDate);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (Convert.ToString(httpResponse.StatusCode) == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                    System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            rptPaymentHistory.DataSource = ds.Tables[1];
                            rptPaymentHistory.DataBind();
                            pnlRepeaterWrapper.Visible = true;
                            txtSearch.Visible = true;
                        }
                        else
                        {
                            rptPaymentHistory.DataSource = null;
                            rptPaymentHistory.DataBind();
                            ShowPopUpMsg("No data found.");
                            rptPaymentHistory.Visible = false;
                        }
                    }
                    else
                    {
                        rptPaymentHistory.DataSource = null;
                        rptPaymentHistory.DataBind();
                        rptPaymentHistory.Visible = false;
                    }
                }
                else
                {
                    rptPaymentHistory.DataSource = null;
                    rptPaymentHistory.DataBind();
                    rptPaymentHistory.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg(ex.Message.ToString());
                rptPaymentHistory.DataSource = null;
                rptPaymentHistory.DataBind();
                rptPaymentHistory.Visible = false;
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