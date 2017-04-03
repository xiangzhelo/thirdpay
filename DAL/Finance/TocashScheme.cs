using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model.Finance;
using viviapi.Model.Settled;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Finance
{
    /// <summary>
    /// 提现方案操作类
    /// </summary>
    public class TocashScheme
    {
        #region Add
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public  int Add(TocashSchemeInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@schemename", SqlDbType.NVarChar,50),
					new SqlParameter("@minamtlimitofeach", SqlDbType.Decimal,9),
					new SqlParameter("@maxamtlimitofeach", SqlDbType.Decimal,9),
					new SqlParameter("@dailymaxtimes", SqlDbType.Int,4),
					new SqlParameter("@dailymaxamt", SqlDbType.Decimal,9),
					new SqlParameter("@chargerate", SqlDbType.Decimal,9),
					new SqlParameter("@chargeleastofeach", SqlDbType.Decimal,9),
					new SqlParameter("@chargemostofeach", SqlDbType.Decimal,9),
					new SqlParameter("@isdefault", SqlDbType.TinyInt,1),
                    new SqlParameter("@tranapi", SqlDbType.Int,4),
                    new SqlParameter("@bankdetentiondays", SqlDbType.Int,4),
					new SqlParameter("@otherdetentiondays", SqlDbType.Int,4),
					new SqlParameter("@carddetentiondays", SqlDbType.Int,4),
                    new SqlParameter("@tranRequiredAudit", SqlDbType.TinyInt,1),
                    new SqlParameter("@type", SqlDbType.TinyInt,1),
                    
                    new SqlParameter("@upperLimit", SqlDbType.TinyInt,1),
                    new SqlParameter("@lowerLimit", SqlDbType.TinyInt,1),
                    new SqlParameter("@startAmt", SqlDbType.Decimal,9)
                                            };
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1].Value = model.schemename;
                parameters[2].Value = model.minamtlimitofeach;
                parameters[3].Value = model.maxamtlimitofeach;
                parameters[4].Value = model.dailymaxtimes;
                parameters[5].Value = model.dailymaxamt;
                parameters[6].Value = model.chargerate;
                parameters[7].Value = model.chargeleastofeach;
                parameters[8].Value = model.chargemostofeach;
                parameters[9].Value = model.isdefault;
                parameters[10].Value = model.vaiInterface;

                parameters[11].Value = model.bankdetentiondays;
                parameters[12].Value = model.otherdetentiondays;
                parameters[13].Value = model.carddetentiondays;
                parameters[14].Value = model.tranRequiredAudit;
                parameters[15].Value = model.type;

                parameters[16].Value = model.upperLimit;
                parameters[17].Value = model.lowerLimit;
                parameters[18].Value = model.startAmt;
                


                if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_tocashscheme_Add", parameters) > 0)
                {
                    return (int)parameters[0].Value;
                }
                return 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0;
            }
        }
        #endregion

        #region Update
        /// <summary>
        ///  更新一条数据
        /// </summary>
        public  bool Update(TocashSchemeInfo model)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@schemename", SqlDbType.NVarChar,50),
					new SqlParameter("@minamtlimitofeach", SqlDbType.Decimal,9),
					new SqlParameter("@maxamtlimitofeach", SqlDbType.Decimal,9),
					new SqlParameter("@dailymaxtimes", SqlDbType.Int,4),
					new SqlParameter("@dailymaxamt", SqlDbType.Decimal,9),
					new SqlParameter("@chargerate", SqlDbType.Decimal,9),
					new SqlParameter("@chargeleastofeach", SqlDbType.Decimal,9),
					new SqlParameter("@chargemostofeach", SqlDbType.Decimal,9),
					new SqlParameter("@isdefault", SqlDbType.TinyInt,1),
                    new SqlParameter("@tranapi", SqlDbType.Int,4),
                    new SqlParameter("@bankdetentiondays", SqlDbType.Int,4),
					new SqlParameter("@otherdetentiondays", SqlDbType.Int,4),
					new SqlParameter("@carddetentiondays", SqlDbType.Int,4),
                    new SqlParameter("@tranRequiredAudit", SqlDbType.TinyInt,1),
                    new SqlParameter("@type", SqlDbType.TinyInt,1),
                    new SqlParameter("@upperLimit", SqlDbType.TinyInt,1),
                    new SqlParameter("@lowerLimit", SqlDbType.TinyInt,1),
                    new SqlParameter("@startAmt", SqlDbType.Decimal,9)
                                            };
                parameters[0].Value = model.id;
                parameters[1].Value = model.schemename;
                parameters[2].Value = model.minamtlimitofeach;
                parameters[3].Value = model.maxamtlimitofeach;
                parameters[4].Value = model.dailymaxtimes;
                parameters[5].Value = model.dailymaxamt;
                parameters[6].Value = model.chargerate;
                parameters[7].Value = model.chargeleastofeach;
                parameters[8].Value = model.chargemostofeach;
                parameters[9].Value = model.isdefault;
                parameters[10].Value = model.vaiInterface;

                parameters[11].Value = model.bankdetentiondays;
                parameters[12].Value = model.otherdetentiondays;
                parameters[13].Value = model.carddetentiondays;
                parameters[14].Value = model.tranRequiredAudit;
                parameters[15].Value = model.type;

                parameters[16].Value = model.upperLimit;
                parameters[17].Value = model.lowerLimit;
                parameters[18].Value = model.startAmt;
                
                rowsAffected = DataBase.ExecuteNonQuery(CommandType.StoredProcedure,"proc_tocashscheme_Update", parameters);
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public  bool Delete(int id)
        {
            try
            {
                int rowsAffected = 0;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
                parameters[0].Value = id;

                rowsAffected = DataBase.ExecuteNonQuery(
                     CommandType.StoredProcedure, "proc_tocashscheme_Delete", parameters);
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        #endregion

        #region GetModel
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public  TocashSchemeInfo GetModelByUser(int type,int userId)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@type", SqlDbType.TinyInt,1),
					new SqlParameter("@userId", SqlDbType.Int,4)
};
                parameters[0].Value = type;
                parameters[1].Value = userId;
                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_tocashscheme_GetModelByUser", parameters);

                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public  TocashSchemeInfo GetModel(int id)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
                parameters[0].Value = id;
                DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_tocashscheme_GetModel", parameters);

                return GetModelFromDs(ds);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static TocashSchemeInfo GetModelFromDs(DataSet ds)
        {
            TocashSchemeInfo model = new TocashSchemeInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                if (row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                model.schemename = row["schemename"].ToString();
                if (row["minamtlimitofeach"].ToString() != "")
                {
                    model.minamtlimitofeach = decimal.Parse(row["minamtlimitofeach"].ToString());
                }
                if (row["maxamtlimitofeach"].ToString() != "")
                {
                    model.maxamtlimitofeach = decimal.Parse(row["maxamtlimitofeach"].ToString());
                }
                if (row["dailymaxtimes"].ToString() != "")
                {
                    model.dailymaxtimes = int.Parse(row["dailymaxtimes"].ToString());
                }
                if (row["dailymaxamt"].ToString() != "")
                {
                    model.dailymaxamt = decimal.Parse(row["dailymaxamt"].ToString());
                }
                if (row["chargerate"].ToString() != "")
                {
                    model.chargerate = decimal.Parse(row["chargerate"].ToString());
                }
                if (row["chargeleastofeach"].ToString() != "")
                {
                    model.chargeleastofeach = decimal.Parse(row["chargeleastofeach"].ToString());
                }
                if (row["chargemostofeach"].ToString() != "")
                {
                    model.chargemostofeach = decimal.Parse(row["chargemostofeach"].ToString());
                }
                if (row["isdefault"].ToString() != "")
                {
                    model.isdefault = int.Parse(row["isdefault"].ToString());
                }
                if (row["tranapi"].ToString() != "")
                {
                    model.vaiInterface = int.Parse(row["tranapi"].ToString());
                }
                if (row["bankdetentiondays"] != null && row["bankdetentiondays"].ToString() != "")
                {
                    model.bankdetentiondays = int.Parse(row["bankdetentiondays"].ToString());
                }
                if (row["otherdetentiondays"] != null && row["otherdetentiondays"].ToString() != "")
                {
                    model.otherdetentiondays = int.Parse(row["otherdetentiondays"].ToString());
                }
                if (row["carddetentiondays"] != null && row["carddetentiondays"].ToString() != "")
                {
                    model.carddetentiondays = int.Parse(row["carddetentiondays"].ToString());
                }
                if (row["tranRequiredAudit"] != null && row["tranRequiredAudit"].ToString() != "")
                {
                    model.tranRequiredAudit = byte.Parse(row["tranRequiredAudit"].ToString());
                }
                if (row["type"].ToString() != "")
                {
                    model.type = int.Parse(row["type"].ToString());
                }
                if (row["upperLimit"].ToString() != "")
                {
                    model.upperLimit = byte.Parse(row["upperLimit"].ToString());
                }
                if (row["lowerLimit"].ToString() != "")
                {
                    model.lowerLimit = byte.Parse(row["lowerLimit"].ToString());
                }
                if (row["startAmt"].ToString() != "")
                {
                    model.startAmt = decimal.Parse(row["startAmt"].ToString());
                }
                return model;
            }
            else
            {
                return new TocashSchemeInfo();
            }
        }
        #endregion

        #region GetList
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public  DataSet GetList(string strWhere)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,tranRequiredAudit,schemename,minamtlimitofeach,maxamtlimitofeach,dailymaxtimes,dailymaxamt,chargerate,chargeleastofeach,chargemostofeach,isdefault,bankdetentiondays,otherdetentiondays,carddetentiondays ");
                strSql.Append(" FROM tocashscheme ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }
        #endregion
    }
}
