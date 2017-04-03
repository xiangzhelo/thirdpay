<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bankreport.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.order.bankreport" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/agentmodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />

    <script type="text/javascript">
        function switchstate(url) { window.open(url, "", ""); }
    </script>

    <script src="/agentmodule/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="/agentmodule/static/js/app/jquery.artDialog.js" type="text/javascript"></script>

    <script src="/agentmodule/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>

    <script type="text/javascript" src="/agentmodule/static/js/date.js"></script>

    <script type="text/javascript">
        function replenish(orderid) {
            $.get("/agentmodule/ws/bankreplenish.ashx?t=" + Math.random(), { order: orderid },
        function(data, textStatus) {
            if (data == "true") {
                $.dialog({ title: lktitle, resize: false, content: '操作成功', ok: function() { }, close: function() { }, icon: 'succeed', width: '250px', height: '90px', time: 30000 });
            } else {
                $.dialog({ title: lktitle, resize: false, content: '操作失败', ok: function() { }, close: function() { }, icon: 'wrong', width: '250px', height: '90px', time: 30000 });
            }
        })
        }
    </script>
<style type="text/css">
    .rowhidden{display:block;width:120px;white-space:nowrap;overflow:hidden;float:left;-o-text-overflow: ellipsis;text-overflow:ellipsis; }

</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/agentmodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/agentmodule/order/index.aspx'">订单管理</a> &nbsp;&gt;&nbsp;
        <span>网银通知</span>
    </div>
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            网银订单通知结果</h2>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        订单状态:
                        <select name="Success" id="Success" runat="server" class="search_txt_01">
                            <option value="0">所有</option>
                            <option value="2" selected="selected">成功</option>
                            <option value="4">失败</option>
                            <option value="1">处理中</option>
                        </select>
                        &nbsp;其它:<select id="select_field" runat="server" class="search_txt_01">
                            <option value="1">商户订单号</option>
                        </select>
                        =
                        <input name="okey" type="text" id="okey" runat="server" maxlength="30" value="" class="txt_01" />
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
                        商户订单
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        接口版本
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        报告状态
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        消息
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        返回内容
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        发送次数
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        最后发送时间
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        操作
                    </th>
                </tr>
            </thead>
            <tbody role="alert" aria-live="polite" aria-relevant="all">
                <asp:Repeater ID="rptOrders" runat="server" OnItemDataBound="rptDetails_ItemDataBound">
                    <ItemTemplate>
                        <tr role="row">
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("userorder")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#viviapi.SysInterface.Bank.Utility.GetVersionName(Eval("version").ToString())%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#viviapi.BLL.Order.Bank.BankNotify.Instance.GetNotifyStatViewText(Eval("notifystat"))%> 
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("StatusDescription")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <span class="rowhidden" title="<%#Server.HtmlEncode(Eval("notifycontext").ToString())%>"> <%#Server.HtmlEncode(Eval("notifycontext").ToString())%></span>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("notifycount")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("notifytime","{0:yyyy-MM-dd HH:mm:ss}")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                 <a href="javascript:switchstate('<%# Eval("againnotifyurl")%>')">&laquo; 查看</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
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
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--按钮-->
                <td height="22" align="center" class="font8">
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
