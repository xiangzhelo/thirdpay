namespace viviLib.Utils
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class FileHelp
    {
        public static bool IsExists(string tempDir)
        {
            return File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + tempDir);
        }

        public static string ReadFile(string tempDir)
        {
            if (File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + tempDir))
            {
                StreamReader reader = new StreamReader(HttpContext.Current.Request.PhysicalApplicationPath + tempDir, Encoding.Default);
                string str = reader.ReadToEnd();
                reader.Close();
                return str;
            }
            return ("未找到模板文件：" + tempDir);
        }

        public static string ReadPhoto(string tempDir)
        {
            if (File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + tempDir))
            {
                return tempDir;
            }
            return @"Images\onlinenone.jpg";
        }

        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                FileStream input = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader reader = new BinaryReader(input);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;
                    long length = input.Length;
                    long num2 = 0L;
                    int count = 0x2800;
                    int millisecondsTimeout = ((int) Math.Floor((double) (((long) (0x3e8 * count)) / _speed))) + 1;
                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 0xce;
                        num2 = Convert.ToInt64(_Request.Headers["Range"].Split(new char[] { '=', '-' })[1]);
                    }
                    _Response.AddHeader("Content-Length", (length - num2).ToString());
                    if (num2 != 0L)
                    {
                        _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", num2, length - 1L, length));
                    }
                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, Encoding.UTF8));
                    reader.BaseStream.Seek(num2, SeekOrigin.Begin);
                    int num5 = ((int) Math.Floor((double) ((length - num2) / ((long) count)))) + 1;
                    for (int i = 0; i < num5; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(reader.ReadBytes(count));
                            Thread.Sleep(millisecondsTimeout);
                        }
                        else
                        {
                            i = num5;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    reader.Close();
                    input.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

