using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using viviLib.Data;
using viviLib.ExceptionHandling;
namespace viviapi.BLL.Quota
{
    public class Quotaquery
    {
        public static Quotaquery Quotaqueryin
        {
            get
            {
                var Quotaqueryin = new Quotaquery();
                return Quotaqueryin;
            }
        }
        internal const string SqlTable = "quotaOrder";
        internal const string key = "[id]";
        internal const string FIELDS = @"
     [userid]
      ,[[quota_type]]
      ,[[quota_balance]]
      ,[[quota_payment]]
      ,[[quota_unpayment]]";

        /// <summary>
        ///     获得AG额度 根据userid
        /// </summary>
        public DataSet GetquotabyUserid(string userid,int quota_type)
        {
            var strSql = new StringBuilder();
            strSql.Append(
                "select userid,quota_type,quota_balance,quota_payment,quota_unpayment");
            strSql.Append(" FROM [api].[dbo].[quota] ");
            if (userid.Trim() != "")
            {
                strSql.Append(" where userid= " + userid + " and quota_type="+ quota_type);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
