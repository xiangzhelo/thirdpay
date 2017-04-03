using System;
using System.Data.Common;
using System.Data;
using viviapi.SysConfig;
using MemcachedLib;
using viviLib.ExceptionHandling;

namespace viviapi.Cache
{
    /// <summary>
    /// 企业级MemCache缓存策略类,只能使用一个web园程序
    /// </summary>
    public class MemCachedStrategy : DefaultCacheStrategy
    {
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">存的对象</param>
        public override void AddObject(string objId, object o)
        {
            if (MemCachedConfig.LocalCacheTime > 0)
                base.AddObject(objId, o, MemCachedConfig.LocalCacheTime);
            else
            {
                base.AddObject(objId, o);
            }

            if(MemCachedConfig.MemCacheTime > 0)
                MemCachedManager.CacheClient.Set(objId, o, DateTime.Now.AddSeconds(MemCachedConfig.MemCacheTime));
            else
                MemCachedManager.CacheClient.Set(objId, o);

            RecordLog(objId, "set");
        }

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算</param>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        public virtual void AddObject(int hashCode, string objId, object o)
        {
            if (MemCachedConfig.LocalCacheTime > 0)
                base.AddObject(objId, o, MemCachedConfig.LocalCacheTime);
            else
                base.AddObject(objId, o);

            if (MemCachedConfig.MemCacheTime > 0)
                MemCachedManager.CacheClient.Set(objId, o, DateTime.Now.AddSeconds(MemCachedConfig.MemCacheTime), hashCode);
            else
                MemCachedManager.CacheClient.Set(objId, o, hashCode);

            RecordLog(objId, "set");
        }

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="expried">过期时间</param> 
        public override void AddObject(string objId, object o, int expried)
        {
            base.AddObject(objId, o);
            MemCachedManager.CacheClient.Set(objId, o, DateTime.Now.AddSeconds(expried));
            RecordLog(objId, "set");
        }

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="saved">是否持久化保存</param>
        public override void AddObject(string objId, object o, bool saved)
        {
            AddObject(objId, o);
            if(saved)
                SaveObject(objId, o);
        }

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="expried">过期时间</param> 
        /// <param name="saved">是否持久化保存</param>
        public override void AddObject(string objId, object o, int expried, bool saved)
        {
            AddObject(objId, o, expried);
            if (saved)
                SaveObject(objId, o);
        }

        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算</param>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="saved">是否持久化保存</param>
        public override void AddObject(int hashCode, string objId, object o, bool saved)
        {
            AddObject(hashCode, objId, o);
            if (saved)
                SaveObject(objId, o);
        }

       
        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        public override void RemoveObject(string objId)
        {
            try
            {
                base.RemoveObject(objId);

                MemCachedManager.CacheClient.Delete(objId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            //MemcachedLib.SyncCache.SyncRemoteCache(objId);
        }

        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算</param>
        /// <param name="objId">对象的键值</param>
        public override void RemoveObject(int hashCode, string objId)
        {
            base.RemoveObject(objId);
            MemCachedManager.CacheClient.Delete(objId, hashCode, DateTime.MaxValue);
            //MemcachedLib.SyncCache.SyncRemoteCache(objId);
        }

        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="saved">是否被持久化，如为true，则在缓存中找不到后，从数据库中检索相应信息并返回</param>
        public override void RemoveObject(string objId, bool saved)
        {
            RemoveObject(objId);
            if (saved)
            {
                //DbParameter[] parms = { DbHelper.MakeInParam("@cachekey", (DbType)SqlDbType.NVarChar, 200, objId) };
                //DbHelper.ExecuteScalar("DELETE FROM [cachedobjects] WHERE cachekey = @cachekey", parms);
            }
        }

        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <returns>返回被缓存的对象</returns>
        public override object RetrieveObject(string objId)
        {
            object obj = base.RetrieveObject(objId);
            //因为要测试llserver,这里将下面两行代码注释了
            if (obj == null)
            {
                obj = MemCachedManager.CacheClient.Get(objId);
                //if (obj != null)
                //    base.AddObject(objId, obj);

                RecordLog(objId, "get");
            }
            return obj;
        }


        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算</param>
        /// <param name="objId">对象的键值</param>
        /// <returns>返回被缓存的对象</returns>
        public override object RetrieveObject(int hashCode, string objId)
        {
            object obj = base.RetrieveObject(objId);
            //因为要测试llserver,这里将下面两行代码注释了
            if(obj == null)
            {
                obj = MemCachedManager.CacheClient.Get(objId, hashCode);
                //if (obj != null)
                //    base.AddObject(objId, obj);

                RecordLog(objId, "get");
            }
            return obj;
        }

        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="type">返回的结果类型</param>
        /// <param name="saved">是否被持久化，如为true，则在缓存中找不到后，从数据库中检索相应信息并返回</param>
        /// <returns>返回被缓存的对象</returns>
        public override object RetrieveObject(string objId, Type type, bool saved)
        {
            object obj = RetrieveObject(objId);
            if (obj == null && saved)
            {
                //DbParameter[] parms = { DbHelper.MakeInParam("@cachekey", (DbType)SqlDbType.NVarChar, 200, objId) };
                //DbDataReader reader = DbHelper.ExecuteReader("SELECT [type], [cachevalue] FROM [cachedobjects] WHERE [cachekey] = '" + objId + "'", null);
                //while (reader.Read())
                //{
                //    obj = SerializationHelper.DeSerialize(type, reader["cachevalue"].ToString());
                //    break;
                //}
                //reader.Close();

                //if (obj != null)
                //    AddObject(objId, obj);//加载进入缓存
            }
            return obj;
        }

        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算</param>
        /// <param name="objId">对象的键值</param>
        /// <param name="type">返回的结果类型</param>
        /// <param name="saved">是否被持久化，如为true，则在缓存中找不到后，从数据库中检索相应信息并返回</param>
        /// <returns>获取缓存的对象</returns>
        public override object RetrieveObject(int hashCode, string objId, Type type, bool saved)
        {
            object obj = RetrieveObject(hashCode, objId);
            if (obj == null && saved)
            {
                //DbParameter[] parms = { DbHelper.MakeInParam("@cachekey", (DbType)SqlDbType.NVarChar, 200, objId) };
                //DbDataReader reader = DbHelper.ExecuteReader("SELECT [type], [cachevalue] FROM [cachedobjects] WHERE [cachekey] = '" + objId + "'", null);
                //while (reader.Read())
                //{
                //    obj = SerializationHelper.DeSerialize(type, reader["cachevalue"].ToString());
                //    break;
                //}
                //reader.Close();

                if (obj != null)
                    AddObject(hashCode, objId, obj);//加载进入缓存
            }
            return obj;
        }

        /// <summary>
        /// 记录日志方法
        /// </summary>
        /// <param name="objId">缓存键值</param>
        /// <param name="opName">操作名称(set,get)</param>
        private void RecordLog(string objId, string opName)
        {
            try
            {
                //当启用写入数据日志时
                if (MemCachedConfig.RecordeLog)
                {
                    //DbParameter[] parms = {
                    //                    DbHelper.MakeInParam("@cachekey", (DbType)SqlDbType.NVarChar, 200, objId),
                    //                    DbHelper.MakeInParam("@opname", (DbType)SqlDbType.NVarChar, 10, opName),
                    //                    DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8, Utils.GetDateTime())
                    //                };
                    //DbHelper.ExecuteScalar("INSERT INTO memcachedlogs (cachekey, opname, postdatetime) Values (@cachekey, @opname, @postdatetime)", parms);
                }
            }
            catch { }
        }

        /// <summary>
        /// 持久化缓存对象到数据库
        /// </summary>
        /// <param name="objId">缓存键值</param>
        /// <param name="opName">操作名称(set,get)</param>
        private void SaveObject(string objId, object o)
        {
            try
            {
                //string value = SerializationHelper.Serialize(o);
                //DbParameter[] parms = {
                //                        DbHelper.MakeInParam("@cachekey", (DbType)SqlDbType.NVarChar, 200, objId),
                //                        DbHelper.MakeInParam("@type", (DbType)SqlDbType.NVarChar, 200, o.GetType().ToString()),
                //                        DbHelper.MakeInParam("@postdatetime", (DbType)SqlDbType.DateTime, 8, Utils.GetDateTime()),
                //                        DbHelper.MakeInParam("@cachevalue", (DbType)SqlDbType.NText, 0, value)
                //                    };

                ////object obj = SerializationHelper.DeSerialize(o.GetType(), value);
                ////object obj = SerializationHelper.DeSerialize(Type.GetType(o.GetType().ToString()), value);
                //DbHelper.ExecuteScalar("DELETE FROM [cachedobjects] WHERE cachekey = @cachekey; INSERT INTO [cachedobjects] ([cachekey], [type], [postdatetime], [cachevalue]) Values (@cachekey, @type, @postdatetime, @cachevalue)", parms);
            }
            catch { }
        }



        /// <summary>
        /// 到期时间
        /// </summary>
        public override int TimeOut
        {
            get
            {
                return MemCachedConfig.MemCacheTime; //0为永不过期
            }
        }

        public int LocalCacheTime
        {
            get
            {
                return MemCachedConfig.LocalCacheTime;//0为使用基类"DefaultCacheStrategy"中的TimeOut属性
            }
        }
    }

    #region memcachedlogs数据表结构
    /*
    CREATE TABLE [dbo].[memcachedlogs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cachekey] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL CONSTRAINT [DF_memcachedlogs_cachekey]  DEFAULT (''),
	[opname] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL CONSTRAINT [DF_memcachedlogs_opname]  DEFAULT (''),
	[postdatetime] [datetime] NOT NULL CONSTRAINT [DF_memcachedlogs_postdatetime]  DEFAULT (getdate())
    ) ON [PRIMARY]
    */
    #endregion

    #region cachedobjects持久化数据表结构
    /*
     * 
IF OBJECT_ID('cachedobjects') IS NOT NULL
DROP TABLE [cachedobjects]
GO
     
    CREATE TABLE [cachedobjects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cachekey] [nchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL CONSTRAINT [DF_memcachedlogs_cachekey]  DEFAULT (''),
	[type] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL CONSTRAINT [DF_memcachedlogs_opname]  DEFAULT (''),
	[postdatetime] [datetime] NOT NULL CONSTRAINT [DF_memcachedlogs_postdatetime]  DEFAULT (getdate()),
    [cachevalue] [ntext] NOT NULL CONSTRAINT [DF_dnt_advertisements_code] DEFAULT (''),
    ) ON [PRIMARY]
    
    CREATE UNIQUE CLUSTERED  INDEX [cachekey_index] ON [cachedobjects] ([cachekey])	ON [PRIMARY]

    */
    #endregion

}
