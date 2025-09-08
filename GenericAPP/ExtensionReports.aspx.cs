using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GenericAPP.Models;

namespace GenericAPP
{
    public partial class ExtensionReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["LoginID"] == null)
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var result = "";
            try
            {
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string toDate = todate.Text != "" ? Convert.ToDateTime(todate.Text).ToString("yyyy-MM-dd") : "";
                string fromDate = fromdate.Text != "" ? Convert.ToDateTime(fromdate.Text).ToString("yyyy-MM-dd") : "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(cGeneral.BaseURL + "ExtensionReport?LoginID=" + LoginID + "&FromDate=" + fromDate + "&ToDate=" + toDate);
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
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            rptActivationReport.DataSource = ds.Tables[1];
                            rptActivationReport.DataBind();
                            rptActivationReport.Visible = true;
                            lblDataFound.Text = "";
                            ReportDiv.Visible = true;
                        }
                        else
                        {
                            rptActivationReport.DataSource = null;
                            rptActivationReport.DataBind();
                            rptActivationReport.Visible = false;
                            lblDataFound.Text = "No data found !!!";
                            ReportDiv.Visible = false;
                        }
                    }
                    else
                    {
                        rptActivationReport.DataSource = null;
                        rptActivationReport.DataBind();
                        rptActivationReport.Visible = false;
                        lblDataFound.Text = "No data found !!!";
                        ReportDiv.Visible = false;
                    }
                }
                else
                {
                    rptActivationReport.DataSource = null;
                    rptActivationReport.DataBind();
                    rptActivationReport.Visible = false;
                    lblDataFound.Text = "No data found !!!";
                    ReportDiv.Visible = false;
                }
            }
            catch (Exception ex)
            {
                rptActivationReport.DataSource = null;
                rptActivationReport.DataBind();
                rptActivationReport.Visible = false;
                lblDataFound.Text = "No data found," + ex.Message.ToString() + " !!!";
                ReportDiv.Visible = false;
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActivationReport.aspx", false);
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