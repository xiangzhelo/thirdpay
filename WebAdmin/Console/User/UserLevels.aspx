<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.User.UserLevels" Codebehind="UserLevels.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>费率调整</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
table {FONT-WEIGHT: normal; FONT-SIZE:12px;LINE-HEIGHT: 170%;}
td{height:11px;}
A:link {COLOR: #02418a; TEXT-DECORATION: none}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="modelPanel" style="background: #F2F2F2">
        </div>
        <table width="100%" border="0" cellspacing="1" cellpadding="0">
            <tr>
                <td align="center" colspan="3" class="title" >
                    商户等级 
                    <input id="btnAdd" type="button" value="新 增" onclick="location.href='LevelEdit.aspx'" class="button"/></td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td align="center">
                        <table width="99%" border="0" cellpadding="1" cellspacing="1" bgcolor="#cccccc" id="table2">
                            <tbody bgcolor="#cccccc">
                                <asp:Repeater ID="repData" runat="server" onitemcommand="repData_ItemCommand">
                                    <HeaderTemplate>
                                        <tr style="height: 22px; background: #507CD1; color: #fff; font-size: 10px;">
                                            <td>
                                                序号</td>
                                            <td>
                                                等级</td>
                                            <td>
                                                名称</td>
                                            <td>
                                                操作</td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#ffffff">
                                            <td>
                                                <%# Eval("id")%>
                                            </td>
                                            <td>
                                                <%# Eval("level")%></td>
                                            <td>
                                                <%# Eval("levName")%></td>
                                            <td>
                                                <a href="LevelEdit.aspx?id=<%# Eval("id") %>" class='ljbg'>编辑</a>
                                                <a href="../finance/payrateedit.aspx?billid=<%# Eval("id") %>&type=1" class='ljbg'>费率</a>
                                                <asp:LinkButton ID="bDel" runat="server" CommandName="del" CommandArgument='<%# Eval("id") %>'>删除</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#F0F6FC">
                                            <td>
                                                <%# Eval("id")%>
                                            </td>
                                            <td>
                                                <%# Eval("level")%></td>
                                            <td>
                                                <%# Eval("levName")%></td>
                                            <td>
                                                <a href="LevelEdit.aspx?id=<%# Eval("id") %>" class='ljbg'>编辑</a>
                                                <a href="../finance/payrateedit.aspx?billid=<%# Eval("id") %>&type=1" class='ljbg'>费率</a>
                                                <asp:LinkButton ID="bDel" runat="server" CommandName="del" CommandArgument='<%# Eval("id") %>'>删除</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                                
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>

        <script type="text/javascript">
 function handler(tp){
 }
        </script>

    </form>

    <script type="text/javascript" language="JavaScript">var table=document.getElementById("table_zyads");if (table){for(i=0;i<table.rows.length;i++){if(i%2==0){table.rows[i].bgColor="ffffff";}else{table.rows[i].bgColor="E6EEFF"}}}var mytr =  document.getElementById("table2").getElementsByTagName("tr");for(var i=1;i<mytr.length;i++){mytr[i].onmouseover= function(){ var rows = this.childNodes.length;for(var row=0;row<rows;row++){this.childNodes[row].style.backgroundColor='#E6EEFF';}};mytr[i].onmouseout= function(){var rows = this.childNodes.length;for(var row=0;row<rows;row++){this.childNodes[row].style.backgroundColor='';}};}</script>

</body>
</html>
