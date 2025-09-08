<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddDealer.aspx.cs" Inherits="GenericAPP.AddDealer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="container mt-2">
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <asp:HiddenField runat="server" ID="hddnDealerID" ClientIDMode="Static" />
                            <asp:HiddenField runat="server" ID="hddnPassword" ClientIDMode="Static" />
                            <label class="form-label" for="basic-default-name">Name<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-name2" class="input-group-text"><i class="bx bx-user"></i></span>
                                <input type="text" class="form-control" id="txtname" runat="server" placeholder="Enter Name" aria-label="John Doe" aria-describedby="basic-icon-default-name2" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-contactperson">Contact Person<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-contactperson2" class="input-group-text"><i class="bx bx-user"></i></span>
                                <input type="text" class="form-control" id="txtcontactperson" runat="server" placeholder="Enter Contact Person" aria-label="John Doe" aria-describedby="basic-icon-default-contactperson2" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-contactnumber">Contact Number<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-contactnumber2" class="input-group-text"><i class="bx bx-phone"></i></span>
                                <input type="text" class="form-control numeric" id="txtcontactnumber" maxlength="10" runat="server" placeholder="Enter Contact Number" aria-label="John Doe" aria-describedby="basic-icon-default-contactnumber2" />
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-emailid">EmailID<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-emailid2" class="input-group-text"><i class="bx bx-envelope"></i></span>
                                <input type="text" class="form-control" id="txtemailid" runat="server" placeholder="Enter EmailID" aria-label="John Doe" aria-describedby="basic-icon-default-emailid2" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-address">Address<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-address2" class="input-group-text"><i class="bx bx-map"></i></span>
                                <input type="text" class="form-control" id="txtaddress" runat="server" placeholder="Enter Address" aria-label="John Doe" aria-describedby="basic-icon-default-address2" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-city">City<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-city2" class="input-group-text"><i class="bx bx-buildings"></i></span>
                                <input type="text" class="form-control" id="txtcity" runat="server" placeholder="Enter City" aria-label="John Doe" aria-describedby="basic-icon-default-city2" />
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-state">State<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-state2" class="input-group-text"><i class="bx bx-map-alt"></i></span>
                                <input type="text" class="form-control" id="txtstate" runat="server" placeholder="Enter State" aria-label="John Doe" aria-describedby="basic-icon-default-state2" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-zip">Zip<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-zip2" class="input-group-text"><i class="bx bx-mail-send"></i></span>
                                <input type="text" class="form-control numeric" id="txtzip" maxlength="6" runat="server" placeholder="Enter ZipCode" aria-label="John Doe" aria-describedby="basic-icon-default-zip2" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="form-label" for="basic-default-country">Country<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-country2" class="input-group-text"><i class="bx bx-world"></i></span>
                                <select class="form-select" id="ddlCountry" aria-label="Default select example" runat="server">
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label class="form-label" for="basic-icon-default-username">Username <span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span id="basic-icon-default-username-icon" class="input-group-text"><i class="bx bx-user"></i></span>
                                <input type="text" class="form-control" id="txtusername" placeholder="Enter Username" aria-label="Username" runat="server" aria-describedby="basic-icon-default-username-icon" />
                            </div>
                        </div>
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
                            <label for="basic-icon-default-transactionfee" class="form-label">Transaction Fee (%) <span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span class="input-group-text">%</span>
                                <input type="text" id="txttransactionfee" runat="server" placeholder="Transaction Fee" class="form-control decimal" />
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label for="basic-icon-default-minAmount" class="form-label">Minimum Top Up Amount<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span class="input-group-text">$</span>
                                <input type="text" id="txtminAmount" runat="server" placeholder="Minimum Top Up Amount" class="form-control decimal" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label for="basic-icon-default-maxAmount" class="form-label">Maximum Top Up Amount<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <span class="input-group-text">$</span>
                                <input type="text" id="txtmaxAmount" runat="server" placeholder="Maximum Top Up Amount" class="form-control decimal" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label for="ddltarrifgroup" class="form-label">Tariff Group <span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:DropDownList ID="ddltarrifgroup" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label for="ddlCreateDealer" class="form-label">Create Dealer?<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:DropDownList ID="ddlCreateDealer" CssClass="form-select" runat="server">
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 m-2 p-2 text-end">
                            <a href="DealerList.aspx" type="button" runat="server" id="A1" class="btn btn-outline-primary waves-effect waves-light"><i class="fa fa-backward"></i><span style="margin-left: 5px;">Back</span></a>
                            <asp:Button runat="server" ID="btnUpdate" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" OnClientClick="if (!ValidateSubmitControls()) { return false;};" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="if (!ValidateSubmitControls()) { return false;};" />
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
            var txtName = document.getElementById('<%=txtname.ClientID%>');
            if (txtName.value.trim() === "")
                return showError(txtName, 'Please enter name.');

            var txtContactPerson = document.getElementById('<%=txtcontactperson.ClientID%>');
            if (txtContactPerson.value.trim() === "")
                return showError(txtContactPerson, 'Please enter contact person name.');

            var txtContactNumber = document.getElementById('<%=txtcontactnumber.ClientID%>');
            if (txtContactNumber.value.trim() === "")
                return showError(txtContactNumber, 'Please enter contact number.');

            var txtEmailID = document.getElementById('<%=txtemailid.ClientID%>');
            if (txtEmailID.value.trim() === "")
                return showError(txtEmailID, 'Please enter contact email id.');

            var emailRegex = /^([a-zA-Z0-9_.\-])+@(([a-zA-Z0-9\-])+\.)+([a-zA-Z]{2,4})+$/;
            if (!emailRegex.test(txtEmailID.value))
                return showError(txtEmailID, 'Please enter a valid contact email id');

            var txtAddress = document.getElementById('<%=txtaddress.ClientID%>');
            if (txtAddress.value.trim() === "")
                return showError(txtAddress, 'Please enter address');

            var txtCity = document.getElementById('<%=txtcity.ClientID%>');
            if (txtCity.value.trim() === "")
                return showError(txtCity, 'Please enter city.');

            var txtState = document.getElementById('<%=txtstate.ClientID%>');
            if (txtState.value.trim() === "")
                return showError(txtState, 'Please enter state.');

            var txtZip = document.getElementById('<%=txtzip.ClientID%>');
            if (txtZip.value.trim() === "")
                return showError(txtZip, 'Please enter zip code.');

            var ddlCountry = document.getElementById('<%=ddlCountry.ClientID%>');
            if (ddlCountry.selectedIndex === 0)
                return showError(ddlCountry, 'Please select a country.');

            var txtUsername = document.getElementById('<%=txtusername.ClientID%>');
            if (txtUsername.value.trim() === "")
                return showError(txtUsername, 'Please enter username.');

            var txtPassword = document.getElementById('<%=hddnPassword.ClientID%>')
            if (txtPassword.value.trim() === "") {
                txtPassword = document.getElementById('<%=txtpassword.ClientID%>') || ;
                if (txtPassword.value.trim() === "")
                    return showError(txtPassword, 'Please enter password.');
            }

            var txtMinAmount = document.getElementById('<%=txtminAmount.ClientID%>');
            var txtMaxAmount = document.getElementById('<%=txtmaxAmount.ClientID%>');
            var minAmountStr = txtMinAmount.value.trim();
            var maxAmountStr = txtMaxAmount.value.trim();
            if (minAmountStr === '' || isNaN(minAmountStr)) {
                return showError(txtMinAmount, 'Please enter a valid minimum topup amount.');
            }
            if (maxAmountStr === '' || isNaN(maxAmountStr)) {
                return showError(txtMaxAmount, 'Please enter a valid maximum topup amount.');
            }
            var minAmount = parseFloat(minAmountStr);
            var maxAmount = parseFloat(maxAmountStr);
            if (minAmount < 10 || minAmount > 2000) {
                return showError(txtMinAmount, 'Minimum topup amount should be between 10 and 2000.');
            }
            if (maxAmount < 10 || maxAmount > 2000) {
                return showError(txtMaxAmount, 'Maximum topup amount should be between 10 and 2000.');
            }
            if (minAmount > maxAmount) {
                return showError(txtMinAmount, 'Minimum topup amount should not be greater than maximum topup amount.');
            }

            var ddlTariff = document.getElementById('<%=ddltarrifgroup.ClientID%>');
            if (ddlTariff.selectedIndex === 0)
                return showError(ddlTariff, 'Please select a tariff group.');

            return true;
        }
    </script>
</asp:Content>



