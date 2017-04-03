using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace viviapi.BLL.Channel
{
    public class CardUtility
    {
        #region GetCardName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public static string GetCardName(int typeid)
        {
            if (typeid == 103)
                return "神州行";
            if (typeid == 104)
                return "盛大卡";
            if (typeid == 105)
                return "征途卡";
            if (typeid == 106)
                return "骏网一卡通";
            if (typeid == 107)
                return "Q币卡";
            if (typeid == 108)
                return "联通卡";
            if (typeid == 109)
                return "久游一卡通";
            if (typeid == 110)
                return "网易卡";
            if (typeid == 111)
                return "完美卡";
            if (typeid == 112)
                return "搜狐卡";
            if (typeid == 113)
                return "电信卡";
            if (typeid == 117)
                return "纵游一卡通";
            if (typeid == 118)
                return "天下一卡通";
            if (typeid == 119)
                return "天宏一卡通";
            if (typeid == 210)
                return "盛付通卡";

            return typeid.ToString();
        }
        #endregion

        #region GetTypeIdByCard
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static int GetTypeIdByCard(string cardno, string cardpwd)
        {
            int _typeid = 0;

            if (isSZX(cardno, cardpwd))
            {
                _typeid = 103;
            }
            else if (isSNDACARD(cardno, cardpwd))
            {
                _typeid = 104;
            }
            else if (isZHENGTU(cardno, cardpwd))
            {
                _typeid = 105;
            }
            else if (isJUNNET(cardno, cardpwd))
            {
                _typeid = 106;
            }
            else if (isQQCARD(cardno, cardpwd))
            {
                _typeid = 107;
            }
            else if (isUNICOM(cardno, cardpwd)) //联通卡
            {
                _typeid = 108;
            }
            else if (isJIUYOU(cardno, cardpwd)) //久游卡
            {
                _typeid = 109;
            }
            else if (isNETEASE(cardno, cardpwd)) //网易一卡通 13 9
            {
                _typeid = 110;
            }
            else if (isWANMEI(cardno, cardpwd)) //完美卡
            {
                _typeid = 111;
            }
            else if (isSOHU(cardno, cardpwd)) //搜狐卡
            {
                _typeid = 112;
            }
            else if (isTELECOM(cardno, cardpwd)) //电信卡
            {
                _typeid = 113;
            }
            else if (isZONGYOU(cardno, cardpwd)) //
            {
                _typeid = 117;
            }
            else if (isTIANXIA(cardno, cardpwd)) //
            {
                _typeid = 118;
            }
            else if (isTIANXIA(cardno, cardpwd)) //
            {
                _typeid = 119;
            }

            return _typeid;
        }
        #endregion

        #region
        #region isSZX
        /// <summary>
        /// 【支持卡种】
        ///※卡号15位的数字字母，密码8位或9位的阿拉伯数字。
        ///【支持面额】
        ///实卡面值：5元、10元、30元、35元、45元、100元、350元、1000元 
        ///虚卡面值：（任意面值，不含卡密，直充） 
        ///【温馨提示】 
        ///请使用卡号以CSC5、CS、S、CA、CSB、YA、YB、YC、YD开头的“盛大互动娱乐卡”进行支付。 
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isSZX(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{10,17}$";
            string cardpwdpatt = "^\\d{8,21}$";

            //全国卡：卡号17位、密码18位的阿拉伯数字
            //浙江：卡号10位 密码8位
            //福建：卡号16位 密码17位
            //广东：卡号17位 密码18位
            //辽宁：卡号16位 密码21位
            if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
            {
                if (cardno.Length == 17 && cardpwd.Length == 18)
                {
                    result = true;
                }
                else if (cardno.Length == 10 && cardpwd.Length == 8)
                {
                    result = true;//浙江 卡号10位 密码8位
                }
                else if (cardno.Length == 16 && cardpwd.Length == 17)
                {
                    result = true; //福建 卡号16位 密码17位
                }
                else if (cardno.Length == 16 && cardpwd.Length == 21)
                {
                    result = true; //辽宁 卡号16位 密码17位
                }
            }
            return result;
        }
        #endregion

        #region isSNDACARD
        /// <summary>
        ////【支持卡种】 
        ////※卡号15位的数字字母，密码8位或9位的阿拉伯数字。 
        ////【支持面额】 
        ////实卡面值：5元、10元、30元、35元、45元、100元、350元、1000元 
        ////虚卡面值：（任意面值，不含卡密，直充） 
        ////【温馨提示】 
        ////请使用卡号以CSC5、CS、S、CA、CSB、YA、YB、YC、YD开头的“盛大互动娱乐卡”进行支付。
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isSNDACARD(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^[0-9a-zA-Z]{15}$";
            string cardpwdpatt = "^\\d{8,9}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isZHENGTU
        /// <summary>
        /// 征途卡
        ///【支持卡种】 
        ////全国官方征途游戏充值卡，卡号16位阿拉伯数字，密码8位阿拉伯数字。 
        ////【支持面额】 
        ////5元，10元，15元，18元，20元，25元，30元，50元，60元，68元，100元，120元，180元，208元，250元，300元，468元，500元 
        ////对应点数：1元=100点 
        ////【温馨提示】 
        ////※请务必使用与您所选择的面额相同的征途卡进行支付,否则引起的交易失败或交易金额丢失，我方不予承担！
        ////如：您选择50元面额但使用100元卡支付，则系统认为实际支付金额为50元，高于50元部分不予退还。
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isZHENGTU(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{16}$";
            string cardpwdpatt = "^\\d{8}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isJUNNET
        /// <summary>
        /// 骏网一卡通
        ///       【支持卡种】 
        ///※卡号、密码都是16位的阿拉伯数字 
        ///【支持面额】 
        ///※任意面额 
        ///※对应点数：1元=100J点 
        ///【温馨提示】 
        ///※不能使用特定游戏专属充值卡支付。 特定游戏包括大唐风云、传说、蜗牛、猫扑一卡通、九鼎、雅典娜、山河等游戏。 
        ///※在此使用过的骏网一卡通，卡内剩余J点只能在易宝支付合作商家进行支付使用。 
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isJUNNET(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{16}$";
            string cardpwdpatt = "^\\d{16}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isQQCARD
        /// <summary>
        ////        【支持卡种】 
        ////※全国各地Q币卡，卡号：9位的阿拉伯数字、密码：12位的阿拉伯数字。 
        ////【支持面额】 
        ////※5元,10元,15元,20元,30元,60元,100元,200元 
        ////※对应点数：1元=1Q币=10Q点 
        ////【重要提示】 
        ////※请务必使用与您所选面额相同的Q币卡进行支付，否则您将承担因此而引起的交易失败或者交易金额丢失所造成的损失。 
        ////※注意：只支持Q币卡卡密支付，不支持QQ账户内Q币或Q点支付 
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isQQCARD(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{9}$";
            string cardpwdpatt = "^\\d{12}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isUNICOM
        /// <summary>
        ////        【支持卡种】 
        ////※联通全国卡，卡号15位阿拉伯数字，密码19位阿拉伯数字。 
        ////【支持面额】 
        ////20元、30元、50元、100元、300元、500元 
        ////【重要提示】 
        ////※请务必使用与您选择的面额相同的联通充值卡进行支付，否则引起的交易失败交易金额不予退还。 
        ////如：选择50元面额但使用100元卡支付，则系统认为实际支付金额为50元， 高于50元部分不予退还；选择50元面额但使用30元卡支付则系统认为支付失败， 30元不予退还。 
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isUNICOM(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{15}$";
            string cardpwdpatt = "^\\d{19}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isJIUYOU
        /// <summary>
        ////        【支持卡种】 
        ////※卡号13位、密码10位的阿拉伯数字 
        ////【支持面额】 
        ////※5元、10元、30元、50元 
        ////※对应点数：1元=100点 
        ////【重要提示】 
        ////※请务必使用与您所选择的面额相同的久游一卡通进行支付,否则引起的交易失败或交易金额丢失，我方不予承担！ 
        ////如：您选择10元面额但使用30元卡支付，则系统认为实际支付金额为10元，高于10元部分不予退还。 
        ////※不支持久游神兵卡、矩阵卡 
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isJIUYOU(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{13}$";
            string cardpwdpatt = "^\\d{10}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isNETEASE
        /// <summary>
        ////            【支持卡种】 
        ////※全国官方网易游戏充值卡，卡号13位、密码9位的阿拉伯数字 
        ////【支持面额】 
        ////实卡面值：15元、30元 
        ////虚卡面值：10元、15元、20元、30元、50元 
        ////对应点数：1元=10点 
        ////【重要提示】 
        ////※请务必使用与您所选择的面额相同的网易一卡通进行支付,否则引起的交易失败或交易金额丢失，我方不予承担！ 
        ////如：您选择10元面额但使用30元卡支付，则系统认为实际支付金额为10元，高于10元部分不予退还。 
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isNETEASE(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{13}$";
            string cardpwdpatt = "^\\d{9}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isWANMEI
        /// <summary>
        ////【支持卡种】 
        ////※全国官方完美游戏充值卡，卡号10位、密码15位的阿拉伯数字 
        ////【支持面额】 
        ////※15元、30元、50元、100元 
        ////※对应点数：1元=150点 
        ////【重要提示】 
        ////※请务必使用与您所选择的面额相同的完美一卡通进行支付,否则引起的交易失败或交易金额丢失，我方不予承担！ 
        ////如：您选择10元面额但使用30元卡支付，则系统认为实际支付金额为10元，高于10元部分不予退还。
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isWANMEI(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{10}$";
            string cardpwdpatt = "^\\d{15}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isSOHU
        /// <summary>
        //////        【支持卡种】 
        //////※卡号20位、密码12位的阿拉伯数字 
        //////【支持面额】 
        //////※5元、10元、15元、30元、40元、100元 
        //////※对应点数：1元=20点 
        //////【重要提示】 
        //////※请务必使用与您所选择的面额相同的搜狐一卡通进行支付,否则引起的交易失败或交易金额丢失，我方不予承担！ 
        //////如：您选择10元面额但使用30元卡支付，则系统认为实际支付金额为10元，高于10元部分不予退还。 
        //////※不支持搜狐矩阵卡。 
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isSOHU(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{20}$";
            string cardpwdpatt = "^\\d{12}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isTELECOM
        /// <summary>
        ////        【支持卡种】 
        ////※中国电信充值付费卡卡号19位，密码18位的阿拉伯数字（即：可拨打11888充值话费的卡）。 
        ////※目前只支持电信全国卡和广东卡，充值卡序列号第四位为“1”的卡为全国卡，为“2”的则为广东卡。 
        ////【支持面额】 
        ////※50元，100元 
        ////【重要提示】 
        ////※请务必使用与您所选择的面额相同的电信卡进行支付,否则引起的交易失败或交易金额丢失，我方不予承担！ 
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isTELECOM(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{19}$";
            string cardpwdpatt = "^\\d{18}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isZONGYOU
        /// <summary>
        ////      【支持卡种】 
        ////※卡号与密码均为15位阿拉伯数字。全国各地能买到纵游一卡通的地区，包括士多店、报刊亭、软件店、网吧、书店等。 
        ////【支持面额】 
        ////※5元、10元、15元、30元、50元、100元面值 
        ////【温馨提示】 
        ////※纵游一卡通支持分次消费直至卡内余额为0 
        ////※纵游一卡通可以充值多款游戏，在纵游一卡通官方网站中搜索到的产品，都是可以用纵游一卡通来充值的。
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isZONGYOU(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{15}$";
            string cardpwdpatt = "^\\d{15}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isTIANXIA
        /// <summary>
        ////【支持卡种】 
        ////※卡号是15位阿拉伯数字，密码是8位阿拉伯数字，所有实卡的自发行日起，两年内有效。 
        ////天下通有小量虚拟卡，虚拟卡只能充值指定游戏，兽血沸腾、龙腾世界、梦三国、梦幻龙族和炼狱。 
        ////【支持面额】 
        ////※5元、6元、10元、15元、30元、50元、100元面值 
        ////【温馨提示】 
        ////※天下通一卡通支持是余额对转，余额对转是将一张卡内的余额转入或转入到另张一卡通上。 
        ////※天下通有小量虚拟卡，虚拟卡只能充值指定游戏，兽血沸腾、龙腾世界、梦三国、梦幻龙族和炼狱。
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isTIANXIA(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^\\d{15}$";
            string cardpwdpatt = "^\\d{8}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region isTIANHONG
        /// <summary>
        ////        【支持卡种】 
        ////※卡号为12位，前2位是大写或小写英文字母，后10位是数字；密码15位是纯数字。 
        ////※卡号为10位，前2位是大写或小写英文字母，后8位是数字；密码10位是纯数字。 
        ////【支持面额】 
        ////※5元、10元、15元、20元、30元、50元、100元 
        ////【温馨提示】 
        ////※天宏一卡通所有实卡的自发行日起，两年内有效。可多次充值，直至卡内余额为零。
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <returns></returns>
        public static bool isTIANHONG(string cardno, string cardpwd)
        {
            bool result = false;

            string cardnopatt = "^[a-zA-Z]{2}\\d{10}$";
            string cardpwdpatt = "^\\d{15}$";

            if (QuickValidate(cardnopatt, cardno)
                && QuickValidate(cardpwdpatt, cardpwd))
            {
                result = true;
            }
            if (!result)
            {
                cardnopatt = "^[a-zA-Z]{2}\\d{8}$";
                cardpwdpatt = "^\\d{15}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion
        #endregion

        #region QuickValidate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_express"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static bool QuickValidate(string _express, string _value)
        {
            Regex regex = new Regex(_express, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (_value == null || _value.Length == 0)
            {
                return false;
            }
            return regex.IsMatch(_value);
        }
        #endregion
    }
}
