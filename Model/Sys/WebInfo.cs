using System;

namespace viviapi.Model
{
    [Serializable]
    public class WebInfo
    {
        private string _author;
        private string _code;
        private string _description;
        private string _domain;
        private string _footer;
        private int _id;
        private string _jsqq;
        private string _keywords;
        private string _kfqq;
        private string _logoPath;
        private string _name;
        private string _payurl;
        private string _phone;
        private string _templateid;
        private bool _mobilval = false;
        private int _MaxSendTimes = 0;

        private string _apibankname = string.Empty;
        private string _apibankversion = string.Empty;
        private string _apicardname = string.Empty;
        private string _apicardversion = string.Empty;

        /// <summary>
        /// 网银接口名称
        /// </summary>
        public string apibankname
        {
            get
            {
                return this._apibankname;
            }
            set
            {
                this._apibankname = value;
            }
        }

        /// <summary>
        /// 网银接口版本
        /// </summary>
        public string apibankversion
        {
            get
            {
                return this._apibankversion;
            }
            set
            {
                this._apibankversion = value;
            }
        }


        /// <summary>
        /// 卡类接口名称
        /// </summary>
        public string apicardname
        {
            get
            {
                return this._apicardname;
            }
            set
            {
                this._apicardname = value;
            }
        }

        /// <summary>
        /// 卡类接口版本
        /// </summary>
        public string apicardversion
        {
            get
            {
                return this._apicardversion;
            }
            set
            {
                this._apicardversion = value;
            }
        }



        /// <summary>
        /// 是否需要手机验证
        /// </summary>
        public bool Mobilval
        {
            get
            {
                return this._mobilval;
            }
            set
            {
                this._mobilval = value;
            }
        }

        //一个手机号码是最多可以发送多少条短信
        public int MaxSendTimes
        {
            get
            {
                return this._MaxSendTimes;
            }
            set
            {
                this._MaxSendTimes = value;
            }
        }

        public string Code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string Domain
        {
            get
            {
                return this._domain;
            }
            set
            {
                this._domain = value;
            }
        }

        public string Footer
        {
            get
            {
                return this._footer;
            }
            set
            {
                this._footer = value;
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string Jsqq
        {
            get
            {
                return this._jsqq;
            }
            set
            {
                this._jsqq = value;
            }
        }

        public string Kfqq
        {
            get
            {
                return this._kfqq;
            }
            set
            {
                this._kfqq = value;
            }
        }

        public string LogoPath
        {
            get
            {
                return this._logoPath;
            }
            set
            {
                this._logoPath = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string PayUrl
        {
            get
            {
                return this._payurl;
            }
            set
            {
                this._payurl = value;
            }
        }

        public string Phone
        {
            get
            {
                return this._phone;
            }
            set
            {
                this._phone = value;
            }
        }

        public string TemplateId
        {
            get
            {
                return this._templateid;
            }
            set
            {
                this._templateid = value;
            }
        }
    }
}

