using Autofac.Extras.DynamicProxy2;
using Committinger.XStrangerService.ServiceInterface.DataContracts;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSSFramework.AOP;
using XSSFramework.Data;
using XSSFramework.Log;

namespace Committinger.XStrangerServic.Core.DA
{
    [Intercept(typeof(Logger))]
    public class UserDA : BaseDA
    {
        public static UserDA Instance { get { return ModuleInjector.Inject<UserDA>(); } }

        public virtual User GetUserByName(User user)
        {
            var helper = DataHelper.CreateHelper();
            string sql = @" select * from T_USER where name = @name";
            List<MySqlParameter> parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@name", MySqlDbType.String) { Value = user.Name });
            DataSet ds = helper.Query(sql, parameterList);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    User result = new User()
                    {
                        RecId = Convert.ToInt32(row["recid"]),
                        Name = user.Name
                    };
                    return result;
                }
            return null;
        }
    }
}

