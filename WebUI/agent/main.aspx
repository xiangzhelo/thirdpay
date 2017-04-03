<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="viviAPI.WebUI7uka.agent.main" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>翁贝网 管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/template/admin/assets/css/dpl-min.css" rel="stylesheet" type="text/css" />
    <link href="/template/admin/assets/css/bui-min.css" rel="stylesheet" type="text/css" />
    <link href="/template/admin/assets/css/main-min.css" rel="stylesheet" type="text/css" />
    <link href="/template/admin/assets/css/jia.css" rel="stylesheet" type="text/css" />
    <script charset="utf-8" async="" src="/template/admin/assets/js/common/main-min.js"></script>
</head>
<body>
    <div class="header">
        <div class="dl-title">
            <a href="/agent/main.aspx"><span>翁贝网 代理后台</span></a>
        </div>
        <div class="dl-log">
            欢迎您， <span class="dl-log-user">ruanpa</span> <a href="logout.aspx" title="退出系统" class="dl-log-quit">
                [退出]</a> <a href="/" target="_blank" title="网站首页" class="dl-log-quit">网站首页</a>
        </div>
    </div>
    <div class="content">
        <div class="dl-main-nav">
            <ul id="J_Nav" class="nav-list ks-clear">
                <li class="nav-item dl-selected">
                    <div class="nav-item-inner nav-inventory">
                        日常使用
                    </div>
                    <div class="nav-item-mask">
                    </div>
                </li>
            </ul>
        </div>
        <ul id="J_NavContent" class="dl-tab-conten">
        </ul>
    </div>
    <script type="text/javascript" src="/template/admin/assets/js/jquery-1.8.1.min.js"></script>
    <script type="text/javascript" src="/template/admin/assets/js/bui-min.js"></script>
    <script type="text/javascript" src="/template/admin/assets/js/config-min.js"></script>
    <script type="text/javascript">

        BUI.use('common/main', function () {

            var config = [{

                id: 'yun',

                homePage: 'yun_ap_ysj1',

                menu: [{

                    text: '日常使用',

                    items: [
                    { id: 'yun_ap_ysj1', text: '网银订单查询', href: 'Order/BankList.aspx' },
                    { id: 'yun_ap_ysj2', text: '点卡订单查询', href: 'Order/CardList.aspx' },
                    { id: 'yun_zdgx', text: '统计分析', href: 'orderreport2.aspx' },
                    { id: 'yun_zdgx2', text: '商户列表', href: 'User/UserList.aspx?UserStatus=2' },
                    { id: 'yun_zdgx3', text: '团队联系页面', href: 'Contact.aspx' },
                    { id: 'yun_zdgx4', text: '员工业务统计', href: 'stat.aspx' },
                    { id: 'yun_zdgx5', text: '员工推广连接获取', href: 'Index.aspx' }
                ]
                }]
            }];

            new PageUtil.MainPage({

                modulesConfig: config

            });
        });

    </script>
</body>
</html>
