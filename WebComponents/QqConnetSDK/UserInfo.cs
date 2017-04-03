using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.WebComponents.QqConnetSDK
{
    /// <summary>
    /// UserInfo  用户实体类
    /// </summary>
    #region UserInfo
    [Serializable]
    public class QQUserInfo
    {
        public string ret { get; set; }
        public string msg { get; set; }
        public string is_lost { get; set; }
        public string nickname { get; set; }
        public string gender { get; set; }
        public string figureurl { get; set; }

        public string figureurl_1 { get; set; }
        public string figureurl_2 { get; set; }
        public string figureurl_qq_1 { get; set; }
        public string figureurl_qq_2 { get; set; }
        public string is_yellow_vip { get; set; }
        public string vip { get; set; }
        public string yellow_vip_level { get; set; }
        public string level { get; set; }
        public string is_yellow_year_vip { get; set; }
    }
    #endregion
}
