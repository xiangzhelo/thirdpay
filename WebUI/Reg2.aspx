<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reg2.aspx.cs" Inherits="viviAPI.WebUI2015.Reg2" Async="true" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title><%=SiteName%></title>
    <link rel="stylesheet" type="text/css" href="/style/3/css/container.css" />
    <link rel="stylesheet" href="css_demo/index.css">

    <script language="javascript">
        function secBoard(n) {
            for (i = 0; i < secTable.cells.length; i++)
                secTable.cells[i].className = "sec1";
            secTable.cells[n].className = "sec2";
            for (i = 0; i < mainTable.tBodies.length; i++)
                mainTable.tBodies[i].style.display = "none";
            mainTable.tBodies[n].style.display = "block";
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
        function refreshValidateCode(_id, url) {
            document.getElementById(_id).src = url + "?date=" + new Date();
        }
        
    </script>

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
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 720px; text-align: left; margin: auto; font-size: 12px;">
        <div style="width: 720px;">
            <table width="720" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <div class="ts" style="height: 50px; background: none; border: none; font-size: 15px;
                                        letter-spacing: 3px;">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" height="100" style="line-height: 50px;
                                            font-size: 16px; letter-spacing: 2px; height: 50px;">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    签约申请成功后，我们会安排专人与您取得联系，请提供真实的联系人和联系方式。
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" height="200"
                                        style="margin-top: 10px;">
                                        <tr>
                                            <td align="right" height="48">
                                                <font color="#FF0000">&nbsp;</font>登录帐号&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <input type="text" name="newusername" id="newusername" class="form-control" style="padding: 0px;
                                                    padding-left: 5px;" placeholder="请输入登录帐号" value="" runat="server" />
                                            </td>
                                            <td width="50" rowspan="5" valign="top">
                                                <img src="images/regmid.png" />
                                            </td>
                                            <td align="right">
                                                <font color="#FF0000">&nbsp;</font>登录密码&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <input type="password" name="password1" id="password1" class="form-control" style="padding: 0px;
                                                    padding-left: 5px;" placeholder="请输入登陆密码" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" height="48">
                                                <font color="#FF0000">&nbsp;</font>重复密码&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <input type="password" name="password2" id="password2" class="form-control" style="padding: 0px;
                                                    padding-left: 5px;" placeholder="请输入重复密码" runat="server" />
                                            </td>
                                            <td align="right">
                                                <font color="#FF0000">&nbsp;</font>电子邮箱&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <input type="text" name="newemail" id="newemail" class="form-control" style="padding: 0px;
                                                    padding-left: 5px;" placeholder="请输入电子邮箱" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" height="48">
                                                <font color="#FF0000">&nbsp;</font>真实姓名&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <input type="text" name="newfullname" id="newfullname" class="form-control" style="padding: 0px;
                                                    padding-left: 5px;" placeholder="请输入真实姓名" runat="server" />
                                            </td>
                                            <td align="right">
                                                <font color="#FF0000">&nbsp;</font>联系QQ&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <input type="text" name="newqq" id="newqq" class="form-control" style="padding: 0px;
                                                    padding-left: 5px;" placeholder="请输入联系QQ" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" height="48">
                                                <font color="#FF0000">&nbsp;</font>验 证 码&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <table border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <input type="text" name="txtvcode" id="txtvcode" class="form-control" style="width: 110px;
                                                                padding: 0px; padding-left: 5px;" placeholder="请输入验证码" />
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
                                                            <span style="margin-left: 10px; margin-right: 5px;">商务:小穆</span>
                                                        </td>
                                                        <td>
                                                            <a href="http://wpa.qq.com/msgrd?v=3&amp;uin=177688013&amp;site=qq&amp;menu=yes"target="_blank">
                                    			<img width="72" height="22" border="0" title="点击与优卡商务交谈" alt="点击与优卡商务交谈" src="http://wpa.qq.com/pa?p=2:177688013:41"></a>
                                                        </td>
                                                        <td>
                                                            <span style="margin-left: 10px; margin-right: 5px;">商务:阿祥</span>
                                                        </td>
                                                        <td>
                                                            <a href="http://wpa.qq.com/msgrd?v=3&amp;uin=177688014&amp;site=qq&amp;menu=yes"target="_blank">
                                    			<img width="72" height="22" border="0" title="点击与优卡商务交谈" alt="点击与优卡商务交谈" src="http://wpa.qq.com/pa?p=2:177688014:41"></a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30">
                                                            <span style="margin-left: 10px; margin-right: 5px;">商务:小小</span>
                                                        </td>
                                                        <td>
                                                             <a href="http://wpa.qq.com/msgrd?v=3&amp;uin=177688015&amp;site=qq&amp;menu=yes"target="_blank">
                                    			<img width="72" height="22" border="0" title="点击与优卡商务交谈" alt="点击与优卡商务交谈" src="http://wpa.qq.com/pa?p=2:177688015:41"></a>
                                                        </td>
                                                        <td>
                                                            <span style="margin-left: 10px; margin-right: 5px;">商务:小王</span>
                                                        </td>
                                                        <td>
                                                            <a href="http://wpa.qq.com/msgrd?v=3&amp;uin=177688016&amp;site=qq&amp;menu=yes"target="_blank">
                                    			<img width="72" height="22" border="0" title="点击与优卡商务交谈" alt="点击与优卡商务交谈" src="http://wpa.qq.com/pa?p=2:177688016:41"></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td height="50" colspan="2" align="center" valign="top" style="line-height: 30px;">
                                                <asp:Button ID="iBtnSubmit1" runat="server" CssClass="" Text="" OnClick="iBtnSubmit1_Click" />
                                                <br />
                                                <a href="/view5.html" target="_blank" style="color: #2d2c32">《亿付商务网用户注册协议》</a>
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
                <span style="margin-right: 30px;"><%=webInfo.Phone %></span>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
