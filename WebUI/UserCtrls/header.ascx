<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="viviapi.web.UserCtrls.header" %>
<div class="top">
    <div class="logo">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" height="75">
            <tr>
                <td width="32%">
                    <img src="/style/3/images/logo.jpg" />
                </td>
                <td width="49%">
                    &nbsp;
                </td>
                <td width="19%">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <a href="/index.aspx">登录</a>&nbsp;| &nbsp; <a href="/regedit.aspx">注册</a>&nbsp;|&nbsp;
                                <a href="#">
                                    商户帮助</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="menu">
        <table width="930" border="0" cellspacing="0" cellpadding="0" height="40" align="center">
            <tr id="menuTr">
                <td width="400" class="bh">
                    支付牌照号：Z2003151000010
                </td>
                <td onclick="change_bg(this)" <%=indexclass1%> width="120" align="center">
                    <a href="/index.aspx"><span <%=indexclass%>>&nbsp;&nbsp;首 页</span></a>
                </td>
                <td width="50" class="mo-dq">
                    <img src="/style/3/images/line.jpg" />
                </td>
                <td onclick="change_bg(this)" <%=productclass1%> width="120"  align="center">
                    <a href="/Product.aspx"><span <%=productclass%>>产品和服务器</span></a>
                </td>
                <td width="50" class="mo-dq">
                    <img src="/style/3/images/line.jpg" />
                </td>
                <td onclick="change_bg(this)" <%=solutionclass1%> width="120" class="mo-dq" align="center">
                    <a href="/solution.aspx"><span <%=solutionclass%>>行业解决方案</span></a>
                </td>
                <td width="50" class="mo-dq">
                    <img src="/style/3/images/line.jpg" />
                </td>
                <td onclick="change_bg(this)" <%=contactclass1%>width="120" class="mo-dq" align="center">
                    <a href="service.aspx" ><span <%=contactclass%>>商户接入方案</span></a>
                </td>
            </tr>
        </table>
    </div>
</div>
