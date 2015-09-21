<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="GestionEntregas.aspx.cs" Inherits="Gialo.GestionEntregas" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .dxgvEditFormTable_Metropolis td:not(.dxgvCommandColumn_Metropolis) {
            float: none;
        }
    </style>
    <script>
        var ultimoTipo = null;
        function OnTipoChanged(cmbTipo) {
            if (grillaEntregas.GetEditor("codigoTaller").InCallback())
                ultimoTipo = cmbTipo.GetValue().toString();
            else
                grillaEntregas.GetEditor("codigoTaller").PerformCallback(cmbTipo.GetValue().toString());
        }
        function OnEndCallback(s, e) {
            if (ultimoTipo) {
                grillaEntregas.GetEditor("codigoTaller").PerformCallback(ultimoTipo);
                ultimoTipo = null;
            }
        }
        function grillaEntregas_EndCallback(s, e) {
            if (s.cpPuedeAgregar != undefined && s.cpPuedeAgregar != "") {
                alert(s.cpPuedeAgregar);
                delete (s.cpPuedeAgregar);
            }
            if (s.cpRedirecciona != undefined && s.cpRedirecciona != "") {
                window.location.assign('CrearEntrega.aspx');
            }
            if (s.cpBorrar != undefined && s.cpBorrar != "") {
                alert(s.cpBorrar);
                delete (s.cpBorrar);
            }
        }
    </script>
    <dx:ASPxGridView ID="grillaTandas" ClientInstanceName="grillaTandas" runat="server"
        KeyFieldName="codigoTanda" AutoGenerateColumns="False" Width="700px"
        Theme="Metropolis">
        <SettingsCommandButton></SettingsCommandButton>
        <Settings ShowFilterRow="true" />
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="grillaEntregas" ClientInstanceName="grillaEntregas" runat="server"
                    KeyFieldName="codigoEntrega" AutoGenerateColumns="False" Width="700px" Theme="MetropolisBlue"
                    OnRowInserting="grillaEntregas_RowInserting" OnRowUpdating="grillaEntregas_RowUpdating" OnLoad="grillaEntregas_Load" OnStartRowEditing="grillaEntregas_StartRowEditing"
                    OnRowValidating="grillaEntregas_RowValidating" OnInitNewRow="grillaEntregas_InitNewRow"
                    OnCellEditorInitialize="grillaEntregas_CellEditorInitialize" OnRowDeleting="grillaEntregas_RowDeleting">
                    <Settings ShowFilterRow="true" />
                    <ClientSideEvents EndCallback="function (s, e) {grillaEntregas_EndCallback(s, e);}" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="codigoEntrega" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Codigo" Visible="false">
                            <EditFormSettings VisibleIndex="0" Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Tipo de Taller"
                            FieldName="codigoTipoTaller" Name="codigoTipoTaller" VisibleIndex="1" CellStyle-CssClass=""
                            Width="150px" Visible="True">
                            <PropertiesComboBox DataSourceID="odsTiposTaller" Width="150px" TextField="descripcion" ValueField="codigoTipoTaller"
                                ValueType="System.String" EnableIncrementalFiltering="True">
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe seleccionar un Tipo de Taller" IsRequired="True" />
                                </ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnTipoChanged(s); }"></ClientSideEvents>
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" />
                            <EditFormCaptionStyle HorizontalAlign="Right">
                            </EditFormCaptionStyle>
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Taller"
                            FieldName="codigoTaller" Name="codigoTaller" VisibleIndex="2" CellStyle-CssClass=""
                            Width="150px" Visible="True">
                            <PropertiesComboBox DataSourceID="odsTalleres" Width="150px" TextField="descripcion" ValueField="codigoTaller"
                                ValueType="System.String" EnableIncrementalFiltering="True">
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe seleccionar un Taller" IsRequired="True" />
                                </ValidationSettings>
                                <ClientSideEvents EndCallback="OnEndCallback" />
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" />
                            <EditFormCaptionStyle HorizontalAlign="Right">
                            </EditFormCaptionStyle>
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataDateColumn FieldName="fechaInicio" Name="fechaInicio" Caption="Fecha Inicio"
                            VisibleIndex="3" Width="125px">
                            <PropertiesDateEdit Width="125px" CalendarProperties-ClearButtonText="Limpiar"
                                CalendarProperties-TodayButtonText="Hoy">
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe ingresar una fecha de inicio" IsRequired="True"></RequiredField>
                                </ValidationSettings>
                            </PropertiesDateEdit>
                            <EditFormSettings Visible="True" />
                            <EditFormCaptionStyle HorizontalAlign="Right">
                            </EditFormCaptionStyle>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="fechaFin" Name="fechaFin" Caption="Fecha Fin"
                            VisibleIndex="4" Width="125px">
                            <PropertiesDateEdit Width="125px" CalendarProperties-ClearButtonText="Limpiar"
                                CalendarProperties-TodayButtonText="Hoy">
                            </PropertiesDateEdit>
                            <EditFormSettings Visible="True" />
                            <EditFormCaptionStyle HorizontalAlign="Right">
                            </EditFormCaptionStyle>
                        </dx:GridViewDataDateColumn>
                    </Columns>
                    <SettingsEditing EditFormColumnCount="2" />
                    <SettingsPager Position="Bottom" Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" ShowSeparators="true">
                        <PageSizeItemSettings Items="10, 50" Visible="true" Position="Right" />
                    </SettingsPager>
                    <Settings ShowTitlePanel="true" />
                    <SettingsText Title="Entregas por Tanda" />
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
        <SettingsDetail ShowDetailRow="true" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="codigoTanda" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Codigo" Visible="false">
                <EditFormSettings VisibleIndex="0" Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="comentario" VisibleIndex="1" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Comentario" Width="250px">
                <PropertiesTextEdit MaxLength="100" EnableClientSideAPI="True" Width="200px">
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe ingresar un comentario" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="1" CaptionLocation="Near" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Articulo"
                FieldName="codigoArticulo" Name="codigoArticulo" VisibleIndex="2" CellStyle-CssClass=""
                Width="100px" Visible="True">
                <PropertiesComboBox DataSourceID="odsArticulos" Width="100px" TextField="descripcion" ValueField="codigoArticulo"
                    ValueType="System.String" EnableIncrementalFiltering="True">
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe seleccionar un articulo" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <EditFormSettings Visible="True" />
                <EditFormCaptionStyle HorizontalAlign="Right">
                </EditFormCaptionStyle>
                <EditCellStyle HorizontalAlign="Left">
                </EditCellStyle>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn FieldName="fechaInicio" Name="fechaInicio" Caption="Fecha Inicio"
                VisibleIndex="2" Width="125px">
                <PropertiesDateEdit Width="125px" CalendarProperties-ClearButtonText="Limpiar"
                    CalendarProperties-TodayButtonText="Hoy">
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe ingresar una fecha de inicio" IsRequired="True"></RequiredField>
                    </ValidationSettings>
                </PropertiesDateEdit>
                <EditFormSettings Visible="True" />
                <EditFormCaptionStyle HorizontalAlign="Right">
                </EditFormCaptionStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="fechaFin" Name="fechaFin" Caption="Fecha Fin"
                VisibleIndex="3" Width="125px">
                <PropertiesDateEdit Width="125px" CalendarProperties-ClearButtonText="Limpiar"
                    CalendarProperties-TodayButtonText="Hoy">
                </PropertiesDateEdit>
                <EditFormSettings Visible="True" />
                <EditFormCaptionStyle HorizontalAlign="Right">
                </EditFormCaptionStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewCommandColumn Visible="false">
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsEditing EditFormColumnCount="3" />
        <SettingsPager Position="Bottom" Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" ShowSeparators="true" > 
            <PageSizeItemSettings Items="10, 50" Visible="true" Position="Right"/>
        </SettingsPager>
        <Settings ShowTitlePanel="true" />
        <SettingsText Title="Tandas" />
    </dx:ASPxGridView>
    <asp:ObjectDataSource ID="odsArticulos" runat="server" SelectMethod="RecuperarTodosArticulos"
        TypeName="BibliotecaGiallo.Controladores.ControladorGeneral"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsTipos" runat="server" SelectMethod="RecuperarTodosTipos"
        TypeName="BibliotecaGiallo.Controladores.ControladorGeneral"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsTalles" runat="server" SelectMethod="RecuperarTodosTalles"
        TypeName="BibliotecaGiallo.Controladores.ControladorGeneral"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsColores" runat="server" SelectMethod="RecuperarTodosColores"
        TypeName="BibliotecaGiallo.Controladores.ControladorGeneral"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsTiposTaller" runat="server" SelectMethod="RecuperarTodosTipoTalleres"
        TypeName="BibliotecaGiallo.Controladores.ControladorGeneral"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsTalleres" runat="server" SelectMethod="RecuperarTodosTalleres"
        TypeName="BibliotecaGiallo.Controladores.ControladorGeneral"></asp:ObjectDataSource>
</asp:Content>
