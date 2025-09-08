<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="GenericAPP.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/@mdi/font@7.2.96/css/materialdesignicons.min.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="card" style="padding: 10px;">
                <div class="row m-2 align-items-center">
                    <div class="col-md-6 p-0">
                        <h5 class="m-0">Users List</h5>
                    </div>
                    <div class="col-md-6 text-end p-0">
                        <div class="m-0">
                            <a href="AddUser.aspx" type="button" class="btn btn-outline-primary waves-effect waves-light"><i class="fa fa-plus"></i><span style="margin-left: 5px;">new</span></a>
                        </div>
                    </div>
                </div>
                <div class="row m-2 align-items-center">
                    <div class="col-md-12 p-0" style="margin-top: 10px;">
                        <div id="DivMain" runat="server" style="height: 520px; overflow-y: scroll;">
                            <asp:Repeater ID="RepeaterUserList" runat="server" OnItemCommand="RepeaterUserList_ItemCommand">
                                <HeaderTemplate>
                                    <table class="table table-bordered" style="padding: 0.72rem 1rem !important;">
                                        <thead class="table-light">
                                            <tr>
                                                <th>Name</th>
                                                <th>Email ID</th>
                                                <th>Dealer Name</th>
                                                <th>Status</th>
                                                <th>Username</th>
                                                <th>Created Date</th>
                                                <th>Role Name</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("EmailID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDealerName" runat="server" Text='<%# Eval("DealerName") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>' Visible="false" />
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCreatedDtTm" runat="server" Text='<%# Eval("CreatedDtTm") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRoleName" runat="server" Text='<%# Eval("RoleName") %>' />
                                        </td>
                                        <td style="text-align: center; width: 10%;">
                                            <asp:LinkButton ID="lnkView" runat="server" CommandName="ViewDealer" CommandArgument='<%#Eval("UserID") %>' title="View"><i class="mdi mdi-eye"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditDealer" CommandArgument='<%#Eval("UserID") %>' title="Edit"><i class="mdi mdi-pencil"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkUpdateStatus" runat="server" CommandName="UpdateStatus" OnClientClick="return confirm('Are you sure you want to Change the Status?');" CommandArgument='<%#Eval("UserID") %>' title="Update Status"><i class="mdi mdi-cog-outline"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkChange" runat="server" OnClientClick='<%# "openPasswordModal(" + Eval("UserID") + "); return false;" %>' title="Change Password"><i class="mdi mdi-lock"></i></asp:LinkButton>
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
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="passwordModal" tabindex="-1" aria-labelledby="passwordModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:HiddenField runat="server" ID="hdnSelectedUserId" />
                    <h5 class="modal-title" id="passwordModalLabel">Change Password</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="New Password"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSubmitPassword" runat="server" CssClass="btn btn-primary" Text="Update Password" OnClick="btnSubmitPassword_Click" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function openPasswordModal(userId) {
            document.getElementById('<%= hdnSelectedUserId.ClientID %>').value = userId;
            var myModal = new bootstrap.Modal(document.getElementById('passwordModal'));
            myModal.show();
        }
    </script>
</asp:Content>
