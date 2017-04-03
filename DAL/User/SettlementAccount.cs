using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBAccess;

namespace viviapi.DAL.User
{
    public class SettlementAccount
    {
        /// <summary>
        ///     是否存在该记录
        /// </summary>
        public bool Exists(int userID)
        {
            var strSql = new StringBuilder();
            strSql.Append("select count(1) from userspaybank");
            strSql.Append(" where status=2 and userID=@userID");
            SqlParameter[] parameters =
            {
                new SqlParameter("@userID", SqlDbType.Int, 4)
            };
            parameters[0].Value = userID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        ///     得到一个对象实体
        /// </summary>
        public Model.User.SettlementAccount GetModel(int userid)
        {
            var strSql = new StringBuilder();
            strSql.Append(
                "select  top 1 id,userid,accoutType,pmode,account,payeeName,BankCode,payeeBank,provinceCode,bankProvince,cityCode,bankCity,bankAddress,status,AddTime,updateTime from userspaybank ");
            strSql.Append(" where userid=@userid");
            SqlParameter[] parameters =
            {
                new SqlParameter("@userid", SqlDbType.Int, 4)
            };
            parameters[0].Value = userid;

            var model = new Model.User.SettlementAccount();
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
        public Model.User.SettlementAccount DataRowToModel(DataRow row)
        {
            var model = new Model.User.SettlementAccount();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["userid"] != null && row["userid"].ToString() != "")
                {
                    model.Userid = int.Parse(row["userid"].ToString());
                }
                if (row["accoutType"] != null && row["accoutType"].ToString() != "")
                {
                    model.AccoutType = int.Parse(row["accoutType"].ToString());
                }
                if (row["pmode"] != null && row["pmode"].ToString() != "")
                {
                    model.Pmode = int.Parse(row["pmode"].ToString());
                }
                if (row["account"] != null)
                {
                    model.Account = row["account"].ToString();
                }
                if (row["payeeName"] != null)
                {
                    model.PayeeName = row["payeeName"].ToString();
                }
                if (row["BankCode"] != null)
                {
                    model.BankCode = row["BankCode"].ToString();
                }
                if (row["payeeBank"] != null)
                {
                    model.PayeeBank = row["payeeBank"].ToString();
                }
                if (row["provinceCode"] != null)
                {
                    model.ProvinceCode = row["provinceCode"].ToString();
                }
                if (row["bankProvince"] != null)
                {
                    model.BankProvince = row["bankProvince"].ToString();
                }
                if (row["cityCode"] != null)
                {
                    model.CityCode = row["cityCode"].ToString();
                }
                if (row["bankCity"] != null)
                {
                    model.BankCity = row["bankCity"].ToString();
                }
                if (row["bankAddress"] != null)
                {
                    model.BankAddress = row["bankAddress"].ToString();
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.Status = int.Parse(row["status"].ToString());
                }
                if (row["AddTime"] != null && row["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(row["AddTime"].ToString());
                }
                if (row["updateTime"] != null && row["updateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["updateTime"].ToString());
                }
            }
            return model;
        }
    }
}