<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quotalist.aspx.cs" Inherits="viviAPI.WebAdmin.Console.quota.quotalist" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />

    <script src="../js/ControlDate/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css"> table {font-weight: normal;font-size: 12px; line-height: 170%;}
        td{ height: 11px; }
        A:link {color: #02418a;text-decoration: none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <input name="v$id" type="hidden" value="orderquery" />
    <!--�Ҳ�����ʼ-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            ���ת����¼</h2>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <!--������-->
                            &nbsp&nbsp��ʼ��
                            <asp:TextBox ID="sdate" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp��ֹ��
                            <asp:TextBox ID="edate" runat="server" Width="65px"></asp:TextBox>
                        <label>
                            &nbsp;����:
                        <select id="quota_type" runat="server" class="search_txt_01">
                            <option value="0">��������</option>
                            <option value="1">AG���</option>
                            <option value="2">BBIN���</option>
                            <option value="3">MG���</option>
                            <option value="4">OG���</option>
                            <option value="5">HG���</option>
							<option value="6">PT���</option>
							<option value="7">EBET���</option>
                        </select>
                            &nbsp;
                            <asp:Button ID="b_search" runat="server" Text="����" CssClass="btn btn-primary" OnClick="b_search_Click" />
                        </label>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            id="dataTable" class="table table-bordered table-striped centered dataTable"
            aria-describedby="dataTable_info">
            <!--�б���-->
            <thead>
                <tr  height="22" style="background-color: #507CD1; color: #fff">
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        ������
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        ת�����
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        ת������
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        ת������(�ٷ�֮)
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        ������
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        �ύʱ��
                    </th>
                </tr>
            </thead>
            <tbody role="alert" aria-live="polite" aria-relevant="all">
                <asp:Repeater ID="rptOrders" runat="server">
                    <ItemTemplate>
                        <tr bgcolor="#EFF3FB">
                            <td>
                                <%# Eval("orderid")%>
                            </td>
                            <td>
                                <%# Eval("quotaValue","{0:f2}")%>
                            </td>
                            <td>
                                <%# quotaType[int.Parse(Eval("quota_type").ToString())]%>
                            </td>
                            <td>
                                <%# toPercent(Eval("payrate").ToString())%>%
                            </td>
                            <td>
                                <%# Eval("charge","{0:f2}")%>
                            </td>
                            <td>
                                <%# Eval("addtime")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr bgcolor="#ffffff">
                            <td>
                                <%# Eval("orderid")%>
                            </td>
                            <td>
                                <%# Eval("quotaValue","{0:f2}")%>
                            </td>
                            <td>
                                <%# quotaType[int.Parse(Eval("quota_type").ToString())]%>
                            </td>
                            <td>
                                <%# toPercent(Eval("payrate").ToString())%>%
                            </td>
                            <td>
                                <%# Eval("charge","{0:f2}")%>
                            </td>
                            <td>
                                <%# Eval("addtime")%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
                <!--������-->
                <%if (this.Pager1.RecordCount <= 0)
                  { %>
                <tr class="odd">
                    <td valign="top" colspan="7" class="dataTables_empty">
                        û�з��������ļ�¼
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
                <!--��ť-->
                <td height="22" align="left" class="font8">
                    <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount% ��ҳ����%PageCount% ��ǰҳ��%CurrentPageIndex%"
                                CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                                NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" Height="30px"
                                OnPageChanged="Pager1_PageChanged">
                            </aspxc:AspNetPager>
                </td>
            </tr>
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
        </table>
    </div>
    <!--�Ҳ�������-->
    </form>
</body>
</html>