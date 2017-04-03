<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index2.aspx.cs" Inherits="viviAPI.WebUI7uka.Index2" %>
<%@ Register Src="head.ascx" TagName="head" TagPrefix="uc1" %>
<%@ Register Src="foot.ascx" TagName="foot" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <title>����֧��-������֧�����򵥣�</title>
    <meta name="KeyWords" content="API֧��,֧���ӿ�,�㿨����,�㿨����,��Ϸ֧�� <%=KeyWords%>" />
    <meta name="description" content="����֧����ȫ�����ȵ���Ϸ�㿨����API�ӿ��ṩ�̣�ӵ�ж����ȶ�����Ϸ֧�����սӿ�����������ʢ�󡢿��������ס����Ρ���ѶQ�ҿ�����������;����ɽ�������һ��ͨ����������������Ϸƽ̨������רҵ�ṩ��ȫ�ȶ���Ч����ϷAPI֧�����սӿ�ƽ̨��������Ӫ��վ����Ϸ����Ҫ������顣<%=Description%>" />
    <meta name="author" content="����֧��">
    <link rel="shortcut icon" href="favicon.ico" />
    <link href="web_css/layout2.css" rel="stylesheet" type="text/css" media="all" />
    <link href="web_css/global2.css" rel="stylesheet" type="text/css" media="all" />
    <!-- �ڸ��������°汾 -->
    <link rel="stylesheet" href="css_demo/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="css_demo/bootstrap-dialog.css" />
    <link rel="stylesheet" type="text/css" media="all" href="web_css/zi.css" />
    <link rel="stylesheet" href="css_demo/index.css" />
    <link rel="stylesheet"  type="text/css" media="all" href="web_css/login.css" />
    <link type="text/css" href="web_css/contact_new.css" rel="stylesheet" />
    <link href="web_css/parallax_classic_withoutParallaxEffect.css" rel="stylesheet" type="text/css" />
    <!-- end -->

    <script language="JavaScript" type="text/javascript" src="web_js/jquery.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="web_js/common.min.js"></script>

    <!--{JS}-->
    <link href="web_css/client.css" rel="stylesheet" type="text/css"/>
    <link rel="stylesheet" href="web_js/easydialog.css" />
    <style type="text/css">
        .als-prev
        {
            background-image: url(images/lknavleft.png);
        }
        .als-prev:hover
        {
            background-image: url(images/lknavleft1.png);
        }
        .als-next
        {
            background-image: url(images/lknavright.png);
        }
        .als-next:hover
        {
            background-image: url(images/lknavright1.png);
        }
    </style>

</head>
<body>
    <uc1:head ID="header1" runat="server" showtype="news" />
    <div class="clear">
    </div>
    <div id="page">
        <!-- �������ѭ�� -->
        <script language="JavaScript" type="text/javascript" src="web_js/jquery.alsEN-1.0.1.min.js"></script>
        <script language="JavaScript" type="text/javascript" src="web_js/index.js"></script>
        <div class="banner">
            <!---ȫ����濪ʼ--->
            <div class="header-content home">
                <a href="/index.html" target="_blank">
                    <div style="background-image: url(images/banner.jpg); background-position: center;height: 360px; width: 100%;">
                    </div>
                </a>
            </div>
            <!---ȫ��������--->
        </div>
        <div id="page">
            <div class="w-1200" style="padding: 0px 0;">
                <div class="clear">
                </div>
                <div class="boxes">
                    <div class="indexpro_block">
                        <div class="item hover-text" style="background-color: rgb(255, 255, 255);">
                            <span class="header-image wp-cs"></span>
                            <div class="item-content">
                                <h2>
                                    ���ٶԽ�</h2>
                                <ul>
                                    <li>API �����ӿڣ���������һ�뼱������</li>
                                    <li>������̨�ύ����������ͨ���޷�Խ�</li>
                                    <li>�࿨ͨ��ѡ�񣬿��ٰ�ȫ�ȶ�����ƽ̨</li>
                                </ul>
                                <div class="info-box">
                                    <p>
                                        ƽ̨�ṩAPI�����ӿںͺ�̨���������ӿڣ���Ϸ��/�绰���������Ľӿڣ�10�뼫�����ģ������ӪЧ�ʣ�������ʵͣ���������ռ䡣</p>
                                </div>
                            </div>
                            <div class="item-footer">
                                <a href="/product.aspx" class="btn mm-btn-success">֧�ֿ��� &gt;</a></div>
                        </div>
                    </div>
                    <div class="indexpro_block m-l-r-15">
                        <div class="item hover-text" style="background-color: rgb(255, 255, 255);">
                            <span class="header-image vps-cs"></span>
                            <div class="item-content">
                                <h2>
                                    �ʽ����</h2>
                                <ul>
                                    <li>����޶�����������˫��������Ӯƽ̨</li>
                                    <li>�ʽ���ٻ��������伴�ᣬ�ʽ������"</li>
                                    <li>���漱�ٴ������ʽ�һ�뵽�˰�ȫ�ȶ�</li>
                                </ul>
                                <div class="info-box">
                                    <p>
                                        ����֧���ӿڣ�ҵ�ڷ��������ߣ�ά���̻�������󻯣�7*24Сʱȫ����������޶�������ʵ��˫Ӯ����֤�ͻ��Ƚ���չ�����ں�����</p>
                                </div>
                            </div>
                            <div class="item-footer">
                                <a href="/Regedit.aspx" class="btn mm-btn-success">ע���̻� &gt;</a></div>
                        </div>
                    </div>
                    <div class="indexpro_block">
                        <div class="item hover-text" style="background-color: rgb(255, 255, 255); color: rgb(51, 51, 51);">
                            <span class="header-image mng-cs"></span>
                            <div class="item-content">
                                <h2>
                                    ��ȫ�ȶ�</h2>
                                <ul style="display: block;">
                                    <li>ȫ�̼����������ʽ���ٻ�����ȫ����</li>
                                    <li>ϵͳ��ȫ�ȶ���7X24Сʱ�ͷ�ά��֧��</li>
                                    <li>�ʽ�ȫ��֤��ȫ���ʽ�ȫ��ܱ�֤</li>
                                </ul>
                                <div class="info-box" style="display: none;">
                                    <p>
                                        ʹ�ù������Ƚ�������ϵͳ����֤�ͻ��ʽ�ȫ��ȫ��365��7*24Сʱ����������ȫ��λ�ĵ㿨��������ʹ�����ϲ�����һ��"����"�����������������棡Ϊ�������û����õ�����㿨����ƽ̨��</p>
                                </div>
                            </div>
                            <div class="item-footer">
                                <a href="/contact.aspx" class="btn mm-btn-success">�ͷ����� &gt;</a></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="bg-banner-1" style="margin-top: 15px;">
                <div class="w-1200 advantage_">
                    <ul style="margin-bottom: 30px;">
                        <li>
                            <img src="images/srz.png" width="80" height="79">
                            <p>
                                �����������ֿ����׷���ƽ̨
                            </p>
                        </li>
                        <li>
                            <img src="images/1.png" width="80" height="79">
                            <p>
                                �������г��κ�һ�ҵĻ�������
                            </p>
                        </li>
                        <li>
                            <img src="images/cc.png" width="80" height="79">
                            <p>
                                ����������Ŷ�
                            </p>
                        </li>
                        <div class="clear">
                        </div>
                    </ul>
                    <ul>
                        <li>
                            <img src="images/3.png" width="80" height="79">
                            <p>
                                ����������Ŷ�
                            </p>
                        </li>
                        <li>
                            <img src="images/big.png" width="80" height="79">
                            <p>
                                ���ĵĿͷ��Ŷ�
                            </p>
                        </li>
                        <li>
                            <img src="images/fj.png" width="80" height="79">
                            <p>
                                �����ļ����Ŷ�
                            </p>
                        </li>
                        <div class="clear">
                        </div>
                    </ul>
                </div>
            </div>
            <div class="news1">
                <div class="overlay">
                </div>
                <div class="w-1200">
                    <div class="row">
                        <div class="col-md-6 fl-left">
                            <div class="agent-title-new">
                                <h4>
                                    ҵ������</h4>
                            </div>
                            <asp:Repeater ID="rpgg" runat="server">
                                <ItemTemplate>
                                    <div class="first_news">
                                        <a href="#" class="activity_image fl">
                                            <img src="images/g<%# Getim() %>.jpg" alt="" height="90" width="140"></a>
                                        <p>
                                            <%# NoHTML(Eval("newscontent").ToString())%></p>
                                        <a target="_blank" href="/view<%#Eval("newsid")%>.aspx" class="x_blue">�鿴����&gt;&gt;</a>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="col-md-5">
                            <div class="agent-title-new">
                                <h4>
                                    ���ι���</h4>
                            </div>
                            <div class="feature_diary">
                                <ul>
                                    <asp:Repeater ID="rpnews" runat="server">
                                        <ItemTemplate>
                                            <li><a href="/view<%#Eval("newsid")%>.aspx" title="<%#Eval("newstitle")%>"
                                                target="_blank" style="color: #FE4003">
                                                <%#Eval("newstitle")%></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="partners">
                <div class="partners-bgarea-semitransparent">
                    <div class="w-1200">
                        <div class="reference-box">
                            <div class="agent-title-new">
                                <h4>
                                    ����֧���������</h4>
                            </div>
                            <div class="partners_logos">
                                <div id="partners_section" class="als-container">
                                    <a class="als-prev" href="#" onclick="return false" data-id="als-prev_0">&nbsp;<span
                                        class="glyphicon glyphicon-chevron-left"></span>&nbsp;</a>
                                    <div class="partners_content als-viewport" id="als-viewport_0" style="width: 1120px;
                                        height: 70px;">
                                        <ul class="als-wrapper" id="als-wrapper_0" style="width: 4340px; height: 70px;">
                                            <li class="als-item" id="als-item_0_0"><a href="javascript:void(0);" class="link1"></a>
                                            </li>
                                            <li class="als-item" id="als-item_0_1"><a href="javascript:void(0);" class="link2"></a>
                                            </li>
                                            <li class="als-item" id="als-item_0_2"><a href="javascript:void(0);" class="link3"></a>
                                            </li>
                                            <li class="als-item" id="als-item_0_3"><a href="javascript:void(0);" class="link4"></a>
                                            </li>
                                            <li class="als-item" id="als-item_0_4"><a href="javascript:void(0);" class="link5"></a>
                                            </li>
                                            <li class="als-item" id="als-item_0_5"><a href="javascript:void(0);" class="link6"></a>
                                            </li>
                                            <li class="als-item" id="als-item_0_6"><a href="javascript:void(0);" class="link7"></a>
                                            </li>
                                            <li class="als-item" id="als-item_0_7"><a href="javascript:void(0);" class="link8"></a>
                                            </li>
                                            <li class="als-item" id="als-item_0_8"><a href="javascript:void(0);" class="link9"></a>
                                            </li>
                                            <li class="als-item" id="als-item_0_9"><a href="javascript:void(0);" class="link10">
                                            </a></li>
                                            <li class="als-item" id="als-item_0_10"><a href="javascript:void(0);" class="link11">
                                            </a></li>
                                            <li class="als-item" id="als-item_0_11"><a href="javascript:void(0);" class="link12">
                                            </a></li>
                                            <li class="als-item" id="als-item_0_12"><a href="javascript:void(0);" class="link13">
                                            </a></li>
                                            <li class="als-item" id="als-item_0_13"><a href="javascript:void(0);" class="link14">
                                            </a></li>
                                            <li class="als-item" id="als-item_0_14"><a href="javascript:void(0);" class="link15">
                                            </a></li>
                                            <li class="als-item" id="als-item_0_15"><a href="javascript:void(0);" class="link16">
                                            </a></li>
                                        </ul>
                                    </div>
                                    <a class="als-next" href="#" onclick="return false" data-id="als-next_0">&nbsp;<span
                                        class="glyphicon glyphicon-chevron-right"></span>&nbsp;</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <uc1:foot ID="footer1" runat="server" showtype="news" />
<script type="text/javascript">
$(document).ready(function(){

	$("#floatShow").bind("click",function(){
	
		$("#onlineService").animate({width:"show", opacity:"show"}, "normal" ,function(){
			$("#onlineService").show();
		});
		
		$("#floatShow").attr("style","display:none");
		$("#floatHide").attr("style","display:block");
		
		return false;
	});
	
	$("#online_qq_layer").bind("click",function(){
	
		$("#onlineService").animate({width:"hide", opacity:"hide"}, "normal" ,function(){
			$("#onlineService").hide();
		});
		
		$("#floatShow").attr("style","display:block");
		$("#floatHide").attr("style","display:none");
		
		return false;
	});
  
});
</script>


<style type="text/css">
*{margin:0;padding:0;list-style-type:none;}
a,img{border:0;}
#online_qq_tab a,.onlineMenu h3,.onlineMenu li.tli,.newpage{background:url(images/float_s.gif) no-repeat;}
#onlineService,.onlineMenu,.btmbg{background:url(images/float_bg.gif) no-repeat;}
#online_qq_layer{z-index:9999;position:fixed;right:0px;top:0;margin:150px 0 0 0;}
*html,*html body{background-image:url(about:blank);background-attachment:fixed;}
*html #online_qq_layer{position:absolute;top:expression(eval(document.documentElement.scrollTop));}
#online_qq_tab{width:28px;float:left;margin:120px 0 0 0;position:relative;z-index:9;}
#online_qq_tab a{display:block;height:118px;line-height:999em;overflow:hidden;}
#online_qq_tab a#floatShow{background-position:-30px -374px;}
#online_qq_tab a#floatHide{background-position:0 -374px;}
#onlineService{display:inline;margin-left:-1px;float:left;width:130px;display:none;background-position:0 0;padding:10px 0 0 0;}
.onlineMenu{background-position:-262px 0;background-repeat:repeat-y;padding:0 15px;}
.onlineMenu h3{height:36px;line-height:999em;overflow:hidden;border-bottom:solid 1px #ACE5F9;}
.onlineMenu h3.tQQ{background-position:0 10px;}
.onlineMenu h3.tele{background-position:0 -47px;}
.onlineMenu li{height:36px;line-height:36px;border-bottom:solid 1px #E6E5E4;text-align:center;}
.onlineMenu li.tli{padding:0 0 0 28px;font-size:12px;text-align:left;}
.onlineMenu li.zixun{background-position:0px -131px;}
.onlineMenu li.fufei{background-position:0px -190px;}
.onlineMenu li.phone{background-position:0px -244px;}
.onlineMenu li a.newpage{display:block;height:36px;line-height:999em;overflow:hidden;background-position:5px -100px;}
.onlineMenu li img{margin:8px 0 0 0;}
.onlineMenu li.last{border:0;}
.wyzx{padding:8px 0 0 5px;height:57px;overflow:hidden;background:url(images/webZx_bg.jpg) no-repeat;}
.btmbg{height:12px;overflow:hidden;background-position:-131px 0;}
</style>

</body>
</html>
