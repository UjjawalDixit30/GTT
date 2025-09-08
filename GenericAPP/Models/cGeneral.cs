using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GenericAPP.Models
{
    public class cGeneral
    {
        public static string WorldMOve_Token = ConfigurationManager.AppSettings.Get("WorldMOve_Token");
        public static string WorldMOve_DeptId = ConfigurationManager.AppSettings.Get("WorldMOve_DeptId");
        public static string WorldMOve_URLType = ConfigurationManager.AppSettings.Get("WorldMOve_URLType");
        public static string WorldMOve_Prod_URL = ConfigurationManager.AppSettings.Get("WorldMOve_Prod_URL");
        public static string WorldMOve_Test_URL = ConfigurationManager.AppSettings.Get("WorldMOve_Test_URL");
        public static string WorldMOve_MerchantId = ConfigurationManager.AppSettings.Get("WorldMOve_MerchantId");

        public static string CMI_ID = ConfigurationManager.AppSettings.Get("CMI_ID");
        public static string CMI_TYPE = ConfigurationManager.AppSettings.Get("CMI_TYPE");
        public static string qrcodeURL = ConfigurationManager.AppSettings.Get("qrcodeURL");

        public static string IsSSL = ConfigurationManager.AppSettings.Get("IsSSL");
        public static string BaseURL = ConfigurationManager.AppSettings.Get("BaseURL");
        public static string LoginURL = ConfigurationManager.AppSettings.Get("LoginURL");
        public static string TXNSerial = ConfigurationManager.AppSettings.Get("TXNSerial");
        public static string Maintenance = ConfigurationManager.AppSettings.Get("Maintenance");
        public static string CompanyName = ConfigurationManager.AppSettings.Get("CompanyName");
        public static string ProcessingPercent = ConfigurationManager.AppSettings.Get("ProcessingPercent");

        public static string SMTP_Host = ConfigurationManager.AppSettings.Get("SMTP_Host");
        public static string SMTP_Port = ConfigurationManager.AppSettings.Get("SMTP_Port");
        public static string SMTP_Email = ConfigurationManager.AppSettings.Get("SMTP_EmailID");
        public static string SMTP_Password = ConfigurationManager.AppSettings.Get("SMTP_Password");
        public static string SMTP_EnableSsl = ConfigurationManager.AppSettings.Get("SMTP_EnableSsl");

        public static string paypalemail = ConfigurationManager.AppSettings.Get("paypalemail");
        public static string AddFundFailedURL = ConfigurationManager.AppSettings.Get("AddFundFailedURL");
        public static string AddFundSuccessURL = ConfigurationManager.AppSettings.Get("AddFundSuccessURL");

        public static void SendMail(string SendTo, string Subject, string Body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                string MailAddress = SMTP_Email;
                string PassWord = SMTP_Password;
                mail.From = new MailAddress(MailAddress);
                mail.To.Add(SendTo);
                TimeSpan ts = new TimeSpan(8, 0, 0);
                mail.Subject = Subject + " " + DateTime.UtcNow.Subtract(ts).ToString();
                mail.Body = Body.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpServer.Host = SMTP_Host;
                SmtpServer.Port = Convert.ToInt32(SMTP_Port);
                SmtpServer.Credentials = new System.Net.NetworkCredential(MailAddress, PassWord);
                SmtpServer.EnableSsl = Convert.ToBoolean(SMTP_EnableSsl);
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "CIRCUMFRANCES6546753";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "CIRCUMFRANCES6546753";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string GetSystemIP()
        {
            try
            {
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        return addresses[0];
                    }
                }
                return context.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string GetUserLocation()
        {
            try
            {
                string ip = "61.247.235.72";
                string url = $"http://ip-api.com/json/{ip}";

                using (var wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0";
                    string json = wc.DownloadString(url);
                    dynamic geo = JsonConvert.DeserializeObject<dynamic>(json);
                    string city = geo.city;
                    string region = geo.regionName;
                    string country = geo.country;
                    return $"{city}, {region}, {country}";
                }
            }
            catch
            {
                return "Unknown Location";
            }
        }
        public static String GetUserPlatform(HttpRequest request)
        {
            var ua = request.UserAgent;

            if (ua.Contains("Android"))
                return string.Format("Android {0}", GetMobileVersion(ua, "Android"));

            if (ua.Contains("iPad"))
                return string.Format("iPad OS {0}", GetMobileVersion(ua, "OS"));

            if (ua.Contains("iPhone"))
                return string.Format("iPhone OS {0}", GetMobileVersion(ua, "OS"));

            if (ua.Contains("Linux") && ua.Contains("KFAPWI"))
                return "Kindle Fire";

            if (ua.Contains("RIM Tablet") || (ua.Contains("BB") && ua.Contains("Mobile")))
                return "Black Berry";

            if (ua.Contains("Windows Phone"))
                return string.Format("Windows Phone {0}", GetMobileVersion(ua, "Windows Phone"));

            if (ua.Contains("Mac OS"))
                return "Mac OS";

            if (ua.Contains("Windows NT 5.1") || ua.Contains("Windows NT 5.2"))
                return "Windows XP";

            if (ua.Contains("Windows NT 6.0"))
                return "Windows Vista";

            if (ua.Contains("Windows NT 6.1"))
                return "Windows 7";

            if (ua.Contains("Windows NT 6.2"))
                return "Windows 8";

            if (ua.Contains("Windows NT 6.3"))
                return "Windows 8.1";

            if (ua.Contains("Windows NT 10"))
                return "Windows 10";
            return request.Browser.Platform + (ua.Contains("Mobile") ? " Mobile " : "");
        }
        public static String GetDeviceInformation(HttpRequest request)
        {
            string ret = "";
            try
            {
                string strUserAgent = request.UserAgent.ToString().ToLower();
                if (strUserAgent != null)
                {
                    if (request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                             strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                             strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                             strUserAgent.Contains("palm"))
                    {
                        ret = "Mobile Device";
                    }
                    else
                    {
                        ret = "Computer";
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static String GetUserEnvironment(HttpRequest request)
        {
            var browser = request.Browser;
            var platform = GetUserPlatform(request);
            return string.Format("{0} {1} / {2}", browser.Browser, browser.Version, platform);
        }
        public static string GetSystemBrowser(HttpRequest request)
        {
            string rt = "";
            try
            {
                HttpBrowserCapabilities browserCapabilities = request.Browser;
                return browserCapabilities.Browser;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static String GetMobileVersion(string userAgent, string device)
        {
            var temp = userAgent.Substring(userAgent.IndexOf(device) + device.Length).TrimStart();
            var version = string.Empty;
            foreach (var character in temp)
            {
                var validCharacter = false;
                int test = 0;

                if (Int32.TryParse(character.ToString(), out test))
                {
                    version += character;
                    validCharacter = true;
                }

                if (character == '.' || character == '_')
                {
                    version += '.';
                    validCharacter = true;
                }

                if (validCharacter == false)
                    break;
            }

            return version;
        }
        public static string fnGetAPICall(string API, string Data)
        {
            string Result = "";
            string Message = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(cGeneral.BaseURL + API + Data);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (Convert.ToString(httpResponse.StatusCode) == "OK")
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        Result = streamReader.ReadToEnd();
                    }
                    if (Result != "" && Result != null)
                    {
                        return Result;
                    }
                    else
                    {
                        Message = "Something Went Wrong";
                        Result = "{\"ErrorNumber\" : \"201\" , \"ErrorMessage\"  :  \"Exception : " + Message + "\" }";
                        return Result;
                    }
                }
                else
                {
                    Message = "Something Went Wrong";
                    Result = "{\"ErrorNumber\" : \"201\" , \"ErrorMessage\"  :  \"Exception : " + Message + "\" }";
                    return Result;
                }
            }
            catch (Exception Ex)
            {
                Message = Ex.Message.ToString();
                Result = "{\"ErrorNumber\" : \"201\" , \"ErrorMessage\"  :  \"Exception : " + Message + "\" }";
                return Result;
            }
        }
        public static string fnPostAPICall(string API, string Data)
        {
            string Result = "";
            string Message = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(cGeneral.BaseURL + API);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = Convert.ToString(Data);
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
                    if (Result != "" && Result != null)
                    {
                        return Result;
                    }
                    else
                    {
                        Message = "Something Went Wrong";
                        Result = "{\"ErrorNumber\" : \"201\" , \"ErrorMessage\"  :  \"Exception : " + Message + "\" }";
                        return Result;
                    }
                }
                else
                {
                    Message = "Something Went Wrong";
                    Result = "{\"ErrorNumber\" : \"201\" , \"ErrorMessage\"  :  \"Exception : " + Message + "\" }";
                    return Result;
                }
            }
            catch (Exception Ex)
            {
                Message = Ex.Message.ToString();
                Result = "{\"ErrorNumber\" : \"201\" , \"ErrorMessage\"  :  \"Exception : " + Message + "\" }";
                return Result;
            }
        }
        public static string GetTransactionID()
        {
            string TXNID = "";
            try
            {
                string Data = "LoginID=1";
                string API = "GetTransactionID?";
                string Result = cGeneral.fnGetAPICall(API, Data);
                System.Data.DataSet ds = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataSet>(Result);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorNumber"]) == "0")
                        {
                            TXNID = Convert.ToString(Convert.ToString(ds.Tables[0].Rows[0]["PurchaseNumber"]));
                            return TXNID;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
            }
            return TXNID;
        }
        public static string ComputeSHA1(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // lowercase hex
                }
                return sb.ToString();
            }
        }
    }
}