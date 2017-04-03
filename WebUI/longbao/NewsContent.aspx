<%@ Page Title="" Language="C#" MasterPageFile="~/longbao/neirong.master" AutoEventWireup="true"  validateRequest=false  CodeBehind="NewsContent.aspx.cs" Inherits="viviAPI.WebUI2015.longbao.NewsContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContent" runat="server">
<%--    新闻内容--%>
            <!--right scroll-->
                <div class="about-span2 y-last managebar">
                    <div class="aboutus">
                    <%--    <br />--%>
                        <h2 style="border-bottom: 2px solid gainsboro; padding: 0 0 10px 0; width: 100%;
                            text-align: right; color: #666; font-size: 12px; display:none">
                            发布时间：<asp:Literal ID="newstime" runat="server"></asp:Literal>&nbsp;&nbsp;</h2>
                        <h1 style="font-family: 'Microsoft yahei', verdana, sans-serif; color: #428BCA; font-size: 16px;
                            padding: 0px; padding-bottom: 10px; text-align: center; margin-top: 20px; padding: 0px 0px 10px;">
                            <asp:Literal ID="newstitle" runat="server"></asp:Literal></h1>
                        <div style="line-height: 200%; width: 100%; float: left; font-size: 14px; color: black;">
                            <asp:Literal ID="newscontenct" runat="server"></asp:Literal>
                           <%-- <a href="news.aspx" class="backbtn">返回列表&gt;&gt;</a>--%>
                        </div>
                    </div>
                </div>
</asp:Content>
