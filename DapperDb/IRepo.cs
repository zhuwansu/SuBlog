/*******************************************************
 * 
 * 作者：朱皖苏
 * 创建日期：20200829
 * 说明：此文件只包含一个类，具体内容见类型注释。
 * 运行环境：.NET Stanard 2.0
 * 版本号：1.0.0
 * 
 * 历史记录：
 * 创建文件 朱皖苏 20200829 14:42
 * 
*******************************************************/

using Dapper;
using DbEntities;
using DBProvider;

namespace DapperDb
{
    public partial interface IRepo<T> : IRepo where T : IEntity
    {
        public virtual async System.Threading.Tasks.Task<T> GetAsync()
        {
            return await GetEntityAsync<T>();
        }
    }

    public partial interface IRepo
    {
        public async System.Threading.Tasks.Task<T> GetEntityAsync<T>() where T : IEntity
        {
            using var cnn = new MySqlProvider().GetMySqlConnection();

            return await cnn.QueryFirstAsync<T>("select * from Account");
        }
    }
}
