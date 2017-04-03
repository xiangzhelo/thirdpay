using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model.User
{
    public enum IdImageStatus
    { 
        未知 = 0,
        审核中 =1,
        审核成功 = 2,
        审核失败 = 4
    }
    /// <summary>
    /// 
    /// </summary>
    public class usersIdImageInfo
    {
        private int _id;
        private int? _userid;
        private byte[] _image_on;
        private byte[] _image_down;
        private string _ptype;
        private int? _filesize;
        private string _ptype1;
        private int? _filesize1;
        private IdImageStatus _status;
        private string _why;
        private int? _admin;
        private DateTime? _checktime;
        private DateTime? _addtime;
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
        public int? userId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] image_on
        {
            set { _image_on = value; }
            get { return _image_on; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] image_down
        {
            set { _image_down = value; }
            get { return _image_down; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ptype
        {
            set { _ptype = value; }
            get { return _ptype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? filesize
        {
            set { _filesize = value; }
            get { return _filesize; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ptype1
        {
            set { _ptype1 = value; }
            get { return _ptype1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? filesize1
        {
            set { _filesize1 = value; }
            get { return _filesize1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public IdImageStatus status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string why
        {
            set { _why = value; }
            get { return _why; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? admin
        {
            set { _admin = value; }
            get { return _admin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? checktime
        {
            set { _checktime = value; }
            get { return _checktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
    }
}
