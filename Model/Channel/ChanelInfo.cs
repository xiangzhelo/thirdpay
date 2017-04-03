using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Channel
{
    /// <summary>
    /// ͨ������
    /// </summary>
    [Serializable]
    public class ChannelInfo
    {
        private int _id;
        private string _code;
        private int _typeid;
        private int? _supplier;
        
        private string _modename;
        private string _modeenname;
        private int _facevalue;
        private int? _isopen;
        private DateTime _addtime;
        private int? _sort;
        private decimal _supprate = 0M;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ͨ������
        /// </summary>
        public string code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// ͨ�����
        /// </summary>
        public int typeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string modeName
        {
            set { _modename = value; }
            get { return _modename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string modeEnName
        {
            set { _modeenname = value; }
            get { return _modeenname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int faceValue
        {
            set { _facevalue = value; }
            get { return _facevalue; }
        }
        /// <summary>
        /// ��Ӧ�̷���
        /// </summary>
        public decimal supprate
        {
            set { _supprate = value; }
            get { return _supprate; }
        }
        /// <summary>
        /// �Ƿ���
        /// </summary>
        public int? isOpen
        {
            set { _isopen = value; }
            get { return _isopen; }
        }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
    }
}
