<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="Show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .auto-style1 {
            width: 100%;
        }
        .auto-style10 {
            height: 23px;
            width: 163px;
        }
        .auto-style2 {
            height: 23px;
        }
        .auto-style9 {
            height: 23px;
            width: 90px;
        }
        .auto-style11 {
            width: 113px;
        }
        .auto-style12 {
            width: 209px;
        }
        .auto-style20 {
            font-size: x-large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <span class="auto-style20"><strong>Lisää näyttelytulos:</strong></span><br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style10">Näyttelyn päivämäärä:</td>
                <td class="auto-style2" colspan="3">
        <asp:TextBox ID="TextBoxShowPVM" runat="server" Width="309px"></asp:TextBox>
                </td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="2">Näyttelypaikka:</td>
                <td class="auto-style2">
        <asp:TextBox ID="TextBoxShowPlace" runat="server" Width="308px"></asp:TextBox>
                </td>
                <td class="auto-style2" colspan="2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="2">Koira:</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="DropDownListShow_Dog" runat="server">
                        <asp:ListItem>Valitse</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style2" colspan="2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
        </table>
    
        <br />
        <asp:RadioButtonList ID="RadioButtonListShowType" runat="server" AutoPostBack="True">
            <asp:ListItem>KV / Kaikkien rotujen näyttely</asp:ListItem>
            <asp:ListItem>Ryhmänäyttely</asp:ListItem>
            <asp:ListItem>Erikoisnäyttely</asp:ListItem>
            <asp:ListItem>Värinäyttely</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <table class="auto-style1">
            <tr>
                <td colspan="2">Näyttelyn tulos:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style11" rowspan="6">
                    <asp:RadioButtonList ID="RadioButtonShowResults" runat="server" Width="134px" OnSelectedIndexChanged="RadioButtonShowResults_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem>ROP</asp:ListItem>
                        <asp:ListItem>VSP</asp:ListItem>
                        <asp:ListItem>PU/PN2</asp:ListItem>
                        <asp:ListItem>PU/PN3</asp:ListItem>
                        <asp:ListItem>PU/PN4</asp:ListItem>
                        <asp:ListItem>Muut tulokset:</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="auto-style12" rowspan="6">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="TextBoxMuuShow" runat="server"></asp:TextBox>
                </td>
                <td>Ryhmäkilpailu:</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownListRYP" runat="server" OnSelectedIndexChanged="DropDownListRYP_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem>Valitse</asp:ListItem>
                        <asp:ListItem>RYP1</asp:ListItem>
                        <asp:ListItem>RYP2</asp:ListItem>
                        <asp:ListItem>RYP3</asp:ListItem>
                        <asp:ListItem>RYP4</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Best In Show</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownListBIS" runat="server" AutoPostBack="True">
                        <asp:ListItem>Valitse</asp:ListItem>
                        <asp:ListItem>BIS1</asp:ListItem>
                        <asp:ListItem>BIS2</asp:ListItem>
                        <asp:ListItem>BIS3</asp:ListItem>
                        <asp:ListItem>BIS4</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">Ilmoitettujen koirien määrä:&nbsp;
                    <asp:TextBox ID="TextBoxLkmAll" runat="server"></asp:TextBox>
                    kpl</td>
            </tr>
            <tr>
                <td colspan="3">Ilmoitettujen saman sukupuolisten määrä:&nbsp;
                    <asp:TextBox ID="TextBoxLkmSex" runat="server"></asp:TextBox>
                    kpl</td>
            </tr>
        </table>
        <asp:Button ID="ButtonAddShow" runat="server" Text="Lisää" Width="84px" OnClick="ButtonAddShow_Click" />
        <br />
        <br />
    
        Lisätystä kokeesta ansaitut pisteet: <asp:TextBox ID="TextBoxAddShowResult" runat="server"></asp:TextBox>
        <br />
    
        </asp:Content>

