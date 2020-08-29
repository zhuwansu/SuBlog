/**********************************************

SafeitemName   : MySqlProvider.cs
CLRVersion     : 4.0.30319.42000
Machine        : ZWS
Overview       : 
Author         : zws
Time           : 2020/8/29 14:44:35
Version        : V1.0.0.0

**********************************************/

using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DBProvider
{
    public sealed class MySqlProvider : DatabaseProvider
    {
        public DbProviderFactory Factory => MySqlClientFactory.Instance;

        public string GetConnectionString()
        {
            return "Server=localhost;Database=SuBlog;Uid=root;Pwd=123456;";
        }

        public DbConnection GetMySqlConnection(bool open = true,  bool convertZeroDatetime = false, bool allowZeroDatetime = false)
        {
            string cs = GetConnectionString();
            var csb = Factory.CreateConnectionStringBuilder();
            csb.ConnectionString = cs;
            ((dynamic)csb).AllowZeroDateTime = allowZeroDatetime;
            ((dynamic)csb).ConvertZeroDateTime = convertZeroDatetime;
            var conn = Factory.CreateConnection();
            conn.ConnectionString = csb.ConnectionString;
            if (open) conn.Open();
            return conn;
        }
    }
}
