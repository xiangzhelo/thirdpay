<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APPQQSet.aspx.cs" Inherits="viviAPI.WebUI7uka.APPQQSet" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=SiteName%>-安全,稳定,快捷</title>
    <style>
        .txt_ERROR
        {
            /*background-color: #FFA8A6;*/
            height: 16px;
            line-height: 16px;
            background-position: 8px;
            color: #ff0000;
            font-family: "微软雅黑";
            text-indent: 10px;
        }
    </style>

    <script language="javascript">
        function secBoard(n) {
            for (i = 0; i < secTable.cells.length; i++)
                secTable.cells[i].className = "sec1";
            secTable.cells[n].className = "sec2";
            for (i = 0; i < mainTable.tBodies.length; i++)
                mainTable.tBodies[i].style.display = "none";
            mainTable.tBodies[n].style.display = "block";
        }
        function refreshValidateCode(_id, url) {
            document.getElementById(_id).src = url + "?date=" + new Date();
        }
        function mOvr(src) {
            if (!src.contains(event.fromElement)) {
                src.style.cursor = 'hand'; src.bgColor = "#ffffff";
            }
        }
        function mOut(src) {
            if (!src.contains(event.toElement)) {
                src.style.cursor = 'default'; src.bgColor = "#FAFAFA";
            }
        }
        function mClk(src) {
            if (event.srcElement.tagName == 'TD') {
                src.children.tags('A')[0].click();
            }
        }


    </script>

    <!-- 头部尾部 -->
    <link rel="shortcut icon" href="ico/favicon.ico" />
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/index.css" />
    <link rel="stylesheet" href="web_js/easydialog.css" />

    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>

    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js" async="false"></script>

    <!-- end -->
    <style type="text/css">
        .pageAll
        {
            width: 1000px;
            margin: 100px auto 50px;
            padding: 20px;
        }
        .page_right
        {
            border-left: 1px solid #DCDCDC;
            padding-left: 50px;
        }
        .loginform-right
        {
            float: none;
            border: none;
        }
        .loginleft
        {
            line-height: 24px;
            color: #666;
        }
        .login_b
        {
            margin-bottom: 8px;
            color: #444;
        }
        .page-top
        {
            background: url(images/page-top.jpg) no-repeat;
            width: 1000px;
            height: 170px;
            margin: 0 auto;
        }
        .mm-kb
        {
            padding-left: 180px;
            background: url(images/page-middle.jpg) repeat-y;
            width: 1000px;
            padding-bottom: 100px;
        }
        .page-bottom
        {
            background: url(images/page-bottom.jpg) no-repeat;
            width: 1000px;
            height: 30px;
            margin: 0 auto;
        }
        .th
        {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:header ID="header1" runat="server" showtype="contact" IsShowQQLogin="false" />
    <div class="container">
        <div class="pageAll" style="width: 1000px; margin: 100px auto 50px; padding: 20px;
            padding-bottom: auto;">
            <div class="page-top">
            </div>
            <div class="mm-kb" style="text-align: left; padding-left: 140px; padding-bottom: 20px;
                margin-top: -1px;">
                <div class="cphfwq-xxxx">
                    <table width="700" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <div class="ts">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0" height="100" style="line-height: 100px;
                                                    font-size: 14px; letter-spacing: 2px;">
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            尊敬的QQ用户<asp:Literal ID="litQQUserName" runat="server"></asp:Literal>,请完善以下信息
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" height="340">
                                                
                                                <tr>
                                                    <td align="right" height="70">
                                                        <font color="#FF0000">&nbsp;</font>登录帐号&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <input type="text" name="newusername" id="newusername" class="form-control" placeholder="请输入登录帐号"
                                                            runat="server" />
                                                    </td>
                                                    <td align="right">
                                                        <font color="#FF0000">&nbsp;</font>电子邮箱&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <input type="text" name="newemail" id="newemail" class="form-control" placeholder="请输入电子邮箱"
                                                            runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" height="70">
                                                        <font color="#FF0000">&nbsp;</font>真实姓名&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <input type="text" name="newfullname" id="newfullname" class="form-control" placeholder="请输入真实姓名"
                                                            runat="server" />
                                                    </td>
                                                    <td align="right">
                                                        
                                                    </td>
                                                    <td>
                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" height="70">
                                                        <font color="#FF0000">&nbsp;</font>验 证 码&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <table border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <input type="text" name="txtvcode" id="txtvcode" class="form-control" style="width: 110px;"
                                                                        placeholder="请输入验证码" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;<img id="imgValidateCode" src="/vercode.aspx" onclick="refreshValidateCode('imgValidateCode','/vercode.aspx');"" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:Literal ID="litError" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center" valign="top">
                                                        <table>
                                                            <tr>
                                                                <td height="50">
                                                                    <span style="margin-left: 10px; margin-right: 5px;">商务审核</span>
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&amp;uin=240472266&amp;site=qq&amp;menu=yes">
                                                                        <img border="0" src="http://wpa.qq.com/pa?p=2:240472266:51" alt="点击这里给我发消息" title="点击这里给我发消息"></a>
                                                                </td>
                                                                <td>
                                                                    <span style="margin-left: 10px; margin-right: 5px;">商务审核</span>
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&amp;uin=240472266&amp;site=qq&amp;menu=yes">
                                                                        <img border="0" src="http://wpa.qq.com/pa?p=2:240472266:51" alt="点击这里给我发消息" title="点击这里给我发消息"></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="30">
                                                                    <span style="margin-left: 10px; margin-right: 5px;">商务审核</span>
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&amp;uin=240472266&amp;site=qq&amp;menu=yes">
                                                                        <img border="0" src="http://wpa.qq.com/pa?p=2:240472266:51" alt="点击这里给我发消息" title="点击这里给我发消息"></a>
                                                                </td>
                                                                <td>
                                                                    <span style="margin-left: 10px; margin-right: 5px;">商务审核</span>
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&amp;uin=240472266&amp;site=qq&amp;menu=yes">
                                                                        <img border="0" src="http://wpa.qq.com/pa?p=2:240472266:51" alt="点击这里给我发消息" title="点击这里给我发消息"></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td height="50" colspan="2" align="center" valign="top" style="line-height: 30px;">
                                                        <asp:ImageButton ID="iBtnSubmit" runat="server" ImageUrl="~/images/dreg.jpg" OnClick="iBtnSubmit_Click" /><br />
                                                        <a href="#" target="_parent" style="color: #2d2c32; text-decoration: none;">《亿付商务网用户注册协议》</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div style="width: 700px; height: 60px; border-top: 1px #cccccc dashed; margin-top: 50px;
                        text-align: right; line-height: 70px;">
                        <span>如果您有什么问题，可随时咨询</span>
                        <img src="images/phonereg.png" />
                        <span style="margin-right: 30px;">
                            <%=webInfo.Phone %></span>
                    </div>
                </div>
            </div>
            <div class="page-bottom" style="background: url(images/page-bottom.jpg) no-repeat;
                width: 1000px; height: 30px; margin: 0 auto;">
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <uc1:footer ID="footer1" runat="server" />
    </form>
</body>
</html>
