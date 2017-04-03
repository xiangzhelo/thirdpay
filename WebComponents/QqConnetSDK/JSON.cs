using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace viviapi.WebComponents.QqConnetSDK
{
    /// <summary>
    /// JSON 分析类
    /// </summary>
    #region JSON
    public class JSON
    {
        private string _json;
        /// <summary>
        /// 传入Json
        /// </summary>
        /// <param name="json"></param>
        public JSON(string json)
        {
            this._json = json;

        }
        /// <summary>
        /// 获取指定名称的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            try
            {
                string value = this._json.Split(new string[] { "" + name + "\":\"" }, StringSplitOptions.RemoveEmptyEntries)[1];
                value = value.Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];
                return value;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 转化为提供的实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="NewT"></param>
        /// <returns></returns>
        public T Convert<T>(T NewT)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                string Name = propertyInfo.Name;
                object value = this.GetValue(Name);
                if (value == null) { NewT = default(T); break; }
                propertyInfo.SetValue(NewT, value, null); //默认字符型处理.
            }
            return NewT;
        }
    }
    #endregion
}
