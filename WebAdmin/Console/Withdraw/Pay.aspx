<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviAPI.WebAdmin.Console.Withdraw.Pay" Codebehind="Pay.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>后台管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
 

    <script type="text/javascript" language="javascript">
function Setchkall(obj){
var objs = document.getElementsByName("chk");
for(i=0;i<objs.length;i++){
objs[i].checked=obj.checked;
}
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfaction" runat="server" />
        <div>
            <table border="0" cellspacing="1" cellpadding="1" style="width:100%">
                <tr>
                    <td align="center" class="title">
                        付款操作</td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="border-right: #c9ddf0 1px solid; border-top: #c9ddf0 1px solid; border-left: #c9ddf0 1px solid;
                            border-bottom: #c9ddf0 1px solid" cellspacing="0" cellpadding="0" width="100%"
                            bgcolor="#f3f9fe" border="0">
                            <tr>
                                <td class="tdTit_h" style="padding-left: 10px">
                                    用户信息
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="table1" style="margin-bottom: 10px" cellspacing="1" cellpadding="1" width="99%"
                                        align="center" bgcolor="#c9ddf0">
                                        <tbody>
                                            <tr bgcolor="F6F6F6">
                                                <td colspan="1" height="40" valign="middle" style="width: 150px" align="center">
                                                    &nbsp;用户ID</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;用户名</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;真实姓名</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;用户状态</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr bgcolor="ffffff">
                                                <td align="center" colspan="1" height="40" style="width: 150px" valign="middle">
                                                    <asp:Label ID="lblUserId" runat="server"></asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    <asp:Label ID="lblUserName" runat="server"></asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    <asp:Label ID="lblFullName" runat="server"></asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    <asp:Label ID="lblUserStatus" runat="server"></asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                   &nbsp; </td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                   &nbsp; </td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                   &nbsp; </td>
                                            </tr>
                                            <tr bgcolor="F6F6F6">
                                                <td colspan="1" height="40" valign="middle" style="width: 150px" align="center">
                                                    &nbsp;可用余额</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;总余额</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;押扣金额</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;提现处理中</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;冻结余额</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr bgcolor="ffffff">
                                                <td align="center" colspan="1" height="40" style="width: 150px" valign="middle">
                                                    &nbsp;<asp:Label ID="lblBalance" runat="server" ForeColor="red">0</asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;<asp:Label ID="lblTotalBalance" runat="server">0</asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;<asp:Label ID="lblDeposit" runat="server">0</asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;<asp:Label ID="lblWithdrawingAmt" runat="server">0</asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;<asp:Label ID="lblFreezeAmt" runat="server">0</asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr bgcolor="F6F6F6">
                                                 <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;账号类型</td>
                                                <td colspan="1" height="40" valign="middle" style="width: 150px" align="center">
                                                    &nbsp;收款人</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;收款银行</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;开户地址</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;银行账号</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;</td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr bgcolor="ffffff">
                                                <td align="center" colspan="1" height="40" style="width: 150px" valign="middle">
                                                    <asp:Label ID="lblStlAcctAccoutType" runat="server"></asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    <asp:Label ID="lblStlAcctPayee" runat="server"></asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    <asp:Label ID="lblStlAcctBank" runat="server"></asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    <asp:Label ID="lblSetAcctBankAddress" runat="server"></asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    <asp:Label ID="lblStlAcctBankAccout" runat="server"></asp:Label></td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    </td>
                                                <td colspan="1" height="40" valign="middle" align="center">
                                                    </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="border-right: #c9ddf0 1px solid; border-top: #c9ddf0 1px solid; margin-top: 10px;
                            border-left: #c9ddf0 1px solid; border-bottom: #c9ddf0 1px solid" cellspacing="0"
                            cellpadding="0" width="100%" bgcolor="#f3f9fe" border="0">
                            <tr>
                                <td class="tdTit_h" style="padding-left: 10px" width="16%">
                                    申请提现</td>
                                <td width="84%">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table id="table_zyads" style="margin-bottom: 10px" cellspacing="1" cellpadding="1"
                                        width="99%" align="center" bgcolor="#c9ddf0">
                                        <tbody>
                                            <tr>
                                                <td bgcolor="#ffffff">
                                                    <table id="searchandSoftSumReport" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td align="right" colspan="1" height="40" style="width: 150px" valign="middle">
                                                                    处理号：</td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <asp:Label ID="lblTranno" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="1" height="40" style="width: 150px" valign="middle">
                                                                    申请时间：</td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <asp:Label ID="lblAddTime" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="1" height="40" style="width: 150px" valign="middle">
                                                                    收款人：</td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <asp:Label ID="lblPayeeName" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="1" height="40" style="width: 150px" valign="middle">
                                                                    收款银行：</td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <asp:Label ID="lblBank" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="1" height="40" style="width: 150px" valign="middle">
                                                                    开户地址：</td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <asp:Label ID="lblPayeeaddress" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="1" height="40" style="width: 150px" valign="middle">
                                                                    银行账号：</td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <asp:Label ID="lblAccount" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="1" height="40" valign="middle" style="width: 150px" align="right">
                                                                    提现金额：
                                                                </td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <span style="font-weight: bolder; font-size: 18px; color: #ff830a; float: left;">
                                                                        <asp:Label ID="lblsettleAmt" runat="server"></asp:Label></span> <span style="float: right;">
                                                                        </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="1" height="40" style="width: 150px" valign="middle">
                                                                    扣 &nbsp;&nbsp; 税：</td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <asp:TextBox ID="TaxBox" runat="server">0</asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="1" height="40" style="width: 150px" valign="middle">
                                                                    手 续 费：</td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <asp:TextBox ID="ChargesBox" runat="server">3</asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="1" height="40" valign="middle" style="width: 150px" align="right">
                                                                </td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <asp:Button ID="btnSave" runat="server" Text="保存资料" CssClass="button" onclick="btnSave_Click" />
                                                                    <asp:Button ID="btnSure" runat="server" Text="确认支付" CssClass="button" 
                                                                        onclick="btnSure_Click" />                                                                  
                                                                    <input type="button" id="btnreturn" class="button" value="返回列表" onclick="javascript:window.location.href='Pays.aspx'" />
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="1" height="40" style="width: 150px" valign="middle">
                                                                </td>
                                                                <td colspan="6" height="40" valign="middle">
                                                                    <asp:Label ID="errLabel" runat="server" ForeColor="Red"></asp:Label></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<script type="text/javascript" language="javascript">
var table=document.getElementById("searchandSoftSumReport");
if (table){
for(i=0;i<table.rows.length;i++){
if(i%2==0){
table.rows[i].bgColor="ffffff";
}else{table.rows[i].bgColor="F6F6F6"}
}
}
</script>

