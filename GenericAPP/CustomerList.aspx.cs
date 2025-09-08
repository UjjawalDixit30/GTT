using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GenericAPP.Models;
using static GenericAPP.Models.GenricDTO;

namespace GenericAPP
{
    public partial class CustomerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
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
                        GetCustomerList();
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
        protected void GetCustomerList()
        {
            try
            {
                var result = "";
                int CustomerID = 0;
                int ActionType = 1;
                string API = "GetCustomer?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string Data = "LoginID=" + LoginID + "&ActionType=" + ActionType + "&CustomerID=" + CustomerID;
                result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                RepeaterCustomerList.DataSource = ds.Tables[1];
                                RepeaterCustomerList.DataBind();
                                RepeaterCustomerList.Visible = true;
                                lblDataFound.Text = "";
                                controlvisibility();
                                DivMain.Visible = true;
                            }
                            else
                            {
                                RepeaterCustomerList.DataSource = null;
                                RepeaterCustomerList.DataBind();
                                RepeaterCustomerList.Visible = true;
                                lblDataFound.Text = "No Data Found !!!";
                                DivMain.Visible = false;
                            }
                        }
                        else
                        {
                            RepeaterCustomerList.DataSource = null;
                            RepeaterCustomerList.DataBind();
                            RepeaterCustomerList.Visible = true;
                            lblDataFound.Text = "No Data Found !!!";
                            DivMain.Visible = false;
                        }
                    }
                    else
                    {
                        RepeaterCustomerList.DataSource = null;
                        RepeaterCustomerList.DataBind();
                        RepeaterCustomerList.Visible = true;
                        lblDataFound.Text = "No Data Found !!!";
                        DivMain.Visible = false;
                    }
                }
                else
                {
                    RepeaterCustomerList.DataSource = null;
                    RepeaterCustomerList.DataBind();
                    RepeaterCustomerList.Visible = true;
                    lblDataFound.Text = "No Data Found !!!";
                    DivMain.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                RepeaterCustomerList.DataSource = null;
                RepeaterCustomerList.DataBind();
                RepeaterCustomerList.Visible = false;
                lblDataFound.Text = "No Data Found, " + Ex.Message.ToString() + "!!!";
                DivMain.Visible = false;
            }
        }
        public void controlvisibility()
        {
            try
            {
                for (int i = 0; i < RepeaterCustomerList.Items.Count; i++)
                {
                    Label lblStatus = (Label)RepeaterCustomerList.Items[i].FindControl("lblStatus");
                    Label lblIsActive = (Label)RepeaterCustomerList.Items[i].FindControl("lblIsActive");
                    if (lblIsActive.Text == "0")
                    {
                        lblStatus.Attributes.Add("class", "badge bg-label-warning me-1");
                    }
                    if (lblIsActive.Text == "1")
                    {
                        lblStatus.Attributes.Add("class", "badge bg-label-success me-1");
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void UpdateCustomerStatus(int LoginID, int CustomerID, int ActionType)
        {
            try
            {
                string API = "GetCustomer?";
                string Data = "LoginID=" + LoginID + "&CustomerID=" + CustomerID + "&ActionType=" + ActionType;
                string Result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            Session["Message"] = "Status Updated Successfully.";
                            Response.Redirect("CustomerList.aspx", false);
                        }
                        else
                        {
                            Session["Message"] = "Status Not Updated, Please try again.";
                            Response.Redirect("CustomerList.aspx", false);
                        }
                    }
                    else
                    {
                        Session["Message"] = "Status Not Updated, Please try again.";
                        Response.Redirect("CustomerList.aspx", false);
                    }
                }
                else
                {
                    Session["Message"] = "Status Not Updated, Please try again.";
                    Response.Redirect("CustomerList.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "Status Not Updated, Please try again. " + Ex.Message.ToString();
                Response.Redirect("CustomerList.aspx", false);
            }
        }
        protected void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Customer_DTO Add = new Customer_DTO();
                Add.LoginID = Convert.ToInt32(Session["LoginID"]);
                Add.CustomerID = Convert.ToInt32(hdnEditCustomerID.Value);
                Add.CustomerName = txtCustomerName.Text.Trim();
                Add.CustomerEmailID = txtCustomerEmail.Text.Trim();

                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(Add);

                string result = cGeneral.fnPostAPICall("UpdateCustomer", jsonData);

                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ErrorNumber"].ToString() == "0")
                    {
                        Session["Message"] = "Customer details updated successfully.";
                        Response.Redirect("CustomerDetails.aspx", false);
                    }
                    else
                    {
                        ShowPopUpMsg("Update failed: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "showModal();", true); // keep modal open
                    }
                }
                else
                {
                    ShowPopUpMsg("Unexpected response from API.");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "showModal();", true);
                }
            }
            catch (Exception ex)
            {
                ShowPopUpMsg("Error occurred: " + ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "showModal();", true);
            }
        }
        protected void RepeaterCustomerList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                int CustomerID = Convert.ToInt32(e.CommandArgument);
                string CommandName = Convert.ToString(e.CommandName);

                if (CommandName == "EditCustomer")
                {
                    int loginId = Convert.ToInt32(Session["LoginID"]);
                    string API = "GetCustomer?";
                    string Data = "LoginID=" + loginId + "&CustomerID=" + CustomerID + "&ActionType=2";

                    string result = cGeneral.fnGetAPICall(API, Data);
                    System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);

                    if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        txtCustomerName.Text = ds.Tables[1].Rows[0]["CustomerName"].ToString();
                        txtCustomerEmail.Text = ds.Tables[1].Rows[0]["CustomerEmailID"].ToString();
                        hdnEditCustomerID.Value = CustomerID.ToString();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "showModal();", true);
                    }
                    else
                    {
                        ShowPopUpMsg("Customer details not found.");
                    }
                }


                if (CommandName == "UpdateStatus")
                {
                    int LoginID = Convert.ToInt32(Session["LoginID"]);
                    UpdateCustomerStatus(LoginID, CustomerID, 3);
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