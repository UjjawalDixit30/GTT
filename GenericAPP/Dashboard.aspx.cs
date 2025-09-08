using GenericAPP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static GenericAPP.Models.GenricDTO;

namespace GenericAPP
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoginID"] != null)
                {
                    spnDealerName.InnerText = Convert.ToString(Session["DealerName"]);
                    spnAccountBalance.InnerText = Convert.ToString(Session["AccountBalance"]);
                    GetDashboardDetails();
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
        protected void GetDashboardDetails()
        {
            var Result = "";
            try
            {
                int ActionType = 1;
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(cGeneral.BaseURL + "GetDashboardDetails?LoginID=" + LoginID + "&ActionType=" + ActionType);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (Convert.ToString(httpResponse.StatusCode) == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        Result = streamReader.ReadToEnd();
                    }
                    System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                                {
                                    int freeSimsCount = Convert.ToInt32(ds.Tables[0].Rows[0]["FreeSims"]);
                                    int activeSimsCount = Convert.ToInt32(ds.Tables[0].Rows[0]["ActiveSims"]);
                                    int lostSimsCount = Convert.ToInt32(ds.Tables[0].Rows[0]["LostSims"]);
                                    int ActivationDataCount = Convert.ToInt32(ds.Tables[0].Rows[0]["ActivationData"]);
                                    int ExtensionDataCount = Convert.ToInt32(ds.Tables[0].Rows[0]["ExtensionData"]);

                                    FreeSims.InnerText = freeSimsCount.ToString();
                                    ActiveSims.InnerText = activeSimsCount.ToString();
                                    LostSims.InnerText = lostSimsCount.ToString();

                                    PieChartDataDTO Add = new PieChartDataDTO();
                                    Add.ActivationData = ActivationDataCount;
                                    Add.ExtensionData = ExtensionDataCount;
                                    Add.FreeSims = freeSimsCount;
                                    Add.ActiveSims = activeSimsCount;
                                    Add.LostSims = lostSimsCount;
                                    Session["PieChartData"] = Add;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
        }
        private static object GetActivationStatistics(int loginID)
        {
            var result = "";
            try
            {
                int actionType = 2;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(cGeneral.BaseURL + "GetDashboardDetails?LoginID=" + loginID + "&ActionType=" + actionType);
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
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            return new
                            {
                                Success = Convert.ToInt32(ds.Tables[0].Rows[0]["SuccessCount"] ?? 0),
                                Pending = Convert.ToInt32(ds.Tables[0].Rows[0]["PendingCount"] ?? 0),
                                Failed = Convert.ToInt32(ds.Tables[0].Rows[0]["FailedCount"] ?? 0)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new
            {
                Success = 0,
                Pending = 0,
                Failed = 0
            };
        }
        private static object GetExtensionStatistics(int loginID)
        {
            var result = "";
            try
            {
                int actionType = 3;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(cGeneral.BaseURL + "GetDashboardDetails?LoginID=" + loginID + "&ActionType=" + actionType);
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
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            return new
                            {
                                Success = Convert.ToInt32(ds.Tables[0].Rows[0]["SuccessCount"] ?? 0),
                                Pending = Convert.ToInt32(ds.Tables[0].Rows[0]["PendingCount"] ?? 0),
                                Failed = Convert.ToInt32(ds.Tables[0].Rows[0]["FailedCount"] ?? 0)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new
            {
                Success = 0,
                Pending = 0,
                Failed = 0
            };
        }
        [WebMethod]
        public static object GetActivationStats()
        {
            try
            {
                if (HttpContext.Current.Session["LoginID"] != null)
                {
                    int loginID = Convert.ToInt32(HttpContext.Current.Session["LoginID"]);
                    return GetActivationStatistics(loginID);
                }
            }
            catch (Exception ex)
            {

            }
            return new
            {
                Success = 0,
                Pending = 0,
                Failed = 0
            };
        }
        [WebMethod]
        public static object GetExtensionStats()
        {
            try
            {
                if (HttpContext.Current.Session["LoginID"] != null)
                {
                    int loginID = Convert.ToInt32(HttpContext.Current.Session["LoginID"]);
                    return GetExtensionStatistics(loginID);
                }
            }
            catch (Exception ex)
            {

            }
            return new
            {
                Success = 0,
                Pending = 0,
                Failed = 0
            };
        }
        [WebMethod]
        public static object GetRevenueDetails()
        {
            string result = "";
            try
            {
                int actionType = 4;
                if (HttpContext.Current.Session["LoginID"] != null)
                {
                    int loginID = Convert.ToInt32(HttpContext.Current.Session["LoginID"]);
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(cGeneral.BaseURL + "GetDashboardDetails?LoginID=" + loginID + "&ActionType=" + actionType);
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
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                            {
                                return new
                                {
                                    TopupRefund = Convert.ToInt32(ds.Tables[0].Rows[0]["TopupRefund"] ?? 0),
                                    Activation = Convert.ToInt32(ds.Tables[0].Rows[0]["Activation"] ?? 0),
                                    Recharge = Convert.ToInt32(ds.Tables[0].Rows[0]["Recharge"] ?? 0)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return new
            {
                TopupRefund = 0,
                Activation = 0,
                Recharge = 0
            };
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