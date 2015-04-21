<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dog.aspx.cs" Inherits="Dog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .auto-style1 {
            width: 720px;
        }
        .auto-style5 {
            height: 23px;
            width: 104px;
        }
        .auto-style2 {
            height: 23px;
        }
        .auto-style9 {
            height: 23px;
            width: 90px;
        }
        .auto-style20 {
            font-size: x-large;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <span class="auto-style20"><strong>Koira:</strong></span><br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style5">Virallinen nimi:</td>
                <td class="auto-style2" colspan="3">
        <asp:TextBox ID="TextBoxVirName" runat="server" Width="309px"></asp:TextBox>
                </td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="2">Kutsumanimi:</td>
                <td class="auto-style2">
        <asp:TextBox ID="TextBoxKutsName" runat="server" Width="308px"></asp:TextBox>
                </td>
                <td class="auto-style2" colspan="2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="2">&nbsp;</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="DropDownListSukup" runat="server">
                        <asp:ListItem>narttu</asp:ListItem>
                        <asp:ListItem>uros</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style2" colspan="2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
        </table>
        <br />
    
                    <asp:Button ID="ButtonAddDog" runat="server" Text="Lisää koira" OnClick="ButtonAddDog_Click" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ButtonEditDog" runat="server" Text="Muokkaa" OnClick="ButtonEditDog_Click" />
    
                    <br />
    
                    <br />
                    <asp:ListBox ID="ListBoxDogs" runat="server" Height="298px" Width="412px" AutoPostBack="True" OnSelectedIndexChanged="ListBoxDogs_SelectedIndexChanged"></asp:ListBox>
                    <br />
        </asp:Content>

