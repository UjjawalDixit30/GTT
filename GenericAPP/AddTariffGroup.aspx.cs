using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    public partial class AddTariffGroup : System.Web.UI.Page
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
                        if (Request.QueryString.Get("TGN") != null && Request.QueryString.Get("TGI") != null)
                        {
                            string TGN = cGeneral.Decrypt(Convert.ToString(Request.QueryString.Get("TGN")));
                            int TGI = Convert.ToInt32(cGeneral.Decrypt(Convert.ToString(Request.QueryString.Get("TGI"))));
                            hddnActionType.Value = "2";
                            txtTariffGroupName.Text = TGN;
                            hddnTariffGroupID.Value = Convert.ToString(TGI);
                        }
                        else
                        {
                            hddnActionType.Value = "1";
                            hddnTariffGroupID.Value = "0";
                            txtTariffGroupName.Text = "";
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
        protected void btnDownloadPlanList_Click(object sender, EventArgs e)
        {
            try
            {
                var result = "";

                string API = "GetTariffGroup?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                int tariffGroupId = Convert.ToInt32(hddnTariffGroupID.Value);
                int ActionType = Convert.ToString(hddnActionType.Value) == "1" ? 3 : Convert.ToString(hddnActionType.Value) == "2" ? 4 : 0;
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
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    string filename = "PlanList_" + DateTime.Now.Date.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + ".xls";
                                    System.Data.DataTable dt = ds.Tables[1];
                                    StringBuilder csvContent = new StringBuilder();
                                    foreach (System.Data.DataColumn column in dt.Columns)
                                    {
                                        csvContent.Append(column.ColumnName + ",");
                                    }
                                    csvContent.AppendLine();
                                    foreach (System.Data.DataRow row in dt.Rows)
                                    {
                                        foreach (var item in row.ItemArray)
                                        {
                                            csvContent.Append(item.ToString().Replace(",", " ") + ",");
                                        }
                                        csvContent.AppendLine();
                                    }
                                    Response.Clear();
                                    Response.Buffer = true;
                                    Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".csv");
                                    Response.ContentType = "application/text";
                                    Response.Output.Write(csvContent.ToString());
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                string API = "AddUpdateTariffGroup?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
                if (FileUpload != null)
                {
                    string Uploadresponse = UploadFile(FileUpload);
                    if (Uploadresponse == "Success")
                    {
                        if (ViewState["ObjPlanList"] != null)
                        {
                            DataTable dtPlanList = (DataTable)ViewState["ObjPlanList"];
                            if (dtPlanList.Rows.Count > 0)
                            {
                                List<PlanItem_DTO> planItem_DTOs = new List<PlanItem_DTO>();
                                for (int i = 0; i < dtPlanList.Rows.Count; i++)
                                {
                                    PlanItem_DTO Additem = new PlanItem_DTO();
                                    Additem.PlanID = Convert.ToInt32(dtPlanList.Rows[i]["PlanID"]);
                                    Additem.NetworkID = Convert.ToString(dtPlanList.Rows[i]["Network"]).ToUpper() == "SKYGO" ? 1 : 2;
                                    Additem.ExtensionCharge = Convert.ToDecimal(dtPlanList.Rows[i]["ActivationPrice"]);
                                    Additem.ActivationPrice = Convert.ToDecimal(dtPlanList.Rows[i]["ExtensionPrice"]);
                                    planItem_DTOs.Add(Additem);
                                }
                                if (planItem_DTOs.Count > 0)
                                {
                                    AddTariffGroup_DTO Add = new AddTariffGroup_DTO();
                                    Add.LoginID = LoginID;
                                    Add.PlanItem_DTO = planItem_DTOs;
                                    Add.ActionType = Convert.ToInt32(hddnActionType.Value);
                                    Add.TariffGroupID = Convert.ToInt32(hddnTariffGroupID.Value);
                                    Add.TariffGroupName = Convert.ToString(txtTariffGroupName.Text);
                                    string strRequest = JsonConvert.SerializeObject(Add);
                                    result = cGeneral.fnPostAPICall(API, strRequest);
                                    System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(result);
                                    if (ds != null)
                                    {
                                        if (ds.Tables.Count > 0)
                                        {
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                                                {
                                                    Session["Message"] = "Tariff group created successfully !!!";
                                                }
                                                else
                                                {
                                                    Session["Message"] = "Failed : " + Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);
                                                }
                                            }
                                            else
                                            {
                                                Session["Message"] = "Failed : Something went wrong, please try again.";
                                            }
                                        }
                                        else
                                        {
                                            Session["Message"] = "Failed : Something went wrong, please try again.";
                                        }
                                    }
                                    else
                                    {
                                        Session["Message"] = "Failed : Something went wrong, please try again.";
                                    }
                                }
                                else
                                {
                                    Session["Message"] = "Failed : Something went wrong, please try again.";
                                }
                            }
                            else
                            {
                                Session["Message"] = "Failed : Something went wrong, please try again.";
                            }
                        }
                        else
                        {
                            Session["Message"] = "Failed : Something went wrong, please try again.";
                        }
                    }
                    else
                    {
                        Session["Message"] = "Failed : Something went wrong, please try again.";
                    }
                }
                else
                {
                    Session["Message"] = "Failed : Something went wrong, please try again.";
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = "Failed : Something went wrong, please try again. " + Ex.Message.ToString();
            }
            Response.Redirect("TariffGroupList.aspx", false);
        }
        protected string UploadFile(FileUpload FuBulkDetails)
        {
            try
            {
                if (FuBulkDetails.HasFile)
                {
                    if (FuBulkDetails.FileName.Contains(".csv"))
                    {
                        string strPath = Server.MapPath("Design/PlanListFiles") + "/" + FuBulkDetails.FileName;
                        FuBulkDetails.SaveAs(strPath);
                        ViewState["ObjPlanList"] = CSVTODatTableMSISDNPurchaseBulk(strPath, (DataTable)ViewState["ObjPlanList"]);
                        return "Success";
                    }
                    else
                    {
                        return "Please select a .csv file";
                    }
                }
                else
                {
                    return "Please select a file for upload";
                }
            }
            catch (Exception Ex)
            {
                return Ex.Message.ToString();
            }
        }
        protected DataTable CSVTODatTableMSISDNPurchaseBulk(string CSVPath, DataTable table)
        {
            try
            {
                StreamReader DataStreamReader = new StreamReader(CSVPath);
                string[] columnNames = DataStreamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.None);
                table = new DataTable("PlanList");
                DataColumn column = null;
                for (int i = 0; i < columnNames.Length; i++)
                {
                    column = new DataColumn(columnNames[i]);
                    table.Columns.Add(column);
                }
                while (!DataStreamReader.EndOfStream)
                {
                    int iRowIndex = table.Rows.Count;
                    table.Rows.Add(DataStreamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.None));
                    table.AcceptChanges();
                }
                DataStreamReader.Close();
                return table;
            }
            catch (Exception Ex)
            {
                return null;
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