using viviLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using viviapi.Model;
using DBAccess;
using viviapi.Model;
using viviapi.Model.User;

namespace viviapi.BLL
{
    public class RegUserFactory : BaseFactory
    {
        public DataTable GetUserList(RegUserInfo reguserinfo, DateTime stime, DateTime etime)
        {
            List<SqlParameter> sqlprams = new List<SqlParameter>();
            if (reguserinfo.Cid > 0)
            {
                sqlprams.Add(DataBase.MakeInParam("@Cid", SqlDbType.Int, 4, reguserinfo.Cid));
            }
            if (reguserinfo.Uid > 0)
            {
                sqlprams.Add(DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, reguserinfo.Uid));
            }
            sqlprams.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, stime));
            sqlprams.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, etime));
            sqlprams.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, base.Page));
            sqlprams.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, base.PageSize));
            SqlParameter _totalparm = DataBase.MakeOutParam("@total", SqlDbType.Int, 4);
            sqlprams.Add(_totalparm);
            SqlParameter[] prams = sqlprams.ToArray();
            DataTable dt = DataBase.ExecuteDataset(CommandType.StoredProcedure, "GetRegUserList", prams).Tables[0];
            base.Total = (int) _totalparm.Value;
            return dt;
        }

        public DataTable GetUserList(RegUserInfo reguserinfo, AdsTypeEnum adstype, DateTime stime, DateTime etime)
        {
            List<SqlParameter> sqlprams = new List<SqlParameter>();
            if (reguserinfo.Cid > 0)
            {
                sqlprams.Add(DataBase.MakeInParam("@Cid", SqlDbType.Int, 4, reguserinfo.Cid));
            }
            if (reguserinfo.Uid > 0)
            {
                sqlprams.Add(DataBase.MakeInParam("@Uid", SqlDbType.Int, 4, reguserinfo.Uid));
            }
            sqlprams.Add(DataBase.MakeInParam("@adstype", SqlDbType.TinyInt, 1, (int) adstype));
            sqlprams.Add(DataBase.MakeInParam("@stime", SqlDbType.DateTime, 8, stime));
            sqlprams.Add(DataBase.MakeInParam("@etime", SqlDbType.DateTime, 8, etime));
            sqlprams.Add(DataBase.MakeInParam("@page", SqlDbType.Int, 4, base.Page));
            sqlprams.Add(DataBase.MakeInParam("@pagesize", SqlDbType.Int, 4, base.PageSize));
            SqlParameter _totalparm = DataBase.MakeOutParam("@total", SqlDbType.Int, 4);
            sqlprams.Add(_totalparm);
            SqlParameter[] prams = sqlprams.ToArray();
            DataTable dt = DataBase.ExecuteDataset(CommandType.StoredProcedure, "GetRegUserList", prams).Tables[0];
            base.Total = (int) _totalparm.Value;
            return dt;
        }
    }
}

