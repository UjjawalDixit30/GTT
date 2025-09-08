using GenericAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GenericAPP
{
    public partial class UserList : System.Web.UI.Page
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
                        int LoginID = Convert.ToInt32(Session["LoginID"]);
                        GetUserList(LoginID, 0, 1);
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
        protected void GetUserList(int LoginID, int UserID, int ActionType)
        {
            try
            {
                string API = "GetUser?";
                string Data = "LoginID=" + LoginID + "&ActionType=" + ActionType + "&UserID=" + UserID;
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
                                    RepeaterUserList.DataSource = ds.Tables[1];
                                    RepeaterUserList.DataBind();
                                    RepeaterUserList.Visible = true;
                                    lblDataFound.Text = "";
                                    controlvisibility();
                                }
                                else
                                {
                                    RepeaterUserList.DataSource = null;
                                    RepeaterUserList.DataBind();
                                    RepeaterUserList.Visible = false;
                                    lblDataFound.Text = "No Data Found !!!";
                                }
                            }
                            else
                            {
                                RepeaterUserList.DataSource = null;
                                RepeaterUserList.DataBind();
                                RepeaterUserList.Visible = false;
                                lblDataFound.Text = "No Data Found !!!";
                            }
                        }
                        else
                        {
                            RepeaterUserList.DataSource = null;
                            RepeaterUserList.DataBind();
                            RepeaterUserList.Visible = false;
                            lblDataFound.Text = "No Data Found !!!";
                        }
                    }
                    else
                    {
                        RepeaterUserList.DataSource = null;
                        RepeaterUserList.DataBind();
                        RepeaterUserList.Visible = false;
                        lblDataFound.Text = "No Data Found !!!";
                    }
                }
                else
                {
                    RepeaterUserList.DataSource = null;
                    RepeaterUserList.DataBind();
                    RepeaterUserList.Visible = false;
                    lblDataFound.Text = "No Data Found !!!";
                }
            }
            catch (Exception Ex)
            {
                RepeaterUserList.DataSource = null;
                RepeaterUserList.DataBind();
                RepeaterUserList.Visible = false;
                lblDataFound.Text = "No Data Found, " + Ex.Message.ToString() + " !!!";
            }
        }
        public void controlvisibility()
        {
            try
            {
                for (int i = 0; i < RepeaterUserList.Items.Count; i++)
                {
                    Label lblStatus = (Label)RepeaterUserList.Items[i].FindControl("lblStatus");
                    Label lblIsActive = (Label)RepeaterUserList.Items[i].FindControl("lblIsActive");
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
        protected void RepeaterUserList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                int UserID = Convert.ToInt32(e.CommandArgument);
                string CommandName = Convert.ToString(e.CommandName);
                if (CommandName == "ViewDealer")
                {
                    string ActionType = cGeneral.Encrypt("V");
                    string UID = cGeneral.Encrypt(Convert.ToString(UserID));
                    Response.Redirect("AddUser.aspx?ActionType=" + ActionType + "&UserID=" + UID, false);
                }
                if (CommandName == "EditDealer")
                {
                    string ActionType = cGeneral.Encrypt("U");
                    string UID = cGeneral.Encrypt(Convert.ToString(UserID));
                    Response.Redirect("AddUser.aspx?ActionType=" + ActionType + "&UserID=" + UID, false);
                }
                if (CommandName == "UpdateStatus")
                {
                    int LoginID = Convert.ToInt32(Session["LoginID"]);
                    UpdateDealerStatus(LoginID, UserID, 3);
                }
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg(Ex.Message.ToString());
            }

        }
        protected void UpdateDealerStatus(int LoginID, int UserID, int ActionType)
        {
            try
            {
                string API = "GetUser?";
                string Data = "LoginID=" + LoginID + "&UserID=" + UserID + "&ActionType=" + ActionType;
                string Result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            Session["Message"] = "Status Updated Successfully.";
                            Response.Redirect("UserList.aspx", false);
                        }
                        else
                        {
                            Session["Message"] = "Status Not Updated, Please try again.";
                            Response.Redirect("UserList.aspx", false);
                        }
                    }
                    else
                    {
                        Session["Message"] = "Status Not Updated, Please try again.";
                        Response.Redirect("UserList.aspx", false);
                    }
                }
                else
                {
                    Session["Message"] = "Status Not Updated, Please try again.";
                    Response.Redirect("UserList.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "Status Not Updated, Please try again. " + Ex.Message.ToString();
                Response.Redirect("UserList.aspx", false);
            }
        }
        protected void btnSubmitPassword_Click(object sender, EventArgs e)
        {
            try
            {
                int ActionType = 1;
                string UserID = hdnSelectedUserId.Value;
                string Password = cGeneral.Encrypt(txtNewPassword.Text);
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string API = "ChangedPassword?";
                string Data = "LoginID=" + LoginID + "&UserID=" + UserID + "&Password=" + Password + "&ActionType=" + ActionType;
                string Result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            Session["Message"] = "User Password Updated Successfully.";
                            Response.Redirect("UserList.aspx", false);
                        }
                        else
                        {
                            Session["Message"] = "User Password  Not Updated, Please try again.";
                            Response.Redirect("UserList.aspx", false);
                        }
                    }
                    else
                    {
                        Session["Message"] = "User Password  Not Updated, Please try again.";
                        Response.Redirect("UserList.aspx", false);
                    }
                }
                else
                {
                    Session["Message"] = "User Password  Not Updated, Please try again.";
                    Response.Redirect("UserList.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "User Password  Not Updated, Please try again. " + Ex.Message.ToString();
                Response.Redirect("UserList.aspx", false);
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