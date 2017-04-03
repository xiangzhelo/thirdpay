﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="importlist.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.behalf.Importlist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
    <link rel="stylesheet" type="text/css" href="/usermodule/static/js/ejs/skin/vista/ymPrompt.css" />

    <script src="/usermodule/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="/usermodule/static/js/app/jquery.artDialog.js" type="text/javascript"></script>
    <script src="/usermodule/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>

    <script src="/usermodule/static/js/ejs/ymPrompt.js" type="text/javascript"></script>
    <script type="text/javascript" src="/usermodule/static/js/date.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/behalf/index.aspx'">对私代发</a>
        &nbsp;&gt;&nbsp; <span>上传记录</span>
    </div>
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            代发记录</h2>
    </div>
    <div id="search">
        <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
            <div id="msgdiv">
            </div>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="left">
                    <!--工具栏-->
                    &nbsp;日期从:<input id="sdate" runat="server" type="text" class="search_txt_01" onfocus="HS_setDate(this)"
                        size="12" />
                    从:<input id="edate" runat="server" type="text" class="search_txt_01" onfocus="HS_setDate(this)"
                        size="12" />
                    &nbsp;
                    <label>
                        &nbsp;
                        <asp:Button ID="b_search" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="b_search_Click" />
                    </label>
                </td>
            </tr>
        </table>
    </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
        class="font2">
        <!--列标题-->
        <tr>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                批次号
            </td>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                应代发条数
            </td>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                成功条数
            </td>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                应代发金额
            </td>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                成功金额
            </td>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                应付手续费
            </td>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                实付手续费
            </td>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                应付金额合计
            </td>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                实付金额合计
            </td>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                状态
            </td>
            <td height="34" align="center" background="/usermodule/static/style/09.jpg" bgcolor="#FFFFFF"
                class="list_title">
                操作
            </td>
        </tr>
        <asp:Repeater ID="rptrecharges" runat="server" OnItemDataBound="rptDetails_ItemDataBound"
            OnItemCommand="rptrecharges_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <a href="importitems.aspx?lotno=<%# Eval("lotno")%>">
                            <%# Eval("lotno")%></a>
                    </td>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <%# Eval("qty")%>
                    </td>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <%# Eval("succqty")%>
                    </td>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <%# Eval("amt","{0:f2}")%>
                    </td>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <%# Eval("succamt", "{0:f2}")%>
                    </td>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <%# Eval("fee", "{0:f2}")%>
                    </td>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <%# Eval("realfee", "{0:f2}")%>
                    </td>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <%# Eval("totalamt", "{0:f2}")%>
                    </td>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <%# Eval("totalsuccamt", "{0:f2}")%>
                    </td>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <%#_bll.GetAuditStatusText(Eval("audit_status"))%>
                    </td>
                    <td height="30" align="center" bgcolor="#FFFFFF">
                        <asp:Button ID="btnSure" runat="server" Text="提交审核" CommandArgument='<%# Eval("lotno")%>'
                            CommandName="sure" Visible="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="取消代发" CommandArgument='<%# Eval("lotno")%>'
                            CommandName="cancel" Visible="false" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <!--列内容-->
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
        <tr>
            <td height="10" colspan="3">
            </td>
        </tr>
        <tr>
            <!--按钮-->
            <td height="22" align="left" class="font8">
                <aspxc:AspNetPager ID="Pager1" CssClass="paginator" CurrentPageButtonClass="cpb"
                    CustomInfoHTML="共%PageCount%页/%RecordCount%条" runat="server" AlwaysShow="True"
                    FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Right"
                    ShowInputBox="Never" OnPageChanged="Pager1_PageChanged" CustomInfoTextAlign="Right"
                    LayoutType="Table" NumericButtonCount="5" CustomInfoSectionWidth="100px" Width="650px"
                    PagingButtonSpacing="0">
                </aspxc:AspNetPager>
            </td>
        </tr>
        <tr>
            <td height="10" colspan="3">
            </td>
        </tr>
    </table>
    </div>
    <!--右部表单结束-->
    </form>
</body>
</html>
