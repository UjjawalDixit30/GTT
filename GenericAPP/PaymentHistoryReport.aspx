<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PaymentHistoryReport.aspx.cs" Inherits="GenericAPP.PaymentHistoryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
        function searchCustomer(searchText) {
            var tableBody = document.querySelector('.table tbody');
            if (!tableBody) return;
            var rows = tableBody.getElementsByTagName('tr');
            var visibleCount = 0;
            searchText = searchText.toLowerCase().trim();
            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];
                var cells = row.getElementsByTagName('td');
                var shouldShow = false;
                for (var j = 0; j < cells.length; j++) {
                    var cellText = cells[j].innerText.toLowerCase();
                    if (cellText.includes(searchText)) {
                        shouldShow = true;
                        break;
                    }
                }
                row.style.display = shouldShow ? '' : 'none';
                if (shouldShow) visibleCount++;
            }
            var lblDataFound = document.getElementById('<%= lblDataFound.ClientID %>');
            if (lblDataFound) {
                if (visibleCount === 0 && searchText !== '') {
                    lblDataFound.textContent = 'No records found matching your search.';
                    lblDataFound.style.display = 'block';
                } else {
                    lblDataFound.textContent = '';
                    lblDataFound.style.display = 'none';
                }
            }
        }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-4 p-2">
        <div class="container mt-2">
            <div class="row mb-3 align-items-end">
                <div class="col-md-3">
                    <label class="form-label" for="basic-default-dealername">Dealer Name</label>
                    <div class="input-group input-group-merge">
                        <span id="basic-icon-default-dealername2" class="input-group-text">
                            <i class="bx bx-user"></i>
                        </span>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label class="form-label" for="html5-fromdate-input">From Date</label>
                    <asp:TextBox ID="fromdate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
                <div class="col-md-2">
                    <label class="form-label" for="html5-todate-input">To Date</label>
                    <asp:TextBox ID="todate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-3">
                    <div class="input-group" border-radius: 6px; overflow: hidden;" >
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Visible="false" placeholder="Search..." onkeyup="searchCustomer(this.value);" />
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlRepeaterWrapper" runat="server" Visible="false">
            <div class="table-responsive" style="max-height: 360px; overflow-y: auto;">
                <table class="table table-bordered table-hover" id="paymentHistoryTable">
                    <asp:Repeater ID="rptPaymentHistory" runat="server">
                        <HeaderTemplate>
                            <thead class="table-light sticky-top">
                                <tr>
                                    <th scope="col">S.No.</th>
                                    <th scope="col">Name</th>
                                    <th scope="col">Dealer Name</th>
                                    <th scope="col">Status</th>
                                    <th scope="col">Action Date</th>
                                    <th scope="col">Charged Amount</th>
                                    <th scope="col">Processing Fee</th>
                                    <th scope="col">TransactionID</th>
                                    <th scope="col">Response</th>
                                </tr>
                            </thead>
                            <tbody class="table-border-bottom-0">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Container.ItemIndex + 1 %></td>
                                <td><%# Eval("Name") %></td>
                                <td><%# Eval("DealerName") %></td>
                                <td><%# Eval("Status") %></td>
                                <td><%# Eval("ActDate") %></td>
                                <td><%# Eval("ChargedAmount") %></td>
                                <td><%# Eval("ProcessingFee") %></td>
                                <td><%# Eval("TransactionID") %></td>
                                <td><%# Eval("Response") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>
        <asp:Label runat="server" ID="lblDataFound" CssClass="text-danger fw-bold mt-2"></asp:Label>
    </div>
</asp:Content>
