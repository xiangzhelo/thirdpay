<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quotarecharge.aspx.cs" Inherits="viviAPI.WebUI2015.usermodule.quota.quotarecharge" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet"  href="../css/yifu.css" />
    <script src="../js/jquery-1.8.1.min.js"></script>
	<script type="text/javascript">
		var payrate=[];
		function checkForm(){
			var quotaValue=$("#quotaValue").val();
			var quota_type=$("#quota_type").val();
			var password=$("#password").val();
			if(quotaValue==null||quotaValue==""||!(quotaValue>0)){
				$(".red").text("输入额度错误！！");
				return;
			}
			if(quota_type==null||quota_type==""){
				$(".red").text("请选择类型！！");
				return;
			}
			if(password==null||password==""){
				$(".red").text("请输入密码！！");
				return;
			}
			$(".red").text("");
			$('#form1').submit();
		}
		function resetForm(){
			$("#quotaValue").val("0");
			$("#password").val("");
			$("#quota_type").val($("#quota_type").children("option").first().val());
			getMoney();
		}
		$.ajax({
		    url: "/usermodule/quota/GetQuotaInfo.ashx",
		    async:"false",
		    type:"POST",
		    dataType:"json",
		    data: { get: "payrate" },
		    success:function(data){
		        //data = JSON.parse(data);
				var html="";
				var d=data.Table;
				$.each(d,function(k,v){
					payrate[v.quota_type]=v.payrate;
					html+='<option value="'+v.quota_type+'">'+v.name+'</option>';
				});
				$("#quota_type").html(html);
				//console.log(payrate);
		    }
		});
		$.ajax({
		    url: "/usermodule/quota/GetQuotaInfo.ashx",
		    async: "false",
		    type: "POST",
		    dataType: "json",
		    data: { get: "password2" },
		    success: function (data) {
		        if (data == "0") {
		            $("#password2div").html("您尚未设置提现密码,暂时无法提现。<a href=\"/usermodule/safety/cashpass.aspx\">点击设置</a>");
		        }
		    }
		});
		getMoney();
		function getMoney(){
		    $("#money").text((payrate[$("#quota_type").val()] * $("#quotaValue").val()).toFixed(2));
		}
	</script>
</head>
<body>
	<form name="form1" method="post" action="quotaPlaceOrder.aspx" id="form1">
		<div class="page">
			<div id="panel-head">
				<h2>额度转换</h2>
			</div>
			<br />
            <div class="prompt"><label>商户名称：</label><span><%=hostname %></span></div>
            <div class="prompt"><label>可用余额：</label><span><%=litAnableAmount %></span></div>
			<div class="prompt"><label>所需金额：</label><span id="money">0.00</span>元</div>
			<div class="prompt"><label>转换额度：</label><input type="text" value="0" maxlength="8" id="quotaValue" name="quotaValue" class="txt" onchange="getMoney();" /></div>
			<div class="prompt"><label>转换类型：</label><select class="txt" id="quota_type" name="quota_type" onchange="getMoney();" ><option value="1">ag</option><option value="2">og</option><option value="3">hg</option></select></div>
			<div class="prompt" id="password2div"><label>提现密码：</label><input type="password" value="" id="password" name="password" class="txt" /></div>
			<span class="red" style="color:red;"></span>
		</div>
	</form>
	<input type="submit" name="btnpost" value="提交" class="btn btn-primary mybtn" onclick="checkForm();"/>
	<button class="btn btn-primary mybtn" onclick="resetForm();">重置</button>
</body>
</html>
