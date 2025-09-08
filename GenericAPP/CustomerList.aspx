<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="GenericAPP.CustomerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #customModal {
            display: none;
            position: fixed;
            z-index: 9999;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0,0,0,0.5);
        }

        #customModalContent {
            background-color: #fff;
            margin: 10% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 400px;
            border-radius: 10px;
        }
    </style>
    <script type="text/javascript">
        function showModal() {
            document.getElementById('customModal').style.display = 'block';
        }

        function hideModal() {
            document.getElementById('customModal').style.display = 'none';
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="row m-2 align-items-center">
                    <div class="col-md-12 p-0">
                        <h5 class="m-0">Customers List</h5>
                    </div>
                </div>
                <div class="row m-2 align-items-center">
                    <div class="col-md-12 p-0" style="margin-top: 10px;">
                        <div id="DivMain" runat="server" style="height: 520px; overflow-y: scroll;">
                            <asp:Repeater ID="RepeaterCustomerList" runat="server" OnItemCommand="RepeaterCustomerList_ItemCommand">
                                <HeaderTemplate>
                                    <table class="table table-bordered" style="padding: 0.72rem 1rem !important;">
                                        <thead class="table-light">
                                            <tr>
                                                <th style="text-align: center;">S.NO</th>
                                                <th style="text-align: center;">Customer Name</th>
                                                <th style="text-align: center;">Customer EmailID</th>
                                                <th style="text-align: center;">Status</th>
                                                <th style="text-align: center;">Username</th>
                                                <th style="text-align: center;">Created Date</th>
                                                <th style="text-align: center;">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody class="table-border-bottom-0">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center;"><%# Container.ItemIndex + 1 %>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="txtCustomerName" runat="server" Text='<%# Eval("CustomerName") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("CustomerEmailID") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>' Visible="false" />
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblCreatedDtTm" runat="server" Text='<%# Eval("CreateDate") %>' />
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:HiddenField ID="hdnUserID" runat="server" Value='<%# Eval("CustomerID") %>' />
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("CustomerID") %>' CommandName="EditCustomer" title="Edit">  <i class="mdi mdi-pencil"></i> </asp:LinkButton>
                                            <asp:LinkButton ID="lnkUpdateStatus" runat="server" CommandName="UpdateStatus" CommandArgument='<%# Eval("CustomerID") %>' OnClientClick="return confirm('Are you sure you want to Change the Status?');" title="Update Status"><i class="mdi mdi-cog-outline"></i> </asp:LinkButton>
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
    <div id="customModal">
        <div id="customModalContent">
            <asp:HiddenField ID="hdnEditCustomerID" runat="server" />
            <h5>Edit Customer Details</h5>
            <div class="border-top my-2" style="border-top: 1px dotted #999;"></div>
            <div class="form-group mt-2">
                <label>Customer Name</label>
                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server" ControlToValidate="txtCustomerName" ErrorMessage="Customer Name is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="UpdateCustomer" />
            </div>
            <div class="form-group mt-2">
                <label>Customer EmailID</label>
                <asp:TextBox ID="txtCustomerEmail" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvCustomerEmail" runat="server" ControlToValidate="txtCustomerEmail" ErrorMessage="Email ID is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="UpdateCustomer" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtCustomerEmail" ErrorMessage="Invalid email format." CssClass="text-danger" Display="Dynamic" ValidationGroup="UpdateCustomer" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
            </div>
            <div class="border-top my-2 mt-3" style="border-top: 1px dotted #999;"></div>
            <div class="text-right mt-3">
                <asp:Button ID="btnUpdateCustomer" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="UpdateCustomer" OnClick="btnUpdateCustomer_Click" />
                <button type="button" class="btn btn-danger" onclick="hideModal()">Close</button>
            </div>
        </div>
    </div>
</asp:Content>
