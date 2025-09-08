using GenericAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GenericAPP
{
    public partial class DealerList : System.Web.UI.Page
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
                        GetDealer();
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
        public void GetDealer()
        {
            try
            {
                var result = "";
                Int32 DealerID = 0;
                int ActionType = 1;
                string API = "GetDealer?";
                Int32 LoginID = Convert.ToInt32(Session["LoginID"]);
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
                                RepeaterDealerList.DataSource = ds.Tables[1];
                                RepeaterDealerList.DataBind();
                                RepeaterDealerList.Visible = true;
                                lblDataFound.Text = "";
                                DivMain.Visible = true;
                                controlvisibility();
                            }
                            else
                            {
                                RepeaterDealerList.DataSource = null;
                                RepeaterDealerList.DataBind();
                                RepeaterDealerList.Visible = false;
                                lblDataFound.Text = "No Data Found!!!";
                                DivMain.Visible = false;
                            }
                        }
                        else
                        {
                            RepeaterDealerList.DataSource = null;
                            RepeaterDealerList.DataBind();
                            RepeaterDealerList.Visible = false;
                            lblDataFound.Text = "No Data Found!!!";
                            DivMain.Visible = false;
                        }
                    }
                    else
                    {
                        RepeaterDealerList.DataSource = null;
                        RepeaterDealerList.DataBind();
                        RepeaterDealerList.Visible = false;
                        lblDataFound.Text = "No Data Found!!!";
                        DivMain.Visible = false;
                    }
                }
                else
                {
                    RepeaterDealerList.DataSource = null;
                    RepeaterDealerList.DataBind();
                    RepeaterDealerList.Visible = false;
                    lblDataFound.Text = "No Data Found!!!";
                    DivMain.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                RepeaterDealerList.DataSource = null;
                RepeaterDealerList.DataBind();
                RepeaterDealerList.Visible = false;
                lblDataFound.Text = "No Data Found, "+ Ex.Message.ToString() + "!!!";
                DivMain.Visible = false;
            }
        }
        public void controlvisibility()
        {
            try
            {
                for (int i = 0; i < RepeaterDealerList.Items.Count; i++)
                {
                    Label lblStatus = (Label)RepeaterDealerList.Items[i].FindControl("lblStatus");
                    Label lblIsActive = (Label)RepeaterDealerList.Items[i].FindControl("lblIsActive");
                    if (lblIsActive.Text == "False")
                    {
                        lblStatus.Attributes.Add("class", "badge bg-label-warning me-1");
                    }
                    if (lblIsActive.Text == "True")
                    {
                        lblStatus.Attributes.Add("class", "badge bg-label-success me-1");
                    }
                }
            }
            catch (Exception Ex)
            {
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
        protected void RepeaterDeviceList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                int DealerID = Convert.ToInt32(e.CommandArgument);
                string CommandName = Convert.ToString(e.CommandName);
                if (CommandName == "ViewDealer")
                {
                    string ActionType = cGeneral.Encrypt("V");
                    string DID = cGeneral.Encrypt(Convert.ToString(DealerID));
                    Response.Redirect("AddDealer.aspx?ActionType=" + ActionType + "&DealerID=" + DID, false);
                }
                if (CommandName == "EditDealer")
                {
                    string ActionType = cGeneral.Encrypt("U");
                    string DID = cGeneral.Encrypt(Convert.ToString(DealerID));
                    Response.Redirect("AddDealer.aspx?ActionType=" + ActionType + "&DealerID=" + DID, false);
                }
                if (CommandName == "UpdateStatus")
                {
                    int LoginID = Convert.ToInt32(Session["LoginID"]);
                    UpdateDealerStatus(LoginID, DealerID, 3);
                }
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg(Ex.Message.ToString());
            }
        }
        protected void UpdateDealerStatus(int LoginID, int DealerID, int ActionType)
        {
            try
            {
                string API = "GetDealer?";
                string Data = "LoginID=" + LoginID + "&DealerID=" + DealerID + "&ActionType=" + ActionType;
                string Result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            Session["Message"] = "Status Updated Successfully.";
                            Response.Redirect("DealerList.aspx", false);
                        }
                        else
                        {
                            Session["Message"] = "Status Not Updated, Please try again.";
                            Response.Redirect("DealerList.aspx", false);
                        }
                    }
                    else
                    {
                        Session["Message"] = "Status Not Updated, Please try again.";
                        Response.Redirect("DealerList.aspx", false);
                    }
                }
                else
                {
                    Session["Message"] = "Status Not Updated, Please try again.";
                    Response.Redirect("DealerList.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "Status Not Updated, Please try again. " + Ex.Message.ToString();
                Response.Redirect("DealerList.aspx", false);
            }
        }
    }
}