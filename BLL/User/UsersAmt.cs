using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using viviapi.BLL.Finance;
using viviapi.Model.User;
using viviLib.ExceptionHandling;
using viviLib.Data;
using DBAccess;

namespace viviapi.BLL.User
{
    public class UsersAmt
    {
        private static DAL.User.UsersAmt Dal = new DAL.User.UsersAmt();


        public static UsersAmtInfo GetModel(int userId)
        {
            try
            {
                return Dal.GetModel(userId);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        public static DataSet PageSearch(List<SearchParam> searchParams, int pageSize, int page, string orderby)
        {
            try
            {
                return Dal.PageSearch(searchParams, pageSize, page, orderby);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        public static DataSet GetList(string strWhere)
        {
            try
            {
                return Dal.GetList(strWhere);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// 取用户可用余额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static decimal GetUserAvailableBalance(int userID)
        {
            try
            {
                decimal balanceAmt = 0M;

                var usersAmt = GetModel(userID);

                if (usersAmt != null)
                {
                    balanceAmt = usersAmt.Balance;

                    decimal freezeAmt = usersAmt.Freeze;
                    decimal withdrawingAmt = usersAmt.Unpayment;
                    decimal detentionAmt = Trade.GetUserTotalDetentionAmt(userID);

                    balanceAmt = balanceAmt - freezeAmt - withdrawingAmt - detentionAmt;

                    if (balanceAmt < 0M)
                        balanceAmt = 0M;
                }
                return balanceAmt;

            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return 0M;
            }
        }

    }
}
