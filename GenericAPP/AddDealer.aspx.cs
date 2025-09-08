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
    public partial class AddDealer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Int32 DealerID = 0;
                    string ActionType = "A";
                    if (Session["LoginID"] != null)
                    {
                        if (Session["Message"] != null)
                        {
                            ShowPopUpMsg(Convert.ToString(Session["Message"]));
                            Session["Message"] = null;
                        }
                        GetTariffList();
                        GetCountryList();
                        int LoginID = Convert.ToInt32(Session["LoginID"]);
                        if (Request.QueryString.Get("ActionType") != null && Request.QueryString.Get("DealerID") != null)
                        {
                            ActionType = cGeneral.Decrypt(Convert.ToString(Request.QueryString.Get("ActionType")));
                            DealerID = Convert.ToInt32(cGeneral.Decrypt(Convert.ToString(Request.QueryString.Get("DealerID"))));
                            GetDealerList(LoginID, DealerID, 2);
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
        public void GetCountryList()
        {
            try
            {
                var result = "";
                string API = "CountryList?";
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
                                ddlCountry.DataSource = ds.Tables[1];
                                ddlCountry.DataTextField = "CountryName";
                                ddlCountry.DataValueField = "CountryID";
                                ddlCountry.DataBind();
                                ddlCountry.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                            else
                            {
                                ddlCountry.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                        }
                        else
                        {
                            ddlCountry.Items.Insert(0, new ListItem("---Select---", "0"));
                        }
                    }
                    else
                    {
                        ddlCountry.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                }
                else
                {
                    ddlCountry.Items.Insert(0, new ListItem("---Select---", "0"));
                }
            }
            catch (Exception Ex)
            {
                ddlCountry.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
        public void GetTariffList()
        {
            try
            {
                int ActionType = 1;
                var result = "";
                string API = "GetTariffGroup?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string Data = "LoginID=" + LoginID + "&ActionType=" + ActionType;
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
                                ddltarrifgroup.DataSource = ds.Tables[1];
                                ddltarrifgroup.DataTextField = "TariffGroupName";
                                ddltarrifgroup.DataValueField = "tariffGroupID";
                                ddltarrifgroup.DataBind();
                                ddltarrifgroup.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                            else
                            {
                                ddltarrifgroup.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                        }
                        else
                        {
                            ddltarrifgroup.Items.Insert(0, new ListItem("---Select---", "0"));
                        }
                    }
                    else
                    {
                        ddltarrifgroup.Items.Insert(0, new ListItem("---Select---", "0"));
                    }
                }
                else
                {
                    ddltarrifgroup.Items.Insert(0, new ListItem("---Select---", "0"));
                }
            }
            catch (Exception Ex)
            {
                ddltarrifgroup.Items.Insert(0, new ListItem("---Select---", "0"));
            }
        }
        protected void GetDealerList(int LoginID, int DealerID, int ActionType)
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
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                hddnDealerID.Value = Convert.ToString(DealerID);
                                txtname.Value = Convert.ToString(ds.Tables[1].Rows[0]["DealerName"]);
                                txtcontactperson.Value = Convert.ToString(ds.Tables[1].Rows[0]["ContactPerson"]);
                                txtcity.Value = Convert.ToString(ds.Tables[1].Rows[0]["City"]);
                                txtcontactnumber.Value = Convert.ToString(ds.Tables[1].Rows[0]["ContactNumber"]);
                                txtemailid.Value = Convert.ToString(ds.Tables[1].Rows[0]["ContactEmailID"]);
                                txtstate.Value = Convert.ToString(ds.Tables[1].Rows[0]["State"]);
                                txtzip.Value = Convert.ToString(ds.Tables[1].Rows[0]["ZipCode"]);
                                txtmaxAmount.Value = Convert.ToString(ds.Tables[1].Rows[0]["MaximumTopUpAmount"]);
                                txtminAmount.Value = Convert.ToString(ds.Tables[1].Rows[0]["MinimumTopUpAmount"]);
                                ddlCountry.Value = Convert.ToString(ds.Tables[1].Rows[0]["CountryID"]);
                                txtaddress.Value = Convert.ToString(ds.Tables[1].Rows[0]["Address"]);
                                txttransactionfee.Value = Convert.ToString(ds.Tables[1].Rows[0]["TransactionFee"]);
                                ddlCreateDealer.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["AllowSellerCreation"]);
                                ddltarrifgroup.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["TariffGroupID"]);
                                ddlCreateDealer.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["AllowSellerCreation"]).ToLower() == "true" ? "1" : "0";

                                txtusername.Value = Convert.ToString(ds.Tables[2].Rows[0]["Username"]);
                                txtpassword.Value = Convert.ToString(ds.Tables[2].Rows[0]["Password"]);
                                hddnPassword.Value = Convert.ToString(ds.Tables[2].Rows[0]["Password"]);

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
            catch (Exception Ex)
            {
                ShowPopUpMsg("Something went wrong, Please try again. " + Ex.Message.ToString());
            }
        }
        protected void ControlVisibility(string AtionType)
        {
            try
            {
                if (AtionType == "A")
                {
                    txtname.Attributes.Remove("disabled");
                    txtcontactperson.Attributes.Remove("disabled");
                    txtcity.Attributes.Remove("disabled");
                    txtaddress.Attributes.Remove("disabled");
                    txtcontactnumber.Attributes.Remove("disabled");
                    txtemailid.Attributes.Remove("disabled");
                    txtstate.Attributes.Remove("disabled");
                    txtzip.Attributes.Remove("disabled");
                    txtmaxAmount.Attributes.Remove("disabled");
                    txtminAmount.Attributes.Remove("disabled");
                    ddlCountry.Attributes.Remove("disabled");
                    ddlCreateDealer.Attributes.Remove("disabled");
                    ddltarrifgroup.Attributes.Remove("disabled");
                    txtusername.Attributes.Remove("disabled");
                    txtpassword.Attributes.Remove("disabled");
                    txttransactionfee.Attributes.Remove("disabled");
                    btnUpdate.Visible = false;
                    btnSubmit.Visible = true;
                }
                if (AtionType == "U")
                {
                    txtname.Attributes.Add("Enabled", "Enabled");
                    txtcontactperson.Attributes.Add("Enabled", "Enabled");
                    txtcity.Attributes.Remove("disabled");
                    txtaddress.Attributes.Remove("disabled");
                    txtcontactnumber.Attributes.Remove("disabled");
                    txtemailid.Attributes.Add("disabled", "disabled");
                    txtstate.Attributes.Remove("disabled");
                    txtzip.Attributes.Remove("disabled");
                    txtmaxAmount.Attributes.Add("disabled", "disabled");
                    txtminAmount.Attributes.Add("disabled", "disabled");
                    ddlCountry.Attributes.Remove("disabled");
                    ddltarrifgroup.Attributes.Add("disabled", "disabled");
                    ddlCreateDealer.Attributes.Add("disabled", "disabled");
                    txtusername.Attributes.Add("disabled", "disabled");
                    txtpassword.Attributes.Add("disabled", "disabled");
                    txttransactionfee.Attributes.Remove("disabled");
                    btnUpdate.Visible = true;
                    btnSubmit.Visible = false;
                }
                if (AtionType == "V")
                {
                    txtname.Attributes.Add("disabled", "disabled");
                    txtcontactperson.Attributes.Add("disabled", "disabled");
                    txtcity.Attributes.Add("disabled", "disabled");
                    txtaddress.Attributes.Add("disabled", "disabled");
                    txtcontactnumber.Attributes.Add("disabled", "disabled");
                    txtemailid.Attributes.Add("disabled", "disabled");
                    txtstate.Attributes.Add("disabled", "disabled");
                    txtzip.Attributes.Add("disabled", "disabled");
                    txtmaxAmount.Attributes.Add("disabled", "disabled");
                    txtminAmount.Attributes.Add("disabled", "disabled");
                    ddlCountry.Attributes.Add("disabled", "disabled");
                    ddltarrifgroup.Attributes.Add("disabled", "disabled");
                    ddlCreateDealer.Attributes.Add("disabled", "disabled");
                    txtusername.Attributes.Add("disabled", "disabled");
                    txtpassword.Attributes.Add("disabled", "disabled");
                    txttransactionfee.Attributes.Add("disabled", "disabled");
                    btnUpdate.Visible = false;
                    btnSubmit.Visible = false;
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtname.Value))
                {
                    ShowPopUpMsg("Dealer Name is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtcontactperson.Value))
                {
                    ShowPopUpMsg("Contact Person is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtcontactnumber.Value))
                {
                    ShowPopUpMsg("Contact Number is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtemailid.Value))
                {
                    ShowPopUpMsg("Contact EmailID is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtaddress.Value))
                {
                    ShowPopUpMsg("Address is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtcity.Value))
                {
                    ShowPopUpMsg("City Name is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtstate.Value))
                {
                    ShowPopUpMsg("State Name is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtzip.Value))
                {
                    ShowPopUpMsg("ZipCode is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(ddlCountry.Value))
                {
                    ShowPopUpMsg("Please Select Country");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtusername.Value))
                {
                    ShowPopUpMsg("UserName is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtpassword.Value))
                {
                    ShowPopUpMsg("Please enter your Password");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txttransactionfee.Value))
                {
                    ShowPopUpMsg("TransactionFee is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtminAmount.Value))
                {
                    ShowPopUpMsg("MinimumTopUpAmount is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtmaxAmount.Value))
                {
                    ShowPopUpMsg("MaximumTopUpAmount is required");
                    return;
                }
                if (string.IsNullOrWhiteSpace(ddltarrifgroup.SelectedValue))
                {
                    ShowPopUpMsg("Please select tarrifgroup");
                    return;
                }
                if (string.IsNullOrWhiteSpace(ddlCreateDealer.SelectedValue))
                {
                    ShowPopUpMsg("Please Create dealer");
                    return;
                }
                string API = "AddUpdateDealer";
                DealerDTO Add = new DealerDTO();
                Add.dealerid = 0;
                Add.Actiontype = 1;
                Add.City = Convert.ToString(txtcity.Value);
                Add.Name = Convert.ToString(txtname.Value);
                Add.ZipCode = Convert.ToString(txtzip.Value);
                Add.State = Convert.ToString(txtstate.Value);
                Add.Address = Convert.ToString(txtaddress.Value);
                Add.LoginID = Convert.ToInt32(Session["LoginID"]);
                Add.CountryID = Convert.ToInt32(ddlCountry.Value);
                Add.Username = Convert.ToString(txtusername.Value);
                Add.Password = cGeneral.Encrypt(Convert.ToString(txtpassword.Value));
                Add.ContactEmailID = Convert.ToString(txtemailid.Value);
                Add.ContactNumber = Convert.ToString(txtcontactnumber.Value);
                Add.ContactPerson = Convert.ToString(txtcontactperson.Value);
                Add.MaximumTopUpAmount = Convert.ToDecimal(txtmaxAmount.Value);
                Add.MinimumTopUpAmount = Convert.ToDecimal(txtminAmount.Value);
                Add.TransactionFee = Convert.ToDecimal(txttransactionfee.Value);
                Add.TariffGroupID = Convert.ToInt32(ddltarrifgroup.SelectedValue);
                Add.AllowSellerCreation = Convert.ToInt32(ddlCreateDealer.SelectedValue);

                var jsonRequest = JsonConvert.SerializeObject(Add);
                string Result = cGeneral.fnPostAPICall(API, jsonRequest);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            string Name = Convert.ToString(txtname.Value);
                            string EmailID = Convert.ToString(txtemailid.Value);
                            string Username = Convert.ToString(txtusername.Value);
                            string Password = Convert.ToString(txtpassword.Value);
                            string Subject = "Dealer Added Successfully - " + cGeneral.CompanyName;
                            string BodyEmail = string.Empty;
                            using (StreamReader reader = new StreamReader(Server.MapPath("~/Design/EmailTemplates/DealerCreateEmail.html")))
                            {
                                BodyEmail = reader.ReadToEnd();
                            }
                            BodyEmail = BodyEmail.Replace("{DealerName}", Name);
                            BodyEmail = BodyEmail.Replace("{Username}", Username);
                            BodyEmail = BodyEmail.Replace("{Password}", Password);
                            BodyEmail = BodyEmail.Replace("{LoginURL}", cGeneral.LoginURL);
                            BodyEmail = BodyEmail.Replace("{CompanyName}", Convert.ToString(cGeneral.CompanyName));
                            cGeneral.SendMail(EmailID, Subject, BodyEmail);
                            Session["Message"] = "Dealer Added Successfully, Login details has been send on registered email id.";
                            Response.Redirect("DealerList.aspx", false);
                        }
                        else
                        {
                            Session["Message"] = Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);
                            Response.Redirect("DealerList.aspx", false);
                        }
                    }
                    else
                    {
                        Session["Message"] = "Dealer Not Added Successfully, Please try again.";
                        Response.Redirect("DealerList.aspx", false);
                    }
                }
                else
                {
                    Session["Message"] = "Dealer Not Added Successfully, Please try again.";
                    Response.Redirect("DealerList.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "Dealer Not Added Successfully, Please try again. " + Ex.Message.ToString();
                Response.Redirect("DealerList.aspx", false);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string API = "AddUpdateDealer";
                DealerDTO Add = new DealerDTO();
                Add.Actiontype = 2;
                Add.City = Convert.ToString(txtcity.Value);
                Add.Name = Convert.ToString(txtname.Value);
                Add.ZipCode = Convert.ToString(txtzip.Value);
                Add.State = Convert.ToString(txtstate.Value);
                Add.Address = Convert.ToString(txtaddress.Value);
                Add.CountryID = Convert.ToInt32(ddlCountry.Value);
                Add.LoginID = Convert.ToInt32(Session["LoginID"]);
                Add.dealerid = Convert.ToInt32(hddnDealerID.Value);
                Add.Password = Convert.ToString(txtpassword.Value);
                Add.Username = Convert.ToString(hddnPassword.Value);
                Add.ContactEmailID = Convert.ToString(txtemailid.Value);
                Add.ContactNumber = Convert.ToString(txtcontactnumber.Value);
                Add.ContactPerson = Convert.ToString(txtcontactperson.Value);
                Add.MaximumTopUpAmount = Convert.ToDecimal(txtmaxAmount.Value);
                Add.MinimumTopUpAmount = Convert.ToDecimal(txtminAmount.Value);
                Add.TransactionFee = Convert.ToDecimal(txttransactionfee.Value);
                Add.TariffGroupID = Convert.ToInt32(ddltarrifgroup.SelectedValue);
                Add.AllowSellerCreation = Convert.ToInt32(ddlCreateDealer.SelectedValue);

                var jsonRequest = JsonConvert.SerializeObject(Add);
                string Result = cGeneral.fnPostAPICall(API, jsonRequest);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            Session["Message"] = "Dealer Updated Successfully.";
                        }
                        else
                        {
                            Session["Message"] = Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);
                        }
                    }
                    else
                    {
                        Session["Message"] = "Dealer Not Updated Successfully, Please try again.";
                    }
                }
                else
                {
                    Session["Message"] = "Dealer Not Updated Successfully, Please try again.";
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "Dealer Not Updated Successfully, Please try again. " + Ex.Message.ToString();
            }
            Response.Redirect("DealerList.aspx", false);
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