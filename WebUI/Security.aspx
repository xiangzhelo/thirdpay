<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="Security.aspx.cs" Inherits="viviAPI.WebUI2015.Security" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Header" runat="server">
    <style type="text/css"> body{background:#fff;}</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div class="nrhead">
        <img src="style1/images/aqzx.jpg" />
    </div>
    <div class="nrcontent" id="nrtabs">
        <header>
            <a href="javascript:;" class="selected">防钓鱼区</a>
            <a href="javascript:;">企业资质</a>
            <a href="javascript:;">不良举报</a>
        </header>
        <div class="tabs cl">
            <div class="item">
                <img src="style1/images/aq1.jpg" />
            </div>
            <div class="item" style="display:none;">
                <img src="style1/images/aq2.jpg" />
            </div>
            <div class="item" style="display:none;">
                <img src="style1/images/aq3.jpg" />
            </div>
        </div>
    </div>
</asp:Content>
