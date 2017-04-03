using System;
using System.Data;
using System.Data.SqlClient;
using DBAccess;
using viviapi.Model.User;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserSetting
    {
        public int PayRateConfig(UserSettingInfo info)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@userId", SqlDbType.Int, 4),
                new SqlParameter("@special", SqlDbType.TinyInt, 1),
                new SqlParameter("@payrate", SqlDbType.Int, 4)
            };
            parameters[0].Value = info.userid;
            parameters[1].Value = info.special;
            parameters[2].Value = info.payrate;

            object ds = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_usersetting_payrate_config", parameters);

            if (ds == null || ds == DBNull.Value)
                return 0;

            return Convert.ToInt32(ds);
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public bool Insert(Model.User.UserSettingInfo model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@defaultpay", SqlDbType.Int,4),
                    new SqlParameter("@payrate", SqlDbType.VarChar,1000),
                    new SqlParameter("@special", SqlDbType.Int,4),
                    new SqlParameter("@istransfer", SqlDbType.TinyInt,1) ,
                    new SqlParameter("@isRequireAgentDistAudit", SqlDbType.TinyInt,1),
                    new SqlParameter("@RiskWarning", SqlDbType.TinyInt,1) ,
                    new SqlParameter("@AlipayRiskWarning", SqlDbType.TinyInt,1) ,
                    new SqlParameter("@AliCodeRiskWarning", SqlDbType.TinyInt,1) ,
                    new SqlParameter("@WxPayRiskWarning", SqlDbType.TinyInt,1) 
   
                                            };
            parameters[0].Value = model.userid;
            parameters[1].Value = model.defaultpay;
            parameters[2].Value = model.payrate;
            parameters[3].Value = model.special;
            parameters[4].Value = model.istransfer;
            parameters[5].Value = model.isRequireAgentDistAudit;
            parameters[6].Value = model.RiskWarning;
            parameters[7].Value = model.AlipayRiskWarning;
            parameters[8].Value = model.AliCodeRiskWarning;
            parameters[9].Value = model.WxPayRiskWarning;

            rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_usersetting_Insert", parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.User.UserSettingInfo GetModel(int userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)			};
            parameters[0].Value = userid;

            var model = new Model.User.UserSettingInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersetting_GetModel", parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["userid"] != null && ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["defaultpay"] != null && ds.Tables[0].Rows[0]["defaultpay"].ToString() != "")
                {
                    model.defaultpay = int.Parse(ds.Tables[0].Rows[0]["defaultpay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["special"] != null && ds.Tables[0].Rows[0]["special"].ToString() != "")
                {
                    model.special = int.Parse(ds.Tables[0].Rows[0]["special"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payrate"] != null && ds.Tables[0].Rows[0]["payrate"].ToString() != "")
                {
                    model.payrate = int.Parse(ds.Tables[0].Rows[0]["payrate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["istransfer"] != null && ds.Tables[0].Rows[0]["istransfer"].ToString() != "")
                {
                    model.istransfer = int.Parse(ds.Tables[0].Rows[0]["istransfer"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isRequireAgentDistAudit"] != null && ds.Tables[0].Rows[0]["isRequireAgentDistAudit"].ToString() != "")
                {
                    model.isRequireAgentDistAudit = byte.Parse(ds.Tables[0].Rows[0]["isRequireAgentDistAudit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RiskWarning"] != null && ds.Tables[0].Rows[0]["RiskWarning"].ToString() != "")
                {
                    model.RiskWarning = byte.Parse(ds.Tables[0].Rows[0]["RiskWarning"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AlipayRiskWarning"] != null && ds.Tables[0].Rows[0]["AlipayRiskWarning"].ToString() != "")
                {
                    model.AlipayRiskWarning = byte.Parse(ds.Tables[0].Rows[0]["AlipayRiskWarning"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AliCodeRiskWarning"] != null && ds.Tables[0].Rows[0]["AliCodeRiskWarning"].ToString() != "")
                {
                    model.AliCodeRiskWarning = byte.Parse(ds.Tables[0].Rows[0]["AliCodeRiskWarning"].ToString());
                }
                if (ds.Tables[0].Rows[0]["WxPayRiskWarning"] != null && ds.Tables[0].Rows[0]["WxPayRiskWarning"].ToString() != "")
                {
                    model.WxPayRiskWarning = byte.Parse(ds.Tables[0].Rows[0]["WxPayRiskWarning"].ToString());
                }
                return model;
            }
            else
            {
                model.userid = userid;
                return model;
            }
        }

        public Model.User.UserSettingInfo GetModel(int userid,int manageid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
                    new SqlParameter("@manageid", SqlDbType.Int,4)};
            parameters[0].Value = userid;
            parameters[2].Value = manageid;

            Model.User.UserSettingInfo model = new Model.User.UserSettingInfo();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_usersetting_GetModel2", parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["userid"] != null && ds.Tables[0].Rows[0]["userid"].ToString() != "")
                {
                    model.userid = int.Parse(ds.Tables[0].Rows[0]["userid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["defaultpay"] != null && ds.Tables[0].Rows[0]["defaultpay"].ToString() != "")
                {
                    model.defaultpay = int.Parse(ds.Tables[0].Rows[0]["defaultpay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["special"] != null && ds.Tables[0].Rows[0]["special"].ToString() != "")
                {
                    model.special = int.Parse(ds.Tables[0].Rows[0]["special"].ToString());
                }
                if (ds.Tables[0].Rows[0]["payrate"] != null && ds.Tables[0].Rows[0]["payrate"].ToString() != "")
                {
                    model.payrate = int.Parse(ds.Tables[0].Rows[0]["payrate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["istransfer"] != null && ds.Tables[0].Rows[0]["istransfer"].ToString() != "")
                {
                    model.istransfer = int.Parse(ds.Tables[0].Rows[0]["istransfer"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isRequireAgentDistAudit"] != null && ds.Tables[0].Rows[0]["isRequireAgentDistAudit"].ToString() != "")
                {
                    model.isRequireAgentDistAudit = byte.Parse(ds.Tables[0].Rows[0]["isRequireAgentDistAudit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RiskWarning"] != null && ds.Tables[0].Rows[0]["RiskWarning"].ToString() != "")
                {
                    model.RiskWarning = byte.Parse(ds.Tables[0].Rows[0]["RiskWarning"].ToString());
                }
                return model;
            }
            else
            {
                model.userid = userid;
                return model;
            }
        }
    }
}
