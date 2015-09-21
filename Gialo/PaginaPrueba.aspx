<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PaginaPrueba.aspx.cs" Inherits="Gialo.PaginaPrueba" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <dx:ASPxGridView ID="grillaArticulos" ClientInstanceName="grid" runat="server"
        KeyFieldName="codigoArticulo" AutoGenerateColumns="False" Width="700px" 
        OnRowInserting="grillaArticulos_RowInserting" OnRowUpdating="grillaArticulos_RowUpdating">
        <Settings ShowFilterRow="true"/>
        <Columns>
            <dx:GridViewDataTextColumn FieldName="codigoArticulo" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Codigo">
                <EditFormSettings VisibleIndex="0" Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="descripcion" VisibleIndex="1" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Descripcion">
                <PropertiesTextEdit MaxLength="50" EnableClientSideAPI="True" >
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe ingresar una descripcion" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="1" CaptionLocation="Near" />
            </dx:GridViewDataTextColumn>
            <%--<dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" VisibleIndex="2" FixedStyle="None">
            </dx:GridViewCommandColumn>--%>
        </Columns>
        <SettingsEditing EditFormColumnCount="3" />
        <SettingsPager Mode="ShowAllRecords" />
        <Settings ShowTitlePanel="true" />
        <SettingsText Title="Items" />
    </dx:ASPxGridView>
</asp:Content>
