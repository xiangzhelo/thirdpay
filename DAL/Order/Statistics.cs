using System;
using System.Data;
using System.Data.SqlClient;
using DBAccess;
using viviLib.ExceptionHandling;

namespace viviapi.DAL.Order
{
    /// <summary>
    /// </summary>
    public class Statistics
    {
        public DataTable Stat(int suppid, DateTime sdt, DateTime edt)
        {
            string sqlText =
                @"select b.typeId,c.modetypename,sum(realvalue) realvalue,sum(supplierAmt) supplierAmt,sum(payAmt) payAmt,sum(profits) profits 
from ordercardamt a with(nolock) 
		left join ordercard b with(nolock) on a.orderid = b.orderid
		left join channeltype c with(nolock) on b.typeId = c.typeId
		left join supplier d with(nolock) on b.supplierID = d.id
where (a.[status] = 2 or a.[status] = 8)
and (b.supplierID = @suppid or @suppid is null)
and a.completetime >= @begindt
and a.completetime < @enddt
group by b.typeId,c.modetypename";

            SqlParameter[] parameters =
            {
                new SqlParameter("@suppid", SqlDbType.Int, 4)
                , new SqlParameter("@begindt", SqlDbType.DateTime, 8)
                , new SqlParameter("@enddt", SqlDbType.DateTime, 8)
            };
            parameters[0].Value = suppid;
            if (suppid == 0)
                parameters[0].Value = DBNull.Value;
            parameters[1].Value = sdt;
            parameters[2].Value = edt;

            return DataBase.ExecuteDataset(CommandType.Text, sqlText, parameters).Tables[0];
        }


        public DataTable StatForBusiness(int manageId, DateTime sdt, DateTime edt)
        {
            string sqlText =
                @"select typeId,modetypename,sum(realvalue) as realvalue,sum(supplierAmt) supplierAmt,sum(commission) commission,sum(payAmt) payAmt
from v_order
where ([status] = 2 or [status] = 8)
and (manageId = @manageId)
and processingtime >= @begindt
and processingtime < @enddt
group by typeId,modetypename";

            SqlParameter[] parameters =
            {
                new SqlParameter("@manageId", SqlDbType.Int, 4)
                , new SqlParameter("@begindt", SqlDbType.DateTime, 8)
                , new SqlParameter("@enddt", SqlDbType.DateTime, 8)
            };
            parameters[0].Value = manageId;
            parameters[1].Value = sdt;
            parameters[2].Value = edt;

            return DataBase.ExecuteDataset(CommandType.Text, sqlText, parameters).Tables[0];
        }

        public DataTable StatForAgent(int agentid, DateTime sdt, DateTime edt)
        {
            string sqlText =
                @"select typeId,modetypename,sum(realvalue) as realvalue,sum(supplierAmt) supplierAmt,sum(promAmt) promAmt,sum(payAmt) payAmt
from v_order
where ([status] = 2 or [status] = 8)
and (agentid = @agentid)
and processingtime >= @begindt
and processingtime < @enddt
group by typeId,modetypename";

            SqlParameter[] parameters =
            {
                new SqlParameter("@agentid", SqlDbType.Int, 4)
                , new SqlParameter("@begindt", SqlDbType.DateTime, 8)
                , new SqlParameter("@enddt", SqlDbType.DateTime, 8)
            };
            parameters[0].Value = agentid;
            parameters[1].Value = sdt;
            parameters[2].Value = edt;

            return DataBase.ExecuteDataset(CommandType.Text, sqlText, parameters).Tables[0];
        }

        public DataSet AgentStat2(DateTime sdt, DateTime edt, int page, int pagesize, string orderby)
        {
            string sqlText = @"
select count(0) as C
	from(
	select agentid
	from v_order with(nolock)
	where agentid > 0 and promAmt > 0 and processingtime >= @sdt and processingtime < @edt 
	group by agentid) A


select D1.agentid,payAmt,promAmt,supplierAmt,realvalue,B.username,B.full_name
from(
	select agentid,payAmt,promAmt,supplierAmt,realvalue,ROW_NUMBER() OVER(ORDER BY D.agentid) AS P_ROW 
	from(
	select agentid,sum(payAmt) payAmt,sum(promAmt) as promAmt,sum(supplierAmt) as supplierAmt,sum(realvalue) as realvalue
	from v_order with(nolock)
	where agentid > 0 and promAmt > 0 and processingtime >= @sdt and processingtime < @edt  
	group by agentid) D 
)D1  left join userbase B with(nolock)  on D1.agentid = B.id
WHERE D1.P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize
order by " + orderby;

            SqlParameter[] parameters =
            {
                new SqlParameter("@sdt", SqlDbType.DateTime, 8)
                , new SqlParameter("@edt", SqlDbType.DateTime, 8)
                , new SqlParameter("@page", SqlDbType.Int, 4)
                , new SqlParameter("@pagesize", SqlDbType.Int, 4)
            };
            parameters[0].Value = sdt;
            parameters[1].Value = edt;
            parameters[2].Value = page;
            parameters[3].Value = pagesize;

            return DataBase.ExecuteDataset(CommandType.Text, sqlText, parameters);
        }


        public DataSet AgentStat3(int agentid, DateTime sdt, DateTime edt, int page, int pagesize, string orderby)
        {
            string sqlText = @"
select count(0) as C
		from orderbankamt a with(nolock)
				left join orderbank b with(nolock) 
							on a.orderid = b.orderid		
		where a.[status] = 2 and b.agentid > 0 and processingtime >= @begintime and processingtime <= @endtime and (b.agentid 
		group by b.agentid


select 
	a.agentid
	,userName
	,tradeAmt
	,promAmt
	,profits
	,lowercount = (select count(0) from PromotionUser where PID = a.agentid)
	,ROW_NUMBER() OVER(ORDER BY a.agentid) AS P_ROW
from 
	(
		select b.agentid,sum(realvalue) as tradeAmt,sum(promAmt) as promAmt,sum(profits) as profits
		from orderbankamt a with(nolock)
				left join orderbank b with(nolock) 
							on a.orderid = b.orderid		
		where a.[status] = 2 and b.agentid > 0 and processingtime >= @begintime and processingtime <= @endtime and (b.agentid = @userid or @userid = 0)
		group by b.agentid
	) a 
	left join userbase b with(nolock) 
					on a.agentid = b.id
where a.P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize";
            //order by " + orderby;

            SqlParameter[] parameters =
            {
                new SqlParameter("@begintime", SqlDbType.DateTime, 8)
                , new SqlParameter("@endtime", SqlDbType.DateTime, 8)
                , new SqlParameter("@page", SqlDbType.Int, 4)
                , new SqlParameter("@pagesize", SqlDbType.Int, 4)
                , new SqlParameter("@userid", SqlDbType.Int, 4)
            };
            parameters[0].Value = sdt;
            parameters[1].Value = edt;
            parameters[2].Value = page;
            parameters[3].Value = pagesize;
            parameters[4].Value = agentid;

            return DataBase.ExecuteDataset(CommandType.Text, sqlText, parameters);
        }

        public decimal GetAgentTotalAmt(int agentid, DateTime sdt, DateTime edt)
        {
            decimal result = 0M;
            try
            {
                string sqlText = @"declare @amt decimal(18,4)
select @amt = sum(realvalue) from v_orderbank 
where agentid=@agentid and status = 2 and processingtime > @begintime and processingtime < @endtime

select @amt = isnull(@amt,0)+ isnull(sum(realvalue),0) from v_order where agentid=@agentid and status = 2
 and processingtime > @begintime and processingtime < @endtime

select isnull(@amt,0)";

                SqlParameter[] parameters = {    
                                            new SqlParameter("@agentid", SqlDbType.Int, 4)
                                           ,new SqlParameter("@begintime", SqlDbType.DateTime,8) 
                                           ,new SqlParameter("@endtime", SqlDbType.DateTime,8) 
                                        };
                parameters[0].Value = agentid;
                parameters[1].Value = sdt;
                parameters[2].Value = edt;

                object amt = DataBase.ExecuteScalar(CommandType.Text, sqlText, parameters);
                if (amt == DBNull.Value)
                    return 0M;

                result = Convert.ToDecimal(amt);
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }

            return result;
        }

        public decimal GetAgentIncome(int agentid, DateTime sdt, DateTime edt)
        {
            decimal result = 0M;
            try
            {
                string sqlText = @"select sum(amt) from trade where billType = 2 and userid = @userid and tradeTime >= @begintime and tradeTime < @endtime";

                SqlParameter[] parameters = {    
                                            new SqlParameter("@userid", SqlDbType.Int, 4) 
                                           ,new SqlParameter("@begintime", SqlDbType.DateTime,8) 
                                           ,new SqlParameter("@endtime", SqlDbType.DateTime,8) 
                                        };
                parameters[0].Value = agentid;
                parameters[1].Value = sdt;
                parameters[2].Value = edt;

                object amt = DataBase.ExecuteScalar(CommandType.Text, sqlText, parameters);
                if (amt == DBNull.Value)
                    return 0M;

                result = Convert.ToDecimal(amt);
            }
            catch (Exception ex)
            {
                viviLib.ExceptionHandling.ExceptionHandler.HandleException(ex);
            }

            return result;
        }

        #region AgentStat4

        /// <summary>
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="typeid"></param>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet AgentStat4(int userid, int typeid, string sdt, string edt, int pagesize, int page, string orderby)
        {
            string sqlText = @"
select count(*) C from
(select mydate,typeId,userId
from usersOrderIncome with(nolock)
where 1=1
and (mydate >= @sdate or @sdate = '')
and (mydate >= @edate or @edate = '')
and (userId = @userId or @userId = 0)
and (typeId = @typeId or @typeId = 0)
group by mydate,typeId,userId) a

select mydate,userId,username,full_name,d.typeId,sumpay,pecent,P_ROW,f.modetypename
from(
select a.mydate,a.userId,c.username,c.full_name,a.typeId,a.sumpay,a.sumpay/b.total pecent,ROW_NUMBER() OVER(ORDER BY a.mydate) AS P_ROW
from 
(select mydate, userId, typeId,sum(sumpay) as sumpay
from usersOrderIncome with(nolock)
where 1=1
and (mydate >= @sdate or @sdate = '')
and (mydate >= @edate or @edate = '')
and (userId = @userId or @userId = 0)
and (typeId = @typeId or @typeId = 0)
group by mydate,typeId,userId ) a
left join (select mydate, userId,sum(sumpay) as total
from usersOrderIncome with(nolock)
where 1=1
and (mydate >= @sdate or @sdate = '')
and (mydate >= @edate or @edate = '')
and (userId = @userId or @userId = 0)
and (typeId = @typeId or @typeId = 0)
group by mydate,userId ) b on a.mydate=b.mydate
and a.userId = b.userId
left join userbase c on a.userId = c.id) d
left join channeltype f ON d.typeId = f.typeId
where P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize";
            //order by " + orderby;

            SqlParameter[] parameters =
            {
                new SqlParameter("@sdate", SqlDbType.VarChar, 10)
                , new SqlParameter("@edate", SqlDbType.VarChar, 10)
                , new SqlParameter("@userId", SqlDbType.Int, 4)
                , new SqlParameter("@typeId", SqlDbType.Int, 4)
                , new SqlParameter("@page", SqlDbType.Int, 4)
                , new SqlParameter("@pagesize", SqlDbType.Int, 4)
            };
            parameters[0].Value = sdt;
            parameters[1].Value = edt;
            parameters[2].Value = userid;
            parameters[3].Value = typeid;
            parameters[4].Value = page;
            parameters[5].Value = pagesize;

            return DataBase.ExecuteDataset(CommandType.Text, sqlText, parameters);
        }

        #endregion

        #region BusinessStat4

        /// <summary>
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet BusinessStat4(DateTime sdt, DateTime edt, int page, int pagesize, string orderby)
        {
            string sqlText = @"
select count(0) as C
	from(
	select manageid
	from v_order with(nolock)
	where manageid > 0  and processingtime >= @sdt and processingtime <= @edt 
	group by manageid) A


select D1.manageid,payAmt,promAmt,supplierAmt,realvalue,B.username,B.relname
from(
	select manageid,payAmt,promAmt,supplierAmt,realvalue,ROW_NUMBER() OVER(ORDER BY D.manageid) AS P_ROW 
	from(
	select manageid,sum(payAmt) payAmt,sum(commission) as promAmt,sum(supplierAmt) as supplierAmt,sum(realvalue) as realvalue
	from v_order with(nolock)
	where manageid > 0 and processingtime >= @sdt and processingtime <= @edt  
	group by manageid) D 
)D1  left join manage B with(nolock)  on D1.manageid = B.id
WHERE D1.P_ROW BETWEEN @page*@pagesize+1 AND @page*@pagesize+@pagesize
order by " + orderby;

            SqlParameter[] parameters =
            {
                new SqlParameter("@sdt", SqlDbType.DateTime, 8)
                , new SqlParameter("@edt", SqlDbType.DateTime, 8)
                , new SqlParameter("@page", SqlDbType.Int, 4)
                , new SqlParameter("@pagesize", SqlDbType.Int, 4)
            };
            parameters[0].Value = sdt;
            parameters[1].Value = edt;
            parameters[2].Value = page;
            parameters[3].Value = pagesize;


            return DataBase.ExecuteDataset(CommandType.Text, sqlText, parameters);
        }

        #endregion

        #region BusinessStat7

        /// <summary>
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="edt"></param>
        /// <returns></returns>
        public DataSet BusinessStat7(DateTime sdt, DateTime edt)
        {
            try
            {
                string sqlText = @"
select '网银利润' as class,sum(supplierAmt-isnull(payAmt,0)-isnull(promAmt,0)) amt
from v_orderbank with(nolock) 
where status=2 and typeid=102 and  processingtime>=@sdt and processingtime <= @edt

union all

select '支付宝利润',sum(supplierAmt-isnull(payAmt,0)-isnull(promAmt,0)) amt
from v_orderbank with(nolock) 
where status=2 and typeid=101 and  processingtime>=@sdt and processingtime <= @edt

union all

select '财付通利润',sum(supplierAmt-isnull(payAmt,0)-isnull(promAmt,0)) amt
from v_orderbank with(nolock) 
where status=2 and typeid=100 and  processingtime>=@sdt and processingtime <= @edt

union all

select '点卡利润',sum(supplierAmt-isnull(payAmt,0)-isnull(promAmt,0)) amt
from v_ordercard with(nolock) 
where status=2 and  processingtime>=@sdt and processingtime <= @edt";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@sdt", SqlDbType.DateTime, 8)
                    , new SqlParameter("@edt", SqlDbType.DateTime, 8)
                };
                parameters[0].Value = sdt;
                parameters[1].Value = edt;


                return DataBase.ExecuteDataset(CommandType.Text, sqlText, parameters);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        #endregion
    }
}