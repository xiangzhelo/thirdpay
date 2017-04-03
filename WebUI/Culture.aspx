<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Culture.aspx.cs" Inherits="viviAPI.WebUI7uka.Culture" %>
<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�ڸ���������ҵ�Ļ�_�㿨����_API֧���ӿ�_��Ϸ�㿨����_��Ϸ֧��ƽ̨�ӿ��ṩ��</title>
    <meta name="KeyWords" content="API,֧���ӿ�,�㿨����,��Ϸ֧�� <%=KeyWords%>" />
    <meta name="description" content="�ڸ���������ȫ�����ȵ���Ϸ�㿨����API�ӿ��ṩ�̣�ӵ�ж����ȶ�����Ϸ֧�����սӿ�����������ʢ�󡢿��������ס����Ρ���ѶQ�ҿ�����������;����ɽ�������һ��ͨ����������������Ϸƽ̨������רҵ�ṩ��ȫ�ȶ���Ч����ϷAPI֧�����սӿ�ƽ̨��������Ӫ��վ����Ϸ����Ҫ������顣<%=Description%>" />
    <link rel="shortcut icon" href="ico/favicon.ico">
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="css_demo/index.css">
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
                <!--left -->
                <div class="about-span1" style="height: 500px;">
                    <div class="about-menu">
                        <ul>
                            <li>
                                <dd>
                                    <a href="/about.aspx">�ſ�����</a></dd>
                                <dd class="about-menu-current">
                                    <a href="/culture.aspx">�����Ļ�</a></dd>
                                <dd>
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
                                <img src="images/c.jpg" /></strong></p>
                        <p style="line-height: 30px; border: none;">
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ڸ�������2013���������ʼ�ն�������Ϊ�û��ṩ�����ʵķ�������������Ŷӡ�����������Ŷӡ����ĵĿͷ��Ŷӡ������ļ����Ŷ�Ĭ������ϣ��𽥽��ſ�����ȫ���г�����Ϊ�����������ֿ����׷���ƽ̨��<br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ڸ�������API�ӿڴ������MD5����������㷨��ȷ���û���������Ϣ֮��ı����ԡ������ԡ��໥�����������ݣ����̻��˺ž���ʹ�õ��ӿ����IP���ܱ������������ְ�ȫ��ʩ�󶨣��˺��ʽ����ȫ��<br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ڸ�������ӵ�е����������г��κ�һ�ҵĻ���������2��䣬ͨ�������Ŷ��Լ�����������Ա�Ĳ�иŬ������Ϊ�ſ����س�Խ��Խ����г���ҲΪ�ſ�פ���ֵ�����׵���������˼�ʵ�Ļ�����������������ſ�ӵ�����ڼ۸����޿ɱ�������ƣ��Ը��Żݵļ۸񡢸��ȶ���������������û���������<br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�ڸ���������ӵ�еĿͷ��Ŷӣ�רҵ�����桢����Ϊ�����һ�����ţ��������԰��ġ����ʵ�ʹ�ã�7*24Сʱ����Ϊ���ṩרҵ�Ķ�����ѯ���񡣼����Ŷ��������û����Ǹ�Ч�ʣ�����Ĺ������鼰����������Ķ���Ӧ����������ȫ����Ѹ�ٵĽ������ͻ��״������֤�û�������ʹ�á�<br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�����г�Խ��Խ�󣬻���Խ��Խ�࣬�������������ţ���һ����ѡ��������͹ۡ��ӿڸ��ȶ������������㡢�ʽ����ȫ���ڸ���������Ϊ���ĺ�����顣
                        </p>
                        <p style="line-height: 36px; border: none;">
                            <br />
                            <span style="font-weight: bold">���ǵ����</span> �� �� ����<br />
                            &nbsp;&nbsp;&nbsp;���� �� �� ��ҵ���ž�Ӫ����ҵ�Ļ��������Ҫ����������֮��<br />
                            <span style="font-weight: bold">���ǵľ���</span>�������¡�����<br>
                            &nbsp;&nbsp;&nbsp;���� �� �� �ȶ���Ч�����ǵķ�չ���������������ǳ�����չ�ı��ϣ�Ҫ�ڸ��ٵķ�չ�Ľ��죬���ڲ���֮�أ�Ψ�в��ϴ��£����Ͻ�ȡ<br />
                            &nbsp;&nbsp;&nbsp;���� �� �� ���ǲ���Ҫ����ҵ����ͬ�������ȣ�ͬʱ����Ҫ�ڹ�����ƺ�������ҲҪ����<br />
                            <span style="font-weight: bold">���ǵĿں�</span> �� �� ��ȫ��ݡ��ȶ���Ч<br>
                            <span style="font-weight: bold">��Ӫ����</span> �� �� �ԡ���ȫ��ݡ��ȶ���Ч��������Ϊ����ʵ����ҵ��Ԫ������ģ��<br />
                            <span style="font-weight: bold">�������</span> �� �� һ�����ģ�������ϵ����������<br />
                            &nbsp;&nbsp;&nbsp;���쵼Ϊ���ģ���չ�ŶӾ���<br>
                            &nbsp;&nbsp;&nbsp;����ù�˾��ͻ��Ĺ�ϵ�������벿�ŵĹ�ϵ��<br />
                            &nbsp;&nbsp;&nbsp;�ͻ����⡢�쵼���⡢����֮������<br>
                            <span style="font-weight: bold">�������</span> �� �� ���ؿͻ������ͻ��������ṩ��Խ�ͻ������Ĳ�Ʒ��������ͻ��ǳ��ڵĺ������<br />
                            <span style="font-weight: bold">�ŶӾ���</span> �� �� ����һ�ģ���־�ɳǣ������ڳ������½�ͨ<br /> <br />
                        </p>
                    </div>
                </div>
            </div>
            <!--�м䲿�ֽ���-->
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
