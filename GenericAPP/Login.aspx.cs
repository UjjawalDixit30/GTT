using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using GenericAPP.Models;
using System.Web.UI.WebControls;
using static GenericAPP.Models.GenricDTO;
using Newtonsoft.Json;

namespace GenericAPP
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                }
                catch (Exception Ex)
                {
                    ShowPopUpMsg(Ex.Message.ToString());
                }
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string API = "Login";
                Login_DTO Add = new Login_DTO();
                Add.RequestedIP = cGeneral.GetSystemIP();
                Add.Username = Convert.ToString(txtemail.Value);
                Add.RequestedOS = cGeneral.GetUserPlatform(Request);
                Add.RequestedDevice = cGeneral.GetDeviceInformation(Request);
                Add.Password = cGeneral.Encrypt(Convert.ToString(txtpassword.Value));

                var jsonRequest = JsonConvert.SerializeObject(Add);
                string Result = cGeneral.fnPostAPICall(API, jsonRequest);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                            {
                                Session["RoleID"] = Convert.ToString(ds.Tables[0].Rows[0]["RoleID"]);
                                Session["LoginID"] = Convert.ToString(ds.Tables[0].Rows[0]["LoginID"]);
                                Session["Username"] = Convert.ToString(ds.Tables[0].Rows[0]["Username"]);
                                Session["DealerID"] = Convert.ToString(ds.Tables[0].Rows[0]["DealerID"]);
                                Session["DealerName"] = Convert.ToString(ds.Tables[0].Rows[0]["DealerName"]);
                                Session["AccountType"] = Convert.ToString(ds.Tables[0].Rows[0]["AccountType"]);
                                Session["RoleDetails"] = Convert.ToString(ds.Tables[0].Rows[0]["RoleDetails"]);
                                Session["ContactNumber"] = Convert.ToString(ds.Tables[0].Rows[0]["ContactNumber"]);
                                Session["ContactEmailID"] = Convert.ToString(ds.Tables[0].Rows[0]["ContactEmailID"]);
                                Session["AccountBalance"] = Convert.ToString(ds.Tables[0].Rows[0]["AccountBalance"]);
                                Session["AccountTypeDetails"] = Convert.ToString(ds.Tables[0].Rows[0]["AccountTypeDetails"]);
                                Response.Redirect("Dashboard.aspx", false);
                            }
                            else
                            {
                                txtemail.Value = "";
                                txtpassword.Value = "";
                                ShowPopUpMsg(Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]));
                            }
                        }
                        else
                        {
                            txtemail.Value = "";
                            txtpassword.Value = "";
                            ShowPopUpMsg("Invalid username/password, Please check and try again.");
                        }
                    }
                    else
                    {
                        txtemail.Value = "";
                        txtpassword.Value = "";
                        ShowPopUpMsg("Invalid username/password, Please check and try again.");
                    }
                }
                else
                {
                    txtemail.Value = "";
                    txtpassword.Value = "";
                    ShowPopUpMsg("Invalid username/password, Please check and try again.");
                }
            }
            catch (Exception Ex)
            {
                txtemail.Value = "";
                txtpassword.Value = "";
                ShowPopUpMsg("Invalid mobile number/username/password, Please check and try again. " + Ex.Message.ToString());
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