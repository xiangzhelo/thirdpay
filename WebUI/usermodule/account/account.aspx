<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="account.aspx.cs" Inherits="viviAPI.WebUI7uka.usermodule.account.Account2" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的账户 － 账户首页 - 亿付商务网 -中国互联网在线收款服务商</title>
    <link rel="stylesheet" href="../css/panel.css" />
    <link rel="stylesheet" type="text/css" href="../css/datatable.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="panel-wrapper">
        <div id="panel-head">
            <h2>
                您好，亿付商务网支付</h2>
            <div class="panel-notify">
            </div>
        </div>
        <div id="panel-content">
            <div id="columnA" class="column">
                <div class="portlet account-portlet">
                    <div class="module">
                        <div class="portlet-header">
                            <h3>账户信息</h3>
                        </div>
                        <div class="portlet-content">
                            <div class="account-info">
                                <h4>
                                    下午好，<%=UserName%> &nbsp;&nbsp;
                            </h4>
                                <ul class="horizonal">
                                    <li><asp:Literal ID="litUserEmail"
                            runat="server"></asp:Literal>  
                            <a id="a_verify" runat="server" href="/usermodule/safety/DoEmailAuthen.aspx" class="font12 weight-n"><font class="txtr">[立即认证]</font></a> 
                            </li>
                                    <li>会员编号：<strong class="red"><%=UserId%></strong> </li>
                                </ul>
                                <p class="login-info">
                                    上次登录时间：<span id="lblgetlastm" runat="server"></span>&nbsp;&nbsp;&nbsp;于&nbsp;<span id="location" runat="server"></span>
                                </p>
                            </div>
                            <div class="balance-info">
                                <h4>
                                    可用余额</h4>
                                <ul class="horizonal">
                                    <li>
                                        <p class="num"><span id="lblgetmoney" class="red" runat="server">0</span> 元</p>
                                         <asp:Literal ID="litOtherAmt" runat="server"></asp:Literal>
                                        </li>
                                    <li><a href="" onclick="parent.location.href='/usermodule/recharg/index.aspx'" id="refresh" class="btn btn-primary">充值</a> 
                                        <a href="" onclick="parent.location.href='/usermodule/settlement/index.aspx'" id="cash" class="btn">提现</a> </li>
                                </ul>
                            </div>
                            
                            <div class="balance-info" id="Unavailable"  runat="server">
                                <h4>
                                    未结算余额</h4>
                                <ul class="horizonal">
                                    <li>
                                        <p class="num"><span id="litUnavailable1" class="red" runat="server">0</span> 元</p>
                                        </li>
                                </ul>
                            </div>
                            <div class="balance-info">
                                <h4>
                                    提现处理</h4>
                                <ul class="horizonal">
                                    <li>
                                        <p class="num"><span id="litWithdrawingAmt1" class="red" runat="server">0</span> 元</p>
                                        </li>
                                </ul>
                            </div>
                            
                            <div class="balance-info">
                                <h4>
                                    冻结余额</h4>
                                <ul class="horizonal">
                                    <li>
                                        <p class="num"><span id="litFreezeAmt1" class="red" runat="server">0</span> 元</p>
                                        </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="portlet notifycation-portlet">
                    <div class="module">
                        <div class="portlet-header">
                            <h3>系统通知</h3>
                        </div>
                        <div class="portlet-content">
                            <h4 id="title" class="red">
                                <asp:Literal ID="lit_news_title" runat="server"></asp:Literal></h4>
                            <p id="createtime">
                                <asp:Literal ID="lit_news_publish_time" runat="server"></asp:Literal></p>
                            <p id="content">
                                <asp:Literal ID="lit_news_content" runat="server"></asp:Literal>
                                </p>
                        </div>
                    </div>
                </div>
            </div>
            <div id="columnB" class="column columnRight">
                <div class="portlet business-portlet">
                    <div class="module">
                        <div class="portlet-header">
                            <h3>今日统计</h3>
                        </div>
                        <div id="stat" class="portlet-content">
                            <table class="table table-striped centered">
                                <thead>
                                    <tr>
                                        <th>
                                            网银笔数
                                        </th>
                                        <th>
                                            网银收益（元）
                                        </th>
                                        <th>
                                            点卡笔数
                                        </th>
                                        <th>
                                            点卡收益（元）
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <span id="ordercount" runat="server" class="num">0</span> 笔
                                        </td>
                                        <td>
                                            <span id="totalmoney" runat="server" class="num success">0</span> 元
                                        </td>
                                        <td>
                                            <span id="succordercount" runat="server" class="num warning">0</span> 笔
                                        </td>
                                        <td>
                                            <span id="succtotalmoney" runat="server" class="num red">0</span> 元
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="portlet records-portlet">
                    <div class="module">
                        <div class="portlet-header">
                            <h3>当日交易统计</h3>
                        </div>
                        <div class="portlet-content">
                            <div id="dataTable_wrapper" class="dataTables_wrapper" role="grid" style="text-align: center;">
                                <table id="dataTable" class="table table-bordered table-striped centered dataTable"
                                    aria-describedby="dataTable_info">
                                    <thead>
                                        <tr role="row">
                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                                colspan="1">
                                                通道类型
                                            </th>
                                            <th class="sorting_desc" role="columnheader" tabindex="0" aria-controls="dataTable"
                                                rowspan="1">
                                                笔数
                                            </th>
                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                                colspan="1">
                                                交易金额
                                            </th>
                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                                colspan="1">
                                                收益
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody role="alert" aria-live="polite" aria-relevant="all">
                                        <asp:Repeater ID="rptdata" runat="server">
                                            <ItemTemplate>
                                                <tr role="row">
                                                    <td>
                                                        <%# Eval("modetypename")%>
                                                    </td>
                                                    <td>
                                                         <%# Eval("ordercount")%> 笔
                                                    </td>
                                                    <td>
                                                        <%# Eval("totalorderamt", "{0:f2}")%> 元
                                                    </td>
                                                    <td>
                                                        <%# Eval("totalpayamt", "{0:f2}")%> 元
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <%if (this.rptdata.Items.Count > 0)
                                          { %>
                                        
                                        <%}
                                          else
                                          { %>
                                        <tr class="odd">
                                            <td valign="top" colspan="6" class="dataTables_empty">
                                                没有符合条件的记录
                                            </td>
                                        </tr>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
