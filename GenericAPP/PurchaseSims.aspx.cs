using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GenericAPP.Models;

namespace GenericAPP
{
    public partial class PurchaseSims : System.Web.UI.Page
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
                        GetNetworkList();
                        ddlNetwork_SelectedIndexChanged(null, null);
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
        public void GetNetworkList()
        {
            try
            {
                var result = "";
                string API = "NetworkList?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string Data = "LoginID=" + LoginID;
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
                                ddlNetwork.DataSource = ds.Tables[1];
                                ddlNetwork.DataTextField = "NetworkName";
                                ddlNetwork.DataValueField = "NetworkID";
                                ddlNetwork.DataBind();
                                ddlNetwork.Items.Insert(0, new ListItem("All", "0"));
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void ddlNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int NetworkID = Convert.ToInt32(ddlNetwork.SelectedValue);
                GetPurchaseList(NetworkID);
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg("Something went wrong, please try again !!!");
            }
        }
        protected void GetPurchaseList(int NetworkID)
        {
            try
            {
                string API = "PurchaseList?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string Data = "LoginID=" + LoginID + "&NetworkID=" + NetworkID;
                string Result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                RepeaterPurchaseList.DataSource = ds.Tables[0];
                                RepeaterPurchaseList.DataBind();
                                RepeaterPurchaseList.Visible = true;
                                lblDataFound.Text = "";
                                DivMain.Visible = true;
                            }
                            else
                            {
                                RepeaterPurchaseList.DataSource = null;
                                RepeaterPurchaseList.DataBind();
                                RepeaterPurchaseList.Visible = false;
                                lblDataFound.Text = "No data found !!!";
                                DivMain.Visible = false;
                            }
                        }
                        else
                        {
                            RepeaterPurchaseList.DataSource = null;
                            RepeaterPurchaseList.DataBind();
                            RepeaterPurchaseList.Visible = false;
                            lblDataFound.Text = "No data found !!!";
                            DivMain.Visible = false;
                        }
                    }
                    else
                    {
                        RepeaterPurchaseList.DataSource = null;
                        RepeaterPurchaseList.DataBind();
                        RepeaterPurchaseList.Visible = false;
                        lblDataFound.Text = "No data found !!!";
                        DivMain.Visible = false;
                    }
                }
                else
                {
                    RepeaterPurchaseList.DataSource = null;
                    RepeaterPurchaseList.DataBind();
                    RepeaterPurchaseList.Visible = false;
                    lblDataFound.Text = "No data found !!!";
                    DivMain.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                RepeaterPurchaseList.DataSource = null;
                RepeaterPurchaseList.DataBind();
                RepeaterPurchaseList.Visible = false;
                lblDataFound.Text = "No data found, " + Ex.Message.ToString() + " !!!";
                DivMain.Visible = false;
            }
        }
        protected void RepeaterSimList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                int IndexID = Convert.ToInt32(e.CommandArgument);
                string CommandName = Convert.ToString(e.CommandName);
                Label lblPurchaseNumber = (Label)RepeaterPurchaseList.Items[IndexID].FindControl("lblPurchaseNumber");
                string encPurchaseID = cGeneral.Encrypt(Convert.ToString(lblPurchaseNumber.Text));
                Response.Redirect("SimList.aspx?DID=" + encPurchaseID, false);
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg(Ex.Message.ToString());
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewSim.aspx", false);
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