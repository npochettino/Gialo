﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ReporteTandas.aspx.cs" Inherits="Gialo.ReporteTandas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .row {
            height: 30px;
        }

        #ctl00_ContentPlaceHolder1_ReportViewer1 {
            width: 100% !important;
            border: 1px white solid;
            background:white;
        }
    </style>
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
                    <dx:ASPxDateEdit ID="dateInicio" runat="server" EditFormat="Custom" Width="200">
                        <TimeSectionProperties>
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                    </dx:ASPxDateEdit>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-xs-12">
                <div style="float: left; color: white">
                    <asp:Label ID="Label1" runat="server">Fecha Hasta</asp:Label>
                </div>
                <div style="float: left; margin-left: 13px">
                    <dx:ASPxDateEdit ID="dateFin" runat="server" EditFormat="Custom" Width="200">
                        <TimeSectionProperties>
                            <TimeEditProperties EditFormatString="hh:mm tt" />
                        </TimeSectionProperties>
                    </dx:ASPxDateEdit>
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
                <asp:Button Text="Generar Reporte"  runat="server" ID="botonGenerar" CssClass="btn-primary" OnClick="botonGenerar_Click" />
            </div>
        </div>
        <div class="row" style="height: auto">
            <div class="col-md-12 col-xs-12">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
            </div>
        </div>
    </div>
</asp:Content>
