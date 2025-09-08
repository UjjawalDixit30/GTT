<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CheckStatus.aspx.cs" Inherits="GenericAPP.CheckStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .nowrap-table td,
        .nowrap-table th {
            white-space: nowrap;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card">
                <div class="container mt-2">
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label class="form-label" for="html5-fromdate-input">Serial Number</label>
                            <asp:TextBox ID="txtSerialNumber" runat="server" CssClass="form-control" placeholder="Serial Number" />
                        </div>
                        <div class="col-md-2" style="margin-top: 29px;">
                            <asp:Button ID="btnGetDetails" runat="server" Text="Get Details" CssClass="btn btn-primary w-100" OnClick="btnGetDetails_Click" />
                        </div>
                    </div>
                    <div class="row mb-3" id="DivMain" runat="server" visible="false">
                        <div class="col-md-12" style="margin-top: 10px;">
                            <table class="table table-bordered">
                                <tr>
                                    <td><b>Dealer Name : </b><span runat="server" id="spnDEALERNAME"></span></td>
                                    <td><b>Serial Number : </b><span runat="server" id="spnSERIALNUMBER"></span></td>
                                    <td><b>Status : </b><span runat="server" id="spnSTATUS"></span></td>
                                    <td><b>Network : </b><span runat="server" id="spnNETWORKNAME"></span></td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-12 table-responsive" style="margin-top: 10px;" runat="server" id="plandiv">
                            <div style="height: 440px; overflow-y: scroll;">
                                <asp:Repeater ID="RepeaterPlanList" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered nowrap-table" style="padding: 0.72rem 1rem !important;">
                                            <thead>
                                                <tr>
                                                    <th>TXNID</th>
                                                    <th>SERIALNUMBER</th>
                                                    <th>DEALERNAME</th>
                                                    <th>SIMTYPE</th>
                                                    <th>REQUESTEDDATE</th>
                                                    <th>STATUS</th>
                                                    <th>AMOUNTCHARGED</th>
                                                    <th>PACKAGENAME</th>
                                                    <th>NUMBEROFDAYS</th>
                                                    <th>ORDERID</th>
                                                    <th>NETWORKNAME</th>
                                                    <th>ACTIONTYPE</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblTXNID" Text='<%# Eval("TXNID") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblSERIALNUMBER" Text='<%# Eval("SERIALNUMBER") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblDEALERNAME" Text='<%# Eval("DEALERNAME") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblREQUESTEDDATE" Text='<%# Eval("REQUESTEDDATE") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblSTATUS" Text='<%# Eval("STATUS") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblAMOUNTCHARGED" Text='<%# Eval("AMOUNTCHARGED") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblPACKAGENAME" Text='<%# Eval("PACKAGENAME") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblNUMBEROFDAYS" Text='<%# Eval("NUMBEROFDAYS") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblORDERID" Text='<%# Eval("ORDERID") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblNETWORKNAME" Text='<%# Eval("NETWORKNAME") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblACTIONTYPE" Text='<%# Eval("ACTIONTYPE") %>' />
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
    </div>
</asp:Content>




