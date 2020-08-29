/**********************************************

SafeitemName   : Base.cs
CLRVersion     : 4.0.30319.42000
Machine        : ZWS
Overview       : 
Author         : zws
Time           : 2020/8/29 15:43:38
Version        : V1.0.0.0

**********************************************/


using DbEntities;
using System.Threading.Tasks;

namespace DapperDb.Repos
{
    public abstract class BaseRepo<T> : IRepo<T>, IReader<T> where T : IEntity
    {
        public async Task<T> GetAsync()
        {
            return await (this as IRepo).GetEntityAsync<T>();
        }
    }
}
