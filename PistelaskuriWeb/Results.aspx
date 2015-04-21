<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Results.aspx.cs" Inherits="Results" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style20 {
            font-size: x-large;
        }
        .auto-style21 {
            width: 100%;
        }
        .auto-style22 {
        width: 382px;
    }
    .auto-style23 {
        width: 382px;
        height: 23px;
    }
    .auto-style24 {
        height: 23px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <span class="auto-style20"><strong>Tulokset:</strong></span><br />
        Valitse haluatko katsella näyttely- vai koetuloksia<br />
        <asp:DropDownList ID="DropDownListChooseType" runat="server">
            <asp:ListItem>Koetulokset</asp:ListItem>
            <asp:ListItem>Näyttelytulokset</asp:ListItem>
            <asp:ListItem>Harrastusdoggikisa</asp:ListItem>
            <asp:ListItem>Kultadoggikisa</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <asp:Button ID="ButtonGetResults" runat="server" Text="Hae tulokset" OnClick="ButtonGetResults_Click" />
        <br />
        <asp:ListBox ID="ListBoxResults" runat="server" Height="275px" Width="878px"></asp:ListBox>
        <br />
        <table class="auto-style21">
            <tr>
                <td class="auto-style23">Harrastusdoggikisa:</td>
                <td class="auto-style24">Kultadoggikisa:</td>
            </tr>
            <tr>
                <td class="auto-style22">
<asp:Chart ID="ChartTest" runat="server" DataSourceID="SqlDataSource1" Palette="SeaGreen">
    <series>
        <asp:Series Name="Series1" XValueMember="VirName" YValueMembers="FullpointsTest">
        </asp:Series>
    </series>
    <chartareas>
        <asp:ChartArea Name="ChartArea1">
        </asp:ChartArea>
    </chartareas>
</asp:Chart>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString1 %>" DeleteCommand="DELETE FROM [Dog] WHERE [VirName] = @VirName" InsertCommand="INSERT INTO [Dog] ([VirName], [KutsName], [FullpointsShow], [FullpointsTest]) VALUES (@VirName, @KutsName, @FullpointsShow, @FullpointsTest)" SelectCommand="SELECT [VirName], [KutsName], [FullpointsShow], [FullpointsTest] FROM [Dog]" UpdateCommand="UPDATE [Dog] SET [KutsName] = @KutsName, [FullpointsShow] = @FullpointsShow, [FullpointsTest] = @FullpointsTest WHERE [VirName] = @VirName">
                        <DeleteParameters>
                            <asp:Parameter Name="VirName" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="VirName" Type="String" />
                            <asp:Parameter Name="KutsName" Type="String" />
                            <asp:Parameter Name="FullpointsShow" Type="Int32" />
                            <asp:Parameter Name="FullpointsTest" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="KutsName" Type="String" />
                            <asp:Parameter Name="FullpointsShow" Type="Int32" />
                            <asp:Parameter Name="FullpointsTest" Type="Int32" />
                            <asp:Parameter Name="VirName" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
<asp:Chart ID="ChartShow" runat="server" DataSourceID="SqlDataSource1" Palette="SeaGreen">
    <series>
        <asp:Series Name="Series1" XValueMember="VirName" YValueMembers="FullpointsShow">
        </asp:Series>
    </series>
    <chartareas>
        <asp:ChartArea Name="ChartArea1">
        </asp:ChartArea>
    </chartareas>
</asp:Chart>
                </td>
            </tr>
        </table>
        <br />
<br />
    </asp:Content>

