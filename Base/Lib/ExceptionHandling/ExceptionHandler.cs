using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using viviLib.Logging;
using viviLib.Configuration;
using viviLib.Web;

namespace viviLib.ExceptionHandling
{
    /// <summary>
    /// ExceptionHandler 的摘要说明。
    /// </summary>
    public sealed class ExceptionHandler
    {
        private static readonly string[] IgnoredProperties = new string[] { "Source", "Message", "HelpLink", "InnerException", "StackTrace" };
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("ExceptionsLogger");

        /// <summary>
        /// 构造函数。
        /// </summary>
        private ExceptionHandler()
        {
        }

        /// <summary>
        /// Writes the name and value of the specified
        /// field to the underlying text stream.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        private static string GetFieldInfo(FieldInfo field, object fieldValue)
        {
            return string.Format("{0} : {1}", field.Name, fieldValue);
        }

        /// <summary>
        /// Writes the name and value of the specified property
        /// to the underlying text stream.
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        private static string GetPropertyInfo(PropertyInfo propertyInfo, object propertyValue)
        {
            return string.Format("{0} : {1}", propertyInfo.Name, propertyValue);
        }

        /// <summary>
        /// Formats an exception using reflection.
        /// </summary>
        /// <param name="e">
        /// The exception to be formatted.
        /// </param>
        /// <remarks>
        /// <para>This method reflects over the public, instance properties 
        /// and public, instance fields
        /// of the specified exception and prints them to the formatter.  
        /// Certain property names are ignored
        /// because they are handled explicitly in other places.</para>
        /// </remarks>
        private static string GetReflectionInfo(Exception e)
        {
            object obj2;
            StringBuilder builder = new StringBuilder();
            Type type = e.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo info in properties)
            {
                if (info.CanRead && (Array.IndexOf(IgnoredProperties, info.Name) == -1))
                {
                    obj2 = info.GetValue(e, null);
                    builder.Append(GetPropertyInfo(info, obj2));
                    builder.Append("\r\n");
                }
            }
            foreach (FieldInfo info2 in fields)
            {
                obj2 = info2.GetValue(e);
                builder.Append(GetFieldInfo(info2, obj2));
                builder.Append("\r\n");
            }
            return builder.ToString();
        }
       
        /// <summary>
        /// 处理异常。
        /// </summary>
        /// <param name="ex">需处理异常信息。</param>
        public static void HandleException(Exception ex)
        {
            if ((ex != null) && LogSetting.ExceptionLogEnabled)
            {
                try
                {
                    if ((((WebBase.Context == null) || System.IO.File.Exists(WebBase.Server.MapPath(WebBase.Request.Path))) 
                        || (!(ex is HttpException) 
                        || (ex.InnerException == null))) 
                        || !(ex.InnerException is FileNotFoundException))
                    {
                        string path = LogSetting.ExceptionLogFilePath(DateTime.Today);

                        string str = string.Format("Path              = {0}\r\nTime              = {1}\r\nClientIP          = {2}\r\nType              = {3}\r\nMessage           = {4}\r\nSource            = {5}\r\nHelpLink          = {6}\r\nReflectionInfo    = {7}\r\nStackTrace        = {8}"
                            , new object[] { (WebBase.Context != null) ? WebBase.Request.RawUrl : string.Empty
                                , string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                                , (WebBase.Context != null) ? ServerVariables.TrueIP : string.Empty
                                , ex.GetType().AssemblyQualifiedName
                                , ex.Message
                                , ex.Source
                                , ex.HelpLink
                                , GetReflectionInfo(ex).Replace("\n", "\n" + new string(' ', 0x18))
                                , ex.StackTrace.Replace("\n", "\n" + new string(' ', 0x18)) });

                        log.Fatal(str);

                        if (ex.InnerException != null)
                        {
                            HandleException(ex.InnerException);
                        }
                    }
                }
                catch
                {
                }
            }
        }
    }
}

