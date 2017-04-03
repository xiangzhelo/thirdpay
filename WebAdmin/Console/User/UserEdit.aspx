<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviAPI.WebAdmin.Console.User.UserEdits" Codebehind="UserEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../style/admin.css?v=1" type="text/css" rel="stylesheet" />
    <link href="../style/edit.css" type="text/css" rel="stylesheet" />
    <script src="../js/common.js" type="text/javascript"></script>
    
<script type="text/javascript">  
function backreturn(){
    history.go(-1);
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="title">
                    商户信息编辑</td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td class="td2b">
                    用户名ID：</td>
                <td class="td1b">
                    <asp:Label ID="lblid" runat="server" Width="160px" CssClass="lable"></asp:Label>
                </td>
                <td class="td2b">
                    用户名：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtuserName" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    签约属性：</td>
                <td class="td1b">
                    &nbsp;<asp:RadioButtonList ID="rbuserclass" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="0">个人</asp:ListItem>
                        <asp:ListItem Value="1">企业</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="td2b">
                    用户名：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtfullname" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    登录密码：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtpassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                </td>
                <td class="td2b">
                    转账密码：</td>
                <td class="td1b">
                <asp:TextBox ID="txtpassword2" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    会员类型：</td>
                <td class="td1b">
                    &nbsp;<asp:RadioButtonList ID="rbluserType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="3">会员</asp:ListItem>
                        <asp:ListItem Value="4">代理</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="td2b">
                    会员等级：</td>
                <td class="td1b">
                    <asp:DropDownList ID="ddlUserLevel" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    所属代理：</td>
                <td class="td1b">
                   <asp:DropDownList ID="ddlagents" runat="server"></asp:DropDownList>
                </td>
                <td class="td2b">
                    所属业务员：</td>
                <td class="td1b">
                    <asp:DropDownList ID="ddlmange" runat="server"></asp:DropDownList>
                    <asp:TextBox ID="txtGetPromSuperior" runat="server" Width="200px" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    扣率：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtCPSDrate" runat="server" Width="200px">0</asp:TextBox>
                </td>
                <td class="td2b">
                    转率：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtCVSNrate" runat="server" Width="200px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    邮箱地址：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtemail" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="td2b">
                    QQ号码：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtqq" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    手机号码：</td>
                <td class="td1b">
                    <asp:TextBox ID="txttel" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="td2b">
                    身份证号码：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtidCard" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    收款方式：</td>
                <td class="td1b">
                    &nbsp;<asp:RadioButtonList ID="rblsettlemode" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">银行帐户</asp:ListItem>
                        <asp:ListItem Value="2">支付宝</asp:ListItem>
                        <asp:ListItem Value="3">财付通</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="td2b">
                    开户银行：</td>
                <td class="td1b"> 
                     <asp:DropDownList ID="ddlpayeeBank" runat="server">                        
                        <asp:ListItem value="">--开户银行--</asp:ListItem>
                        <asp:ListItem value="0002">支付宝</asp:ListItem>
                        <asp:ListItem value="0003">财付通</asp:ListItem>
                        <asp:ListItem value="1002">中国工商银行</asp:ListItem>
                        <asp:ListItem value="1005">中国农业银行</asp:ListItem>
                        <asp:ListItem value="1003">中国建设银行</asp:ListItem>
                        <asp:ListItem value="1026">中国银行</asp:ListItem>
                        <asp:ListItem value="1001">招商银行</asp:ListItem>
                        <asp:ListItem value="1006">民生银行</asp:ListItem>
                        <asp:ListItem value="1020">交通银行</asp:ListItem>
                        <asp:ListItem value="1025">华夏银行</asp:ListItem>
                        <asp:ListItem value="1009">兴业银行</asp:ListItem>
                        <asp:ListItem value="1027">广发银行</asp:ListItem>
                        <asp:ListItem value="1004">浦发银行</asp:ListItem>
                        <asp:ListItem value="1022">光大银行</asp:ListItem>
                        <asp:ListItem value="1021">中信银行</asp:ListItem>
                        <asp:ListItem value="1010">平安银行</asp:ListItem>
                        <asp:ListItem value="1066">中国邮政储蓄银行</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    账号类型：</td>
                <td class="td1b">
                    <asp:RadioButtonList ID="rblaccoutType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="0">私人帐户</asp:ListItem>
                        <asp:ListItem Value="1">公司帐户</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="td2b">
                    省份城市：</td>
                <td class="td1b">
                    <asp:DropDownList ID="ddlprovince" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlprovince_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlcity" runat="server">
                            <asp:ListItem Value="">--市区--</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    支行名称：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtbankAddress" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="td2b">
                    收款人：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtpayeeName" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    银行帐号：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtaccount" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="td2b">
                    状态 ：</td>
                <td class="td1b">
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>            
            <tr>
                <td class="td2b">
                    转账方案 ：</td>
                <td class="td1b">
                    <asp:DropDownList ID="ddlTocashScheme" runat="server" Width="150px"></asp:DropDownList>
                </td>
                <td class="td2b">
                    归属地址：</td>
                <td class="td1b">
                     <asp:TextBox ID="txtProvince" runat="server" Width="80px"></asp:TextBox>
                     <asp:TextBox ID="txtCity" runat="server" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    注册时间：</td>
                <td class="td1b">
                    <asp:Label ID="lblregTime" runat="server" CssClass="lable" Width="160px" ></asp:Label>
                </td>
                <td class="td2b">
                    最后登录IP：</td>
                <td class="td1b">
                    <asp:Label ID="lbllastLoginIp" runat="server" CssClass="lable" Width="160px" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    最后登录时间：</td>
                <td class="td1b">
                    <asp:Label ID="lbllastLoginTime" runat="server" CssClass="lable" Width="160px" ></asp:Label>
                </td>
                <td class="td2b">
                    帐户余额：</td>
                <td class="td1b">
                    <asp:Label ID="lblbalance" runat="server" CssClass="lable" Width="160px" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    网站名称 ：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtsiteName" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="td2b">
                    API账号：</td>
                <td class="td1b">
                     <asp:TextBox ID="txtapiAcct" runat="server"  CssClass="lable" Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    安全问题 ：</td>
                <td class="td1b">
                    <asp:TextBox ID="txtquestion" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="td2b">
                    安全答案：</td>
                <td class="td1b">
                     <asp:TextBox ID="txtanswer" runat="server"  CssClass="lable" Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="td2b">
                    无来路支付地址：</td>
                <td class="td1b" colspan="4">
                    <asp:TextBox ID="txtUrlNoRefPayUrl" runat="server" Width="90%"></asp:TextBox>                   
                </td>
            </tr>
            <tr style="display: none">
                <td class="td2b">
                    短信接收地址：</td>
                <td class="td1b" colspan="4">
                    <asp:TextBox ID="txtsmsNotifyUrl" runat="server" Width="90%"></asp:TextBox>                   
                </td>
            </tr>
            <tr>                
                <td class="td2b">
                    无来路状态 ：</td>
                <td class="td1b">
                    <asp:CheckBox ID="cb_UrlNoRefPayUrl" runat="server" Text="开启" />
                    是否记录交易错误日志：<asp:CheckBox ID="cb_isdebug" runat="server" Text="是" />
                 </td>
                 <td class="td2b">
                    结算模式：</td>
                <td class="td1b">
                    <asp:RadioButtonList ID="rbl_settledmode" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="T+0" Value="0"></asp:ListItem>
                        <asp:ListItem Text="T+1" Value="1"></asp:ListItem>                        
                    </asp:RadioButtonList>             
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    网站地址：</td>
                <td class="td1b" colspan="5" style="width:100%">
                    <asp:TextBox ID="txtsiteUrl" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>           
            <tr>
                <td class="td2b">
                    API通讯密钥：</td>
                <td class="td1b" colspan="5" style="width:100%">
                    <asp:TextBox ID="txtapikey" runat="server" Rows="2" TextMode="MultiLine" Width="80%" CssClass="lable"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    认证信息 ：</td>
                <td class="td1b" colspan="5" style="width:100%">
                    <asp:CheckBox ID="cb_isRealNamePass" runat="server" Text="实名认证" />
                &nbsp;<asp:CheckBox ID="cb_isEmailPass" runat="server" Text="邮件认证" />
                &nbsp;<asp:CheckBox ID="cb_isPhonePass" runat="server" Text="手机认证" />
                &nbsp;<asp:CheckBox ID="cb_istransfer" runat="server" Text="开启转账" />
                &nbsp;<asp:CheckBox ID="cb_isagentDistribution" runat="server" Text="对私代发" />
              <%--  &nbsp;<asp:CheckBox ID="cb_RiskWarning" runat="server" Text="交易风险提醒" />--%>
                
                &nbsp;<asp:CheckBox ID="ckb_rw_bank" runat="server"  Text="网银交易风险"/>
                    &nbsp; <asp:CheckBox ID="ckb_rw_alipay" runat="server" Text="支付宝交易风险"/>
                     &nbsp; <asp:CheckBox ID="ckb_rw_alicode" runat="server" Text="支付宝扫码交易风险"/>
                      &nbsp; <asp:CheckBox ID="ckb_rw_wxpay" runat="server" Text="微信支付交易风险"/>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    代发规则 ：</td>
                <td class="td1b">
                    <asp:DropDownList ID="ddlagentDistscheme" runat="server" Width="150px"></asp:DropDownList>
                </td>
                <td class="td2b">
                    点卡版本：</td>
                <td class="td1b">
                    <asp:DropDownList ID="ddlcardversion" runat="server" Width="150px">
                        <asp:ListItem Value="0" Selected="True">--点卡版本--</asp:ListItem>
                        <asp:ListItem Value="1">普及</asp:ListItem>
                        <asp:ListItem Value="2">专业</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td2b">
                    备注 ：</td>
                <td class="td1b" colspan="5" style="width:100%">
                    <asp:TextBox ID="txtdesc" runat="server" Rows="3" TextMode="MultiLine" Width="80%" CssClass="lable"></asp:TextBox>
                </td>
            </tr>            
            <tr>
                <td colspan="10" style="height: 20px">
                    <div align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="提 交" OnClick="btnAdd_Click"></asp:Button>
                        <asp:Button ID="btnNew" runat="server" Text="重置APIKey" onclick="btnNew_Click" ></asp:Button>
                        <asp:Button ID="btnUnbind" runat="server" Text="QQ登录解绑" Visible="False" 
                            onclick="btnUnbind_Click" ></asp:Button>
                            
                            <asp:Button ID="btnClearCache" runat="server" Text="清空缓存" 
                            onclick="btnClearCache_Click" ></asp:Button>
                        <input type="button" value="返 回" onclick="backreturn()" />
                    </div>
                </td>
            </tr>
        </table>
        
    </form>
</body>
</html>
