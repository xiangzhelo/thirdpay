<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="viviAPI.WebUI7uka.agentmodule.main" %>

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
            <a href="/admincp.php?ac=index"><span>翁贝网 系统后台</span></a>
        </div>
        <div class="dl-log">
            欢迎您， <span class="dl-log-user">ruanpa</span> <a href="/index.php?ac=common&amp;op=logout&amp;uhash=e51f64fcaaee8134e4bec7657587fad8"
                title="退出系统" class="dl-log-quit">[退出]</a> <a href="/" target="_blank" title="网站首页"
                    class="dl-log-quit">网站首页</a>
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
                    <div class="nav-item-inner nav-union">
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

                id: 'yun',

                homePage: 'yun_ap_ysj1',

                menu: [{

                    text: '商户中心',

                    items: [
                    { id: 'yun_ap_ysj1', text: '商户首页', href: 'account/account.aspx' },
  { id: 'yun_ap_ysj2', text: 'API接入', href: 'account/api.aspx' },

  { id: 'yun_zdgx', text: '认证安全', href: 'account/safety.aspx' },
  { id: 'yun_zdgx', text: '站内消息', href: 'account/messages.aspx' },
  { id: 'yun_zdgx', text: '提现密码', href: 'safety/cashpass.aspx' },
  { id: 'yun_zdgx', text: '兑换比例', href: 'product/costrate.aspx' },
  { id: 'yun_zdgx', text: '资料下载', href: '####' }

                ]

                }]

            }, {

                id: 'shydl',

                homePage: 'UserList2',

                menu: [{

                    text: '订单查询',

                    items: [

                { id: 'UserList2', text: '网银订单', href: 'order/orderbank.aspx' },

{ id: 'peizhi_jiben', text: '付款操作', href: '#######' },

                { id: 'peizhi_xinxi', text: '微信订单', href: '#############' },

                { id: 'peizhi_mokuai', text: '卡类订单', href: 'order/ordercards.aspx' },

                { id: 'peizhi_mima', text: '支付宝订单', href: '' },
                { id: 'peizhi_mima', text: '财付通订单', href: '' },
                { id: 'peizhi_mima', text: '所有订单', href: '' }

              ]

                }]
            }, {

                id: 'peizhi',

                homePage: 'peizhi_jiben',

                menu: [{

                    text: '结算中心',

                    items: [

                { id: 'peizhi_jiben', text: '提现申请', href: 'settlement/applycost.aspx' },

                { id: 'peizhi_jiben', text: '结算记录', href: 'settlement/costlog.aspx' },

                { id: 'peizhi_xinxi', text: '收入明细', href: 'settlement/incomestat.aspx' },

                { id: 'peizhi_mokuai', text: '修改结算账户', href: 'account/costinfo.aspx' }

              ]

                }]
            }, {

                id: 'shuju',

                homePage: 'shuju_ap_yy',

                menu: [{

                    text: '个人资料',

                    items: [

                  { id: 'shuju_an_yy', text: '账户资料', href: 'account/costinfo.aspx' },

                  { id: 'shuju_an_lm', text: '修改资料', href: 'account/info.aspx' }

                ]

                }]
            }, {

                id: 'user',

                homePage: 'shuju_yh',

                menu: [{

                    text: '点卡消耗',

                    items: [

                  { id: 'shuju_yh', text: '智能提交', href: 'Cache/Manage.aspx' },

  { id: 'shuju_yhz', text: '普通提交', href: 'order/excha.aspx' },

  { id: 'shuju_rzyh', text: '消耗记录', href: 'order/excha.aspx' }
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
