<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ViewTariffGroup.aspx.cs" Inherits="GenericAPP.ViewTariffGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="container mt-2">
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label for="txtTariffGroupName" class="form-label">Tariff Group Name <span style="color: red">*</span></label>
                            <asp:TextBox ID="txtTariffGroupName" runat="server" CssClass="form-control" placeholder="Enter Tariff Group Name" ReadOnly="true" Style="width: 100%;"></asp:TextBox>
                        </div>
                        <div class="col-md-8" style="margin-top: 29px;">
                            <a href="TariffGroupList.aspx" type="button" runat="server" id="btnBack" class="btn btn-outline-primary waves-effect waves-light" style="float: right;"><i class="fa fa-backward"></i><span style="margin-left: 5px;">Back</span></a>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12" style="height: 480px; overflow-y: scroll;">
                            <asp:Repeater ID="RepeaterPlanList" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-bordered" style="padding: 0.72rem 1rem !important;">
                                        <thead>
                                            <tr>
                                                <th>PlanName</th>
                                                <th>Network</th>
                                                <th>PackageID</th>
                                                <th>Plan_Price</th>
                                                <th>Activation_Price</th>
                                                <th>Extension_Price</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblPlanName" Text='<%#Eval("PlanName") %>' />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblNetwork" Text='<%#Eval("Network") %>' />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPackageID" Text='<%#Eval("PackageID") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label runat="server" ID="lblPlanPrice" Text='<%#Eval("PlanPrice") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label runat="server" ID="lblActivationPrice" Text='<%#Eval("ActivationPrice") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label runat="server" ID="lblExtensionPrice" Text='<%#Eval("ExtensionPrice") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            <asp:Label runat="server" ID="lblDataFound" Style="font-size: 20px; margin-top: 20px; font-weight: bold;"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



