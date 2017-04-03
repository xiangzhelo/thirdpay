using System.Linq;
using System.Web;

namespace viviLib.database
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlKey
    {
        public bool ProcessSqlStr(string str)
        {
            bool returnValue = true;
            try
            {
                if (str.Trim() != "")
                {
                    const string sqlStr = "and |exec |insert |select |delete |update |count | * |chr |mid |master |truncate |char |declare ";

                    string[] anySqlStr = sqlStr.Split('|');
                    if (anySqlStr.Any(ss => str.ToLower().IndexOf(ss, System.StringComparison.Ordinal) >= 0))
                    {
                        returnValue = false;
                    }
                }
            }
            catch
            {
                returnValue = false;
            }
            return returnValue;
        }

        public void StartProcessRequest()
        {
            try
            {
                string getkeys = "";
                const string sqlErrorPage = "default.aspx"; //转向的错误提示页面 
                for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
                {
                    getkeys = HttpContext.Current.Request.QueryString.Keys[i];
                    if (!ProcessSqlStr(HttpContext.Current.Request.QueryString[getkeys]))
                    {
                        HttpContext.Current.Response.Redirect(sqlErrorPage);
                        HttpContext.Current.Response.End();
                    }
                }
                for (int i = 0; i < System.Web.HttpContext.Current.Request.Form.Count; i++)
                {
                    getkeys = System.Web.HttpContext.Current.Request.Form.Keys[i];
                    if (getkeys == "__VIEWSTATE") continue;
                    if (!ProcessSqlStr(System.Web.HttpContext.Current.Request.Form[getkeys]))
                    {
                        System.Web.HttpContext.Current.Response.Redirect(sqlErrorPage);
                        System.Web.HttpContext.Current.Response.End();
                    }
                }
            }
            catch
            {
                // 错误处理: 处理用户提交信息! 
            }
        }
    }
}