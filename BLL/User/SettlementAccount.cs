using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Web;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.User;
using viviLib;
using DBAccess;
using viviLib.Data;

namespace viviapi.BLL.User
{
	/// <summary>
	/// 数据访问类:结算账户userspaybank
	/// </summary>
	public partial class SettlementAccount
	{
        private readonly DAL.User.SettlementAccount dal = new DAL.User.SettlementAccount();  

        public SettlementAccount()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		    return DbHelperSQL.GetMaxID("id", "userspaybank"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(int userid)
		{
		    try
		    {
		        return dal.Exists(userid);
		    }
		    catch (Exception)
		    {
		        return false;
		    }
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.User.SettlementAccount model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into userspaybank(");
			strSql.Append("userid,accoutType,pmode,account,payeeName,BankCode,payeeBank,provinceCode,bankProvince,cityCode,bankCity,bankAddress,status,AddTime,updateTime)");
			strSql.Append(" values (");
			strSql.Append("@userid,@accoutType,@pmode,@account,@payeeName,@BankCode,@payeeBank,@provinceCode,@bankProvince,@cityCode,@bankCity,@bankAddress,@status,@AddTime,@updateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@accoutType", SqlDbType.TinyInt,1),
					new SqlParameter("@pmode", SqlDbType.TinyInt,1),
					new SqlParameter("@account", SqlDbType.VarChar,50),
					new SqlParameter("@payeeName", SqlDbType.VarChar,50),
					new SqlParameter("@BankCode", SqlDbType.VarChar,50),
					new SqlParameter("@payeeBank", SqlDbType.VarChar,50),
					new SqlParameter("@provinceCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankProvince", SqlDbType.VarChar,50),
					new SqlParameter("@cityCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankCity", SqlDbType.VarChar,50),
					new SqlParameter("@bankAddress", SqlDbType.VarChar,100),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@updateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.Userid;
			parameters[1].Value = model.AccoutType;
			parameters[2].Value = model.Pmode;
			parameters[3].Value = model.Account;
			parameters[4].Value = model.PayeeName;
			parameters[5].Value = model.BankCode;
			parameters[6].Value = model.PayeeBank;
			parameters[7].Value = model.ProvinceCode;
			parameters[8].Value = model.BankProvince;
			parameters[9].Value = model.CityCode;
			parameters[10].Value = model.BankCity;
			parameters[11].Value = model.BankAddress;
			parameters[12].Value = model.Status;
			parameters[13].Value = model.AddTime;
			parameters[14].Value = model.UpdateTime;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Model.User.SettlementAccount model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update userspaybank set ");
			strSql.Append("userid=@userid,");
			strSql.Append("accoutType=@accoutType,");
			strSql.Append("pmode=@pmode,");
			strSql.Append("account=@account,");
			strSql.Append("payeeName=@payeeName,");
			strSql.Append("BankCode=@BankCode,");
			strSql.Append("payeeBank=@payeeBank,");
			strSql.Append("provinceCode=@provinceCode,");
			strSql.Append("bankProvince=@bankProvince,");
			strSql.Append("cityCode=@cityCode,");
			strSql.Append("bankCity=@bankCity,");
			strSql.Append("bankAddress=@bankAddress,");
			strSql.Append("status=@status,");
			strSql.Append("AddTime=@AddTime,");
			strSql.Append("updateTime=@updateTime");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
					new SqlParameter("@accoutType", SqlDbType.TinyInt,1),
					new SqlParameter("@pmode", SqlDbType.TinyInt,1),
					new SqlParameter("@account", SqlDbType.VarChar,50),
					new SqlParameter("@payeeName", SqlDbType.VarChar,50),
					new SqlParameter("@BankCode", SqlDbType.VarChar,50),
					new SqlParameter("@payeeBank", SqlDbType.VarChar,50),
					new SqlParameter("@provinceCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankProvince", SqlDbType.VarChar,50),
					new SqlParameter("@cityCode", SqlDbType.VarChar,50),
					new SqlParameter("@bankCity", SqlDbType.VarChar,50),
					new SqlParameter("@bankAddress", SqlDbType.VarChar,100),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@updateTime", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.Userid;
			parameters[1].Value = model.AccoutType;
			parameters[2].Value = model.Pmode;
			parameters[3].Value = model.Account;
			parameters[4].Value = model.PayeeName;
			parameters[5].Value = model.BankCode;
			parameters[6].Value = model.PayeeBank;
			parameters[7].Value = model.ProvinceCode;
			parameters[8].Value = model.BankProvince;
			parameters[9].Value = model.CityCode;
			parameters[10].Value = model.BankCity;
			parameters[11].Value = model.BankAddress;
			parameters[12].Value = model.Status;
			parameters[13].Value = model.AddTime;
			parameters[14].Value = model.UpdateTime;
			parameters[15].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from userspaybank ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from userspaybank ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public Model.User.SettlementAccount GetModel(int userid)
		{
            try
            {
                return dal.GetModel(userid);
            }
            catch (Exception)
            {
                return null;
            }
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,userid,accoutType,pmode,account,payeeName,BankCode,payeeBank,provinceCode,bankProvince,cityCode,bankCity,bankAddress,status,AddTime,updateTime ");
			strSql.Append(" FROM userspaybank ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,userid,accoutType,pmode,account,payeeName,BankCode,payeeBank,provinceCode,bankProvince,cityCode,bankCity,bankAddress,status,AddTime,updateTime ");
			strSql.Append(" FROM userspaybank ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM userspaybank ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from userspaybank T ");
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
			parameters[0].Value = "userspaybank";
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

        public static string GetSettleModeName(object obj)
        {
            if (obj == DBNull.Value) return string.Empty;

            string _name = string.Empty;
            switch (Convert.ToInt32(obj))
            {
                case 1:
                    _name = "银行帐户";
                    break;
                case 2:
                    _name = "支付宝";
                    break;
                case 3:
                    _name = "财付通";
                    break;
            }
            return _name;
        }

        public static string GetAccoutTypeName(object obj)
        {
            if (obj == DBNull.Value) return string.Empty;

            string _name = string.Empty;
            switch (Convert.ToInt32(obj))
            {
                case 0:
                    _name = "私";
                    break;
                case 1:
                    _name = "公";
                    break;
            }
            return _name;
        }
	}
}

