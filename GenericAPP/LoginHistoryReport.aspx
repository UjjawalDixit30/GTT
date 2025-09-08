<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LoginHistoryReport.aspx.cs" Inherits="GenericAPP.LoginHistoryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .sticky-header th {
            position: sticky;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-4 p-2">
        <div class="container mt-2">
            <div class="row mb-3 align-items-end">
                <div class="col-md-3">
                    <label class="form-label" for="basic-default-dealername">Dealer Name</label>
                    <div class="input-group input-group-merge">
                        <span id="basic-icon-default-dealername2" class="input-group-text">
                            <i class="bx bx-user"></i>
                        </span>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label class="form-label" for="html5-fromdate-input">From Date</label>
                    <asp:TextBox ID="fromdate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
                <div class="col-md-2">
                    <label class="form-label" for="html5-todate-input">To Date</label>
                    <asp:TextBox ID="todate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div class="row mb-3 align-items-end">
                <div class="table-responsive" style="max-height: 490px; overflow-y: auto;">
                    <table class="table table-bordered" style="padding: 0.72rem 1rem !important;">
                        <asp:Repeater ID="rptLoginHistory" runat="server">
                            <HeaderTemplate>
                                <thead>
                                    <tr>
                                        <th scope="col">S.No.</th>
                                        <th scope="col">Dealer Name</th>
                                        <th scope="col">Username</th>
                                        <th scope="col">Login Date/Time</th>
                                    </tr>
                                </thead>
                                <tbody class="table-border-bottom-0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex + 1 %></td>
                                    <td><%# Eval("Dealername") %></td>
                                    <td><%# Eval("Username") %></td>
                                    <td><%# Eval("CreateDate") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <asp:Label ID="lblNoRecords" runat="server" CssClass="text-danger fw-bold" Visible="false" Text="No records found."></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
