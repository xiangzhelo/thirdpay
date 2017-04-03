using System;
using System.Web.Caching;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using viviLib.Configuration;
using viviLib.ExceptionHandling;

namespace viviLib.ScheduledTask
{  
    /// <summary>
    /// ScheduledTaskConfigurationSectionHandler 的摘要说明。
    /// </summary>
    public class ScheduledTaskConfigurationSectionHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// 由所有配置节处理程序实现，以分析配置节的 XML。返回的对象被添加到配置集合中，并通过 GetConfig 访问。
        /// </summary>
        /// <param name="parent">对应父配置节中的配置设置。 </param>
        /// <param name="configContext">在从 ASP.NET 配置系统中调用 Create 时为 HttpConfigurationContext。否则，该参数是保留参数，并且为空引用（Visual Basic 中为 Nothing）。 </param>
        /// <param name="section">一个 XmlNode，它包含配置文件中的配置信息。提供对配置节 XML 内容的直接访问。</param>
        /// <returns>配置对象。</returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            return Parse(section);
        }

        protected static System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;

        /// <summary>
        /// 分析配置节的 XML。
        /// </summary>
        /// <returns>配置对象。</returns>
        public static List<ScheduledTaskConfiguration> GetConfigs()
        {
            string filePath = ConfigHelper.FilePath;
            string xpath = string.Format("/configuration/scheduledTaskConfiguration", new object[0]);
            string key = filePath + "|" + xpath;
            if (webCache[key] != null)
            {
                List<ScheduledTaskConfiguration> configurations = webCache[key] as List<ScheduledTaskConfiguration>;
                if (configurations != null)
                {
                    return configurations;
                }
            }
            try
            {
                if (File.Exists(filePath))
                {
                    List<ScheduledTaskConfiguration> configurations2 = new List<ScheduledTaskConfiguration>();
                    XmlDocument xmlDocument = ConfigHelper.GetXmlDocument(filePath);
                    if (xmlDocument != null)
                    {
                        XmlNode section = xmlDocument.SelectSingleNode(xpath);
                        if (section != null)
                        {
                            configurations2 = Parse(section);
                        }
                    }
                    webCache.Add(key, configurations2, new CacheDependency(filePath), DateTime.Now.AddDays(10), TimeSpan.Zero, CacheItemPriority.Default, null);
//                    ConfigHelper.Cache.Add(key, configurations2, CacheItemPriority.Normal, null, new ICacheItemExpiration[] { new FileDependency(filePath) });
                    return configurations2;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
            return new List<ScheduledTaskConfiguration>(0);
        }

        /// <summary>
        /// 分析配置节的 XML。
        /// </summary>
        /// <param name="section">一个 XmlNode，它包含配置文件中的配置信息。提供对配置节 XML 内容的直接访问。</param>
        /// <returns>配置对象。</returns>
        public static List<ScheduledTaskConfiguration> Parse(XmlNode section)
        {
            List<ScheduledTaskConfiguration> configurations = new List<ScheduledTaskConfiguration>();
            foreach (XmlNode node in section.ChildNodes)
            {
                if (node.Name == "scheduledTask")
                {
                    ScheduledTaskConfiguration configuration = new ScheduledTaskConfiguration();
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        string name = attribute.Name;
                        if (name == null)
                        {
                            continue;
                        }
                        name = string.IsInterned(name);
                        if (name == "ScheduledTaskType")
                        {
                            configuration.ScheduledTaskType = attribute.Value;
                            continue;
                        }
                        if (name == "ThreadSleepSecond")
                        {
                            configuration.ThreadSleepSecond = Convert.ToInt32(attribute.Value, 10);
                        }
                    }
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name == "execute")
                        {
                            foreach (XmlAttribute attribute2 in node2.Attributes)
                            {
                                if (attribute2.Name == "type")
                                {
                                    configuration.Executes.Add(attribute2.Value);
                                    break;
                                }
                            }
                            continue;
                        }
                    }
                    configurations.Add(configuration);
                }
            }
            return configurations;
        }
    }
}

