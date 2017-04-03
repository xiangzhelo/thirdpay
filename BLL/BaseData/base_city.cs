using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBAccess;

namespace viviapi.BLL.basedata
{
    public class base_city
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CityID,CityName,ZipCode,ProvinceID,DateCreated,DateUpdated ");
            strSql.Append(" FROM base_city ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
