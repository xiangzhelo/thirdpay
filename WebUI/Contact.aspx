<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="viviAPI.WebUI2015.Contact" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�ڸ���������ϵ��ʽ_�㿨����_API֧���ӿ�_��Ϸ�㿨����_��Ϸ֧��ƽ̨�ӿ��ṩ��</title>
    <meta name="KeyWords" content="API,֧���ӿ�,�㿨����,��Ϸ֧�� <%=KeyWords%>" />
    <meta name="description" content="�ڸ���������ȫ�����ȵ���Ϸ�㿨����API�ӿ��ṩ�̣�ӵ�ж����ȶ�����Ϸ֧�����սӿ�����������ʢ�󡢿��������ס����Ρ���ѶQ�ҿ�����������;����ɽ�������һ��ͨ����������������Ϸƽ̨������רҵ�ṩ��ȫ�ȶ���Ч����ϷAPI֧�����սӿ�ƽ̨��������Ӫ��վ����Ϸ����Ҫ������顣<%=Description%>" />
    <link rel="shortcut icon" href="ico/favicon.ico" />
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/index.css" />
    <link rel="stylesheet" href="web_js/easydialog.css" />
    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js" async="false"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:header ID="header1" runat="server" showtype="contact" />
    <div class="clear">
    </div>
    <div id="page">
        <div class="wrapsemibox about-box-mm">
            <div class="semiboxshadow">
                <img alt="" class="semiboxshadow_bg" src="images/shp.png">
            </div>
            <div class="w-1200">
                <div class="about-span1" style="height: 500px;">
                    <div class="about-menu">
                        <ul>
                            <li>
                                <dd>
                                    <a href="/about.aspx">�ſ�����</a></dd>
                                <dd>
                                    <a href="/culture.aspx">�����Ļ�</a></dd>
                                <dd class="about-menu-current">
                                    <a href="/contact.aspx">��ϵ����</a></dd>
                                <dd>
                                    <a href="/service.aspx">�ͷ�����</a></dd>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="about-span2 y-last managebar">
                    <div class="aboutus">
                        <p>
                            <strong>
                                <img src="images/b.jpg" /></strong></p>
                    </div>
                    <br />
                    <div style="width: 100%;margin: 0 0 0 30px;">
                        <div style="margin: 3px;"><label>��ϵ�ˣ�</label><span>������</span></div>
                        <div style="margin: 3px;"><label>��ϵ�绰��</label><span>0592-6511723</span></div>
                        <div style="margin: 3px;"><label>QQ���룺</label><span>325915361</span></div>
                        <div style="margin: 3px;"><label>���䣺</label><span>325915361@qq.com</span></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="pagebottom">
        </div>
    </div>
    <div class="line">
    </div>
    <!--footer------------------------------->
    <uc1:footer ID="footer1" runat="server" showtype="news" />
    <!--footer end------------------------------->
    </form>
</body>
</html>