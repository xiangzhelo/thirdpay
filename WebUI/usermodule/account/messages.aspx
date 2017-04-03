<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="messages.aspx.cs" Inherits="viviAPI.WebUI7uka.usermodule.account.Messages" %>

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

    <script type="text/javascript">

        function viewmsg(msgid) {
            ymPrompt.win('msgview.aspx?id=' + msgid, 600, 380, '消息详细信息', handler, null, null, { id: 'a' })

        }
        function handler(tp) {
            window.location.reload();
        }
        function SelectAll(box) {
            for (var i = 0; i < document.form1.elements.length; i++) {
                var e = document.form1.elements[i];
                if ((e.type == 'checkbox')) {
                    var o = e.name.lastIndexOf('ckbIndex');
                    if (o != -1) {
                        e.checked = box.checked;
                    }
                }
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <%--<a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>站内消息</span>--%>
    </div>
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            站内消息</h2>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <!--工具栏-->
                        <asp:Button ID="btnRpt" runat="server" OnClick="btnRpt_Click" Text="批量删除" OnClientClick="return confirm('确定要删除选择的消息吗？')"
                            CssClass="btn btn-primary" />
                        &nbsp;&nbsp;&nbsp;&nbsp;日期从:<input id="sdate" runat="server" type="text" class="search_txt_01"
                            onfocus="HS_setDate(this)" size="12" width="90" />
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
            id="dataTable" class="table table-bordered table-striped centered dataTable"
            aria-describedby="dataTable_info">
            <!--列标题-->
            <thead>
                <tr role="row">
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        <input id="chkHeader" type="checkbox" onclick="SelectAll(this)" />
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1">
                        标题
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1">
                        时间
                    </th>
                </tr>
            </thead>
            <tbody role="alert" aria-live="polite" aria-relevant="all">
                <asp:Repeater ID="msg_data" runat="server">
                    <ItemTemplate>
                        <tr role="row">
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:CheckBox runat="server" ID="ckbIndex" /><asp:Label ID="lbmsid" runat="server"
                                    Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <a href="javascript:viewmsg('<%#Eval("id")%>');">
                                    <%# GetMsgTit(Eval("msgtitle").ToString(),Convert.ToBoolean(Eval("isRead")))%></a>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("addtime", "{0:yyyy-MM-dd}")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <!--列内容-->
                <!--合计-->
                <%if (this.Pager1.RecordCount <= 0)
                  { %>
                <tr class="odd">
                    <td valign="top" colspan="3" class="dataTables_empty">
                        没有符合条件的记录
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <aspxc:AspNetPager ID="Pager1" CssClass="paginator" CurrentPageButtonClass="cpb"
                        CustomInfoHTML="共%PageCount%页/%RecordCount%条" runat="server" AlwaysShow="True"
                        FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Right"
                        ShowInputBox="Never" OnPageChanged="Pager1_PageChanged" CustomInfoTextAlign="Right"
                        LayoutType="Table" NumericButtonCount="5" CustomInfoSectionWidth="100px" Width="650px"
                        PagingButtonSpacing="0">
                    </aspxc:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    <!--右部表单结束-->
    </form>
</body>
</html>
