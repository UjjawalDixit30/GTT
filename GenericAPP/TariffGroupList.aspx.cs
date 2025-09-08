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
    public partial class TariffGroupList : System.Web.UI.Page
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
                        GetTariffGroup();
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
        protected void GetTariffGroup()
        {
            try
            {
                int ActionType = 2;
                int TariffGroupID = 0;
                string API = "GetTariffGroup?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string Data = "LoginID=" + LoginID + "&TariffGroupID=" + TariffGroupID + "&ActionType=" + ActionType;
                string Result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    RepeaterTariffGroupList.DataSource = ds.Tables[1];
                                    RepeaterTariffGroupList.DataBind();
                                    RepeaterTariffGroupList.Visible = true;
                                    lblDataFound.Text = "";
                                    DivMain.Visible = true;
                                }
                                else
                                {
                                    RepeaterTariffGroupList.DataSource = null;
                                    RepeaterTariffGroupList.DataBind();
                                    RepeaterTariffGroupList.Visible = false;
                                    lblDataFound.Text = "No data found !!!";
                                    DivMain.Visible = false;
                                }
                            }
                            else
                            {
                                RepeaterTariffGroupList.DataSource = null;
                                RepeaterTariffGroupList.DataBind();
                                RepeaterTariffGroupList.Visible = false;
                                lblDataFound.Text = "No data found !!!";
                                DivMain.Visible = false;
                            }
                        }
                        else
                        {
                            RepeaterTariffGroupList.DataSource = null;
                            RepeaterTariffGroupList.DataBind();
                            RepeaterTariffGroupList.Visible = false;
                            lblDataFound.Text = "No data found !!!";
                            DivMain.Visible = false;
                        }
                    }
                    else
                    {
                        RepeaterTariffGroupList.DataSource = null;
                        RepeaterTariffGroupList.DataBind();
                        RepeaterTariffGroupList.Visible = false;
                        lblDataFound.Text = "No data found !!!";
                        DivMain.Visible = false;
                    }
                }
                else
                {
                    RepeaterTariffGroupList.DataSource = null;
                    RepeaterTariffGroupList.DataBind();
                    RepeaterTariffGroupList.Visible = false;
                    lblDataFound.Text = "No data found !!!";
                    DivMain.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                RepeaterTariffGroupList.DataSource = null;
                RepeaterTariffGroupList.DataBind();
                RepeaterTariffGroupList.Visible = false;
                lblDataFound.Text = "No data found, " + Ex.Message.ToString() + " !!!";
                DivMain.Visible = false;
            }
        }
        protected void RepeaterTariffGroupList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                Int32 IndexID = Convert.ToInt32(e.CommandArgument);
                string CommandName = Convert.ToString(e.CommandName);
                Label lblTariffGroupID = (Label)RepeaterTariffGroupList.Items[IndexID].FindControl("lblTariffGroupID");
                Label lblTariffGroupName = (Label)RepeaterTariffGroupList.Items[IndexID].FindControl("lblTariffGroupName");
                string TGI = Convert.ToString(cGeneral.Encrypt(lblTariffGroupID.Text));
                string TGN = Convert.ToString(cGeneral.Encrypt(lblTariffGroupName.Text));
                if (CommandName == "View")
                {
                    Response.Redirect("ViewTariffGroup.aspx?TGI=" + TGI, false);
                }
                else if (CommandName == "Edit")
                {
                    Response.Redirect("AddTariffGroup.aspx?TGI=" + TGI + "&TGN=" + TGN, false);
                }
                else if (CommandName == "UpdateStatus")
                {
                    int LoginID = Convert.ToInt32(Session["LoginID"]);
                }
                else
                {
                    ShowPopUpMsg("Something went wrong, Please try again.");
                }
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg(Ex.Message.ToString());
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