<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rechargelist.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.recharg.rechargelist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
    <link rel="stylesheet" type="text/css" href="/usermodule/static/js/ejs/skin/simple_gray/ymPrompt.css" />

    <script src="/usermodule/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="/usermodule/static/js/app/jquery.artDialog.js" type="text/javascript"></script>
    <script src="/usermodule/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>
    <script src="/usermodule/static/js/ejs/ymPrompt.js" type="text/javascript"></script>

    <script type="text/javascript" src="/usermodule/static/js/date.js"></script>

    <script type="text/javascript">
        function replenish(orderid) {
            $.get("/usermodule/Ajax/replenish_new.ashx?t=" + Math.random(), { type: "1", order: orderid },
        function(data, textStatus) {
            if (data == "true") {
                $.dialog({ title: lktitle, resize: false, content: '操作成功', ok: function() { }, close: function() { }, icon: 'succeed', width: '250px', height: '90px', time: 30000 });
            } else {
                $.dialog({ title: lktitle, resize: false, content: '操作失败', ok: function() { }, close: function() { }, icon: 'wrong', width: '250px', height: '90px', time: 30000 });
            }
        })
        }
        function ordermore(orderid) {
            ymPrompt.win('orderview.aspx?orderid=' + orderid, 600, 380, '订单详细信息', handler, null, null, { id: 'a' })
        }
        function handler(tp) {
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/recharge/index.aspx'">账户充值</a>
        &nbsp;&gt;&nbsp; <span>充值记录</span>
    </div>
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            充值记录</h2>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <!--工具栏-->
                        &nbsp;日期从:<input id="sdate" runat="server" type="text" class="search_txt_01" onfocus="HS_setDate(this)"
                            size="12" />
                        从:<input id="edate" runat="server" type="text" class="search_txt_01" onfocus="HS_setDate(this)"
                            size="12" />
                        &nbsp; 银行卡号:<input name="okey" type="text" id="okey" runat="server" maxlength="30"
                            value="" class="txt_01" />
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
                        充值方式
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        充值金额
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        到账金额
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        账户余额
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        充值时间
                    </th>
                </tr>
            </thead>
            <tbody role="alert" aria-live="polite" aria-relevant="all">
                <asp:Repeater ID="rptrecharges" runat="server" OnItemDataBound="rptDetails_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#GetPayTypeName(Eval("paytype"))%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("rechargeAmt", "{0:f2}")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("realPayAmt", "{0:f2}")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("Balance", "{0:f2}")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("processtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <!--列内容-->
                <%if (this.Pager1.RecordCount <= 0)
                  { %>
                <tr class="odd">
                    <td valign="top" colspan="5" class="dataTables_empty">
                        没有符合条件的记录
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
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
        </table>
    </div>
    <!--右部表单结束-->
    </form>
</body>
</html>
