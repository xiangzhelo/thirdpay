<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.order.CardDetail" Codebehind="CardDetail.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />

    <script src="../js/common.js" type="text/javascript"></script>

    
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" class="title">
                卡类订单信息查看
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2b">
                订单编号：
            </td>
            <td class="td1b">
                <asp:Label ID="lblid" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                系统订单号 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblorderid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                接口版本：
            </td>
            <td class="td1b">
                <asp:Label ID="lblversion" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                总卡张数：
            </td>
            <td class="td1b">
                <asp:Label ID="lblcardNum" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                卡号：
            </td>
            <td class="td1b">
                <asp:Label ID="lblcardno" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                密码 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblcardpwd" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                订单类别 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblordertype" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                用户信息 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lbluserid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                通道类型 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lbltypeId" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                卡信息 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblpaymodeId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr >
            <td class="td2b">
                商户订单号 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lbluserorder" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                用户提交金额 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblrefervalue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="display:none">
            <td class="td2b">
                下行异步通知地址 ：
            </td>
            <td class="td1b">
                
            </td>
            <td class="td2b">
                下行异步通知地址 ：
            </td>
            <td class="td1b">
                
            </td>
        </tr>
        <tr>
            <td class="td2b">
                异步通知总次数 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblnotifycount" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                通知状态:
            </td>
            <td class="td1b">
                <asp:Label ID="lblnotifystat" runat="server"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td class="td2b">
                异步返回内容 ：
            </td>
            <td class="td1b" colspan="4">
                <asp:Label ID="lblnotifycontext" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                备注消息 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblattach" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                支付者IP ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblpayerip" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                传送IP ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblclientip" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                新增时间 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lbladdtime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>            
            <td class="td2b">
                通道厂商 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblsupplierId" runat="server"></asp:Label>
            </td>
             <td class="td2b">
                通道商订单号 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblsupplierOrder" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>           
            <td class="td2b">
                实际面值：
            </td>
            <td class="td1b">
                <asp:Label ID="lblcardfaceval" runat="server"></asp:Label>
            </td>
             <td class="td2b">
                结算金额：
            </td>
            <td class="td1b">
                <asp:Label ID="lblrealvalue" runat="server"></asp:Label> 
            </td>
        </tr>
        <tr>           
            <td class="td2b">
                订单状态：
            </td>
            <td class="td1b">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label><asp:Label ID="lblsmsg" runat="server"></asp:Label>                
            </td>
             <td class="td2b">
                扣卡规则：
            </td>
            <td class="td1b">
                    <asp:Label ID="lblwithhold" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                <asp:Label ID="lblmakeup" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>           
            <td class="td2b">
                商家费率：
            </td>
            <td class="td1b">
                <asp:Label ID="lblpayRate" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                商家金额 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblpayAmt" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">
                 平台费率 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblsupplierRate" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                平台金额 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblsupplierAmt" runat="server"></asp:Label>
            </td>           
        </tr>
        <tr>
             <td class="td2b">
                代理费率 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblpromRate" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                代理金额 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblpromAmt" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>            
            <td class="td2b">
                平台利润 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblprofits" runat="server"></asp:Label>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="td2b">
                提交的服务器 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblserver" runat="server"></asp:Label>
            </td>
            <td class="td2b">
                完成时间 ：
            </td>
            <td class="td1b">
                <asp:Label ID="lblcompletetime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2b">提交地址 ：
            </td>
            <td colspan="4" class="td1b">
                <asp:Label ID="lblreferUrl" runat="server"></asp:Label>
        </tr>
        <tr>
            <td class="td2b">异步通知 ：
            </td>
            <td colspan="4" class="td1b">
                <asp:Label ID="lblnotifyurl" runat="server"></asp:Label>
        </tr>
        <tr>
            <td class="td2b">同步返回 ：
            </td>
            <td colspan="4" class="td1b">
                <asp:Label ID="lblreturnurl" runat="server"></asp:Label>
        </tr>
        <tr>
            <td class="td2b">返回参数 ：
            </td>
            <td colspan="4" class="td1b">
                <asp:Label ID="litreback" runat="server"></asp:Label> </td>
        </tr>
        <tr>
            <td class="td2b">异步通知 ：
            </td>
            <td colspan="4" class="td1b" style="word-wrap:break-word;overflow:hidden; ">
                <asp:Literal ID="litNotify" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
                <div align="center">
                <br />                    
                    <input type="button" value="关 闭" onclick="javascript:window.close()" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
