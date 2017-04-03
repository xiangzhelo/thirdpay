using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using DBAccess;
using viviLib;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.Channel;
using viviLib.Data;
using viviLib.Utils;

namespace viviapi.BLL.Channel
{
    /// <summary>
    /// 支付通道
    /// 2012-02-17
    /// </summary>
    public class Channel
    {
        static viviapi.DAL.Channel.Channel dal = new DAL.Channel.Channel();

        public static string CHANEL_CACHEKEY = Sys.Constant.CacheMark + "CHANNELS";

        internal static string SQL_TABLE = "channel";
        internal static string SQL_TABLE_FIELD = @"[id]
      ,[code]
      ,[typeId]
      ,[supplier]
      ,[supprate]
      ,[modeName]
      ,[modeEnName]
      ,[faceValue]
      ,[isOpen]
      ,[addtime]
      ,[sort]";

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(ChannelInfo model)
        {
            try
            {
                int id = dal.Add(model);
                if (id > 0)
                {
                    ClearCache();
                }
                return id;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region Update
        /// <summary>
        ///  更新一条数据
        /// </summary>
        public static bool Update(ChannelInfo model)
        {
            try
            {
                bool success = dal.Update(model);
                if (success)
                {
                    ClearCache();
                }
                return success;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int id)
        {
            try
            {
                bool success = dal.Delete(id);
                if (success)
                {
                    ClearCache();
                }
                return success;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region GetModel
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ChannelInfo GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ChannelInfo GetModelByCode(string code)
        {
            try
            {
                return dal.GetModelByCode(code);

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 从缓存中最到实体
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ChannelInfo GetCacheModel(string code)
        {
            try
            {
                DataTable data = GetCacheList();
                if (data == null)
                    return null;

                DataRow[] list = data.Select("code='" + code + "'");
                if (list.Length <= 0)
                {
                    return null;
                }
                return GetModelFromRow(list[0]);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        #region GetModelFromDs
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ChannelInfo GetModelFromRow(DataRow dr)
        {
            ChannelInfo model = new ChannelInfo();

            if (dr["id"].ToString() != "")
            {
                model.id = int.Parse(dr["id"].ToString());
            }
            model.code = dr["code"].ToString();
            if (dr["typeId"].ToString() != "")
            {
                model.typeId = int.Parse(dr["typeId"].ToString());
            }
            if (dr["supplier"].ToString() != "")
            {
                model.supplier = int.Parse(dr["supplier"].ToString());
            }
            if (dr["suppRate"].ToString() != "")
            {
                model.supprate = Convert.ToDecimal(dr["suppRate"]);
            }
            model.modeName = dr["modeName"].ToString();
            model.modeEnName = dr["modeEnName"].ToString();
            if (dr["faceValue"].ToString() != "")
            {
                model.faceValue = int.Parse(dr["faceValue"].ToString());
            }
            if (dr["isOpen"].ToString() != "")
            {
                model.isOpen = int.Parse(dr["isOpen"].ToString());
            }
            //if (dr["addtime"].ToString() != "")
            //{
            //    model.addtime = DateTime.Parse(dr["addtime"].ToString());
            //}
            if (dr["sort"].ToString() != "")
            {
                model.sort = int.Parse(dr["sort"].ToString());
            }
            return model;
        }
        #endregion
        #endregion

        #region GetList
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(int typeId)
        {
            try
            {
                return dal.GetList(typeId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        #region 分页
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <param name="facevalue"></param>
        /// <param name="chanelstatus"></param>
        /// <returns></returns>
        public static DataSet GetBankChanels(int pageindex, int pagesize, int userid, int typeid, int facevalue, int chanelstatus)
        {
            try
            {
                return dal.GetBankChanels(pageindex, pagesize, userid, typeid, facevalue, chanelstatus);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static DataTable GetCardChanels(int userid, int typeid, int facevalue, int chanelstatus)
        {
            try
            {
                return dal.GetCardChanels(userid, typeid, facevalue, chanelstatus);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        public static DataSet GetCardChanels(int pageindex, int pagesize, int userid, int typeid, int facevalue, int chanelstatus)
        {
            try
            {
                return dal.GetCardChanels(pageindex, pagesize, userid, typeid, facevalue, chanelstatus);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParams"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }

        #endregion
        #endregion

        #region GetChannelInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="value"></param>
        /// <param name="userId"></param>
        /// <param name="cycle">循环</param>
        /// <returns></returns>
        public static ChannelInfo GetModel(int typeId, int value, int userId, bool cycle)
        {
            try
            {
                DataTable data = GetCacheList();
                if (data == null)
                    return null;

                DataRow[] list = data.Select("typeId=" + typeId.ToString(CultureInfo.InvariantCulture) + " and faceValue=" + value.ToString(CultureInfo.InvariantCulture));
                if (list.Length <= 0)
                {
                    return null;
                }
                string chanelNo = list[0]["code"].ToString();

                return GetModel(chanelNo, userId, cycle);
            }
            catch(Exception exception)
            {
                return null;
            }
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

        /// <summary>
        /// 获通道的相关信息
        /// </summary>
        /// <param name="chanelNo">通道代码</param>
        /// <param name="userId">用户ID</param>
        /// <param name="cycle">是否更新当前渠道</param>
        /// <returns></returns>
        public static ChannelInfo GetModel(string chanelNo, int userId, bool cycle)
        {
            int open = 0;

            var info = GetCacheModel(chanelNo);
            if (info == null)
                return null;

            var typeinfo = ChannelType.GetCacheModel(info.typeId);
            if (typeinfo == null)
            {
                return null;
            }

            int usersuppid = -1;
            if (!info.supplier.HasValue || info.supplier == 0)
            {
                info.supplier = typeinfo.supplier;
                info.supprate = typeinfo.supprate;
            }

            if (cycle)
            {
                #region 
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

                    var rancontroller = new RandomController(1);
                    rancontroller.datas = datas;
                    rancontroller.weights = weights;

                    int seed = GetRandomSeed();
                    Random rand = new Random(seed);//

                    int[] suppid = rancontroller.ControllerRandomExtract(rand);

                    info.supplier = suppid[0];
                }
                #endregion
            }

            /*系统通道状态*/
            switch (typeinfo.isOpen)
            {
                case OpenEnum.AllClose://全部关闭
                    usersuppid = GetUserSupp(userId, info.typeId);
                    open = 0;
                    break;
                case OpenEnum.AllOpen://全部开启
                    usersuppid = GetUserSupp(userId, info.typeId);
                    open = 1;
                    break;
                case OpenEnum.Close://根据设置 默认为关闭算法，如果后台对用户单独设置了就计算单独的开启状态。 如果没有看通道本身的状态 默认为关闭。如果后台都未设置为默认状态
                    open = GetChanelSysStatus(4, userId, chanelNo, info.typeId, ref usersuppid);// GetSysOpenStatus(userId, chanelNo, info.typeId, 0);
                    break;
                case OpenEnum.Open://
                    open = GetChanelSysStatus(8, userId, chanelNo, info.typeId, ref usersuppid); //GetSysOpenStatus(userId, chanelNo, info.typeId, 1);
                    break;
            }
            //如果网银数据库
            if (usersuppid > -1)
            {
                info.supplier = usersuppid;
            }
            /*用户通道状态 只有系统通道状态为开启时用户的设置才有效*/
            if (open == 1)
            {
                open = GetUserOpenStatus(userId, chanelNo, info.typeId, 1);
            }
            info.isOpen = open;

            return info;
        }

        #region GetUserSupp
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
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
        #endregion

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
                channelInfo = GetCacheModel(chanelNo);

            ChannelTypeUserInfo typeUserConf = ChannelTypeUsers.GetCacheModel(userId, typeId);

            if (channelInfo != null && channelInfo.isOpen.HasValue)
            {
                chanelValue = channelInfo.isOpen.Value;
            }

            if (typeUserConf != null && typeUserConf.sysIsOpen.HasValue)
            {
                sysSettingValue = typeUserConf.sysIsOpen.Value ? 1 : 0;
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
        #endregion

        #region CacheData
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataTable GetCacheList()
        {
            try
            {
                string cacheKey = CHANEL_CACHEKEY;

                DataSet ds;
                ds = (DataSet)Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

                if (ds == null)
                {
                    var sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE, SQL_TABLE_FIELD, "", null);

                    ds = GetList(0);

                    Cache.WebCache.GetCacheService().AddObject(cacheKey, ds);
                }

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion

        //清理缓存 
        static void ClearCache()
        {
            string cacheKey = CHANEL_CACHEKEY;
            Cache.WebCache.GetCacheService().RemoveObject(cacheKey);
        }

        
    }
}
