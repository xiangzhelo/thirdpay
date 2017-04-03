using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DBAccess;
using viviLib;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.Channel;

namespace viviapi.BLL.Channel
{
    /// <summary>
    /// 支付通道类别
    /// 2012-02-17
    /// </summary>
    public class ChannelType
    {
        public static string CHANNELTYPE_CACHEKEY = Sys.Constant.CacheMark + "CHANNEL_TYPE";
        internal static string SQL_TABLE = "channeltype";
        internal static string SQL_TABLE_FIELD = @"[id]
      ,[typeId]
      ,[code]
      ,[classid]
      ,[modetypename]
      ,[isOpen]
      ,[supplier]
      ,[supplier2]
      ,[supprate]
      ,[addtime]
      ,[sort]
      ,[release]
      ,[runmode]
      ,[runset],[suppsWhenExceOccurred],[timeout]";

        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(ChannelTypeInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@classid", SqlDbType.TinyInt,1),
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@modetypename", SqlDbType.VarChar,50),
					new SqlParameter("@isOpen", SqlDbType.TinyInt,1),
					new SqlParameter("@supplier", SqlDbType.Int,4),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@sort", SqlDbType.Int,4),
					new SqlParameter("@release", SqlDbType.Bit,1),
                    new SqlParameter("@runmode", SqlDbType.TinyInt,4),
                    new SqlParameter("@runset", SqlDbType.VarChar,1000),
                    new SqlParameter("@supplier2", SqlDbType.Int,4),
                    new SqlParameter("@suppsWhenExceOccurred", SqlDbType.VarChar,100)   
                                            };
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.Class;
                parameters[2].Value = model.typeId;
                parameters[3].Value = model.modetypename;
                parameters[4].Value = (int)model.isOpen;
                parameters[5].Value = model.supplier;
                parameters[6].Value = model.addtime;
                parameters[7].Value = model.sort;
                parameters[8].Value = model.release;

                parameters[9].Value = model.runmode;
                parameters[10].Value = model.runset;

                parameters[11].Value = model.supplier2;
                parameters[12].Value = model.SuppsWhenExceOccurred;


                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channeltype_add", parameters);
                int id = (int)parameters[0].Value;
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
        public static bool Update(ChannelTypeInfo model)
        {
            try
            {
                int rowsAffected;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@classid", SqlDbType.TinyInt,1),
					new SqlParameter("@typeId", SqlDbType.Int,4),
					new SqlParameter("@modetypename", SqlDbType.VarChar,50),
					new SqlParameter("@isOpen", SqlDbType.TinyInt,1),
					new SqlParameter("@supplier", SqlDbType.Int,4),
					new SqlParameter("@addtime", SqlDbType.DateTime),
					new SqlParameter("@sort", SqlDbType.Int,4),
					new SqlParameter("@release", SqlDbType.Bit,1), 
                    new SqlParameter("@runmode", SqlDbType.TinyInt,4),
                    new SqlParameter("@runset", SqlDbType.VarChar,1000),
                                             new SqlParameter("@supplier2", SqlDbType.Int,4),
                                            new SqlParameter("@suppsWhenExceOccurred", SqlDbType.VarChar,100),
                                            new SqlParameter("@timeout", SqlDbType.Int,4)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.Class;
                parameters[2].Value = model.typeId;
                parameters[3].Value = model.modetypename;
                parameters[4].Value = (int)model.isOpen;
                parameters[5].Value = model.supplier;
                parameters[6].Value = DateTime.Now;
                parameters[7].Value = model.sort;
                parameters[8].Value = model.release;

                parameters[9].Value = model.runmode;
                parameters[10].Value = model.runset;
                parameters[11].Value = model.supplier2;
                parameters[12].Value = model.SuppsWhenExceOccurred;
                parameters[13].Value = model.timeout;

                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_channeltype_Update", parameters);
                bool success = rowsAffected > 0;
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
        public static ChannelTypeInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
                parameters[0].Value = id;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltype_GetModel", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return GetInfoFromRow(ds.Tables[0].Rows[0]);
                }
                return null;
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
        public static ChannelTypeInfo GetModelByTypeId(int typeId)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@typeId", SqlDbType.Int, 4) };
                parameters[0].Value = typeId;

                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltype_GetByTypeId", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return GetInfoFromRow(ds.Tables[0].Rows[0]);
                }
                return null;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static ChannelTypeInfo GetCacheModel(int typeId)
        {
            try
            {
                DataTable data = GetCacheList();

                if (data == null || data.Rows.Count <= 0)
                    return null;

                DataRow[] typeInfo = data.Select("typeId=" + typeId.ToString());
                if (typeInfo.Length <= 0)
                    return null;

                return GetInfoFromRow(typeInfo[0]);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 取类型开通状态
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetChannelTypeStatus(int typeId, int userId)
        {
            int open = 0;

            ChannelTypeInfo typeinfo = ChannelType.GetCacheModel(typeId);
            if (typeinfo == null)
            {
                return open;
            }
            int usersuppid = 0;
            /*系统通道状态*/
            switch (typeinfo.isOpen)
            {
                case OpenEnum.AllClose://全部关闭                    
                    open = 0;
                    break;
                case OpenEnum.AllOpen://全部开启                    
                    open = 1;
                    break;
                case OpenEnum.Close://根据设置 默认为关闭算法，如果后台对用户单独设置了就计算单独的开启状态。 如果没有看通道本身的状态 默认为关闭。如果后台都未设置为默认状态
                    open = BLL.Channel.Channel.GetChanelSysStatus(4, userId, string.Empty, typeId, ref usersuppid);// GetSysOpenStatus(userId, chanelNo, info.typeId, 0);
                    break;
                case OpenEnum.Open://
                    open = BLL.Channel.Channel.GetChanelSysStatus(8, userId, string.Empty, typeId, ref usersuppid); //GetSysOpenStatus(userId, chanelNo, info.typeId, 1);
                    break;
            }

            /*用户通道状态 只有系统通道状态为开启时用户的设置才有效*/
            if (open == 1)
            {
                open = BLL.Channel.Channel.GetUserOpenStatus(userId, string.Empty, typeId, 1);
            }
            return open;
        }

        #region GetInfoFromRow
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ChannelTypeInfo GetInfoFromRow(DataRow dr)
        {
            ChannelTypeInfo model = new ChannelTypeInfo();
            if (dr["id"].ToString() != "")
            {
                model.id = int.Parse(dr["id"].ToString());
            }
            if (dr["classid"].ToString() != "")
            {
                model.Class = (ChannelClassEnum)int.Parse(dr["classid"].ToString());
            }
            if (dr["typeId"].ToString() != "")
            {
                model.typeId = int.Parse(dr["typeId"].ToString());
            }
            model.modetypename = dr["modetypename"].ToString();
            model.code = dr["code"].ToString();
            if (dr["isOpen"].ToString() != "")
            {
                model.isOpen = (OpenEnum)int.Parse(dr["isOpen"].ToString());
            }
            if (dr["supplier"].ToString() != "")
            {
                model.supplier = int.Parse(dr["supplier"].ToString());
            }
            if (dr["suppRate"].ToString() != "")
            {
                model.supprate = Convert.ToDecimal(dr["suppRate"]);
            }
            //if (dr["addtime"].ToString() != "")
            //{
            //    model.addtime = DateTime.Parse(dr["addtime"].ToString());
            //}
            if (dr["sort"].ToString() != "")
            {
                model.sort = int.Parse(dr["sort"].ToString());
            }
            if (dr["release"].ToString() != "")
            {
                if ((dr["release"].ToString() == "1") || (dr["release"].ToString().ToLower() == "true"))
                {
                    model.release = true;
                }
                else
                {
                    model.release = false;
                }
            }
            if (dr["runmode"].ToString() != "")
            {
                model.runmode = int.Parse(dr["runmode"].ToString());
            }
            model.runset = dr["runset"].ToString();
            if (dr["supplier2"].ToString() != "")
            {
                model.supplier2 = int.Parse(dr["supplier2"].ToString());
            }
            if (dr["suppsWhenExceOccurred"].ToString() != "")
            {
                model.SuppsWhenExceOccurred = dr["suppsWhenExceOccurred"].ToString();
            }
            if (dr["timeout"].ToString() != "")
            {
                model.timeout = int.Parse(dr["timeout"].ToString());
            }
            return model;
        }
        #endregion
        #endregion

        #region GetList
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(bool? release)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@release", SqlDbType.Bit,1)};
            if (release.HasValue)
                parameters[0].Value = release.Value;
            else
                parameters[0].Value = DBNull.Value;

            return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltype_GetList", parameters);
        }
        #endregion

        #region GetList
        /// <summary>
        /// 
        /// </summary>
        public static DataTable GetListByUser(int userid)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Bit,1)};
                parameters[0].Value = userid;

                return DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_channeltype_GetListByUser", parameters).Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region CacheData
        #region CacheData
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataTable GetCacheList()
        {
            try
            {
                string cacheKey = CHANNELTYPE_CACHEKEY;

                DataSet ds = new DataSet();

                ds = (DataSet)viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cacheKey);

                if (ds == null)
                {
                    SqlDependency sqlDep = DataBase.AddSqlDependency(cacheKey, SQL_TABLE, SQL_TABLE_FIELD, "", null);
                    ds = GetList(true);
                    viviapi.Cache.WebCache.GetCacheService().AddObject(cacheKey, ds);
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

        #region ChannelTypeInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static ChannelTypeInfo GetModel(int typeId, int userId, out bool enable)
        {
            enable = false;
            int open = 0;

            ChannelTypeInfo typeinfo = ChannelType.GetCacheModel(typeId);
            if (typeinfo == null)
            {
                return null;
            }
            /*系统通道状态*/
            switch (typeinfo.isOpen)
            {
                case OpenEnum.AllClose://全部关闭
                    open = 0;
                    break;
                case OpenEnum.AllOpen://全部开启
                    open = 1;
                    break;
                case OpenEnum.Close://
                    open = GetSysOpenStatus(userId, typeId, 0);
                    break;
                case OpenEnum.Open://
                    open = GetSysOpenStatus(userId, typeId, 1);
                    break;
            }
            /*用户通道状态 只有系统通道状态为开启时用户的设置才有效*/
            if (open == 1)
            {
                open = GetUserOpenStatus(userId, typeId, 1);
            }
            enable = open == 1;
            return typeinfo;
        }

        #region GetSysOpenStatus
        /// <summary>
        /// 系统通道状态 包括后台对用户的设置，通道本身的开户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chanelNo"></param>
        /// <param name="typeId"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static int GetSysOpenStatus(int userId, int typeId, int defaultvalue)
        {
            int result = defaultvalue;
            ChannelTypeUserInfo typeuserinfo = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if (typeuserinfo == null)
                return result;

            /*系统设置状态*/
            if (typeuserinfo.sysIsOpen.HasValue)
            {
                result = typeuserinfo.sysIsOpen.Value ? 1 : 0;
            }
            return result;
        }
        #endregion

        #region GetUserOpenStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="typeId"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static int GetUserOpenStatus(int userId, int typeId, int defaultvalue)
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
        #endregion

        //清理缓存 
        static void ClearCache()
        {
            string cacheKey = CHANNELTYPE_CACHEKEY;
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cacheKey);

            cacheKey = Channel.CHANEL_CACHEKEY;
            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cacheKey);
        }

        #region GetSysTypeId
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceTypeId"></param>
        /// <returns></returns>
        public static int GetSysTypeId(int interfaceTypeId)
        {
            int _interfaceTypeId = interfaceTypeId;
            switch (interfaceTypeId)
            {
                case 1:
                    _interfaceTypeId = 107;//QQ卡
                    break;
                case 2:
                    _interfaceTypeId = 104;//盛大卡                   
                    break;
                case 3:
                    _interfaceTypeId = 106;//骏网卡
                    break;
                case 4:
                    _interfaceTypeId = 117;//亿卡通 =>纵游一卡通
                    break;
                case 5:
                    _interfaceTypeId = 111;//完美一卡通
                    break;
                case 6:
                    _interfaceTypeId = 112;//搜狐一卡通
                    break;
                case 7:
                    _interfaceTypeId = 105;//征途游戏卡
                    break;
                case 8:
                    _interfaceTypeId = 109;//久游一卡通
                    break;
                case 9:
                    _interfaceTypeId = 110;//网易一卡通
                    break;
                case 10:
                    _interfaceTypeId = 118;//天下一卡通
                    break;
                case 11:
                    _interfaceTypeId = 119;//天宏一卡通
                    break;
                case 12:
                    _interfaceTypeId = 113;//电信充值卡
                    break;
                case 13:
                    _interfaceTypeId = 103;//神州行充值卡
                    break;
                case 14:
                    _interfaceTypeId = 108;//联通充值卡
                    break;
                case 15:
                    _interfaceTypeId = 116;//金山一卡通
                    break;
                case 16:
                    _interfaceTypeId = 115;//光宇一卡通
                    break;
                case 17:
                    _interfaceTypeId = 103;//神州行浙江卡
                    break;
                case 18:
                    _interfaceTypeId = 103;//神州行江苏卡
                    break;
                case 19:
                    _interfaceTypeId = 103;//神州行辽宁卡
                    break;
                case 20:
                    _interfaceTypeId = 103;//神州行福建卡
                    break;
                case 21:
                    _interfaceTypeId = 118;//天下一卡通
                    break;
                case 22:
                    _interfaceTypeId = 119;//天宏一卡通
                    break;
                case 23:
                    _interfaceTypeId = 117;//纵游一卡通
                    break;
                case 26:
                    _interfaceTypeId = 208;//殴飞一卡通
                    break;
                case 27:
                    _interfaceTypeId = 209;//天下一卡通专项
                    break;
                case 28:
                    _interfaceTypeId = 210;//盛付通卡
                    break;

            }
            return _interfaceTypeId;
        }
        #endregion

        #region GetSysTypeId
        /// <summary>
        /// 接口类型转换
        /// </summary>
        /// <param name="interfaceTypeId"></param>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public static int GetSysTypeId(int interfaceTypeId, string cardno)
        {
            int typeId = 0;
            switch (interfaceTypeId)
            {
                case 1:
                    typeId = 107;//QQ卡
                    break;
                case 2:
                    typeId = 104;//盛大卡
                    if (cardno.StartsWith("80"))
                    {
                        typeId = 210;//盛付通卡
                    }
                    break;
                case 3:
                    typeId = 106;//骏网卡
                    break;
                case 4:
                    typeId = 117;//亿卡通 =>纵游一卡通
                    break;
                case 5:
                    typeId = 111;//完美一卡通
                    break;
                case 6:
                    typeId = 112;//搜狐一卡通
                    break;
                case 7:
                    typeId = 105;//征途游戏卡
                    break;
                case 8:
                    typeId = 109;//久游一卡通
                    break;
                case 9:
                    typeId = 110;//网易一卡通
                    break;
                case 10:
                    typeId = 118;//魔兽卡=>天下一卡通
                    break;
                case 11:
                    typeId = 119;//联华卡=>天宏一卡通
                    break;
                case 12:
                    typeId = 113;//电信充值卡
                    break;
                case 13:
                    typeId = 103;//神州行充值卡
                    break;
                case 14:
                    typeId = 108;//联通充值卡
                    break;
                case 15:
                    typeId = 116;//金山一卡通
                    break;
                case 16:
                    typeId = 115;//光宇一卡通
                    break;
                case 17:
                    typeId = 103;//神州行浙江卡
                    break;
                case 18:
                    typeId = 103;//神州行江苏卡
                    break;
                case 19:
                    typeId = 103;//神州行辽宁卡
                    break;
                case 20:
                    typeId = 103;//神州行福建卡
                    break;
                case 21:
                    typeId = 118;//天下一卡通
                    break;
                case 22:
                    typeId = 119;//天宏一卡通
                    break;
                case 23:
                    typeId = 117;//天宏一卡通
                    break;
                case 26:
                    typeId = 208;//殴飞一卡通
                    break;
                case 27:
                    typeId = 209;//天下一卡通专项
                    break;
                case 28:
                    typeId = 210;//盛付通卡
                    break;

            }
            return typeId;
        }
        #endregion

        #region IsOpen
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool IsOpen(int typeId, int userId)
        {
            bool open = false;

            ChannelTypeInfo typeinfo = GetCacheModel(typeId);
            if (typeinfo == null)
            {
                return open;
            }

            /*系统通道状态*/
            switch (typeinfo.isOpen)
            {
                case OpenEnum.AllClose://全部关闭
                    open = false;
                    break;
                case OpenEnum.AllOpen://全部开启
                    open = true;
                    break;
                case OpenEnum.Close://
                    open = GetSysOpenStatus(userId, typeId, false);
                    break;
                case OpenEnum.Open://
                    open = GetSysOpenStatus(userId, typeId, true);
                    break;
            }

            /*用户通道状态 只有系统通道状态为开启时用户的设置才有效*/
            if (open == true)
            {
                open = GetUserOpenStatus(userId, typeId, true);
            }

            return open;
        }

        #region GetSysOpenStatus
        /// <summary>
        /// 系统通道状态 包括后台对用户的设置，通道本身的开户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chanelNo"></param>
        /// <param name="typeId"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static bool GetSysOpenStatus(int userId, int typeId, bool defaultvalue)
        {
            bool result = defaultvalue;
            ChannelTypeUserInfo typeuserinfo = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if (typeuserinfo == null)
                return result;

            /*系统设置状态*/
            if (typeuserinfo.sysIsOpen.HasValue)
            {
                result = typeuserinfo.sysIsOpen.Value;
            }
            return result;
        }
        #endregion

        #region GetUserOpenStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="typeId"></param>
        /// <param name="defaultvalue"></param>
        /// <returns></returns>
        public static bool GetUserOpenStatus(int userId, int typeId, bool defaultvalue)
        {
            bool result = defaultvalue;
            ChannelTypeUserInfo typeuserinfo = ChannelTypeUsers.GetCacheModel(userId, typeId);
            if (typeuserinfo == null)
                return result;

            /*用户设置状态*/
            if (typeuserinfo.userIsOpen.HasValue)
            {
                result = typeuserinfo.userIsOpen.Value;
            }

            return result;
        }
        #endregion


        #endregion

        #region CheckCardFormat

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="cardno"></param>
        /// <param name="cardpwd"></param>
        /// <param name="facevalue"></param>
        /// <returns></returns>
        public static bool CheckCardFormat(int typeId, string cardno, string cardpwd, int facevalue)
        {
            bool result = false;
            //全国卡 卡号17位、密码18位的阿拉伯数字 10，20，30，50，100，300，500
            //浙江：卡号10位 密码8位 10，20，30，50，100，200，300，500元，1000元 
            //福建：卡号16位 密码17位 50，100元
            //广东：卡号17位 密码18位 10，30，50，100， 300，500元
            //辽宁：卡号16位 密码21位 50，100元
            string cardnopatt = "^\\d{17}$";
            string cardpwdpatt = "^\\d{18}$";

            if (typeId == 103)
            {
                #region 移动充值卡
                //全国卡
                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                if (!result) // 浙江
                {
                    cardnopatt = "^\\d{10}$";
                    cardpwdpatt = "^\\d{8}$";

                    if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                    {
                        result = true;
                    }
                }
                if (!result) // 福建
                {
                    cardnopatt = "^\\d{16}$";
                    cardpwdpatt = "^\\d{17}$";

                    if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                    {
                        result = true;
                    }
                }
                if (!result) //辽宁
                {
                    cardnopatt = "^\\d{16}$";
                    cardpwdpatt = "^\\d{21}$";

                    if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                    {
                        result = true;
                    }
                }
                #endregion
            }
            else if (typeId == 104)
            {
                #region 盛大游戏卡
                //卡号15位的数字字母，密码8位或9位的阿拉伯数字。
                cardnopatt = "^[0-9a-zA-Z]{15}$";
                cardpwdpatt = "^\\d{8,9}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 105)
            {
                #region 征途卡
                //全国官方征途游戏充值卡，卡号16位阿拉伯数字，密码8位阿拉伯数字。
                cardnopatt = "^\\d{16}$";
                cardpwdpatt = "^\\d{8}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 106)
            {
                #region 骏网一卡通
                //卡号、密码都是16位的阿拉伯数字
                cardnopatt = "^\\d{16}$";
                cardpwdpatt = "^\\d{16}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 107)
            {
                #region 腾讯Q币卡
                //卡全国各地Q币卡，卡号：9位的阿拉伯数字、密码：12位的阿拉伯数字。 
                cardnopatt = "^\\d{9}$";
                cardpwdpatt = "^\\d{12}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 108)
            {
                #region 联通充值卡
                //联通全国卡，卡号15位阿拉伯数字，密码19位阿拉伯数字。 
                cardnopatt = "^\\d{15}$";
                cardpwdpatt = "^\\d{19}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 110)
            {
                #region 网易一卡通
                //全国官方网易游戏充值卡，卡号13位、密码9位的阿拉伯数字 
                cardnopatt = "^\\d{13}$";
                cardpwdpatt = "^\\d{9}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 111)
            {
                #region 完美一卡通
                //全国官方完美游戏充值卡，卡号10位、密码15位的阿拉伯数字  
                cardnopatt = "^\\d{10}$";
                cardpwdpatt = "^\\d{15}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 112)
            {
                #region 搜狐一卡通
                //卡号20位、密码12位的阿拉伯数字 
                cardnopatt = "^\\d{20}$";
                cardpwdpatt = "^\\d{12}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 113)
            {
                #region 电信充值卡
                //中国电信充值付费卡卡号19位，密码18位的阿拉伯数字（即：可拨打11888充值话费的卡）。 
                //目前只支持电信全国卡和广东卡，充值卡序列号第四位为“1”的卡为全国卡，为“2”的则为广东卡。
                cardnopatt = "^\\d{19}$";
                cardpwdpatt = "^\\d{18}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 117)
            {
                #region 纵游一卡通
                //卡号与密码均为15位阿拉伯数字。全国各地能买到纵游一卡通的地区，包括士多店、报刊亭、软件店、网吧、书店等。
                cardnopatt = "^\\d{15}$";
                cardpwdpatt = "^\\d{15}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 118)
            {
                #region 天下通一卡通
                //卡号是15位阿拉伯数字，密码是8位阿拉伯数字，所有实卡的自发行日起，两年内有效。 
                cardnopatt = "^\\d{15}$";
                cardpwdpatt = "^\\d{8}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                #endregion
            }
            else if (typeId == 119)
            {
                #region 天宏一卡通
                //卡号为12位，前2位是大写或小写英文字母，后10位是数字；密码15位是纯数字。 
                //卡号为10位，前2位是大写或小写英文字母，后8位是数字；密码10位是纯数字。 
                cardnopatt = "^[a-zA-Z]{2}\\d{10}$";
                cardpwdpatt = "^\\d{15}$";

                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                if (!result)
                {
                    cardnopatt = "^[a-zA-Z]{2}\\d{8}$";
                    cardpwdpatt = "^\\d{15}$";

                    if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                    {
                        result = true;
                    }
                }
                #endregion
            }

            return result;
        }
        #endregion

        #region QuickValidate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="express"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool QuickValidate(string express, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            var regex = new Regex(express, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            return regex.IsMatch(value);
        }
        #endregion

        #region IsShengFuTong
        /// <summary>
        /// 
        /// 是否为盛付通卡
        /// 80133+YA YB YC YD
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public static bool IsShengFuTong(string cardno)
        {
            if (string.IsNullOrEmpty(cardno))
            {
                return false;
            }

            string pattern = "^(8013|YA|YB|YC|YD)";
            return QuickValidate(pattern, cardno);

            //cardno = cardno.ToUpper();
            //if (cardno.StartsWith("80133")
            //    || cardno.StartsWith("YA")
            //    || cardno.StartsWith("YB")
            //    || cardno.StartsWith("YC")
            //    || cardno.StartsWith("YD")
            //    )
            //{
            //    return true;
            //}
            //return false;

            //string  pattern = "^(80133|YA|YB|YC|YD)";    
            //return QuickValidate(pattern,cardno);
        }
        #endregion


    }
}
