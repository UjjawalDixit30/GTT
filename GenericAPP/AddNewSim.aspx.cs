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
    public partial class AddNewSim : System.Web.UI.Page
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
                        txtPurchaseCode.Text = cGeneral.TXNSerial + "PNS" + cGeneral.GetTransactionID();
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
        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TypeValue = Convert.ToInt32(ddltype.SelectedValue);

            if (TypeValue == 1)
            {
                SingleUploadDiv.Visible = true;
                bulkUploadDiv.Visible = false;
            }
            else
            {
                SingleUploadDiv.Visible = false;
                bulkUploadDiv.Visible = true;
            }
        }
        protected void btnAddSim_Click(object sender, EventArgs e)
        {
            try
            {

                string DeviceNumber = Convert.ToString(txtSimNumber.Text);
                DataTable dtValidateSerialNumber = new DataTable();
                dtValidateSerialNumber.Columns.Add("SerialNumber", typeof(string));
                dtValidateSerialNumber.Columns.Add("Status", typeof(string));
                dtValidateSerialNumber.Columns.Add("Reason", typeof(string));



                for (int i = 0; i < rptDeviceNumberList.Items.Count; i++)
                {
                    Label lblStatus = (Label)rptDeviceNumberList.Items[i].FindControl("lblStatus");
                    Label lblReason = (Label)rptDeviceNumberList.Items[i].FindControl("lblReason");
                    Label lblDeviceNumber = (Label)rptDeviceNumberList.Items[i].FindControl("lblSerialNumber");
                    if (lblDeviceNumber.Text == txtSimNumber.Text)
                    {
                        ShowPopUpMsg("Serial number already added.");
                        return;
                    }
                    else
                    {
                        dtValidateSerialNumber.Rows.Add(lblDeviceNumber.Text, lblStatus.Text, lblReason.Text);
                    }
                }
                if (DeviceNumber.Length < 18)
                {
                    dtValidateSerialNumber.Rows.Add(DeviceNumber, "No", "Device Number Length Less Then 18");
                }
                else
                {
                    string ValidationResponse = ValidateDeviceNumber(DeviceNumber);
                    if (ValidationResponse == "Success")
                    {
                        dtValidateSerialNumber.Rows.Add(DeviceNumber, "Yes", ValidationResponse);
                    }
                    else
                    {
                        dtValidateSerialNumber.Rows.Add(DeviceNumber, "No", ValidationResponse);
                    }
                }
                rptDeviceNumberList.DataSource = null;
                rptDeviceNumberList.DataBind();
                if (dtValidateSerialNumber.Rows.Count > 0)
                {
                    rptDeviceNumberList.DataSource = dtValidateSerialNumber;
                    rptDeviceNumberList.DataBind();
                    RepeaterDiv.Visible = true;
                    SubmitDiv.Visible = true;
                }
                else
                {
                    rptDeviceNumberList.DataSource = null;
                    rptDeviceNumberList.DataBind();
                    RepeaterDiv.Visible = false;
                    SubmitDiv.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                rptDeviceNumberList.DataSource = null;
                rptDeviceNumberList.DataBind();
                RepeaterDiv.Visible = false;
                SubmitDiv.Visible = false;
            }
        }
        protected string ValidateDeviceNumber(string SerialNumber)
        {
            var Result = "";
            try
            {
                int NetworkID = 0;
                int ActionType = 1;
                string API = "ValidateDeviceNumber?";
                int LoginID = Convert.ToInt32(Session["LoginID"]);
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
                                return "Success";
                            }
                            else if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "1")
                            {
                                return Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);
                            }
                            else
                            {
                                return "Failed : Serial number not valid.";
                            }
                        }
                        else
                        {
                            return "Failed : Serial number not valid.";
                        }
                    }
                    else
                    {
                        return "Failed : Serial number not valid.";
                    }
                }
                else
                {
                    return "Failed : Serial number not valid.";
                }
            }
            catch (Exception Ex)
            {
                return "Failed : Serial number not valid" + Ex.Message.ToString() + ".";
            }
        }
        protected void rptDeviceNumberList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                int IndexID = Convert.ToInt32(e.CommandArgument);
                DataTable dtValidateSerialNumber = new DataTable();
                dtValidateSerialNumber.Columns.Add("SerialNumber", typeof(string));
                dtValidateSerialNumber.Columns.Add("Status", typeof(string));
                dtValidateSerialNumber.Columns.Add("Reason", typeof(string));
                for (int i = 0; i < rptDeviceNumberList.Items.Count; i++)
                {
                    Label lblStatus = (Label)rptDeviceNumberList.Items[IndexID].FindControl("lblStatus");
                    Label lblReason = (Label)rptDeviceNumberList.Items[IndexID].FindControl("lblReason");
                    LinkButton lnkDelete = (LinkButton)rptDeviceNumberList.Items[IndexID].FindControl("lnkDelete");
                    Label lblSerialNumber = (Label)rptDeviceNumberList.Items[IndexID].FindControl("lblSerialNumber");
                    if (IndexID != i)
                    {
                        dtValidateSerialNumber.Rows.Add(lblSerialNumber.Text, lblStatus.Text, lblReason.Text);
                    }
                }
                if (dtValidateSerialNumber.Rows.Count > 0)
                {
                    rptDeviceNumberList.DataSource = dtValidateSerialNumber;
                    rptDeviceNumberList.DataBind();
                    RepeaterDiv.Visible = true;
                    SubmitDiv.Visible = true;
                }
                else
                {
                    rptDeviceNumberList.DataSource = null;
                    rptDeviceNumberList.DataBind();
                    RepeaterDiv.Visible = false;
                    SubmitDiv.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                rptDeviceNumberList.DataSource = null;
                rptDeviceNumberList.DataBind();
                RepeaterDiv.Visible = false;
                SubmitDiv.Visible = false;
                ShowPopUpMsg("Something went wrong, please try again." + Ex.Message.ToString() + "");
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtValidateSerialNumber = new DataTable();
                dtValidateSerialNumber.Columns.Add("SerialNumber", typeof(string));
                dtValidateSerialNumber.Columns.Add("Status", typeof(string));
                dtValidateSerialNumber.Columns.Add("Reason", typeof(string));
                if (BulkUpload != null)
                {
                    string Uploadresponse = UploadFile(BulkUpload);
                    if (Uploadresponse == "Success")
                    {
                        if (ViewState["ObjSerialNumber"] != null)
                        {
                            DataTable dtSerialNumber = (DataTable)ViewState["ObjSerialNumber"];
                            if (dtSerialNumber.Rows.Count > 0)
                            {
                                var duplicates = dtSerialNumber.AsEnumerable().GroupBy(r => r[0]).Where(gr => gr.Count() > 1);
                                if (duplicates.Any())
                                {
                                    ShowPopUpMsg("Duplicate data in csv file.");
                                    return;
                                }
                                for (int i = 0; i < dtSerialNumber.Rows.Count; i++)
                                {
                                    string SerialNumber = Convert.ToString(dtSerialNumber.Rows[i]["SerialNumber"]);
                                    if (SerialNumber.Length < 18)
                                    {
                                        dtValidateSerialNumber.Rows.Add(SerialNumber, "No", "Device Number Length Less Then 18");
                                    }
                                    else
                                    {
                                        string ValidationResponse = ValidateDeviceNumber(SerialNumber);
                                        if (ValidationResponse == "Success")
                                        {
                                            dtValidateSerialNumber.Rows.Add(SerialNumber, "Yes", ValidationResponse);
                                        }
                                        else
                                        {
                                            dtValidateSerialNumber.Rows.Add(SerialNumber, "No", ValidationResponse);
                                        }
                                    }
                                }
                                rptDeviceNumberList.DataSource = dtValidateSerialNumber;
                                rptDeviceNumberList.DataBind();
                                RepeaterDiv.Visible = true;
                                SubmitDiv.Visible = true;
                            }
                            else
                            {
                                rptDeviceNumberList.DataSource = null;
                                rptDeviceNumberList.DataBind();
                                RepeaterDiv.Visible = false;
                                SubmitDiv.Visible = false;
                                ShowPopUpMsg("Something went wrong, no data found.");
                            }
                        }
                        else
                        {
                            rptDeviceNumberList.DataSource = null;
                            rptDeviceNumberList.DataBind();
                            RepeaterDiv.Visible = false;
                            SubmitDiv.Visible = false;
                            ShowPopUpMsg("Something went wrong, no data found.");
                        }
                    }
                    else
                    {
                        rptDeviceNumberList.DataSource = null;
                        rptDeviceNumberList.DataBind();
                        RepeaterDiv.Visible = false;
                        SubmitDiv.Visible = false;
                        ShowPopUpMsg("Something went wrong, no data found.");
                    }
                }
                else
                {
                    rptDeviceNumberList.DataSource = null;
                    rptDeviceNumberList.DataBind();
                    RepeaterDiv.Visible = false;
                    SubmitDiv.Visible = false;
                    ShowPopUpMsg("Something went wrong, no data found.");
                }
            }
            catch (Exception Ex)
            {
                rptDeviceNumberList.DataSource = null;
                rptDeviceNumberList.DataBind();
                RepeaterDiv.Visible = false;
                SubmitDiv.Visible = false;
                ShowPopUpMsg("Something went wrong, no data found, " + Ex.Message.ToString() + ".");
            }
        }
        protected string UploadFile(FileUpload FuBulkDetails)
        {
            try
            {
                if (FuBulkDetails.HasFile)
                {
                    if (FuBulkDetails.FileName.Contains(".csv"))
                    {
                        string strPath = Server.MapPath("Design/InventoryFiles") + "/" + FuBulkDetails.FileName;
                        FuBulkDetails.SaveAs(strPath);
                        ViewState["ObjSerialNumber"] = CSVTODatTableMSISDNPurchaseBulk(strPath, (DataTable)ViewState["ObjSerialNumber"]);
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
                int iInvalidFileFormat = 0;
                StreamReader DataStreamReader = new StreamReader(CSVPath);
                string[] columnNames = DataStreamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.None);
                table = new DataTable("SerialNumberList");
                DataColumn column = null;
                for (int i = 0; i < columnNames.Length; i++)
                {
                    column = new DataColumn(columnNames[i]);
                    table.Columns.Add(column);
                }
                while (!DataStreamReader.EndOfStream)
                {
                    int iRowIndex = table.Rows.Count;
                    if (columnNames.Length != 1)
                    {
                        iInvalidFileFormat = 1;
                        ShowPopUpMsg("Serial Number bulk upload File should contains 1 columns,please check your file");
                        break;
                    }
                    table.Rows.Add(DataStreamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.None));
                    if (iInvalidFileFormat == 1)
                    {
                        return null;
                    }
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Result = "";
            string API = "PurchaseSerialNumbers";
            try
            {
                List<SimNumberDTO> obj = new List<SimNumberDTO>();
                for (int i = 0; i < rptDeviceNumberList.Items.Count; i++)
                {
                    Label lblStatus = (Label)rptDeviceNumberList.Items[i].FindControl("lblStatus");
                    Label lblDeviceNumber = (Label)rptDeviceNumberList.Items[i].FindControl("lblSerialNumber");
                    if (lblStatus.Text == "Yes")
                    {
                        SimNumberDTO Add1 = new SimNumberDTO();
                        Add1.SerialNumber = Convert.ToString(lblDeviceNumber.Text);
                        obj.Add(Add1);
                    }
                }
                if (obj.Count > 0)
                {
                    PurchaseSimDTO Add = new PurchaseSimDTO();
                    Add.SerialNumber = obj;
                    Add.LoginID = Convert.ToInt32(Session["LoginID"]);
                    Add.NetworkID = Convert.ToInt32(ddlNetwork.SelectedValue);
                    Add.PurchaseCode = Convert.ToString(txtPurchaseCode.Text);
                    var jsonRequest = JsonConvert.SerializeObject(Add);
                    Result = cGeneral.fnPostAPICall(API, jsonRequest);
                    System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                                {
                                    Session["Message"] = "SIM Added successfully.";
                                }
                                else
                                {
                                    Session["Message"] = "Something went wrong, SIM purchase failed.";
                                }
                            }
                            else
                            {
                                Session["Message"] = "Something went wrong, SIM purchase failed.";
                            }
                        }
                        else
                        {
                            Session["Message"] = "Something went wrong, SIM purchase failed.";
                        }
                    }
                    else
                    {
                        Session["Message"] = "Something went wrong, SIM purchase failed.";
                    }
                }
                else
                {
                    Session["Message"] = "Please enter valid SIM number for upload inventory.";
                }
            }
            catch (Exception Ex)
            {
                Session["Message"] = Ex.Message.ToString();
            }
            Response.Redirect("PurchaseSims.aspx", false);
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