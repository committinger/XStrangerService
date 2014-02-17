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
    public class MessageDA
    {
        public static MessageDA Instance { get { return ModuleInjector.Inject<MessageDA>(); } }





        internal int SaveMessage(MessageData msg)
        {
            var helper = DataHelper.CreateHelper();
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("INSERT INTO `xss`.`T_MESSAGE`(`type`,`from`,`to`,`content`,`forwardtime`)");
            sqlBuilder.Append("VALUES(@type,@from,@to,@content,@forwardtime)");

            IEnumerable<DbParameter> parameterList = new List<MySqlParameter>(){
            new MySqlParameter("@type", MySqlDbType.String) { Value = msg.MessageType },
            new MySqlParameter("@from", MySqlDbType.String) { Value = msg.UserFrom },
            new MySqlParameter("@to", MySqlDbType.String) { Value = msg.UserTo },
            new MySqlParameter("@content", MySqlDbType.String) { Value =msg.Content },
            new MySqlParameter("@forwardtime", MySqlDbType.DateTime) { Value = DateTime.Now }
            };

            return helper.InsertAndGetId(sqlBuilder.ToString(), parameterList);
        }

        public virtual MessageCollectionData GetMessage(int sequence, string userName)
        {
            MessageCollectionData data = new MessageCollectionData()
            {
                Sequence = sequence,
                UserFrom = userName,
                MessageList = new List<MessageData>()
            };
            var helper = DataHelper.CreateHelper();
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select * from T_MESSAGE");
            sqlBuilder.Append(" where recid>@sequence and T_MESSAGE.to=@user");
            //sqlBuilder.Append(" where recid>@sequence and (T_MESSAGE.from = @user or T_MESSAGE.to=@user)");
            sqlBuilder.Append(" order by recid");
            List<MySqlParameter> parameterList = new List<MySqlParameter>();
            parameterList.Add(new MySqlParameter("@sequence", MySqlDbType.Int32) { Value = sequence });
            parameterList.Add(new MySqlParameter("@user", MySqlDbType.String) { Value = userName });
            DataSet ds = helper.Query(sqlBuilder.ToString(), parameterList);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        MessageData msg = null;
                        if (Convert.ToInt32(row["type"]) == MessageType.ConversationStart)
                        {
                            msg = new ConversationControlData()
                            {
                                sequence = Convert.ToInt32(row["recid"]),
                                UserTo = Convert.ToString(row["to"]),
                                UserFrom = Convert.ToString(row["from"]),
                                Content = Convert.ToString(row["content"]),
                                MessageType = Convert.ToInt32(row["type"]),
                                Time = Convert.ToDateTime(row["forwardtime"]).ToString("yyyy-MM-dd HH:mm:ss +0800"),
                                MaxPeriodSec = ConversationModule.ConstantPeriodMaxSec,
                                MinPeriodSec = ConversationModule.ConstantPeriodMinSec,
                            };
                        }
                        else
                        {
                            msg = new MessageData()
                            {
                                sequence = Convert.ToInt32(row["recid"]),
                                UserTo = Convert.ToString(row["to"]),
                                UserFrom = Convert.ToString(row["from"]),
                                Content = Convert.ToString(row["content"]),
                                MessageType = Convert.ToInt32(row["type"]),
                                Time = Convert.ToDateTime(row["forwardtime"]).ToString("yyyy-MM-dd HH:mm:ss +0800")
                            };
                        }
                        data.MessageList.Add(msg);
                        data.Sequence = msg.sequence;
                    }
                }
            return data;
        }
    }
}
