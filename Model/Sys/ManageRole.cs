using System;


namespace viviapi.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ManageRole
    {
        None = 0,
        //新闻管理员
        News = 1,
        //系统管理员
        System = 2,
        //接口管理员
        Interfaces = 4,
        //商户管理
        Merchant = 8,
        //订单管理
        Orders = 16,
        //财务
        Financial = 32,
        //超级管理员
       // SuperAdmin = 64,
        //报表分析员
        Report = 128
    }
}

