<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .auto-style1 {
            width: 100%;
        }
        .auto-style10 {
            height: 23px;
            width: 132px;
        }
        .auto-style2 {
            height: 23px;
        }
        .auto-style9 {
            height: 23px;
            width: 90px;
        }
        .auto-style11 {
            width: 868px;
        }
        .auto-style17 {
            width: 173px;
        }
        .auto-style18 {
            width: 57px;
        }
        .auto-style16 {
            width: 174px;
        }
        .auto-style20 {
            font-size: x-large;
        }
        .auto-style21 {
            width: 165px;
        }
        .auto-style22 {
            width: 278px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <span class="auto-style20"><strong>Lisää koetulos:</strong></span><table class="auto-style1">
            <tr>
                <td class="auto-style10">Kokeen päivämäärä:</td>
                <td class="auto-style2" colspan="3">
        <asp:TextBox ID="TextBoxTestPVM" runat="server" Width="309px"></asp:TextBox>
                </td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="2">Koepaikka:</td>
                <td class="auto-style2">
        <asp:TextBox ID="TextBoxTestPlace" runat="server" Width="308px"></asp:TextBox>
                </td>
                <td class="auto-style2" colspan="2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="2">Koira:</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="DropDownListTest_Dog" runat="server">
                        <asp:ListItem>Valitse</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style2" colspan="2">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style9">&nbsp;</td>
            </tr>
        </table>
        <br />
        <table class="auto-style11">
            <tr>
                <td class="auto-style17">Laji:</td>
                <td class="auto-style17">Luokka:</td>
                <td class="auto-style17">Tulos:</td>
                <td colspan="2">Sijoitus:</td>
            </tr>
            <tr>
                <td class="auto-style17">
                    <asp:RadioButtonList ID="RadioButtonListLaji" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonListLaji_SelectedIndexChanged">
                        <asp:ListItem>TOKO</asp:ListItem>
                        <asp:ListItem>Rally-Toko</asp:ListItem>
                        <asp:ListItem>MEJÄ</asp:ListItem>
                        <asp:ListItem>BH</asp:ListItem>
                        <asp:ListItem>Rotu-Toko</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                </td>
                <td class="auto-style17">
                    <asp:RadioButtonList ID="RadioButtonListLuokka" runat="server" AutoPostBack="True">
                        <asp:ListItem>Mölli</asp:ListItem>
                        <asp:ListItem>ALO</asp:ListItem>
                        <asp:ListItem>AVO</asp:ListItem>
                        <asp:ListItem>VOI</asp:ListItem>
                        <asp:ListItem>EVL / MES</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                </td>
                <td class="auto-style17">
                    <asp:RadioButtonList ID="RadioButtonListTulos" runat="server" AutoPostBack="True">
                        <asp:ListItem>I - tulos / hyväksytty</asp:ListItem>
                        <asp:ListItem>II - tulos</asp:ListItem>
                        <asp:ListItem>III - tulos</asp:ListItem>
                        <asp:ListItem>hylätty</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    <br />
                </td>
                <td class="auto-style18">
                    <asp:RadioButtonList ID="RadioButtonListSijoitus" runat="server" AutoPostBack="True">
                        <asp:ListItem>1.</asp:ListItem>
                        <asp:ListItem>2.</asp:ListItem>
                        <asp:ListItem>3.</asp:ListItem>
                        <asp:ListItem>4.</asp:ListItem>
                        <asp:ListItem>5.</asp:ListItem>
                        <asp:ListItem>Muu:</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="auto-style16">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="TextBoxMuuSijoitus" runat="server" Width="95px"></asp:TextBox>
                </td>
            </tr>
        </table>
        Pisteet:
        <asp:TextBox ID="TextBoxKoePisteet" runat="server"></asp:TextBox>
        <br />
        <asp:CheckBox ID="CheckBoxRT_TP" runat="server" Text="Rally-Toko tuomaripalkinto" />
        <br />
        <asp:CheckBox ID="CheckBoxValio" runat="server" Text="Valioituminen" />
        <br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style21">Koulutustunnus:</td>
                <td class="auto-style22">Luokanvaihto:</td>
                <td>Arvokilpailu:</td>
            </tr>
            <tr>
                <td class="auto-style21">
                    <asp:DropDownList ID="DropDownListKoulari" runat="server">
                        <asp:ListItem>Valitse</asp:ListItem>
                        <asp:ListItem>TK2 / RTK2</asp:ListItem>
                        <asp:ListItem>TK1 / RTK1</asp:ListItem>
                        <asp:ListItem>TK3 / RTK3</asp:ListItem>
                        <asp:ListItem>TK4 / RTK4</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style22">
                    <asp:DropDownList ID="DropDownListLuokanVaihto" runat="server">
                        <asp:ListItem>Valitse</asp:ListItem>
                        <asp:ListItem>Luokanvaihto ALO -&gt; AVO</asp:ListItem>
                        <asp:ListItem>Luokanvaihto AVO -&gt; VOI</asp:ListItem>
                        <asp:ListItem>Luokanvaihto VOI -&gt; EVL / MES</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListArvokilpailu" runat="server">
                        <asp:ListItem>Valitse</asp:ListItem>
                        <asp:ListItem>Piirimestaruus</asp:ListItem>
                        <asp:ListItem>SM</asp:ListItem>
                        <asp:ListItem>PM</asp:ListItem>
                        <asp:ListItem>EM</asp:ListItem>
                        <asp:ListItem>MM</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:Button ID="ButtonAddTest" runat="server" Text="Lisää" Width="84px" OnClick="ButtonAddTest_Click" />
        <br />
        <br />
    
        Lisätystä kokeesta ansaitut pisteet:
        <asp:TextBox ID="TextBoxAddTestResult" runat="server"></asp:TextBox>
    
        <br />
    </asp:Content>

