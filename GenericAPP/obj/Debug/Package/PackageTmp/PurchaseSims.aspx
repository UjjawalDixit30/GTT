<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PurchaseSims.aspx.cs" Inherits="GenericAPP.PurchaseSims" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="row m-2 align-items-center">
                    <div class="col-md-9 p-0">
                        <h5 class="m-0">Purchase List</h5>
                    </div>
                    <div class="col-md-2 text-end p-0">
                        <div class="m-0">
                            <asp:DropDownList ID="ddlNetwork" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlNetwork_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-1 text-end p-0">
                        <div class="m-0">
                            <a href="AddNewSim.aspx" type="button" class="btn btn-outline-primary waves-effect waves-light"><i class="fa fa-plus"></i><span style="margin-left: 5px;">new</span></a>
                        </div>
                    </div>
                </div>
                <div class="row m-2 align-items-center">
                    <div class="col-md-12 p-0" style="margin-top: 10px;">
                        <div id="DivMain" runat="server" style="height: 520px; overflow-y: scroll;">
                            <asp:Repeater ID="RepeaterPurchaseList" runat="server" OnItemCommand="RepeaterSimList_ItemCommand">
                                <HeaderTemplate>
                                    <table class="table table-bordered" style="padding: 0.72rem 1rem !important;">
                                        <thead class="table-light">
                                            <tr>
                                                <th>S.No.</th>
                                                <th>Purchase Number</th>
                                                <th>Network Name</th>
                                                <th>Purchase Type</th>
                                                <th>Purchase Date</th>
                                                <th>Purchased By</th>
                                                <th>Sim Count</th>
                                                <th>View</th>
                                            </tr>
                                        </thead>
                                        <tbody class="table-border-bottom-0">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPurchaseID" runat="server" Visible="false" Text='<%# Eval("PurchaseID") %>' />
                                            <asp:Label runat="server" ID="lblIndex" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPurchaseNumber" runat="server" Text='<%# Eval("PurchaseNumber") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNetworkName" runat="server" Text='<%# Eval("NetworkName") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPurchaseType" runat="server" Text='<%# Eval("PurchaseType") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPurchaseDtTm" runat="server" Text='<%# Eval("PurchaseDtTm") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPurchasedBy" runat="server" Text='<%# Eval("PurchasedBy") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSimCount" runat="server" Text='<%# Eval("SimCount") %>' />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkView" runat="server" CommandName="View" CommandArgument='<%# Container.ItemIndex %>' title="View"><i class="mdi mdi-eye"></i></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:Label runat="server" ID="lblDataFound" Style="font-size: 20px; margin-top: 20px; font-weight: bold;"></asp:Label>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
