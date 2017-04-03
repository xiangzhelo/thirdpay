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

namespace viviapi.BLL.channel
{
    public class currchannel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <param name="len"></param>
        /// <param name="serverid"></param>
        /// <returns></returns>
        public static int Get(int userid,int typeid,int len,string serverid)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@typeid", SqlDbType.Int,4),
					new SqlParameter("@len", SqlDbType.Int,4),
					new SqlParameter("@serverid", SqlDbType.VarChar,50),
					new SqlParameter("@datetime", SqlDbType.DateTime)};
                parameters[0].Value = userid;
                parameters[1].Value = typeid;
                parameters[2].Value = len;
                parameters[3].Value = serverid;
                parameters[4].Value = DateTime.Now;

                return Convert.ToInt32(DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_currchannel_get", parameters));
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 1;
            }
        }
    }
}
