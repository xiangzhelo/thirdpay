<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accountmoney.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.settlement.Accountmoney" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/account/index.aspx'">结算管理</a>
        &nbsp;&gt;&nbsp; <span>账户余额</span>
    </div>
    <input name="v$id" type="hidden" value="accountmoney" />
    <input name="v$fid" type="hidden" value="ruili" />
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>账户余额</h2>
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="6" align="left" >
                    成功订单的结算金额将自动累计到当前余额
                </td>
            </tr>
            <tr>
                <td height="39" align="right"  style="width: 15%;border: none; font-weight: bold;">
                    商户编号:
                </td>
                <td align="left"  style="padding-left: 15px; width: 35%">
                    <asp:Literal ID="litUserId" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left"  colspan="4">
                </td>
            </tr>
            <tr>
                <td height="39" align="right" style=";border: none; font-weight: bold;">
                    可提余额:
                </td>
                <td align="left"  style="padding-left: 15px;">
                     <span id="Span1" style="color:#ff9000;font-size:20px;"><asp:Literal ID="litBalance" runat="server"></asp:Literal></span>
                </td>
                <td height="39" align="left"  style="width: 15%" colspan="4">
                   
                </td>
            </tr>
            <tr id="tr_Unavailable" runat="server">
                <td height="39" align="right" style=";border: none; font-weight: bold;">
                    暂不可用余额:
                </td>
                <td align="left"  style="padding-left: 15px;">
                    <span style="color:deeppink;font-size:20px;"><asp:Literal ID="litUnavailable" runat="server"></asp:Literal></span>
                </td>
                <td height="39" align="left"  style="width: 15%" colspan="4">
                   
                </td>
            </tr>
            <tr>
                <td height="39" align="right" style=";border: none; font-weight: bold;">
                    提现处理:
                </td>
                <td align="left"  style="padding-left: 15px;">
                    <span style="color:#5895ad;font-size:20px;"><asp:Literal ID="litWithdrawingAmt" runat="server"></asp:Literal></span>
                </td>
                <td height="39" align="left"  colspan="2">
                </td>
            </tr>
            <tr>
                <td height="39" align="right" style=";border: none; font-weight: bold;">
                    冻结余额:
                </td>
                <td align="left"  style="padding-left: 15px;">
                    <span style="color:red;font-size:20px;"><asp:Literal ID="litFreezeAmt" runat="server"></asp:Literal></span>
                </td>
                <td height="39" align="left"  colspan="2">
                </td>
            </tr>
           
            
        </table>
        
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
            <tr>
                <td height="22" align="left" class="font8">
                    <td align="right">
                        &nbsp;
                    </td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
