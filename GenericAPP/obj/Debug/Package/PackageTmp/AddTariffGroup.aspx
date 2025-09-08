<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddTariffGroup.aspx.cs" Inherits="GenericAPP.AddTariffGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="container mt-2">
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <asp:HiddenField runat="server" ID="hddnActionType" />
                            <asp:HiddenField runat="server" ID="hddnTariffGroupID" />
                            <label for="txtTariffGroupName" class="form-label">Tariff Group Name <span style="color: red">*</span></label>
                            <asp:TextBox ID="txtTariffGroupName" runat="server" CssClass="form-control" placeholder="Enter Tariff Group Name" Style="width: 100%;"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="FileUpload" class="form-label">Upload File<span style="color: red">*</span></label>
                            <asp:FileUpload ID="FileUpload" runat="server" CssClass="form-control" Style="width: 100%;"></asp:FileUpload>
                        </div>
                        <div class="col-md-5" style="margin-top: 29px;">
                            <asp:Button runat="server" ID="btnUpload" CssClass="btn btn-primary" Text="Upload" OnClick="btnUpload_Click" />
                            <a href="TariffGroupList.aspx" type="button" runat="server" id="btnBack" class="btn btn-outline-primary waves-effect waves-light"><i class="fa fa-backward"></i><span style="margin-left: 5px;">Back</span></a>
                            <asp:Button runat="server" ID="btnDownloadPlanList" Style="float: right;" CssClass="btn btn-primary" Text="Download Plan List" OnClick="btnDownloadPlanList_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function ValidateSubmitControls() {
            function showError(ctrl, msg) {
                ctrl.focus();
                alert(msg);
                return false;
            }
            var txtName = document.getElementById('<%=txtTariffGroupName.ClientID%>');
            if (txtName.value.trim() === "")
                return showError(txtName, 'Please enter tariff group name.');
            return true;
        }
    </script>
</asp:Content>



