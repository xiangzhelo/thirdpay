using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model
{
    public partial class phoneValid
    {
        public phoneValid()
        { }
        #region Model

        private int _id;
        private string _phone;
        private int _count;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// �ֻ�����
        /// </summary>
        public string phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        /// <summary>
        /// �ܷ��Ͷ��Ŵ���
        /// </summary>
        public int count
        {
            set { _count = value; }
            get { return _count; }
        }
        #endregion Model

    }
}
