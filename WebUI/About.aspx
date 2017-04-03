<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="viviAPI.WebUI7uka.About" %>
<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�ڸ���������ҵ���_��Ϸ�㿨����_API���Ľӿ�_��Ϸ֧��ƽ̨�ӿ��ṩ��</title>
    <meta name="KeyWords" content="API,֧���ӿ�,�㿨����,��Ϸ֧�� <%=KeyWords%>" />
    <meta name="description" content="�ڸ���������ȫ�����ȵ���Ϸ�㿨����API�ӿ��ṩ�̣�ӵ�ж����ȶ�����Ϸ֧�����սӿ�����������ʢ�󡢿��������ס����Ρ���ѶQ�ҿ�����������;����ɽ�������һ��ͨ����������������Ϸƽ̨������רҵ�ṩ��ȫ�ȶ���Ч����ϷAPI֧�����սӿ�ƽ̨��������Ӫ��վ����Ϸ����Ҫ������顣<%=Description%>" />
    <link rel="shortcut icon" href="ico/favicon.ico" />
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/index.css" />
    <link rel="stylesheet" href="web_js/easydialog.css" />
    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js"></script>
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
                                <dd class="about-menu-current">
                                    <a href="/about.aspx">�ڸ����</a></dd>
                                <dd>
                                    <a href="/culture.aspx">�ڸ��Ļ�</a></dd>
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
                                <img src="images/a.jpg" /></strong></p>
                        <p style="line-height: 35px; border: none;">
				<br>&nbsp;&nbsp;&nbsp;&nbsp;���ڸ����������ǹ������ȵ���Ϸ�㿨����ƽ̨���ٷ���ַ��www.59az.com���ڸ��������ɼ�����������Ƽ����޹�˾��2013��10�´�������
				���Ż��ݻ�����������Ӯ��ԭ��Ϊ�����ڶ����Ϸ��Ӫ�̼�����������Ӫ����������ְ�ȫ����ݡ��ȶ��ĳ�ֵ�������շѷ�ʽ��
				<br>&nbsp;&nbsp;&nbsp;&nbsp;�����й����õĿ��ٷ�չ������Ӧ�õĲ��ϳ��죬���������ҵ�ѽ�����ٷ�չ�׶Ρ��ڵ������������ڸ�������ƾ�贴�¶���ʵ�ķ�����ȵļ�����������г�Ԥ��������һֱ���				���̼ҹ�����API�ӿڼ������������Ʒ�ƣ����ϸ��ݿͻ��������Ƴ����²�Ʒ��Ϊ�ٽ����������ҵ�ĳ�����չ������иŬ����
				<br>&nbsp;&nbsp;&nbsp;&nbsp;�������գ��ڸ������������й��ƶ����й���ͨ���й����š�֧�������Ƹ�ͨ����Ѹ֧������Ѷ��������ʢ�󡢱������ױ��ȶ������֧������Ϸ��ҵ������ս�Ժ�������ϵ��
				��Ӯ���˻��ĸ߶���ͬ��	
				<br>&nbsp;&nbsp;&nbsp;&nbsp;�ڸ�������Ŀǰ��Ҫ���¸�����Ϸ�㿨�Ļ���������ҵ�񣬲�������Ϊ������������Ӫ���ṩһ����ȫ���ȶ�����ݡ�������API�㿨���Ľӿ�ƽ̨������������ʵ���Ϸ��Ӫ�̣�
				����������Ӫ�̣����ǽ�����߳ϵ�̬��Ϊ���ṩ�����ʵĵ㿨���ļ����շ���<br><br>
                            <span class="sp">��������޶�������ʵ��˫Ӯ</span><br><br>
                            <span class="sp">�����г��������ָ��</span><br><br>
                            <span class="sp">�����Ƚ���չ�����ں���</span><br><br>
                            <span class="sp">��������Э������ֵķ���֧��</span><br> <br>
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
