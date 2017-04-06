<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quotaorder.aspx.cs" Inherits="viviAPI.WebUI2015.usermodule.quota.quotaorder" %>

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
        function ordermore(orderid) {
            ymPrompt.win('orderview.aspx?orderid=' + orderid, 600, 380, '订单详细信息', handler, null, null, { id: 'dataDetail' });
        }
        function handler(tp) {
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            额度转换记录</h2>
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
                        <label>
                            &nbsp;类型:
                        <select id="quota_type" runat="server" class="search_txt_01">
                            <option value="0">所有类型</option>
                            <option value="1">AG额度</option>
                            <option value="2">BBIN额度</option>
                            <option value="3">MG额度</option>
                            <option value="4">OG额度</option>
                            <option value="5">HG额度</option>
							<option value="6">PT额度</option>
							<option value="7">EBET额度</option>
                        </select>
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
                        订单号
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        转换额度
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        转换类型
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        转换费率(百分之)
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        手续费
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        提交时间
                    </th>
                   <!-- <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        操作
                    </th>-->
                </tr>
            </thead>
            <tbody role="alert" aria-live="polite" aria-relevant="all">
                <asp:Repeater ID="rptOrders" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("orderid")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("quotaValue","{0:f2}")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# quotaType[int.Parse(Eval("quota_type").ToString())]%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# toPercent(Eval("payrate", "{0:f2}"))%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("charge","{0:f2}")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("addtime")%>
                            </td>
                            <%-- <td height="30" align="center" bgcolor="#FFFFFF">
                                <a href="javascript:ordermore('<%#Eval("orderid")%>');">&laquo; 查看</a>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <!--列内容-->
                <%if (this.Pager1.RecordCount <= 0)
                  { %>
                <tr class="odd">
                    <td valign="top" colspan="7" class="dataTables_empty">
                        没有符合条件的记录
                    </td>
                </tr>
                <%} %>
                <!--合计-->
                <tr>
                    <td height="30" align="center" bgcolor="#FFFFFF" colspan="12">
                        本页提交订单：<%=pageordertotal%>笔 
                        │总提交订单：<%=totalordertotal%>笔
                        │本页消费金额：<%=pagerefervalue%>元
                        │ 总消费金额 ：<%=totalrefervalue%>元 
                    </td>
                </tr>
            </tbody>
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