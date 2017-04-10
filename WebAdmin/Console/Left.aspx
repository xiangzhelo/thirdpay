<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.web.Manage.Left" Codebehind="Left.aspx.cs" %>

<html xmlns="">
<head>
    <title>导航</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" href="style/left1a.css" type="text/css" />
    <link rel="stylesheet" href="style/left1b.css" type="text/css" />
    <style type="text/css">
.left_color { text-align:left; }
.left_color a {text-indent:40px; background: url(style/images/item.gif) 18px 1px no-repeat;color: #083772; text-decoration: none; font-size:12px; display:block !important; width:150px !important; width:150px; height:22px; line-height:22px;}
.left_color a:hover { color: #1075bd; width:149px;background:#d6e3ef url(style/images/item.gif) 18px 1px no-repeat; height:22px;line-height:22px;}
img { float:none; vertical-align:middle; }
#on { background:#fff url("images/menubg_on.gif") right no-repeat; color:#f20; font-weight:bold; }
hr { width:90%; text-align:left; size:0; height:0px; border-top:1px solid #46A0C8;}
</style>

    <script type="text/javascript">
	function disp(n){
	    try {
	        for (var i = 0; i < 9; i++) {
	            //if (!document.getElementById("left"+i)) return;			
	            document.getElementById("left" + i).style.display = "none";
	        }
	        document.getElementById("left" + n).style.display = "block";
	    } catch(e) {
	        alert(e.message);
	    }
	}

	
	  
    function ShowMenu(strValue){
	    document.getElementById("left1").style.display="block";
    }
    </script>

</head>
<body style="margin-top: 0px;">
    <div class="columncontent" style="margin: 0px;">
        <table width="150" border="0" cellpadding="0" cellspacing="0">
            <tr class="tdbg">
                <td valign="top" class="left_color" id="menubar">
                    <div id="left0" style="display: ">
                        <div class="lefttab">日常使用</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="Order/banklist.aspx?status=2">网银订单</a>
                        <a target="rightframe" href="Order/cardlist.aspx?status=2">点卡订单</a>
                        <%--<a target="rightframe" href="DayCount.aspx">综合统计</a>--%>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=1">商户列表</a>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=0">商户审核</a>
                        <a target="rightframe" href="News/NewsEdit.aspx">新闻发布</a>
                        <a target="rightframe" href="Withdraw/Audits.aspx">结算审核</a>
                    </div>
                    <div id="left1" style="display: none;">
                        <div class="lefttab">常规设置</div>
                        <div style="padding-top: 10px"></div>                    
                        <a target="rightframe" href="News/NewsList.aspx" > 新闻管理</a>
                        <a target="rightframe" href="News/NewsEdit.aspx" >新闻发布</a>                                             
                        <a target="rightframe" href="SiteInfo.aspx">站点设置</a> 
                        <a target="rightframe" href="Sys/RegisterSettings.aspx">注册设置</a> 
                        <a target="rightframe" href="Sys/TransactionSettings.aspx">交易设置</a> 
                        <a target="rightframe" href="Sys/OtherSettings.aspx">其它设置</a> 
                        <a target="rightframe" href="SysMailList.aspx">邮箱配置</a> 
                        <a target="rightframe" href="SysMailConfig.aspx">添加邮箱</a>   
                        <%--<a target="rightframe" href="User/Questions.aspx">问题管理</a>--%>   
                        <a target="rightframe" href="Manage/List.aspx">管理员列表</a>
                        <a target="rightframe" href="Manage/LoginLog.aspx">管理员登录日志</a>
                        <a target="rightframe" href="Manage/Salesman.aspx" >业务业绩</a>
                        <a target="rightframe" href="manage/Trades.aspx" >结算记录</a>  
                        <a target="rightframe" href="Cache/Manage.aspx" >清理系统缓存</a>
                        <a target="rightframe" href="Cache/Remote.aspx" >远程缓存管理</a>
                        <a target="rightframe" href="Sys/DataBackup.aspx" >数据备份</a>
                        <a target="rightframe" href="Sys/CleanUpData.aspx" >数据清理</a> 
                        <a target="rightframe" href="Template/Configuration.aspx" >短信模板</a>                         
                    </div>
                    <div id="left2" style="display: none">
                        <div class="lefttab">订单管理</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="order/banklist.aspx" >订单查询</a>
      <%-- <a target="rightframe" href="order/weixinlist.aspx" >微信订单</a>
  <a target="rightframe" href="order/alipaylist.aspx" >支付宝订单</a>
  <a target="rightframe" href="order/qqpaylist.aspx" >财付通订单</a>--%>

                        <a target="rightframe" href="order/cardlist.aspx" >点卡订单</a>
                        <a target="rightframe" href="order/cardsendlog.aspx" >点卡提交记录</a>
                        <a target="rightframe" href="order/cardWithholds.aspx" >卡密管理</a>
                        
                        <a target="rightframe" href="order/banklist.aspx?deduct=1" >网银扣量</a>
                        <a target="rightframe" href="order/cardlist.aspx?deduct=1" >点卡扣量</a>
                        <a target="rightframe" href="order/BankReports.aspx" >网银状态报告</a>
                        <a target="rightframe" href="order/CardReports.aspx" >点卡状态报告</a> 
                                              
                        <a target="rightframe" href="order/BankResetOrder.aspx" >网银手动补单</a>
                        <a target="rightframe" href="order/CardResetOrder.aspx" >点卡补单</a>
                        <a target="rightframe" href="order/Reconciliation2.aspx" >接口查询补单</a>
                        <a target="rightframe" href="Stat/orderReport.aspx" >综合统计</a>
                        <a target="rightframe" href="order/DebugInfos.aspx" >交易日志</a>
                        <a target="rightframe" href="order/Reconciliation.aspx" >对账查询</a>
                        <a target="rightframe" href="Tools/Md5.aspx" >MD5加密工具</a>
                        <a target="rightframe" href="Settled/recharges.aspx" >充值记录</a>                 
                    </div>    
                    <div id="left6" style="display: none">
                        <div class="lefttab">统计分析</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="stat/orderreport7.aspx" >利润分析</a> 
                        <a target="rightframe" href="stat/orderreport2.aspx" >对账统计</a>
                        <a target="rightframe" href="stat/orderreport3.aspx" >收支统计</a>
                        <a target="rightframe" href="stat/orderreport4.aspx" >代理收益</a>
                        <a target="rightframe" href="stat/usersOrderIncomes.aspx">商户收益</a>
                        <a target="rightframe" href="stat/orderreport5.aspx" >风控管理</a> 
                        <a target="rightframe" href="stat/orderreport6.aspx" >业务统计</a>  
    <%--                    <a target="rightframe" href="Tools/Calculators.html" >计算器</a>     --%>        
                    </div>                     
                    <div id="left3" style="display: none">
                        <div class="lefttab">接口管理</div>
                        <div style="padding-top: 10px"></div>                        
                        <a target="rightframe" href="supplier/List.aspx" >接口商列表</a>
                        <a target="rightframe" href="finance/payRate.aspx?type=3" >接口商费率</a>
                        <a target="rightframe" href="channel/TypeList.aspx" >通道类型</a> 
                        <a target="rightframe" href="channel/List.aspx" >通道管理</a>
                        <a target="rightframe" href="channel/WithdrawChannels.aspx" >结算通道</a>
                        <%--<a target="rightframe" href="Channel/CodeMappinglList.aspx" >代码映射</a>--%>  
                        <%--<a target="rightframe" href="PayPriceConv.aspx">转化设置</a>--%>
                    </div>
                    <div id="left4" style="display: none">
                        <div class="lefttab">商户&代理管理</div>
                        <div style="padding-top: 10px">
                        </div>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=2">商户列表</a> 
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=4">已锁定商户</a> 
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=1">未审核商户</a>
                        <a target="rightframe" href="User/Credentials.aspx?s=1">证件审核</a> 
                        <a target="rightframe" href="User/DomainList.aspx">域名管理</a>
                        <a target="rightframe" href="User/UserLevels.aspx">商户等级</a> 
                        <a target="rightframe" href="User/SettleAccountBill.aspx">账户审核</a>
                        <a target="rightframe" href="finance/PayRate.aspx">费率查看</a>                        
                        <a target="rightframe" href="Message/SMSRecords.aspx">短信记录</a>
                        <a target="rightframe" href="News/MsgList.aspx">内部消息</a>
                        <a target="rightframe" href="User/UserLoginLog.aspx">登录日志</a> 
                        <a target="rightframe" href="User/UserUpdateLog.aspx">修改日志</a> 
                        <a target="rightframe" href="Message/Feedbacks.aspx">反馈处理</a> 
                        <a target="rightframe" href="Message/Reports.aspx" >投诉处理</a>
                       <%-- <a target="rightframe" href="Tools/Calculators.html" >计算器</a>  --%>
                    </div>
                    <div id="left5" style="display: none">
                        <div class="lefttab"> 财务管理</div>
                        <div style="padding-top: 10px"></div>                       
                        <a target="rightframe" href="Withdraw/Audits.aspx" >提现审核</a>
                        <a target="rightframe" href="Withdraw/Pays.aspx" >付款操作</a>
                        <a target="rightframe" href="Withdraw/PayingByApi.aspx" >付款中（API）</a>
                        <a target="rightframe" href="Withdraw/SuppTransLogs.aspx" >接口付款</a>
                        <a target="rightframe" href="Withdraw/Historys.aspx" >结算记录</a>
                        <a target="rightframe" href="Finance/Trades.aspx" >交易记录</a>
                        <a target="rightframe" href="Finance/transfers.aspx" >转账记录</a>
                        <a target="rightframe" href="Finance/IncreaseAmts.aspx" >加款扣款</a>
                        <a target="rightframe" href="Finance/Freeze.aspx" >冻结款项</a>
                        <a target="rightframe" href="Finance/Thaw.aspx" >解冻款项</a>
                        <a target="rightframe" href="Withdraw/SysSettle.aspx" >商户结算</a>
                        <a target="rightframe" href="Withdraw/TocashSchemes.aspx" >提现方案</a>
                        <a target="rightframe" href="Settled/TransferschemeModi.aspx" >转账规则</a>  
                       <%-- <a target="rightframe" href="Tools/Calculators.html" >计算器</a>  --%>                      
                    </div>
                    <div id="left7" style="display: none">
                        <div class="lefttab"> 对私代发</div>
                        <div style="padding-top: 10px"></div>    
                        <a target="rightframe" href="AgentWithdraw/AgentDists.aspx" >对私代发</a>
                        <a target="rightframe" href="AgentWithdraw/AgentSummarys.aspx" >代发(上传)</a>              
                        <a target="rightframe" href="AgentWithdraw/AgentDists.aspx?audit_status=1" >代发审核</a>
                        <a target="rightframe" href="AgentWithdraw/AgentDists.aspx?audit_status=2" >等待付款</a>
                        <a target="rightframe" href="Withdraw/SuppTransLogs.aspx" >接口付款</a>
                        <a target="rightframe" href="AgentWithdraw/AgentDists.aspx?payment_status=4" >代发付款中</a>
                        <a target="rightframe" href="AgentWithdraw/AgentDists.aspx?audit_status=2&payment_status=2" >代发成功</a>
                        <a target="rightframe" href="AgentWithdraw/AgentDistNotifys.aspx" >结果通知</a>
                        <a target="rightframe" href="AgentWithdraw/AgentDistsSchemes.aspx" >代发规则</a>                     
                    </div>
                    
                    <div id="left8" style="display: none">
                        <div class="lefttab"> 额度管理</div>
                        <div style="padding-top: 10px"></div>    
                        <a target="rightframe" href="quota/quotalist.aspx" >额度转换列表</a>   
                        <a target="rightframe" href="quota/quotatype.aspx" >额度类型</a>
                        <a target="rightframe" href="quota/quotapayrate.aspx" >额度费率</a>                    
                    </div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
