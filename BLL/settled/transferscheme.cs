using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBAccess;
using viviapi.Model;

namespace viviapi.BLL.Settled
{
    public partial class transferscheme
    {
        public transferscheme()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from transferscheme");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Settled.transferscheme model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into transferscheme(");
            strSql.Append("schemename,minamtlimitofeach,maxamtlimitofeach,dailymaxtimes,dailymaxamt,monthmaxtimes,monthmaxamt,chargerate,chargeleastofeach,chargemostofeach,isdefault)");
            strSql.Append(" values (");
            strSql.Append("@schemename,@minamtlimitofeach,@maxamtlimitofeach,@dailymaxtimes,@dailymaxamt,@monthmaxtimes,@monthmaxamt,@chargerate,@chargeleastofeach,@chargemostofeach,@isdefault)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@schemename", SqlDbType.NVarChar,50),
					new SqlParameter("@minamtlimitofeach", SqlDbType.Decimal,9),
					new SqlParameter("@maxamtlimitofeach", SqlDbType.Decimal,9),
					new SqlParameter("@dailymaxtimes", SqlDbType.Int,4),
					new SqlParameter("@dailymaxamt", SqlDbType.Decimal,9),
					new SqlParameter("@monthmaxtimes", SqlDbType.Int,4),
					new SqlParameter("@monthmaxamt", SqlDbType.Decimal,9),
					new SqlParameter("@chargerate", SqlDbType.Decimal,9),
					new SqlParameter("@chargeleastofeach", SqlDbType.Decimal,9),
					new SqlParameter("@chargemostofeach", SqlDbType.Decimal,9),
					new SqlParameter("@isdefault", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.schemename;
            parameters[1].Value = model.minamtlimitofeach;
            parameters[2].Value = model.maxamtlimitofeach;
            parameters[3].Value = model.dailymaxtimes;
            parameters[4].Value = model.dailymaxamt;
            parameters[5].Value = model.monthmaxtimes;
            parameters[6].Value = model.monthmaxamt;
            parameters[7].Value = model.chargerate;
            parameters[8].Value = model.chargeleastofeach;
            parameters[9].Value = model.chargemostofeach;
            parameters[10].Value = model.isdefault;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Settled.transferscheme model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update transferscheme set ");
            strSql.Append("schemename=@schemename,");
            strSql.Append("minamtlimitofeach=@minamtlimitofeach,");
            strSql.Append("maxamtlimitofeach=@maxamtlimitofeach,");
            strSql.Append("dailymaxtimes=@dailymaxtimes,");
            strSql.Append("dailymaxamt=@dailymaxamt,");
            strSql.Append("monthmaxtimes=@monthmaxtimes,");
            strSql.Append("monthmaxamt=@monthmaxamt,");
            strSql.Append("chargerate=@chargerate,");
            strSql.Append("chargeleastofeach=@chargeleastofeach,");
            strSql.Append("chargemostofeach=@chargemostofeach,");
            strSql.Append("isdefault=@isdefault");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@schemename", SqlDbType.NVarChar,50),
					new SqlParameter("@minamtlimitofeach", SqlDbType.Decimal,9),
					new SqlParameter("@maxamtlimitofeach", SqlDbType.Decimal,9),
					new SqlParameter("@dailymaxtimes", SqlDbType.Int,4),
					new SqlParameter("@dailymaxamt", SqlDbType.Decimal,9),
					new SqlParameter("@monthmaxtimes", SqlDbType.Int,4),
					new SqlParameter("@monthmaxamt", SqlDbType.Decimal,9),
					new SqlParameter("@chargerate", SqlDbType.Decimal,9),
					new SqlParameter("@chargeleastofeach", SqlDbType.Decimal,9),
					new SqlParameter("@chargemostofeach", SqlDbType.Decimal,9),
					new SqlParameter("@isdefault", SqlDbType.TinyInt,1),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.schemename;
            parameters[1].Value = model.minamtlimitofeach;
            parameters[2].Value = model.maxamtlimitofeach;
            parameters[3].Value = model.dailymaxtimes;
            parameters[4].Value = model.dailymaxamt;
            parameters[5].Value = model.monthmaxtimes;
            parameters[6].Value = model.monthmaxamt;
            parameters[7].Value = model.chargerate;
            parameters[8].Value = model.chargeleastofeach;
            parameters[9].Value = model.chargemostofeach;
            parameters[10].Value = model.isdefault;
            parameters[11].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from transferscheme ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from transferscheme ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Settled.transferscheme GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,schemename,minamtlimitofeach,maxamtlimitofeach,dailymaxtimes,dailymaxamt,monthmaxtimes,monthmaxamt,chargerate,chargeleastofeach,chargemostofeach,isdefault from transferscheme ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.Settled.transferscheme model = new Model.Settled.transferscheme();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 取一个用户当天最转
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public decimal GetUserMonthTotalAmt(int userid)
        {
             SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.Int,4),
                    new SqlParameter("@month", SqlDbType.Int,4),
                    new SqlParameter("@userid", SqlDbType.Int,4)
			};
             parameters[0].Value = DateTime.Now.Month;
             parameters[1].Value = DateTime.Now.Year;
             parameters[2].Value = userid;

           object result =   DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_transfer_getusermonthtotalamt", parameters);

           if (result == DBNull.Value)
               return 0M;
           else
               return Convert.ToDecimal(result);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Settled.transferscheme DataRowToModel(DataRow row)
        {
            Model.Settled.transferscheme model = new Model.Settled.transferscheme();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["schemename"] != null)
                {
                    model.schemename = row["schemename"].ToString();
                }
                if (row["minamtlimitofeach"] != null && row["minamtlimitofeach"].ToString() != "")
                {
                    model.minamtlimitofeach = decimal.Parse(row["minamtlimitofeach"].ToString());
                }
                if (row["maxamtlimitofeach"] != null && row["maxamtlimitofeach"].ToString() != "")
                {
                    model.maxamtlimitofeach = decimal.Parse(row["maxamtlimitofeach"].ToString());
                }
                if (row["dailymaxtimes"] != null && row["dailymaxtimes"].ToString() != "")
                {
                    model.dailymaxtimes = int.Parse(row["dailymaxtimes"].ToString());
                }
                if (row["dailymaxamt"] != null && row["dailymaxamt"].ToString() != "")
                {
                    model.dailymaxamt = decimal.Parse(row["dailymaxamt"].ToString());
                }
                if (row["monthmaxtimes"] != null && row["monthmaxtimes"].ToString() != "")
                {
                    model.monthmaxtimes = int.Parse(row["monthmaxtimes"].ToString());
                }
                if (row["monthmaxamt"] != null && row["monthmaxamt"].ToString() != "")
                {
                    model.monthmaxamt = decimal.Parse(row["monthmaxamt"].ToString());
                }
                if (row["chargerate"] != null && row["chargerate"].ToString() != "")
                {
                    model.chargerate = decimal.Parse(row["chargerate"].ToString());
                }
                if (row["chargeleastofeach"] != null && row["chargeleastofeach"].ToString() != "")
                {
                    model.chargeleastofeach = decimal.Parse(row["chargeleastofeach"].ToString());
                }
                if (row["chargemostofeach"] != null && row["chargemostofeach"].ToString() != "")
                {
                    model.chargemostofeach = decimal.Parse(row["chargemostofeach"].ToString());
                }
                if (row["isdefault"] != null && row["isdefault"].ToString() != "")
                {
                    model.isdefault = int.Parse(row["isdefault"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,schemename,minamtlimitofeach,maxamtlimitofeach,dailymaxtimes,dailymaxamt,monthmaxtimes,monthmaxamt,chargerate,chargeleastofeach,chargemostofeach,isdefault ");
            strSql.Append(" FROM transferscheme ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,schemename,minamtlimitofeach,maxamtlimitofeach,dailymaxtimes,dailymaxamt,monthmaxtimes,monthmaxamt,chargerate,chargeleastofeach,chargemostofeach,isdefault ");
            strSql.Append(" FROM transferscheme ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM transferscheme ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from transferscheme T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "transferscheme";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
