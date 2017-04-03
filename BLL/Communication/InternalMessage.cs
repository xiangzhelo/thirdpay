using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model;
using viviapi.Model.News;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.BLL.Communication
{
    /// <summary>
    /// 数据访问类:Msg
    /// </summary>
    public partial class InternalMessage
    {
        private static viviapi.DAL.Communication.InternalMessage dal = new viviapi.DAL.Communication.InternalMessage();

        public InternalMessage()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int ID)
        {
            try
            {
                return dal.Exists(ID);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(Model.News.InternalMessage model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Model.News.InternalMessage model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        public static bool IsRead(int userId)
        {
            try
            {
                return dal.IsRead(userId);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetUserMsgCount(int userId)
        {
            try
            {
                return dal.GetUserMsgCount(userId);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return 0;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int ID)
        {
            try
            {
                return dal.Delete(ID);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool DeleteList(string IDlist)
        {
            try
            {
                return dal.DeleteList(IDlist);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Model.News.InternalMessage GetModel(int ID,int msg_to)
        {
            try
            {
                return dal.GetModel(ID, msg_to);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Model.News.InternalMessage GetModel(int ID)
        {
            try
            {
                return dal.GetModel(ID);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Model.News.InternalMessage GetModelByTo(int msg_to)
        {
            try
            {
                return dal.GetModelByTo(msg_to);
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
                return null;
            }
        }
        /// <summary>
        /// 根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
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

        public static string GetUserTypeName(byte useType)
        {
            string viewName = useType.ToString();
            if (useType == 0)
                viewName = "管理员";
            else if (useType == 1)
                viewName = "用户";
            else
            {
                viewName = "系统消息";
            }
            return viewName;

        }

        #endregion  Method
    }
}

