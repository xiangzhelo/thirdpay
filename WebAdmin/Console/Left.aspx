<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.web.Manage.Left" Codebehind="Left.aspx.cs" %>

<html xmlns="">
<head>
    <title>����</title>
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
	        for (var i = 0; i < 8; i++) {
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
                        <div class="lefttab">�ճ�ʹ��</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="Order/banklist.aspx?status=2">��������</a>
                        <a target="rightframe" href="Order/cardlist.aspx?status=2">�㿨����</a>
                        <%--<a target="rightframe" href="DayCount.aspx">�ۺ�ͳ��</a>--%>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=1">�̻��б�</a>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=0">�̻����</a>
                        <a target="rightframe" href="News/NewsEdit.aspx">���ŷ���</a>
                        <a target="rightframe" href="Withdraw/Audits.aspx">�������</a>
                    </div>
                    <div id="left1" style="display: none;">
                        <div class="lefttab">��������</div>
                        <div style="padding-top: 10px"></div>                    
                        <a target="rightframe" href="News/NewsList.aspx" > ���Ź���</a>
                        <a target="rightframe" href="News/NewsEdit.aspx" >���ŷ���</a>                                             
                        <a target="rightframe" href="SiteInfo.aspx">վ������</a> 
                        <a target="rightframe" href="Sys/RegisterSettings.aspx">ע������</a> 
                        <a target="rightframe" href="Sys/TransactionSettings.aspx">��������</a> 
                        <a target="rightframe" href="Sys/OtherSettings.aspx">��������</a> 
                        <a target="rightframe" href="SysMailList.aspx">��������</a> 
                        <a target="rightframe" href="SysMailConfig.aspx">�������</a>   
                        <%--<a target="rightframe" href="User/Questions.aspx">�������</a>--%>   
                        <a target="rightframe" href="Manage/List.aspx">����Ա�б�</a>
                        <a target="rightframe" href="Manage/LoginLog.aspx">����Ա��¼��־</a>
                        <a target="rightframe" href="Manage/Salesman.aspx" >ҵ��ҵ��</a>
                        <a target="rightframe" href="manage/Trades.aspx" >�����¼</a>  
                        <a target="rightframe" href="Cache/Manage.aspx" >����ϵͳ����</a>
                        <a target="rightframe" href="Cache/Remote.aspx" >Զ�̻������</a>
                        <a target="rightframe" href="Sys/DataBackup.aspx" >���ݱ���</a>
                        <a target="rightframe" href="Sys/CleanUpData.aspx" >��������</a> 
                        <a target="rightframe" href="Template/Configuration.aspx" >����ģ��</a>                         
                    </div>
                    <div id="left2" style="display: none">
                        <div class="lefttab">��������</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="order/banklist.aspx" >������ѯ</a>
      <%-- <a target="rightframe" href="order/weixinlist.aspx" >΢�Ŷ���</a>
  <a target="rightframe" href="order/alipaylist.aspx" >֧��������</a>
  <a target="rightframe" href="order/qqpaylist.aspx" >�Ƹ�ͨ����</a>--%>

                        <a target="rightframe" href="order/cardlist.aspx" >�㿨����</a>
                        <a target="rightframe" href="order/cardsendlog.aspx" >�㿨�ύ��¼</a>
                        <a target="rightframe" href="order/cardWithholds.aspx" >���ܹ���</a>
                        
                        <a target="rightframe" href="order/banklist.aspx?deduct=1" >��������</a>
                        <a target="rightframe" href="order/cardlist.aspx?deduct=1" >�㿨����</a>
                        <a target="rightframe" href="order/BankReports.aspx" >����״̬����</a>
                        <a target="rightframe" href="order/CardReports.aspx" >�㿨״̬����</a> 
                                              
                        <a target="rightframe" href="order/BankResetOrder.aspx" >�����ֶ�����</a>
                        <a target="rightframe" href="order/CardResetOrder.aspx" >�㿨����</a>
                        <a target="rightframe" href="order/Reconciliation2.aspx" >�ӿڲ�ѯ����</a>
                        <a target="rightframe" href="Stat/orderReport.aspx" >�ۺ�ͳ��</a>
                        <a target="rightframe" href="order/DebugInfos.aspx" >������־</a>
                        <a target="rightframe" href="order/Reconciliation.aspx" >���˲�ѯ</a>
                        <a target="rightframe" href="Tools/Md5.aspx" >MD5���ܹ���</a>
                        <a target="rightframe" href="Settled/recharges.aspx" >��ֵ��¼</a>                 
                    </div>    
                    <div id="left6" style="display: none">
                        <div class="lefttab">ͳ�Ʒ���</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="stat/orderreport7.aspx" >�������</a> 
                        <a target="rightframe" href="stat/orderreport2.aspx" >����ͳ��</a>
                        <a target="rightframe" href="stat/orderreport3.aspx" >��֧ͳ��</a>
                        <a target="rightframe" href="stat/orderreport4.aspx" >��������</a>
                        <a target="rightframe" href="stat/usersOrderIncomes.aspx">�̻�����</a>
                        <a target="rightframe" href="stat/orderreport5.aspx" >��ع���</a> 
                        <a target="rightframe" href="stat/orderreport6.aspx" >ҵ��ͳ��</a>  
    <%--                    <a target="rightframe" href="Tools/Calculators.html" >������</a>     --%>        
                    </div>                     
                    <div id="left3" style="display: none">
                        <div class="lefttab">�ӿڹ���</div>
                        <div style="padding-top: 10px"></div>                        
                        <a target="rightframe" href="supplier/List.aspx" >�ӿ����б�</a>
                        <a target="rightframe" href="finance/payRate.aspx?type=3" >�ӿ��̷���</a>
                        <a target="rightframe" href="channel/TypeList.aspx" >ͨ������</a> 
                        <a target="rightframe" href="channel/List.aspx" >ͨ������</a>
                        <a target="rightframe" href="channel/WithdrawChannels.aspx" >����ͨ��</a>
                        <%--<a target="rightframe" href="Channel/CodeMappinglList.aspx" >����ӳ��</a>--%>  
                        <%--<a target="rightframe" href="PayPriceConv.aspx">ת������</a>--%>
                    </div>
                    <div id="left4" style="display: none">
                        <div class="lefttab">�̻�&�������</div>
                        <div style="padding-top: 10px">
                        </div>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=2">�̻��б�</a> 
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=4">�������̻�</a> 
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=1">δ����̻�</a>
                        <a target="rightframe" href="User/Credentials.aspx?s=1">֤�����</a> 
                        <a target="rightframe" href="User/DomainList.aspx">��������</a>
                        <a target="rightframe" href="User/UserLevels.aspx">�̻��ȼ�</a> 
                        <a target="rightframe" href="User/SettleAccountBill.aspx">�˻����</a>
                        <a target="rightframe" href="finance/PayRate.aspx">���ʲ鿴</a>                        
                        <a target="rightframe" href="Message/SMSRecords.aspx">���ż�¼</a>
                        <a target="rightframe" href="News/MsgList.aspx">�ڲ���Ϣ</a>
                        <a target="rightframe" href="User/UserLoginLog.aspx">��¼��־</a> 
                        <a target="rightframe" href="User/UserUpdateLog.aspx">�޸���־</a> 
                        <a target="rightframe" href="Message/Feedbacks.aspx">��������</a> 
                        <a target="rightframe" href="Message/Reports.aspx" >Ͷ�ߴ���</a>
                       <%-- <a target="rightframe" href="Tools/Calculators.html" >������</a>  --%>
                    </div>
                    <div id="left5" style="display: none">
                        <div class="lefttab"> �������</div>
                        <div style="padding-top: 10px"></div>                       
                        <a target="rightframe" href="Withdraw/Audits.aspx" >�������</a>
                        <a target="rightframe" href="Withdraw/Pays.aspx" >�������</a>
                        <a target="rightframe" href="Withdraw/PayingByApi.aspx" >�����У�API��</a>
                        <a target="rightframe" href="Withdraw/SuppTransLogs.aspx" >�ӿڸ���</a>
                        <a target="rightframe" href="Withdraw/Historys.aspx" >�����¼</a>
                        <a target="rightframe" href="Finance/Trades.aspx" >���׼�¼</a>
                        <a target="rightframe" href="Finance/transfers.aspx" >ת�˼�¼</a>
                        <a target="rightframe" href="Finance/IncreaseAmts.aspx" >�ӿ�ۿ�</a>
                        <a target="rightframe" href="Finance/Freeze.aspx" >�������</a>
                        <a target="rightframe" href="Finance/Thaw.aspx" >�ⶳ����</a>
                        <a target="rightframe" href="Withdraw/SysSettle.aspx" >�̻�����</a>
                        <a target="rightframe" href="Withdraw/TocashSchemes.aspx" >���ַ���</a>
                        <a target="rightframe" href="Settled/TransferschemeModi.aspx" >ת�˹���</a>  
                       <%-- <a target="rightframe" href="Tools/Calculators.html" >������</a>  --%>                      
                    </div>
                    <div id="left7" style="display: none">
                        <div class="lefttab"> ��˽����</div>
                        <div style="padding-top: 10px"></div>    
                        <a target="rightframe" href="AgentWithdraw/AgentDists.aspx" >��˽����</a>
                        <a target="rightframe" href="AgentWithdraw/AgentSummarys.aspx" >����(�ϴ�)</a>              
                        <a target="rightframe" href="AgentWithdraw/AgentDists.aspx?audit_status=1" >�������</a>
                        <a target="rightframe" href="AgentWithdraw/AgentDists.aspx?audit_status=2" >�ȴ�����</a>
                        <a target="rightframe" href="Withdraw/SuppTransLogs.aspx" >�ӿڸ���</a>
                        <a target="rightframe" href="AgentWithdraw/AgentDists.aspx?payment_status=4" >����������</a>
                        <a target="rightframe" href="AgentWithdraw/AgentDists.aspx?audit_status=2&payment_status=2" >�����ɹ�</a>
                        <a target="rightframe" href="AgentWithdraw/AgentDistNotifys.aspx" >���֪ͨ</a>
                        <a target="rightframe" href="AgentWithdraw/AgentDistsSchemes.aspx" >��������</a>                     
                    </div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
