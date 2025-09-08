using GenericAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static GenericAPP.Models.GenricDTO;

namespace GenericAPP
{
    public partial class AddUser : System.Web.UI.Page
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
                        GetRoleList();
                        GetDealerList();
                        string ActionType = "A";
                        int LoginID = Convert.ToInt32(Session["LoginID"]);
                        if (Request.QueryString.Get("ActionType") != null && Request.QueryString.Get("UserID") != null)
                        {
                            ActionType = cGeneral.Decrypt(Convert.ToString(Request.QueryString.Get("ActionType")));
                            int UserID = Convert.ToInt32(cGeneral.Decrypt(Convert.ToString(Request.QueryString.Get("UserID"))));
                            GetUserList(LoginID, UserID, 2);
                        }
                        ControlVisibility(ActionType);
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
        protected void ControlVisibility(string AtionType)
        {
            try
            {
                if (AtionType == "A")
                {
                    ddlDealername.Attributes.Add("Enabled", "Enabled");
                    txtdealername.Attributes.Add("Enabled", "Enabled");
                    txtusername.Attributes.Add("Enabled", "Enabled");
                    txtpassword.Attributes.Add("Enabled", "Enabled");
                    ddlroleid.Attributes.Add("Enabled", "Enabled");
                    txtemailid.Attributes.Add("Enabled", "Enabled");
                    btnSubmit.Visible = true;
                    btnupdate.Visible = false;
                }
                if (AtionType == "V")
                {
                    ddlDealername.Attributes.Add("disabled", "disabled");
                    txtdealername.Attributes.Add("disabled", "disabled");
                    txtusername.Attributes.Add("disabled", "disabled");
                    txtpassword.Attributes.Add("disabled", "disabled");
                    ddlroleid.Attributes.Add("disabled", "disabled");
                    txtemailid.Attributes.Add("disabled", "disabled");
                    btnSubmit.Visible = false;
                    btnupdate.Visible = false;
                }
                if (AtionType == "U")
                {
                    ddlDealername.Attributes.Add("disabled", "disabled");
                    txtdealername.Attributes.Add("Enabled", "Enabled");
                    txtusername.Attributes.Add("disabled", "disabled");
                    txtpassword.Attributes.Add("disabled", "disabled");
                    ddlroleid.Attributes.Add("Enabled", "Enabled");
                    txtemailid.Attributes.Add("disabled", "disabled");
                    btnupdate.Visible = true;
                    btnSubmit.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg(Ex.Message.ToString());
            }
        }
        public void GetDealerList()
        {
            try
            {
                var result = "";
                int DealerID = 0;
                int ActionType = 4;
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
                                ddlDealername.DataSource = ds.Tables[1];
                                ddlDealername.DataTextField = "DealerName";
                                ddlDealername.DataValueField = "DealerID";
                                ddlDealername.DataBind();
                                ddlDealername.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                            else
                            {
                                ddlDealername.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                        }
                        else
                        {
                            ddlDealername.Items.Insert(0, new ListItem("---Select---", "0"));
                        }
                    }
                    else
                    {
                        ddlDealername.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                }
                else
                {
                    ddlDealername.Items.Insert(0, new ListItem("---Select---", "0"));
                }
            }
            catch (Exception Ex)
            {
                ddlDealername.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
        public void GetRoleList()
        {
            try
            {
                var result = "";
                string API = "GetRoleList?";
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
                                ddlroleid.DataSource = ds.Tables[1];
                                ddlroleid.DataTextField = "RoleName";
                                ddlroleid.DataValueField = "RoleID";
                                ddlroleid.DataBind();
                                ddlroleid.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                            else
                            {
                                ddlroleid.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                        }
                        else
                        {
                            ddlroleid.Items.Insert(0, new ListItem("---Select---", "0"));
                        }
                    }
                    else
                    {
                        ddlroleid.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                }
                else
                {
                    ddlroleid.Items.Insert(0, new ListItem("---Select---", "0"));
                }
            }
            catch (Exception Ex)
            {
                ddlroleid.Items.Insert(0, new ListItem("---Select---", "0"));
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
                                    hddnUserID.Value = Convert.ToString(ds.Tables[1].Rows[0]["UserID"]);
                                    txtdealername.Value = Convert.ToString(ds.Tables[1].Rows[0]["Name"]);
                                    ddlDealername.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["DealerID"]);
                                    txtusername.Value = Convert.ToString(ds.Tables[1].Rows[0]["Username"]);
                                    txtemailid.Value = Convert.ToString(ds.Tables[1].Rows[0]["EmailID"]);
                                    txtpassword.Value = cGeneral.Encrypt(Convert.ToString(ds.Tables[1].Rows[0]["Password"]));
                                    ddlroleid.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["RoleID"]);
                                }
                                else
                                {
                                    ShowPopUpMsg(Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]));
                                }
                            }
                            else
                            {
                                ShowPopUpMsg("Something went wrong, Please try again.");
                            }
                        }
                        else
                        {
                            ShowPopUpMsg("Something went wrong, Please try again.");
                        }
                    }
                    else
                    {
                        ShowPopUpMsg("Something went wrong, Please try again.");
                    }
                }
                else
                {
                    ShowPopUpMsg("Something went wrong, Please try again.");
                }
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg("Something went wrong, Please try again. " + Ex.Message.ToString());
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserList.aspx");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string API = "User";
                User_DTO Add = new User_DTO();
                Add.UserID = 0;
                Add.ActionType = 1;
                Add.LoginID = Convert.ToInt32(Session["LoginID"]);
                Add.DealerID = Convert.ToInt32(ddlDealername.SelectedValue);
                Add.DealerName = Convert.ToString(txtdealername.Value);
                Add.Username = Convert.ToString(txtusername.Value);
                Add.Password = cGeneral.Encrypt(Convert.ToString(txtpassword.Value));
                Add.RoleID = Convert.ToInt32(ddlroleid.SelectedValue);
                Add.Email = Convert.ToString(txtemailid.Value);

                var jsonRequest = JsonConvert.SerializeObject(Add);

                string Name = Convert.ToString(txtdealername.Value);
                string EmailID = Convert.ToString(txtemailid.Value);
                string Username = Convert.ToString(txtusername.Value);
                string Password = Convert.ToString(txtpassword.Value);

                string Result = cGeneral.fnPostAPICall(API, jsonRequest);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            string Subject = "User Added Successfully - " + cGeneral.CompanyName;
                            string BodyEmail = string.Empty;
                            using (StreamReader reader = new StreamReader(Server.MapPath("~/Design/EmailTemplates/UserCreateEmail.html")))
                            {
                                BodyEmail = reader.ReadToEnd();
                            }
                            BodyEmail = BodyEmail.Replace("{DealerName}", Name);
                            BodyEmail = BodyEmail.Replace("{Username}", Username);
                            BodyEmail = BodyEmail.Replace("{Password}", Password);
                            BodyEmail = BodyEmail.Replace("{LoginURL}", cGeneral.LoginURL);
                            BodyEmail = BodyEmail.Replace("{CompanyName}", Convert.ToString(cGeneral.CompanyName));
                            cGeneral.SendMail(EmailID, Subject, BodyEmail);
























                            Session["Message"] = "User Added Successfully.";
                            Response.Redirect("UserList.aspx", false);
                        }
                        else
                        {
                            Session["Message"] = Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);
                            Response.Redirect("UserList.aspx", false);
                        }
                    }
                    else
                    {
                        Session["Message"] = "User Not Added Successfully, Please try again.";
                        Response.Redirect("UserList.aspx", false);
                    }
                }
                else
                {
                    Session["Message"] = "User Not Added Successfully, Please try again.";
                    Response.Redirect("UserList.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "User Not Added Successfully, Please try again. " + Ex.Message.ToString();
                Response.Redirect("UserList.aspx", false);
            }
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string API = "User";
                User_DTO Add = new User_DTO();
                Add.UserID = Convert.ToInt32(hddnUserID.Value);
                Add.ActionType = 2;
                Add.LoginID = Convert.ToInt32(Session["LoginID"]);
                Add.DealerID = Convert.ToInt32(ddlDealername.SelectedValue);
                Add.DealerName = Convert.ToString(txtdealername.Value);
                Add.Username = Convert.ToString(txtusername.Value);
                Add.Password = Convert.ToString(txtpassword.Value);
                Add.RoleID = Convert.ToInt32(ddlroleid.SelectedValue);
                Add.Email = Convert.ToString(txtemailid.Value);
                var jsonRequest = JsonConvert.SerializeObject(Add);
                string Result = cGeneral.fnPostAPICall(API, jsonRequest);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            Session["Message"] = "User Updated Successfully.";
                            Response.Redirect("UserList.aspx", false);
                        }
                        else
                        {
                            Session["Message"] = Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);
                            Response.Redirect("UserList.aspx", false);
                        }
                    }
                    else
                    {
                        Session["Message"] = "User Not Updated Successfully, Please try again.";
                        Response.Redirect("UserList.aspx", false);
                    }
                }
                else
                {
                    Session["Message"] = "User Not Updated Successfully, Please try again.";
                    Response.Redirect("UserList.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "User Not Updated Successfully, Please try again. " + Ex.Message.ToString();
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