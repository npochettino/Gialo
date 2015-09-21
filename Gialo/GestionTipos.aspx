<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="GestionTipos.aspx.cs" Inherits="Gialo.GestionTipos" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxGridView ID="grillaTipos" ClientInstanceName="grid" runat="server"
        KeyFieldName="codigoTipo" AutoGenerateColumns="False" Width="700px" Theme="Metropolis"
        OnRowInserting="grillaTipos_RowInserting" OnRowUpdating="grillaTipos_RowUpdating">
        <Settings ShowFilterRow="true" />
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="grillaTalles" ClientInstanceName="gridTalles" runat="server"
                    KeyFieldName="codigoTalle" AutoGenerateColumns="False" Width="700px" Theme="MetropolisBlue"
                    OnRowInserting="grillaTalles_RowInserting" OnRowUpdating="grillaTalles_RowUpdating" OnLoad="grillaTalles_Load">
                    <Settings ShowFilterRow="true" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="codigoTalle" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Código">
                            <EditFormSettings VisibleIndex="0" Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="descripcionTalle" VisibleIndex="1" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Descripción">
                            <PropertiesTextEdit MaxLength="50" EnableClientSideAPI="True">
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe ingresar una descripción" IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <EditFormSettings VisibleIndex="1" CaptionLocation="Near" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsEditing EditFormColumnCount="3" />
                    <SettingsPager Position="Bottom" Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" ShowSeparators="true">
                        <PageSizeItemSettings Items="10, 50" Visible="true" Position="Right" />
                    </SettingsPager>
                    <Settings ShowTitlePanel="true" />
                    <SettingsText Title="Talles" />
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
        <SettingsDetail ShowDetailRow="true" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="codigoTipo" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Código">
                <EditFormSettings VisibleIndex="0" Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="descripcion" VisibleIndex="1" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Descripción">
                <PropertiesTextEdit MaxLength="50" EnableClientSideAPI="True">
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
        <SettingsPager Position="Bottom" Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" ShowSeparators="true" > 
            <PageSizeItemSettings Items="10, 50" Visible="true" Position="Right"/>
        </SettingsPager>
        <Settings ShowTitlePanel="true" />
        <SettingsText Title="Tipos" />
    </dx:ASPxGridView>
</asp:Content>
