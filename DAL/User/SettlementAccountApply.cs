using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;
using viviapi.Model.User;

namespace viviapi.DAL.User
{
    /// <summary>
    ///     数据访问类:userspaybankapp
    /// </summary>
    public class SettlementAccountApply
    {
        #region  BasicMethod

        /// <summary>
        ///     得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "userspaybankapp");
        }

        /// <summary>
        ///     是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select count(1) from userspaybankapp");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public int Insert(UserPayBankAppInfo model)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int, 4),
                new SqlParameter("@accoutType", SqlDbType.TinyInt, 1),
                new SqlParameter("@pmode", SqlDbType.TinyInt, 1),
                new SqlParameter("@account", SqlDbType.VarChar, 50),
                new SqlParameter("@payeeName", SqlDbType.VarChar, 50),
                new SqlParameter("@payeeBank", SqlDbType.VarChar, 50),
                new SqlParameter("@bankProvince", SqlDbType.VarChar, 50),
                new SqlParameter("@bankCity", SqlDbType.VarChar, 50),
                new SqlParameter("@bankAddress", SqlDbType.VarChar, 100),
                new SqlParameter("@status", SqlDbType.TinyInt, 1),
                new SqlParameter("@AddTime", SqlDbType.DateTime),
                new SqlParameter("@SureTime", SqlDbType.DateTime),
                new SqlParameter("@SureUser", SqlDbType.Int, 4),
                new SqlParameter("@BankCode", SqlDbType.VarChar, 50),
                new SqlParameter("@provinceCode", SqlDbType.VarChar, 50),
                new SqlParameter("@cityCode", SqlDbType.VarChar, 50),
                new SqlParameter("@Reason", SqlDbType.NVarChar, 500)
            };
            parameters[0].Value = model.userid;
            parameters[1].Value = model.accoutType;
            parameters[2].Value = model.pmode;
            parameters[3].Value = model.account;
            parameters[4].Value = model.payeeName;
            parameters[5].Value = model.payeeBank;
            parameters[6].Value = model.bankProvince;
            parameters[7].Value = model.bankCity;
            parameters[8].Value = model.bankAddress;
            parameters[9].Value = (int) model.status;
            parameters[10].Value = model.AddTime;
            parameters[11].Value = model.SureTime;
            parameters[12].Value = model.SureUser;
            parameters[13].Value = model.BankCode;
            parameters[14].Value = model.provinceCode;
            parameters[15].Value = model.cityCode;
            parameters[16].Value = model.Reason;

            object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_userspaybankapp_insert", parameters);
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }

        /// <summary>
        ///     增加一条数据
        /// </summary>
        public int Add(UserPayBankAppInfo model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into userspaybankapp(");
            strSql.Append(
                "userid,accoutType,pmode,account,payeeName,payeeBank,bankProvince,bankCity,bankAddress,status,AddTime,SureTime,SureUser,BankCode,provinceCode,cityCode,Reason)");
            strSql.Append(" values (");
            strSql.Append(
                "@userid,@accoutType,@pmode,@account,@payeeName,@payeeBank,@bankProvince,@bankCity,@bankAddress,@status,@AddTime,@SureTime,@SureUser,@BankCode,@provinceCode,@cityCode,@Reason)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int, 4),
                new SqlParameter("@accoutType", SqlDbType.TinyInt, 1),
                new SqlParameter("@pmode", SqlDbType.TinyInt, 1),
                new SqlParameter("@account", SqlDbType.VarChar, 50),
                new SqlParameter("@payeeName", SqlDbType.VarChar, 50),
                new SqlParameter("@payeeBank", SqlDbType.VarChar, 50),
                new SqlParameter("@bankProvince", SqlDbType.VarChar, 50),
                new SqlParameter("@bankCity", SqlDbType.VarChar, 50),
                new SqlParameter("@bankAddress", SqlDbType.VarChar, 100),
                new SqlParameter("@status", SqlDbType.TinyInt, 1),
                new SqlParameter("@AddTime", SqlDbType.DateTime),
                new SqlParameter("@SureTime", SqlDbType.DateTime),
                new SqlParameter("@SureUser", SqlDbType.Int, 4),
                new SqlParameter("@BankCode", SqlDbType.VarChar, 50),
                new SqlParameter("@provinceCode", SqlDbType.VarChar, 50),
                new SqlParameter("@cityCode", SqlDbType.VarChar, 50),
                new SqlParameter("@Reason", SqlDbType.NVarChar, 500)
            };
            parameters[0].Value = model.userid;
            parameters[1].Value = model.accoutType;
            parameters[2].Value = model.pmode;
            parameters[3].Value = model.account;
            parameters[4].Value = model.payeeName;
            parameters[5].Value = model.payeeBank;
            parameters[6].Value = model.bankProvince;
            parameters[7].Value = model.bankCity;
            parameters[8].Value = model.bankAddress;
            parameters[9].Value = model.status;
            parameters[10].Value = model.AddTime;
            parameters[11].Value = model.SureTime;
            parameters[12].Value = model.SureUser;
            parameters[13].Value = model.BankCode;
            parameters[14].Value = model.provinceCode;
            parameters[15].Value = model.cityCode;
            parameters[16].Value = model.Reason;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }

        /// <summary>
        ///     更新一条数据
        /// </summary>
        public bool Update(UserPayBankAppInfo model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update userspaybankapp set ");
            strSql.Append("userid=@userid,");
            strSql.Append("accoutType=@accoutType,");
            strSql.Append("pmode=@pmode,");
            strSql.Append("account=@account,");
            strSql.Append("payeeName=@payeeName,");
            strSql.Append("payeeBank=@payeeBank,");
            strSql.Append("bankProvince=@bankProvince,");
            strSql.Append("bankCity=@bankCity,");
            strSql.Append("bankAddress=@bankAddress,");
            strSql.Append("status=@status,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("SureTime=@SureTime,");
            strSql.Append("SureUser=@SureUser,");
            strSql.Append("BankCode=@BankCode,");
            strSql.Append("provinceCode=@provinceCode,");
            strSql.Append("cityCode=@cityCode,");
            strSql.Append("Reason=@Reason");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int, 4),
                new SqlParameter("@accoutType", SqlDbType.TinyInt, 1),
                new SqlParameter("@pmode", SqlDbType.TinyInt, 1),
                new SqlParameter("@account", SqlDbType.VarChar, 50),
                new SqlParameter("@payeeName", SqlDbType.VarChar, 50),
                new SqlParameter("@payeeBank", SqlDbType.VarChar, 50),
                new SqlParameter("@bankProvince", SqlDbType.VarChar, 50),
                new SqlParameter("@bankCity", SqlDbType.VarChar, 50),
                new SqlParameter("@bankAddress", SqlDbType.VarChar, 100),
                new SqlParameter("@status", SqlDbType.TinyInt, 1),
                new SqlParameter("@AddTime", SqlDbType.DateTime),
                new SqlParameter("@SureTime", SqlDbType.DateTime),
                new SqlParameter("@SureUser", SqlDbType.Int, 4),
                new SqlParameter("@BankCode", SqlDbType.VarChar, 50),
                new SqlParameter("@provinceCode", SqlDbType.VarChar, 50),
                new SqlParameter("@cityCode", SqlDbType.VarChar, 50),
                new SqlParameter("@Reason", SqlDbType.NVarChar, 500),
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = model.userid;
            parameters[1].Value = model.accoutType;
            parameters[2].Value = model.pmode;
            parameters[3].Value = model.account;
            parameters[4].Value = model.payeeName;
            parameters[5].Value = model.payeeBank;
            parameters[6].Value = model.bankProvince;
            parameters[7].Value = model.bankCity;
            parameters[8].Value = model.bankAddress;
            parameters[9].Value = (int) model.status;
            parameters[10].Value = model.AddTime;
            parameters[11].Value = model.SureTime;
            parameters[12].Value = model.SureUser;
            parameters[13].Value = model.BankCode;
            parameters[14].Value = model.provinceCode;
            parameters[15].Value = model.cityCode;
            parameters[16].Value = model.Reason;
            parameters[17].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from userspaybankapp ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from userspaybankapp ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public UserPayBankAppInfo GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append(
                "select  top 1 id,userid,accoutType,pmode,account,payeeName,payeeBank,bankProvince,bankCity,bankAddress,status,AddTime,SureTime,SureUser,BankCode,provinceCode,cityCode,Reason from userspaybankapp ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;

            var model = new UserPayBankAppInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return null;
        }


        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public UserPayBankAppInfo DataRowToModel(DataRow row)
        {
            var model = new UserPayBankAppInfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["userid"] != null && row["userid"].ToString() != "")
                {
                    model.userid = int.Parse(row["userid"].ToString());
                }
                if (row["accoutType"] != null && row["accoutType"].ToString() != "")
                {
                    model.accoutType = int.Parse(row["accoutType"].ToString());
                }
                if (row["pmode"] != null && row["pmode"].ToString() != "")
                {
                    model.pmode = int.Parse(row["pmode"].ToString());
                }
                if (row["account"] != null)
                {
                    model.account = row["account"].ToString();
                }
                if (row["payeeName"] != null)
                {
                    model.payeeName = row["payeeName"].ToString();
                }
                if (row["payeeBank"] != null)
                {
                    model.payeeBank = row["payeeBank"].ToString();
                }
                if (row["bankProvince"] != null)
                {
                    model.bankProvince = row["bankProvince"].ToString();
                }
                if (row["bankCity"] != null)
                {
                    model.bankCity = row["bankCity"].ToString();
                }
                if (row["bankAddress"] != null)
                {
                    model.bankAddress = row["bankAddress"].ToString();
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = (AcctChangeEnum) int.Parse(row["status"].ToString());
                }
                if (row["AddTime"] != null && row["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(row["AddTime"].ToString());
                }
                if (row["SureTime"] != null && row["SureTime"].ToString() != "")
                {
                    model.SureTime = DateTime.Parse(row["SureTime"].ToString());
                }
                if (row["SureUser"] != null && row["SureUser"].ToString() != "")
                {
                    model.SureUser = int.Parse(row["SureUser"].ToString());
                }
                if (row["BankCode"] != null)
                {
                    model.BankCode = row["BankCode"].ToString();
                }
                if (row["provinceCode"] != null)
                {
                    model.provinceCode = row["provinceCode"].ToString();
                }
                if (row["cityCode"] != null)
                {
                    model.cityCode = row["cityCode"].ToString();
                }
                if (row["Reason"] != null)
                {
                    model.Reason = row["Reason"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        ///     获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(
                "select id,userid,accoutType,pmode,account,payeeName,payeeBank,bankProvince,bankCity,bankAddress,status,AddTime,SureTime,SureUser,BankCode,provinceCode,cityCode,Reason ");
            strSql.Append(" FROM userspaybankapp ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        ///     获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top);
            }
            strSql.Append(
                " id,userid,accoutType,pmode,account,payeeName,payeeBank,bankProvince,bankCity,bankAddress,status,AddTime,SureTime,SureUser,BankCode,provinceCode,cityCode,Reason ");
            strSql.Append(" FROM userspaybankapp ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        ///     获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append("select count(1) FROM userspaybankapp ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj);
        }

        /// <summary>
        ///     分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            var strSql = new StringBuilder();
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
            strSql.Append(")AS Row, T.*  from userspaybankapp T ");
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
			parameters[0].Value = "userspaybankapp";
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