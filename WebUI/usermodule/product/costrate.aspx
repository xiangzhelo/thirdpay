<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costrate.aspx.cs" Inherits="viviAPI.WebUI7uka.usermodule.product.Costrate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
    <script type="text/javascript">
        function switchstate(stype, sid, channelid) {
            location.href = "?flag=" + stype + "&sid=" + sid + "&id=" + channelid;
            return false;
        }
    </script>
    <style>
    .button
    {
        display:inline-block;
        width:40px;
        height:30px;
        background-color:White;
        }
        .green
        {
            background-color:orange;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            结算费率</h2>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            id="dataTable" class="table table-bordered table-striped centered dataTable"
            aria-describedby="dataTable_info">
            <!--列标题-->
            <thead>
                <tr role="row">
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 5%; text-align: center;">
                        序号
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        大类ID
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        类型名称
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        结算费率
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        操作
                    </th>
                </tr>
            </thead>
            <tbody role="alert" aria-live="polite" aria-relevant="all">
                <!--列内容-->
                <asp:Repeater ID="rpt_paymode" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Container.ItemIndex +1%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("code")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("modetypename")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("payrate","{0:0.00}")%>%
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%--<asp:Button ID="btnOpen" runat="server" Text="启用" />
                                <asp:Button ID="btnClose" runat="server" Text="关闭" />--%>
                                <button class="button <%# Eval("usermodestatus")=="wrong"?"green":""%>"  <%# Eval("usermodestatus")=="wrong"?"":"disabled=\"disabled\""%>
                                    type="button" onclick="javascipt:switchstate('on','<%=CurrentUser.ID%>','<%#Eval("typeId")%>')">
                                    启用</button>
                                <button class="button <%# Eval("usermodestatus")=="right"?"green":""%>" type="button" <%# Eval("usermodestatus")=="right"?"":"disabled=\"disabled\""%> onclick="javascipt:switchstate('off','<%=CurrentUser.ID%>','<%#Eval("typeId")%>')"
                                    style="margin-right: 0">
                                    关闭</button>
                            </td>
                            <td style="display: none;">
                                <%#Eval("typeId")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <!--右部表单结束-->
    </form>
</body>
</html>
