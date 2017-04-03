using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using viviapi.BLL.News;
using viviapi.BLL.Supplier;
using viviapi.Model;
using viviapi.WebComponents.Web;

namespace viviAPI.WebAdmin.Console.Cache
{
    public partial class CahceManage : ManagePageBase
    {
        string[] suppList = new string[] { "51", "70", "80", "100", "101", "102", "300", "600", "800", "990", "900" };

        protected void Page_Load(object sender, EventArgs e)
        {
            setPower();
            if (!this.IsPostBack)
            {
                this.CacheCountLabel.Text = base.Cache.Count.ToString();
                LoadCaches();
            }
        }

        #region setPower
        /// <summary>
        /// 
        /// </summary>
        private void setPower()
        {
            bool result = viviapi.BLL.ManageFactory.CheckCurrentPermission(false
, ManageRole.System);

            if (result == false)
            {
                Response.Write("Sorry,No authority!");
                Response.End();
            }
        }
        #endregion

        protected void btnDelAll_Click(object sender, EventArgs e)
        {
            IDictionaryEnumerator enumerator = base.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                base.Cache.Remove(enumerator.Key.ToString());
            }
            this.CacheCountLabel.Text = base.Cache.Count.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        void LoadCaches()
        {
            DataTable caches = new DataTable();
            caches.Columns.Add("cacheType", typeof(string));
            caches.Columns.Add("cacheTypeName", typeof(string));
            caches.Columns.Add("cacheKey", typeof(string));

            #region 支付通道
            //ChannelType 支付通道类别
            string cachekey = viviapi.BLL.Channel.ChannelType.CHANNELTYPE_CACHEKEY;
            DataRow dr = caches.NewRow();
            object obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
            if (obj != null)
            {
                dr["cacheType"] = "ChannelType";
                dr["cacheTypeName"] = "支付通道类别";
                dr["cacheKey"] = cachekey;
                caches.Rows.Add(dr);
            }

            List<int> users = viviapi.BLL.User.Factory.GetUsers("[status] = 2");
            foreach (int user in users)
            {
                cachekey = string.Format(viviapi.BLL.Channel.ChannelTypeUsers.ChannelTypeUsers_CACHEKEY, user);
                obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
                if (obj != null)
                {
                    dr = caches.NewRow();
                    dr["cacheType"] = "Channel_type_user";
                    dr["cacheTypeName"] = "支付通道类别用户设置";
                    dr["cacheKey"] = cachekey;
                    caches.Rows.Add(dr);
                }
            }

            cachekey = viviapi.BLL.Channel.Channel.CHANEL_CACHEKEY;
            dr = caches.NewRow();
            obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
            if (obj != null)
            {
                dr["cacheType"] = "Channel";
                dr["cacheTypeName"] = "支付通道";
                dr["cacheKey"] = cachekey;
                caches.Rows.Add(dr);
            }


            #endregion

            #region 站点缓存
            DataTable webs = viviapi.BLL.WebInfoFactory.GetList(string.Empty);
            foreach (DataRow web in webs.Rows)
            {
                cachekey = string.Format(viviapi.BLL.WebInfoFactory.WEBINFO_DOMAIN_CACHEKEY, web["domain"]);
                obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
                if (obj != null)
                {
                    dr = caches.NewRow();
                    dr["cacheType"] = "webinfo_domain_";
                    dr["cacheTypeName"] = "网站设置信息";
                    dr["cacheKey"] = cachekey;
                    caches.Rows.Add(dr);
                }
            }
            cachekey = string.Format(viviapi.BLL.WebInfoFactory.WEBINFO_DOMAIN_CACHEKEY, HttpContext.Current.Request.Url.Host);
            obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
            if (obj != null)
            {
                dr = caches.NewRow();
                dr["cacheType"] = "webinfo_domain_";
                dr["cacheTypeName"] = "网站设置信息";
                dr["cacheKey"] = cachekey;
                caches.Rows.Add(dr);
            }
            #endregion

            #region 用户缓存
            foreach (int user in users)
            {
                cachekey = string.Format(viviapi.BLL.User.Factory.USER_CACHE_KEY, user);
                obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
                if (obj != null)
                {
                    dr = caches.NewRow();
                    dr["cacheType"] = "users";
                    dr["cacheTypeName"] = "用户信息缓存";
                    dr["cacheKey"] = cachekey;
                    caches.Rows.Add(dr);
                }
            }
            #endregion

            #region 新闻缓存
            cachekey = NewsFactory.NEWS_CACHE_KEY;
            dr = caches.NewRow();
            obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
            if (obj != null)
            {
                dr["cacheType"] = "News";
                dr["cacheTypeName"] = "新闻通知缓存";
                dr["cacheKey"] = cachekey;
                caches.Rows.Add(dr);
            }
            #endregion

            #region 接口商
            //string[] suppList = new string[] {"51","70","80","100","101","102","300","600","800","990" };
            foreach (string supp in suppList)
            {
                cachekey = string.Format(Factory.CacheKey, supp);
                dr = caches.NewRow();
                obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
                if (obj != null)
                {

                    dr["cacheType"] = "SUPPLIER";
                    dr["cacheTypeName"] = "接口商";
                    dr["cacheKey"] = cachekey;
                    caches.Rows.Add(dr);
                }
            }
            #endregion

            #region 接口商费率
            //cachekey = viviapi.BLL.SupplierPayRateFactory.CACHE_KEY;
            //dr = caches.NewRow();
            //obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
            //if (obj != null)
            //{
            //    dr["cacheType"] = "SUPPPAYRATE";
            //    dr["cacheTypeName"] = "接口商费率";
            //    dr["cacheKey"] = cachekey;
            //    caches.Rows.Add(dr);
            //}
            #endregion

            #region 配置信息
            cachekey = viviapi.BLL.Sys.SysConfig.SysconfigCachekey;
            dr = caches.NewRow();
            obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
            if (obj != null)
            {
                dr["cacheType"] = "SysConfig";
                dr["cacheTypeName"] = "配置信息";
                dr["cacheKey"] = cachekey;
                caches.Rows.Add(dr);
            }
            #endregion

            #region 问题列表
            cachekey = viviapi.BLL.User.Question.CACHE_KEY;
            dr = caches.NewRow();
            obj = viviapi.Cache.WebCache.GetCacheService().RetrieveObject(cachekey);
            if (obj != null)
            {
                dr["cacheType"] = "Question";
                dr["cacheTypeName"] = "问题列表";
                dr["cacheKey"] = cachekey;
                caches.Rows.Add(dr);
            }
            #endregion

            gv_cache.DataSource = caches;
            gv_cache.DataBind();
            //cachekey = BLL.Channel.ChannelTypeUsers.
        }
        protected void btnBigClass_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in cbl_cacheTypeList.Items)
            {
                if (item.Selected)
                {
                    if (item.Value == "CHANNELS" || item.Value == "CHANNEL_TYPE" || item.Value == "NEWS" || item.Value == "Question" || item.Value == "SUPPPAYRATE" || item.Value == "SYSCONFIG")
                    {
                        viviapi.Cache.WebCache.GetCacheService().RemoveObject(item.Value);
                    }
                    else if (item.Value == "CHANNEL_TYPE_USER_" || item.Value == "USER_" || item.Value == "USERHOST_")
                    {
                        List<int> users = viviapi.BLL.User.Factory.GetUsers("[status] = 2");
                        foreach (int user in users)
                        {
                            string cachekey = string.Format(item.Value + "{0}", user);
                            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cachekey);
                        }
                    }
                    else if (item.Value == "SUPPLIER_")
                    {
                        foreach (string supp in suppList)
                        {
                            string cachekey = string.Format(item.Value + "{0}", supp);
                            viviapi.Cache.WebCache.GetCacheService().RemoveObject(cachekey);
                        }
                    }
                    else if (item.Value == "WEBINFO_")
                    {
                        string cacheKey = string.Format("WEBINFOCONFIG_{0}", viviapi.BLL.WebInfoFactory.CurrentWebInfo);
                        viviapi.Cache.WebCache.GetCacheService().RemoveObject(cacheKey);
                    }
                }
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow dr in gv_cache.Rows)
            {
                CheckBox cb = dr.FindControl("item") as CheckBox;
                if (cb != null && cb.Checked)
                {
                    string cacheKey = this.gv_cache.DataKeys[dr.RowIndex].Value.ToString();                    
                    viviapi.Cache.WebCache.GetCacheService().RemoveObject(cacheKey);
                }
            }
            LoadCaches();
        }
    }
}
