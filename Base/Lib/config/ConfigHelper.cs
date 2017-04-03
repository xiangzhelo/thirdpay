using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Xml;
using System.Web.Caching;
using viviLib.ExceptionHandling;
using System.Globalization;
///
namespace viviLib.Configuration
{

    /// <summary>
    /// 相关配置项操作类。
    /// </summary>
    public sealed class ConfigHelper
    {
        //private static CacheManager _cache = CacheFactory.GetCacheManager("Utility Configuration Cache Manager");
        private static readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configurations\runtimeconfiguration.config");
        private static readonly CultureInfo _culture = CultureInfo.CreateSpecificCulture("zh-CN");
        private ConfigHelper()
        {
        }

        /// <summary>
        /// 返回配置值。
        /// </summary>
        /// <param name="group">组。</param>
        /// <param name="key">键值。</param>
        /// <returns>配置值。</returns>
        public static string GetConfig(string group, string key)
        {
            return GetConfig(null, group, key);
        }

        /// <summary>
        /// 返回配置值。
        /// </summary>
        /// <param name="path">配置文件路径。</param>
        /// <param name="group">组。</param>
        /// <param name="key">键值。</param>
        /// <returns>配置值。</returns>
        public static string GetConfig(string path, string group, string key)
        {
            if ((key != null) && (key.Length != 0))
            {
                NameValueCollection configs = GetConfigs(path, group);
                if ((configs != null) && (configs[key] != null))
                {
                    return configs[key];
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 返回配置集合。
        /// </summary>
        /// <param name="group">组。</param>
        /// <returns>配置集合。</returns>
        public static NameValueCollection GetConfigs(string group)
        {
            return GetConfigs(null, group);
        }

        /// <summary>
        /// 返回配置集合。
        /// </summary>
        /// <param name="path">配置文件路径。</param>
        /// <param name="group">组。</param>
        /// <returns>配置集合。</returns>
        public static NameValueCollection GetConfigs(string path, string group)
        {
            if ((group != null) && (group.Length != 0))
            {
                if ((path == null) || (path.Trim().Length == 0))
                {
                    path = FilePath;
                }
                string key = group + "_" + path;
                if (System.Web.HttpRuntime.Cache.Get(key) != null)// Cache.Contains(key))
                {
                    NameValueCollection values = System.Web.HttpRuntime.Cache.Get(key) as NameValueCollection; //Cache[key] as NameValueCollection;
                    if (values != null)
                    {
                        return values;
                    }
                }
                try
                {
                    if (File.Exists(path))
                    {
                        NameValueCollection values2 = new NameValueCollection();
                        XmlDocument xmlDocument = GetXmlDocument(path);
                        if (xmlDocument != null)
                        {
                            foreach (XmlNode node in xmlDocument.SelectNodes("/configuration/" + group + "/add"))
                            {
                                values2.Add(node.Attributes["key"].Value, node.Attributes["value"].Value);
                            }
                        }
                        CacheDependency dep = new CacheDependency(path,DateTime.Now);
                        System.Web.HttpRuntime.Cache.Insert(key, values2, dep);
                        //DefaultCacheStrategy.instance.AddObjectWithDepend(key, values2, new string[] { path });
                       // CacheOperate.Insert(key, values2, new CacheDependency(path));  
                       // Cache.Add(key, values2, CacheItemPriority.Normal, null, new ICacheItemExpiration[] { new FileDependency(path) });
                        return values2;
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandler.HandleException(exception);
                }
            }
            return new NameValueCollection(0);
        }

        /// <summary>
        /// 从指定的路径返回XmlDocument对象。
        /// </summary>
        /// <param name="path">配置文件路径。</param>
        /// <returns>配置文件对应的XML文件。</returns>
        public static XmlDocument GetXmlDocument(string path)
        {
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(path);
                reader.MoveToContent();
                XmlDocument document = new XmlDocument();
                document.LoadXml(reader.ReadOuterXml());
                return document;
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return null;
        }

        /// <summary>
        /// 返回配置值。
        /// </summary>&gt;
        /// <param name="group">组。</param>
        /// <param name="list">配置值列表。</param>
        /// <returns>是否成功写入。</returns>
        public static bool WriteConfig(string group, NameValueCollection list)
        {
            return WriteConfig(null, group, list);
        }

        /// <summary>
        /// 返回配置值。
        /// </summary>
        /// <param name="path">配置文件路径。</param>
        /// <param name="group">组。</param>
        /// <param name="list">配置值列表。</param>
        /// <returns>是否成功写入。</returns>
        public static bool WriteConfig(string path, string group, NameValueCollection list)
        {
            if ((path == null) || (path.Trim().Length == 0))
            {
                path = FilePath;
            }
            XmlDocument xmlDocument = GetXmlDocument(path);
            if (xmlDocument == null)
            {
                xmlDocument = new XmlDocument();
                xmlDocument.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration></configuration>");
            }
            XmlNode documentElement = xmlDocument.DocumentElement;
            XmlNode newChild = xmlDocument.SelectSingleNode("configuration/" + group);
            if (newChild == null)
            {
                newChild = xmlDocument.CreateElement(group);
                documentElement.AppendChild(newChild);
            }
            XmlNodeList list2 = xmlDocument.SelectNodes("/configuration/" + group + "/add");
            for (int i = 0; i < list.AllKeys.Length; i++)
            {
                bool flag = false;
                foreach (XmlElement element in list2)
                {
                    if (element.Attributes["key"].Value == list.AllKeys[i])
                    {
                        element.SetAttribute("value", list[list.AllKeys[i]]);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    XmlElement element2 = xmlDocument.CreateElement("add");
                    element2.SetAttribute("key", list.AllKeys[i]);
                    element2.SetAttribute("value", list[list.AllKeys[i]]);
                    newChild.AppendChild(element2);
                }
            }
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.Write(xmlDocument.OuterXml);
                writer.Close();
            }
            return true;
        }       

        /// <summary>
        /// 返回配置文件路径。
        /// </summary>
        public static string FilePath
        {
            get
            {
                return _filePath;
            }
        }

        /// <summary>
        /// 默认区域信息。
        /// </summary>
        public static CultureInfo DefaultCulture
        {
            get
            {
                return _culture;
            }
        }
    }
}

