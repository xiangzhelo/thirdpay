using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace viviapi.WebComponents
{
    public class IpList
    {
        private int _countryFlag;
        private string _dataPath = (AppDomain.CurrentDomain.BaseDirectory.ToString(CultureInfo.InvariantCulture) + "/App_Data/ipList.Dat");
        private long _endIp;
        private long _endIpOff;
        private long _firstStartIp;
        private string _ip;
        private long _lastStartIp;
        private FileStream _objfs;
        private long _startIp;

        private string GetCountry()
        {
            switch (this._countryFlag)
            {
                case 1:
                case 2:
                    this.Country = this.GetFlagStr(this._endIpOff + 4L);
                    this.Local = (1 == this._countryFlag) ? " " : this.GetFlagStr(this._endIpOff + 8L);
                    break;

                default:
                    this.Country = this.GetFlagStr(this._endIpOff + 4L);
                    this.Local = this.GetFlagStr(this._objfs.Position);
                    break;
            }
            return " ";
        }

        private long GetEndIp()
        {
            this._objfs.Position = this._endIpOff;
            var buffer = new byte[5];
            this._objfs.Read(buffer, 0, 5);
            this._endIp = ((Convert.ToInt64(buffer[0].ToString(CultureInfo.InvariantCulture)) + (Convert.ToInt64(buffer[1].ToString(CultureInfo.InvariantCulture)) * 0x100L)) + ((Convert.ToInt64(buffer[2].ToString(CultureInfo.InvariantCulture)) * 0x100L) * 0x100L)) + (((Convert.ToInt64(buffer[3].ToString(CultureInfo.InvariantCulture)) * 0x100L) * 0x100L) * 0x100L);
            this._countryFlag = buffer[4];
            return this._endIp;
        }

        private string GetFlagStr(long offSet)
        {
            int num = 0;
            byte[] buffer = new byte[3];
            while (true)
            {
                this._objfs.Position = offSet;
                num = this._objfs.ReadByte();
                if ((num != 1) && (num != 2))
                {
                    break;
                }
                this._objfs.Read(buffer, 0, 3);
                if (num == 2)
                {
                    this._countryFlag = 2;
                    this._endIpOff = offSet - 4L;
                }
                offSet = (Convert.ToInt64(buffer[0].ToString()) + (Convert.ToInt64(buffer[1].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[2].ToString()) * 0x100L) * 0x100L);
            }
            if (offSet < 12L)
            {
                return " ";
            }
            this._objfs.Position = offSet;
            return this.GetStr();
        }

        private long GetStartIp(long recNO)
        {
            long num = this._firstStartIp + (recNO * 7L);
            this._objfs.Position = num;
            byte[] buffer = new byte[7];
            this._objfs.Read(buffer, 0, 7);
            this._endIpOff = (Convert.ToInt64(buffer[4].ToString()) + (Convert.ToInt64(buffer[5].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[6].ToString()) * 0x100L) * 0x100L);
            this._startIp = ((Convert.ToInt64(buffer[0].ToString()) + (Convert.ToInt64(buffer[1].ToString()) * 0x100L)) + ((Convert.ToInt64(buffer[2].ToString()) * 0x100L) * 0x100L)) + (((Convert.ToInt64(buffer[3].ToString()) * 0x100L) * 0x100L) * 0x100L);
            return this._startIp;
        }

        private string GetStr()
        {
            byte num = 0;
            byte num2 = 0;
            string str = "";
            byte[] bytes = new byte[2];
            while (true)
            {
                num = (byte)this._objfs.ReadByte();
                if (num == 0)
                {
                    return str;
                }
                if (num > 0x7f)
                {
                    num2 = (byte)this._objfs.ReadByte();
                    bytes[0] = num;
                    bytes[1] = num2;
                    Encoding encoding = Encoding.GetEncoding("GB2312");
                    str = str + encoding.GetString(bytes);
                }
                else
                {
                    str = str + ((char)num);
                }
            }
        }

        private string IntToIP(long ipInt)
        {
            long num = (long)((ipInt & 0xff000000L) >> 0x18);
            if (num < 0L)
            {
                num += 0x100L;
            }
            long num2 = (ipInt & 0xff0000L) >> 0x10;
            if (num2 < 0L)
            {
                num2 += 0x100L;
            }
            long num3 = (ipInt & 0xff00L) >> 8;
            if (num3 < 0L)
            {
                num3 += 0x100L;
            }
            long num4 = ipInt & 0xffL;
            if (num4 < 0L)
            {
                num4 += 0x100L;
            }
            return (num.ToString(CultureInfo.InvariantCulture) + "." + num2.ToString(CultureInfo.InvariantCulture) + "." + num3.ToString(CultureInfo.InvariantCulture) + "." + num4.ToString(CultureInfo.InvariantCulture));
        }

        public string IPAddInfo()
        {
            this.QQwry();
            return this.Local;
        }

        public string IPLocation()
        {
            this.QQwry();
            return this.Country;
        }

        public string IPLocation(string dataPath, string ip)
        {
            try
            {
                this._ip = ip;
                this.QQwry();
                return (this.Country + this.Local);
            }
            catch
            {
                return "火星";
            }
        }

        private long IpToInt(string ip)
        {
            char[] separator = new char[] { '.' };
            if (ip.Split(separator).Length == 3)
            {
                ip = ip + ".0";
            }
            string[] strArray = ip.Split(separator);
            long num2 = ((long.Parse(strArray[0]) * 0x100L) * 0x100L) * 0x100L;
            long num3 = (long.Parse(strArray[1]) * 0x100L) * 0x100L;
            long num4 = long.Parse(strArray[2]) * 0x100L;
            long num5 = long.Parse(strArray[3]);
            return (((num2 + num3) + num4) + num5);
        }

        private int QQwry()
        {
            const string pattern = @"(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))";
            Regex regex = new Regex(pattern);
            if (!regex.Match(this._ip).Success)
            {
                this.ErrMsg = "未知IP";
                return 4;
            }
            long num = this.IpToInt(this._ip);
            int num2 = 0;
            if ((num >= this.IpToInt("127.0.0.0")) && (num <= this.IpToInt("127.255.255.255")))
            {
                this.Country = "局域网IP";
                this.Local = "";
                num2 = 1;
            }
            else if ((((num >= this.IpToInt("0.0.0.0")) && (num <= this.IpToInt("2.255.255.255"))) || ((num >= this.IpToInt("64.0.0.0")) && (num <= this.IpToInt("126.255.255.255")))) || ((num >= this.IpToInt("58.0.0.0")) && (num <= this.IpToInt("60.255.255.255"))))
            {
                this.Country = "网络保留地址";
                this.Local = "";
                num2 = 1;
            }
            this._objfs = new FileStream(this._dataPath, FileMode.Open, FileAccess.Read);
            try
            {
                this._objfs.Position = 0L;
                byte[] buffer = new byte[8];
                this._objfs.Read(buffer, 0, 8);
                this._firstStartIp = ((buffer[0] + (buffer[1] * 0x100)) + ((buffer[2] * 0x100) * 0x100)) + (((buffer[3] * 0x100) * 0x100) * 0x100);
                this._lastStartIp = ((buffer[4] + (buffer[5] * 0x100)) + ((buffer[6] * 0x100) * 0x100)) + (((buffer[7] * 0x100) * 0x100) * 0x100);
                long num3 = Convert.ToInt64((double)(((double)(this._lastStartIp - this._firstStartIp)) / 7.0));
                if (num3 <= 1L)
                {
                    this.Country = "FileDataError";
                    this._objfs.Close();
                    return 2;
                }
                long num4 = num3;
                long recNO = 0L;
                long num6 = 0L;
                while (recNO < (num4 - 1L))
                {
                    num6 = (num4 + recNO) / 2L;
                    this.GetStartIp(num6);
                    if (num == this._startIp)
                    {
                        recNO = num6;
                        break;
                    }
                    if (num > this._startIp)
                    {
                        recNO = num6;
                    }
                    else
                    {
                        num4 = num6;
                    }
                }
                this.GetStartIp(recNO);
                this.GetEndIp();
                if ((this._startIp <= num) && (this._endIp >= num))
                {
                    this.GetCountry();
                    this.Local = this.Local.Replace("（我们一定要解放台湾！！！）", "");
                }
                else
                {
                    num2 = 3;
                    this.Country = "火星";
                    this.Local = "";
                }
                this._objfs.Close();
                return num2;
            }
            catch
            {
                return 1;
            }
        }

        public string Country { get; private set; }

        public string DataPath
        {
            set
            {
                this._dataPath = value;
            }
        }

        public string ErrMsg { get; private set; }

        public string IP
        {
            set
            {
                this._ip = value;
            }
        }

        public string Local { get; private set; }
    }
}

