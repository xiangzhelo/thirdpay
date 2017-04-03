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
    /// ֧��ͨ�����
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
        ///  ����һ������
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
        ///  ����һ������
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
        /// �õ�һ������ʵ��
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
        /// �õ�һ������ʵ��
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
        /// ȡ���Ϳ�ͨ״̬
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
            /*ϵͳͨ��״̬*/
            switch (typeinfo.isOpen)
            {
                case OpenEnum.AllClose://ȫ���ر�                    
                    open = 0;
                    break;
                case OpenEnum.AllOpen://ȫ������                    
                    open = 1;
                    break;
                case OpenEnum.Close://�������� Ĭ��Ϊ�ر��㷨�������̨���û����������˾ͼ��㵥���Ŀ���״̬�� ���û�п�ͨ�������״̬ Ĭ��Ϊ�رա������̨��δ����ΪĬ��״̬
                    open = BLL.Channel.Channel.GetChanelSysStatus(4, userId, string.Empty, typeId, ref usersuppid);// GetSysOpenStatus(userId, chanelNo, info.typeId, 0);
                    break;
                case OpenEnum.Open://
                    open = BLL.Channel.Channel.GetChanelSysStatus(8, userId, string.Empty, typeId, ref usersuppid); //GetSysOpenStatus(userId, chanelNo, info.typeId, 1);
                    break;
            }

            /*�û�ͨ��״̬ ֻ��ϵͳͨ��״̬Ϊ����ʱ�û������ò���Ч*/
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
        /// ��������б�
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
        /// ��������б�
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
            /*ϵͳͨ��״̬*/
            switch (typeinfo.isOpen)
            {
                case OpenEnum.AllClose://ȫ���ر�
                    open = 0;
                    break;
                case OpenEnum.AllOpen://ȫ������
                    open = 1;
                    break;
                case OpenEnum.Close://
                    open = GetSysOpenStatus(userId, typeId, 0);
                    break;
                case OpenEnum.Open://
                    open = GetSysOpenStatus(userId, typeId, 1);
                    break;
            }
            /*�û�ͨ��״̬ ֻ��ϵͳͨ��״̬Ϊ����ʱ�û������ò���Ч*/
            if (open == 1)
            {
                open = GetUserOpenStatus(userId, typeId, 1);
            }
            enable = open == 1;
            return typeinfo;
        }

        #region GetSysOpenStatus
        /// <summary>
        /// ϵͳͨ��״̬ ������̨���û������ã�ͨ������Ŀ���״̬
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

            /*ϵͳ����״̬*/
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

            /*�û�����״̬*/
            if (typeuserinfo.userIsOpen.HasValue)
            {
                result = typeuserinfo.userIsOpen.Value ? 1 : 0;
            }

            return result;
        }
        #endregion


        #endregion
        #endregion

        //������ 
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
                    _interfaceTypeId = 107;//QQ��
                    break;
                case 2:
                    _interfaceTypeId = 104;//ʢ��                   
                    break;
                case 3:
                    _interfaceTypeId = 106;//������
                    break;
                case 4:
                    _interfaceTypeId = 117;//�ڿ�ͨ =>����һ��ͨ
                    break;
                case 5:
                    _interfaceTypeId = 111;//����һ��ͨ
                    break;
                case 6:
                    _interfaceTypeId = 112;//�Ѻ�һ��ͨ
                    break;
                case 7:
                    _interfaceTypeId = 105;//��;��Ϸ��
                    break;
                case 8:
                    _interfaceTypeId = 109;//����һ��ͨ
                    break;
                case 9:
                    _interfaceTypeId = 110;//����һ��ͨ
                    break;
                case 10:
                    _interfaceTypeId = 118;//����һ��ͨ
                    break;
                case 11:
                    _interfaceTypeId = 119;//���һ��ͨ
                    break;
                case 12:
                    _interfaceTypeId = 113;//���ų�ֵ��
                    break;
                case 13:
                    _interfaceTypeId = 103;//�����г�ֵ��
                    break;
                case 14:
                    _interfaceTypeId = 108;//��ͨ��ֵ��
                    break;
                case 15:
                    _interfaceTypeId = 116;//��ɽһ��ͨ
                    break;
                case 16:
                    _interfaceTypeId = 115;//����һ��ͨ
                    break;
                case 17:
                    _interfaceTypeId = 103;//�������㽭��
                    break;
                case 18:
                    _interfaceTypeId = 103;//�����н��տ�
                    break;
                case 19:
                    _interfaceTypeId = 103;//������������
                    break;
                case 20:
                    _interfaceTypeId = 103;//�����и�����
                    break;
                case 21:
                    _interfaceTypeId = 118;//����һ��ͨ
                    break;
                case 22:
                    _interfaceTypeId = 119;//���һ��ͨ
                    break;
                case 23:
                    _interfaceTypeId = 117;//����һ��ͨ
                    break;
                case 26:
                    _interfaceTypeId = 208;//Ź��һ��ͨ
                    break;
                case 27:
                    _interfaceTypeId = 209;//����һ��ͨר��
                    break;
                case 28:
                    _interfaceTypeId = 210;//ʢ��ͨ��
                    break;

            }
            return _interfaceTypeId;
        }
        #endregion

        #region GetSysTypeId
        /// <summary>
        /// �ӿ�����ת��
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
                    typeId = 107;//QQ��
                    break;
                case 2:
                    typeId = 104;//ʢ��
                    if (cardno.StartsWith("80"))
                    {
                        typeId = 210;//ʢ��ͨ��
                    }
                    break;
                case 3:
                    typeId = 106;//������
                    break;
                case 4:
                    typeId = 117;//�ڿ�ͨ =>����һ��ͨ
                    break;
                case 5:
                    typeId = 111;//����һ��ͨ
                    break;
                case 6:
                    typeId = 112;//�Ѻ�һ��ͨ
                    break;
                case 7:
                    typeId = 105;//��;��Ϸ��
                    break;
                case 8:
                    typeId = 109;//����һ��ͨ
                    break;
                case 9:
                    typeId = 110;//����һ��ͨ
                    break;
                case 10:
                    typeId = 118;//ħ�޿�=>����һ��ͨ
                    break;
                case 11:
                    typeId = 119;//������=>���һ��ͨ
                    break;
                case 12:
                    typeId = 113;//���ų�ֵ��
                    break;
                case 13:
                    typeId = 103;//�����г�ֵ��
                    break;
                case 14:
                    typeId = 108;//��ͨ��ֵ��
                    break;
                case 15:
                    typeId = 116;//��ɽһ��ͨ
                    break;
                case 16:
                    typeId = 115;//����һ��ͨ
                    break;
                case 17:
                    typeId = 103;//�������㽭��
                    break;
                case 18:
                    typeId = 103;//�����н��տ�
                    break;
                case 19:
                    typeId = 103;//������������
                    break;
                case 20:
                    typeId = 103;//�����и�����
                    break;
                case 21:
                    typeId = 118;//����һ��ͨ
                    break;
                case 22:
                    typeId = 119;//���һ��ͨ
                    break;
                case 23:
                    typeId = 117;//���һ��ͨ
                    break;
                case 26:
                    typeId = 208;//Ź��һ��ͨ
                    break;
                case 27:
                    typeId = 209;//����һ��ͨר��
                    break;
                case 28:
                    typeId = 210;//ʢ��ͨ��
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

            /*ϵͳͨ��״̬*/
            switch (typeinfo.isOpen)
            {
                case OpenEnum.AllClose://ȫ���ر�
                    open = false;
                    break;
                case OpenEnum.AllOpen://ȫ������
                    open = true;
                    break;
                case OpenEnum.Close://
                    open = GetSysOpenStatus(userId, typeId, false);
                    break;
                case OpenEnum.Open://
                    open = GetSysOpenStatus(userId, typeId, true);
                    break;
            }

            /*�û�ͨ��״̬ ֻ��ϵͳͨ��״̬Ϊ����ʱ�û������ò���Ч*/
            if (open == true)
            {
                open = GetUserOpenStatus(userId, typeId, true);
            }

            return open;
        }

        #region GetSysOpenStatus
        /// <summary>
        /// ϵͳͨ��״̬ ������̨���û������ã�ͨ������Ŀ���״̬
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

            /*ϵͳ����״̬*/
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

            /*�û�����״̬*/
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
            //ȫ���� ����17λ������18λ�İ��������� 10��20��30��50��100��300��500
            //�㽭������10λ ����8λ 10��20��30��50��100��200��300��500Ԫ��1000Ԫ 
            //����������16λ ����17λ 50��100Ԫ
            //�㶫������17λ ����18λ 10��30��50��100�� 300��500Ԫ
            //����������16λ ����21λ 50��100Ԫ
            string cardnopatt = "^\\d{17}$";
            string cardpwdpatt = "^\\d{18}$";

            if (typeId == 103)
            {
                #region �ƶ���ֵ��
                //ȫ����
                if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                {
                    result = true;
                }
                if (!result) // �㽭
                {
                    cardnopatt = "^\\d{10}$";
                    cardpwdpatt = "^\\d{8}$";

                    if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                    {
                        result = true;
                    }
                }
                if (!result) // ����
                {
                    cardnopatt = "^\\d{16}$";
                    cardpwdpatt = "^\\d{17}$";

                    if (QuickValidate(cardnopatt, cardno) && QuickValidate(cardpwdpatt, cardpwd))
                    {
                        result = true;
                    }
                }
                if (!result) //����
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
                #region ʢ����Ϸ��
                //����15λ��������ĸ������8λ��9λ�İ��������֡�
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
                #region ��;��
                //ȫ���ٷ���;��Ϸ��ֵ��������16λ���������֣�����8λ���������֡�
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
                #region ����һ��ͨ
                //���š����붼��16λ�İ���������
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
                #region ��ѶQ�ҿ�
                //��ȫ������Q�ҿ������ţ�9λ�İ��������֡����룺12λ�İ��������֡� 
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
                #region ��ͨ��ֵ��
                //��ͨȫ����������15λ���������֣�����19λ���������֡� 
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
                #region ����һ��ͨ
                //ȫ���ٷ�������Ϸ��ֵ��������13λ������9λ�İ��������� 
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
                #region ����һ��ͨ
                //ȫ���ٷ�������Ϸ��ֵ��������10λ������15λ�İ���������  
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
                #region �Ѻ�һ��ͨ
                //����20λ������12λ�İ��������� 
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
                #region ���ų�ֵ��
                //�й����ų�ֵ���ѿ�����19λ������18λ�İ��������֣������ɲ���11888��ֵ���ѵĿ����� 
                //Ŀǰֻ֧�ֵ���ȫ�����͹㶫������ֵ�����кŵ���λΪ��1���Ŀ�Ϊȫ������Ϊ��2������Ϊ�㶫����
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
                #region ����һ��ͨ
                //�����������Ϊ15λ���������֡�ȫ��������������һ��ͨ�ĵ���������ʿ��ꡢ����ͤ������ꡢ���ɡ����ȡ�
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
                #region ����ͨһ��ͨ
                //������15λ���������֣�������8λ���������֣�����ʵ�����Է���������������Ч�� 
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
                #region ���һ��ͨ
                //����Ϊ12λ��ǰ2λ�Ǵ�д��СдӢ����ĸ����10λ�����֣�����15λ�Ǵ����֡� 
                //����Ϊ10λ��ǰ2λ�Ǵ�д��СдӢ����ĸ����8λ�����֣�����10λ�Ǵ����֡� 
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
        /// �Ƿ�Ϊʢ��ͨ��
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
