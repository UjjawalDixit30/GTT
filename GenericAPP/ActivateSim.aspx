<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ActivateSim.aspx.cs" Inherits="GenericAPP.ActivateSim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding-left: 10px; padding-right: 10px;">
                <div class="container mt-2">
                    <div class="row mb-3">
                        <div class="col-md-3" style="margin-top: 15px;">
                            <label for="ddlNetwork" class="form-label">Network<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:DropDownList ID="ddlNetwork" CssClass="form-select" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 15px;">
                            <label for="ddlType" class="form-label">Type<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:DropDownList ID="ddlType" CssClass="form-select" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="27">pSim</asp:ListItem>
                                    <asp:ListItem Value="28">eSim</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 15px;">
                            <label for="txtRequestedFor" class="form-label">Activation Date<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:TextBox runat="server" ID="txtActivationDate" CssClass="form-control" MaxLength="22" placeholder="Enter Activation Date" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 15px;" runat="server" id="SerialNumberDiv">
                            <label for="txtSerialNumber" class="form-label">Serial Number<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:TextBox runat="server" ID="txtSerialNumber" CssClass="form-control" MaxLength="22" placeholder="Enter serial number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 15px;" runat="server" id="destinationdiv" visible="false">
                            <label for="ddlDestination" class="form-label">Destination<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:DropDownList ID="ddlDestination" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDestination_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 15px;" runat="server" id="Plandiv" visible="false">
                            <label for="ddlPlan" class="form-label">Plans<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:DropDownList ID="ddlPlan" CssClass="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 15px;" runat="server" id="PlanValidityDiv" visible="false">
                            <label for="txtPlanValidity" class="form-label">Plan Validity<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:TextBox runat="server" ID="txtPlanValidity" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 15px;" runat="server" id="TotalPaybleAmountDiv" visible="false">
                            <label for="txtTotalPaybleAmount" class="form-label">Total Payble Amount<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:TextBox runat="server" ID="txtTotalPaybleAmount" CssClass="form-control" MaxLength="22"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 15px;" runat="server" id="CustomerEmailIDdiv" visible="false">
                            <label for="txtCustomerEmailID" class="form-label">Customer Email ID<span class="text-danger">*</span></label>
                            <div class="input-group input-group-merge">
                                <asp:TextBox runat="server" ID="txtCustomerEmailID" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-md-12 m-2 p-2">
                            <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="Submit" Visible="false" />
                            <asp:Button runat="server" ID="btnValidate" CssClass="btn btn-primary" OnClick="btnValidate_Click" Text="Validate" />
                            <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger" OnClick="btnReset_Click" Text="Reset" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



