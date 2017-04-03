using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using viviLib.ExceptionHandling;
using DBAccess;

namespace viviapi.BLL.Tools
{
    public class db
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Backup(string path)
        {
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[] { 
                  DataBase.MakeInParam("@datapath", SqlDbType.VarChar, 200, path) 
            };
                DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "proc_database_backup", commandParameters);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
    }
}
