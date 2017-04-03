using viviLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBAccess;

namespace viviapi.BLL
{
    /// <summary>
    /// 数据访问类:PTypeConf
    /// </summary>
    public partial class PTypeConfFactory
    {
        public PTypeConfFactory()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {            
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            int result =  DataBase.RunProc("PTypeConf_Exists", parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(viviapi.Model.PTypeConf model)
        {            
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@GoodType", SqlDbType.TinyInt,1),
					new SqlParameter("@GM_ID", SqlDbType.Int,4),
					new SqlParameter("@PayType", SqlDbType.Int,4),
					new SqlParameter("@IsUse", SqlDbType.Bit,1)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.GoodType;
            parameters[2].Value = model.GM_ID;
            parameters[3].Value = model.PayType;
            parameters[4].Value = model.IsUse;

            DataBase.RunProc("PTypeConf_ADD", parameters);
            return (int)parameters[0].Value;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public bool Update(viviapi.Model.PTypeConf model)
        {
            object rowsAffected = null;
            SqlParameter[] parameters = {
					new SqlParameter("@GoodType", SqlDbType.TinyInt,1),
					new SqlParameter("@GM_ID", SqlDbType.Int,4),
					new SqlParameter("@PayType", SqlDbType.Int,4),
					new SqlParameter("@IsUse", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.GoodType;
            parameters[1].Value = model.GM_ID;
            parameters[2].Value = model.PayType;
            parameters[3].Value = model.IsUse;

           DataBase.RunProc("PTypeConf_Update", parameters, out rowsAffected);
           if (rowsAffected != null && rowsAffected != DBNull.Value)
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
        public bool Delete(int ID)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
            parameters[0].Value = ID;

            rowsAffected = DataBase.RunProc("PTypeConf_Delete", parameters);
            if (rowsAffected > 0)
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
        public bool DeleteList(string IDlist)
        {
            //参数检查
            string ids = string.Empty;

            foreach (string id in IDlist.Split(','))
            {
                int temp = 0;
                if (int.TryParse(id, out temp))
                {
                    ids += id + ",";
                }
            }
            if (!string.IsNullOrEmpty(ids))
            {
                ids = ids.Substring(0, ids.Length - 1);
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PTypeConf ");
            strSql.Append(" where ID in (" + ids + ")  ");
            int rows = DataBase.ExecuteNonQuery(CommandType.Text, strSql.ToString());
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
        public viviapi.Model.PTypeConf GetModel(int ID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
            parameters[0].Value = ID;

            viviapi.Model.PTypeConf model = new viviapi.Model.PTypeConf();
            DataSet ds = null;
            DataBase.RunProc("PTypeConf_GetModel", parameters, out ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GoodType"].ToString() != "")
                {
                    model.GoodType = int.Parse(ds.Tables[0].Rows[0]["GoodType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GM_ID"].ToString() != "")
                {
                    model.GM_ID = int.Parse(ds.Tables[0].Rows[0]["GM_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayType"].ToString() != "")
                {
                    model.PayType = int.Parse(ds.Tables[0].Rows[0]["PayType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUse"].ToString() != "")
                {
                    model.IsUse = Convert.ToInt32(ds.Tables[0].Rows[0]["IsUse"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ds"></param>
        //public static void UpdateSetting(DataSet ds)
        //{
        //    SqlConnection conn = new SqlConnection(Database.ConnectionString);

        //    //设置select查询命令
        //    SqlCommand selectCMD = new SqlCommand("select [GoodType],[GM_ID],[PayType],[IsUse],[isDisable] PTypeConf ", conn);
        //    //Insert命令
        //    SqlCommand insertCMD = new SqlCommand("UPDATE [PTypeConf] SET [GoodType] = @GoodType,[GM_ID] = @GM_ID,[PayType] = @PayType,[IsUse] = @IsUse,[isDisable] = @isDisable", conn);
        //    //Update命令
        //    SqlCommand updateCMD = new SqlCommand("UPDATE [PTypeConf] SET [GoodType] = @GoodType,[GM_ID] = @GM_ID,[PayType] = @PayType,[IsUse] = @IsUse,[isDisable] = @isDisable WHERE ID=@ID ", conn);
        //    //Delete命令
        //    SqlCommand deleteCMD = new SqlCommand("DELETE [PTypeConf] WHERE ID=@ID", conn);

        //    //给Insert,Update,Delete三个命令添加参数
        //    SqlParameter paraGoodType1, paraGoodType2;//第二个指定参数值的来源,这里的SNo是指DataTable中的列名
        //    paraGoodType1 = new SqlParameter("@GoodType", "GoodType");
        //    paraGoodType2 = new SqlParameter("@GoodType", "GoodType");
        //    paraGoodType1.SourceVersion = DataRowVersion.Current;
        //    //指定SourceVersion确定参数值是列的当前值(Current)，还是原始值(Original)，还是建议值(Proposed)
        //    paraGoodType2.SourceVersion = DataRowVersion.Current;

        //    SqlParameter paraGM_ID1, paraGM_ID2;
        //    paraGM_ID1 = new SqlParameter("@GM_ID", "GM_ID");
        //    paraGM_ID2 = new SqlParameter("@GM_ID", "GM_ID");
        //    paraGM_ID1.SourceVersion = DataRowVersion.Current;
        //    paraGM_ID2.SourceVersion = DataRowVersion.Current;

        //    SqlParameter paraPayType1, paraPayType2;
        //    paraPayType1 = new SqlParameter("@PayType", "PayType");
        //    paraPayType2 = new SqlParameter("@PayType", "PayType");
        //    paraPayType1.SourceVersion = DataRowVersion.Current;
        //    paraPayType2.SourceVersion = DataRowVersion.Current;

        //    SqlParameter paraisDisable1, paraisDisable2;
        //    paraIsUse1 = new SqlParameter("@IsUse", "IsUse");
        //    paraIsUse2 = new SqlParameter("@IsUse", "IsUse");
        //    paraIsUse1.SourceVersion = DataRowVersion.Current;
        //    paraIsUse2.SourceVersion = DataRowVersion.Current;

        //    SqlParameter paraisDisable1, paraisDisable2;
        //    paraisDisable1 = new SqlParameter("@isDisable", "isDisable");
        //    paraisDisable2 = new SqlParameter("@isDisable", "isDisable");
        //    paraisDisable1.SourceVersion = DataRowVersion.Current;
        //    paraisDisable2.SourceVersion = DataRowVersion.Current;

        //    SqlParameter paraid;
        //    paraid = new SqlParameter("@ID", "ID");
        //    paraid.SourceVersion = DataRowVersion.Current;

        //    insertCMD.Parameters.AddRange(new SqlParameter[] { GoodType, GM_ID, PayType, IsUse, isDisable });
        //    updateCMD.Parameters.AddRange(new SqlParameter[] { GoodType, GM_ID, PayType, IsUse, isDisable });
        //    deleteCMD.Parameters.AddRange(new SqlParameter[] { paraid });

        //    DataTable dt = new DataTable();
        //    SqlDataAdapter sda = new SqlDataAdapter(selectCMD);
        //    sda.Fill(dt);
        //    //插入2条数据
        //    dt.Rows.Add(new object[] { 11, "aa11", 31 });
        //    dt.Rows.Add(new object[] { 12, "aa12", 32 });


        //    //先更新第1，2条数据的SName和SAge
        //    dt.Rows[0]["SName"] = "CCC";
        //    dt.Rows[0]["SAge"] = 55;
        //    dt.Rows[1]["SName"] = "DDD";
        //    dt.Rows[1]["SAge"] = 66;

        //    //使用Delete删除第3，4条数据
        //    dt.Rows[2].Delete();
        //    dt.Rows[3].Delete();

        //    SqlCommandBuilder scb = new SqlCommandBuilder(sda);
        //    //执行更新
        //    sda.Update(dt.GetChanges());
        //    //使DataTable保存更新
        //    dt.AcceptChanges();



        //}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int userid,int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,GoodType,GM_ID,PayType,IsUse,isDisable ");
            strSql.Append(" FROM PTypeConf where GM_ID=@GM_ID AND GoodType=@type");
            
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(DataBase.MakeInParam("@GM_ID", SqlDbType.Int, 4, userid));
            list.Add(DataBase.MakeInParam("@type", SqlDbType.Int, 4, type));

            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetBankList(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,GoodType,GM_ID,PayType,IsUse,isDisable ");
            strSql.Append(" FROM PTypeConf where GM_ID=@GM_ID AND (PayType=100 or PayType=101 or PayType =102)");

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(DataBase.MakeInParam("@GM_ID", SqlDbType.Int, 4, userid));

            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetGMCloseList(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,GoodType,GM_ID,PayType,IsUse,isDisable ");
            strSql.Append(" FROM PTypeConf where GM_ID=@GM_ID and GoodType=1 and PayType <> 300 and (IsUse=0 or isDisable = 1)");

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(DataBase.MakeInParam("@GM_ID", SqlDbType.Int, 4, userid));

            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetCardCloseList(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,GoodType,GM_ID,PayType,IsUse,isDisable ");
            strSql.Append(" FROM PTypeConf where GM_ID=@GM_ID and GoodType=2 and PayType <> 300 and (IsUse=0 or isDisable = 1)");

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(DataBase.MakeInParam("@GM_ID", SqlDbType.Int, 4, userid));

            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), list.ToArray());
        }

        ///// <summary>
        ///// 获得前几行数据
        ///// </summary>
        //public DataSet GetList(int Top, string strWhere, string filedOrder)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ");
        //    if (Top > 0)
        //    {
        //        strSql.Append(" top " + Top.ToString());
        //    }
        //    strSql.Append(" ID,GoodType,GM_ID,PayType,IsUse ");
        //    strSql.Append(" FROM PTypeConf ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    strSql.Append(" order by " + filedOrder);
        //    return Database.ExecuteDataset(CommandType.Text, strSql.ToString());
        //}

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
            parameters[0].Value = "PTypeConf";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return Database.RunProc("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public viviapi.Model.PTypeConf GetModelByUser(int UserID, int gdtype)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.Int,4),
                new SqlParameter("@gdType", SqlDbType.Int,4)};

            parameters[0].Value = UserID;
            parameters[1].Value = gdtype;

            viviapi.Model.PTypeConf model = new viviapi.Model.PTypeConf();

            DataSet ds = null;
            DataBase.RunProc("PTypeConf_GetModelByUserID", parameters, out ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int type = 0;
                int isUse = 0;
                int isDisable = 0;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int.TryParse(dr["PayType"].ToString(), out type);
                    int.TryParse(dr["IsUse"].ToString(), out isUse);
                    int.TryParse(dr["isDisable"].ToString(), out isDisable);

                    if (isUse == 0 || isDisable == 1)
                    {
                        if (type == 100)
                        {
                            model.PayAlipay = false;
                        }
                        else if (type == 101)
                        {
                            model.PayTanPay = false;
                        }
                        else if (type == 102)
                        {
                            model.PayBank = false;
                        }
                        else if (type == 103)
                        {
                            model.Pay103 = false;
                        }
                        else if (type == 104)
                        {
                            model.Pay104 = false;
                        }
                        else if (type == 105)
                        {
                            model.Pay105 = false;
                        }
                        else if (type == 106)
                        {
                            model.Pay106 = false;
                        }
                        else if (type == 107)
                        {
                            model.Pay107 = false;
                        }
                        else if (type == 108)
                        {
                            model.Pay108 = false;
                        }
                        else if (type == 109)
                        {
                            model.Pay109 = false;
                        }
                        else if (type == 110)
                        {
                            model.Pay110 = false;
                        }
                        else if (type == 111)
                        {
                            model.Pay111 = false;
                        }
                        else if (type == 112)
                        {
                            model.Pay112 = false;
                        }
                    }
                }
            }
            return model;
        }
    }
}

