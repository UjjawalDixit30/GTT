using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GenericAPP.Models;

namespace GenericAPP
{
    public partial class ViewTariffGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["LoginID"] != null)
                    {
                        if (Request.QueryString.Get("TGI") != null)
                        {
                            string TGI = Convert.ToString(Request.QueryString.Get("TGI"));
                            int tariffGroupId = Convert.ToInt32(cGeneral.Decrypt(TGI));
                            GetTariffGroup(tariffGroupId);
                        }
                        else
                        {
                            Session["Message"] = "Something went wrong, please try again.";
                            Response.Redirect("TariffGroupList.aspx", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("Login.aspx", false);
                    }
                }
                catch (Exception Ex)
                {
                    Session["Message"] = "Something went wrong, please try again. " + Ex.Message.ToString();
                    Response.Redirect("TariffGroupList.aspx", false);
                }
            }
        }
        public void GetTariffGroup(int tariffGroupId)
        {
            try
            {
                var result = "";
                int ActionType = 4;
                string API = "GetTariffGroup?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                string Data = "LoginID=" + LoginID + "&tariffGroupId=" + tariffGroupId + "&ActionType=" + ActionType;
                result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                            {
                                txtTariffGroupName.Text = Convert.ToString(ds.Tables[0].Rows[0]["TariffGroup"]);
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    RepeaterPlanList.DataSource = ds.Tables[1];
                                    RepeaterPlanList.DataBind();
                                    RepeaterPlanList.Visible = true;
                                    lblDataFound.Text = "";
                                }
                                else
                                {
                                    RepeaterPlanList.DataSource = null;
                                    RepeaterPlanList.DataBind();
                                    RepeaterPlanList.Visible = false;
                                    lblDataFound.Text = "No Data Found !!!";
                                }
                            }
                            else
                            {
                                RepeaterPlanList.DataSource = null;
                                RepeaterPlanList.DataBind();
                                RepeaterPlanList.Visible = false;
                                lblDataFound.Text = "No Data Found !!!";
                            }
                        }
                        else
                        {
                            RepeaterPlanList.DataSource = null;
                            RepeaterPlanList.DataBind();
                            RepeaterPlanList.Visible = false;
                            lblDataFound.Text = "No Data Found !!!";
                        }
                    }
                    else
                    {
                        RepeaterPlanList.DataSource = null;
                        RepeaterPlanList.DataBind();
                        RepeaterPlanList.Visible = false;
                        lblDataFound.Text = "No Data Found !!!";
                    }
                }
                else
                {
                    RepeaterPlanList.DataSource = null;
                    RepeaterPlanList.DataBind();
                    RepeaterPlanList.Visible = false;
                    lblDataFound.Text = "No Data Found !!!";
                }
            }
            catch (Exception ex)
            {
                RepeaterPlanList.DataSource = null;
                RepeaterPlanList.DataBind();
                RepeaterPlanList.Visible = false;
                lblDataFound.Text = "No Data Found," + ex.Message.ToString() + " !!!";
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