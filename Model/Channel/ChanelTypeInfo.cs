using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Channel
{
    /// <summary>
    /// 
    /// </summary>
    public enum EnumChannelType
    {
        None = 0,
        �Ƹ�ͨ = 100,
        ֧���� = 101,
        �������� = 102,
        �����г�ֵ�� = 103,
        ʢ��һ��ͨ = 104,
        ��;֧���� = 105,
        ����һ��ͨ = 106,
        ��ѶQ�ҿ� = 107,
        ��ͨ��ֵ�� = 108,
        ����һ��ͨ = 109,
        ����һ��ͨ = 110,
        ����һ��ͨ = 111,
        �Ѻ�һ��ͨ = 112,
        ���ų�ֵ�� = 113,
        ��Ѷ�� = 114,
        ����һ��ͨ = 115,
        ��ɽһ��ͨ = 116,
        ����һ��ͨ = 117,
        ����һ��ͨ = 118,
        ���һ��ͨ = 119,
        �������㽭�� = 200,
        �����н��տ� = 201,
        ������������ = 202,
        �����и����� = 203,
        ħ�޿� = 204,
        ������ = 205,
        Ź��һ��ͨ = 208,
        ����һ��ͨר�� = 209,
        ���� = 300
    }

    /// <summary>
    /// ͨ�����
    /// </summary>
    [Serializable]
    public class ChannelTypeInfo
    {
        private int _id;
        private ChannelClassEnum _class;
        private string _modetypename;
        private string _code=string.Empty;
        private int _typeId;
        private OpenEnum _isopen;
        private DateTime _addtime;
        private int? _sort;
        private bool _release;
        private int _supplier;
        private decimal _supprate = 0M;
        private int _runmode = 0;
        private string _runset = string.Empty;
        private int _supplier2;
        private string _suppsWhenExceOccurred = string.Empty;
        private int _timeout;


        public int timeout
        {
            set { _timeout = value; }
            get { return _timeout; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int typeId
        {
            set { _typeId = value; }
            get { return _typeId; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string code
        {
            set { _code = value; }
            get { return _code; }
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
        /// 
        /// </summary>
        public int supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        /// <summary>
        /// ֧��ͨ����� 1 ���� 2��Ϸ�㿨 3��ֵ��
        /// </summary>
        public ChannelClassEnum Class
        {
            set { _class = value; }
            get { return _class; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string modetypename
        {
            set { _modetypename = value; }
            get { return _modetypename; }
        }
        
        /// <summary>
        /// �Ƿ���
        /// </summary>
        public OpenEnum isOpen
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
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        public bool release
        {
            set { _release = value; }
            get { return _release; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int runmode
        {
            set { _runmode = value; }
            get { return _runmode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string runset
        {
            set { _runset = value; }
            get { return _runset; }
        }

        /// <summary>
        /// �����ӿ� ���ӿ� ���س�ʱ�л�ͨ�����ύ
        /// </summary>
        public int supplier2
        {
            set { _supplier2 = value; }
            get { return _supplier2; }
        }

        /// <summary>
        /// �����ӿ��ύ��������ʱ�л�ͨ��
        /// </summary>
        public string SuppsWhenExceOccurred
        {
            set { _suppsWhenExceOccurred = value; }
            get { return _suppsWhenExceOccurred; }
        }
    }
}
