using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using viviapi.Model.Channel;
using viviLib.Utils;

namespace viviapi.BLL.Channel
{
    /// <summary>
    /// 
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeid">通道类型</param>
        /// <param name="chanelNo">银行编号</param>
        /// <param name="userId"></param>
        /// <param name="cycle">循环通道</param>
        /// <returns></returns>
        public static ChannelInfo GetModel(int typeid,string chanelNo, int userId, bool cycle)
        {
            int open = 0;

            var typeinfo = ChannelType.GetCacheModel(typeid);
            if (typeinfo == null)
            {
                return null;
            }

            var channel = Channel.GetCacheModel(chanelNo);
            if (channel == null)
                return null;

            if (typeid != channel.typeId)
            {
                return null;
            }

            int usersuppid = -1;
            //通道没设置供应商的时候，自动获取通道类型的供应商
            //当通道设置了供应商时才按照通道设置的供应商
            if (!channel.supplier.HasValue || channel.supplier == 0)
            {
                channel.supplier = typeinfo.supplier;
                channel.supprate = typeinfo.supprate;
            }

            if (cycle)
            {
                #region 循环通道
                if (typeinfo.runmode == 1)
                {
                    string set = typeinfo.runset;

                    var datas = new List<int>();
                    var weights = new List<ushort>();

                    foreach (string item in set.Split('|'))
                    {
                        string[] arr = item.Split(':');

                        datas.Add(Convert.ToInt32(arr[0]));
                        weights.Add(Convert.ToUInt16(arr[1]));
                    }

                    var rancontroller = new RandomController(1) {datas = datas, weights = weights};

                    int seed = GetRandomSeed();
                    var rand = new Random(seed);//

                    int[] suppid = rancontroller.ControllerRandomExtract(rand);

                    if (suppid.Length > 0)
                    {
                        channel.supplier = suppid[0];
                    }
                   
                }
                #endregion
            }

            #region 通道开启状态
            //
            switch (typeinfo.isOpen)
            {
                case OpenEnum.AllClose://全部关闭
                    usersuppid = GetUserSupp(userId, typeid);
                    open = 0;
                    break;
                case OpenEnum.AllOpen://全部开启
                    usersuppid = GetUserSupp(userId, typeid);
                    open = 1;
                    break;
                case OpenEnum.Close://根据设置 默认为关闭算法，如果后台对用户单独设置了就计算单独的开启状态。 如果没有看通道本身的状态 默认为关闭。如果后台都未设置为默认状态
                    open = GetChanelSysStatus(4, userId, chanelNo, typeid, ref usersuppid);// GetSysOpenStatus(userId, chanelNo, info.typeId, 0);
                    break;
                case OpenEnum.Open://
                    open = GetChanelSysStatus(8, userId, chanelNo, typeid, ref usersuppid); //GetSysOpenStatus(userId, chanelNo, info.typeId, 1);
                    break;
            }
            #endregion

            //自定义通道
            if (usersuppid > -1)
            {
                channel.supplier = usersuppid;
            }
            /*用户通道状态 只有系统通道状态为开启时用户的设置才有效*/
            if (open == 1)
            {
                open = GetUserOpenStatus(userId, chanelNo, typeid, 1);
            }
            channel.isOpen = open;

            return channel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeed()
        {
            var bytes = new byte[4];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        static int GetUserSupp(int userId, int typeId)
        {
            int suppid = -1;

            var typeUserConf = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if (typeUserConf != null && typeUserConf.suppid.HasValue)
            {
                if (typeUserConf.suppid.Value > 0)
                    suppid = typeUserConf.suppid.Value;
            }
            return suppid;
        }

        /// <summary>
        /// 两者同时为1才 为开启
        /// </summary>
        /// <param name="typeStatus">类型状态</param>
        /// <param name="userId">用户ID</param>
        /// <param name="chanelNo">通道</param>
        /// <param name="typeId">通道类型ID</param>
        /// <returns></returns>
        public static int GetChanelSysStatus(int typeStatus, int userId, string chanelNo, int typeId, ref int suppid)
        {
            suppid = -1;

            int result = 0;
            int chanelValue = -1;
            int sysSettingValue = -1;

            ChannelInfo channelInfo = null;
            if (!string.IsNullOrEmpty(chanelNo))
                channelInfo = Channel.GetCacheModel(chanelNo);

            ChannelTypeUserInfo typeUserConf = ChannelTypeUsers.GetCacheModel(userId, typeId);

            if (channelInfo != null && channelInfo.isOpen.HasValue)
            {
                chanelValue = channelInfo.isOpen.Value;
            }

            if (typeUserConf != null)
            {
                if (typeUserConf.sysIsOpen.HasValue)
                {
                    sysSettingValue = typeUserConf.sysIsOpen.Value ? 1 : 0;
                }

                if (typeUserConf.suppid.HasValue)
                {
                    if (typeUserConf.suppid.Value > 0)
                        suppid = typeUserConf.suppid.Value;
                }
            }

            //默认是关闭           
            if (typeStatus == 4)
            {
                if (chanelValue == -1)
                    chanelValue = 0;
                if (sysSettingValue == -1)
                    sysSettingValue = 0;
            }
            //默认是开启           
            else if (typeStatus == 8)
            {
                if (chanelValue == -1)
                    chanelValue = 1;
                if (sysSettingValue == -1)
                    sysSettingValue = 1;
            }

            //同时开启才开启
            if (chanelValue == 1 && sysSettingValue == 1)
                result = 1;
            return result;
        }

        #region GetUserOpenStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chanelNo"></param>
        /// <param name="typeId"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static int GetUserOpenStatus(int userId, string chanelNo, int typeId, int defaultvalue)
        {
            int result = defaultvalue;
            ChannelTypeUserInfo typeuserinfo = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if (typeuserinfo == null)
                return result;

            /*用户设置状态*/
            if (typeuserinfo.userIsOpen.HasValue)
            {
                result = typeuserinfo.userIsOpen.Value ? 1 : 0;
            }

            return result;
        }
        #endregion
    }
}
