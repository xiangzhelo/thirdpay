using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
//Please add references
using DBAccess;
using viviLib.Data;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Finance
{
	/// <summary>
	/// 数据访问类:PayRate
	/// </summary>
	public partial class PayRate
	{
        internal const string SQL_TABLE = "payrate";
        //money,pay,nopay,Integral
        internal const string SQL_TABLE_FIELDS = @"[id]
      ,[rateType]
      ,[billId]
      ,[billame]
      ,[p100]
      ,[p101]
      ,[p102]
      ,[p103]
      ,[p104]
      ,[p105]
      ,[p106]
      ,[p107]
      ,[p108]
      ,[p109]
      ,[p110]
      ,[p111]
      ,[p112]
      ,[p113]
      ,[p114]
      ,[p115]
      ,[p116]
      ,[p117]
      ,[p118]
      ,[p119]
      ,[p300]
      ,[p200]
      ,[p201]
      ,[p202]
      ,[p203]
      ,[p204]
      ,[p205]
      ,[p207]
      ,[p208]
      ,[p209]
      ,[p210]
      ,[p206]";
		public PayRate()
		{}
		#region  BasicMethod


        public int Insert(viviapi.Model.Finance.PayRate model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@rateType", SqlDbType.TinyInt,1),
					new SqlParameter("@billId", SqlDbType.Int,4),
					new SqlParameter("@billame", SqlDbType.VarChar,50),
					new SqlParameter("@p100", SqlDbType.Decimal,9),
					new SqlParameter("@p101", SqlDbType.Decimal,9),
					new SqlParameter("@p102", SqlDbType.Decimal,9),
					new SqlParameter("@p103", SqlDbType.Decimal,9),
					new SqlParameter("@p104", SqlDbType.Decimal,9),
					new SqlParameter("@p105", SqlDbType.Decimal,9),
					new SqlParameter("@p106", SqlDbType.Decimal,9),
					new SqlParameter("@p107", SqlDbType.Decimal,9),
					new SqlParameter("@p108", SqlDbType.Decimal,9),
					new SqlParameter("@p109", SqlDbType.Decimal,9),
					new SqlParameter("@p110", SqlDbType.Decimal,9),
					new SqlParameter("@p111", SqlDbType.Decimal,9),
					new SqlParameter("@p112", SqlDbType.Decimal,9),
					new SqlParameter("@p113", SqlDbType.Decimal,9),
					new SqlParameter("@p114", SqlDbType.Decimal,9),
					new SqlParameter("@p115", SqlDbType.Decimal,9),
					new SqlParameter("@p116", SqlDbType.Decimal,9),
					new SqlParameter("@p117", SqlDbType.Decimal,9),
					new SqlParameter("@p118", SqlDbType.Decimal,9),
					new SqlParameter("@p119", SqlDbType.Decimal,9),
					new SqlParameter("@p300", SqlDbType.Decimal,9),
					new SqlParameter("@p200", SqlDbType.Decimal,9),
					new SqlParameter("@p201", SqlDbType.Decimal,9),
					new SqlParameter("@p202", SqlDbType.Decimal,9),
					new SqlParameter("@p203", SqlDbType.Decimal,9),
					new SqlParameter("@p204", SqlDbType.Decimal,9),
					new SqlParameter("@p205", SqlDbType.Decimal,9),
					new SqlParameter("@p207", SqlDbType.Decimal,9),
					new SqlParameter("@p208", SqlDbType.Decimal,9),
					new SqlParameter("@p209", SqlDbType.Decimal,9),
					new SqlParameter("@p210", SqlDbType.Decimal,9),
					new SqlParameter("@p206", SqlDbType.Decimal,9)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.rateType;
            parameters[2].Value = model.billId;
            parameters[3].Value = model.billame;
            parameters[4].Value = model.p100;
            parameters[5].Value = model.p101;
            parameters[6].Value = model.p102;
            parameters[7].Value = model.p103;
            parameters[8].Value = model.p104;
            parameters[9].Value = model.p105;
            parameters[10].Value = model.p106;
            parameters[11].Value = model.p107;
            parameters[12].Value = model.p108;
            parameters[13].Value = model.p109;
            parameters[14].Value = model.p110;
            parameters[15].Value = model.p111;
            parameters[16].Value = model.p112;
            parameters[17].Value = model.p113;
            parameters[18].Value = model.p114;
            parameters[19].Value = model.p115;
            parameters[20].Value = model.p116;
            parameters[21].Value = model.p117;
            parameters[22].Value = model.p118;
            parameters[23].Value = model.p119;
            parameters[24].Value = model.p300;
            parameters[25].Value = model.p200;
            parameters[26].Value = model.p201;
            parameters[27].Value = model.p202;
            parameters[28].Value = model.p203;
            parameters[29].Value = model.p204;
            parameters[30].Value = model.p205;
            parameters[31].Value = model.p207;
            parameters[32].Value = model.p208;
            parameters[33].Value = model.p209;
            parameters[34].Value = model.p210;
            parameters[35].Value = model.p206;

            object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_payrate_insert", parameters);
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
		/// 增加一条数据
		/// </summary>
		public int Add(viviapi.Model.Finance.PayRate model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PayRate(");
			strSql.Append("rateType,billId,billame,p100,p101,p102,p103,p104,p105,p106,p107,p108,p109,p110,p111,p112,p113,p114,p115,p116,p117,p118,p119,p300,p200,p201,p202,p203,p204,p205,p207,p208,p209,p210,p206)");
			strSql.Append(" values (");
			strSql.Append("@rateType,@billId,@billame,@p100,@p101,@p102,@p103,@p104,@p105,@p106,@p107,@p108,@p109,@p110,@p111,@p112,@p113,@p114,@p115,@p116,@p117,@p118,@p119,@p300,@p200,@p201,@p202,@p203,@p204,@p205,@p207,@p208,@p209,@p210,@p206)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@rateType", SqlDbType.TinyInt,1),
					new SqlParameter("@billId", SqlDbType.Int,4),
					new SqlParameter("@billame", SqlDbType.VarChar,50),
					new SqlParameter("@p100", SqlDbType.Decimal,9),
					new SqlParameter("@p101", SqlDbType.Decimal,9),
					new SqlParameter("@p102", SqlDbType.Decimal,9),
					new SqlParameter("@p103", SqlDbType.Decimal,9),
					new SqlParameter("@p104", SqlDbType.Decimal,9),
					new SqlParameter("@p105", SqlDbType.Decimal,9),
					new SqlParameter("@p106", SqlDbType.Decimal,9),
					new SqlParameter("@p107", SqlDbType.Decimal,9),
					new SqlParameter("@p108", SqlDbType.Decimal,9),
					new SqlParameter("@p109", SqlDbType.Decimal,9),
					new SqlParameter("@p110", SqlDbType.Decimal,9),
					new SqlParameter("@p111", SqlDbType.Decimal,9),
					new SqlParameter("@p112", SqlDbType.Decimal,9),
					new SqlParameter("@p113", SqlDbType.Decimal,9),
					new SqlParameter("@p114", SqlDbType.Decimal,9),
					new SqlParameter("@p115", SqlDbType.Decimal,9),
					new SqlParameter("@p116", SqlDbType.Decimal,9),
					new SqlParameter("@p117", SqlDbType.Decimal,9),
					new SqlParameter("@p118", SqlDbType.Decimal,9),
					new SqlParameter("@p119", SqlDbType.Decimal,9),
					new SqlParameter("@p300", SqlDbType.Decimal,9),
					new SqlParameter("@p200", SqlDbType.Decimal,9),
					new SqlParameter("@p201", SqlDbType.Decimal,9),
					new SqlParameter("@p202", SqlDbType.Decimal,9),
					new SqlParameter("@p203", SqlDbType.Decimal,9),
					new SqlParameter("@p204", SqlDbType.Decimal,9),
					new SqlParameter("@p205", SqlDbType.Decimal,9),
					new SqlParameter("@p207", SqlDbType.Decimal,9),
					new SqlParameter("@p208", SqlDbType.Decimal,9),
					new SqlParameter("@p209", SqlDbType.Decimal,9),
					new SqlParameter("@p210", SqlDbType.Decimal,9),
					new SqlParameter("@p206", SqlDbType.Decimal,9)};
			parameters[0].Value = model.rateType;
			parameters[1].Value = model.billId;
			parameters[2].Value = model.billame;
			parameters[3].Value = model.p100;
			parameters[4].Value = model.p101;
			parameters[5].Value = model.p102;
			parameters[6].Value = model.p103;
			parameters[7].Value = model.p104;
			parameters[8].Value = model.p105;
			parameters[9].Value = model.p106;
			parameters[10].Value = model.p107;
			parameters[11].Value = model.p108;
			parameters[12].Value = model.p109;
			parameters[13].Value = model.p110;
			parameters[14].Value = model.p111;
			parameters[15].Value = model.p112;
			parameters[16].Value = model.p113;
			parameters[17].Value = model.p114;
			parameters[18].Value = model.p115;
			parameters[19].Value = model.p116;
			parameters[20].Value = model.p117;
			parameters[21].Value = model.p118;
			parameters[22].Value = model.p119;
			parameters[23].Value = model.p300;
			parameters[24].Value = model.p200;
			parameters[25].Value = model.p201;
			parameters[26].Value = model.p202;
			parameters[27].Value = model.p203;
			parameters[28].Value = model.p204;
			parameters[29].Value = model.p205;
			parameters[30].Value = model.p207;
			parameters[31].Value = model.p208;
			parameters[32].Value = model.p209;
			parameters[33].Value = model.p210;
			parameters[34].Value = model.p206;

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
		public bool Update(viviapi.Model.Finance.PayRate model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PayRate set ");
			strSql.Append("rateType=@rateType,");
			strSql.Append("billId=@billId,");
			strSql.Append("billame=@billame,");
			strSql.Append("p100=@p100,");
			strSql.Append("p101=@p101,");
			strSql.Append("p102=@p102,");
			strSql.Append("p103=@p103,");
			strSql.Append("p104=@p104,");
			strSql.Append("p105=@p105,");
			strSql.Append("p106=@p106,");
			strSql.Append("p107=@p107,");
			strSql.Append("p108=@p108,");
			strSql.Append("p109=@p109,");
			strSql.Append("p110=@p110,");
			strSql.Append("p111=@p111,");
			strSql.Append("p112=@p112,");
			strSql.Append("p113=@p113,");
			strSql.Append("p114=@p114,");
			strSql.Append("p115=@p115,");
			strSql.Append("p116=@p116,");
			strSql.Append("p117=@p117,");
			strSql.Append("p118=@p118,");
			strSql.Append("p119=@p119,");
			strSql.Append("p300=@p300,");
			strSql.Append("p200=@p200,");
			strSql.Append("p201=@p201,");
			strSql.Append("p202=@p202,");
			strSql.Append("p203=@p203,");
			strSql.Append("p204=@p204,");
			strSql.Append("p205=@p205,");
			strSql.Append("p207=@p207,");
			strSql.Append("p208=@p208,");
			strSql.Append("p209=@p209,");
			strSql.Append("p210=@p210,");
			strSql.Append("p206=@p206");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@rateType", SqlDbType.TinyInt,1),
					new SqlParameter("@billId", SqlDbType.Int,4),
					new SqlParameter("@billame", SqlDbType.VarChar,50),
					new SqlParameter("@p100", SqlDbType.Decimal,9),
					new SqlParameter("@p101", SqlDbType.Decimal,9),
					new SqlParameter("@p102", SqlDbType.Decimal,9),
					new SqlParameter("@p103", SqlDbType.Decimal,9),
					new SqlParameter("@p104", SqlDbType.Decimal,9),
					new SqlParameter("@p105", SqlDbType.Decimal,9),
					new SqlParameter("@p106", SqlDbType.Decimal,9),
					new SqlParameter("@p107", SqlDbType.Decimal,9),
					new SqlParameter("@p108", SqlDbType.Decimal,9),
					new SqlParameter("@p109", SqlDbType.Decimal,9),
					new SqlParameter("@p110", SqlDbType.Decimal,9),
					new SqlParameter("@p111", SqlDbType.Decimal,9),
					new SqlParameter("@p112", SqlDbType.Decimal,9),
					new SqlParameter("@p113", SqlDbType.Decimal,9),
					new SqlParameter("@p114", SqlDbType.Decimal,9),
					new SqlParameter("@p115", SqlDbType.Decimal,9),
					new SqlParameter("@p116", SqlDbType.Decimal,9),
					new SqlParameter("@p117", SqlDbType.Decimal,9),
					new SqlParameter("@p118", SqlDbType.Decimal,9),
					new SqlParameter("@p119", SqlDbType.Decimal,9),
					new SqlParameter("@p300", SqlDbType.Decimal,9),
					new SqlParameter("@p200", SqlDbType.Decimal,9),
					new SqlParameter("@p201", SqlDbType.Decimal,9),
					new SqlParameter("@p202", SqlDbType.Decimal,9),
					new SqlParameter("@p203", SqlDbType.Decimal,9),
					new SqlParameter("@p204", SqlDbType.Decimal,9),
					new SqlParameter("@p205", SqlDbType.Decimal,9),
					new SqlParameter("@p207", SqlDbType.Decimal,9),
					new SqlParameter("@p208", SqlDbType.Decimal,9),
					new SqlParameter("@p209", SqlDbType.Decimal,9),
					new SqlParameter("@p210", SqlDbType.Decimal,9),
					new SqlParameter("@p206", SqlDbType.Decimal,9),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.rateType;
			parameters[1].Value = model.billId;
			parameters[2].Value = model.billame;
			parameters[3].Value = model.p100;
			parameters[4].Value = model.p101;
			parameters[5].Value = model.p102;
			parameters[6].Value = model.p103;
			parameters[7].Value = model.p104;
			parameters[8].Value = model.p105;
			parameters[9].Value = model.p106;
			parameters[10].Value = model.p107;
			parameters[11].Value = model.p108;
			parameters[12].Value = model.p109;
			parameters[13].Value = model.p110;
			parameters[14].Value = model.p111;
			parameters[15].Value = model.p112;
			parameters[16].Value = model.p113;
			parameters[17].Value = model.p114;
			parameters[18].Value = model.p115;
			parameters[19].Value = model.p116;
			parameters[20].Value = model.p117;
			parameters[21].Value = model.p118;
			parameters[22].Value = model.p119;
			parameters[23].Value = model.p300;
			parameters[24].Value = model.p200;
			parameters[25].Value = model.p201;
			parameters[26].Value = model.p202;
			parameters[27].Value = model.p203;
			parameters[28].Value = model.p204;
			parameters[29].Value = model.p205;
			parameters[30].Value = model.p207;
			parameters[31].Value = model.p208;
			parameters[32].Value = model.p209;
			parameters[33].Value = model.p210;
			parameters[34].Value = model.p206;
			parameters[35].Value = model.id;

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
			strSql.Append("delete from PayRate ");
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
			strSql.Append("delete from PayRate ");
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
		public viviapi.Model.Finance.PayRate GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,rateType,billId,billame,p100,p101,p102,p103,p104,p105,p106,p107,p108,p109,p110,p111,p112,p113,p114,p115,p116,p117,p118,p119,p300,p200,p201,p202,p203,p204,p205,p207,p208,p209,p210,p206 from PayRate ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			viviapi.Model.Finance.PayRate model=new viviapi.Model.Finance.PayRate();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public viviapi.Model.Finance.PayRate GetModel(byte rateType, int billId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,rateType,billId,billame,p100,p101,p102,p103,p104,p105,p106,p107,p108,p109,p110,p111,p112,p113,p114,p115,p116,p117,p118,p119,p300,p200,p201,p202,p203,p204,p205,p207,p208,p209,p210,p206 from PayRate ");
            strSql.Append(" where rateType=@rateType and billId=@billId");
            SqlParameter[] parameters = {
					new SqlParameter("@rateType", SqlDbType.TinyInt,1)
                    ,new SqlParameter("@billId", SqlDbType.Int,4)
			};
            parameters[0].Value = rateType;
            parameters[1].Value = billId;

            viviapi.Model.Finance.PayRate model = new viviapi.Model.Finance.PayRate();
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
        /// 得到一个对象实体
        /// </summary>
        public viviapi.Model.Finance.PayRate GetModelByUser(int userId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4)
			};
            parameters[0].Value = userId;

            viviapi.Model.Finance.PayRate model = new viviapi.Model.Finance.PayRate();
            DataSet ds = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_payrate_getmodelbyuser", parameters);
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
		/// 得到一个对象实体
		/// </summary>
		public viviapi.Model.Finance.PayRate DataRowToModel(DataRow row)
		{
			viviapi.Model.Finance.PayRate model=new viviapi.Model.Finance.PayRate();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["rateType"]!=null && row["rateType"].ToString()!="")
				{
					model.rateType=int.Parse(row["rateType"].ToString());
				}
				if(row["billId"]!=null && row["billId"].ToString()!="")
				{
					model.billId=int.Parse(row["billId"].ToString());
				}
				if(row["billame"]!=null)
				{
					model.billame=row["billame"].ToString();
				}
				if(row["p100"]!=null && row["p100"].ToString()!="")
				{
					model.p100=decimal.Parse(row["p100"].ToString());
				}
				if(row["p101"]!=null && row["p101"].ToString()!="")
				{
					model.p101=decimal.Parse(row["p101"].ToString());
				}
				if(row["p102"]!=null && row["p102"].ToString()!="")
				{
					model.p102=decimal.Parse(row["p102"].ToString());
				}
				if(row["p103"]!=null && row["p103"].ToString()!="")
				{
					model.p103=decimal.Parse(row["p103"].ToString());
				}
				if(row["p104"]!=null && row["p104"].ToString()!="")
				{
					model.p104=decimal.Parse(row["p104"].ToString());
				}
				if(row["p105"]!=null && row["p105"].ToString()!="")
				{
					model.p105=decimal.Parse(row["p105"].ToString());
				}
				if(row["p106"]!=null && row["p106"].ToString()!="")
				{
					model.p106=decimal.Parse(row["p106"].ToString());
				}
				if(row["p107"]!=null && row["p107"].ToString()!="")
				{
					model.p107=decimal.Parse(row["p107"].ToString());
				}
				if(row["p108"]!=null && row["p108"].ToString()!="")
				{
					model.p108=decimal.Parse(row["p108"].ToString());
				}
				if(row["p109"]!=null && row["p109"].ToString()!="")
				{
					model.p109=decimal.Parse(row["p109"].ToString());
				}
				if(row["p110"]!=null && row["p110"].ToString()!="")
				{
					model.p110=decimal.Parse(row["p110"].ToString());
				}
				if(row["p111"]!=null && row["p111"].ToString()!="")
				{
					model.p111=decimal.Parse(row["p111"].ToString());
				}
				if(row["p112"]!=null && row["p112"].ToString()!="")
				{
					model.p112=decimal.Parse(row["p112"].ToString());
				}
				if(row["p113"]!=null && row["p113"].ToString()!="")
				{
					model.p113=decimal.Parse(row["p113"].ToString());
				}
				if(row["p114"]!=null && row["p114"].ToString()!="")
				{
					model.p114=decimal.Parse(row["p114"].ToString());
				}
				if(row["p115"]!=null && row["p115"].ToString()!="")
				{
					model.p115=decimal.Parse(row["p115"].ToString());
				}
				if(row["p116"]!=null && row["p116"].ToString()!="")
				{
					model.p116=decimal.Parse(row["p116"].ToString());
				}
				if(row["p117"]!=null && row["p117"].ToString()!="")
				{
					model.p117=decimal.Parse(row["p117"].ToString());
				}
				if(row["p118"]!=null && row["p118"].ToString()!="")
				{
					model.p118=decimal.Parse(row["p118"].ToString());
				}
				if(row["p119"]!=null && row["p119"].ToString()!="")
				{
					model.p119=decimal.Parse(row["p119"].ToString());
				}
				if(row["p300"]!=null && row["p300"].ToString()!="")
				{
					model.p300=decimal.Parse(row["p300"].ToString());
				}
				if(row["p200"]!=null && row["p200"].ToString()!="")
				{
					model.p200=decimal.Parse(row["p200"].ToString());
				}
				if(row["p201"]!=null && row["p201"].ToString()!="")
				{
					model.p201=decimal.Parse(row["p201"].ToString());
				}
				if(row["p202"]!=null && row["p202"].ToString()!="")
				{
					model.p202=decimal.Parse(row["p202"].ToString());
				}
				if(row["p203"]!=null && row["p203"].ToString()!="")
				{
					model.p203=decimal.Parse(row["p203"].ToString());
				}
				if(row["p204"]!=null && row["p204"].ToString()!="")
				{
					model.p204=decimal.Parse(row["p204"].ToString());
				}
				if(row["p205"]!=null && row["p205"].ToString()!="")
				{
					model.p205=decimal.Parse(row["p205"].ToString());
				}
				if(row["p207"]!=null && row["p207"].ToString()!="")
				{
					model.p207=decimal.Parse(row["p207"].ToString());
				}
				if(row["p208"]!=null && row["p208"].ToString()!="")
				{
					model.p208=decimal.Parse(row["p208"].ToString());
				}
				if(row["p209"]!=null && row["p209"].ToString()!="")
				{
					model.p209=decimal.Parse(row["p209"].ToString());
				}
				if(row["p210"]!=null && row["p210"].ToString()!="")
				{
					model.p210=decimal.Parse(row["p210"].ToString());
				}
				if(row["p206"]!=null && row["p206"].ToString()!="")
				{
					model.p206=decimal.Parse(row["p206"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,rateType,billId,billame,p100,p101,p102,p103,p104,p105,p106,p107,p108,p109,p110,p111,p112,p113,p114,p115,p116,p117,p118,p119,p300,p200,p201,p202,p203,p204,p205,p207,p208,p209,p210,p206 ");
			strSql.Append(" FROM PayRate ");
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
			strSql.Append(" id,rateType,billId,billame,p100,p101,p102,p103,p104,p105,p106,p107,p108,p109,p110,p111,p112,p113,p114,p115,p116,p117,p118,p119,p300,p200,p201,p202,p203,p204,p205,p207,p208,p209,p210,p206 ");
			strSql.Append(" FROM PayRate ");
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
			strSql.Append("select count(1) FROM PayRate ");
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
			strSql.Append(")AS Row, T.*  from PayRate T ");
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
			parameters[0].Value = "PayRate";
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

        #region GetUserPayRate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="payType"></param>
        /// <returns></returns>
        public decimal GetUserPayRate(int userId, int payType)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int,4),
                    new SqlParameter("@typeId", SqlDbType.Int,4),
                    new SqlParameter("@rate", SqlDbType.Decimal,9)};
            parameters[0].Value = userId;
            parameters[1].Value = payType;
            parameters[2].Direction = ParameterDirection.Output;

            parameters[2].Precision = 18;
            parameters[2].Scale = 4;

             DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_payrate_getuser_paytype_payrate",
                parameters);

             return Convert.ToDecimal(parameters[2].Value);
        }
        #endregion

        #region GetUserPayRate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="payType"></param>
        /// <returns></returns>
        public decimal GetSupplierPayRate(int supplierId, int payType)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@supplierId", SqlDbType.Int,4),
                    new SqlParameter("@typeId", SqlDbType.Int,4),
                    new SqlParameter("@Rate", SqlDbType.Decimal,9)};
            parameters[0].Value = supplierId;
            parameters[1].Value = payType;
            parameters[2].Direction = ParameterDirection.Output;

            object obj = DataBase.ExecuteScalar(CommandType.StoredProcedure, "proc_payrate_getsupplier_rate",
                parameters);

            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(parameters[2].Value);
            }
        }
        #endregion

        #endregion  ExtensionMethod

        #region PageSearch
        /// <summary>
        ///     根据搜索条件返回指定分页的商户信息。
        /// </summary>
        /// <param name="searchParams">搜索条件数组。</param>
        /// <param name="pageSize">分页大小。</param>
        /// <param name="page">页码。</param>
        /// <param name="orderby">排序方式。</param>
        /// <returns>分页数据。</returns>
        public DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            var ds = new DataSet();
            string tables = SQL_TABLE;
            string key = "[id]";
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = "id desc";
            }

            var paramList = new List<SqlParameter>();
            string where = BuilderWhere(searchParams, paramList);

            string sql = SqlHelper.GetCountSQL(tables, where, string.Empty) + "\r\n" +
                         SqlHelper.GetPageSelectSQL(SQL_TABLE_FIELDS, tables, where, orderby, key, pageSize, page,
                             false);


            ds = DataBase.ExecuteDataset(CommandType.Text, sql, paramList.ToArray());
            return ds;
        }

        /// <summary>
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private static string BuilderWhere(List<SearchParam> param, List<SqlParameter> paramList)
        {
            var builder = new StringBuilder(" 1 = 1");

            if ((param != null) && (param.Count > 0))
            {
                for (int i = 0; i < param.Count; i++)
                {
                    SqlParameter parameter;
                    SearchParam iparam = param[i];
                    switch (iparam.ParamKey.Trim().ToLower())
                    {
                        case "billid":
                            builder.Append(" AND [billId] = @billId");
                            parameter = new SqlParameter("@billId", SqlDbType.Int);
                            parameter.Value = (int)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "ratetype":
                            builder.Append(" AND [rateType] = @rateType");
                            parameter = new SqlParameter("@rateType", SqlDbType.TinyInt);
                            parameter.Value = (byte)iparam.ParamValue;
                            paramList.Add(parameter);
                            break;
                        case "billname":
                            builder.Append(" AND [billame] like @billname");
                            parameter = new SqlParameter("@billname", SqlDbType.VarChar, 20);
                            parameter.Value = "%" + SqlHelper.CleanString((string)iparam.ParamValue, 20) + "%";
                            paramList.Add(parameter);
                            break;
                    }
                }
            }
            return builder.ToString();
        }
        #endregion
    }
}

