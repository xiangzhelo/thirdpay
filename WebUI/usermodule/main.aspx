<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="viviAPI.WebUI7uka.usermodule.main" %>

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
            <a href="/usermodule/main.aspx"><span>翁贝网 商户后台</span></a>
        </div>
        <div class="dl-log">
            欢迎您， <span class="dl-log-user">
                <%=getnm %></span> <a href="logout.aspx" title="退出系统" class="dl-log-quit">[退出]</a>
            <a href="/" target="_blank" title="网站首页" class="dl-log-quit">网站首页</a>
        </div>
    </div>
    <div class="content">
        <div class="dl-main-nav">
            <ul id="J_Nav" class="nav-list ks-clear">
                <li class="nav-item dl-selected">
                    <div class="nav-item-inner nav-inventory">
                        商户首页
                    </div>
                    <div class="nav-item-mask">
                    </div>
                </li>
                <li class="nav-item">
                    <div class="nav-item-inner nav-home">
                        订单查询
                    </div>
                    <div class="nav-item-mask">
                    </div>
                </li>
                <li class="nav-item">
                    <div class="nav-item-inner nav-order">
                        结算中心
                    </div>
                    <div class="nav-item-mask">
                    </div>
                </li>
                <li class="nav-item">
                    <div class="nav-item-inner nav-user">
                        个人资料
                    </div>
                    <div class="nav-item-mask">
                    </div>
                </li>
                <li class="nav-item">
                    <div class="nav-item-inner nav-distribution">
                        点卡消耗
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

                id: 'shzx',

                homePage: 'account',

                menu: [{

                    text: '商户中心',

                    items: [
                    { id: 'account', text: '商户首页', href: 'account/account.aspx' },
  { id: 'account_api', text: 'API接入', href: 'account/api.aspx' },

  { id: 'account_safety', text: '认证安全', href: 'account/safety.aspx' },
  { id: 'account_messages', text: '站内消息', href: 'account/messages.aspx' },
  { id: 'safety_cash', text: '提现密码', href: 'safety/cashpass.aspx' },
  { id: 'product_cos', text: '兑换比例', href: 'product/costrate.aspx' },
  { id: 'account_ziliao', text: '资料下载', href: 'account/doc_download.aspx' }

                ]

                }]

            }, {

                id: 'ddcx',

                homePage: 'orderbank',

                menu: [{

                    text: '订单查询',

                    items: [

                { id: 'orderbank', text: '网银订单', href: 'order/orderbank.aspx' },

                { id: 'orderweixin', text: '微信订单', href: 'order/orderweixin.aspx' },

                { id: 'ordercards', text: '卡类订单', href: 'order/ordercards.aspx' },

                { id: 'orderalipay', text: '支付宝订单', href: 'order/orderalipay.aspx' },
                { id: 'ordertenpay', text: '财付通订单', href: 'order/ordertenpay.aspx' },
                { id: 'orderlist', text: '所有订单', href: 'order/orderlist.aspx' }

              ]

                }]
            }, {

                id: 'jszx',

                homePage: 'applycost',

                menu: [{

                    text: '结算中心',

                    items: [

                { id: 'applycost', text: '提现申请', href: 'settlement/applycost.aspx' },

                { id: 'costlog', text: '结算记录', href: 'settlement/costlog.aspx' },

                { id: 'incomestat', text: '收入明细', href: 'settlement/incomestat.aspx' },

                { id: 'costinfo', text: '修改结算账户', href: 'account/costinfo.aspx' }

              ]

                }]
            }, {

                id: 'grzl',

                homePage: 'costinfo',

                menu: [{

                    text: '个人资料',

                    items: [

                  { id: 'costinfo', text: '账户资料', href: 'account/costinfo.aspx' },

                  { id: 'info', text: '修改资料', href: 'account/info.aspx' }

                ]

                }]
            }, {

                id: 'dkxh',

                homePage: 'znexcha',

                menu: [{

                    text: '点卡消耗',

                    items: [

                  { id: 'znexcha', text: '智能提交', href: 'order/znexcha.aspx' },

  { id: 'excha', text: '普通提交', href: 'order/excha.aspx' },

  { id: 'exchalist', text: '消耗记录', href: 'order/exchalist.aspx' }
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
