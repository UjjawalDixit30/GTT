<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddFund.aspx.cs" Inherits="GenericAPP.AddFund" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://www.paypalobjects.com/api/checkout.js"></script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to deduct Amount?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        paypal.Button.render({
            env: 'production',
            client: {
                sandbox: 'AcO4UqEIC_Ity0ClrWB2ThE9hBpQUah2npCbAAqMCW5iHlDhzRSzLm_wR1-j1g34RqBOHGBHoe3BKbmM',
                production: 'AZhqNfrQvabHK5ohCMmSzh6Rt6o2krELyVYr1wxYRPe4IEkX-LsLa0i3lRSdUB2mR1apFsrZko5e6kng'
            },
            commit: true,
            intent: "sale",
            payer: {
                "payment_method": "paypal"
            },
            payment: function (data, actions) {
                return actions.payment.create({
                    payment: {
                        transactions: [
                            {
                                amount: { total: '10.00', currency: 'USD' }
                            }
                        ],
                        redirect_urls: {
                            return_url: 'https://pos.globaltraveltelecom.com/RechargeSuccess.aspx',
                            cancel_url: 'https://pos.globaltraveltelecom.com/RechargeSuccess.aspx'
                        }
                    }
                });
            },
            onAuthorize: function (data, actions) {
                return actions.payment.get().then(function (data) {
                    return actions.payment.execute().then(function () {
                        actions.redirect();
                    });
                });
            },
            onCancel: function (data, actions) {
                actions.redirect();
            }

        }, '#paypal-button-container');
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="container mt-2">
                    <div class="row mb-3">
                        <div class="col-md-5">
                            <div class="col-md-12">
                                <label class="form-label" for="basic-default-dealername">Dealer Name<span class="text-danger">*</span></label>
                                <div class="input-group input-group-merge">
                                    <span class="input-group-text"><i class="bx bx-user"></i></span>
                                    <asp:DropDownList runat="server" class="form-select" ID="ddlDealer" aria-label="Default select example" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 20px; display: none;" runat="server" id="AccountBalanceDiv">
                                <label class="control-label">Account Balance :  <span id="lblDealerAccountBalance" runat="server"></span></label>
                            </div>
                            <div class="col-md-12" style="margin-top: 20px;">
                                <label class="form-label" for="basic-icon-default-Amount">Amount($)<span class="text-danger">*</span></label>
                                <div class="input-group input-group-merge">
                                    <span class="input-group-text"><i class="bx bx-dollar"></i></span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtAmount" aria-label="Default select example" placeholder="Top Up Amount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 20px;">
                                <label class="form-label" for="basic-default-reason">Reason (Optional)</label>
                                <div class="input-group input-group-merge">
                                    <asp:TextBox runat="server" TextMode="MultiLine" Style="height: 100px;" CssClass="form-control" ID="txtRemarks" placeholder="Write your reason here." aria-describedby="basic-icon-default-reason2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 20px;">
                                <asp:Button ID="btnDeductAmount" runat="server" Text="Deduct Amount" CssClass="btn btn-primary" OnClick="btnDeductAmount_Click" />
                                <asp:Button ID="btnTopUp" runat="server" Text="TOPUP" CssClass="btn btn-primary" OnClick="btnTopUp_Click" />
                                <asp:ImageButton ID="btnPaypal" runat="server" src='design/assets/img/expresscheckout_buttons.png' class="btn btn-primary pul-left" ClientIDMode="Static" OnClientClick="javascript:return validateTopup();" OnClick="btnPaypal_Click" Style="padding: 0px!important; background-color: white!important; border-color: white!important; width: 50%;" />
                            </div>
                            <div class="col-md-12" style="margin-top: 50px;">
                                <div class="widget-body">
                                    <fieldset>
                                        <small><strong>Note</strong>:- If in case, after paypal transaction you are not redirect to the Main account and see the updated amount, then call for assistance</small>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
