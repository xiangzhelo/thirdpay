using System;
using System.Collections.Generic;
using System.Text;

namespace viviapi.Model.Channel
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CodeMappingInfo
    {
        private int _id;
        private string _pmodecode;
        private int _suppid;
        private string _suppcode;

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pmodeCode
        {
            set { _pmodecode = value; }
            get { return _pmodecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int suppId
        {
            set { _suppid = value; }
            get { return _suppid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string suppCode
        {
            set { _suppcode = value; }
            get { return _suppcode; }
        }
    }
}
