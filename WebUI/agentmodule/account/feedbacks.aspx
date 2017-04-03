<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedbacks.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.account.Feedbacks" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
    <style type="text/css">
        #tinybox
        {
            position: absolute;
            display: none;
            padding: 10px;
            background: #ffffff url(../image/preload.gif) no-repeat 50% 50%;
            border: 10px solid #e3e3e3;
            z-index: 2000;
        }
        #tinymask
        {
            position: absolute;
            display: none;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            background: #000000;
            z-index: 1500;
        }
        #tinycontent
        {
            background: #ffffff;
            font-size: 1.1em;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="/usermodule/static/js/ejs/skin/vista/ymPrompt.css" />

    <script src="/usermodule/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="/usermodule/static/js/app/jquery.artDialog.js" type="text/javascript"></script>

    <script src="/usermodule/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>

    <script src="/usermodule/static/js/ejs/ymPrompt.js" type="text/javascript"></script>

    <script type="text/javascript" src="/usermodule/static/js/date.js"></script>

    <script type="text/javascript">

        function view(id) {
            ymPrompt.win('feedbackview.aspx?id=' + id, 600, 380, '反馈查看', handler, null, null, { id: 'a' })
        }
        function infeedback() {
            ymPrompt.win('feedback.aspx', 600, 420, '留言反馈', handler, null, null, { id: 'a' })
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
                onclick="parent.location.href='/usermodule/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>留言反馈</span>
    </div>
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            留言反馈
        </h2>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                    <a href="javascript:void(0);" onclick="infeedback()" class="btn btn-primary" style="color: White;">
                        添加反馈</a>
                </div>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            class="table table-bordered table-striped centered dataTable" aria-describedby="dataTable_info">
            <!--列标题-->
            <thead>
                <tr role="row">
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        类型
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        问题或建议
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        时间
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        管理员回复
                    </th>
                </tr>
            </thead>
            <tbody role="alert" aria-live="polite" aria-relevant="all">
                <asp:Repeater ID="rptfeedback" runat="server" OnItemDataBound="rptfeedback_ItemDataBound">
                    <ItemTemplate>
                        <tr role="row">
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Enum.GetName(typeof(viviapi.Model.feedbacktype), Eval("typeid"))%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <a href="javascript:view('<%#Eval("id")%>');">
                                    <%# Eval("title")%></a>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("addtime","{0:yyyy-MM-dd HH:ss:mm}")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <asp:Literal ID="litdetail" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <!--列内容-->
                <%if (this.Pager1.RecordCount <= 0)
                  { %>
                <tr class="odd">
                    <td valign="top" colspan="4" class="dataTables_empty">
                        没有符合条件的记录
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <!--按钮-->
                <td height="35" align="center" style="text-align: center;">
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
