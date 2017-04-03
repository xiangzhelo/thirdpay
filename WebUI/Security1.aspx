<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Security1.aspx.cs" Inherits="viviAPI.WebUI2015.Security1" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>��ȫ���� - <%=SiteName%></title>
<meta name="KeyWords" content="API,֧���ӿ�,�㿨����,��Ϸ֧�� <%=KeyWords%>" />
<meta name="description" content="�ڸ���������ȫ�����ȵ���Ϸ�㿨����API�ӿ��ṩ�̣�ӵ�ж����ȶ�����Ϸ֧�����սӿ�����������ʢ�󡢿��������ס����Ρ���ѶQ�ҿ�����������;����ɽ�������һ��ͨ����������������Ϸƽ̨������רҵ�ṩ��ȫ�ȶ���Ч����ϷAPI֧�����սӿ�ƽ̨��������Ӫ��վ����Ϸ����Ҫ������顣<%=Description%>" />
<link href="/themes/css/skin.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/themes/js/jquery.min.js"></script>
<script type="text/javascript" src="/themes/js/jquery.easing.1.3.js"></script>
<script src="/themes/js/ainatec.js" type="text/javascript"></script>
</head>
<body>
<script src="/themes/js/sub.js" type="text/javascript"></script>
<uc1:header ID="header1" runat="server" showtype="contact" />
<div class="sub_top">
	<h2><img src="/themes/images/news_title.png" width="200" height="50" border="0" alt=""></h2>
	<div class="sub_nav clearbox">
		<ul >
			<li><a href="/Product.aspx" title="֧�ֿ���">֧�ֿ���</a></li><li class="active"><a href="/Security.aspx" title="��ȫ����">��ȫ����</a></li><li><a href="/news.aspx?tid=1" title="���Ŷ�̬">���Ŷ�̬</a></li><li><a href="/solution.aspx" title="��������">��������</a></li><li><a href="/contact.aspx" title="��ϵ����">��ϵ����</a></li>
        </ul>
	</div>
</div>
<div style=" background:#31C3D3; height:330px;">
    <div class="w_anquan"><img style=" position:relative;top: -1px;" src="/themes/images/safe_top_bg.png" alt="��ȫ���ױ�����ϵ" /></div>
</div>

    <div class="security_bg">
        <div class="w_anquan">
            <div class="security_title_bg"></div>
            <p class="security_title_p" >���뱣��</p>
            <p class="security_p">���뱣��������Ҫ���������һ�ʱ�İ�ȫ��֤���ƣ�����������󶨡�<br/>
            �󶨷�ʽ��<br/>
            1����¼�û�ϵͳ > ��ȫ����<br/>
            2��ѡ�����Ⲣ��д��<br/>
            3����ɰ󶨡�
</p> 
<div class="security_right"><img src="/themes/images/security_pic01.png" alt="�㸶��Ͷ����" /></div>

        <div class="cl"></div>
        <div style=" margin:80px 0 0 0">
        <div class="security_title_bg"></div>
            <p class="security_title_p" id="safe3">�����</p>
            <div class="security_right" style=" margin:60px 0 0 0"><img src="/themes/images/security_pic02.png" alt="���տ���" /></div>
            <p class="security_p" style=" margin:95px 0 0 80px">�������Ҫ�����޸Ľ�������͸������п������˵���Ҫ�������Ƿǳ���Ҫ�İ�ȫ���ϻ��ƣ�����������󶨡�<br/>
            �󶨷�ʽ��<br/>
            1��ע��ʱʹ������ע�ᣬע����ɺ󼴿ɰ󶨣�<br/>
             2����ͨע����û�����д��QQ����Ĭ��Ϊ��QQ�˺ŵ����䣬��ע��ʱ�����õ�QQ���롣
</p> 


        <div class="cl"></div>
        </div>
            <div style=" margin:80px 0 0 0">
         <div class="security_title_bg"></div>
            <p class="security_title_p" >��������</p>
            <p class="security_p">������������������п����������ʱ����Ķ���������֤���ƣ��Ƿǳ���Ҫ�İ�ȫ������ʩ���û��״ε�¼ϵͳʱ��Ҫ�������á���������Ҫ���õĹ��ڼ򵥣�����ʹ����ĸ+����+������ɵġ�</p> 
<div class="security_right"><img src="/themes/images/security_pic03.png" alt="��Ϣ�渶" /></div>

        <div class="cl"></div>
        </div>



       </div>
    </div>





<uc1:footer ID="footer1" runat="server" showtype="news" />
</body>
</html>
