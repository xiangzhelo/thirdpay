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
        /// ���캯����
        /// </summary>
        /// <param name="paramKey">������</param>
        /// <param name="paramValue">����ֵ��</param>
        public SearchParam(string paramKey, object paramValue)
        {
            this._paramKey = string.Empty;
            this._cmpOperator = "=";
            this._paramValue = null;
            this.ParamKey = paramKey;
            this.ParamValue = paramValue;
        }

        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="paramKey">������</param>
        /// <param name="cmpOperator">�ȽϷ���</param>
        /// <param name="paramValue">����ֵ��</param>
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
        /// �ȽϷ���
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
        /// ������
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
        /// ����ֵ��
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
