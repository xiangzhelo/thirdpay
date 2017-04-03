<%@ Page Title="" Language="C#" MasterPageFile="~/longbao/neirong.master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="viviAPI.WebUI2015.longbao.News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContent" runat="server">
    <header>网站动态</header>
<style>
       .clear
        {
            clear: both;
        }
        .page
        {
            margin-left: 5px;
        }
        .page span
        {
            padding: 3px;
            min-width: 22px;
            text-align: center;
            margin-top: 30px;
            display: block;
            float: left;
            border: 1px #ccc solid;
            margin-right: 10px;
        }
        .page a
        {
            padding: 3px;
            min-width: 22px;
            text-align: center;
            margin-top: 30px;
            display: block;
            float: left;
            border: 1px #ccc solid;
            margin-right: 10px;
        }
        .page .yes
        {
            background: #0f8ffc;
            color: #fff;
        }
        .page .no
        {
            width: auto;
            border: none;
        }
    </style>


    <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
    <form runat=server >
    <ul class="list">
            <asp:Repeater ID="rptnewlist" runat="server">
                            <ItemTemplate>
        <li><a href='NewsContent.aspx?newsid=<%#Eval("newsid")%>'> <%#Eval("newstitle")%></a><span><%# Convert.ToDateTime(Eval("addtime")).ToString("yyyy年MM月dd日")%></span></li>
                               </ItemTemplate>
                </asp:Repeater>
  
    </ul>
      <aspxc:AspNetPager ID="Pager1" runat="server" CustomInfoHTML="共%RecordCount%/%CurrentPageIndex%条"
                            OnPageChanged="Pager1_PageChanged" CssClass="page c" AlwaysShow="true" PageSize="15"
                            ShowPageIndexBox="Never">
                        </aspxc:AspNetPager>
                        
</form>
</asp:Content>
