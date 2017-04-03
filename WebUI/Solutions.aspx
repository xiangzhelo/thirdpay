<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="Solutions.aspx.cs" Inherits="viviAPI.WebUI2015.Solutions" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Header" runat="server">
    <style type="text/css"> body{background:#fff;}</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div class="nrhead">
        <img src="style1/images/hyfa.jpg" />
    </div>
    <div class="nrcontent" id="nrtabs">
        <header>
            <a href="javascript:;" class="selected">电商支付</a>
            <a href="javascript:;">交易市场</a>
            <a href="javascript:;">航空商旅</a>
            <a href="javascript:;">P2P资金托管</a>
            <a href="javascript:;">数娱行业</a>
        </header>
        <div class="tabs">
            <div class="item">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon1.png" />
                    <div>
                        <span>电商支付</span>
                        现如今电商行业日益壮大，对提供连接全国各大商业银行的支付通道，无缝集成及快速电子化要求急剧增加，而银行接入成本高，现金风险大，结算周期长，退款较慢，缺乏诚信机制，营销推广成本高等问题。 Mo宝支付经过不断的完善和发展，针对网上销售行业提供在线收付收款、交易管理、财务对账单、结算及提现等专业的在线支付解决方案。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>解决方案</span>
                    <p>针对目前B2C网上销售的业务开展情况，Mo宝支付致力于为网上购物平台提升在线收款的效率、对账的准确性、交易查询实时性、结算到账快捷、支付安全性等方面，提供专业的在线支付解决方案及综合服务。</p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>多种支付方式支持</span>
                                提供个人网上银行、快捷支付、Mo宝账户、手机支付、蓝牙pos等多种支付方式，用户支付便捷，付款实时到账。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>覆盖80%银行卡用户</span>
                                覆盖80%银行卡用户，持卡用户可以使用国内多家银行的网上银行实现在线付款和对Mo宝账户进行充值。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>降低接入银行成本</span>
                                合作商家无需与多家银行一一接入，为商家缩减了系统开发和维护的成本，无需任何计算机及网络硬件和人力成本投入，在线即可轻松实现收付款。
                            </div>
                        </li>
                        <li class="num num4">
                            <i></i>
                            <div>
                                <span>支付网关轻松接入</span>
                                Mo宝支付提供标准的接入说明文档，提供多种网络程序语言接入样例，接入更方便快捷。
                            </div>
                        </li>
                        <li class="num num5">
                            <i></i>
                            <div>
                                <span>交易订单管理方便</span>
                                交易订单实时管理，实时查询订单状态和退款情况，功能强大，操作管理方便。
                            </div>
                        </li>
                        <li class="num num6">
                            <i></i>
                            <div>
                                <span>解决资金结算及对账问题</span>
                                Mo宝支付提供交易款快速结算，有效提高资金周转速度，提供专业对账单，支持财务人员对账。
                            </div>
                        </li>
                        <li class="num num7">
                            <i></i>
                            <div>
                                <span>解决付款实时到账问题</span>
                                传统付款方式用户付款有时不能及时到账，或到账后也无法及时获知，用户通过Mo宝账户支付，实时到达商家账户。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon2.png" />
                    <div>
                        <span>交易市场</span>
                        中国互联网已全面步入商务时代，在线交易也逐渐发展成为现货交易的主流模式。交易市场在线交易采用B2B商业模式，是一种网上和网下相结合， 现实和虚拟相结合，传统经济与新经济相结合的双赢模式，充分解决了信息传递、客户来源、交易成本等传统交易的瓶颈问题。Mo宝支付通过深入调研交易行业需求，全新研发解决交易市场行业需求的在线支付及账务管理产品。为交易市场采购商、供应商、交易平台提供专业的支付解决方案。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>解决方案</span>
                    <p>目前交易市场在线交易，资金结算主要还是使用银行转账方式，支持银行少，转账成本高，到账时间长， 财务工作量非常大。Mo宝支付公司针对交易市场行业支付金额大、资金安全性要求高，回单信息详细，保证金交易等特点，提供专业解决方案。</p>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon3.png" />
                    <div>
                        <span>航空商旅</span>
                        自中国航空业全部实行电子客票以来，传统的机票销售渠道逐渐转到线上，并趋向随着市场的不断发展，航空票务业正面临着诸多的挑战，支付方式正成为亟须解决的问题。通过与航空业内领先企业合作，深挖航空票务行业需求，经过多年的辛勤耕耘和经验的积累，Mo宝支付全新构建覆盖航空机票行业的丰富在线支付产品及服务，为航空公司、机票B2B交易平台及机票B2C直销平台等，量身定制安全、便捷、专业的行业支付解决方案。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>解决方案</span>
                    <p>Mo宝支付通过分析机票B2B，B2C平台订单、支付、分账、结算、交易对账、账户资金管理等各业务环节，为机票平台构建全新的行业支付解决方案，提供便捷的大额支付支持，规则灵活的交易分账及退款，高效快速的资金结算及对账服务，解决机票平台商、供应商和分销商之间的复杂资金周转、多方分润、资金结算及提现、财务对账等一系列业务需求。</p>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon4.png" />
                    <div>
                        <span>P2P资金托管</span>
                        摩宝资金托管服务平台，是专门为P2P平台打造的金融级资金账户管理体系，满足P2P行业中对第三方资金托管、安全支付、高效结算的需求，支持平台收/付款、账户资金划拨、账户资金管理功能。摩宝资金托管服务平台，保障用户账户内资金独立存放， 仅用户有权对本人进行充值、提现操作，P2P平台未经授权无权动用用户账户，从而保障账户内资金安全。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>解决方案</span>
                    <p>摩宝为普通用户提供年化收益12%的理财产品，通过力帆善融平台投资理财， 可获得高于银行30倍活期存款收益。同时，充分考虑普通大众的投资理财心理，提供多种理财产品组合满足投资人个性化需求。</p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>投资人</span>
                                投资资金全额直接划入借款人，避免平台挪用；投资人可以便利查询投资资金流向 ，投资人投资时可以通过网银、快捷、大额支付等多种通道进行充值和投资。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>借款人</span>
                                代扣功能，方便借款人便利还款；借款人能够便利查询借款和还款资金的流向；借款人对借款资金提现时，只能提款到同名银行卡，资金安全，后续可以根据业务需求，对借款资金进行定向划拨。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>平台方</span>
                                投资资金不经过P2P平台，可以规避风险，满足政策监管要求；分账功能满足P2P平台与用户之间的收佣、分润、红包、缴费等业务需求；支持设置风险准备金账户，当借款人拖欠本金或利息时， 支持平台通过风险准备金账户代借款人还本金或利息。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon4.png" />
                    <div>
                        <span>数娱行业</span>
                        摩宝支付依据行业需求，数字娱乐企业提供方便快捷的收款服务；在确保账户资金安全的同时， 提供灵活多样的结算方式。为企业在收款、付款、对账、查询和结算等方面提供无限便利。
                    </div>
                </div>
                <div class="itemcontent cl">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
