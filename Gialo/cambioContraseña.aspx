<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="cambioContraseña.aspx.cs" Inherits="Gialo.cambioContraseña" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .page-content {
            width: 500px !important;
            margin-left: 0px !important;
            height: 451px !important;
        }
    </style>
    <div style="width:300px; background:#CFCACA; padding:10px; height: 335px">
        <h3 class="page-title">Perfil <small>cambio de contraseña</small>
        </h3>
        <div class="form-body">
            <div class="form-group">
                <label class="control-label">Usuario</label>
                <asp:TextBox class="form-control" type="text" autocomplete="off" placeholder="" name="username" ID="txtUsuario" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label class="control-label">Nueva contraseña</label>
                <div class="input-group">
                    <asp:TextBox class="form-control placeholder-no-fix" type="password" autocomplete="off" placeholder="ingrese contraseña" name="password" ID="txtOldPassword" runat="server"></asp:TextBox>
                    <span class="input-group-addon">
                        <i class="fa fa-user"></i>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label">Repita nueva contraseña</label>
                <div class="input-group">
                    <asp:TextBox class="form-control placeholder-no-fix" type="password" autocomplete="off" placeholder="repita la contraseña" name="password" ID="txtNewPassword" runat="server"></asp:TextBox>
                    <span class="input-group-addon">
                        <i class="fa fa-user"></i>
                    </span>
                </div>
                <label id="lblPassword" class="help-block" style="color: red" visible="false" runat="server"></label>
            </div>
        </div>
        <div class="form-actions">
            <div class="btn-set pull-right">
                <asp:Button ID="btnConfirmar" class="btn blue" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" />
                <asp:Button ID="btnCancelar" class="btn red" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
            </div>
        </div>
    </div>
    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core componets
            Layout.init(); // init layout
            QuickSidebar.init(); // init quick sidebar
            Demo.init(); // init demo features 
            Index.init();
            Index.initDashboardDaterange();
            Index.initJQVMAP(); // init index page's custom scripts
            Index.initCalendar(); // init index page's custom scripts
            Index.initCharts(); // init index page's custom scripts
            Index.initChat();
            Index.initMiniCharts();
            Tasks.initDashboardWidget();
        });
    </script>
    <!-- END JAVASCRIPTS -->

    <dx:ASPxPopupControl ClientInstanceName="pcCambioPassword" Width="250px" Height="250px"
        MaxWidth="800px" MaxHeight="800px" MinHeight="150px" MinWidth="150px" CloseOnEscape="true" ID="pcCambioPassword"
        AllowDragging="True" PopupElementID="imgButton" HeaderText=""
        runat="server" EnableViewState="False" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        EnableHierarchyRecreation="True" Modal="True" Theme="Metropolis" PopupAnimationType="Slide" CloseButtonStyle-Wrap="False">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <asp:Panel ID="Panel1" runat="server">
                    <div class="row">

                        <div class="col-md-12">
                            <div class="portlet box yellow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Cambio de Contraseña
                                    </div>

                                </div>
                                <div class="portlet-body form">
                                    <!-- BEGIN FORM-->

                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="control-label">El cambió de contraseña se realizo con éxito.</label>
                                        </div>
                                    </div>
                                    <!-- END FORM-->
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>

