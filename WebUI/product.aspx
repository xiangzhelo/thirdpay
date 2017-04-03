<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="viviAPI.WebUI2015.Product" %>

<%@ Register Src="head.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�ڸ�������������Ŀ����_<%=SiteName%>_�㿨����_API֧���ӿ�_��Ϸ�㿨����_��Ϸ֧��ƽ̨�ӿ��ṩ��</title>
    <meta name="KeyWords" content="API,֧���ӿ�,�㿨����,��Ϸ֧�� <%=KeyWords%>" />
    <meta name="description" content="�ڸ���������ȫ�����ȵ���Ϸ�㿨����API�ӿ��ṩ�̣�ӵ�ж����ȶ�����Ϸ֧�����սӿ�����������ʢ�󡢿��������ס����Ρ���ѶQ�ҿ�����������;����ɽ�������һ��ͨ����������������Ϸƽ̨������רҵ�ṩ��ȫ�ȶ���Ч����ϷAPI֧�����սӿ�ƽ̨��������Ӫ��վ����Ϸ����Ҫ������顣<%=Description%>" />
    <!-- ͷ��β�� -->
    <link rel="shortcut icon" href="ico/favicon.ico" />
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/index.css" />
    <link rel="stylesheet" href="web_js/easydialog.css" />

    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>

    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js" async="false"></script>

    <!-- end -->
</head>
<body>
    <form id="form1" runat="server">
    <uc1:header ID="header1" runat="server" showtype="product" />
    <div class="clear">
    </div>
    <div id="page">

        <script language="JavaScript" type="text/javascript" src="web_js/tooltip_ext.js"></script>

        <link type="text/css" rel="stylesheet" href="web_css/index.css" media="all">
        <div class="wrapsemibox about-box-mm">
            <div class="semiboxshadow">
                <img alt="" class="semiboxshadow_bg" src="images/shp.png">
            </div>
            <!--�м䲿�ֿ�ʼ-->
            <div class="w-1200">
                <!--left -->
                <div class="about-span1" style="height: 500px;">
                    <div class="about-menu">
                        <ul>
                            <li>
                                <dd class="about-menu-current">
                                    <a href="/product.html">��Ʒ����</a></dd>
                            </li>
                            <li>
                                <dd>
                                    <a href="/product.html?#cptd">��Ʒ�ص�</a></dd>
                            </li>
                            <li>
                                <dd>
                                    <a href="/product.html?#ywkt">ҵ��ͨ������</a></dd>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--right scroll-->
                <div class="about-span2 y-last managebar">
                    <div class="aboutus" style="border: none;">
                        <p style="line-height: 35px; border: none;">
                            <div style="width: 684px; height: 124px; margin-bottom: 5px;">
                                <div style="width: 684px; height: 62px; background-image: url(/images/cpjs_02.jpg);">
                                </div>
                                <div style="width: 684px; height: 52px; background-color: #F6F6F6;">
                                    <div style="width: 10px; height: 52px; background-image: url(/images/cpjs_03.jpg);
                                        float: left;">
                                    </div>
                                    <div style="width: 674px; height: 52px; float: right;">
                                        <div style="width: 668px; height: 52px; background-color: #F6F6F6; float: left; text-align: left;
                                            line-height: 22px; color: #666;">
                                            &nbsp;����ֵ�����չ���һ��������֤�ͽ����û��ύ�ĳ�ֵ�����ֻ���ֵ������Ϸ��ֵ�����������û�ʹ�ø��ַ����п����ѷ�ʽ�����������ѻ���ڸ��������˻���ֵ��</div>
                                        <div style="width: 6px; height: 52px; background-image: url(/images/cpjs_04.jpg);
                                            float: right;">
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 684px; height: 10px; background-image: url(/images/cpjs_01.jpg);">
                                </div>
                            </div>
                            <div style="width: 684px; height: 552px; margin-bottom: 15px;">
                                <table style="width: 684; text-align: center">
                                    <tbody>
                                        <tr>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/sh.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/QQ.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/wm.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/wy.jpg">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;�Ѻ�һ��ͨ
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;��ѶQQ
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;����һ��ͨ
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;����һ��ͨ
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/sd.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/gy.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/jy.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/zt.jpg">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;ʢ��һ��ͨ
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;����һ��ͨ
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;����һ��ͨ
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;��;һ��ͨ
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/lt.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/szx.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/szx.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/dx.jpg">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;��ͨ��ֵ��
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;������ȫ����
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;�����еط���
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;���ſ�һ��ͨ
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/jw.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/zy.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                <img class="img" src="images/yx/TianHong.jpg">
                                            </td>
                                            <td width="171px" height="96px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;����һ��ͨ
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;����һ��ͨ
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;���һ��ͨ
                                            </td>
                                            <td width="171px" height="32pxpx">
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </p>
                        <a name="cptd"></a>
                        <p style="line-height: 35px; border: none;">
                            <div style="display: block; width: 684px; height: 146px; margin-bottom: 21px;">
                                <div style="width: 684px; height: 61px; background-image: url(/images/cptd_01.jpg);">
                                </div>
                                <div style="width: 684px; height: 86px; background-color: #F6F6F6;">
                                    <div style="width: 17px; height: 86px; float: left; background-image: url(/images/cptd_02.jpg);">
                                    </div>
                                    <div style="width: 667px; height: 86px; float: right;">
                                        <div style="width: 656px; height: 86px; float: left; text-align: left; color: #666;
                                            line-height: 22px;">
                                            1���û�ֻ�����������еĳ�ֵ���ϱ��������кź͹ο��Ŀ�����Ϳ�����ɶһ��������������п����ż����룬��ȫ�Ըߣ������������������п���һ����ʹ�ã��������С�Ƶ�����ѵķ�ʽ��<br>
                                            2���ڸ�������������߳�ֵ�����ѹ��ܣ�Ϊ�̼��������ൣ���������Ѱ�ȫ��Ⱥ�������п���Ⱥ��С���Ƶ����Ⱥ����Ǳ�ڵ�����Ⱥ�塣</div>
                                        <div style="width: 11px; height: 86px; float: right; background-image: url(/images/cptd_03.jpg);">
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 684px; height: 10px; background-image: url(/images/cptd_04.jpg);">
                                </div>
                            </div>
                        </p>
                        <a name="ywkt"></a>
                        <p style="line-height: 35px; border: none;">
                            <div style="display: block; width: 684px; height: 193px;">
                                <div style="width: 684px; height: 61px; background-image: url(/images/ywkt_01.jpg);">
                                </div>
                                <div style="width: 684px; height: 86px; background-color: #F6F6F6;">
                                    <div style="width: 17px; height: 86px; float: left; background-image: url(/images/cptd_02.jpg);">
                                    </div>
                                    <div style="width: 667px; height: 86px; float: right;">
                                        <div style="width: 656px; height: 86px; float: left; text-align: left; color: #666;
                                            line-height: 22px;">
                                            1�� ����/��ҵע������������Ա��ϵ�������˽���������󣬰�����˳��ǩԼ����ͨ����<br>
                                            2���ӿڽ��������ṩ��ҵ����Ӫҵִ�ա���֯��������֤���������֤��ӡ����һ�ݣ�<br>
                                            3��һ��һ�ķ�����Ա��˺��������Ϻ�����ǩ������Э�飻<br>
                                            4������Э��ǩ������1-5���������ڣ������ṩʹ���˺ţ�<br>
                                        </div>
                                        <div style="width: 11px; height: 86px; float: right; background-image: url(/images/cptd_03.jpg);">
                                        </div>
                                    </div>
                                </div>
                                <div style="width: 684px; height: 10px; background-image: url(/images/cptd_04.jpg);">
                                </div>
                            </div>
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
    <uc1:footer ID="footer1" runat="server" />
    </form>
</body>
</html>
