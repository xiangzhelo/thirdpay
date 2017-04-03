<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="safeques.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.safety.Safeques" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/page.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>设置密保</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title" style="color: Black;">
            <h2>
                设置密保</h2>
        </div>
        <br />
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01" style="color: Black; border: none;">
                    您还未设置密码保护！请立即设置以便以后在找回密码等时使用。<br />
                    问题或答案最多可输入15个字。请保证您的密码问题不出现泄密，以保障您的隐私。<br />
                    如果设置答案的时候有带符号、空格等的，验证时也都必须输入完整<br />
                    示例问题：我最喜欢说的一句话？
                    <br />
                    示例答案：你赢了<br />
                </td>
            </tr>
            <tr id="p_oldans" runat="server">
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;
                    color: Black;">
                    旧问题:
                </td>
                <td align="center" class="line_01" style="padding-left: 15px; border: none; width: 240px;">
                    <input id="txtoldques" runat="server" type="text" class="txt_02" size="40" readonly="readonly" />
                </td>
                <td height="45" align="left" class="line_01" style="border: none;">
                </td>
            </tr>
            <tr id="p_oldques" runat="server">
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;
                    color: Black;">
                    旧答案:
                </td>
                <td align="center" class="line_01" style="padding-left: 15px; border: none; width: 240px;">
                    <input id="txtoldans" runat="server" type="text" class="txt_02" size="40" />
                </td>
                <td height="45" align="left" class="line_01" style="border: none;">
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;
                    color: Black;">
                    新问题:
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none; width: 240px;">
                    <input id="txtnewques" runat="server" type="text" class="txt_01" size="25" value="" />
                </td>
                <td height="45" align="left" class="line_01" style="border: none;">
                    
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;
                    color: Black;">
                    新答案:
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none; width: 240px;">
                    <input id="txtnewans" runat="server" type="text" class="txt_01" size="25" value="" />
                </td>
                <td height="45" align="left" class="line_01" style="border: none;">
                    
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;
                    color: Black;">
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none; width: 240px;">
                     <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn btn-primary" onclick="lbtnSave_Click">确认提交</asp:LinkButton>&nbsp;
                     <a href="/usermodule/account/safety.aspx" target="mainframe" class="btn btn-primary">
                        取消</a>
                </td>
                <td height="45" align="left" class="line_01">
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
