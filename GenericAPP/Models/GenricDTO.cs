using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericAPP.Models
{
    public class GenricDTO
    {
        public class Login_DTO
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string RequestedIP { get; set; }
            public string RequestedOS { get; set; }
            public string RequestedDevice { get; set; }

        }

        public class DealerDTO
        {
            public int LoginID { get; set; }
            public string Name { get; set; }
            public string ContactPerson { get; set; }
            public string ContactNumber { get; set; }
            public string ContactEmailID { get; set; }
            public string Username { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public int CountryID { get; set; }
            public decimal TransactionFee { get; set; }
            public string Password { get; set; }
            public decimal MinimumTopUpAmount { get; set; }
            public decimal MaximumTopUpAmount { get; set; }
            public int TariffGroupID { get; set; }
            public int AllowSellerCreation { get; set; }
            public int dealerid { get; set; }
            public int Actiontype { get; set; }

        }

        public class User_DTO
        {

            public int LoginID { get; set; }
            public int RoleID { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }

            public string DealerName { get; set; }
            public string Password { get; set; }

            public int DealerID { get; set; }
            public int UserID { get; set; }
            public int ActionType { get; set; }
        }
        public class Customer_DTO
        {
            public int LoginID { get; set; }
            public long CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string CustomerEmailID { get; set; }
        }
        public class AddFund_DTO
        {
            public int LoginID { get; set; }
            public long DealerID { get; set; }
            public string RequestedIP { get; set; }
            public string RequestedDevice { get; set; }
            public string RequestedOS { get; set; }
            public int RequestStatus { get; set; }
            public decimal AmountCharged { get; set; }
            public decimal ProcessingFee { get; set; }
            public string TXNID { get; set; }
            public string ThirdPartyOrderID { get; set; }
            public string Response { get; set; }
            public string Remarks { get; set; }
            public int PaymentTypeID { get; set; }
            public int PaymentMode { get; set; }
            public int PaymentStatus { get; set; }
            public int ActionType { get; set; }
        }
        public class PurchaseSimDTO
        {
            public int LoginID { get; set; }
            public int NetworkID { get; set; }
            public string PurchaseCode { get; set; }
            public List<SimNumberDTO> SerialNumber { get; set; }
        }
        public class SimNumberDTO
        {
            public string SerialNumber { get; set; }
        }
        public class AddTariffGroup_DTO
        {
            public long LoginID { get; set; }
            public int ActionType { get; set; }
            public Int32 TariffGroupID { get; set; }
            public string TariffGroupName { get; set; }
            public List<PlanItem_DTO> PlanItem_DTO { get; set; }
        }
        public class PlanItem_DTO
        {
            public Int32 PlanID { get; set; }
            public int NetworkID { get; set; }
            public decimal ActivationPrice { get; set; }
            public decimal ExtensionCharge { get; set; }
        }
        public class PieChartDataDTO
        {
            public int FreeSims { get; set; }
            public int ActiveSims { get; set; }
            public int LostSims { get; set; }
            public int ExtensionData { get; set; }
            public int ActivationData { get; set; }
        }
        public class AddPendingRequest_ActivationDTO
        {
            public long LoginID { get; set; }
            public string TXNID { get; set; }
            public string SerialNumber { get; set; }
            public string IMEI { get; set; }
            public string ActivationCode { get; set; }
            public long RequestedBy { get; set; }
            public string RequestedForDtTm { get; set; }
            public string RequestedIP { get; set; }
            public string RequestedDevice { get; set; }
            public string RequestedOS { get; set; }
            public int RequestStatus { get; set; }
            public decimal AmountCharged { get; set; }
            public long PlanId { get; set; }
            public int NumberOfDays { get; set; }
            public string MSISDN { get; set; }
            public string EmailID { get; set; }
            public int NetworkID { get; set; }
            public string DownloadURL { get; set; }
            public int ActivatedFrom { get; set; }
            public string Remarks { get; set; }
            public int SimType { get; set; }
            public int PaymentTypeID { get; set; }
            public int PaymentMode { get; set; }
            public int PaymentStatus { get; set; }
        }
        public class UpdatePendingRequest_ActivationDTO
        {
            public int RequestStatus { get; set; }
            public int RequestID { get; set; }
            public int LoginID { get; set; }
            public int PaymentID { get; set; }
            public string Status { get; set; }
            public string AddOrder_OrderId { get; set; }
            public string MSISDN { get; set; }
            public string AddOrder_DownloadUrl { get; set; }
            public string QRCodeURL { get; set; }
            public string AddOrder_JsonRequest { get; set; }
            public string AddOrder_StrResponse { get; set; }
        }
        public class WorldMoveProduct
        {
            public string WmproductId { get; set; }
            public int Day { get; set; }
            public string SimNum { get; set; }
        }
        public class eSimWorldMoveProduct
        {
            public string WmproductId { get; set; }
            public int qty { get; set; }
        }
        public class WorldMoveMerchantRequest
        {
            public string MerchantId { get; set; }
            public string DeptId { get; set; }
            public List<WorldMoveProduct> ProdList { get; set; }
            public string EncStr { get; set; }
            public string orderId { get; set; }
        }
    }
}