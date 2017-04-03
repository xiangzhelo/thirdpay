<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="import.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.behalf.Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/usermodule/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/behalf/index.aspx'">对私代发</a>
        &nbsp;&gt;&nbsp; <span>代发上传</span>
    </div>
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            文件上传</h2>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="line-height: 26px;">
            <tr>
                <td colspan="2" class="line_01">
                </td>
            </tr>
            <tr>
                <td align="right" class="line_01">
                    代发需知：
                </td>
                <td class="line_01">
                    1、请参照代发模板文件，按格式填写本批次的代发明细信息。 <a href="../download/templetfile.xlsx">下载</a>代发模板文件
                    <br />
                    2、目前支持的收款银行有16家，分别为：工商银行、农业银行、建设银行、交通银行、招商银行、中国银行、邮政储蓄银行、民生银行、华夏银 行、兴业银行、广发银行、浦发银行、光大银行、中信银行、平安银行、杭州银行。
                    <br />
                    3、关于单笔代发限额：工、农、建、交四家收款银行最高20万元，其他银行最高5万元。
                    <br />
                    4、按照反洗钱规定，代发金额超过1万元的，请自行保存代发目标用户的真实有效身份信息，要做到随时可查。代发金额超过5万元的，请向贝付 传真相关收款人的身份证复印件，并写明所属代发批次号，传真号码：0571-86584668。工作人员据此审核这些代发明细。
                    <br />
                    5、账户余额应>=代发金额合计+代发手续费合计。 查看当前费率设置
                </td>
            </tr>
            <tr>
                <td z align="right" class="line_01">
                </td>
                <td align="left" class="line_01">
                    <asp:CheckBox ID="cbx_sure" runat="server" Text="以上内容我已知悉" AutoPostBack="true" OnCheckedChanged="cbx_sure_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                </td>
                <td style="width: 75%" align="left" class="line_01">
                    <asp:FileUpload ID="file_data" runat="server" class="mutitxt_03" Width="80%" />
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                </td>
                <td style="width: 75%" class="line_01">
                    <asp:Button ID="btnupload" runat="server" Text="确定上传" CssClass="btn btn-primary" OnClick="btnupload_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
