namespace viviapi.BLL
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Xml;
    using viviapi.Model;
    public class templateFactory
    {
        public static List<template> AllTemplate()
        {
            List<template> config = HttpRuntime.Cache.Get("TemplateConfiguration") as List<template>;
            if (config == null)
            {
                List<template> _template = new List<template>();
                string[] dirs = Directory.GetFiles(HttpContext.Current.Server.MapPath("/Template/"), "*.xml", SearchOption.AllDirectories);
                foreach (string _dir in dirs)
                {
                    _template.Add(Get(_dir));
                }
                config = _template;
                HttpRuntime.Cache.Insert("TemplateConfiguration", config);
            }
            return config;
        }

        public static template Get(string dir)
        {
            template _temp = new template();
            XmlNodeList xnl = GetConfig(dir).SelectSingleNode("about").ChildNodes;
            foreach (XmlNode xnf in xnl)
            {
                XmlElement xe = (XmlElement) xnf;
                _temp.ID = xe.GetAttribute("ID");
                _temp.Name = xe.GetAttribute("name");
                _temp.Author = xe.GetAttribute("author");
                _temp.Createdate = xe.GetAttribute("createdate");
                _temp.IsAgent = xe.GetAttribute("isAgent");
                _temp.Copyright = xe.GetAttribute("copyright");
                _temp.Photo = _temp.ID + "/about.jpg";
                _temp.Bigphoto = _temp.ID + "/bigabout.jpg";
            }
            return _temp;
        }

        public static XmlDocument GetConfig(string dir)
        {
            string filename = dir;
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }
            XmlDocument xmlDom = new XmlDocument();
            xmlDom.Load(filename);
            return xmlDom;
        }
    }
}

