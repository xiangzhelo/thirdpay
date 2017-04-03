<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="viviAPI.WebAdmin.Console.News.SendMsg"
    CodeBehind="SendMsg.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head >
    <title>发站内信</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <base target="_self" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    
    <script type="text/javascript" charset="utf-8">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>

    <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/ueditor/ueditor.all.js"> </script>

    <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
    <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
        <script type="text/javascript" charset="utf-8" src="/ueditor/lang/zh-cn/zh-cn.js"></script>

    
    <script src="../js/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfcontent" runat="server" />
    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
                发送内部消息
            </td>
        </tr>
        <tr>
            <td align="right" class="jfontItem" style="width: 125px">
                收信者：
            </td>
            <td align="left">
                <asp:TextBox CssClass="label" ID="txtMsgTo" MaxLength="50" runat="server" Width="300px"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" class="jfontItem" style="width: 125px">
                消息标题：<asp:HiddenField ID="NewsID" Value="0" runat="server" />
            </td>
            <td align="left">
                <asp:TextBox CssClass="label" ID="tb_title" MaxLength="50" runat="server" Width="300px"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" class="jfontItem" style="width: 125px">
                消息内容：
            </td>
            <td align="left">

               <script id="editor" type="text/plain" style="width:1024px;height:500px;"></script>

            </td>
        </tr>
        <tr>
            <td align="right" class="jfontItem" style="width: 125px; height: 40px;">
            </td>
            <td align="left" style="height: 40px">
                <asp:Button CssClass="button" ID="bt_sub" runat="server" Text=" 发 送 " OnClick="bt_sub_Click"
                    OnClientClick="return getContent();"></asp:Button>
            </td>
        </tr>
        <tr>
            <td align="right" class="jfontItem" style="width: 125px; height: 40px;">
            </td>
            <td align="left" style="height: 40px">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>

    <script type="text/javascript">

        //实例化编辑器
        //建议使用工厂方法getEditor创建和引用编辑器实例，如果在某个闭包下引用该编辑器，直接调用UE.getEditor('editor')就能拿到相关的实例
        var ue = UE.getEditor('editor');


        function isFocus(e) {
            alert(UE.getEditor('editor').isFocus());
            UE.dom.domUtils.preventDefault(e)
        }
        function setblur(e) {
            UE.getEditor('editor').blur();
            UE.dom.domUtils.preventDefault(e)
        }
        function insertHtml() {
            var value = prompt('插入html代码', '');
            UE.getEditor('editor').execCommand('insertHtml', value)
        }
        function createEditor() {
            enableBtn();
            UE.getEditor('editor');
        }
        function getAllHtml() {
            alert(UE.getEditor('editor').getAllHtml())
        }
        function getContent() {
//            var arr = [];
//            arr.push("使用editor.getContent()方法可以获得编辑器的内容");
//            arr.push("内容为：");
//            arr.push(UE.getEditor('editor').getContent());
//            alert(arr.join("\n"));

            var content = UE.getEditor('editor').getContent();


            $("#hfcontent").val(content);

           
            return true;
        }
        function getPlainTxt() {
            var arr = [];
            arr.push("使用editor.getPlainTxt()方法可以获得编辑器的带格式的纯文本内容");
            arr.push("内容为：");
            arr.push(UE.getEditor('editor').getPlainTxt());
            alert(arr.join('\n'))
        }
        function setContent(isAppendTo) {
            var arr = [];
            arr.push("使用editor.setContent('欢迎使用ueditor')方法可以设置编辑器的内容");
            UE.getEditor('editor').setContent('欢迎使用ueditor', isAppendTo);
            alert(arr.join("\n"));
        }
        function setDisabled() {
            UE.getEditor('editor').setDisabled('fullscreen');
            disableBtn("enable");
        }

        function setEnabled() {
            UE.getEditor('editor').setEnabled();
            enableBtn();
        }

        function getText() {
            //当你点击按钮时编辑区域已经失去了焦点，如果直接用getText将不会得到内容，所以要在选回来，然后取得内容
            var range = UE.getEditor('editor').selection.getRange();
            range.select();
            var txt = UE.getEditor('editor').selection.getText();
            alert(txt);
        }

        function getContentTxt() {
            var arr = [];
            arr.push("使用editor.getContentTxt()方法可以获得编辑器的纯文本内容");
            arr.push("编辑器的纯文本内容为：");
            arr.push(UE.getEditor('editor').getContentTxt());
            alert(arr.join("\n"));
        }
        function hasContent() {
            var arr = [];
            arr.push("使用editor.hasContents()方法判断编辑器里是否有内容");
            arr.push("判断结果为：");
            arr.push(UE.getEditor('editor').hasContents());
            alert(arr.join("\n"));
        }
        function setFocus() {
            UE.getEditor('editor').focus();
        }
        function deleteEditor() {
            disableBtn();
            UE.getEditor('editor').destroy();
        }
        function disableBtn(str) {
            var div = document.getElementById('btns');
            var btns = UE.dom.domUtils.getElementsByTagName(div, "button");
            for (var i = 0, btn; btn = btns[i++]; ) {
                if (btn.id == str) {
                    UE.dom.domUtils.removeAttributes(btn, ["disabled"]);
                } else {
                    btn.setAttribute("disabled", "true");
                }
            }
        }
        function enableBtn() {
            var div = document.getElementById('btns');
            var btns = UE.dom.domUtils.getElementsByTagName(div, "button");
            for (var i = 0, btn; btn = btns[i++]; ) {
                UE.dom.domUtils.removeAttributes(btn, ["disabled"]);
            }
        }

        function getLocalData() {
            alert(UE.getEditor('editor').execCommand("getlocaldata"));
        }

        function clearLocalData() {
            UE.getEditor('editor').execCommand("clearlocaldata");
            alert("已清空草稿箱")
        }
</script>

    </form>
</body>
</html>
