using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.User
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class QuestionInfo
    {
        private int _id;
        private string _question;
        private bool _release;
        private int _sort;

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
        public string question
        {
            set { _question = value; }
            get { return _question; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool release
        {
            set { _release = value; }
            get { return _release; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
    }
}
