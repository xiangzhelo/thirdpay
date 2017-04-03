<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="viviAPI.WebUI7uka.Userlogin" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta charset="gb2312" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <title>��½</title>
    <link rel="stylesheet" href="css_demo/index.css">
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/bootstrap-dialog.css" />
    <link href="web_css/layout2.css" rel="stylesheet" type="text/css" media="all" />
    <link href="web_css/global2.css" rel="stylesheet" type="text/css" media="all" />
    <link rel="stylesheet" type="text/css" media="all" href="web_css/zi.css" />
    <link rel="stylesheet"  type="text/css" media="all" href="web_css/login.css" />

    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js"></script>
    <script language="javascript">
        function refreshValidateCode(_id, url) {
            document.getElementById(_id).src = url + "?date=" + new Date();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: auto; text-align: center; width: 350px; margin-top: 20px;">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" align="left" height="240">
            <tr>
                <td width="100" align="right">
                    �� �� ����
                </td>
                <td colspan="3" align="left">
                    <input id="username" type="text" class="form-control" style="padding: 0px; padding-left: 5px;"
                        placeholder="�������˻���" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="100" align="right">
                    ��¼���룺
                </td>
                <td colspan="3" align="left">
                    <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="form-control"
                        placeholder="�������½����" Style="padding: 0px; padding-left: 5px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="100" align="right">
                    �� ֤ �룺
                </td>
                <td colspan="3" align="left">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <input id="imycode" name="imycode" type="text" style="width: 100px; padding: 0px;
                                    padding-left: 5px;" class="form-control" placeholder="��������֤��" />
                            </td>
                            <td>
                                &nbsp;<img id='imgValidateCode' src="/vercode.aspx" width="67" height="23" />
                            </td>
                            <td class="partfont" style="font-size: 11px;">
                                &nbsp;<a href="javascript:refreshValidateCode('imgValidateCode','/vercode.aspx');">��һ��</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" height="25">
                </td>
                <td colspan="3" align="left">
                    <table border="0" cellspacing="0" cellpadding="0" style="font-size: 14px;">
                        <tr>
                            <td>
                                <input id="ckbsavepass" name="ckbsavepass" type="checkbox" checked="checked" />��ס��½�˺�
                            </td>
                            <td style="padding-left: 30px;">
                                <a href="findpwd.html" target="_parent">�������룿</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3" align="left">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/dl1.jpg" OnClick="ImageButton1_Click" />
                            </td>
                            <td class="partfont" style="font-size: 11px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
