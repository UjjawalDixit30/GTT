<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SimList.aspx.cs" Inherits="GenericAPP.SimList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="row m-2 align-items-center">
                    <div class="col-md-12 p-0">
                        <h5 class="m-0">SIM List</h5>
                    </div>
                </div>
                <div class="row m-2 align-items-center">
                    <div class="col-md-12 p-0">
                        <div class="row">
                            <asp:DropDownList runat="server" ID="ddlNumberofRowsCount" Visible="false">
                                <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                            </asp:DropDownList>
                            <div class="col-md-5">
                                <input type="text" class="form-control" runat="server" id="txtTextContext" placeholder="Search Serial Number, Status, Dealer Name..." />
                            </div>
                            <div class="col-md-1">
                                <asp:Button runat="server" ID="btnGetDetails" ClientIDMode="Static" CssClass="btn btn-primary" Text="Search" OnClick="btnGetDetails_Click" OnClientClick="if (!ValidateSubmitControls()) { return false;};" />
                            </div>
                            <div class="col-md-1">
                                <asp:Button runat="server" ID="btnReset" ClientIDMode="Static" CssClass="btn btn-outline-primary waves-effect waves-light" Text="Reset" OnClick="btnReset_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnDownloadExcel" class="btn btn-outline-primary waves-effect waves-light" Text="Download Excel" Style="float: right; width: 100%;" OnClick="btnDownloadExcel_Click" />
                            </div>
                        </div>
                        <div class="row" style="margin-top: 10px;">
                            <div class="col-md-12 table-responsive" id="DivMain" runat="server" style="height: 440px; overflow-y: scroll;">
                                <asp:HiddenField runat="server" ID="hddnminindex" />
                                <asp:HiddenField runat="server" ID="hddnmaxindex" />
                                <asp:HiddenField runat="server" ID="hddnpagenumber" />
                                <asp:Repeater ID="RepeaterSIMList" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered dt-responsive nowrap ShortingDatatable" width="100%" id="myTable">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center;">S.No.</th>
                                                    <th>Serial Number</th>
                                                    <th>Network Name</th>
                                                    <th style="text-align: center;">Status</th>
                                                    <th style="text-align: center;">Dealer</th>
                                                    <th style="text-align: center;">Purchase Number</th>
                                                    <th style="text-align: center;">MSISDN</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tblSearch">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblINDEXID" Text='<%# Eval("INDEXID") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblID" Text='<%# Eval("ID") %>' Visible="false" />
                                                <asp:Label runat="server" ID="lblSERIALNUMBER" Text='<%# Eval("SERIALNUMBER") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblNETWORKNAME" Text='<%# Eval("NETWORKNAME") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblSTATUS" Text='<%# Eval("STATUS") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblDEALERNAME" Text='<%# Eval("DEALERNAME") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblPURCHASENUMBER" Text='<%# Eval("PURCHASENUMBER") %>' />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label runat="server" ID="lblMSISDN" Text='<%# Eval("MSISDN") %>' />
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
                        <div class="row" runat="server" id="divPageCount" style="margin-top: 20px;">
                            <div class="col-md-8"></div>
                            <div class="col-md-4" style="text-align: right;">
                                <asp:Button runat="server" ID="btnPre" CssClass="btn btn-outline-primary waves-effect waves-light" Text="<<" Style="margin-right: 20px; font-weight: bold; border: none;" OnClick="btnPre_Click" />
                                <asp:Label runat="server" ID="lblPageDetails">page 1 of 135</asp:Label>
                                <asp:Button runat="server" ID="btnnext" CssClass="btn btn-outline-primary waves-effect waves-light" Text=">>" Style="margin-left: 20px; font-weight: bold; border: none;" OnClick="btnnext_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

