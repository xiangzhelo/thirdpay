<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="znexcha.aspx.cs" Inherits="viviAPI.WebUI7uka.usermodule.order.znexcha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />--%>
    <script src="/js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="/js/xiaoka_new.js?=1"></script>--%>
    <style type="text/css"> 
.x_radio{float:left;width:86px;}
.x_radio i{line-height:35px; font-style:normal}
.x_radio input{float:left}
.x_radio label{line-height:25px;height:25px; display:block;float:left;padding:0;width:auto}

/* 通用样式 */
body { font-size: 12px; font-family: Verdana,Helvetica,Sans-Serif; background-color: #fff; padding: 0px; margin: 0px;overflow:scroll;overflow-x:hidden; }
ul,li,div,img,p,input{padding: 0;margin: 0;}
img,input{ border: none; }
ul li{list-style: none;}
a:link, a:visited {text-decoration: none;}
.clear{clear:both;}
.wrapper{width: 100%;}
.wrap{width: 1000px;margin: 0 auto;}
.col_fff{color:#fff;}
.col_534F69{color:#534F69;}
.col_bg_fff{color:#fff;}
.col_C59C05{color: #C59C05;}
.col_FF1600{color: #FF1600;}
.col_595757{color: #595757;}
.col_F19343{color:#F19343;}
.col_border_E2E2E2{border: 1px solid #E2E2E2;}
.float_left{ float:left;margin-left:7px;}
.float_right{float: right;}


/* 右内容 */
.right_center{float: right; width: 825px;height: 100%;}
.right_center_on{padding:15px 10px;overflow: hidden;}
.center_on_left{width:440px;height:260px; float: left; padding:10px;font-size: 16px;font-family: "微软雅黑";}
.left_text_title ul{height: 27px;text-align: center;  border-top:solid 1px #E2E2E2; border-left:solid 1px #E2E2E2; border-right:solid 1px #E2E2E2;}
.left_text_title ul li{margin-right: 1px; float: left;display: inline-block;line-height: 27px;border-right:solid 1px #E2E2E2; }
.li_width175{width:173px;}
.li_width73{width:84px;}
.li_width{width:91px;}
.center_on_left_btntext{width:420px;margin:0;height:145px;}
.center_on_left_mz{margin:10px 0 10px 0;}
.center_on_left_mz label{margin-right:5px;}
.center_on_left_mz span{margin-left:10px;}
.center_on_left_btn{margin:10px 0 10px 0;}
.center_on_left_btn label{font-size: 14px;}
.center_on_left_btn input[type="checkbox"]{vertical-align: -15%;}
.sub_btn,.rst_btn{width:65px;height:27px;line-height: 27px;font-size: 12px;cursor:pointer;}
.sub_btn{background:#3fa6e3;margin-left:6px;}
.rst_btn{/*background:#FAC91D;*/background-color:#ff1600;}
.center_on_right{}
.center_on_right_msg{height: 100%;width:162px;}
.center_on_right_msg p{ padding-left:15px;/*color: #fff;*/font-size: 14px;/*background: #FAFAFA;color: #C59C05;*/background-color: #6B6A6A;width:147px;height:27px;border-bottom:1px solid #E2E2E2;line-height: 27px; text-align:left;}
.article{font-size: 12px;text-align:justify;padding:15px 13px;color: #595757;background: #EEEDEC; height:110px;}


.right_center_ud{padding:10px;height: 100%;}
.right_center_ud_title{padding: 10px;overflow: hidden;}
.right_center_ud_title img{width: 30px;height: 45px; float:left;position:relative;top:-10px;}
.right_center_ud_title p{float: right;width: 745px;height: 45px; line-height:18px; margin-left: 5px;}
.right_center_ud_title p a{/*color: #C59C05;*/ color:#FF1600; font-size: 13px;}
.right_center_ud_table{padding:10px;}
.right_center_ud_table table{width: 100%;border-collapse:collapse;}
.right_center_ud_table table th,.right_center_ud_table table td{border: 1px solid #E2E2E2; height:25px; padding:3px;}
.right_center_ud_table table th{background: #FAFAFA;}

.left_text_title_bg{ background:#9F9F9F;color: #fff;}
.left_text_title_bg_two{background: #3FA6E3;color: #fff;}
.left_text_title_bg_s{background: #fff;color:#5B5959;} 
.car_tab{font-size:14px; width:437px;height:22px;}
.car_tab ul{width:auto;}
.car_tab ul li{ float:left; line-height:19px; width:83px;display: inline-block; text-align:center;border-right: 1px solid #fff; padding:2px 2px;}
.input_div{margin:15px 0;}
.input_div label{ margin:0 10px; display:inline-block;}
.input_div input{ width:323px; height:29px; line-height:29px;}
.kazhong{width:88px;min-height:150px;
 text-align:center; float:right;}
.fanhuihome{position: absolute; left:16%;*left:17%; line-height: 50px; font-size: 16px;}


</style>
    <script type="text/javascript" src="/js/app_user_common.js"></script>
    <script type="text/javascript" src="/js/delete.js"></script>
    <script type="text/javascript" src="/js/NewSub.js"></script>
    <script type="text/javascript">
            $(function () {
//                $.ajax({
//                    type: "get",
//                    contentType: "text/html",
//                    url: "/Merchant/Ajax/GetBalance.ashx?t=" + Math.random(),
//                    data: "",
//                    error: function () {
//                        $("#Cbalance").html("提交出现错误");
//                    },
//                    success: function (a) {
//                        if (a != "") {

//                            $("#Cbalance").html(a);
//                        }
//                    }
//                });

                var isAuto = "5788";
                if (isAuto != 1617 && isAuto != 1619) {
                    setInterval("queryOrder(2)", 20000);
                }

                //$(".car_tab ul li a").eq(1).click();
            })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
    </div>
    <div id="list_content">
        <div class="right_center_on">
            <div class="center_on_left col_border_E2E2E2">
                <div class="car_tab">
                    <ul>
                        <li class="left_text_title_bg_two"><a href="#mutil"><span class="col_fff" style="color: white">多卡提交</span></a></li>
                        <li class="left_text_title_bg"><a class="col_fff" href="#sign" style="color: white">单卡提交</a></li>
                        <li style="margin-left: 173px;"></li>
                    </ul>
                </div>
                <!-- 多卡 -->
                <div class="center_on_left_text" id="mutil" style="display: block;">
                    <div class="left_text_title">
                        <ul>
                            <li class="li_width175 left_text_title_bg_s">卡 号</li>
                            <li class="li_width175 left_text_title_bg_s">卡 密</li>
                            <li style="border: 0;" class="li_width73 left_text_title_bg_s">卡 种</li>
                        </ul>
                        <input type="hidden" class="CardRegular" value="^\d{17}$;^\d{18}$;103;移动;1000,500,300,200,100;1;100">
                        <input type="hidden" class="CardRegular" value="^\d{15}$;^\d{19}$;108;联通;100,300,500;2;100">
                        <input type="hidden" class="CardRegular" value="^\d{19}$;^\d{18}$;113;电信;500,300,100,50,30,20,10;3;100">
                        <input type="hidden" class="CardRegular" value="^([Yy]{1}[CcDd]{1}[A-Za-z0-9]{2}\d{11}|[8]\d{15})$;^\d{8}$;210;盛付通;350,300,100;4;100">
                        <input type="hidden" class="CardRegular" value="^[a-xzA-XZ]{1}([A-Za-z0-9]{14})$;^\d{8}$;104;盛大;350,300,100;5;100">
                        <input type="hidden" class="CardRegular" value="^\d{16}$;^\d{16}$;106;骏网;20,30,50,100,300,500;6;100">
                        <input type="hidden" class="CardRegular" value="^\d{10}$ ;^\d{15}$;111;完美;100,50,30,15;7;100">
                        <input type="hidden" class="CardRegular" value="^\d{13}$;^\d{9}$;110;网易;50,30,20,15,10;8;50">
                        <input type="hidden" class="CardRegular" value="^0\d{15}$;^\d{8}$;105;征途;500,468,300,100,60,50,30,25,20,10;9;100">
                        <input type="hidden" class="CardRegular" value="^\d{20}$;^\d{12}$;112;搜狐;100,40,30,15,10,5;10;100">
                        <input type="hidden" class="CardRegular" value="^\d{13}$;^\d{10}$;109;久游;50,30,25,20,10,5;11;50">
                        <input type="hidden" class="CardRegular" value="^([A-Za-z0-9]{20})$;^\d{8}$;115;光宇;100,50,30,20,10;12;100">
                        <input type="hidden" class="CardRegular" value="^\d{15}$;^\d{15}$;117;纵游;100,50,30,15,10;13;100">
                        <input type="hidden" class="CardRegular" value="^\d{15}$;^\d{8}$;118;天下;100,50,30,15;14;100">
                        <input type="hidden" class="CardRegular" value="^\d{9}$;^\d{12}$;107;QQ;200,100,60,30,15,10,5;15;100">
                        <input type="hidden" class="CardRegular" value="^([A-Za-z0-9]{12})$;^\d{15}$;119;天宏;100,50,30,15,10,5;16;100">
                        <textarea style="width: 343px; float: left; font-size: 12px" class="center_on_left_btntext col_border_E2E2E2 jqtransformdone"
                            rows="50" id="cards" name=""></textarea>
                        <div id="cardType" class="col_border_E2E2E2 kazhong" style="font-size: 12px">
                        </div>
                    </div>
                </div>
                <!-- end 多卡 -->
                <!--单卡 -->
                <div style="display: none;" class="center_on_left_btntext col_border_E2E2E2" id="sign">
                    <div class="input_div">
                        <label>
                            卡号：</label><input type="text" class="col_border_E2E2E2 jqtranformdone" id="cardNumber">
                    </div>
                    <div class="input_div">
                        <label>
                            卡密：</label><input type="text" class="col_border_E2E2E2 jqtranformdone" id="cardPassword">
                    </div>
                    <div class="input_div">
                        <label>
                            卡种：</label><span id="singleKZ"></span>
                    </div>
                </div>
                <!-- end 单卡 -->
                <div>
                </div>
                <div id="cardMz" class="center_on_left_mz">
                    <label>
                        面值：</label>
                </div>
                <div class="center_on_left_btn">
                    <input type="hidden" id="cardPar" value="">
                    <input type="button" data-enabled="true" class="sub_btn col_fff jqtransformdone"
                        id="subCards" value="确认提交">
                    <input type="button" data-enabled="true" class="sub_btn col_fff jqtransformdone"
                        id="subCards1" value="暴力提交">
                    <input type="button" class="rst_btn col_fff jqtransformdone" onclick="kmdd()" value="整理卡密">
                    <input type="reset" class="rst_btn col_fff jqtransformdone" value="一键清除">
                    <input id="cleanWord" name="cleanWord" value="" type="hidden">
                    <div style="display: none; color: red" id="wait">
                        正在提交数据</div>
                </div>
            </div>
            <div style="text-align: center" class="center_on_right">
                <div class="float_left">
                    <div style="width: 277px; height: 280px; no-repeat 10px 50px; line-height: 160%;
                        border: 1px solid #e2e2e2; text-align: left" class="b_r">
                        <br>
                        <span style="font-weight: bold;" class="font14 txtc">操作提示：</span>
                        <br>
                        <span class="font14 txtr">输入正确的卡密，系统会自动识别卡的种类 选择正确的面额 确认提交，等待系统返回结果</span>
                        <br>
                        <br>
                        <span style="font-weight: bold;" class="font14 txtc">重要提示：</span>
                        <br>
                        在提交移动、联通卡时不清楚面额的，请一定选择最大面额进行提交，避免造成损失。
                        <br>
                        <br>
                        <span style="font-weight: bold;" class="font14 txtc">温馨提示：</span>
                        <br>
                        如果确定卡号、密码、卡面额输入正确，但系统提示错误，请重新输入提交一次
                    </div>
                </div>
                <div class="float_right">
                </div>
            </div>
        </div>
        <table width="900px" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            id="dataTable" class="table table-bordered table-striped centered dataTable"
            aria-describedby="dataTable_info">
            <!--列标题-->
            <thead>
                <tr role="row">
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        卡号
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        提交金额
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        成功金额
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        订单状态
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        说明
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        提交时间
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        操作
                    </th>
                </tr>
                <tbody id="toporder">
                    <asp:Repeater ID="rptorders" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td height="30" align="center" bgcolor="#FFFFFF">
                                    <%# Eval("cardNo")%>
                                </td>
                                <td height="30" align="center" bgcolor="#FFFFFF">
                                    <%#Eval("refervalue","{0:0.00}")%>
                                </td>
                                <td id="paymoney<%#Eval("ID")%>" height="30" align="center" bgcolor="#FFFFFF">
                                    <%#GetViewSuccessAmt(Eval("status"),Eval("realvalue"))%>
                                </td>
                                <td id="orderzt<%#Eval("ID")%>" height="30" align="center" bgcolor="#FFFFFF">
                                    <%#GetViewStatusName(Eval("status"))%>
                                </td>
                                <td id="errorMsg<%#Eval("ID")%>" height="30" align="center" bgcolor="#FFFFFF">
                                    <%#Eval("msg")%>
                                </td>
                                <td height="30" align="center" bgcolor="#FFFFFF">
                                    <%#Eval("addtime","{0:yyyy-MM-dd HH:mm}")%>
                                </td>
                                <td height="30" align="center" bgcolor="#FFFFFF">
                                    <button class="btn btn-primary" id="sub<%#Eval("ID")%>" style="margin-right: 0" type="button"
                                        onclick="checkflag('<%#Eval("ID")%>')">
                                        刷新</button>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
        </table>
    </div>
    </form>
</body>
</html>
