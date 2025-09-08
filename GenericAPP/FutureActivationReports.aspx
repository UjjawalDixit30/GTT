<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FutureActivationReports.aspx.cs" Inherits="GenericAPP.FutureActivationReports" %>

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
            <div class="row mb-3" style="margin-top: 20px;">
                <div class="col-md-12" style="max-height: 550px; overflow-y: auto;" runat="server" id="ReportDiv" visible="false">
                    <table class="table table-bordered table-hover nowrap">
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




