<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quotatype.aspx.cs" Inherits="viviAPI.WebAdmin.Console.quota.quotatype" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/ControlDate/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css"> table {font-weight: normal;font-size: 12px; line-height: 170%;}
        td{ height: 11px; }
        A:link {color: #02418a;text-decoration: none;}
    </style>
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
        function changeDefaultPayrate(payrate,quotaType) {
            location.href = "quotasetting.aspx?type=defaultpayrate&payrate=" + payrate + "&quotaType=" + quotaType;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            额度类型</h2>
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
                <tr  height="22" style="background-color: #507CD1; color: #fff">
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
                        默认费率(%)
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
                        <tr bgcolor="#EFF3FB">
                            <td style="text-align:center;">
                                <%#Container.ItemIndex +1%>
                            </td>
                            <td>
                                <%#Eval("quota_type")%>
                            </td>
                            <td>
                                <%#Eval("name")%>
                            </td>
                            <td>
                                <input type="text" value="<%#toPercent(Eval("default_payrate").ToString())%>" onchange="changeDefaultPayrate($(this).val(),<%#Eval("quota_type")%>);" />
                            </td>
                            <td style="text-align:center;">
                                <button class="button <%# Eval("isopen").ToString()=="0"?"green":""%>"  <%# Eval("isopen").ToString()=="0"?"":"disabled=\"disabled\""%>
                                    type="button" onclick="location.href='quotasetting.aspx?type=quotaTypesetting&isopen=1&quotaType=<%#Eval("quota_type")%>'">
                                    启用</button>
                                <button class="button <%# Eval("isopen").ToString()=="1"?"green":""%>" type="button" <%# Eval("isopen").ToString()=="1"?"":"disabled=\"disabled\""%> onclick="location.href='quotasetting.aspx?type=quotaTypesetting&isopen=0&quotaType=<%#Eval("quota_type")%>'" 
                                    style="margin-right: 0">
                                    关闭</button>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr bgcolor="#ffffff">
                            <td style="text-align:center;">
                                <%#Container.ItemIndex +1%>
                            </td>
                            <td>
                                <%#Eval("quota_type")%>
                            </td>
                            <td>
                                <%#Eval("name")%>
                            </td>
                            <td>
                                <input type="text" value="<%#toPercent(Eval("default_payrate").ToString())%>" onchange="changeDefaultPayrate($(this).val(),<%#Eval("quota_type")%>);" />
                            </td>
                            <td style="text-align:center;">
                                <button class="button <%# Eval("isopen").ToString()=="0"?"green":""%>"  <%# Eval("isopen").ToString()=="0"?"":"disabled=\"disabled\""%>
                                    type="button" onclick="location.href='quotasetting.aspx?type=quotaTypesetting&isopen=1&quotaType=<%#Eval("quota_type")%>'">
                                    启用</button>
                                <button class="button <%# Eval("isopen").ToString()=="1"?"green":""%>" type="button" <%# Eval("isopen").ToString()=="1"?"":"disabled=\"disabled\""%> onclick="location.href='quotasetting.aspx?type=quotaTypesetting&isopen=0&quotaType=<%#Eval("quota_type")%>'" 
                                    style="margin-right: 0">
                                    关闭</button>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <!--右部表单结束-->
    </form>
</body>
</html>

