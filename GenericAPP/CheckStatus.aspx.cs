using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using GenericAPP.Models;

namespace GenericAPP
{
    public partial class CheckStatus : System.Web.UI.Page
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
        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSerialNumber.Text))
                {
                    ShowPopUpMsg("Please enter serial number.");
                    return;
                }
                var result = "";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string SerialNumber = Convert.ToString(txtSerialNumber.Text);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(cGeneral.BaseURL + "CheckStatus?LoginID=" + LoginID + "&SerialNumber=" + SerialNumber);
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
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                                {
                                    spnSERIALNUMBER.InnerText = SerialNumber;
                                    spnSTATUS.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                                    spnDEALERNAME.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["DealerName"]);
                                    spnNETWORKNAME.InnerText = Convert.ToString(ds.Tables[0].Rows[0]["NetworkName"]);
                                    DivMain.Visible = true;
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        RepeaterPlanList.DataSource = ds.Tables[1];
                                        RepeaterPlanList.DataBind();
                                        RepeaterPlanList.Visible = true;
                                        lblDataFound.Text = "";
                                        plandiv.Visible = true;
                                    }
                                    else
                                    {
                                        RepeaterPlanList.DataSource = null;
                                        RepeaterPlanList.DataBind();
                                        RepeaterPlanList.Visible = false;
                                        lblDataFound.Text = "No data found !!!";
                                        plandiv.Visible = false;
                                    }
                                }
                                else
                                {
                                    RepeaterPlanList.DataSource = null;
                                    RepeaterPlanList.DataBind();
                                    RepeaterPlanList.Visible = false;
                                    lblDataFound.Text = "No data found !!!";
                                    plandiv.Visible = false;
                                }
                            }
                            else
                            {
                                RepeaterPlanList.DataSource = null;
                                RepeaterPlanList.DataBind();
                                RepeaterPlanList.Visible = false;
                                lblDataFound.Text = "No data found !!!";
                                plandiv.Visible = false;
                            }
                        }
                        else
                        {
                            RepeaterPlanList.DataSource = null;
                            RepeaterPlanList.DataBind();
                            RepeaterPlanList.Visible = false;
                            lblDataFound.Text = "No data found !!!";
                            plandiv.Visible = false;
                        }
                    }
                    else
                    {
                        RepeaterPlanList.DataSource = null;
                        RepeaterPlanList.DataBind();
                        RepeaterPlanList.Visible = false;
                        lblDataFound.Text = "No data found !!!";
                        plandiv.Visible = false;
                    }
                }
                else
                {
                    RepeaterPlanList.DataSource = null;
                    RepeaterPlanList.DataBind();
                    RepeaterPlanList.Visible = false;
                    lblDataFound.Text = "No data found !!!";
                    plandiv.Visible = false;
                }
            }
            catch (Exception ex)
            {
                RepeaterPlanList.DataSource = null;
                RepeaterPlanList.DataBind();
                RepeaterPlanList.Visible = false;
                lblDataFound.Text = "No data found," + ex.Message.ToString() + " !!!";
                plandiv.Visible = false;
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