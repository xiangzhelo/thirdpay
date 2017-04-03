using System;

namespace viviapi.Cache
{
    public interface ICacheStrategy
    {
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        void AddObject(string objId, object o);
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算</param>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        void AddObject(int hashCode, string objId, object o);
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="expires">过期时间,单位:秒</param>
        void AddObject(string objId, object o, int expires);
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="saved">是否持久化保存</param>
        void AddObject(string objId, object o, bool saved);
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="expires">过期时间,单位:秒</param>
        /// <param name="saved">是否持久化保存</param>
        void AddObject(string objId, object o, int expires, bool saved);
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算</param>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="saved">是否持久化保存</param>
        void AddObject(int hashCode, string objId, object o, bool saved);
        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        void RemoveObject(string objId);
        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="saved">是否被持久化，如为true，则在缓存中找不到后，从数据库中检索相应信息并返回</param>
        void RemoveObject(string objId, bool saved);
        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算</param>
        /// <param name="objId">对象的键值</param>
        void RemoveObject(int hashCode, string objId);
        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <returns>获取缓存的对象</returns>
        object RetrieveObject(string objId);
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算</param>
        /// <param name="objId">对象的键值</param>
        /// <returns>获取缓存的对象</returns>
        object RetrieveObject(int hashCode, string objId);
        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="type">返回的结果类型</param>
        /// <param name="saved">是否被持久化，如为true，则在缓存中找不到后，从数据库中检索相应信息并返回</param>
        /// <returns>获取缓存的对象</returns>
        object RetrieveObject(string objId, Type type, bool saved);
        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="hashCode">用户指定的hashCode，如该值被指定，则使用该值而不是缓存键进行Hashing计算</param>
        /// <param name="objId">对象的键值</param>
        /// <param name="type">返回的结果类型</param>
        /// <param name="saved">是否被持久化，如为true，则在缓存中找不到后，从数据库中检索相应信息并返回</param>
        /// <returns>获取缓存的对象</returns>
        object RetrieveObject(int hashCode, string objId, Type type, bool saved);
        /// <summary>
        /// 到期时间,单位：秒
        /// </summary>
        int TimeOut { set; get; }
    }
}

