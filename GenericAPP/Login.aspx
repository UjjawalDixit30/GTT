<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GenericAPP.Login" %>

<!DOCTYPE html>
<html lang="en" class="light-style customizer-hide" dir="ltr" data-theme="theme-default" data-assets-path="Design/assets/" data-template="vertical-menu-template-free">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0" />
    <title>Global Travel Telecom</title>
    <meta name="description" content="" />
    <link rel="icon" type="image/x-icon" href="Design/assets/img/favicon/favicon.ico" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Public+Sans:ital,wght@0,300;0,400;0,500;0,600;0,700;1,300;1,400;1,500;1,600;1,700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="Design/assets/vendor/fonts/boxicons.css" />
    <link rel="stylesheet" href="Design/assets/vendor/css/core.css" class="template-customizer-core-css" />
    <link rel="stylesheet" href="Design/assets/vendor/css/theme-default.css" class="template-customizer-theme-css" />
    <link rel="stylesheet" href="Design/assets/css/demo.css" />
    <link rel="stylesheet" href="Design/assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.css" />
    <link rel="stylesheet" href="Design/assets/vendor/css/pages/page-auth.css" />
    <script src="Design/assets/vendor/js/helpers.js"></script>
    <script src="Design/assets/js/config.js"></script>
</head>

<body>
    <div class="container-xxl">
        <div class="authentication-wrapper authentication-basic container-p-y">
            <div class="authentication-inner">
                <div class="card">
                    <div class="card-body">
                        <div class="app-brand justify-content-center">
                            <a href="Login.aspx" class="app-brand-link gap-2">
                                <span class="app-brand-logo demo">
                                    <img src="Design/assets/img/Logo.png" />
                                </span>
                            </a>
                        </div>
                        <h5 class="mb-2" style="text-align: center;">Welcome to Global Travel Telecom! 👋</h5>
                        <p class="mb-4" style="text-align: center;">Please sign-in to your account and start the adventure</p>
                        <form id="formAuthentication" runat="server" class="mb-3">

                            <div class="mb-3">
                                <input type="text" class="form-control" runat="server" id="txtemail" name="email-username" placeholder="Please enter username" autofocus />
                            </div>
                            <div class="mb-3">
                                <div class="input-group input-group-merge">
                                    <input type="password" id="txtpassword" class="form-control" name="password" runat="server" placeholder="Please enter password" aria-describedby="password" />
                                </div>
                            </div>
                            <div class="mb-3">
                                <asp:Button ID="btnsubmit" type="button" runat="server" class="btn btn-primary d-grid w-100" Text="Sign in" OnClick="btnsubmit_Click" OnClientClick="if (!ValidateSubmitControls()) { return false;};" />
                            </div>
                            <div class="mb-3" style="text-align: center;">
                                <a href="ForgotPassword.aspx">
                                    <small>Forgot Password?</small>
                                </a>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="Design/assets/js/main.js"></script>
    <script src="Design/assets/vendor/js/menu.js"></script>
    <script src="Design/assets/vendor/js/bootstrap.js"></script>
    <script src="Design/assets/vendor/libs/jquery/jquery.js"></script>
    <script src="Design/assets/vendor/libs/popper/popper.js"></script>
    <script async defer src="https://buttons.github.io/buttons.js"></script>
    <script src="Design/assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.js"></script>
    <script>
        function ValidateSubmitControls() {
            if ($("#txtemail").val() == '') {
                alert("Please enter username.");
                $("#txtemail").focus();
                return false;
            }
            if ($("#txtpassword").val() == '') {
                alert("Please enter password.");
                $("#txtpassword").focus();
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
