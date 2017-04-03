using viviLib;
using System;
using System.Data;
using System.Data.SqlClient;
using viviapi.Model;
using DBAccess;
namespace viviapi.BLL
{
    public class TempLabelFactory
    {
        public static int ADD(TempLabel _templabel)
        {
            SqlParameter idoutparam = DataBase.MakeOutParam("@Id", SqlDbType.Int, 4);
            SqlParameter[] prams = new SqlParameter[] { idoutparam, DataBase.MakeInParam("@Title", SqlDbType.VarChar, 200, _templabel.Title), DataBase.MakeInParam("@Content", SqlDbType.NText, 500, _templabel.Content), DataBase.MakeInParam("@info", SqlDbType.VarChar, 200, _templabel.Info), DataBase.MakeInParam("@TemplateId", SqlDbType.VarChar, 20, _templabel.TemplateId), DataBase.MakeInParam("@sort", SqlDbType.Int, 4, _templabel.Sort), DataBase.MakeInParam("@source", SqlDbType.VarChar, 200, _templabel.Source) };
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_TempLabel_ADD", prams) == 1)
            {
                _templabel.ID = (int) idoutparam.Value;
                return _templabel.ID;
            }
            return 0;
        }

        public static TempLabel Get(int id)
        {
            TempLabel _templabel = new TempLabel();
            string slqstr = "SELECT * FROM [TempLabel] WHERE [ID]=" + id;
            SqlDataReader dr = DataBase.ExecuteReader(CommandType.Text, slqstr);
            if (dr.Read())
            {
                _templabel.ID = (int) dr["id"];
                _templabel.Title = dr["Title"].ToString();
                _templabel.Content = dr["Content"].ToString();
                _templabel.Info = dr["Info"].ToString();
                _templabel.TemplateId = dr["TemplateId"].ToString();
                _templabel.Sort = int.Parse(dr["Sort"].ToString());
                _templabel.Source = dr["Source"].ToString();
            }
            dr.Close();
            return _templabel;
        }

        public static bool Update(TempLabel _templabel)
        {
            SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@id", SqlDbType.Int, 4, _templabel.ID), DataBase.MakeInParam("@Title", SqlDbType.VarChar, 200, _templabel.Title), DataBase.MakeInParam("@Content", SqlDbType.NText, 500, _templabel.Content), DataBase.MakeInParam("@info", SqlDbType.VarChar, 8, _templabel.Info), DataBase.MakeInParam("@TemplateId", SqlDbType.VarChar, 200, _templabel.TemplateId), DataBase.MakeInParam("@sort", SqlDbType.Int, 4, _templabel.Sort), DataBase.MakeInParam("@source", SqlDbType.VarChar, 200, _templabel.Source) };
            return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_TempLabel_Update", prams) == 1);
        }
    }
}

