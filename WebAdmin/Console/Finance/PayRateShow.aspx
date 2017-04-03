<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.Finance.PayRateShow" Codebehind="PayRateShow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>费率编辑</title>
     <link href="../style/admin.css" type="text/css" rel="stylesheet" />
     <link href="../style/page.css" type="text/css" rel="stylesheet" />

    <script src="../js/common.js" type="text/javascript"></script>
    <script type="text/javascript">    
function backreturn(){
    location.href = 'PayRate.aspx';
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellspacing="1" cellpadding="0">
            <tr>
                <td align="center" colspan="3" class="title" >
                    费率查看(单位%)</td>
            </tr>
        </table>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>                
                <td class="td2a">
                    费率类型
                </td>
                <td class="td1a">
                    <asp:Label ID="lblType" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="td2a">
                    名称：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtlevName" runat="server" Width="120px"></asp:Label>
                </td>
                <td class="td2a">
                     
                </td>
                <td class="td1a">
                  
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="td2a">
                    财付通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp100" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    支付宝：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp101" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    网上银行：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp102" runat="server" Width="80px"></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="td2a">
                    神州行充值卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp103" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    神州行浙江卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp200" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    神州行江苏卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp201" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    神州行辽宁卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp202" runat="server" Width="80px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td2a">
                    神州行福建卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp203" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    盛大一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp104" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    征途支付卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp105" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    骏网一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp106" runat="server" Width="80px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td2a">
                    腾讯Q币卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp107" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    联通充值卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp108" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    久游一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp109" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    网易一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp110" runat="server" Width="80px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td2a">
                    完美一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp111" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    搜狐一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp112" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    电信充值卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp113" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    声讯卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp114" runat="server" Width="80px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td2a">
                    光宇一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp115" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    金山一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp116" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    纵游一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp117" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    天下一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp118" runat="server" Width="80px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td2a">
                    天宏一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp119" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    魔兽卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp204" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    联华卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp205" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    短信：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp300" runat="server" Width="80px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td2a">
                    天下一卡通专项：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp209" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    殴飞一卡通：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp208" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    盛付通卡：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp210" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                    支付宝扫码
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp206" runat="server" Width="80px"></asp:Label>
                </td>
            </tr>
              <tr>
                <td class="td2a">
                    微信支付：
                </td>
                <td class="td1a">
                    <asp:Label ID="txtp207" runat="server" Width="80px"></asp:Label>
                </td>
                <td class="td2a">
                  
                </td>
                <td class="td1a">
                    
                </td>
                <td class="td2a">
                   
                </td>
                <td class="td1a">
                  
                </td>
                <td class="td2a">
                    
                </td>
                <td class="td1a">
                    
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; height: 30px;">
                    <input type="button" value="返 回" onclick="backreturn()" />
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
