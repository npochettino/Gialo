<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prueba.aspx.cs" Inherits="Gialo.Prueba" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 col-xs-12">
                        <asp:CheckBox Text="Historico completo" runat="server" ID="checkTodas" OnCheckedChanged="checkTodas_CheckedChanged" AutoPostBack="true"
                            ForeColor="White" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-xs-12">
                        <div style="float: left; color: white">
                            <asp:Label ID="Label2" runat="server">Fecha Desde</asp:Label>
                        </div>
                        <div style="float: left; margin-left: 10px">
                            <dx:aspxdateedit id="dateInicio" runat="server" editformat="Custom" width="200">
                        <TimeSectionProperties>
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                    </dx:aspxdateedit>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-xs-12">
                        <div style="float: left; color: white">
                            <asp:Label ID="Label1" runat="server">Fecha Hasta</asp:Label>
                        </div>
                        <div style="float: left; margin-left: 13px">
                            <dx:aspxdateedit id="dateFin" runat="server" editformat="Custom" width="200">
                        <TimeSectionProperties>
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                    </dx:aspxdateedit>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-xs-12">
                        <div style="float: left; color: white">
                            <asp:Label ID="Label3" runat="server">Articulos</asp:Label>
                        </div>
                        <div style="float: left; margin-left: 35px">
                            <asp:DropDownList runat="server" ID="comboArticulos"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-xs-12">
                        <asp:Button Text="Generar Reporte" CssClass="btn btn-primary" runat="server" ID="botonGenerar" OnClick="botonGenerar_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-xs-12">
                        <rsweb:reportviewer id="ReportViewer1" runat="server" AsyncRendering="false"></rsweb:reportviewer>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
