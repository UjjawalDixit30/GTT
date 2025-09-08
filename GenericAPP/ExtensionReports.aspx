<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ExtensionReports.aspx.cs" Inherits="GenericAPP.ExtensionReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .nowrap td {
            white-space: nowrap;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-4 p-2">
        <div class="container mt-2">
            <div class="row mb-3 align-items-end">
                <div class="col-md-3">
                    <label class="form-label" for="html5-fromdate-input">Active From</label>
                    <asp:TextBox ID="fromdate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
                <div class="col-md-3">
                    <label class="form-label" for="html5-todate-input">Active To</label>
                    <asp:TextBox ID="todate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                </div>
            </div>
            <div class="row mb-3 align-items-end" style="margin-top: 20px;">
                <div class="col-md-12 table-responsive" style="max-height: 490px; overflow-y: auto;" runat="server" id="ReportDiv" visible="false">
                    <table class="table table-bordered table-hover nowrap" id="ActivationReportsTable">
                        <thead>
                            <tr>
                                <th scope="col">S.No.</th>
                                <th scope="col">TXNID</th>
                                <th scope="col">SIM</th>
                                <th scope="col">IMEI</th>
                                <th scope="col">MSISDN</th>
                                <th scope="col">Date</th>
                                <th scope="col">Dealer</th>
                                <th scope="col">Status</th>
                                <th scope="col">Amount</th>
                                <th scope="col">Days</th>
                                <th scope="col">Network</th>
                                <th scope="col">Plan</th>
                            </tr>
                        </thead>
                        <tbody class="table-border-bottom-0">
                            <asp:Repeater ID="rptActivationReport" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("TXNID") %></td>
                                        <td><%# Eval("SERIALNUMBER") %></td>
                                        <td><%# Eval("IMEI") %></td>
                                        <td><%# Eval("MSISDN") %></td>
                                        <td><%# Eval("REQUESTDATE") %></td>
                                        <td><%# Eval("REQUESTEDBY") %></td>
                                        <td><%# Eval("STATUS") %></td>
                                        <td><%# Eval("AMOUNTCHARGED") %></td>
                                        <td><%# Eval("NUMBEROFDAYS") %></td>
                                        <td><%# Eval("NETWORKNAME") %></td>
                                        <td><%# Eval("PLANNAME") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
                <asp:Label runat="server" ID="lblDataFound" Style="font-size: 20px; margin-top: 20px; font-weight: bold;"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

