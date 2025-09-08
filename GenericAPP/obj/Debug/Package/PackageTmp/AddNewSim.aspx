<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddNewSim.aspx.cs" Inherits="GenericAPP.AddNewSim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="container mt-2">
                    <div class="row g-3">
                        <div class="col-md-2">
                            <label for="ddlNetwork" class="form-label">Network</label>
                            <asp:DropDownList ID="ddlNetwork" runat="server" CssClass="form-select">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label for="ddltype" class="form-label">Type</label>
                            <asp:DropDownList ID="ddltype" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                <asp:ListItem Value="1" Text="Single"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Bulk"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label for="txtPurchaseCode" class="form-label">Purchase Code</label>
                            <asp:TextBox runat="server" ID="txtPurchaseCode" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-6" runat="server" id="SingleUploadDiv">
                            <div class="row g-3">
                                <div class="col-md-8">
                                    <label for="txtSimNumber" class="form-label">Sim Number</label>
                                    <asp:TextBox runat="server" ID="txtSimNumber" MaxLength="50" CssClass="form-control" placeholder="Sim"></asp:TextBox>
                                </div>
                                <div class="col-md-4" style="margin-top: 46px;">
                                    <asp:Button runat="server" CssClass="btn btn-primary" ID="btnAddSim" Text="Add Sim" Style="width: 100%;" OnClick="btnAddSim_Click" OnClientClick="if (!ValidateAddNewSimControls()) { return false;};" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6" runat="server" id="bulkUploadDiv" visible="false">
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label for="BulkUpload" class="form-label">Upload File</label>
                                    <asp:FileUpload runat="server" ID="BulkUpload" CssClass="form-control" />
                                </div>
                                <div class="col-md-3" style="margin-top: 45px;">
                                    <asp:Button runat="server" CssClass="btn btn-primary" ID="btnUpload" Style="width: 100%;" Text="Upload" OnClick="btnUpload_Click" />
                                </div>
                                <div class="col-md-3" style="margin-top: 45px;">
                                    <a href="design/format/BlankSimFormat.csv" type="button" runat="server" id="btnDownloadFormat" class="btn btn-outline-primary waves-effect waves-light"><i class="fa fa-download"></i><span style="margin-left: 5px;">Format</span></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row g-3" runat="server" id="RepeaterDiv" visible="false" style="margin-top: 20px;">
                        <div class="col-md-12" style="height: 425px; overflow-y: scroll;">
                            <asp:Repeater ID="rptDeviceNumberList" runat="server" OnItemCommand="rptDeviceNumberList_ItemCommand">
                                <HeaderTemplate>
                                    <table id="exampleTbl" class="table table-striped table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>S.No.</th>
                                                <th>Device Number</th>
                                                <th>Status</th>
                                                <th>Reason</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblIndex" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblSerialNumber" Text='<%# Eval("SerialNumber") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("Status") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblReason" Text='<%# Eval("Reason") %>'></asp:Label>
                                        </td>
                                        <td style="width: 50px;">
                                            <asp:LinkButton runat="server" ID="lnkDelete" CommandArgument='<%# Container.ItemIndex %>' CommandName="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                        </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="row g-3 align-items-end" runat="server" visible="false" id="SubmitDiv" style="margin-top: 10px;">
                        <div class="col-md-12" style="text-align: right;">
                            <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                            <a href="SimList.aspx" type="button" runat="server" id="btnBack" class="btn btn-outline-primary waves-effect waves-light"><i class="fa fa-backward"></i><span style="margin-left: 5px;">Back</span></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function ValidateAddNewSimControls() {
            function showError(ctrl, msg) {
                ctrl.focus();
                alert(msg);
                return false;
            }
            var txtName = document.getElementById('<%=txtSimNumber.ClientID%>');
            if (txtName.value.trim() === "")
                return showError(txtName, 'Please enter serial number.');
            return true;
        }
    </script>
</asp:Content>



