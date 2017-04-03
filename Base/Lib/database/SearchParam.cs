using System;
using System.Collections.Generic;
using System.Text;

namespace viviLib.Data
{
    public class SearchParam
    {
        private string _cmpOperator;
        private string _paramKey;
        private object _paramValue;

        /// <summary>
        /// 
        /// </summary>
        public SearchParam()
        {
            this._paramKey = string.Empty;
            this._cmpOperator = "=";
            this._paramValue = null;
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="paramKey">参数。</param>
        /// <param name="paramValue">参数值。</param>
        public SearchParam(string paramKey, object paramValue)
        {
            this._paramKey = string.Empty;
            this._cmpOperator = "=";
            this._paramValue = null;
            this.ParamKey = paramKey;
            this.ParamValue = paramValue;
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="paramKey">参数。</param>
        /// <param name="cmpOperator">比较符。</param>
        /// <param name="paramValue">参数值。</param>
        public SearchParam(string paramKey, string cmpOperator, object paramValue)
        {
            this._paramKey = string.Empty;
            this._cmpOperator = "=";
            this._paramValue = null;
            this.ParamKey = paramKey;
            this.CmpOperator = cmpOperator;
            this.ParamValue = paramValue;
        }

        /// <summary>
        /// 比较符。
        /// </summary>
        public string CmpOperator
        {
            get
            {
                return this._cmpOperator;
            }
            set
            {
                this._cmpOperator = value;
            }
        }

        /// <summary>
        /// 参数。
        /// </summary>
        public string ParamKey
        {
            get
            {
                return this._paramKey;
            }
            set
            {
                this._paramKey = value;
            }
        }

        /// <summary>
        /// 参数值。
        /// </summary>
        public object ParamValue
        {
            get
            {
                return this._paramValue;
            }
            set
            {
                this._paramValue = value;
            }
        }
    }
}
