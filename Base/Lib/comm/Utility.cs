using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
//using viviapi.SysConfig;

namespace viviLib
{
    public class Utility
    {
        public static string ChkSQL(string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Replace("'", "''");
            return str;
        }

        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }

        public static string ClearBR(string str)
        {
            Regex regex = null;
            Match match = null;
            regex = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);
            for (match = regex.Match(str); match.Success; match = match.NextMatch())
            {
                str = str.Replace(match.Groups[0].ToString(), "");
            }
            return str;
        }

        public static string ClearHtml(string strHtml)
        {
            if (strHtml != "")
            {
                Regex regex = null;
                Match match = null;
                regex = new Regex(@"<\/?[^>]*>", RegexOptions.IgnoreCase);
                for (match = regex.Match(strHtml); match.Success; match = match.NextMatch())
                {
                    strHtml = strHtml.Replace(match.Groups[0].ToString(), "");
                }
            }
            return strHtml;
        }

        public static bool CreateDir(string name)
        {
            return MakeSureDirectoryPathExists(name);
        }

        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }

        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length *= -1;
                    if ((startIndex - length) < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex -= length;
                    }
                }
                if (startIndex > str.Length)
                {
                    return "";
                }
            }
            else if ((length >= 0) && ((length + startIndex) > 0))
            {
                length += startIndex;
                startIndex = 0;
            }
            else
            {
                return "";
            }
            if ((str.Length - startIndex) < length)
            {
                length = str.Length - startIndex;
            }
            try
            {
                return str.Substring(startIndex, length);
            }
            catch
            {
                return str;
            }
        }

       

        public static string EncodeHtml(string strHtml)
        {
            if (strHtml != "")
            {
                strHtml = strHtml.Replace(",", "&def");
                strHtml = strHtml.Replace("'", "&dot");
                strHtml = strHtml.Replace(";", "&dec");
                return strHtml;
            }
            return "";
        }

        
        public static bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        public static string[] FindNoUTF8File(string Path)
        {
            StringBuilder builder = new StringBuilder();
            FileInfo[] files = new DirectoryInfo(Path).GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension.ToLower().Equals(".htm"))
                {
                    FileStream sbInputStream = new FileStream(files[i].FullName, FileMode.Open, FileAccess.Read);
                    bool flag = IsUTF8(sbInputStream);
                    sbInputStream.Close();
                    if (!flag)
                    {
                        builder.Append(files[i].FullName);
                        builder.Append("\r\n");
                    }
                }
            }
            return SplitString(builder.ToString(), "\r\n");
        }

        public static string FormatBytesStr(int bytes)
        {
            double num;
            if (bytes > 0x40000000)
            {
                num = bytes / 0x40000000;
                return (num.ToString("0") + "G");
            }
            if (bytes > 0x100000)
            {
                num = bytes / 0x100000;
                return (num.ToString("0") + "M");
            }
            if (bytes > 0x400)
            {
                num = bytes / 0x400;
                return (num.ToString("0") + "K");
            }
            return (bytes.ToString() + "Bytes");
        }

        public static string GetAssemblyCopyright()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalCopyright;
        }

        public static string GetAssemblyProductName()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductName;
        }

        public static string GetAssemblyVersion()
        {
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            return string.Format("{0}.{1}.{2}", versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart);
        }

        public static string GetCookie(string strName)
        {
            if ((HttpContext.Current.Request.Cookies != null) && (HttpContext.Current.Request.Cookies[strName] != null))
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }

        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
            {
                return replacestr;
            }
            if (datetimestr.Equals(""))
            {
                return replacestr;
            }
            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }

        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays((double) relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }

        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] strArray = url.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].Split(new char[] { '?' })[0];
        }

        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }

        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else if (strSearch == stringArray[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        public static string GetPageNumbers(int page, int pageSize, int Count, string Url)
        {
            int num3;
            string str = "";
            int num = page - 1;
            int num2 = page + 1;
            int num4 = (int) Math.Ceiling((double) (((double) Count) / ((double) pageSize)));
            object obj2 = str;
            str = string.Concat(new object[] { obj2, "<span>页码：", page, "/", num4, "</span>" });
            if (num < 1)
            {
                str = str + "<span title='首页'>首页</span>" + "<span title='上一页'>上一页</span>";
            }
            else
            {
                obj2 = str + "<span title='首页'><a href='" + Url + "=1'>首页</a></span>";
                str = string.Concat(new object[] { obj2, "<span title='上一页'><a href='", Url, "=", num, "'>上一页</a></span>" });
            }
            if ((page % pageSize) == 0)
            {
                num3 = (page - pageSize) + 1;
            }
            else
            {
                num3 = (page - (page % pageSize)) + 1;
            }
            if (num3 > pageSize)
            {
                obj2 = str;
                str = string.Concat(new object[] { obj2, "<span title='前", pageSize, "页'><a href='", Url, "=", num3 - 1, "'>...</a></span>" });
            }
            for (int i = num3; i < (num3 + pageSize); i++)
            {
                if (i > num4)
                {
                    break;
                }
                if (i == page)
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, "<span title='页 ", i, "'> <font color='#ff0000'>[", i, "]</font> </span>" });
                }
                else
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, "<span title='页 ", i, "'> <a href='", Url, "=", i, "'>[", i, "]</a> </span>" });
                }
            }
            if (num4 >= (num3 + pageSize))
            {
                obj2 = str;
                str = string.Concat(new object[] { obj2, "<span title='后", pageSize, "页'><a href='", Url, "=", num3 + pageSize, "'>...</a></span>" });
            }
            if (num2 > num4)
            {
                return (str + "<span title='下一页'>下一页</span>" + "<span title='末页'>末页</span>");
            }
            obj2 = str;
            obj2 = string.Concat(new object[] { obj2, "<span title='下一页'><a href='", Url, "=", num2, "'>下一页</a></span>" });
            return string.Concat(new object[] { obj2, "<span title='末页'><a href='", Url, "=", num4, "'>末页</a></span>" });
        }

        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag, string anchor)
        {
            if (pagetag == "")
            {
                pagetag = "page";
            }
            int num = 1;
            int num2 = 1;
            if (url.IndexOf("?") > 0)
            {
                url = url + "&";
            }
            else
            {
                url = url + "?";
            }
            string str = "<a href=\"" + url + "&" + pagetag + "=1";
            string str2 = string.Concat(new object[] { "<a href=\"", url, "&", pagetag, "=", countPage });
            if (anchor != null)
            {
                str = str + anchor;
                str2 = str2 + anchor;
            }
            str = str + "\">第一页</a>";
            str2 = str2 + "\">最后一页</a>";
            if (countPage < 1)
            {
                countPage = 1;
            }
            if (extendPage < 3)
            {
                extendPage = 2;
            }
            if (countPage > extendPage)
            {
                if ((curPage - (extendPage / 2)) > 0)
                {
                    if ((curPage + (extendPage / 2)) < countPage)
                    {
                        num = curPage - (extendPage / 2);
                        num2 = (num + extendPage) - 1;
                    }
                    else
                    {
                        num2 = countPage;
                        num = (num2 - extendPage) + 1;
                        str2 = "";
                    }
                }
                else
                {
                    num2 = extendPage;
                    str = "";
                }
            }
            else
            {
                num = 1;
                num2 = countPage;
                str = "";
                str2 = "";
            }
            StringBuilder builder = new StringBuilder("");
            builder.Append(str);
            for (int i = num; i <= num2; i++)
            {
                if (i == curPage)
                {
                    builder.Append("<span>");
                    builder.Append(i);
                    builder.Append("</span>");
                }
                else
                {
                    builder.Append("<a href=\"");
                    builder.Append(url);
                    builder.Append(pagetag);
                    builder.Append("=");
                    builder.Append(i);
                    if (anchor != null)
                    {
                        builder.Append(anchor);
                    }
                    builder.Append("\">");
                    if ((num > 1) && (i == num))
                    {
                        builder.Append("...");
                    }
                    builder.Append(i);
                    if ((num2 < countPage) && (i == num2))
                    {
                        builder.Append("...");
                    }
                    builder.Append("</a>");
                }
            }
            builder.Append(str2);
            return builder.ToString();
        }

        public static string GetPostPageNumbers(int countPage, string url, string expname, int extendPage)
        {
            int num = 1;
            int num2 = 1;
            int num3 = 1;
            string str = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>&nbsp;";
            string str2 = string.Concat(new object[] { "<a href=\"", url, "-", countPage, expname, "\">&raquo;</a>&nbsp;" });
            if (countPage < 1)
            {
                countPage = 1;
            }
            if (extendPage < 3)
            {
                extendPage = 2;
            }
            if (countPage > extendPage)
            {
                if ((num3 - (extendPage / 2)) > 0)
                {
                    if ((num3 + (extendPage / 2)) < countPage)
                    {
                        num = num3 - (extendPage / 2);
                        num2 = (num + extendPage) - 1;
                    }
                    else
                    {
                        num2 = countPage;
                        num = (num2 - extendPage) + 1;
                        str2 = "";
                    }
                }
                else
                {
                    num2 = extendPage;
                    str = "";
                }
            }
            else
            {
                num = 1;
                num2 = countPage;
                str = "";
                str2 = "";
            }
            StringBuilder builder = new StringBuilder("");
            builder.Append(str);
            for (int i = num; i <= num2; i++)
            {
                builder.Append("&nbsp;<a href=\"");
                builder.Append(url);
                builder.Append("-");
                builder.Append(i);
                builder.Append(expname);
                builder.Append("\">");
                builder.Append(i);
                builder.Append("</a>&nbsp;");
            }
            builder.Append(str2);
            return builder.ToString();
        }

        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            return Convert.ToDateTime(fDateTime).ToString(formatStr);
        }

        public static string GetStaticPageNumbers(int curPage, int countPage, string url, string expname, int extendPage)
        {
            int num = 1;
            int num2 = 1;
            string str = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>&nbsp;";
            string str2 = string.Concat(new object[] { "<a href=\"", url, "-", countPage, expname, "\">&raquo;</a>&nbsp;" });
            if (countPage < 1)
            {
                countPage = 1;
            }
            if (extendPage < 3)
            {
                extendPage = 2;
            }
            if (countPage > extendPage)
            {
                if ((curPage - (extendPage / 2)) > 0)
                {
                    if ((curPage + (extendPage / 2)) < countPage)
                    {
                        num = curPage - (extendPage / 2);
                        num2 = (num + extendPage) - 1;
                    }
                    else
                    {
                        num2 = countPage;
                        num = (num2 - extendPage) + 1;
                        str2 = "";
                    }
                }
                else
                {
                    num2 = extendPage;
                    str = "";
                }
            }
            else
            {
                num = 1;
                num2 = countPage;
                str = "";
                str2 = "";
            }
            StringBuilder builder = new StringBuilder("");
            builder.Append(str);
            for (int i = num; i <= num2; i++)
            {
                if (i == curPage)
                {
                    builder.Append("&nbsp;");
                    builder.Append(i);
                    builder.Append("&nbsp;");
                }
                else
                {
                    builder.Append("&nbsp;<a href=\"");
                    builder.Append(url);
                    builder.Append("-");
                    builder.Append(i);
                    builder.Append(expname);
                    builder.Append("\">");
                    builder.Append(i);
                    builder.Append("</a>&nbsp;");
                }
            }
            builder.Append(str2);
            return builder.ToString();
        }

        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            string str = p_SrcString;
            if (p_Length < 0)
            {
                return str;
            }
            byte[] bytes = Encoding.Default.GetBytes(p_SrcString);
            if (bytes.Length <= p_Length)
            {
                return str;
            }
            int length = p_Length;
            int[] numArray = new int[p_Length];
            byte[] destinationArray = null;
            int num2 = 0;
            for (int i = 0; i < p_Length; i++)
            {
                if (bytes[i] > 0x7f)
                {
                    num2++;
                    if (num2 == 3)
                    {
                        num2 = 1;
                    }
                }
                else
                {
                    num2 = 0;
                }
                numArray[i] = num2;
            }
            if ((bytes[p_Length - 1] > 0x7f) && (numArray[p_Length - 1] == 1))
            {
                length = p_Length + 1;
            }
            destinationArray = new byte[length];
            Array.Copy(bytes, destinationArray, length);
            return (Encoding.Default.GetString(destinationArray) + p_TailString);
        }

        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        public static string GetTrueForumPath()
        {
            string path = HttpContext.Current.Request.Path;
            if (path.LastIndexOf("/") != path.IndexOf("/"))
            {
                return path.Substring(path.IndexOf("/"), path.LastIndexOf("/") + 1);
            }
            return "/";
        }

        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }

        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }

        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }

        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return (GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0);
        }

        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }

        public static bool InIPArray(string ip, string[] iparray)
        {
            string[] strArray = SplitString(ip, ".");
            for (int i = 0; i < iparray.Length; i++)
            {
                string[] strArray2 = SplitString(iparray[i], ".");
                int num2 = 0;
                for (int j = 0; j < strArray2.Length; j++)
                {
                    if (strArray2[j] == "*")
                    {
                        return true;
                    }
                    if ((strArray.Length > j) && (strArray2[j] == strArray[j]))
                    {
                        num2++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (num2 == 4)
                {
                    return true;
                }
            }
            return false;
        }

        public static string IntToStr(int intValue)
        {
            return Convert.ToString(intValue);
        }

        public static bool IsBase64String(string str)
        {
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }

        public static bool IsCompriseStr(string str, string stringarray, string strsplit)
        {
            if ((stringarray != "") && (stringarray != null))
            {
                str = str.ToLower();
                string[] strArray = SplitString(stringarray.ToLower(), strsplit);
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (str.IndexOf(strArray[i]) > -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }

        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || (filename.IndexOf(".") == -1))
            {
                return false;
            }
            string str = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return ((((str == "jpg") || (str == "jpeg")) || ((str == "png") || (str == "bmp"))) || (str == "gif"));
        }

        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static bool IsNumber(string strNumber)
        {
            return new Regex(@"^([0-9])[0-9]*(\.\w*)?$").IsMatch(strNumber);
        }

        public static bool IsNumberArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string str in strNumber)
            {
                if (!IsNumber(str))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, "/^\\s*$|^c:\\\\con\\\\con$|[%,\\*\"\\s\\t\\<\\>\\&]|$guestexp/is");
        }

        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, "^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        private static bool IsUTF8(FileStream sbInputStream)
        {
            bool flag = true;
            long length = sbInputStream.Length;
            byte num2 = 0;
            for (int i = 0; i < length; i++)
            {
                byte num3 = (byte) sbInputStream.ReadByte();
                if ((num3 & 0x80) != 0)
                {
                    flag = false;
                }
                if (num2 == 0)
                {
                    if (num3 >= 0x80)
                    {
                        do
                        {
                            num3 = (byte) (num3 << 1);
                            num2 = (byte) (num2 + 1);
                        }
                        while ((num3 & 0x80) != 0);
                        num2 = (byte) (num2 - 1);
                        if (num2 == 0)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if ((num3 & 0xc0) != 0x80)
                    {
                        return false;
                    }
                    num2 = (byte) (num2 - 1);
                }
            }
            if (num2 > 0)
            {
                return false;
            }
            if (flag)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        [DllImport("dbgHelp", SetLastError=true)]
        private static extern bool MakeSureDirectoryPathExists(string name);
        public static string mashSQL(string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Replace("'", "'");
            return str;
        }

        public static string MD5(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string str2 = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str2 = str2 + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return str2;
        }

        public static int RandomInt(int _up, int _down)
        {
            Random random = new Random();
            return random.Next(_up, _down);
        }

        public static string RemoveHtml(string content)
        {
            string pattern = "<[^>]*>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        public static string ReplaceStrToScript(string str)
        {
            str = str.Replace(@"\", @"\\");
            str = str.Replace("'", @"\'");
            str = str.Replace("\"", "\\\"");
            return str;
        }

        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream stream = null;
            byte[] buffer = new byte[0x2710];
            try
            {
                stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                long length = stream.Length;
                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + UrlEncode(filename.Trim()).Replace("+", " "));
                while (length > 0L)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int count = stream.Read(buffer, 0, 0x2710);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, count);
                        HttpContext.Current.Response.Flush();
                        buffer = new byte[0x2710];
                        length -= count;
                    }
                    else
                    {
                        length = -1L;
                    }
                }
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.Write("Error : " + exception.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }

        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                char ch = str[i];
                if (!ch.Equals(" "))
                {
                    ch = str[i];
                }
                if (ch.Equals("\r") || (ch = str[i]).Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }

        public static int SafeInt32(object objNum)
        {
            if (objNum != null)
            {
                string strNumber = objNum.ToString();
                if (IsNumber(strNumber))
                {
                    if (strNumber.ToString().Length > 9)
                    {
                        return 0x7fffffff;
                    }
                    return int.Parse(strNumber);
                }
            }
            return 0;
        }

        public static string SHA256(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            SHA256Managed managed = new SHA256Managed();
            return Convert.ToBase64String(managed.ComputeHash(bytes));
        }

        public static string Spaces(int nSpaces)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < nSpaces; i++)
            {
                builder.Append(" &nbsp;&nbsp;");
            }
            return builder.ToString();
        }

        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                return new string[] { strContent };
            }
            return Regex.Split(strContent, strSplit.Replace(".", @"\."), RegexOptions.IgnoreCase);
        }

        public static int StrDateDiffHours(string time, int hours)
        {
            if ((time == "") || (time == null))
            {
                return 1;
            }
            TimeSpan span = (TimeSpan) (DateTime.Now - DateTime.Parse(time).AddHours((double) hours));
            if (span.TotalHours > 2147483647.0)
            {
                return 0x7fffffff;
            }
            if (span.TotalHours < -2147483648.0)
            {
                return -2147483648;
            }
            return (int) span.TotalHours;
        }

        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if ((time == "") || (time == null))
            {
                return 1;
            }
            TimeSpan span = (TimeSpan) (DateTime.Now - DateTime.Parse(time).AddMinutes((double) minutes));
            if (span.TotalMinutes > 2147483647.0)
            {
                return 0x7fffffff;
            }
            if (span.TotalMinutes < -2147483648.0)
            {
                return -2147483648;
            }
            return (int) span.TotalMinutes;
        }

        public static int StrDateDiffSeconds(string Time, int Sec)
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - DateTime.Parse(Time).AddSeconds((double) Sec));
            if (span.TotalSeconds > 2147483647.0)
            {
                return 0x7fffffff;
            }
            if (span.TotalSeconds < -2147483648.0)
            {
                return -2147483648;
            }
            return (int) span.TotalSeconds;
        }

        public static string StrFilter(string str, string bantext)
        {
            string oldValue = "";
            string newValue = "";
            string[] strArray = SplitString(bantext, "\r\n");
            for (int i = 0; i < strArray.Length; i++)
            {
                oldValue = strArray[i].Substring(0, strArray[i].IndexOf("="));
                newValue = strArray[i].Substring(strArray[i].IndexOf("=") + 1);
                str = str.Replace(oldValue, newValue);
            }
            return str;
        }

        public static string StrFormat(string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Replace("\r\n", "<br />");
            str = str.Replace("\n", "<br />");
            return str;
        }

        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }
            float num = defValue;
            if ((strValue != null) && new Regex(@"^([-]|[0-9])[0-9]*(\.\w*)?$").IsMatch(strValue.ToString()))
            {
                num = Convert.ToSingle(strValue);
            }
            return num;
        }

        public static int StrToInt(object strValue, int defValue)
        {
            if (((strValue == null) || (strValue.ToString() == string.Empty)) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }
            string str = strValue.ToString();
            string strNumber = str[0].ToString();
            if (((str.Length == 10) && IsNumber(strNumber)) && (int.Parse(strNumber) > 1))
            {
                return defValue;
            }
            if (!((str.Length != 10) || IsNumber(strNumber)))
            {
                return defValue;
            }
            int num = defValue;
            if ((strValue != null) && new Regex("^([-]|[0-9])[0-9]*$").IsMatch(strValue.ToString()))
            {
                num = Convert.ToInt32(strValue);
            }
            return num;
        }

        public static string ToSChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        }

        public static string ToTChinese(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        }

        public void transHtml(string path, string outpath)
        {
            FileStream stream;
            Page page = new Page();
            StringWriter writer = new StringWriter();
            page.Server.Execute(path, writer);
            if (File.Exists(page.Server.MapPath("") + @"\" + outpath))
            {
                File.Delete(page.Server.MapPath("") + @"\" + outpath);
                stream = File.Create(page.Server.MapPath("") + @"\" + outpath);
            }
            else
            {
                stream = File.Create(page.Server.MapPath("") + @"\" + outpath);
            }
            byte[] bytes = Encoding.Default.GetBytes(writer.ToString());
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }

        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes((double) expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static string[] Monthes
        {
            get
            {
                return new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            }
        }
    }
}

