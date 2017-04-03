<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="viviAPI.WebUI2015.Products" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Header" runat="server">
    <style type="text/css"> body{background:#fff;}</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div class="nrhead">
        <img src="style1/images/zfcp.jpg" />
    </div>
    <div class="nrcontent" id="nrtabs">
        <header>
            <a href="javascript:;" class="selected">网上银行支付</a>
            <a href="javascript:;">移动支付</a>
            <a href="javascript:;">快捷支付</a>
            <a href="javascript:;">信用卡支付</a>
            <a href="javascript:;">B2B大额支付</a>
        </header>
        <div class="tabs">
            <div class="item">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon1.png" />
                    <div>
                        <span>网上银行支付</span>
                        网上银行支付，是指通过互联网跳转到各个银行网上银行进行支付的方式。 Mo宝支付现在支持工行、农行、中行、建行、交行、招商银行等多家银行，满足不同网银用户的支付需求。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>覆盖银行卡用户范围广</span>
                                支持工行、农行、中行、建行、交行、招商银行等多银行卡用户，覆盖银行多，支付银行卡用户范围广。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>节省商家接入银行成本</span>
                                合作商家无需与多家银行一一接入，无需任何服务器、网络硬件、人力成本的投入，为商家缩减了系统开发和维护成本，通过接入Mo宝支付即可轻松在线实现收付款。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>支付网关轻松接入</span>
                                Mo宝支付提供标准的接入说明文档，提供多种网络程序语言接入样例，接入更方便快捷。
                            </div>
                        </li>
                        <li class="num num4">
                            <i></i>
                            <div>
                                <span>交易管理方便</span>
                                Mo宝支付提供交易订单管理，账户流水查询，财务对账，交易退款等相关服务。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon2.png" />
                    <div>
                        <span>手机银行支付</span>
                        手机银行支付是指用户用手机登录商户WAP网站，下单后通过WAP方式跳转到银行的手机银行完成支付的过程。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>随时随地支付</span>
                                由于手机的便捷性，用可随时随地的使用手机进行Wap支付服务，手机Wap购物、Wap缴费、移动付费等个性化消费理财服务。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>巨大潜在客户群</span>
                                随着手机的普及和智能手机的日新月异的发展和运营商的网络优化提速，未来必将是手机用户的天下。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>未来消费发展趋势</span>
                                随着网络和手机技术的发展，手机WAP服务已成为未来发展的必然趋势，拥有手机支付习惯的消费群体日渐增长，Mo宝支付助力合作伙伴抢占市场先机。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon3.png" />
                    <div>
                        <span>快捷支付</span>
                        快捷支付是Mo宝支付与银行合作推出的快捷支付产品，用户将银行卡账户与Mo宝账户进行绑定后， 在支付时只需输入Mo宝账户名和支付密码即可完成支付。无需开通网银，只需绑定您的信用卡或者储蓄卡，每次支付时只需输入身份证末四位即可完成付款。 首次使用快捷支付交易，您只需输入银行卡必填信息并且同意快捷支付协议。 当您首次使用快捷支付交易成功后，系统将您的手机号作为您唯一身份标识，下次可使用手机号登录支付。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>安全、快捷，支付成功率高。</span>
                                用户支付无需网银跳转，只需输入Mo宝账户名和支付密码即可完成支付。简单的支付流程给用户更好的支付体验。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>开通和管理更方便。</span>
                                用户通过Mo宝支付网站在线进行快捷支付的开通和解除，通过Mo宝支付平台跳转到银行进行签约和解约，足不出户即可完成，免除去银行排队开通的成本。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>用户体验好，支持多种渠道支付。</span>
                                简单的支付流程，进入门槛低，使用方便。同时没有传统的网银跳转方式，可用在互联网、移动互联网、POS、手机客户端、IPTV等不同渠道的支付。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon4.png" />
                    <div>
                        <span>信用卡支付</span>
                        信用卡支付是Mo宝支付与银行的合作新推出的支付产品，解决商家与消费者非面对面交易的信用卡支付问题的产品。只需要客户提供信用卡卡号、有效期等信息，即可通过银行的直连接口完成支付。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>支付更方便快捷</span>
                                客户只要填写信用卡卡号、有效期信息，即可方便的完成支付，无需开通网上银行。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>提升客户购买力</span>
                                支持支持信用透支提前消费，支付限额是信用卡本身的限额，大大提高了客户的购买力。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>专业系统控制风险</span>
                                业务选择上面需要选择实名制可以追索的业务，通过专业风险控制系统有效防范控制盗刷、恶意套现等，保证业务安全正常进行。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon4.png" />
                    <div>
                        <span>B2B大额支付</span>
                        用于企业之间（B2B）交易所涉及的大额资金收款、企业资金归集的支付产品，支持国内主要银行企业账户无限额支付、个人银行卡及信用卡大额支付，是Mo宝支付专为您大额交易收付款和资金运转提供方便、快捷、安全的大额资金收款服务。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>真正大额支付</span>
                                支持个人网银大额和企业网银无限额支付，最大满足B2B支付业务需求。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>方便后台管理</span>
                                提供交易订单管理，账户流水查询，财务对账，交易退款等相关服务。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>专业接入支持</span>
                                一对一专人负责技术接入支持，提供多种程序语言接入样例，接入方便快捷。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
