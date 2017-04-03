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
    /// ���ݷ�����:Msg
    /// </summary>
    public partial class InternalMessage
    {
        private static viviapi.DAL.Communication.InternalMessage dal = new viviapi.DAL.Communication.InternalMessage();

        public InternalMessage()
        { }

        #region  Method
        /// <summary>
        /// �Ƿ���ڸü�¼
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
        /// ����һ������
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
        /// ����һ������
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
        /// ɾ��һ������
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
        /// ɾ��һ������
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
        /// �õ�һ������ʵ��
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
        /// �õ�һ������ʵ��
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
        /// �õ�һ������ʵ��
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
        /// ����������������ָ����ҳ���̻���Ϣ��
        /// </summary>
        /// <param name="searchParams">�����������顣</param>
        /// <param name="pageSize">��ҳ��С��</param>
        /// <param name="page">ҳ�롣</param>
        /// <param name="orderby">����ʽ��</param>
        /// <returns>��ҳ���ݡ�</returns>
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
                viewName = "����Ա";
            else if (useType == 1)
                viewName = "�û�";
            else
            {
                viewName = "ϵͳ��Ϣ";
            }
            return viewName;

        }

        #endregion  Method
    }
}

