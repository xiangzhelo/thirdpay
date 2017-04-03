using System;
using System.Data;
using DBAccess;
using viviapi.Model.User;

namespace viviapi.DAL.User
{
    public class Login
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isClient"></param>
        /// <param name="sessionId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="loginip"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public DataRow SignIn(byte isClient, byte agentlogin, string sessionId, string username, string password, string loginip, string address)
        {
            DataSet loginReulst = null;

            var prams = new[]
            {
                  DataBase.MakeInParam("@username", SqlDbType.VarChar, 50, username)
                , DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, password)
                , DataBase.MakeInParam("@loginip", SqlDbType.VarChar, 50, loginip)
                , DataBase.MakeInParam("@logintime", SqlDbType.DateTime, 8, DateTime.Now)
                , DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, sessionId)
                , DataBase.MakeInParam("@address", SqlDbType.VarChar, 20,address)
                , DataBase.MakeInParam("@remark", SqlDbType.VarChar, 100, address)
                , DataBase.MakeInParam("@agentlogin", SqlDbType.TinyInt, 1, agentlogin)
                , DataBase.MakeInParam("@isClient", SqlDbType.TinyInt, 1, isClient)
            };

            loginReulst = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_Login", prams);

            return loginReulst.Tables[0].Rows[0];
        }


        public DataRow SignInByEmail(byte isClient, string sessionId, string email, string password, string loginip, string address)
        {
            DataSet loginReulst = null;

            var prams = new[]
            {
                  DataBase.MakeInParam("@email", SqlDbType.VarChar, 50, email)
                , DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, password)
                , DataBase.MakeInParam("@loginip", SqlDbType.VarChar, 50, loginip)
                , DataBase.MakeInParam("@logintime", SqlDbType.DateTime, 8, DateTime.Now)
                , DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, sessionId)
                , DataBase.MakeInParam("@address", SqlDbType.VarChar, 20,address)
                , DataBase.MakeInParam("@remark", SqlDbType.VarChar, 100, address)
                , DataBase.MakeInParam("@isClient", SqlDbType.TinyInt, 1, isClient)
            };

            loginReulst = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_LoginByEmail", prams);

            return loginReulst.Tables[0].Rows[0];
        }

        public DataRow SignInByMobile(byte isClient, string sessionId, string email, string password, string loginip, string address)
        {
            DataSet loginReulst = null;

            var prams = new[]
            {
                  DataBase.MakeInParam("@mobile", SqlDbType.VarChar, 50, email)
                , DataBase.MakeInParam("@password", SqlDbType.VarChar, 100, password)
                , DataBase.MakeInParam("@loginip", SqlDbType.VarChar, 50, loginip)
                , DataBase.MakeInParam("@logintime", SqlDbType.DateTime, 8, DateTime.Now)
                , DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, sessionId)
                , DataBase.MakeInParam("@address", SqlDbType.VarChar, 20,address)
                , DataBase.MakeInParam("@remark", SqlDbType.VarChar, 100, address)
                , DataBase.MakeInParam("@isClient", SqlDbType.TinyInt, 1, isClient)
            };

            loginReulst = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_LoginByMobile", prams);

            return loginReulst.Tables[0].Rows[0];
        }

        public DataRow SignInByPartner(int plant, string openid, string sessionId, string loginip, string address)
        {
            DataSet loginReulst = null;

            var prams = new[]
            {
                  DataBase.MakeInParam("@plant", SqlDbType.TinyInt, 1, plant)
                , DataBase.MakeInParam("@openid", SqlDbType.VarChar, 100, openid)
                , DataBase.MakeInParam("@loginip", SqlDbType.VarChar, 50, loginip)
                , DataBase.MakeInParam("@logintime", SqlDbType.DateTime, 8, DateTime.Now)
                , DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, sessionId)
                , DataBase.MakeInParam("@address", SqlDbType.VarChar, 20,address)
                , DataBase.MakeInParam("@remark", SqlDbType.VarChar, 100, address)
            };

            loginReulst = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_users_LoginByPartner", prams);

            return loginReulst.Tables[0].Rows[0];
        }


        public int GetUserIdBySession(string sessionId)
        {
            var prams = new[]
            {
                DataBase.MakeInParam("@sessionId", SqlDbType.VarChar, 100, sessionId)
            };
            object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getIdBySession", prams);
            if (result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return 0;
        }

        public int GetUserIdByToken(string token)
        {
            var prams = new[]
            {
                DataBase.MakeInParam("@token", SqlDbType.VarChar, 100, token)
            };
            object result = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_users_getIdByToken", prams);
            if (result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            return 0;
        }
    }
}