<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quotapayrate.aspx.cs" Inherits="viviAPI.WebUI2015.usermodule.quota.quotapayrate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
    <script src="../js/jquery-1.8.1.min.js"></script>
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
    <script type="text/javascript">
        function switchstate(isopen, id, quota_type) {
            var selfisopen;
            if (isopen == "on") {
                selfisopen = 1;
            } else if (isopen == "off") {
                selfisopen = 0;
            }
            $.ajax({
                url: "/usermodule/quota/GetQuotaInfo.ashx",
                async:"false",
                type:"POST",
                dataType:"html",
                data: { get: "quotatype_selfisopen", selfisopen: selfisopen, id: id, quota_type: quota_type },
                success: function(json) {
                    if(json=="success")
                        window.location.reload();
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            额度转换费率</h2>
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
                        转换费率
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
                                <%#Eval("quota_type")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("name")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#toPercent(Eval("payrate").ToString())%>%
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <button class="button <%# Eval("selfisopen").ToString()=="0"?"green":""%>"  <%# Eval("selfisopen").ToString()=="0"?"":"disabled=\"disabled\""%>
                                    type="button" onclick="javascipt:switchstate('on','<%=CurrentUser.ID%>','<%#Eval("quota_type")%>')">
                                    启用</button>
                                <button class="button <%# Eval("selfisopen").ToString()=="1"?"green":""%>" type="button" <%# Eval("selfisopen").ToString()=="1"?"":"disabled=\"disabled\""%> onclick="javascipt:switchstate('off','<%=CurrentUser.ID%>','<%#Eval("quota_type")%>')"
                                    style="margin-right: 0">
                                    关闭</button>
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
