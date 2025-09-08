<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DealerList.aspx.cs" Inherits="GenericAPP.DealerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="row m-2 align-items-center">
                    <div class="col-md-6 p-0">
                        <h5 class="m-0">Dealers List</h5>
                    </div>
                    <div class="col-md-6 text-end p-0">
                        <div class="m-0">
                            <a href="AddDealer.aspx" type="button" class="btn btn-outline-primary waves-effect waves-light"><i class="fa fa-plus"></i><span style="margin-left: 5px;">new</span></a>
                        </div>
                    </div>
                </div>
                <div class="row m-2 align-items-center">
                    <div class="col-md-12 p-0" style="margin-top: 10px;">
                        <div id="DivMain" runat="server" style="height: 520px; overflow-y: scroll;">
                            <asp:Repeater ID="RepeaterDealerList" OnItemCommand="RepeaterDeviceList_ItemCommand" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-bordered" style="padding: 0.72rem 1rem !important;">
                                        <thead>
                                            <tr>
                                                <th>DealerName</th>
                                                <th style="text-align: center; width: 10%;">ContactPerson</th>
                                                <th style="text-align: center; width: 10%;">ParentName</th>
                                                <th style="text-align: center; width: 10%;">Status</th>
                                                <th style="text-align: center;">CreatedDtTm</th>
                                                <th style="text-align: center; width: 10%;">ContactNumber</th>
                                                <th style="text-align: center; width: 8%;">ContactEmailID</th>
                                                <th style="text-align: center; width: 8%;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblDealerName" Text='<%#Eval("DealerName") %>' />
                                            <asp:Label runat="server" ID="lblDeviceID" Text='<%# Eval("DealerID") %>' Visible="false" />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label runat="server" ID="lblContactPerson" Text='<%# Eval("ContactPerson") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label runat="server" ID="lblParentName" Text='<%# Eval("ParentName") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label runat="server" ID="lblIsActive" Text='<%# Eval("IsActive") %>' Visible="false" />
                                            <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("Status") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label runat="server" ID="lblCreatedDtTm" Text='<%# Eval("CreatedDtTm") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label runat="server" ID="lblContactNumber" Text='<%# Eval("ContactNumber") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label runat="server" ID="lblContactEmailID" Text='<%# Eval("ContactEmailID") %>' />
                                        </td>
                                        <td style="text-align: center; width: 10%;">
                                            <asp:LinkButton ID="lnkView" runat="server" CommandName="ViewDealer" CommandArgument='<%#Eval("DealerID") %>' title="View"><i class="mdi mdi-eye" style="color:#45569A;"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditDealer" CommandArgument='<%#Eval("DealerID") %>' title="Edit"><i class="mdi mdi-pencil" style="color:#45569A;"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkUpdateStatus" runat="server" CommandName="UpdateStatus" OnClientClick="return confirm('Are you sure you want to Change the Status?');" CommandArgument='<%#Eval("DealerID") %>' title="Update Status"><i class="mdi mdi-cog-outline" style="color:#45569A;"></i></asp:LinkButton>
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
