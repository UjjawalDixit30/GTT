using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GenericAPP.Models;
using Newtonsoft.Json;
using static GenericAPP.Models.GenricDTO;

namespace GenericAPP
{
    public partial class AddFund : System.Web.UI.Page
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
                        if (Convert.ToInt32(Session["AccountType"]) == 1)
                        {
                            btnTopUp.Visible = true;
                            btnPaypal.Visible = false;
                            btnDeductAmount.Visible = true;
                            ddlDealer.SelectedValue = "0";
                            ddlDealer.Attributes.Remove("disabled");
                        }
                        else
                        {
                            btnTopUp.Visible = false;
                            btnPaypal.Visible = true;
                            btnDeductAmount.Visible = false;
                            ddlDealer.SelectedValue = Convert.ToString(Session["DealerID"]);
                            ddlDealer.Attributes.Add("disabled", "disabled");
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
                                ddlDealer.DataSource = ds.Tables[1];
                                ddlDealer.DataTextField = "DealerName";
                                ddlDealer.DataValueField = "DealerID";
                                ddlDealer.DataBind();
                                ddlDealer.Items.Insert(0, new ListItem("---Select---", "0"));
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var result = "";
                int ActionType = 5;
                string API = "GetDealer?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
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
                                lblDealerAccountBalance.InnerText = "USD : " + Convert.ToString(ds.Tables[1].Rows[0]["AccountBalance"]);
                                AccountBalanceDiv.Style.Add("display", "block");
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void btnTopUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDealer.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(ddlDealer.SelectedValue))
                {
                    ShowPopUpMsg("Please select a dealer name");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtAmount.Text))
                {
                    ShowPopUpMsg("Please enter amount");
                    return;
                }

                if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
                {
                    ShowPopUpMsg("Please enter a valid amount greater than 0");
                    return;
                }

                AddFund_DTO Add = new AddFund_DTO()
                {
                    ActionType = 1,
                    AmountCharged = Convert.ToDecimal(txtAmount.Text),
                    DealerID = Convert.ToInt32(ddlDealer.SelectedValue),
                    LoginID = Convert.ToInt32(Session["LoginID"]),
                    PaymentMode = 10,
                    PaymentStatus = 13,
                    PaymentTypeID = 6,
                    ProcessingFee = 0,
                    Remarks = "Manual fund addition by admin : " + Convert.ToString(txtRemarks.Text.Trim()),
                    RequestedDevice = cGeneral.GetDeviceInformation(Request),
                    RequestedIP = cGeneral.GetSystemIP(),
                    RequestedOS = cGeneral.GetUserPlatform(Request),
                    RequestStatus = 16,
                    Response = "Success",
                    ThirdPartyOrderID = "",
                    TXNID = cGeneral.TXNSerial + "AFM" + cGeneral.GetTransactionID()
                };
                string strRequest = JsonConvert.SerializeObject(Add);
                (string strResponse, Int32 RequestID) = Funds(strRequest);
                if (strResponse == "Success")
                {
                    Session["Message"] = "Fund added successfully !!!";
                }
                else
                {
                    Session["Message"] = strResponse;
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "Something went wrong, please try again. " + Ex.Message.ToString();
            }
            Response.Redirect("AddFund.aspx", false);
        }
        protected void btnDeductAmount_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDealer.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(ddlDealer.SelectedValue))
                {
                    ShowPopUpMsg("Please select a dealer name");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtAmount.Text))
                {
                    ShowPopUpMsg("Please enter amount");
                    return;
                }
                if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
                {
                    ShowPopUpMsg("Please enter a valid amount greater than 0");
                    return;
                }

                AddFund_DTO Add = new AddFund_DTO()
                {
                    ActionType = 2,
                    AmountCharged = Convert.ToDecimal(txtAmount.Text),
                    DealerID = Convert.ToInt32(ddlDealer.SelectedValue),
                    LoginID = Convert.ToInt32(Session["LoginID"]),
                    PaymentMode = 10,
                    PaymentStatus = 13,
                    PaymentTypeID = 7,
                    ProcessingFee = 0,
                    Remarks = "Manual deduction by admin " + Convert.ToString(txtRemarks.Text.Trim()) != "" ? " : " + Convert.ToString(txtRemarks.Text) : "",
                    RequestedDevice = cGeneral.GetDeviceInformation(Request),
                    RequestedIP = cGeneral.GetSystemIP(),
                    RequestedOS = cGeneral.GetUserPlatform(Request),
                    RequestStatus = 16,
                    Response = "Success",
                    ThirdPartyOrderID = "",
                    TXNID = cGeneral.TXNSerial + "DFM" + cGeneral.GetTransactionID()
                };
                string strRequest = JsonConvert.SerializeObject(Add);
                (string strResponse, Int32 RequestID) = Funds(strRequest);
                if (strResponse == "Success")
                {
                    Session["Message"] = "Fund deducted successfully !!!";
                }
                else
                {
                    Session["Message"] = strResponse;
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "Something went wrong, please try again. " + Ex.Message.ToString();
            }
            Response.Redirect("AddFund.aspx", false);
        }
        protected void btnPaypal_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                decimal Amount = Convert.ToDecimal(txtAmount.Text);
                decimal ProcessingPercent = Convert.ToDecimal(cGeneral.ProcessingPercent);
                decimal ProcessingFee = Amount * ProcessingPercent / 100;
                decimal TotalPaybleAmount = Amount + ProcessingFee;
                AddFund_DTO Add = new AddFund_DTO()
                {
                    ActionType = 3,
                    AmountCharged = Amount,
                    DealerID = Convert.ToInt32(ddlDealer.SelectedValue),
                    LoginID = Convert.ToInt32(Session["LoginID"]),
                    PaymentMode = 11,
                    PaymentStatus = 12,
                    PaymentTypeID = 6,
                    ProcessingFee = ProcessingFee,
                    Remarks = "Add fund addition by dealer " + Convert.ToString(txtRemarks.Text.Trim()) != "" ? " : " + Convert.ToString(txtRemarks.Text) : "",
                    RequestedDevice = cGeneral.GetDeviceInformation(Request),
                    RequestedIP = cGeneral.GetSystemIP(),
                    RequestedOS = cGeneral.GetUserPlatform(Request),
                    RequestStatus = 15,
                    Response = "Add fund process start by dealer.",
                    ThirdPartyOrderID = "",
                    TXNID = cGeneral.TXNSerial + "AFP" + cGeneral.GetTransactionID()
                };
                string strRequest = JsonConvert.SerializeObject(Add);
                (string strResponse, Int32 RequestID) = Funds(strRequest);
                if (strResponse == "Success")
                {
                    PayWithPayPal(TotalPaybleAmount);
                }
                else
                {
                    Session["Message"] = strResponse;
                    Response.Redirect("AddFund.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "Something went wrong, please try again. " + Ex.Message.ToString();
                Response.Redirect("AddFund.aspx", false);
            }
        }
        public (string strResponse, Int32 RequestID) Funds(string strRequest)
        {
            string result = "";
            Int32 RequestID = 0;
            string strResponse = "";
            try
            {
                string APIName = "AddFund";
                result = cGeneral.fnPostAPICall(APIName, strRequest);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            strResponse = "Success";
                            RequestID = Convert.ToInt32(ds.Tables[0].Rows[0]["RequestID"]);
                        }
                        else
                        {
                            strResponse = Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);
                        }
                    }
                    else
                    {
                        strResponse = "Something went wrong, we are facing some network/internal issue. Please try again.";
                    }
                }
                else
                {
                    strResponse = "Something went wrong, we are facing some network/internal issue. Please try again.";
                }
            }
            catch (Exception Ex)
            {
                strResponse = "Something went wrong, we are facing some network/internal issue. Please try again. " + Ex.Message.ToString();
            }
            return (strResponse, RequestID);
        }
        protected void PayWithPayPal(decimal amount)
        {
            string redirecturl = "";
            redirecturl += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + cGeneral.paypalemail;
            redirecturl += "&first_name=";
            redirecturl += "&city=";
            redirecturl += "&state=";
            redirecturl += "&item_name=Topup";
            redirecturl += "&amount=" + amount;
            redirecturl += "&night_phone_a=";
            redirecturl += "&address1=";
            redirecturl += "&shipping=";
            redirecturl += "&handling=";
            redirecturl += "&tax=";
            redirecturl += "&quantity=1";
            redirecturl += "&currency=USD";
            redirecturl += "&return=" + cGeneral.AddFundSuccessURL;
            redirecturl += "&cancel_return=" + cGeneral.AddFundFailedURL;
            Response.Redirect(redirecturl, false);
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