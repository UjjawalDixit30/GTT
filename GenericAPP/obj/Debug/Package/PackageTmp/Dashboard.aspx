<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="GenericAPP.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            function formatNumberWithCommas(num) {
                return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            }
            function formatK(num) {
                if (num >= 1000) {
                    return (num / 1000).toFixed(num % 1000 === 0 ? 0 : 2) + "k";
                }
                return num;
            }
            function createDonutChart(selector, seriesData) {
                const style = document.createElement("style");
                style.innerHTML = `
            .apexcharts-datalabel-label {
                transform: translateY(17px); 
            }
        `;
                document.head.appendChild(style);
                var total = seriesData.reduce((a, b) => a + b, 0);
                var chartOptions = {
                    series: seriesData,
                    chart: {
                        type: 'donut',
                        height: 150,
                        events: {
                            dataPointMouseEnter: function (event, chartContext, config) {
                                var val = config.w.config.series[config.dataPointIndex];
                                var percent = ((val / total) * 100).toFixed(1) + "%";
                                chartContext.updateOptions({
                                    plotOptions: {
                                        pie: {
                                            donut: {
                                                labels: {
                                                    total: {
                                                        label: percent,
                                                        formatter: () => percent
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }, false, false);
                            },
                            mouseLeave: function (event, chartContext, config) {
                                chartContext.updateOptions({
                                    plotOptions: {
                                        pie: {
                                            donut: {
                                                labels: {
                                                    total: {
                                                        label: '100%',
                                                        formatter: () => '100%'
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }, false, false);
                            }
                        }
                    },
                    labels: ['Success', 'Pending', 'Failed'],
                    colors: ['#71dd37', '#eeb902', '#5664d2'],
                    dataLabels: { enabled: false },
                    legend: { show: false },
                    tooltip: {
                        y: {
                            formatter: function (val, opts) {
                                var percent = ((val / total) * 100).toFixed(1) + "%";
                                return percent;
                            }
                        }
                    },
                    plotOptions: {
                        pie: {
                            donut: {
                                size: '80%',
                                labels: {
                                    show: true,
                                    total: {
                                        show: true,
                                        label: '100%',
                                        formatter: function () {
                                            return '100%';
                                        }
                                    },
                                    value: { show: false }
                                }
                            }
                        }
                    }
                };
                new ApexCharts(document.querySelector(selector), chartOptions).render();
            }
            $.ajax({
                type: 'POST',
                url: 'Dashboard.aspx/GetActivationStats',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (res) {
                    var data = res.d;
                    var total = data.Success + data.Pending + data.Failed;
                    $("#successCount").text(formatNumberWithCommas(data.Success));
                    $("#pendingCount").text(formatNumberWithCommas(data.Pending));
                    $("#failedCount").text(formatNumberWithCommas(data.Failed));
                    $("#totalRequests").text(formatNumberWithCommas(total));
                    $("#activationTotalText").text(formatK(total) + " Total Request");
                    createDonutChart("#ActivationData", [data.Success, data.Pending, data.Failed]);
                },
                error: function () {
                    alert("Error loading Activation stats");
                }
            });
            $.ajax({
                type: 'POST',
                url: 'Dashboard.aspx/GetExtensionStats',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (res) {
                    var data = res.d;
                    var total = data.Success + data.Pending + data.Failed;
                    $("#extensionSuccessCount").text(formatNumberWithCommas(data.Success));
                    $("#extensionPendingCount").text(formatNumberWithCommas(data.Pending));
                    $("#extensionFailedCount").text(formatNumberWithCommas(data.Failed));
                    $("#extensionTotalRequests").text(formatNumberWithCommas(total));
                    $("#extensionTotalText").text(formatNumberWithCommas(total) + " Total Request");
                    createDonutChart("#ExtensionData", [data.Success, data.Pending, data.Failed]);

                },
                error: function () {
                    alert("Error loading Extension stats");
                }
            });
            $.ajax({
                type: 'POST',
                url: 'Dashboard.aspx/GetRevenueDetails',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (res) {
                    var data = res.d;
                    var total = data.TopupRefund + data.Activation + data.Recharge;
                    $("#RevenueTopupRefund").text(formatNumberWithCommas(data.TopupRefund));
                    $("#RevenueActivation").text(formatNumberWithCommas(data.Activation));
                    $("#RevenueRecharge").text(formatNumberWithCommas(data.Recharge));
                    $("#RevenueTotalAmount").text(formatNumberWithCommas(total));
                    $("#renewalTotalText").text(formatNumberWithCommas(total) + " Total Amount");
                    createDonutChart("#RevenueData", [data.TopupRefund, data.Activation, data.Recharge]);
                },
                error: function () {
                    alert("Error loading Revenue details.");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="container-xxl flex-grow-1 container-p-y">
            <div class="row">
                <!-- Welcome Card -->
                <div class="col-lg-6 mb-4 order-0">
                    <div class="card">
                        <div class="d-flex align-items-end row">
                            <div class="col-sm-7">
                                <div class="card-body">
                                    <h5 class="card-title text-primary">Welcome <span runat="server" id="spnDealerName"></span>! 🎉
                                    </h5>
                                    <p class="mb-4">Welcome to a space made just for you.</p>
                                    <a href="javascript:void(0);"
                                        class="btn btn-sm text-primary border-primary"
                                        style="background: transparent; cursor: default;">Account balance ($): <span runat="server" id="spnAccountBalance"></span>
                                    </a>
                                </div>
                            </div>
                            <div class="col-sm-5 text-center text-sm-left">
                                <div class="card-body pb-0 px-0 px-md-4">
                                    <img src="Design/assets/img/illustrations/man-with-laptop-light.png"
                                        height="140"
                                        alt="User Illustration"
                                        data-app-dark-img="illustrations/man-with-laptop-dark.png"
                                        data-app-light-img="illustrations/man-with-laptop-light.png" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Sims Summary -->
                <div class="col-lg-6 col-md-6 order-1">
                    <div class="row">
                        <!-- Free Sims -->
                        <div class="col-lg-4 col-md-12 col-4 mb-4">
                            <div class="card">
                                <div class="card-body">
                                    <div class="card-title d-flex align-items-start justify-content-between">
                                        <div class="avatar flex-shrink-0">
                                            <img src="Design/assets/img/icons/unicons/cc-primary.png" alt="Free Sims" class="rounded" />
                                        </div>
                                        <a href="SimList.aspx?Status=NG3OLDtv274ECMHO/pBUpA==" runat="server" class="btn p-0" title="View More">
                                            <i class="mdi mdi-eye" style="font-size: 25px;"></i>
                                        </a>
                                    </div>
                                    <span class="fw-semibold d-block mb-1">Free Sims</span>
                                    <h4 class="card-title mb-2" runat="server" id="FreeSims" style="margin-top: 10px;"></h4>
                                </div>
                            </div>
                        </div>
                        <!-- Active Sims -->
                        <div class="col-lg-4 col-md-12 col-4 mb-4">
                            <div class="card">
                                <div class="card-body">
                                    <div class="card-title d-flex align-items-start justify-content-between">
                                        <div class="avatar flex-shrink-0">
                                            <img src="Design/assets/img/icons/unicons/cc-primary.png" alt="Active Sims" class="rounded" />
                                        </div>
                                        <a href="SimList.aspx?Status=Gc5z6qrJmAnYynWiHXYOnA==" class="btn p-0" title="View More">
                                            <i class="mdi mdi-eye" style="font-size: 25px;"></i>
                                        </a>
                                    </div>
                                    <span class="fw-semibold d-block mb-1">Active Sims</span>
                                    <h4 class="card-title mb-2" runat="server" id="ActiveSims" style="margin-top: 10px;"></h4>
                                </div>
                            </div>
                        </div>
                        <!-- Lost Sims -->
                        <div class="col-lg-4 col-md-12 col-4 mb-4">
                            <div class="card">
                                <div class="card-body">
                                    <div class="card-title d-flex align-items-start justify-content-between">
                                        <div class="avatar flex-shrink-0">
                                            <img src="Design/assets/img/icons/unicons/cc-primary.png" alt="Lost Sims" class="rounded" />
                                        </div>
                                        <a href="SimList.aspx?Status=B9+j2I1v54/wfRYayGdj+w==" runat="server" class="btn p-0" title="View More">
                                            <i class="mdi mdi-eye" style="font-size: 25px;"></i>
                                        </a>
                                    </div>
                                    <span class="fw-semibold d-block mb-1">Lost Sims</span>
                                    <h4 class="card-title mb-2" runat="server" id="LostSims" style="margin-top: 10px;"></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Statistics Row -->
            <div class="row">
                <!-- Activation Statistics -->
                <div class="col-md-6 col-lg-4 col-xl-4 order-0 mb-4">
                    <div class="card h-100">
                        <div class="card-header d-flex align-items-center justify-content-between pb-0">
                            <div class="card-title mb-0">
                                <h5 class="m-0 me-2">Activation Statistics</h5>
                                <small class="text-muted" id="activationTotalText">Loading...</small>
                            </div>
                            <div class="dropdown">
                                <button class="btn p-0" type="button" id="btnActivationData" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu dropdown-menu-end" aria-labelledby="btnActivationData">
                                    <a class="dropdown-item" href="javascript:void(0);">Select All</a>
                                    <a class="dropdown-item" href="javascript:void(0);">Refresh</a>
                                    <a class="dropdown-item" href="javascript:void(0);">Share</a>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center mb-3">
                                <div class="d-flex flex-column align-items-center gap-1">
                                    <h2 class="mb-2" id="totalRequests">0</h2>
                                    <span>Total Request</span>
                                </div>
                                <div id="ActivationData" style="width: 120px; height: 120px;"></div>
                            </div>
                            <ul class="p-0 m-0">
                                <li class="d-flex mb-4 pb-1">
                                    <div class="avatar flex-shrink-0 me-3">
                                        <span class="avatar-initial rounded bg-label-success"><i class="bx bx-check-circle"></i></span>
                                    </div>
                                    <div class="d-flex w-100 flex-wrap align-items-center justify-content-between gap-2">
                                        <div class="me-2">
                                            <h6 class="mb-0">Success Request</h6>
                                            <small class="text-muted">Completed Successfully</small>
                                        </div>
                                        <div class="user-progress">
                                            <small class="fw-semibold" id="successCount">0</small>
                                        </div>
                                    </div>
                                </li>
                                <li class="d-flex mb-4 pb-1">
                                    <div class="avatar flex-shrink-0 me-3">
                                        <span class="avatar-initial rounded bg-label-warning"><i class="bx bx-time-five"></i></span>
                                    </div>
                                    <div class="d-flex w-100 flex-wrap align-items-center justify-content-between gap-2">
                                        <div class="me-2">
                                            <h6 class="mb-0">Pending Request</h6>
                                            <small class="text-muted">In Progress</small>
                                        </div>
                                        <div class="user-progress">
                                            <small class="fw-semibold" id="pendingCount">0</small>
                                        </div>
                                    </div>
                                </li>
                                <li class="d-flex mb-4 pb-1">
                                    <div class="avatar flex-shrink-0 me-3">
                                        <span class="avatar-initial rounded bg-label-primary"><i class="bx bx-x-circle"></i></span>
                                    </div>
                                    <div class="d-flex w-100 flex-wrap align-items-center justify-content-between gap-2">
                                        <div class="me-2">
                                            <h6 class="mb-0">Failed Request</h6>
                                            <small class="text-muted">Request Failed</small>
                                        </div>
                                        <div class="user-progress">
                                            <small class="fw-semibold" id="failedCount">0</small>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Extension Statistics -->
                <div class="col-md-6 col-lg-4 col-xl-4 order-0 mb-4">
                    <div class="card h-100">
                        <div class="card-header d-flex align-items-center justify-content-between pb-0">
                            <div class="card-title mb-0">
                                <h5 class="m-0 me-2">Extension Statistics</h5>
                                <small class="text-muted" id="extensionTotalText">0</small>
                            </div>
                            <div class="dropdown">
                                <button class="btn p-0" type="button" id="btnExtensionData" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu dropdown-menu-end" aria-labelledby="btnExtensionData">
                                    <a class="dropdown-item" href="javascript:void(0);">Select All</a>
                                    <a class="dropdown-item" href="javascript:void(0);">Refresh</a>
                                    <a class="dropdown-item" href="javascript:void(0);">Share</a>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center mb-3">
                                <div class="d-flex flex-column align-items-center gap-1">
                                    <h2 class="mb-2" id="extensionTotalRequests">0</h2>
                                    <span>Total Request</span>
                                </div>
                                <div id="ExtensionData" style="width: 120px; height: 120px;"></div>
                            </div>
                            <ul class="p-0 m-0">
                                <li class="d-flex mb-4 pb-1">
                                    <div class="avatar flex-shrink-0 me-3">
                                        <span class="avatar-initial rounded bg-label-success"><i class="bx bx-check-circle"></i></span>
                                    </div>
                                    <div class="d-flex w-100 flex-wrap align-items-center justify-content-between gap-2">
                                        <div class="me-2">
                                            <h6 class="mb-0">Success Request</h6>
                                            <small class="text-muted">Completed Successfully</small>
                                        </div>
                                        <div class="user-progress">
                                            <small class="fw-semibold" id="extensionSuccessCount">0</small>
                                        </div>
                                    </div>
                                </li>
                                <li class="d-flex mb-4 pb-1">
                                    <div class="avatar flex-shrink-0 me-3">
                                        <span class="avatar-initial rounded bg-label-warning"><i class="bx bx-time-five"></i></span>
                                    </div>
                                    <div class="d-flex w-100 flex-wrap align-items-center justify-content-between gap-2">
                                        <div class="me-2">
                                            <h6 class="mb-0">Pending Request</h6>
                                            <small class="text-muted">In Progress</small>
                                        </div>
                                        <div class="user-progress">
                                            <small class="fw-semibold" id="extensionPendingCount">0</small>
                                        </div>
                                    </div>
                                </li>
                                <li class="d-flex mb-4 pb-1">
                                    <div class="avatar flex-shrink-0 me-3">
                                        <span class="avatar-initial rounded bg-label-primary"><i class="bx bx-x-circle"></i></span>
                                    </div>
                                    <div class="d-flex w-100 flex-wrap align-items-center justify-content-between gap-2">
                                        <div class="me-2">
                                            <h6 class="mb-0">Failed Request</h6>
                                            <small class="text-muted">Request Failed</small>
                                        </div>
                                        <div class="user-progress">
                                            <small class="fw-semibold" id="extensionFailedCount">0</small>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Revenue Statistics -->
                <div class="col-md-6 col-lg-4 col-xl-4 order-0 mb-4">
                    <div class="card h-100">
                        <div class="card-header d-flex align-items-center justify-content-between pb-0">
                            <div class="card-title mb-0">
                                <h5 class="m-0 me-2">Revenue Statistics</h5>
                                <small class="text-muted" id="renewalTotalText"></small>
                            </div>
                            <div class="dropdown">
                                <button class="btn p-0" type="button" id="btnOrderChart" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu dropdown-menu-end" aria-labelledby="btnOrderChart">
                                    <a class="dropdown-item" href="javascript:void(0);">Select All</a>
                                    <a class="dropdown-item" href="javascript:void(0);">Refresh</a>
                                    <a class="dropdown-item" href="javascript:void(0);">Share</a>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center mb-3">
                                <div class="d-flex flex-column align-items-center gap-1">
                                    <h2 class="mb-2" id="RevenueTotalAmount">0</h2>
                                    <span>Total Request</span>
                                </div>
                                <div id="RevenueData" style="width: 120px; height: 120px;"></div>
                            </div>
                            <ul class="p-0 m-0">
                                <li class="d-flex mb-4 pb-1">
                                    <div class="avatar flex-shrink-0 me-3">
                                        <span class="avatar-initial rounded bg-label-success"><i class="bx bx-credit-card"></i></span>
                                    </div>
                                    <div class="d-flex w-100 flex-wrap align-items-center justify-content-between gap-2">
                                        <div class="me-2">
                                            <h6 class="mb-0">Topup/Refund Amount</h6>
                                            <small class="text-muted">By Payment Gateway</small>
                                        </div>
                                        <div class="user-progress">
                                            <small class="fw-semibold" id="RevenueTopupRefund">0</small>
                                        </div>
                                    </div>
                                </li>
                                <li class="d-flex mb-4 pb-1">
                                    <div class="avatar flex-shrink-0 me-3">
                                        <span class="avatar-initial rounded bg-label-warning"><i class="bx bx-credit-card"></i></span>
                                    </div>
                                    <div class="d-flex w-100 flex-wrap align-items-center justify-content-between gap-2">
                                        <div class="me-2">
                                            <h6 class="mb-0">Activation Amount</h6>
                                            <small class="text-muted">Network-1, Network-2</small>
                                        </div>
                                        <div class="user-progress">
                                            <small class="fw-semibold" id="RevenueActivation">0</small>
                                        </div>
                                    </div>
                                </li>
                                <li class="d-flex mb-4 pb-1">
                                    <div class="avatar flex-shrink-0 me-3">
                                        <span class="avatar-initial rounded bg-label-primary"><i class="bx bx-credit-card"></i></span>
                                    </div>
                                    <div class="d-flex w-100 flex-wrap align-items-center justify-content-between gap-2">
                                        <div class="me-2">
                                            <h6 class="mb-0">Recharge Amount</h6>
                                            <small class="text-muted">Network-1, Network-2</small>
                                        </div>
                                        <div class="user-progress">
                                            <small class="fw-semibold" id="RevenueRecharge">0</small>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="content-backdrop fade"></div>
    </div>
</asp:Content>
