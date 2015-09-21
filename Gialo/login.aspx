<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Gialo.login" %>

<!DOCTYPE html>

<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
    <meta charset="utf-8" />
    <title>Giallo</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="../../assets/global/plugins/select2/select2.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/admin/pages/css/login-soft.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME STYLES -->
    <link href="../../assets/global/css/components.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/admin/layout/css/layout.css" rel="stylesheet" type="text/css" />
    <link id="style_color" href="../../assets/admin/layout/css/themes/darkblue.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/admin/layout/css/custom.css" rel="stylesheet" type="text/css" />
    <!-- END THEME STYLES -->
    <link rel="shortcut icon" href="assets/rouss/favicon.ico" />
</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="login">
    <!-- BEGIN LOGO -->
    <div class="logo">
        <%--<h1 style="color: white">Giallo</h1>--%>
        <a href="index.aspx" style="width: 300px">
            <img src="assets/giallo/giallo_imagen.jpg" alt="" style="width: 300px"/>
        </a>
    </div>
    <!-- END LOGO -->
    <!-- BEGIN SIDEBAR TOGGLER BUTTON -->
    <div class="menu-toggler sidebar-toggler">
    </div>
    <!-- END SIDEBAR TOGGLER BUTTON -->
    <!-- BEGIN LOGIN -->
    <form runat="server" id="form1">
        <div class="content">
            <!-- BEGIN LOGIN FORM -->
            <form class="login-form" action="index.aspx" method="post">
                <h3 class="form-title">Ingrese a su cuenta</h3>
                <a runat="server" id="msjErrorLogin" visible="false">
                    <div class="alert alert-danger">
                        <button class="close" data-close="alert"></button>
                        <span>Usuario o contraseña incorrectos. </span>
                    </div>
                </a>
                <div class="form-group">
                    <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                    <label class="control-label visible-ie8 visible-ie9">Usuario</label>
                    <div class="input-icon">
                        <i class="fa fa-user"></i>
                        <asp:TextBox class="form-control placeholder-no-fix" type="text" autocomplete="off" placeholder="Usuario" name="username" ID="txtUsuario" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label visible-ie8 visible-ie9">Contraseña</label>
                    <div class="input-icon">
                        <i class="fa fa-lock"></i>
                        <asp:TextBox class="form-control placeholder-no-fix" type="password" autocomplete="off" placeholder="Contraseña" name="password" ID="txtContraseña" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-actions">
                    <label class="checkbox">
                        <input type="checkbox" name="remember" value="1" />
                        Recordarme
                    </label>
                    <asp:Button type="submit" class="btn blue pull-right" ID="txtLogin" runat="server" Text="Login" OnClick="txtLogin_Click" /><%--<i class="m-icon-swapright m-icon-white"></i>--%>
                </div>
                <%--<div class="forget-password">
                    <h4>Olvidaste tu contraseña ?</h4>
                    <p>
                        no te preocupes, click <a href="javascript:;" id="forget-password">aqui </a>
                        para resetear la contraseña.
                    </p>
                </div>--%>
            </form>
            <!-- END LOGIN FORM -->
            <!-- BEGIN FORGOT PASSWORD FORM -->
            <form class="forget-form" action="index.aspx" method="post">
                <h3>Olvidaste tu contraseña ?</h3>
                <p>
                    Ingresa tu dirección de e-mail para resetear tu contraseña.
                </p>
                <div class="form-group">
                    <div class="input-icon">
                        <i class="fa fa-envelope"></i>
                        <asp:TextBox class="form-control placeholder-no-fix" type="text" autocomplete="off" placeholder="Email" name="email" ID="txtEmail" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-actions">
                    <button type="button" id="back-btn" class="btn">
                        <i class="m-icon-swapleft"></i>Volver
                    </button>
                    <asp:Button type="submit" class="btn blue pull-right" ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" /><%--<i class="m-icon-swapright m-icon-white"></i>--%>
                </div>
            </form>
            <!-- END FORGOT PASSWORD FORM -->
        </div>
    </form>
    <!-- END LOGIN -->
    <!-- BEGIN COPYRIGHT -->
    <div class="copyright">
        2015 &copy; Giallo| by <a href="http://www.sempait.com.ar" target="_blank">SempaIT</a>
    </div>
    <!-- END COPYRIGHT -->
    <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
    <!-- BEGIN CORE PLUGINS -->
    <!--[if lt IE 9]>
    <script src="../../assets/global/plugins/respond.min.js"></script>
    <script src="../../assets/global/plugins/excanvas.min.js"></script> 
    <![endif]-->
    <script src="../../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="../../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/backstretch/jquery.backstretch.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../assets/global/plugins/select2/select2.min.js"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="../../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../../assets/admin/layout/scripts/layout.js" type="text/javascript"></script>
    <script src="../../assets/admin/layout/scripts/demo.js" type="text/javascript"></script>
    <script src="../../assets/admin/pages/scripts/login-soft.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Login.init();
            Demo.init();
            // init background slide images
            //   $.backstretch([
            //    "../../assets/admin/pages/media/bg/1.jpg",
            //    "../../assets/admin/pages/media/bg/2.jpg",
            //    "../../assets/admin/pages/media/bg/3.jpg",
            //    "../../assets/admin/pages/media/bg/4.jpg"
            //   ], {
            //       fade: 1000,
            //       duration: 8000
            //   }
            //);
        });
    </script>
    
    <script>
        $(document).ready(function () {
            $("#txtUsuario").focus();
        });
    </script>
    <!-- END JAVASCRIPTS -->
</body>
<!-- END BODY -->
</html>

