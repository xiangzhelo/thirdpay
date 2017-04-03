using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using viviLib.Utils;

namespace viviLib.comm
{
    public class Utils
    {
        private static FileVersionInfo AssemblyFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        private static Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);
        public static Regex RegexFont = new Regex("<font color=\".*?\">([\\s\\S]+?)</font>", GetRegexCompiledOptions());
        private static string TemplateCookieName = string.Format("dnttemplateid_{0}_{1}_{2}", AssemblyFileVersion.FileMajorPart, AssemblyFileVersion.FileMinorPart, AssemblyFileVersion.FileBuildPart);

        public static string AdDeTime(int times)
        {
            return DateTime.Now.AddMinutes((double) times).ToString();
        }

        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }

        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            bool flag;
            if (!System.IO.File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!(overwrite || !System.IO.File.Exists(destFileName)))
            {
                return false;
            }
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

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
            Match match = null;
            for (match = RegexBr.Match(str); match.Success; match = match.NextMatch())
            {
                str = str.Replace(match.Groups[0].ToString(), "");
            }
            return str;
        }

        public static string ClearLastChar(string str)
        {
            if (str == "")
            {
                return "";
            }
            return str.Substring(0, str.Length - 1);
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
            return str.Substring(startIndex, length);
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
            return System.IO.File.Exists(filename);
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

        public static string FormatDate(DateTime dt)
        {
            return string.Concat(new object[] { dt.Year, "-", dt.Month, "-", dt.Day });
        }

        public static string FormatDateYearTwo(DateTime dt)
        {
            return string.Concat(new object[] { dt.Year.ToString().Substring(2), "-", dt.Month, "-", dt.Day });
        }

        public static string FormatYearMonth(DateTime dt)
        {
            return string.Concat(new object[] { dt.Year, "-", dt.Month, "-" });
        }

        public static string Get_Https(string a_strUrl, int timeout)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(a_strUrl);
                request.Timeout = timeout;
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                StringBuilder builder = new StringBuilder();
                while (-1 != reader.Peek())
                {
                    builder.Append(reader.ReadLine());
                }
                return builder.ToString();
            }
            catch (Exception)
            {
                return "true";
            }
        }

        public static string GetAssemblyCopyright()
        {
            return AssemblyFileVersion.LegalCopyright;
        }

        public static string GetAssemblyProductName()
        {
            return AssemblyFileVersion.ProductName;
        }

        public static string GetAssemblyVersion()
        {
            return string.Format("{0}.{1}.{2}", AssemblyFileVersion.FileMajorPart, AssemblyFileVersion.FileMinorPart, AssemblyFileVersion.FileBuildPart);
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

        public static string getIntZero(string j)
        {
            if (j.Length < 2)
            {
                return ("0" + j);
            }
            return j;
        }

        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, "page");
        }

        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, pagetag, null);
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
            str = str + "\">&laquo;</a>";
            str2 = str2 + "\">&raquo;</a>";
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
                    builder.Append(i);
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
            string str = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>";
            string str2 = string.Concat(new object[] { "<a href=\"", url, "-", countPage, expname, "\">&raquo;</a>" });
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
                builder.Append("<a href=\"");
                builder.Append(url);
                builder.Append("-");
                builder.Append(i);
                builder.Append(expname);
                builder.Append("\">");
                builder.Append(i);
                builder.Append("</a>");
            }
            builder.Append(str2);
            return builder.ToString();
        }

        public static RegexOptions GetRegexCompiledOptions()
        {
            return RegexOptions.None;
        }

        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
            {
                return fDateTime;
            }
            return Convert.ToDateTime(fDateTime).ToString(formatStr);
        }

        public static string GetStaticPageNumbers(int curPage, int countPage, string url, string expname, int extendPage)
        {
            int num = 1;
            int num2 = 1;
            string str = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>";
            string str2 = string.Concat(new object[] { "<a href=\"", url, "-", countPage, expname, "\">&raquo;</a>" });
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
                    builder.Append("-");
                    builder.Append(i);
                    builder.Append(expname);
                    builder.Append("\">");
                    builder.Append(i);
                    builder.Append("</a>");
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
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }

        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string str = p_SrcString;
            if (Regex.IsMatch(p_SrcString, "[ࠀ-一]+") || Regex.IsMatch(p_SrcString, "[가-힣]+"))
            {
                if (p_StartIndex >= p_SrcString.Length)
                {
                    return "";
                }
                return p_SrcString.Substring(p_StartIndex, ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
            }
            if (p_Length < 0)
            {
                return str;
            }
            byte[] bytes = Encoding.Default.GetBytes(p_SrcString);
            if (bytes.Length <= p_StartIndex)
            {
                return str;
            }
            int length = bytes.Length;
            if (bytes.Length > (p_StartIndex + p_Length))
            {
                length = p_Length + p_StartIndex;
            }
            else
            {
                p_Length = bytes.Length - p_StartIndex;
                p_TailString = "";
            }
            int num2 = p_Length;
            int[] numArray = new int[p_Length];
            byte[] destinationArray = null;
            int num3 = 0;
            for (int i = p_StartIndex; i < length; i++)
            {
                if (bytes[i] > 0x7f)
                {
                    num3++;
                    if (num3 == 3)
                    {
                        num3 = 1;
                    }
                }
                else
                {
                    num3 = 0;
                }
                numArray[i] = num3;
            }
            if ((bytes[length - 1] > 0x7f) && (numArray[p_Length - 1] == 1))
            {
                num2 = p_Length + 1;
            }
            destinationArray = new byte[num2];
            Array.Copy(bytes, p_StartIndex, destinationArray, 0, num2);
            return (Encoding.Default.GetString(destinationArray) + p_TailString);
        }

        public static string GetTemplateCookieName()
        {
            return TemplateCookieName;
        }

        public static string GetTextFromHTML(string HTML)
        {
            Regex regex = new Regex("</?(?!br|/?p|img)[^>]*>", RegexOptions.IgnoreCase);
            return regex.Replace(HTML, "");
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

        public static bool IsDouble(object Expression)
        {
            return TypeParse.IsDouble(Expression);
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

        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, "^[0-9]*$");
        }

        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
        }

        public static bool IsNumberId(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        public static bool IsNumeric(object Expression)
        {
            return TypeParse.IsNumeric(Expression);
        }

        public static bool IsNumericArray(string[] strNumber)
        {
            return TypeParse.IsNumericArray(strNumber);
        }

        public static bool IsRuleTip(Hashtable NewHash, string ruletype, out string key)
        {
            key = "";
            foreach (DictionaryEntry entry in NewHash)
            {
                try
                {
                    string[] strArray = SplitString(entry.Value.ToString(), "\r\n");
                    foreach (string str in strArray)
                    {
                        if (str != "")
                        {
                            string str2 = ruletype.Trim().ToLower();
                            if (str2 != null)
                            {
                                if (!(str2 == "email"))
                                {
                                    if (str2 == "ip")
                                    {
                                        goto Label_00BF;
                                    }
                                    if (str2 == "timesect")
                                    {
                                        goto Label_00D8;
                                    }
                                }
                                else if (!IsValidDoEmail(str.ToString()))
                                {
                                    throw new Exception();
                                }
                            }
                        }
                        goto Label_011C;
                    Label_00BF:
                        if (!IsIPSect(str.ToString()))
                        {
                            throw new Exception();
                        }
                        goto Label_011C;
                    Label_00D8:;
                        string[] strArray2 = str.Split(new char[] { '-' });
                        if (!(IsTime(strArray2[1].ToString()) && IsTime(strArray2[0].ToString())))
                        {
                            throw new Exception();
                        }
                    Label_011C:;
                    }
                }
                catch
                {
                    key = entry.Key.ToString();
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
            return !Regex.IsMatch(str, "^\\s*$|^c:\\\\con\\\\con$|[%,\\*\"\\s\\t\\<\\>\\&]|游客|^Guest");
        }

        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, "^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
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

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
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

        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null)
            {
                return false;
            }
            Regex regex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return regex.IsMatch(_value);
        }

        public static string RemoveFontTag(string title)
        {
            Match match = RegexFont.Match(title);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return title;
        }

        public static string RemoveHtml(string content)
        {
            string pattern = "<[^>]*>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
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
                stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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

        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }

        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return true;
        }

        public static string RTrim(string str)
        {
            try
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
            catch (Exception)
            {
                return str;
            }
        }

        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] chars = SBCCase.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(chars, i, 1);
                if ((bytes.Length == 2) && (bytes[1] == 0xff))
                {
                    bytes[0] = (byte) (bytes[0] + 0x20);
                    bytes[1] = 0;
                    chars[i] = Encoding.Unicode.GetChars(bytes)[0];
                }
            }
            return new string(chars);
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
            if (!String.IsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        public static string[] SplitString(string strContent, string strSplit, int p_3)
        {
            string[] strArray = new string[p_3];
            string[] strArray2 = SplitString(strContent, strSplit);
            for (int i = 0; i < p_3; i++)
            {
                if (i < strArray2.Length)
                {
                    strArray[i] = strArray2[i];
                }
                else
                {
                    strArray[i] = string.Empty;
                }
            }
            return strArray;
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

        public static bool StrToBool(object Expression, bool defValue)
        {
            return TypeParse.StrToBool(Expression, defValue);
        }

        public static float StrToFloat(object strValue, float defValue)
        {
            return TypeParse.StrToFloat(strValue, defValue);
        }

        public static int StrToInt(object Expression, int defValue)
        {
            return TypeParse.StrToInt(Expression, defValue);
        }

        public static Color ToColor(string color)
        {
            int num;
            int num2;
            char[] chArray;
            int blue = 0;
            color = color.TrimStart(new char[] { '#' });
            color = Regex.Replace(color.ToLower(), "[g-zG-Z]", "");
            switch (color.Length)
            {
                case 3:
                    chArray = color.ToCharArray();
                    num = Convert.ToInt32(chArray[0].ToString() + chArray[0].ToString(), 0x10);
                    num2 = Convert.ToInt32(chArray[1].ToString() + chArray[1].ToString(), 0x10);
                    blue = Convert.ToInt32(chArray[2].ToString() + chArray[2].ToString(), 0x10);
                    return Color.FromArgb(num, num2, blue);

                case 6:
                    chArray = color.ToCharArray();
                    num = Convert.ToInt32(chArray[0].ToString() + chArray[1].ToString(), 0x10);
                    num2 = Convert.ToInt32(chArray[2].ToString() + chArray[3].ToString(), 0x10);
                    blue = Convert.ToInt32(chArray[4].ToString() + chArray[5].ToString(), 0x10);
                    return Color.FromArgb(num, num2, blue);
            }
            return Color.FromName(color);
        }

        public void transHtml(string path, string outpath)
        {
            FileStream stream;
            Page page = new Page();
            StringWriter writer = new StringWriter();
            page.Server.Execute(path, writer);
            if (System.IO.File.Exists(page.Server.MapPath("") + @"\" + outpath))
            {
                System.IO.File.Delete(page.Server.MapPath("") + @"\" + outpath);
                stream = System.IO.File.Create(page.Server.MapPath("") + @"\" + outpath);
            }
            else
            {
                stream = System.IO.File.Create(page.Server.MapPath("") + @"\" + outpath);
            }
            byte[] bytes = Encoding.Default.GetBytes(writer.ToString());
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }

        private static string Unicode2UnitCharacter(string str)
        {
            if (str.Length != 4)
            {
                return str;
            }
            try
            {
                byte num = Convert.ToByte(str.Substring(0, 2), 0x10);
                byte num2 = Convert.ToByte(str.Substring(2), 0x10);
                return Encoding.Unicode.GetString(new byte[] { num2, num });
            }
            catch (Exception)
            {
                return str;
            }
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

