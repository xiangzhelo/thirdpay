using System.Data;
using System.Web;
using viviapi.BLL.Channel;
using viviapi.Model.Channel;

namespace viviAPI.WebUI7uka.usermodule.WS.Client
{   
    /// <summary>
    /// 
    /// </summary>
    public class GetCardTypes : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string msg = string.Empty;

            try
            {
                string userid = viviLib.Web.WebBase.GetFormString("userid", string.Empty);
                if (!string.IsNullOrEmpty(userid))
                {
                    int UserId = 0;

                    if (int.TryParse(userid, out UserId))
                    {
                        DataTable dtcardtypes = new DataTable();
                        dtcardtypes.Columns.Add("typeid", typeof(int));
                        dtcardtypes.Columns.Add("typename", typeof(string));
                        dtcardtypes.Columns.Add("facevalues", typeof(string));


                        DataTable data = viviapi.BLL.Channel.ChannelType.GetCacheList();
                        DataTable facevalue = viviapi.BLL.Channel.Channel.GetCardChanels(UserId, 0, 0, 1);

                        foreach (DataRow dr in data.Rows)
                        {
                            int typeId = int.Parse(dr["typeId"].ToString());
                            if (typeId < 103 || typeId == 300 || typeId == 114)
                                continue;

                            bool isuserOpen = true;
                            bool issysOpen = false;

                            //用户关闭
                            ChannelTypeUserInfo setting = viviapi.BLL.Channel.ChannelTypeUsers.GetCacheModel(UserId, typeId);
                            if (setting != null)
                            {
                                if (setting.userIsOpen.HasValue)
                                {
                                    isuserOpen = setting.userIsOpen.Value;
                                    if (!isuserOpen)
                                    {
                                        continue;
                                    }
                                }
                            }

                            ChannelTypeInfo typeInfo = ChannelType.GetCacheModel(typeId);
                            switch (typeInfo.isOpen)
                            {
                                case OpenEnum.AllClose:
                                    issysOpen = false;
                                    break;
                                case OpenEnum.AllOpen:
                                    issysOpen = true;
                                    break;
                                case OpenEnum.Close:
                                    issysOpen = false;
                                    if (setting != null)
                                    {
                                        if (setting.sysIsOpen.HasValue)
                                            issysOpen = setting.sysIsOpen.Value;
                                    }
                                    break;
                                case OpenEnum.Open:
                                    issysOpen = true;
                                    if (setting != null && setting.sysIsOpen.HasValue)
                                    {
                                        if (setting.sysIsOpen.HasValue)
                                            issysOpen = setting.sysIsOpen.Value;
                                    }
                                    break;
                            }

                            if (!isuserOpen)
                            {
                                continue;
                            }
                            if (!issysOpen)
                            {
                                continue;
                            }

                            DataRow _dr = dtcardtypes.NewRow();
                            _dr["typeid"] = typeId;
                            _dr["typename"] = dr["modetypename"];
                            _dr["facevalues"] = GetFaceValues(facevalue, typeId);
                            dtcardtypes.Rows.Add(_dr);
                        }

                        msg = "success" + Newtonsoft.Json.JsonConvert.SerializeObject(dtcardtypes, Newtonsoft.Json.Formatting.Indented);
                    }
                }
            }
            catch
            {

            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
        }

        public string GetFaceValues(DataTable dt, int otypeid)
        {
            System.Text.StringBuilder facevalues = new System.Text.StringBuilder();

            DataRow[] list = dt.Select("typeid=" + otypeid.ToString(), "faceValue");
            if (list != null)
            {
                foreach (DataRow dr in list)
                {
                    facevalues.AppendFormat("{0}|", dr["faceValue"]);
                }
            }
            return facevalues.ToString();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
