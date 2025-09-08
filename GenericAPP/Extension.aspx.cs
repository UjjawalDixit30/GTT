using GenericAPP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static GenericAPP.Models.GenricDTO;

namespace GenericAPP
{
    public partial class Extension : System.Web.UI.Page
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
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void btnValidate_Click(object sender, EventArgs e)
        {
            var Result = "";
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtSerialNumber.Text))
                {
                    ShowPopUpMsg("Please enter serial number.");
                    return; 
                }

                int ActionType = 3;
                string API = "ValidateDeviceNumber?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                int NetworkID = Convert.ToInt32(ddlNetwork.SelectedValue);
                string SerialNumber = Convert.ToString(txtSerialNumber.Text);
                string Data = "LoginID=" + LoginID + "&ActionType=" + ActionType + "&SerialNumber=" + SerialNumber + "&NetworkID=" + NetworkID;
                Result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                            {
                                GetDestinationList();
                                destinationdiv.Visible = true;

                                Plandiv.Visible = false;
                                CustomerEmailIDdiv.Visible = false;
                                PlanValidityDiv.Visible = false;
                                TotalPaybleAmountDiv.Visible = false;

                                txtActivationDate.Enabled = false;
                                txtPlanValidity.Text = "";
                                ddlPlan.SelectedValue = "0";
                                txtTotalPaybleAmount.Text = "";

                                txtSerialNumber.Enabled = false;
                                btnValidate.Visible = false;
                            }
                            else if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "1")
                            {
                                ShowPopUpMsg("Failed : " + Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]));
                            }
                            else
                            {
                                ShowPopUpMsg("Failed : Serial number not valid.");
                            }
                        }
                        else
                        {
                            ShowPopUpMsg("Failed : Serial number not valid.");
                        }
                    }
                    else
                    {
                        ShowPopUpMsg("Failed : Serial number not valid.");
                    }
                }
                else
                {
                    ShowPopUpMsg("Failed : Serial number not valid.");
                }
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg("Failed : Serial number not valid. " + Ex.Message.ToString());
            }
        }
        public void GetDestinationList()
        {
            try
            {
                var result = "";
                int ActionType = 1;
                int DestinationID = 0;
                string API = "GetDestinationList?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                int NetworkID = Convert.ToInt32(ddlNetwork.SelectedValue);
                string Data = "LoginID=" + LoginID + "&NetworkID=" + NetworkID + "&ActionType=" + ActionType + "&DestinationID=" + DestinationID;
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
                                ddlDestination.DataSource = ds.Tables[1];
                                ddlDestination.DataTextField = "DestinationName";
                                ddlDestination.DataValueField = "DestinationID";
                                ddlDestination.DataBind();
                                ddlDestination.Items.Insert(0, new ListItem("Select", "0"));
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void ddlDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var result = "";
                int ActionType = 2;
                Plandiv.Visible = false;
                string API = "GetDestinationList?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                int NetworkID = Convert.ToInt32(ddlNetwork.SelectedValue);
                int DestinationID = Convert.ToInt32(ddlDestination.SelectedValue);
                string Data = "LoginID=" + LoginID + "&NetworkID=" + NetworkID + "&ActionType=" + ActionType + "&DestinationID=" + DestinationID;
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
                                ddlPlan.DataSource = ds.Tables[1];
                                ddlPlan.DataTextField = "PLANNAME";
                                ddlPlan.DataValueField = "PLANID";
                                ddlPlan.DataBind();
                                ddlPlan.Items.Insert(0, new ListItem("Select", "0"));

                                Plandiv.Visible = true;
                                CustomerEmailIDdiv.Visible = false;
                                PlanValidityDiv.Visible = false;
                                TotalPaybleAmountDiv.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var result = "";
                int ActionType = 3;
                Plandiv.Visible = false;
                string API = "GetPlanDetails?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                int PlanID = Convert.ToInt32(ddlPlan.SelectedValue);
                int NetworkID = Convert.ToInt32(ddlNetwork.SelectedValue);
                int DestinationID = Convert.ToInt32(ddlDestination.SelectedValue);
                string Data = "LoginID=" + LoginID + "&NetworkID=" + NetworkID + "&ActionType=" + ActionType + "&DestinationID=" + DestinationID + "&PlanID=" + PlanID;
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
                                Session["Validity"] = Convert.ToString(ds.Tables[1].Rows[0]["VALIDITY"]);
                                Session["PlanPrice"] = Convert.ToString(ds.Tables[1].Rows[0]["PLANPRICE"]);
                                Session["PackageID"] = Convert.ToString(ds.Tables[1].Rows[0]["PACKAGEID"]);
                                Session["PaybleAmount"] = Convert.ToString(ds.Tables[1].Rows[0]["PAYBLEAMOUNT"]);
                                Session["PackagePrice_NetworkWiseAndCurrencyWise"] = Convert.ToString(ds.Tables[1].Rows[0]["PACKAGEPRICE_NETWORKWISEANDCURRENCYWISE"]);

                                txtPlanValidity.Text = Convert.ToString(ds.Tables[1].Rows[0]["VALIDITY"]);
                                txtTotalPaybleAmount.Text = Convert.ToString(ds.Tables[1].Rows[0]["PAYBLEAMOUNT"]);

                                txtActivationDate.Enabled = false;
                                txtPlanValidity.Enabled = false;
                                txtTotalPaybleAmount.Enabled = false;

                                Plandiv.Visible = true;
                                CustomerEmailIDdiv.Visible = true;
                                PlanValidityDiv.Visible = true;
                                TotalPaybleAmountDiv.Visible = true;
                                btnSubmit.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Extension.aspx", false);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Status = "", addorder_orderid = "", addorder_download_url = "", addorder_jsonrequest = "", addorder_strrespomnse = "", qrstrpath = "";
            try
            {
                var result = "";
                string MSISDN = "";
                int ActivationType = Convert.ToInt32(27);
                string API = "AddPendingRequest_Extension";
                int NetworkID = Convert.ToInt32(ddlNetwork.SelectedValue);
                string TXNID = cGeneral.TXNSerial + "EXT" + cGeneral.GetTransactionID();
                string serialnumber = Convert.ToString(txtSerialNumber.Text);
                AddPendingRequest_ActivationDTO Add = new AddPendingRequest_ActivationDTO()
                {
                    IMEI = "",
                    Remarks = "Extension requested",
                    LoginID = Convert.ToInt32(Session["LoginID"]),
                    TXNID = TXNID,
                    SerialNumber = Convert.ToString(txtSerialNumber.Text),
                    RequestedBy = Convert.ToInt32(Session["DealerID"]),
                    RequestedForDtTm = Convert.ToString(txtActivationDate.Text),
                    RequestedIP = cGeneral.GetSystemIP(),
                    RequestedDevice = cGeneral.GetDeviceInformation(Request),
                    RequestedOS = cGeneral.GetUserPlatform(Request),
                    RequestStatus = 30,
                    AmountCharged = Convert.ToDecimal(txtTotalPaybleAmount.Text),
                    PlanId = Convert.ToInt32(ddlPlan.SelectedValue),
                    NumberOfDays = Convert.ToInt32(txtPlanValidity.Text),
                    MSISDN = "",
                    EmailID = Convert.ToString(txtCustomerEmailID.Text),
                    NetworkID = Convert.ToInt16(ddlNetwork.SelectedValue),
                    DownloadURL = "",
                    ActivatedFrom = 33,
                    SimType = 27,
                    PaymentTypeID = 9,
                    PaymentMode = 10,
                    PaymentStatus = 13
                };
                string strRequest = JsonConvert.SerializeObject(Add);
                result = cGeneral.fnPostAPICall(API, strRequest);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            int days = Convert.ToInt32(Session["Validity"]);
                            string databundleid = Convert.ToString(Session["PackageID"]);
                            Int32 RequestID = Convert.ToInt32(ds.Tables[0].Rows[0]["RequestID"]);
                            Int32 PaymentID = Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentID"]);
                            string PackagePrice_NetworkWiseAndCurrencyWise = Convert.ToString(Session["PackagePrice_NetworkWiseAndCurrencyWise"]);
                            if (NetworkID == 1)
                            {
                                (Status, addorder_orderid, addorder_download_url, addorder_jsonrequest, addorder_strrespomnse, qrstrpath) = CallCMIAPI(serialnumber, databundleid, TXNID, ActivationType);
                            }
                            if (NetworkID == 2)
                            {
                                (Status, addorder_orderid, addorder_download_url, addorder_jsonrequest, addorder_strrespomnse, qrstrpath) = CallWorldMoveAPI(serialnumber, databundleid, TXNID, PackagePrice_NetworkWiseAndCurrencyWise, days, ActivationType);
                            }
                            if (Status == "Success")
                            {
                                UpdatePendingRequest_Extension(22, "Success", addorder_orderid, MSISDN, addorder_download_url, "", addorder_jsonrequest, addorder_strrespomnse, RequestID, PaymentID);
                            }
                            else
                            {
                                UpdatePendingRequest_Extension(23, "Failed", addorder_orderid, MSISDN, addorder_download_url, "", addorder_jsonrequest, addorder_strrespomnse, RequestID, PaymentID);
                            }
                        }
                        else
                        {
                            ShowPopUpMsg("Extension Failed : The Extension process could not be completed due to a network error or an internal issue.Please try again later or contact support if the problem persists.");
                        }
                    }
                    else
                    {
                        ShowPopUpMsg("Extension Failed : The Extension process could not be completed due to a network error or an internal issue.Please try again later or contact support if the problem persists.");
                    }
                }
                else
                {
                    ShowPopUpMsg("ActivatExtensionion Failed : The Extension process could not be completed due to a network error or an internal issue.Please try again later or contact support if the problem persists.");
                }
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg("Extension Failed : The Extension process could not be completed due to a network error or an internal issue.Please try again later or contact support if the problem persists. " + Ex.Message.ToString());
            }
        }
        public (string Status, string addorder_orderid, string addorder_download_url, string addorder_jsonrequest, string addorder_strrespomnse, string qrstrpath) CallCMIAPI(string serialnumber, string databundleid, string transactionid, int ActivationType)
        {
            string qrstrpath = "", Status = "", gettoken_status = "", gettoken_accesstoken = "", gettoken_jsonrequest = "",
                 addorder_status = "", addorder_orderid = "", addorder_download_url = "", addorder_jsonrequest = "", addorder_strrespomnse = "";
            try
            {
                (gettoken_status, gettoken_accesstoken, gettoken_jsonrequest) = GetToken_CMI();
                if (gettoken_status == "Success")
                {
                    (addorder_status, addorder_orderid, addorder_download_url, addorder_jsonrequest, addorder_strrespomnse) = AddOrder_CMI(serialnumber, databundleid, gettoken_accesstoken, transactionid);
                    if (addorder_status == "Success")
                    {
                        Status = "Success";
                    }
                    else
                    {
                        Status = "Add Order Failed : " + addorder_status;
                    }
                }
                else
                {
                    Status = "Get Token Failed : " + gettoken_status;
                }
            }
            catch (Exception Ex)
            {
                Status = "Failed : Something went wrong,  " + gettoken_status;
            }
            return (Status, addorder_orderid, addorder_download_url, addorder_jsonrequest, addorder_strrespomnse, qrstrpath);
        }
        public (string gettoken_status, string gettoken_accesstoken, string gettoken_jsonrequest) GetToken_CMI()
        {
            string code = "";
            string Result = "";
            string status = "";
            var jsonrequest = "";
            string description = "";
            string accesstoken = "";
            try
            {
                jsonrequest = @"{'id': '" + cGeneral.CMI_ID + "','type': '" + cGeneral.CMI_TYPE + "'}";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://globalapi.udbac.com:18081/aep/APP_getAccessToken_SBO/v1");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = Convert.ToString(jsonrequest);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (Convert.ToString(httpResponse.StatusCode) == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        Result = streamReader.ReadToEnd();
                    }
                    if (Result != "")
                    {
                        var Json = JObject.Parse(Result);
                        code = Convert.ToString(Json["code"]);
                        if (code == "0000000")
                        {
                            status = "Success";
                            description = Convert.ToString(Json["description"]);
                            accesstoken = Convert.ToString(Json["accessToken"]);
                        }
                        else
                        {
                            status = "Failed : Something went wrong.";
                        }
                    }
                    else
                    {
                        status = "Failed : Something went wrong.";
                    }
                }
                else
                {
                    status = "Failed : Something went wrong.";
                }
            }
            catch (Exception Ex)
            {
                status = "Failed : Something went wrong. " + Ex.Message.ToString();
            }
            return (status, accesstoken, jsonrequest);
        }
        public (string addorder_status, string addorder_orderid, string addorder_download_url, string addorder_jsonrequest, string addorder_strrespomnse) AddOrder_CMI(string serialnumber, string databundleid, string accesstoken, string thirdorderid)
        {
            string code = "";
            string status = "";
            string orderid = "";
            var jsonrequest = "";
            string strresponse = "";
            string description = "";
            string download_url = "";
            try
            {
                string quantity = "1";
                string sendLang = "2";
                string is_Refuel = "1";
                string currency = "USD";
                string includeCard = "1";
                jsonrequest = @"{'accessToken': '" + accesstoken + "','thirdOrderId': '" + thirdorderid + "','includeCard': '" + includeCard + "','is_Refuel': '" + is_Refuel + "','dataBundleId': '" + databundleid + "','quantity': '" + quantity + "','ICCID': '" + serialnumber + "','currency': '" + currency + "','sendLang': '" + sendLang + "'}";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://globalapi.udbac.com:18081/aep/APP_createOrder_SBO/v1");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = Convert.ToString(jsonrequest);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (Convert.ToString(httpResponse.StatusCode) == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        strresponse = streamReader.ReadToEnd();
                        if (strresponse != "")
                        {
                            var Json = JObject.Parse(strresponse);
                            code = Convert.ToString(Json["code"]);
                            description = Convert.ToString(Json["description"]);
                            if (code == "0000000")
                            {
                                status = "Success";
                                orderid = Convert.ToString(Json["orderID"]);
                                download_url = Convert.ToString(Json["download_url"]);
                            }
                            else
                            {
                                status = "Failed : Something went wrong.";
                            }
                        }
                        else
                        {
                            status = "Failed : Something went wrong.";
                        }
                    }
                }
                else
                {
                    status = "Failed : Something went wrong.";
                }
            }
            catch (Exception Ex)
            {
                status = "Failed : Something went wrong." + Ex.Message.ToString();
            }
            return (status, orderid, download_url, jsonrequest, strresponse);
        }
        public (string Status, string addorder_orderid, string addorder_download_url, string addorder_jsonrequest, string addorder_strrespomnse, string qrstrpath) CallWorldMoveAPI(string serialnumber, string databundleid, string transactionid, string PackagePrice_NetworkWiseAndCurrencyWise, int days, int ActivationType)
        {
            string qrstrpath = "", Status = "", gettoken_status = "", gettoken_accesstoken = "", addorder_status = "", addorder_orderid = "",
                 addorder_download_url = "", addorder_jsonrequest = "", addorder_strrespomnse = "";
            try
            {
                (addorder_status, addorder_orderid, addorder_download_url, addorder_jsonrequest, addorder_strrespomnse) = AddOrder_WorldMove(serialnumber, databundleid, gettoken_accesstoken, transactionid, days, ActivationType);
                if (addorder_status == "Success")
                {
                    Status = "Success";
                }
                else
                {
                    Status = "Add Order Failed : " + addorder_status;
                }
            }
            catch (Exception Ex)
            {
                Status = "Failed : Something went wrong,  " + gettoken_status;
            }
            return (Status, addorder_orderid, addorder_download_url, addorder_jsonrequest, addorder_strrespomnse, qrstrpath);
        }
        public (string addorder_status, string addorder_orderid, string addorder_download_url, string addorder_jsonrequest, string addorder_strrespomnse) AddOrder_WorldMove(string serialnumber, string databundleid, string accesstoken, string thirdorderid, int days, int ActivationType)
        {
            string EncStr = "";
            string status = "";
            string orderid = "";
            var jsonrequest = "";
            string strresponse = "";
            string download_url = "";
            try
            {
                EncStr = cGeneral.WorldMOve_MerchantId + cGeneral.WorldMOve_DeptId + databundleid + days + serialnumber + cGeneral.WorldMOve_Token;
                EncStr = cGeneral.ComputeSHA1(EncStr);
                List<WorldMoveProduct> wmlist = new List<WorldMoveProduct>();
                WorldMoveProduct add = new WorldMoveProduct();
                List<eSimWorldMoveProduct> wmlist_esim = new List<eSimWorldMoveProduct>();
                eSimWorldMoveProduct add_esim = new eSimWorldMoveProduct();
                if (ActivationType == 27)
                {
                    add.WmproductId = databundleid;
                    add.SimNum = serialnumber;
                    add.Day = days;
                    wmlist.Add(add);
                }
                if (ActivationType == 28)
                {
                    add_esim.WmproductId = databundleid;
                    add_esim.qty = 1;
                    wmlist_esim.Add(add_esim);
                }
                WorldMoveMerchantRequest obj = new WorldMoveMerchantRequest();
                obj.MerchantId = cGeneral.WorldMOve_MerchantId;
                obj.DeptId = cGeneral.WorldMOve_DeptId;
                obj.ProdList = wmlist;
                obj.EncStr = EncStr;
                obj.orderId = thirdorderid;
                jsonrequest = JsonConvert.SerializeObject(obj);
                string WorldAPIURL = cGeneral.WorldMOve_URLType == "0" ? cGeneral.WorldMOve_Test_URL : cGeneral.WorldMOve_Prod_URL;
                WorldAPIURL = WorldAPIURL + "mydeposit";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(WorldAPIURL);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = Convert.ToString(jsonrequest);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (Convert.ToString(httpResponse.StatusCode) == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        strresponse = streamReader.ReadToEnd();
                        if (strresponse != "")
                        {
                            var Json = JObject.Parse(strresponse);
                            orderid = Convert.ToString(Json["orderId"]);
                            if (orderid != "")
                            {
                                status = "Success";
                            }
                            else
                            {
                                status = "Failed : Something went wrong.";
                            }
                        }
                        else
                        {
                            status = "Failed : Something went wrong.";
                        }
                    }
                }
                else
                {
                    status = "Failed : Something went wrong.";
                }
            }
            catch (Exception Ex)
            {
                status = "Failed : Something went wrong." + Ex.Message.ToString();
            }
            return (status, orderid, download_url, jsonrequest, strresponse);
        }
        public string UpdatePendingRequest_Extension(int requestStatus, string Status, string addorder_orderid, string MSISDN, string addorder_download_url, string QRCodeURL, string addorder_jsonrequest, string addorder_strrespomnse, Int32 RequestID, Int32 PaymentID)
        {
            string result = "";
            string strResponse = "";
            try
            {
                string API = "UpdatePendingRequest_Extension";
                UpdatePendingRequest_ActivationDTO add = new UpdatePendingRequest_ActivationDTO()
                {
                    RequestStatus = requestStatus,
                    Status = Status,
                    AddOrder_OrderId = addorder_orderid,
                    MSISDN = MSISDN,
                    AddOrder_DownloadUrl = addorder_download_url,
                    QRCodeURL = QRCodeURL,
                    AddOrder_JsonRequest = addorder_jsonrequest,
                    AddOrder_StrResponse = addorder_strrespomnse,
                    RequestID = RequestID,
                    PaymentID = PaymentID,
                    LoginID = Convert.ToInt32(Session["LoginID"])
                };
                string strRequest = JsonConvert.SerializeObject(add);
                result = cGeneral.fnPostAPICall(API, strRequest);

                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strResponse = Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);
                    }
                    else
                    {
                        strResponse = "Activation failed : No matching request was found in the inventory.";
                    }
                }
                else
                {
                    strResponse = "Activation failed : No matching request was found in the inventory.";
                }
            }
            catch (Exception Ex)
            {
                strResponse = "Activation failed : No matching request was found in the inventory. " + Ex.Message.ToString();
            }
            return strResponse;
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