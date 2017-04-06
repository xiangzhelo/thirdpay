<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderview.aspx.cs" Inherits="viviAPI.WebUI2015.usermodule.quota.orderview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0px;
            font-family: tahoma,宋体,fantasy;
            font-size: 12px;
        }
        input
        {
            border: 1px solid #ccc;
            font-family: tahoma,宋体,fantasy;
            color: #333333;
            background: #FFF;
            font-size: 11px;
            height: 16px;
            line-height: 16px;
            padding-left: 3px;
        }
        .button
        {
            font-size: 9pt;
            height: 20px;
            border-width: 1px;
            cursor: hand;
            background: url(/usermodule/images/cxbg.gif);
        }
        .Mir_List
        {
            width: 100%;
            height: auto;
            margin-top: 5px;
        }
        .Mir_List .M_Item
        {
            width: 100%;
            height: 25px;
            overflow: hidden;
            background: #F5F5F5;
        }
        .Mir_List .Pub
        {
            background: #ffffff;
        }
        .Mir_List .Head
        {
            color: #fff;
            font-weight: bold;
        }
        .Mir_List .M_Item ul
        {
            padding: 0px;
            margin: 0px;
        }
        .Mir_List li
        {
            margin: 2px;
            height: 25px;
            line-height: 25px;
            float: left;
            list-style-type: none;
        }
        .Mir_List .li_0
        {
            text-align: right;
            width: 82px;
            overflow: hidden;
        }
        .Mir_List .li_1
        {
            overflow: hidden;
        }
        .Mir_List .li_2
        {
            text-align: right;
            width: 100px;
            overflow: hidden;
        }
        .Mir_List .li_3
        {
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class='Mir_List'>
        <div class='M_Item Pub'>
            <ul>
                <li class='li_0'>系统订单号：</li>
                <li class='li_1'>
                    <asp:Label ID="lblorderid" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>转换类型：</li>
                <li class='li_3'>
                    <asp:Label ID="lblquotatype" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item'>
            <ul>
                <li class='li_0'>转换额度：</li>
                <li class='li_1'>
                    <asp:Label ID="lblquotaValue" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>消费金额：</li>
                <li class='li_3'>
                    <asp:Label ID="lblcharge" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item Pub'>
            <ul>
                <li class='li_0'>转换费率：</li>
                <li class='li_1'>
                    <asp:Label ID="lblpayrate" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>操作时间：</li>
                <li class='li_3'>
                    <asp:Label ID="lbladdtime" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item'>
            <ul>
                <li class='li_0'>操作ip：</li>
                <li class='li_1'>
                    <asp:Label ID="lblclientip" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>