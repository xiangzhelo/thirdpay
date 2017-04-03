<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.supplier.SupplierEdit" Codebehind="Edit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建编辑供应商</title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
     <link href="../style/edit.css" type="text/css" rel="stylesheet" />
    

    <script type="text/javascript">
function backreturn(){
    location.href='List.aspx';
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="td2">
                        供应商编号：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtcode" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        供应商名称：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtname" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        网址地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpurl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        Logo图片：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtlogourl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        支持种类：</td>
                    <td class="td1">
                        <asp:CheckBox ID="chkisbank" Text="在线" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkiscard" Text="卡类" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkissms" Text="短信" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkissx" Text="声讯" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkisdistribution" Text="代付" runat="server" Checked="True" />
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        多账号：</td>
                    <td class="td1">
                        <asp:RadioButtonList ID="rblmultiacct" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">多账号</asp:ListItem>
                            <asp:ListItem Value="0">单账号</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        账号：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        密钥：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey" runat="server" Width="50%" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        用户名称：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpusername" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        账号1：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid1" runat="server" Width="50%"></asp:TextBox>
                        快钱 神州行账号
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        密钥1：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey1" runat="server" Width="50%" TextMode="MultiLine"></asp:TextBox>
                        快钱 神州行密钥
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="td2">
                        账号2：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid2" runat="server" Width="50%"></asp:TextBox>
                          快钱 联通充值卡账号
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="td2">
                        密钥2：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey2" runat="server" Width="50%"></asp:TextBox>
                          快钱 联通充值卡账号
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="td2">
                        账号3：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid3" runat="server" Width="50%"></asp:TextBox>
                        快钱 电信账号
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="td2">
                        密钥3：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey3" runat="server" Width="50%"></asp:TextBox>
                        快钱  电信密钥
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="td2">
                        账号4：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid4" runat="server" Width="50%"></asp:TextBox> 快钱 骏网一卡通账号
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="td2">
                        密钥4：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey4" runat="server" Width="50%"></asp:TextBox> 快钱 骏网一卡通密钥
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="td2">
                        账号5：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid5" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="td2">
                        密钥5：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey5" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        网址返回地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpbakurl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        卡类返回地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtCardbakUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        域名跳转：</td>
                    <td class="td1">
                        <asp:RadioButtonList ID="rdobtn1" runat="server" RepeatDirection="Horizontal" 
                            RepeatLayout="Flow">
                            <asp:ListItem Value="0" Selected="True">未启用</asp:ListItem>
                            <asp:ListItem Value="1">已启用</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        跳转地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtJumpUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        网银提交地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpostBankUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        卡类提交地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpostCardUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="td2">
                        卡类查询地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtQueryCardUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        短信息提交地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpostSMSUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        代付款提交地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtdistributionUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>  

                  <tr>

                    <td class="td2">
                        限制金额：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtlimitAmount" runat="server" Width="50%"></asp:TextBox>(元)一次性最多能充值多少
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        说明：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtdesc" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        排序：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtsort" runat="server" Width="50%">0</asp:TextBox>
                    </td>
                </tr>
                <tr style="display:none">
                    <td class="td2">
                        销卡超时：</td>
                    <td class="td1">
                        <asp:TextBox ID="txttimeout" runat="server" Width="50%"></asp:TextBox> 单位为毫秒
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        同步返回码：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtSynsRetCode" runat="server" Width="50%" TextMode="MultiLine"></asp:TextBox> 需要切换通道的返回码用逗号隔开
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        异步通知返回码：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtAsynsRetCode" runat="server" Width="50%" TextMode="MultiLine"></asp:TextBox> 需要切换通道的返回码用逗号隔开
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                       日志：</td>
                    <td class="td1">
                        <asp:CheckBox ID="chkSynsSummitLog" Text="同步日志" runat="server" Checked="False" />
                        <asp:CheckBox ID="chkAsynsRetLog" Text="异步通知日志" runat="server" Checked="False" />
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        启用状态：</td>
                    <td class="td1">
                        <asp:RadioButtonList ID="rblused" runat="server" RepeatDirection="Horizontal" 
                            RepeatLayout="Flow">
                            <asp:ListItem Value="0" Selected="True">未启用</asp:ListItem>
                            <asp:ListItem Value="1">已启用</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; height: 30px;">
                        <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click">
                        </asp:Button>
                        <input type="button" value="返 回" onclick="backreturn()" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
