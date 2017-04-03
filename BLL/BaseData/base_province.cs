using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBAccess;

namespace viviapi.BLL.basedata
{
    public class base_province
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select ProvinceID,ProvinceName,DateCreated,DateUpdated ");
                strSql.Append(" FROM base_province ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return DbHelperSQL.Query(strSql.ToString());
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
                return null;
            }
        }

    }
}
