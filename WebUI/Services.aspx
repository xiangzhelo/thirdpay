<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="viviAPI.WebUI2015.Services" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Header" runat="server">
    <style type="text/css"> body{background:#fff;}</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div class="nrhead">
        <img src="style1/images/shfw.jpg" />
    </div>
    <div class="nrcontent" id="nrtabs">
        <header>
            <a href="javascript:;" class="selected">即时到账交易</a>
            <a href="javascript:;">委托扣款服务</a>
            <a href="javascript:;">委托结算服务</a>
            <a href="javascript:;">交易对账服务</a>
            <a href="javascript:;">收付管理系统</a>
            <a href="javascript:;">集团账户管理服务</a>
        </header>
        <div class="tabs">
            <div class="item">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon1.png" />
                    <div>
                        <span>即时到账交易</span>
                        即时到账交易是买家基于对您信任，在进行交易时自愿付款给您，一旦买家完成支付，交易款项就立即到达您的Mo宝支付账户。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>交易流程步骤少，货款实时到账。</span>
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>强大的商户后台管理系统。</span>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon2.png" />
                    <div>
                        <span>委托扣款服务</span>
                        Mo宝支付为合作商户提供的一项无密扣款支付服务，支持定期或不定期从已签署委托扣款业务的Mo宝支付账户进行扣款的服务。合作商户与Mo宝支付签署相关协议并接入后，由用户与合作商户签署扣款协议，并在Mo宝支付设定委托扣款业务，Mo宝支付受合作商户委托对用户Mo宝支付账户进行扣款。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>省时省力省心</span>
                                省去用户跑银行或业务公司柜台排队缴费时间，用户只须一次设定即可长期使用，全程电子化，扣款无需手动，省时、省力、省心。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>账户资金安全</span>
                                值得信赖的Mo宝支付合作商户，用户可选择设定单笔和日累计扣款限额，扣款业务设定自动短信及邮件通知用户，用户账户资金安全有保障。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>用户管理方便</span>
                                用户可登录Mo宝支付网站随时查看已设定委托代扣业务，可随时更改扣款限额，每笔扣款记录均可通过交易管理进行查询管理。
                            </div>
                        </li>
                        <li class="num num4">
                            <i></i>
                            <div>
                                <span>商户管理对账方便</span>
                                合作商户可登录Mo宝支付企业版，在交易管理查看每笔扣款交易记录，Mo宝支付提供对账单下载，方便商户财务人员对账。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon3.png" />
                    <div>
                        <span>委托结算服务</span>
                        Mo宝支付为B2B交易平台、上下游渠道商或集团企业提供的支持多层级、复杂的资金周转的解决方案。通过委托结算服务，可实现一笔交易订单付款金额实时或非实时的拆分，并向多个不同Mo宝支付账户进行分发，为合作企业提供更灵活、更方便的分账体系，为上下游渠道商、集团企业和各企业之间的复杂资金流周转提供完整的解决方案。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>分账规则自由灵活</span>
                                您可以选择自己设定分账规则，也可以将分账规则交由Mo宝支付管理。每个分账环节都支持按固定金额或按百分比拆分，合作伙伴接入时可任意选择和自定义分账规则。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>结算提现灵活及时</span>
                                Mo宝支付分账交易款项支持实时清算，提供T+1等周期提现到账服务，支持交易手续费实时划扣和定期划扣等多种方式。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>交易管理、对账方便</span>
                                Mo宝支付提供强大商户后台管理系统支持，商户可以登录商户管理中心，进行交易管理、账户流水查询、对账单下载等丰富功能。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon4.png" />
                    <div>
                        <span>交易对账服务</span>
                        Mo宝支付为保证双方交易的一致性，每天在日终时定时生成对账文件，商户管理员可登录商户中心下载对账文件后进行对账处理。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>支持自动对账</span>
                                提供人工下载对账文件和service对service自动对账支持，及时稳定可靠。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>省时省力</span>
                                省去合作商户财务人员进行一一核对，一次接入长期使用，全程电子化，无需手动。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>对账管理方便</span>
                                交易管理查看每笔交易记录，方便商户财务人员对账。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon4.png" />
                    <div>
                        <span>收付管理系统</span>
                        收代付平台是一套能提供批量资金归集与发放功能的智能平台，通过它能够大大提高企业资金管理效率。收付平台为企业提供便捷的回笼销售交易资金功能。 收发平台可让企业足不出户将归集的资金，结算至指定银行账户，如为发放工资、批量支付费用等。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>便捷高效T+1日资金自动结算至企业指定银行账户，支持网银支付、mPOS支付、总部代扣支付等多种资金归集方式。</span>
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>安全灵活汇款清单即时提交、系统即时处理、收款方即时入帐、收款情况即时反馈，提高账务效率。</span>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item" style="display:none;">
                <div class="itemhead">
                    <img src="style1/images/nricon/icon4.png" />
                    <div>
                        <span>集团账户管理服务</span>
                        集团账户主要是指集团总公司设立的结算账户。集团总公司通过集团账户资金的上收和下拨，在其子公司之间进行资金的灵活调剂，达到加强资金的集中管理，提高资金使用效率的目的。
                    </div>
                </div>
                <div class="itemcontent cl">
                    <span>产品特点</span>
                    <p></p>
                    <ul>
                        <li class="num num1">
                            <i></i>
                            <div>
                                <span>资金集中化管理</span>
                                摩宝支付专为集团企业商家提供的多账户管理服务，拥有完善的集中资金管理体系，通过高效的资金运转和资金监控，实现资金集中化管理。
                            </div>
                        </li>
                        <li class="num num2">
                            <i></i>
                            <div>
                                <span>自动归集</span>
                                所有的企业存款被设定后，将自动上划至总公司财务账户。
                            </div>
                        </li>
                        <li class="num num3">
                            <i></i>
                            <div>
                                <span>自动下拨</span>
                                成员企业需要对外付款时，摩宝系统会自动从总公司财务的账户上划款至企业账户， 用于对外支付。所有操作都瞬间完成。下拨的金额不能大于该企业在总公司财务账户的存款余额。
                            </div>
                        </li>
                        <li class="num num4">
                            <i></i>
                            <div>
                                <span>自定义对账单</span>
                                可以按照总公司财务要求给企业打印对账单。
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
