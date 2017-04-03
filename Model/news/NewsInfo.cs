using System;

namespace viviapi.Model.News
{
    /// <summary>
    /// 
    /// </summary>
    public class NewsInfo
    {
        #region Model
        private int _newsid;
        private NewsType _newstype;
        private string _newstitle;
        private DateTime _addtime;
        private string _newscontent;
        private int _isred;
        private int _istop;
        private int _ispop;
        private int _isbold = 0;
        private string _color;

        /// <summary>
        /// 
        /// </summary>
        public int newsid
        {
            set { _newsid = value; }
            get { return _newsid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public NewsType newstype
        {
            set { _newstype = value; }
            get { return _newstype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string newstitle
        {
            set { _newstitle = value; }
            get { return _newstitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime addTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string newscontent
        {
            set { _newscontent = value; }
            get { return _newscontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsRed
        {
            set { _isred = value; }
            get { return _isred; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsPop
        {
            set { _ispop = value; }
            get { return _ispop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Isbold
        {
            set { _isbold = value; }
            get { return _isbold; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Color
        {
            set { _color = value; }
            get { return _color; }
        }

        //ÊÇ·ñ·¢²¼
        public bool release { get; set; }
        #endregion Model
    }
}
