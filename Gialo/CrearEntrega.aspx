<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CrearEntrega.aspx.cs" Inherits="Gialo.CrearEntrega" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="CrearEntrega.js"></script>
    <style>
        .contenedorFecha {
            display: inline-flex;
        }

        .labelsCampos {
            color: white;
        }

        #ContentPlaceHolder1_botonAgregarTodos {
            margin-left: 300px;
        }

        #ContentPlaceHolder1_botonGuardar {
            margin-left: 293px;
        }
    </style>
    <asp:Label ID="titulo" runat="server" Font-Size="Large" ForeColor="White"></asp:Label>
    <br />
    <br />
    <div class="contenedorFecha">
        <asp:Label ID="Label2" runat="server" Width="120px" CssClass="labelsCampos">Fecha Desde</asp:Label>
        <dx:ASPxDateEdit ID="dateInicio" runat="server" EditFormat="Custom" Width="200">
            <CalendarProperties ShowClearButton="false"></CalendarProperties>
            <TimeSectionProperties>
                <TimeEditProperties EditFormatString="hh:mm tt" />
            </TimeSectionProperties>
        </dx:ASPxDateEdit>
    </div>
    <br />
    <br />
    <div class="contenedorFecha">
        <asp:Label ID="Label1" runat="server" Width="120px" CssClass="labelsCampos">Fecha Hasta</asp:Label>
        <dx:ASPxDateEdit ID="dateFin" runat="server" EditFormat="Custom" Width="200">
            <TimeSectionProperties>
                <TimeEditProperties EditFormatString="hh:mm tt" />
            </TimeSectionProperties>
        </dx:ASPxDateEdit>
    </div>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Width="120px" CssClass="labelsCampos">Tipo de taller</asp:Label>
    <asp:DropDownList runat="server" ID="comboTipoTaller" OnSelectedIndexChanged="comboTipoTaller_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Width="120px" CssClass="labelsCampos">Taller</asp:Label>
    <asp:DropDownList runat="server" ID="comboTaller"></asp:DropDownList>
    <br />
    <br />
    <dx:ASPxGridView ID="grillaTandaItem" ClientInstanceName="grillaTandaItem" runat="server" Font-Size="10"
        KeyFieldName="codigoTandaItem" AutoGenerateColumns="False" Width="700px" Theme="MetropolisBlue"
        OnStartRowEditing="grillaTandaItem_StartRowEditing">
        <ClientSideEvents EndCallback="grillaTandaItem_OnEndCallBack" />
        <Settings ShowFilterRow="true" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="codigoTandaItem" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Codigo" Visible="false" Width="120px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="tipo" VisibleIndex="1" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Tipo" Visible="true" Width="120px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="talle" VisibleIndex="2" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Talle" Visible="true" Width="120px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="color" VisibleIndex="3" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Color" Visible="true" Width="120px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="cantidad" VisibleIndex="4" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Cantidad en tanda" Width="120px">
                <PropertiesTextEdit MaxLength="100" EnableClientSideAPI="True" Width="50px">
                    <MaskSettings Mask="&lt;0..9999&gt;"></MaskSettings>
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe ingresar una cantidad" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="1" CaptionLocation="Near" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn Visible="true" Caption=" ">
                <EditButton Text="Agregar" Visible="true"></EditButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="cantidadDisponible" VisibleIndex="5" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Cantidad disponible" Visible="true" Width="120px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsEditing EditFormColumnCount="3" />
        <SettingsPager Mode="ShowAllRecords" />
        <Settings ShowTitlePanel="true" />
        <SettingsText Title="Talles de la Tanda" />

    </dx:ASPxGridView>
    <br />
    <asp:Button Text="Agregar todos" runat="server" ID="botonAgregarTodos" OnClick="botonAgregarTodos_Click" CssClass="btn-primary"/>
    <br />
    <br />
    <dx:ASPxGridView ID="grillaTandaItemAgregados" ClientInstanceName="grillaTandaItemAgregados" runat="server"
        KeyFieldName="codigoTandaItem" AutoGenerateColumns="False" Width="700px" Theme="MetropolisBlue"
        OnRowDeleting="grillaTandaItemAgregados_RowDeleting" Font-Size="10"
        OnRowValidating="grillaTandaItemAgregados_RowValidating" OnRowUpdating="grillaTandaItemAgregados_RowUpdating">
        <Settings ShowFilterRow="true" />
        <ClientSideEvents EndCallback="grillaTandaItemAgregados_OnEndCallBack" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="codigoTandaItem" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Codigo" Visible="false" Width="120px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="tipo" VisibleIndex="1" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Tipo" Visible="true" Width="120px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="talle" VisibleIndex="2" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Talle" Visible="true" Width="120px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="color" VisibleIndex="3" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Color" Visible="true" Width="120px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="cantidad" VisibleIndex="4" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Cantidad en entrega" Width="120px">
                <PropertiesTextEdit MaxLength="100" EnableClientSideAPI="True" Width="50px">
                    <MaskSettings Mask="&lt;0..9999&gt;"></MaskSettings>
                    <ValidationSettings>
                        <RequiredField ErrorText="Debe ingresar una cantidad" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="1" CaptionLocation="Near" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="cantidadDisponible" VisibleIndex="5" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Caption="Cantidad disponible" Visible="true" Width="120px">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsEditing EditFormColumnCount="1" />
        <SettingsPager Mode="ShowAllRecords" />
        <Settings ShowTitlePanel="true" />
        <SettingsText Title="Talles en la Entrega" />
    </dx:ASPxGridView>
    <br />
    <asp:Button runat="server" ID="botonGuardar" Text="Guardar" OnClick="botonGuardar_Click"  CssClass="btn-primary"/>
    <asp:Button runat="server" ID="botonVolver" Text="Volver" OnClick="botonVolver_Click"  CssClass="btn-danger"/>
</asp:Content>
