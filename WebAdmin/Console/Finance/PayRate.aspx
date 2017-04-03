<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.Finance.PayRate" Codebehind="PayRate.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>

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
                    费率列表(单位%)</td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td align="center">
                <table width="100%" id="Table1" border="0" align="center" cellpadding="2" cellspacing="1">
                    <tr>
                        <td>
                            费率类型：
                            <asp:DropDownList ID="ddlused" runat="server">
                    <asp:ListItem Value="">--请选择--</asp:ListItem>
                    <asp:ListItem Value="1" Selected="true">商家等级</asp:ListItem>
                    <asp:ListItem Value="2">独立商家</asp:ListItem>
                    <asp:ListItem Value="3">接口商</asp:ListItem>
                </asp:DropDownList>
                            ID：<asp:TextBox ID="txtBillId" runat="server"></asp:TextBox>
                            名称：<asp:TextBox ID="txtBillName" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click"></asp:Button>
                        </td>
                       <%-- <td><asp:Button ID="btnAdd" runat="server" CssClass="button" Text=" 新 增 " OnClick="btnAdd_Click"></asp:Button></td>  --%>                     
                    </tr>                    
                </table>
            </td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="99%" border="0" cellpadding="1" cellspacing="1" bgcolor="#cccccc" id="tab">
                            <tbody bgcolor="#cccccc">
                                <asp:Repeater ID="rptdata" runat="server">
                                    <HeaderTemplate>
                                        <tr style="height: 22px; background: #507CD1; color: #fff; font-size: 10px;">
                                            <td>
                                                类型</td>
                                            <td>
                                                名称</td> 
                                            <td>
                                                网银</td> 
                                            
                                            <td>
                                                支付宝</td>   <td>
                                                微信</td> 
                                             <td>
                                                财付通</td>
                                                   <td>
                                                QQ支付</td>
                                            <td>
                                                神州行</td>
                                             <td>
                                                联通卡</td>
                                             <td>
                                                电信卡</td>

                                          <%--  <td>
                                                浙江</td>
                                            <td>
                                                江苏</td>
                                            <td>
                                                辽宁</td>
                                             <td>
                                                福建</td>--%>
                                            <td>
                                                盛大卡</td> <td>
                                                骏网卡</td> <td>
                                                Q币卡</td>
                                           <%-- <td>
                                                征途卡</td>--%>
                                           
                                           
                                           
                                            <td>
                                                久游卡</td>
                                            <td>
                                                网易卡</td>
                                            <td>
                                                完美卡</td>
                                            <td>
                                                搜狐卡</td>
                                           
                                        <%--    <td>
                                                光宇卡</td>
                                            <td>
                                                金山</td>--%>
                                            <td>
                                                纵游</td>
                                          <%--  <td>
                                                天下</td>--%>
                                            <td>
                                                天宏</td>  
                                             <td>
                                                征途卡</td>
                                            <td>
                                                天下</td>       <td>
                                                wap微信支付</td>     <td>
                                                wap支付宝</td>                                       
                                           <%-- <td>
                                                魔兽</td>
                                            <td>
                                                联华</td>
                                            <td>
                                                短信</td>
                                            <td>
                                                声讯</td>--%>
                                            <td>
                                                操作</td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#ffffff">
                                            <td>
                                                <%#viviapi.BLL.Finance.PayRate.Instance.GetRateTypeName(Eval("rateType"))%>
                                            </td>
                                            <td>
                                                <%# Eval("billame")%>(#<%# Eval("billId")%>)
                                            </td>
                                         
                                            
                                            <td>
                                                <%# Convert.ToDouble(Eval("p102"))*100%>
                                                </td>  <td>
                                                <%# Convert.ToDouble(Eval("p101"))*100%>
                                                </td> 
                                                <td>
                                                <%# Convert.ToDouble(Eval("p207"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p100"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p203")) * 100%><%--浙江变成qq支付--%>
                                                </td>

                                            <td>
                                                <%# Convert.ToDouble(Eval("p103"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p108")) * 100%><%--联通--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113")) * 100%><%--电信--%>
                                                </td>
                                             <td>
                                                <%# Convert.ToDouble(Eval("p104")) * 100%><%--盛大--%>
                                                </td>
                                              
                                            <td>
                                                <%# Convert.ToDouble(Eval("p106"))*100%><%-- 骏网--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p107"))*100%><%-- Q币--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p109"))*100%><%-- 109--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p110"))*100%><%-- 网易--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p111"))*100%><%-- 完美--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p112"))*100%><%-- 搜狐--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p117"))*100%><%-- 纵游--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p119"))*100%><%-- 天宏--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p105"))*100%><%-- 征途--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113"))*100%><%-- 天下--%>
                                                </td>  <td>
                                                <%# Convert.ToDouble(Eval("p204"))*100%><%-- wap微信--%>
                                                </td>
                                          <td>
                                                <%# Convert.ToDouble(Eval("p200"))*100%><%-- wap支付宝--%>
                                                </td>
                                            <td>
                                                <a href="PayRateEdit.aspx?billid=<%# Eval("billid") %>&type=<%# Eval("rateType") %>" class='ljbg'>编辑</a></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                       <%--<tr bgcolor="#F0F6FC">
                                             <td>
                                                <%#viviapi.BLL.Finance.PayRate.Instance.GetRateTypeName(Eval("rateType"))%>
                                            </td>
                                            <td>
                                                <%# Eval("billame")%>(#<%# Eval("billId")%>)
                                            </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p100"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p101"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p102"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p103"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p200")) * 100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p201")) * 100%>
                                                </td>
                                             <td>
                                                <%# Convert.ToDouble(Eval("p202")) * 100%>
                                                </td>
                                              <td>
                                                <%# Convert.ToDouble(Eval("p203")) * 100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p104"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p105"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p106"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p107"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p108"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p109"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p110"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p111"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p112"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p115"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p116"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p117"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p118"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p119"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p118"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p204")) * 100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p205")) * 100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p114"))*100%>
                                                </td>
                                            <td>
                                                <a href="PayRateEdit.aspx?billid=<%# Eval("billid") %>&type=<%# Eval("rateType") %>" class='ljbg'>编辑</a></td>
                                        </tr>--%>


                                        <tr bgcolor="#F0F6FC">
                                            <td>
                                                <%#viviapi.BLL.Finance.PayRate.Instance.GetRateTypeName(Eval("rateType"))%>
                                            </td>
                                            <td>
                                                <%# Eval("billame")%>(#<%# Eval("billId")%>)
                                            </td>
                                         
                                            
                                            <td>
                                                <%# Convert.ToDouble(Eval("p102"))*100%>
                                                </td>  
                                            
                                            <td>
                                                <%# Convert.ToDouble(Eval("p101"))*100%>
                                                </td> 
                                                <td>
                                                <%# Convert.ToDouble(Eval("p107"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p100"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p203")) * 100%><%--浙江变成qq支付--%>
                                                </td>

                                            <td>
                                                <%# Convert.ToDouble(Eval("p103"))*100%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p108")) * 100%><%--联通--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113")) * 100%><%--电信--%>
                                                </td>
                                             <td>
                                                <%# Convert.ToDouble(Eval("p104")) * 100%><%--盛大--%>
                                                </td>
                                              
                                            <td>
                                                <%# Convert.ToDouble(Eval("p106"))*100%><%-- 骏网--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p107"))*100%><%-- Q币--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p109"))*100%><%-- 109--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p110"))*100%><%-- 网易--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p111"))*100%><%-- 完美--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p112"))*100%><%-- 搜狐--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p117"))*100%><%-- 纵游--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p119"))*100%><%-- 天宏--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p105"))*100%><%-- 征途--%>
                                                </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113"))*100%><%-- 天下--%>
                                                </td>
                                         <td>
                                                <%# Convert.ToDouble(Eval("p204"))*100%><%-- wap微信--%>
                                                </td>
                                          <td>
                                                <%# Convert.ToDouble(Eval("p200"))*100%><%-- wap支付宝--%>
                                                </td>
                                            <td>
                                                <a href="PayRateEdit.aspx?billid=<%# Eval("billid") %>&type=<%# Eval("rateType") %>" class='ljbg'>编辑</a></td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                                
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #EFEFEF">
                        <td style="height: 16px;">
                            <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanged="Pager1_PageChanged"
                                AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount% 总页数：%PageCount% 当前页：%CurrentPageIndex%"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                                PageSize="8" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px">
                            </aspxc:AspNetPager>
                        </td>
                    </tr>
                </table>
                    </td>
                </tr>
            </tbody>
        </table>

        

    </form>

    <script src="../js/public.js" type="text/javascript"></script>
</body>
</html>
