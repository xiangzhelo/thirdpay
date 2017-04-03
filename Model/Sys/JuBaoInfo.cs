using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viviapi.Model
{
    [Serializable]
    public class JuBaoInfo
    {
        #region Model
        private int _id;
		private string _name;
		private string _email;
		private string _tel;
		private string _url;
        private JuBaoEnum _type;
		private string _remark;
		private DateTime? _addtime;
        private JuBaoStatusEnum _status;
		private DateTime? _checktime;
		private int? _check;
		private string _checkremark;
		private string _pwd;
		private string _field1;
		private string _field2;
		private string _field3;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 
		/// </summary>
        public JuBaoEnum type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? addtime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
        public JuBaoStatusEnum status
        {
            set { _status = value; }
            get { return _status; }
        }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? checktime
		{
			set{ _checktime=value;}
			get{return _checktime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? check
		{
			set{ _check=value;}
			get{return _check;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string checkremark
		{
			set{ _checkremark=value;}
			get{return _checkremark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pwd
		{
			set{ _pwd=value;}
			get{return _pwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string field1
		{
			set{ _field1=value;}
			get{return _field1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string field2
		{
			set{ _field2=value;}
			get{return _field2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string field3
		{
			set{ _field3=value;}
			get{return _field3;}
		}
		#endregion Model
    }
}
