using System;
using System.Data;
using System.Data.SqlClient;
using viviapi.Model;
using viviLib;
using DBAccess;
namespace viviapi.BLL
{

    public class SinglePageFactory
    {
        public static int ADD(SinglePage _singlepage)
        {
            SqlParameter idoutparam = DataBase.MakeOutParam("@Sid", SqlDbType.Int, 4);
            SqlParameter[] prams = new SqlParameter[] { idoutparam, DataBase.MakeInParam("@Title", SqlDbType.VarChar, 200, _singlepage.Title), DataBase.MakeInParam("@Content", SqlDbType.NText, 0x1f40, _singlepage.Content), DataBase.MakeInParam("@Addtime", SqlDbType.DateTime, 8, _singlepage.Addtime), DataBase.MakeInParam("@interface1", SqlDbType.VarChar, 200, _singlepage.Interface1), DataBase.MakeInParam("@Interface2", SqlDbType.VarChar, 200, _singlepage.Interface2) };
            if (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_SinglePage_ADD", prams) == 1)
            {
                _singlepage.Sid = (int) idoutparam.Value;
                return _singlepage.Sid;
            }
            return 0;
        }

        public static SinglePage Get(int id)
        {
            SinglePage _singlepage = new SinglePage();
            string slqstr = "SELECT * FROM [SinglePage] WHERE [SID]=" + id;
            SqlDataReader dr = DataBase.ExecuteReader(CommandType.Text, slqstr);
            if (dr.Read())
            {
                _singlepage.Sid = (int) dr["Sid"];
                _singlepage.Title = dr["Title"].ToString();
                _singlepage.Content = dr["Content"].ToString();
                _singlepage.Addtime = (DateTime) dr["addtime"];
                _singlepage.Interface1 = dr["interface1"].ToString();
                _singlepage.Interface2 = dr["interface2"].ToString();
            }
            dr.Close();
            return _singlepage;
        }

        public static bool Update(SinglePage _singlepage)
        {
            SqlParameter[] prams = new SqlParameter[] { DataBase.MakeInParam("@Sid", SqlDbType.Int, 4, _singlepage.Sid), DataBase.MakeInParam("@Title", SqlDbType.VarChar, 200, _singlepage.Title), DataBase.MakeInParam("@Content", SqlDbType.NText, 0x1f40, _singlepage.Content), DataBase.MakeInParam("@Addtime", SqlDbType.DateTime, 8, _singlepage.Addtime), DataBase.MakeInParam("@interface1", SqlDbType.VarChar, 200, _singlepage.Interface1), DataBase.MakeInParam("@Interface2", SqlDbType.VarChar, 200, _singlepage.Interface2) };
            return (DataBase.ExecuteNonQuery(CommandType.StoredProcedure, "UP_SinglePage_Update", prams) == 1);
        }
    }
}

