using DBAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace viviapi.DAL.Quota
{
    public class Quotapayrate
    {
        public string Getpayratelist(viviapi.Model.Quota.quotapayrate model)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@userid", SqlDbType.Int,4)};
            parameters[0].Value = model.Userid;
            DataSet obj = DataBase.ExecuteDataset(CommandType.StoredProcedure, "proc_quotapayrate_Getpayratelist", parameters);
            //return JsonConvert.SerializeObject(obj);
            if (obj == null)
            {
                return "null";
            }
            else
            {
                return JsonConvert.SerializeObject(obj);
                //return obj.Tables[0].Rows[0].Table;
            }
        }
    }
}
