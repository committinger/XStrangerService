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
    public class CircleDA : BaseDA
    {
        public static CircleDA Instance
        {
            get
            { return ModuleInjector.Inject<CircleDA>(); }
        }

        public virtual Circle GetCircleByKey(string key)
        {
            var helper = DataHelper.CreateHelper();
            string sql = @" select * from T_CIRCLE where find_in_set(@keywords, keywords) ";
            List<MySqlParameter> parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@keywords", MySqlDbType.String) { Value = key });
            DataSet ds = helper.Query(sql, parameterList);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    Circle result = new Circle()
                    {
                        RecId = Convert.ToInt32(row["recid"]),
                        Keywords = Convert.ToString(row["keywords"]).Split(ConstantSplitters, StringSplitOptions.RemoveEmptyEntries),
                        Body = new CircleData()
                        {
                            Name = Convert.ToString(row["name"]),
                            Description = Convert.ToString(row["description"]),
                            IconUrl = Convert.ToString(row["iconUrl"]),
                            ImageUrls = Convert.ToString(row["imageUrls"]).Split(ConstantSplitters, StringSplitOptions.RemoveEmptyEntries)
                        }
                    };
                    return result;
                }
            return null;
        }
    }
}
