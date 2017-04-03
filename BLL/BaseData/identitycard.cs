using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using DBAccess;
using viviLib;
using viviLib.ExceptionHandling;
using viviapi.Model;
using viviapi.Model.Channel;


namespace viviapi.BLL.basedata
{
    public class identitycard
    {
        #region  BasicMethod
        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(int ID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) from base_identitycard");
        //    strSql.Append(" where ID=@ID");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,4)
        //    };
        //    parameters[0].Value = ID;

        //    return DbHelperSQL.Exists(strSql.ToString(), parameters);
        //}



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Model.basedata.identitycard GetModel(string BM)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,BM,DQ from base_identitycard ");
            strSql.Append(" where BM=@BM");
            SqlParameter[] parameters = {
					new SqlParameter("@BM", SqlDbType.NVarChar,6)
			};
            parameters[0].Value = BM;

            Model.basedata.identitycard model = new Model.basedata.identitycard();
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
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
        public static Model.basedata.identitycard GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,BM,DQ from base_identitycard ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            Model.basedata.identitycard model = new Model.basedata.identitycard();
            DataSet ds = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
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
        public static Model.basedata.identitycard DataRowToModel(DataRow row)
        {
            Model.basedata.identitycard model = new Model.basedata.identitycard();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["BM"] != null)
                {
                    model.BM = row["BM"].ToString();
                }
                if (row["DQ"] != null)
                {
                    model.DQ = row["DQ"].ToString();
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
            strSql.Append("select ID,BM,DQ ");
            strSql.Append(" FROM base_identitycard ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
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
            strSql.Append(" ID,BM,DQ ");
            strSql.Append(" FROM base_identitycard ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);

            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM base_identitycard ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from base_identitycard T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DataBase.ExecuteDataset(CommandType.Text, strSql.ToString());
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
            parameters[0].Value = "base_identitycard";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        public static bool GetBirthdayAndSex(string identityCard, out string birthday, out string sex)
        {

            birthday = string.Empty;
            sex = string.Empty;

            try
            {
                //处理18位的身份证号码从号码中得到生日和性别代码 
                if (identityCard.Length == 18)
                {
                    birthday = identityCard.Substring(6, 4) + "年" + identityCard.Substring(10, 2) + "月" + identityCard.Substring(12, 2) + "日";
                    sex = identityCard.Substring(14, 3);
                }
                //处理15位的身份证号码从号码中得到生日和性别代码 
                if (identityCard.Length == 15)
                {
                    birthday = "19" + identityCard.Substring(6, 2) + "年" + identityCard.Substring(8, 2) + "月" + identityCard.Substring(10, 2) + "日";
                    sex = identityCard.Substring(12, 3);
                }
                //性别代码为偶数是女性奇数为男性 
                if (int.Parse(sex) % 2 == 0)
                {
                    sex = "女";
                }
                else
                {
                    sex = "男";
                }

                return true;
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);

                return false;
            }
        }


        #endregion  ExtensionMethod
    }
}
