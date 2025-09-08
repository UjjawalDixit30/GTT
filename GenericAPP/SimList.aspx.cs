using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GenericAPP.Models;

namespace GenericAPP
{
    public partial class SimList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Session["LoginID"] != null)
                    {
                        hddnminindex.Value = "1";
                        hddnpagenumber.Value = "1";
                        ddlNumberofRowsCount.SelectedValue = "10";
                        hddnmaxindex.Value = ddlNumberofRowsCount.SelectedValue;

                        if (Session["Message"] != null)
                        {
                            ShowPopUpMsg(Session["Message"].ToString());
                            Session["Message"] = null;
                        }

                        string statusParam = Request.QueryString.Get("Status");
                        string didParam = Request.QueryString.Get("DID");

                        if (!string.IsNullOrEmpty(statusParam))
                        {
                            string status = cGeneral.Decrypt(statusParam);
                            GetSimList(status);
                        }
                        else if (!string.IsNullOrEmpty(didParam))
                        {
                            string did = cGeneral.Decrypt(didParam);
                            GetSimList(did);
                        }
                        else
                        {
                            GetSimList(string.Empty);
                        }
                    }
                    else
                    {
                        Response.Redirect("Login.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    ShowPopUpMsg(ex.Message);
                }
            }
           
        }
        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            try
            {
                hddnminindex.Value = "1";
                hddnpagenumber.Value = "1";
                hddnmaxindex.Value = Convert.ToString(ddlNumberofRowsCount.SelectedValue);
                string TextContext = Convert.ToString(txtTextContext.Value);
                GetSimList(TextContext);
            }
            catch (Exception Ex)
            {
                ShowPopUpMsg(Ex.Message.ToString());
            }
        }
        private void GetSimList(string TextContext)
        {
            try
            {
                var result = "";
                string API = "GetSimList?";
                Int32 MINID = Convert.ToInt32(hddnminindex.Value);
                Int32 MAXID = Convert.ToInt32(hddnmaxindex.Value);
                Int32 LoginID = Convert.ToInt32(Session["LoginID"]);
                Int32 PageNumber = Convert.ToInt32(hddnpagenumber.Value);
                var httpWebRequest = "LoginID=" + LoginID + "&TextContext=" + TextContext + "&MINID=" + MINID + "&MAXID=" + MAXID;
                result = cGeneral.fnGetAPICall(API, httpWebRequest);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                RepeaterSIMList.DataSource = ds.Tables[1];
                                RepeaterSIMList.DataBind();
                                RepeaterSIMList.Visible = true;
                                lblDataFound.Text = "";
                                controlcss();
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    int remainder = 0;
                                    Int32 TotalPageNumber = 0;

                                    Int32 TotalCount = Convert.ToInt32(ds.Tables[2].Rows[0]["TotalCount"]);
                                    Session["TotalCount"] = TotalCount;
                                    if (TotalCount != 0)
                                    {
                                        int quotient = Math.DivRem(TotalCount, Convert.ToInt32(ddlNumberofRowsCount.SelectedValue), out remainder);
                                        TotalPageNumber = quotient != 0 && remainder != 0 ? quotient + 1 : quotient != 0 && remainder == 0 ? quotient : quotient == 0 && remainder != 0 ? 1 : 0;
                                        lblPageDetails.Text = "page " + PageNumber + " of " + TotalPageNumber;
                                        divPageCount.Visible = true;
                                        btnPre.Visible = PageNumber == 1 ? false : true;
                                        btnnext.Visible = PageNumber == TotalPageNumber ? false : true;
                                    }
                                    else
                                    {
                                        lblPageDetails.Text = "";
                                        divPageCount.Visible = false;
                                    }
                                }
                                else
                                {
                                    lblPageDetails.Text = "";
                                    divPageCount.Visible = false;
                                }
                            }
                            else
                            {
                                RepeaterSIMList.DataSource = null;
                                RepeaterSIMList.DataBind();
                                RepeaterSIMList.Visible = false;
                                lblDataFound.Text = "No Data Found.";
                                lblPageDetails.Text = "";
                                divPageCount.Visible = false;
                            }
                        }
                        else
                        {
                            RepeaterSIMList.DataSource = null;
                            RepeaterSIMList.DataBind();
                            RepeaterSIMList.Visible = false;
                            lblDataFound.Text = "No Data Found.";
                            lblPageDetails.Text = "";
                            divPageCount.Visible = false;
                        }
                    }
                    else
                    {
                        RepeaterSIMList.DataSource = null;
                        RepeaterSIMList.DataBind();
                        RepeaterSIMList.Visible = false;
                        lblDataFound.Text = "No Data Found.";
                        lblPageDetails.Text = "";
                        divPageCount.Visible = false;
                    }
                }
                else
                {
                    RepeaterSIMList.DataSource = null;
                    RepeaterSIMList.DataBind();
                    RepeaterSIMList.Visible = false;
                    lblDataFound.Text = "No Data Found.";
                    lblPageDetails.Text = "";
                    divPageCount.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                RepeaterSIMList.DataSource = null;
                RepeaterSIMList.DataBind();
                RepeaterSIMList.Visible = false;
                lblDataFound.Text = "No Data Found. " + Ex.Message.ToString();
                lblPageDetails.Text = "";
                divPageCount.Visible = false;
            }
        }
        protected void controlcss()
        {
            try
            {
                for (int i = 0; i < RepeaterSIMList.Items.Count; i++)
                {
                    Label lblStatus = (Label)RepeaterSIMList.Items[i].FindControl("lblStatus");

                }
            }
            catch (Exception Ex)
            {
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("SimList.aspx", false);
        }
        protected void btnPre_Click(object sender, EventArgs e)
        {
            Int32 PageNumber = Convert.ToInt32(hddnpagenumber.Value);
            Int32 minID = Convert.ToInt32(hddnminindex.Value);
            Int32 maxID = Convert.ToInt32(hddnmaxindex.Value);
            hddnpagenumber.Value = Convert.ToString(PageNumber - 1);
            hddnminindex.Value = Convert.ToString(minID - Convert.ToInt32(ddlNumberofRowsCount.SelectedValue));
            hddnmaxindex.Value = Convert.ToString(maxID - Convert.ToInt32(ddlNumberofRowsCount.SelectedValue));
            int LoginID = Convert.ToInt32(Session["LoginID"]);
            string TextContext = Convert.ToString(txtTextContext.Value);
            if (Request.QueryString.Get("Status") != null)
            {
                string Status = cGeneral.Decrypt(Request.QueryString.Get("Status"));
                GetSimList(Status);
            }
            else if (Request.QueryString.Get("PurchaseID") != null)
            {
                string PurchaseID = cGeneral.Decrypt(Convert.ToString(Request.QueryString.Get("PurchaseID")));
                GetSimList(PurchaseID);
            }
            else
            {
                GetSimList("");
            }
        }
        protected void btnnext_Click(object sender, EventArgs e)
        {
            Int32 PageNumber = Convert.ToInt32(hddnpagenumber.Value);
            Int32 minID = Convert.ToInt32(hddnminindex.Value);
            Int32 maxID = Convert.ToInt32(hddnmaxindex.Value);
            hddnpagenumber.Value = Convert.ToString(PageNumber + 1);
            hddnminindex.Value = Convert.ToString(minID + Convert.ToInt32(ddlNumberofRowsCount.SelectedValue));
            hddnmaxindex.Value = Convert.ToString(maxID + Convert.ToInt32(ddlNumberofRowsCount.SelectedValue));
            int LoginID = Convert.ToInt32(Session["LoginID"]);
            string TextContext = Convert.ToString(txtTextContext.Value);
            if (Request.QueryString.Get("Status") != null)
            {
                string Status = cGeneral.Decrypt(Request.QueryString.Get("Status"));
                GetSimList(Status);
            }
            else if (Request.QueryString.Get("PurchaseID") != null)
            {
                string PurchaseID = cGeneral.Decrypt(Convert.ToString(Request.QueryString.Get("PurchaseID")));
                GetSimList(PurchaseID);
            }
            else
            {
                GetSimList("");
            }
        }
        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var result = "";
                string API = "GetSimListForExcel?";
                Int32 LoginID = Convert.ToInt32(Session["LoginID"]);
                var httpWebRequest = "LoginID=" + LoginID;
                result = cGeneral.fnGetAPICall(API, httpWebRequest);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            DataTable dtt = new DataTable();
                            dtt.TableName = "Export";
                            dtt.Columns.Add("S.No.");
                            dtt.Columns.Add("Serial Number");
                            dtt.Columns.Add("Status");
                            dtt.Columns.Add("Dealer");
                            dtt.Columns.Add("Purchase Number");
                            dtt.Columns.Add("MSISDN");
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                {
                                    DataRow dr = dtt.NewRow();
                                    dr["S.No."] = Convert.ToString(ds.Tables[1].Rows[i]["INDEXID"]);
                                    dr["Serial Number"] = Convert.ToString(ds.Tables[1].Rows[i]["SERIALNUMBER"]) + "F";
                                    dr["Status"] = Convert.ToString(ds.Tables[1].Rows[i]["STATUS"]);
                                    dr["Dealer"] = Convert.ToString(ds.Tables[1].Rows[i]["DEALERNAME"]);
                                    dr["Purchase Number"] = Convert.ToString(ds.Tables[1].Rows[i]["PURCHASENUMBER"]);
                                    dr["MSISDN"] = Convert.ToString(ds.Tables[1].Rows[i]["MSISDN"]);
                                    dtt.Rows.Add(dr);
                                    dtt.AcceptChanges();
                                }
                            }
                            if (dtt.Rows.Count > 0)
                            {
                                DataView view = new DataView(dtt);
                                DataTable dtExcel = view.ToTable("Selected", false, "S.No.", "Serial Number", "Status", "Dealer", "Purchase Number", "MSISDN");
                                if (dtExcel.Rows.Count > 0)
                                {
                                    string filename = "SimList_" + DateTime.Now.Date.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".xls";
                                    System.IO.StringWriter tw = new System.IO.StringWriter();
                                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                                    GridView grdView = new GridView();
                                    grdView.DataSource = dtExcel;
                                    grdView.DataBind();
                                    grdView.RenderControl(hw);
                                    Response.ContentType = "application/vnd.ms-excel";
                                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                                    this.EnableViewState = false;
                                    Response.Write(tw.ToString());
                                    Response.End();
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
        protected void btnReset_Click1(object sender, EventArgs e)
        {
            Response.Redirect("SimList.aspx", false);
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