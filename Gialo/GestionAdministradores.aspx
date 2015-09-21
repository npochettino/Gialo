<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="GestionAdministradores.aspx.cs" Inherits="Gialo.GestionAdministradores" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxGridView ID="grillaAdministradores" ClientInstanceName="grid" runat="server"
        KeyFieldName="idAdministrador" AutoGenerateColumns="False" Width="700px" Theme="Metropolis" 
        OnRowInserting="grillaAdministradores_RowInserting" OnRowValidating="grillaAdministradores_RowValidating">
        <Settings ShowFilterRow="true"/>
        <Columns>
            <dx:GridViewDataTextColumn Visible="false" FieldName="idAdministrador" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="ID">
                <EditFormSettings VisibleIndex="0" Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="usuario" VisibleIndex="1" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Nombre Usuario">
                <PropertiesTextEdit MaxLength="50" EnableClientSideAPI="True" >
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe ingresar un nombre de usuario" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="1" CaptionLocation="Near" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="contraseña" VisibleIndex="2" Visible="false" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Contraseña">
                <PropertiesTextEdit MaxLength="50" EnableClientSideAPI="True" Password="true">
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe ingresar una contraseña" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="2" CaptionLocation="Near" Visible="true"/>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="contraseñaRepetir" VisibleIndex="3" Visible="false" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Repita Contraseña">
                <PropertiesTextEdit MaxLength="50" EnableClientSideAPI="True" Password="true">
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe ingresar una contraseña" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="3" CaptionLocation="Near" Visible="true"/>
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsEditing EditFormColumnCount="2" />
        <SettingsPager Position="Bottom" Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" ShowSeparators="true" > 
            <PageSizeItemSettings Items="1, 10, 50" Visible="true" Position="Right"/>
        </SettingsPager>
        <Settings ShowTitlePanel="true" />
        <SettingsText Title="Administradores" />
    </dx:ASPxGridView>
</asp:Content>
