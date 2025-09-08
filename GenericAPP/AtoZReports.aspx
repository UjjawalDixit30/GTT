<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AtoZReports.aspx.cs" Inherits="GenericAPP.AtoZReports" %>

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
                <div class="col-md-3">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" />
                </div>
            </div>
            <div class="row mb-3" style="margin-top: 20px;">
                <div class="col-md-12 table-responsive" style="max-height: 490px; overflow-y: auto;" runat="server" id="ReportDiv" visible="false">
                    <table class="table table-bordered table-hover nowrap" id="ActivationReportsTable">
                        <thead>
                            <tr>
                                <th scope="col">S.NO.</th>
                                <th scope="col">PAYMENT TYPE</th>
                                <th scope="col">PAYMENT DATE</th>
                                <th scope="col">TXNID</th>
                                <th scope="col">MSISDN</th>
                                <th scope="col">SERIAL NUMBER</th>
                                <th scope="col">DEALER NAME</th>
                                <th scope="col">DEALER PREVIOUS BALANCE</th>
                                <th scope="col">AMOUNT</th>
                                <th scope="col">DEALER CURRENT BALANCE</th>
                                <th scope="col">ADMIN PREVIOUS BALANCE</th>
                                <th scope="col">ADMIN CURRENT BALANCE</th>
                                <th scope="col">REMARKS</th>
                            </tr>
                        </thead>
                        <tbody class="table-border-bottom-0">
                            <asp:Repeater ID="rptActivationReport" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("PAYMENTTYPE") %></td>
                                        <td><%# Eval("PAYMENTDATE") %></td>
                                        <td><%# Eval("TXNID") %></td>
                                        <td><%# Eval("MSISDN") %></td>
                                        <td><%# Eval("SERIALNUMBER") %></td>
                                        <td><%# Eval("DEALERNAME") %></td>
                                        <td><%# Eval("DEALERPREVIOUSBALANCE") %></td>
                                        <td><%# Eval("AMOUNT") %></td>
                                        <td><%# Eval("DEALERCURRENTBALANCE") %></td>
                                        <td><%# Eval("ADMINPREVIOUSBALANCE") %></td>
                                        <td><%# Eval("ADMINCURRENTBALANCE") %></td>
                                        <td><%# Eval("REMARKS") %></td>
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




