<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.Withdraw.Pays" Codebehind="Pays.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "Width=800px;Height=350px;");
        }
    </script>
    <script type="text/javascript" language="javascript">
        function Setchkall(obj) {
            var objs = document.getElementsByName("TranNoList");
            for (i = 0; i < objs.length; i++) {
                objs[i].checked = obj.checked;
            }
        }
        function checkall(obj) {
            var check = document.getElementsByName("TranNoList");
            for (i = 0; i < check.length; i++) {
                check[i].checked = obj.checked;
            }
        }
    </script>
    <style type="text/css">
        table
        {
            font-weight: normal;
            font-size: 12px;
            line-height: 170%;
            font-family: Arial;
        }
        td
        {
            height: 11px;
        }
        A:link
        {
            color: #02418a;
            text-decoration: none;
        }
    </style>
    
    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "Width=800px;Height=350px;");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="center" class="title">
                    付款管理
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                    商户ID：<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                    处理号：<asp:TextBox ID="txtTranno" runat="server" Width="80px"></asp:TextBox>
                    <asp:DropDownList ID="ddlbankName" runat="server">                        
                        <asp:ListItem value="">--收款银行--</asp:ListItem>
                        <asp:ListItem value="0002">支付宝</asp:ListItem>
                        <asp:ListItem value="0003">财付通</asp:ListItem>
                        <asp:ListItem value="1002">中国工商银行</asp:ListItem>
                        <asp:ListItem value="1005">中国农业银行</asp:ListItem>
                        <asp:ListItem value="1003">中国建设银行</asp:ListItem>
                        <asp:ListItem value="1026">中国银行</asp:ListItem>
                        <asp:ListItem value="1001">招商银行</asp:ListItem>
                        <asp:ListItem value="1006">民生银行</asp:ListItem>
                        <asp:ListItem value="1020">交通银行</asp:ListItem>
                        <asp:ListItem value="1025">华夏银行</asp:ListItem>
                        <asp:ListItem value="1009">兴业银行</asp:ListItem>
                        <asp:ListItem value="1027">广发银行</asp:ListItem>
                        <asp:ListItem value="1004">浦发银行</asp:ListItem>
                        <asp:ListItem value="1022">光大银行</asp:ListItem>
                        <asp:ListItem value="1021">中信银行</asp:ListItem>
                        <asp:ListItem value="1010">平安银行</asp:ListItem>
                        <asp:ListItem value="1066">中国邮政储蓄银行</asp:ListItem>
                    </asp:DropDownList>
                    收款账户：<asp:TextBox ID="txtAccount" runat="server" Width="80px"></asp:TextBox>
                    收款人：<asp:TextBox ID="txtpayeeName" runat="server" Width="80px"></asp:TextBox>                    
                    <asp:DropDownList ID="ddlmode" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                        </asp:Button>
                         <asp:RadioButtonList ID="rbl_export_mode" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="1">excel</asp:ListItem>
                    <asp:ListItem Value="2">txt</asp:ListItem>
                </asp:RadioButtonList>
                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="导出"
                            OnClick="btnExport_Click"></asp:Button>
                            
                             二级密码：<asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>         
                <asp:Button ID="btnAllSettle" runat="server" CssClass="button" Text="批量支付" 
                    onclick="btnAllSettle_Click" OnClientClick="return checkAll();">
                </asp:Button>
                </td>
            </tr>
            <tr>
                <td bgcolor="#F9F9F9">
                    <span style="color: #ff0000; text-align: left">总申请金额：<%=TotalAmount%></span>
                    <span style="color: #ff0000; text-align: left">总手续费：<%=TotalCharges%></span>
                    <span style="color: #ff0000; text-align: left">总应付金额：<%=TotalWithdrawAmt%></span>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="tab">
                        <asp:Repeater ID="rptdata" runat="server">
                            <HeaderTemplate>
                                <tr height="22" style="background-color: #507CD1; color: #fff">
                                    <td style="width: 3%; text-align: center">
                                        <input id="Checkboxall" type="checkbox" onclick="checkall(this)" />
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        处理号
                                    </td>
                                    <td style="width: 8%; text-align: center">
                                        商户名
                                    </td>
                                    <td style="width: 25%">
                                        收款信息
                                    </td>                                    
                                    <td style="width: 6%; text-align: center">
                                        申请金额
                                    </td>
                                    <td style="width: 5%; text-align: center">
                                        手 续 费
                                    </td>
                                    <td style="width: 6%; text-align: center">
                                        实际应付
                                    </td>
                                    <td style="width: 8%; text-align: center">
                                        提现方式
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        申请时间
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        操作
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td style="text-align:center">
                                        <input id="<%# Eval("tranno") %>" type="checkbox" name="TranNoList" value="<%# Eval("tranno") %>" />
                                    </td>
                                    <td style="text-align:center">
                                       <%# Eval("tranno")%>
                                    </td>
                                    <td style="text-align:center">
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                           <%#Eval("UserName")%>(#<%#Eval("userid")%>)
                                        </a>
                                    </td>
                                    <td>
                                        <%# Eval("PayeeBank")%> <%# Eval("bankProvince")%> <%# Eval("bankCity")%> <%# Eval("Payeeaddress")%> <br />
                                         &nbsp;<%# Eval("payeeName")%> <br />
                                         &nbsp;<%# Eval("account")%>
                                    </td>                                   
                                    <td style="text-align:right">
                                        <%# Eval("amount", "{0:f2}")%>
                                    </td>
                                    <td style="text-align:right">
                                        <%# Eval("Charges", "{0:f2}")%>
                                    </td>
                                    <td style="text-align:right">
                                       <%# Eval("withdraw", "{0:f2}")%>
                                    </td>
                                    <td style="text-align:center">
                                        <%#viviapi.BLL.Finance.Withdraw.Instance.GetModeName(Convert.ToInt32(Eval("settmode")))%>
                                    </td>
                                    <td style="text-align:center">
                                        <%# Eval("addTime","{0:yyyy-MM-dd HH:mm:ss}") %>
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        <a href="Pay.aspx?action=modi&tranno=<%# Eval("tranno") %>">修改</a>
                                        <a href="Pay.aspx?action=pay&tranno=<%# Eval("tranno") %>">进行支付</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="text-align:center">
                                        &nbsp;
                                    </td>
                                    <td style="text-align:center">
                                        &nbsp;
                                    </td>
                                    <td style="text-align:right">
                                        &nbsp;合计
                                    </td>                                   
                                    <td style="text-align:right">
                                        <%=PageTotalAmount%>
                                    </td>
                                    <td style="text-align:right">
                                        <%=PageTotalCharges%>
                                    </td>
                                    <td style="text-align:right">
                                        <%=PageTotalWithdrawAmt%>
                                    </td>
                                    <td style="text-align:center">
                                    </td>
                                    <td style="text-align:center">
                                    </td>
                                    <td style="width: 10%; text-align: center">
                                        
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    </td>
            </tr>
            <tr>
                <td colspan="20">
                    <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanging="Pager1_PageChanging"
                        AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount% 总页数：%PageCount% 当前页：%CurrentPageIndex%"
                        CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                        NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                        PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                        ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                        TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px">
                    </aspxc:AspNetPager>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </table>
    </div>
    </form>
     <script src="../js/public.js" type="text/javascript"></script>
</body>
</html>
