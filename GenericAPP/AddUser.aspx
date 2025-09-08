<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="GenericAPP.AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="row m-2 align-items-center">
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <asp:HiddenField runat="server" ID="hddnUserID" ClientIDMode="Static" />
                            <label class="form-label" for="basic-default-dealername">Dealer Name<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-dealername2" class="input-group-text"><i class="bx bx-user"></i></span>
                                <asp:DropDownList ID="ddlDealername" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-name">Name<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-name2" class="input-group-text"><i class="bx bx-user"></i></span>
                                <input type="text" class="form-control" runat="server" id="txtdealername" placeholder="Enter Name" aria-label="John Doe" aria-describedby="basic-icon-default-name2" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-username">User Name<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-username2" class="input-group-text"><i class="bx bx-user"></i></span>
                                <input type="text" class="form-control" runat="server" id="txtusername" placeholder="Enter User Name" aria-label="John Doe" aria-describedby="basic-icon-default-username2" />
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <div class="form-password-toggle">
                                <label class="form-label" for="basic-default-password">Password</label>
                                <div class="input-group">
                                    <input type="password" class="form-control" id="txtpassword" runat="server" placeholder="&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;&#xb7;" />
                                    <span id="basic-default-password2" class="input-group-text cursor-pointer"><i class="bx bx-hide"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-password-toggle">
                                <label class="form-label" for="basic-default-confirmpassword">RoleID</label>
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlroleid" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-emailid">EmailID<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-emailid2" class="input-group-text"><i class="bx bx-envelope"></i></span>
                                <input type="text" class="form-control" runat="server" id="txtemailid" placeholder="Enter EmailIDssss" aria-label="John Doe" aria-describedby="basic-icon-default-emailid2" />
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 m-2 p-2 text-end">
                            <a href="UserList.aspx" type="button" runat="server" class="btn btn-outline-primary waves-effect waves-light"><i class="fa fa-backward"></i><span style="margin-left: 5px;">Back</span></a>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnupdate_Click" />
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
            var ddlCountry = document.getElementById('<%=ddlDealername.ClientID%>');
            if (ddlCountry.selectedIndex === 0)
                return showError(ddlCountry, 'Please select a Dealer name.');

            var txtName = document.getElementById('<%=txtdealername.ClientID%>');
            if (txtName.value.trim() === "")
                return showError(txtName, 'Please enter name.');

            var txtContactPerson = document.getElementById('<%=txtusername.ClientID%>');
            if (txtContactPerson.value.trim() === "")
                return showError(txtContactPerson, 'Please enter user name.');

            var txtContactNumber = document.getElementById('<%=txtpassword.ClientID%>');
            if (txtContactNumber.value.trim() === "")
                return showError(txtContactNumber, 'Please enter password.');

            var txtAddress = document.getElementById('<%=ddlroleid.ClientID%>');
            if (txtAddress.value.trim() === "")
                return showError(txtAddress, 'Please select role');

            var txtEmailID = document.getElementById('<%=txtemailid.ClientID%>');
            if (txtEmailID.value.trim() === "")
                return showError(txtEmailID, 'Please enter contact email id.');

            var emailRegex = /^([a-zA-Z0-9_.\-])+@(([a-zA-Z0-9\-])+\.)+([a-zA-Z]{2,4})+$/;
            if (!emailRegex.test(txtEmailID.value))
                return showError(txtEmailID, 'Please enter a valid contact email id');
            return true;
        }
    </script>
</asp:Content>
