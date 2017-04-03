using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.Model.User;
using DBAccess;
using viviLib.ExceptionHandling;
using viviapi.Model;

namespace viviapi.BLL.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserSetting
    {
        private readonly viviapi.DAL.User.UserSetting dal = new viviapi.DAL.User.UserSetting();

        public static UserSetting Instance
        {
            get
            {
                var instance = new UserSetting();
                return instance;
            }
        }

        public bool PayRateConfig(Model.User.UserSettingInfo model)
        {
            try
            {
                return dal.PayRateConfig(model) == 1;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public bool Insert(Model.User.UserSettingInfo model)
        {
            try
            {
                return dal.Insert(model);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.User.UserSettingInfo GetModel(int userid)
        {
            try
            {
                return dal.GetModel(userid);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        public Model.User.UserSettingInfo GetModel(int userid,int manageid)
        {
            try
            {
                return dal.GetModel(userid, manageid);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
    }
}
