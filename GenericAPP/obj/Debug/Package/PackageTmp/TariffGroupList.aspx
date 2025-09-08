<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="TariffGroupList.aspx.cs" Inherits="GenericAPP.TariffGroupList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="row m-2 align-items-center">
                    <div class="col-md-6 p-0">
                        <h5 class="m-0">Tariff Group List</h5>
                    </div>
                    <div class="col-md-6 text-end p-0">
                        <div class="m-0">
                            <a href="AddTariffGroup.aspx" type="button" class="btn btn-outline-primary waves-effect waves-light"><i class="fa fa-plus"></i><span style="margin-left: 5px;">new</span></a>
                        </div>
                    </div>
                </div>
                <div class="row m-2 align-items-center">
                    <div class="col-md-12 p-0" style="margin-top: 10px;">
                        <div id="DivMain" runat="server" style="height: 520px; overflow-y: scroll;">
                            <asp:Repeater ID="RepeaterTariffGroupList" runat="server" OnItemCommand="RepeaterTariffGroupList_ItemCommand">
                                <HeaderTemplate>
                                    <table class="table table-bordered">
                                        <thead class="table-light">
                                            <tr>
                                                <th>S.NO</th>
                                                <th>Tariff Group Name</th>
                                                <th>Create Date</th>
                                                <th>Status</th>
                                                <th>Actions</th>
                                            </tr>
                                        </thead>
                                        <tbody class="table-border-bottom-0">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.ItemIndex + 1 %></td>
                                        <td>
                                            <asp:Label ID="lblTariffGroupName" runat="server" Text='<%# Eval("TariffGroupName") %>' />
                                            <asp:Label ID="lblTariffGroupID" runat="server" Text='<%# Eval("tariffGroupID") %>' Visible="false" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("CreateDate", "{0:dd/MM/yyyy}") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                                        </td>
                                        <td style="text-align: center; width: 10%;">
                                            <asp:LinkButton ID="lnkView" runat="server" CommandName="View" CommandArgument='<%# Container.ItemIndex %>' title="View"><i class="mdi mdi-eye"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Container.ItemIndex %>' title="Edit"><i class="mdi mdi-pencil"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkUpdateStatus" runat="server" CommandName="UpdateStatus" OnClientClick="return confirm('Are you sure you want to Change the Status?');" CommandArgument='<%# Container.ItemIndex %>' title="Update Status"><i class="mdi mdi-cog-outline"></i></asp:LinkButton>
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
















