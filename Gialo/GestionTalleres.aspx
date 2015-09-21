<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="GestionTalleres.aspx.cs" Inherits="Gialo.GestionTalleres" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function grillaTalleres_EndCallback(s, e) {
            if (s.cpBorrar != undefined && s.cpBorrar != "") {
                alert(s.cpBorrar);
                delete (s.cpBorrar);
            }
        }
    </script>
    <dx:ASPxGridView ID="grillaTiposTalleres" ClientInstanceName="grid" runat="server"
        KeyFieldName="codigoTipoTaller" AutoGenerateColumns="False" Width="700px"
        OnRowInserting="grillaTiposTalleres_RowInserting" OnRowUpdating="grillaTiposTalleres_RowUpdating" Theme="Metropolis">
        <Settings ShowFilterRow="true" />
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="grillaTalleres" ClientInstanceName="grillaTalleres" runat="server"
                    KeyFieldName="codigoTaller" AutoGenerateColumns="False" Width="700px" Theme="MetropolisBlue"
                    OnRowInserting="grillaTalleres_RowInserting" OnRowUpdating="grillaTalleres_RowUpdating" OnLoad="grillaTalleres_Load"
                    OnRowDeleting="grillaTalleres_RowDeleting">
                    <ClientSideEvents EndCallback="function (s, e) {grillaTalleres_EndCallback(s, e);}" />
                    <Settings ShowFilterRow="true" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="codigoTaller" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Codigo">
                            <EditFormSettings VisibleIndex="0" Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="descripcion" VisibleIndex="1" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Descripción">
                            <PropertiesTextEdit MaxLength="50" EnableClientSideAPI="True">
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe ingresar una descripción" IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <EditFormSettings VisibleIndex="1" CaptionLocation="Near" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="responsable" VisibleIndex="2" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Responsable">
                            <PropertiesTextEdit MaxLength="100" EnableClientSideAPI="True" Width="200px">
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe ingresar un responsable" IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <EditFormSettings VisibleIndex="2" CaptionLocation="Near" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="contacto" VisibleIndex="3" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Contacto">
                            <PropertiesTextEdit MaxLength="100" EnableClientSideAPI="True" Width="200px">
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe ingresar un contacto" IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <EditFormSettings VisibleIndex="3" CaptionLocation="Near" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsEditing EditFormColumnCount="2" />
                    <SettingsPager Position="Bottom" Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" ShowSeparators="true">
                        <PageSizeItemSettings Items="10, 50" Visible="true" Position="Right" />
                    </SettingsPager>
                    <Settings ShowTitlePanel="true" />
                    <SettingsText Title="Talleres" />
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
        <SettingsDetail ShowDetailRow="true" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="codigoTipoTaller" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Código">
                <EditFormSettings VisibleIndex="0" Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="descripcion" VisibleIndex="1" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Descripción">
                <PropertiesTextEdit MaxLength="50" EnableClientSideAPI="True">
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe ingresar una descripción" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="1" CaptionLocation="Near" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsEditing EditFormColumnCount="3" />
        <SettingsPager Position="Bottom" Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" ShowSeparators="true" > 
            <PageSizeItemSettings Items="10, 50" Visible="true" Position="Right"/>
        </SettingsPager>
        <Settings ShowTitlePanel="true" />
        <SettingsText Title="Tipos de Talleres" />
    </dx:ASPxGridView>
</asp:Content>
