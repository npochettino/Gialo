<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="GestionTandas.aspx.cs" Inherits="Gialo.GestionTandas" %>

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
            if (grillaTandaItem.GetEditor("codigoTalle").InCallback())
                ultimoTipo = cmbTipo.GetValue().toString();
            else
                grillaTandaItem.GetEditor("codigoTalle").PerformCallback(cmbTipo.GetValue().toString());
        }
        function OnEndCallback(s, e) {
            if (ultimoTipo) {
                grillaTandaItem.GetEditor("codigoTalle").PerformCallback(ultimoTipo);
                ultimoTipo = null;
            }
        }


        function grillaTandaItems_EndCallback(s, e) {
            if (s.cpBorrar != undefined && s.cpBorrar != "") {
                alert(s.cpBorrar);
                delete (s.cpBorrar);
            }
        }
        function grillaTandas_EndCallback(s, e) {
            if (s.cpBorrar != undefined && s.cpBorrar != "") {
                alert(s.cpBorrar);
                delete (s.cpBorrar);
            }
        }
    </script>
    <dx:ASPxGridView ID="grillaTandas" ClientInstanceName="grillaTandas" runat="server"
        KeyFieldName="codigoTanda" AutoGenerateColumns="False" Width="700px"
        OnRowInserting="grillaTandas_RowInserting" OnRowUpdating="grillaTandas_RowUpdating" OnRowValidating="grillaTandas_RowValidating"
        OnRowDeleting="grillaTandas_RowDeleting"
        Theme="Metropolis">
        <Settings ShowFilterRow="true" />
        <ClientSideEvents EndCallback="function (s, e) {grillaTandas_EndCallback(s, e);}"/>
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="grillaTandaItem" ClientInstanceName="grillaTandaItem" runat="server"
                    KeyFieldName="codigoTandaItem" AutoGenerateColumns="False" Width="700px" Theme="MetropolisBlue"
                    OnRowInserting="grillaTandaItem_RowInserting" OnRowUpdating="grillaTandaItem_RowUpdating" OnLoad="grillaTandaItem_Load"
                    OnRowValidating="grillaTandaItem_RowValidating" OnInitNewRow="grillaTandaItem_InitNewRow" OnStartRowEditing="grillaTandaItem_StartRowEditing"
                    OnCellEditorInitialize="grillaTandaItem_CellEditorInitialize" OnRowDeleting="grillaTandaItem_RowDeleting">
                    <Settings ShowFilterRow="true" />
                    <ClientSideEvents EndCallback="function (s, e) {grillaTandaItems_EndCallback(s, e);}"/>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="codigoTandaItem" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Codigo" Visible="false">
                            <EditFormSettings VisibleIndex="0" Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Tipo"
                            FieldName="codigoTipo" Name="codigoTipo" VisibleIndex="1" CellStyle-CssClass=""
                            Width="150px" Visible="True">
                            <PropertiesComboBox DataSourceID="odsTipos" Width="150px" TextField="descripcion" ValueField="codigoTipo"
                                ValueType="System.String" EnableIncrementalFiltering="True">
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe seleccionar un Tipo" IsRequired="True" />
                                </ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnTipoChanged(s); }"></ClientSideEvents>
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" />
                            <EditFormCaptionStyle HorizontalAlign="Right">
                            </EditFormCaptionStyle>
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Talle"
                            FieldName="codigoTalle" Name="codigoTalle" VisibleIndex="2" CellStyle-CssClass=""
                            Width="150px" Visible="True">
                            <PropertiesComboBox DataSourceID="odsTalles" Width="150px" TextField="descripcionTalle" ValueField="codigoTalle"
                                ValueType="System.String" EnableIncrementalFiltering="True">
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe seleccionar un Talle" IsRequired="True" />
                                </ValidationSettings>
                                <ClientSideEvents EndCallback="OnEndCallback" />
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" />
                            <EditFormCaptionStyle HorizontalAlign="Right">
                            </EditFormCaptionStyle>
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Color"
                            FieldName="codigoColor" Name="codigoColor" VisibleIndex="3" CellStyle-CssClass=""
                            Width="150px" Visible="True">
                            <PropertiesComboBox DataSourceID="odsColores" Width="150px" TextField="descripcion" ValueField="codigoColor"
                                ValueType="System.String" EnableIncrementalFiltering="True">
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe seleccionar un Color" IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" />
                            <EditFormCaptionStyle HorizontalAlign="Right">
                            </EditFormCaptionStyle>
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn FieldName="cantidad" VisibleIndex="4" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Cantidad" Width="100px">
                            <PropertiesTextEdit MaxLength="100" EnableClientSideAPI="True" Width="50px">
                                <MaskSettings Mask="&lt;0..9999&gt;"></MaskSettings>
                                <ValidationSettings>
                                    <RequiredField ErrorText="Debe ingresar una cantidad" IsRequired="True" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <EditFormSettings VisibleIndex="1" CaptionLocation="Near" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsEditing EditFormColumnCount="3" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <Settings ShowTitlePanel="true" />
                    <SettingsText Title="Talles por Tanda" />
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
        <SettingsDetail ShowDetailRow="true" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="codigoTanda" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Código" Visible="false">
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
            <dx:GridViewDataComboBoxColumn Caption="Artículo"
                FieldName="codigoArticulo" Name="codigoArticulo" VisibleIndex="2" CellStyle-CssClass=""
                Width="100px" Visible="True">
                <PropertiesComboBox DataSourceID="odsArticulos" Width="100px" TextField="descripcion" ValueField="codigoArticulo"
                    ValueType="System.String" EnableIncrementalFiltering="True">
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe seleccionar un artículo" IsRequired="True" />
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
</asp:Content>
