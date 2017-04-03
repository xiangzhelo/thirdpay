using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace viviLib
{
    [Serializable]
    public class PageData
    {
       // private ArrayList _items = new ArrayList();
        private DataSet _items = new DataSet();
        private int _recordCount = 0;

        /// <summary>
        /// ��ǰҳ���ݼ���
        /// </summary>
        public DataSet Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
            }
        }

        ///// <summary>
        ///// ��ǰҳ���ݼ���
        ///// </summary>
        //public ArrayList Items
        //{
        //    get
        //    {
        //        return this._items;
        //    }
        //}

        /// <summary>
        /// ���м�¼������
        /// </summary>
        public int RecordCount
        {
            get
            {
                return this._recordCount;
            }
            set
            {
                this._recordCount = value;
            }
        }
    }
}
